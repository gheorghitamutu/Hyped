using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs;
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

namespace BackEndAPI.Business.VMHandlers.VHDHandlers
{
    public class DeleteVHDHandler:IRequestHandler<DeleteVHD>
    {
        private readonly DataContext context;

        public DeleteVHDHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteVHD request, CancellationToken cancellationToken)
        {
            //sterge un VHD care se poate gasi dupa vhd.InstanceID
            var vhd =await context.VHDs.SingleOrDefaultAsync(n => n.VHDId == request.VHDId);
            if (vhd == null)
            {
                throw new Exception("Requested vhd could not be found");
            }
            var sc = await context.SCs.SingleOrDefaultAsync(s=>s.SCId==vhd.SCId);
            if(sc==null)
            {
                throw new Exception("The SCSI Controller of this VHD could not be found!");
            }
            var vm = await context.VMs.SingleOrDefaultAsync(v => v.VMId == sc.VMId);
            if (vm == null)
            {
                throw new Exception("The virtual machine of this SCSI Controller and VHD could not be found!");
            }

            var viridianUtils = new ViridianUtils();
            var computerSystem =
                ComputerSystem.GetInstances()
                    .Cast<ComputerSystem>()
                    .Where((cs) => cs.Name == vm.RealID)
                    .ToList()
                    .First();

            var virtualSystemSettingData =
                    SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();
            var AffectedConfiguration = virtualSystemSettingData.Path;
            var virtualHardDiskCollection =
                            ViridianUtils.GetStorageAllocationSettingDataList(virtualSystemSettingData, 31, "Microsoft:Hyper-V:Virtual Hard Disk")
                                    .Where((sasd) => string.Compare(sasd.Caption, "Hard Disk Image", true, CultureInfo.InvariantCulture) == 0)
                                    .ToList();
            var thisVHD = virtualHardDiskCollection.Where(hd => hd.InstanceID == vhd.InstanceId).FirstOrDefault();
            var RemoveResourceSettings = new ManagementPath[] { thisVHD.Path };
            var ReturnValue = viridianUtils.VSMS.RemoveResourceSettings(RemoveResourceSettings, out ManagementPath Job);

         
            context.VHDs.Remove(vhd);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
