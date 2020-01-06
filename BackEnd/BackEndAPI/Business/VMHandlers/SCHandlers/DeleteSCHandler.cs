using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs.SCDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;
using ViridianTester;

namespace BackEndAPI.Business.VMHandlers.SCHandlers
{
    public class DeleteSCHandler:IRequestHandler<DeleteSC>
    {
        private readonly DataContext context;

        public DeleteSCHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteSC request, CancellationToken cancellationToken)
        {
            //sterge un SCSI Controller care se poate gasi dupa sc.InstanceID
            
            var sc = await context.SCs.SingleOrDefaultAsync(u => u.SCId == request.SCId);
            if (sc == null)
            {
                throw new Exception("This SCSI Contorller doesn't exist!");
            }
            var vm = await context.VMs.SingleOrDefaultAsync(v => v.VMId == sc.VMId);
            if(vm==null)
            {
                throw new Exception("Requested virtual machine doesn't exist!");
            }

            var vhds = await context.VHDs.ToListAsync();
            var cddvds = await context.CDVDs.ToListAsync();

            var sc_vhds = vhds.Where((vhd) => vhd.SCId == sc.SCId).ToList();
            var sc_cddvds = cddvds.Where((cddvd) => cddvd.SCId == sc.SCId).ToList();
           // sc.Update(sc.Name, sc.InstanceId, vhds.Where((vhd) => vhd.SCId == sc.SCId).ToList(), cddvds.Where((cddvd) => cddvd.SCId == sc.SCId).ToList(), sc.VMId);//add all the cddvds and vhds to this SCSI controller collection

            foreach (var vhd in sc_vhds)//for each vhd of this SCSI controller
            {
                context.VHDs.Remove(vhd);
                await context.SaveChangesAsync(cancellationToken);
            }
            foreach (var cddvd in sc_cddvds)//for each cddvd of this SCSI controller
            {
                context.CDVDs.Remove(cddvd);
                await context.SaveChangesAsync(cancellationToken);
            }

            var viridianUtils = new ViridianUtils();
            var computerSystem = ComputerSystem.GetInstances().Where((cs) => cs.Name == vm.RealID).ToList().First();
            var virtualSystemSettingData = SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();

            var rasdCollection = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 6, "Microsoft:Hyper-V:Synthetic SCSI Controller");
            var scontroller = rasdCollection.Where(r => r.InstanceID == sc.InstanceId).FirstOrDefault();
            //TODO: remove this scsi controller
            var RemoveResourceSettings = new ManagementPath[] { scontroller.Path };
            var ReturnValue = viridianUtils.VSMS.RemoveResourceSettings(RemoveResourceSettings, out ManagementPath Job);
            

            //remove this SCSI controller
            context.SCs.Remove(sc);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
