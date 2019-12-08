using System.Linq;
using System.Management;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;

namespace Viridian.Resources.Drives
{
    public class DVD
    {
        public void AddToScsi(ComputerSystem vm, int controllerSlot, int driveSlot)
        {
            using (var pool = ResourcePool.GetPool(ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType))
            using (var rasd = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(pool))
            using (var parent = vm?.VirtualSystemSettingData.GetScsiController(controllerSlot))
            {
                rasd["Parent"] = parent;
                rasd["AddressOnParent"] = driveSlot;

                VirtualSystemManagementService.Instance.AddResourceSettings(vm.VirtualSystemSettingData.MsvmVirtualSystemSettingData, new[] { rasd.GetText(TextFormat.WmiDtd20) });
            }
        }

        public bool IsISOAttached(ComputerSystem vm, int scsiIndex, int driveIndex)
        {
            using (var scsi = vm?.VirtualSystemSettingData.GetScsiController(scsiIndex))
            using (var dvd = ResourceAllocationSettingData.GetRelatedResourceAllocationSettingData(scsi, ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType, driveIndex))
                return
                    dvd.GetRelated("Msvm_StorageAllocationSettingData", null, null, null, "Dependent", "Antecedent", false, null)
                        .Cast<ManagementObject>()
                        .Where((c) => c["Caption"]?.ToString() == "ISO Disk Image")
                        .Any();
        }
    }
}
