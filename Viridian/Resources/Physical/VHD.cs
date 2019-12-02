using System.Linq;
using System.Management;
using Viridian.Exceptions;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;
using Viridian.Resources.Controllers;

namespace Viridian.Resources.Drives
{
    public class VHD
    {
        public enum HardDiskAccess
        {
            Unknown = 0,
            Readable = 1,
            Writeable = 2,
            ReadWrite = 3
        }

        public string[] AddToSyntheticDiskDrive(ComputerSystem vm, string hostResource, int scsiIndex, int address, HardDiskAccess access)
        {
            using (var scsiController = vm.GetScsiController(scsiIndex))
            using (var parent = SCSI.GetScsiControllerChildBySubtypeAndIndex(scsiController, ResourcePool.ResourceTypeInfo.SyntheticDiskDrive.ResourceSubType, address))
            using (var pool = ResourcePool.GetPool(ResourcePool.ResourceTypeInfo.VirtualHardDisk.ResourceSubType))
            using (var rasd = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(pool))
            {
                rasd["Access"] = (ushort)access;
                rasd["Address"] = address;
                rasd["Parent"] = parent ?? throw new ViridianException("Failure retrieving Syntethic Disk Drive class!");
                rasd["HostResource"] = new[] { hostResource };

                return VirtualSystemManagementService.Instance.AddResourceSettings(vm.VirtualSystemSettingData.MsvmVirtualSystemSettingData, new[] { rasd.GetText(TextFormat.WmiDtd20) });
            }
        }

        public static void RemoveFromSyntheticDiskDrive(ComputerSystem vm, string vhdPath)
        {
            vm.VirtualSystemSettingData.MsvmVirtualSystemSettingData.GetRelated("Msvm_StorageAllocationSettingData", null, null, null, null, null, false, null)
                .Cast<ManagementObject>()
                .Where((settings) => ((string[])settings?["HostResource"])[0] == vhdPath)
                .ToList()
                .ForEach((settings) => VirtualSystemManagementService.Instance.RemoveResourceSettings(new[] { settings }));
        }

        public static bool IsVHDAttached(ComputerSystem vm, int scsiIndex, int driveIndex)
        {
            using (var scsi = vm?.GetScsiController(scsiIndex))
            using (var dvd = SCSI.GetScsiControllerChildBySubtypeAndIndex(scsi, ResourcePool.ResourceTypeInfo.SyntheticDiskDrive.ResourceSubType, driveIndex))
                return
                    dvd.GetRelated("Msvm_StorageAllocationSettingData", null, null, null, "Dependent", "Antecedent", false, null)
                        .Cast<ManagementObject>()
                        .Where((media) => media["Caption"].ToString() == "Hard Disk Image")
                        .Any();
        }
    }
}
