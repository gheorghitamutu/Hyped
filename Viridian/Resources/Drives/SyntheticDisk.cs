using System.Management;
using Viridian.Exceptions;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;

namespace Viridian.Resources.Drives
{
    public class SyntheticDisk
    {
        public void AddToScsi(ComputerSystem vm, int scsiIndex, int addressOnParent)
        {
            using (var pool = ResourcePool.GetPool(ResourcePool.ResourceTypeInfo.SyntheticDiskDrive.ResourceSubType))
            using (var rasd = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(pool))
            using (var scsiController = vm.VirtualSystemSettingData.GetScsiController(scsiIndex))
            {
                rasd["Parent"] = scsiController ?? throw new ViridianException("Failure retrieving SCSI Controller class!");
                rasd["AddressOnParent"] = addressOnParent;

                VirtualSystemManagementService.Instance.AddResourceSettings(vm.VirtualSystemSettingData.MsvmVirtualSystemSettingData, new[] { rasd.GetText(TextFormat.WmiDtd20) });
            }
        }
    }
}
