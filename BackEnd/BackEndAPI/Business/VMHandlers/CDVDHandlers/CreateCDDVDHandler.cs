using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs;
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

namespace BackEndAPI.Business.VMHandlers.CDVDHandlers
{
    public class CreateCDDVDHandler : IRequestHandler<CreateCDVD, CDDVD>
    {
        private readonly DataContext context;

        public CreateCDDVDHandler(DataContext context)
        {
            this.context = context;
        }
        public async Task<CDDVD> Handle(CreateCDVD request, CancellationToken cancellationToken)
        {
            //creeaza un CDDVD si il ataseaza unui SCSI Controller deja existent care se poate gasi dupa sc.InstanceID
       
            
            var sc = await context.SCs.SingleOrDefaultAsync(u => u.SCId == request.SCId);
            if (sc == null)
            {
                throw new Exception("Requested SCSI Controller doesn't exist!");
            }
            var vm = await context.VMs.SingleOrDefaultAsync(v => v.VMId == sc.VMId);
            if(vm==null)
            {
                throw new Exception("Requested virtual machine doesn't exist!");
            }
            var viridianUtils = new ViridianUtils();
            var computerSystem = ComputerSystem.GetInstances().Where((cs) => cs.Name == vm.RealID).ToList().First();
            var virtualSystemSettingData = SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();

            var isoName = $"{"Resources"}\\{vm.RealID}\\{vm.RealID}.iso";
        
            var AffectedConfiguration = virtualSystemSettingData.Path;
            var scsiController = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 6, "Microsoft:Hyper-V:Synthetic SCSI Controller").Where(s => s.InstanceID == sc.InstanceId).First(); 
            var primordialResourcePoolDVDDrive = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic DVD Drive");
            var allocationCapabilitiesDVDDrive = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolDVDDrive);
            var resourceAllocationSettingDataDVDDrive = ViridianUtils.GetDefaultResourceAllocationSettingData(allocationCapabilitiesDVDDrive);

            resourceAllocationSettingDataDVDDrive.LateBoundObject["Parent"] = scsiController.Path;
            resourceAllocationSettingDataDVDDrive.LateBoundObject["AddressOnParent"] = 0;

            var ResourceSettings = new string[] { resourceAllocationSettingDataDVDDrive.LateBoundObject.GetText(TextFormat.WmiDtd20) };

            var ReturnValue=viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out ManagementPath Job, out ManagementPath[] ResultingResourceSettings);

            var synthethicDVD = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 16, "Microsoft:Hyper-V:Synthetic DVD Drive").First();
            var primordialResourcePoolVirtualCDDVD = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Virtual CD/DVD Disk");
            var allocationCapabilitiesVirtualCDDVD = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolVirtualCDDVD);
            var resourceAllocationSettingVirtualCDDVD = ViridianUtils.GetDefaultStorageAllocationSettingData(allocationCapabilitiesVirtualCDDVD);

            Directory.CreateDirectory(Path.GetDirectoryName(isoName)); // make sure you create the directory if it's missing
            var isoFile = File.Create(isoName);

            isoFile.Close();

            resourceAllocationSettingVirtualCDDVD.LateBoundObject["Address"] = 0;
            resourceAllocationSettingVirtualCDDVD.LateBoundObject["Parent"] = synthethicDVD.Path;
            resourceAllocationSettingVirtualCDDVD.LateBoundObject["HostResource"] = new[] { Path.GetFullPath(isoName) };

            ResourceSettings = new string[] { resourceAllocationSettingVirtualCDDVD.LateBoundObject.GetText(TextFormat.WmiDtd20) };
            ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);
            
            var virtualCDDVDCollection =
                              ViridianUtils.GetStorageAllocationSettingDataList(virtualSystemSettingData, 31, "Microsoft:Hyper-V:Virtual CD/DVD Disk")
                                  .Where((sasd) => string.Compare(sasd.Caption, "ISO Disk Image", true, CultureInfo.InvariantCulture) == 0)
                                  .ToList();
            var lastCDDVD = virtualCDDVDCollection.LastOrDefault();
            var cddvd = CDDVD.Create(lastCDDVD.InstanceID,lastCDDVD.ElementName,sc.SCId);
            context.CDVDs.Add(cddvd);

            await context.SaveChangesAsync(cancellationToken);

            return cddvd;
        }
    }
}
