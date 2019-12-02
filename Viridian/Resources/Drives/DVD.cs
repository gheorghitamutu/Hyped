using System.Linq;
using System.Management;
using Viridian.Machine;
using Viridian.Resources.Controllers;
using Viridian.Resources.Msvm;
using Viridian.Service.Msvm;

namespace Viridian.Resources.Drives
{
    public class DVD
    {
        public void AddToScsi(ComputerSystem vm, int controllerSlot, int driveSlot)
        {
            using (var vms = ComputerSystem.GetVirtualMachineSettings(vm?.ElementName))
            using (var pool = ResourcePool.GetPool(ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType))
            using (var rasd = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(pool))
            using (var parent = vm.GetScsiController(controllerSlot))
            {
                rasd["Parent"] = parent;
                rasd["AddressOnParent"] = driveSlot;

                VirtualSystemManagement.Instance.AddResourceSettings(vms, new[] { rasd.GetText(TextFormat.WmiDtd20) });
            }
        }

        public bool IsISOAttached(ComputerSystem vm, int scsiIndex, int driveIndex)
        {
            using (var scsi = vm?.GetScsiController(scsiIndex))
            using (var dvd = SCSI.GetScsiControllerChildBySubtypeAndIndex(scsi, ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType, driveIndex))
                return
                    dvd.GetRelated("Msvm_StorageAllocationSettingData", null, null, null, "Dependent", "Antecedent", false, null)
                        .Cast<ManagementObject>()
                        .Where((c) => c["Caption"]?.ToString() == "ISO Disk Image")
                        .Any();
        }
    }
}
