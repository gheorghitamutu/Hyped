using BackEnd.Data;
using BackEnd.DTOs.VMDTOs.CDVDDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;
using ViridianTester;

namespace BackEnd.Business.VMHandlers.CDVDHandlers
{
    public class DeleteCDDVDHandler : IRequestHandler<DeleteCDVD>
    {
        private readonly DataContext context;

        public DeleteCDDVDHandler(DataContext context)
        {
            this.context = context;
        }
        public async Task<Unit> Handle(DeleteCDVD request, CancellationToken cancellationToken)
        {
            //sterge un CDDVD dupa cddvd.InstanceID
            
            var cddvd = await context.CDVDs.SingleOrDefaultAsync(c => c.CDDVDId == request.CDDVDId);
            if (cddvd == null)
            {
                throw new Exception("Requested CDDVD could not be found");
            }
            var sc = await context.SCs.SingleOrDefaultAsync(s => s.SCId == cddvd.SCId);
            if(sc==null)
            {
                throw new Exception("Could not find the SCSI Controller of this CDDVD!");
            }
            var vm = await context.VMs.SingleOrDefaultAsync(v => v.VMId == sc.VMId);
            if(vm==null)
            {
                throw new Exception("Requested virtual machine could not be found!");
            }

            var viridianUtils = new ViridianUtils();
            var computerSystem = ComputerSystem.GetInstances().Where((cs) => cs.Name == vm.RealID).ToList().First();
            var virtualSystemSettingData = SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();

            var virtualCDDVDCollection =
                                ViridianUtils.GetStorageAllocationSettingDataList(virtualSystemSettingData, 31, "Microsoft:Hyper-V:Virtual CD/DVD Disk")
                                    .Where((sasd) => string.Compare(sasd.Caption, "ISO Disk Image", true, CultureInfo.InvariantCulture) == 0)
                                    .ToList();
            var thisCDDVD = virtualCDDVDCollection.Where(cd => cd.InstanceID == cddvd.InstanceId).FirstOrDefault();
            var RemoveResourceSettings = new ManagementPath[] { thisCDDVD.Path };//virtualCDDVDCollection.First().Path
            var ReturnValue = viridianUtils.VSMS.RemoveResourceSettings(RemoveResourceSettings, out ManagementPath Job);
            var scsiController = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 6, "Microsoft:Hyper-V:Synthetic SCSI Controller").Where(s => s.InstanceID == sc.InstanceId).FirstOrDefault();

            File.Delete(cddvd.Name);
            //var synthethicDVD = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 16, "Microsoft:Hyper-V:Synthetic DVD Drive").First();
            RemoveResourceSettings = new ManagementPath[] { thisCDDVD.Path };
            ReturnValue = viridianUtils.VSMS.RemoveResourceSettings(RemoveResourceSettings, out Job);

            RemoveResourceSettings = new ManagementPath[] { scsiController.Path };
            ReturnValue = viridianUtils.VSMS.RemoveResourceSettings(RemoveResourceSettings, out Job);

            context.CDVDs.Remove(cddvd);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
