using System.Management;
using Viridian.Exceptions;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;
using Viridian.Resources.Controllers;

namespace Viridian.Resources.Physical
{
    public class ISO
    {
        public void AddIso(ComputerSystem vm, string hostResource, int scsiIndex, int address)
        {
            using (var pool = ResourcePool.GetPool(ResourcePool.ResourceTypeInfo.VirtualCDDVDDisk.ResourceSubType))
            using (var sasd = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(pool))
            using (var scsi = vm.GetScsiController(scsiIndex))
            using (var parent = SCSI.GetScsiControllerChildBySubtypeAndIndex(scsi, ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType, address))
            {
                sasd["Address"] = address;
                sasd["Parent"] = parent ?? throw new ViridianException("Failure retrieving Virtual CD/DVD Disk class!");
                sasd["HostResource"] = new[] { hostResource };

                VirtualSystemManagementService.Instance.AddResourceSettings(vm.VirtualSystemSettingData.MsvmVirtualSystemSettingData, new[] { sasd.GetText(TextFormat.WmiDtd20) });
            }
        }
    }
}
