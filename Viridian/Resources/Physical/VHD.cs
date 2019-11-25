using System.Management;
using Viridian.Exceptions;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Service.Msvm;
using Viridian.Utilities;

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

        public void AddToSyntheticDiskDrive(VM vm, string hostResource, int scsiIndex, int address, HardDiskAccess access)
        {
            using (var vms = Utils.GetVirtualMachineSettings(vm.VmName, vm.Scope))
            using (var scsiController = vm.GetScsiController(scsiIndex))
            using (var parent = Utils.GetScsiControllerChildBySubtypeAndIndex(scsiController, Utils.GetResourceSubType("SyntheticDisk"), address))
            using (var rp = Utils.GetWmiObject(vm.Scope, "Msvm_ResourcePool", "ResourceSubType = 'Microsoft:Hyper-V:Virtual Hard Disk' and Primordial = True"))
            using (var rasd = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(rp))
            {
                rasd["Access"] = (ushort)access;
                rasd["Address"] = address;
                rasd["Parent"] = parent ?? throw new ViridianException("Failure retrieving Syntethic Disk Drive class!");
                rasd["HostResource"] = new[] { hostResource };

                VirtualSystemManagement.Instance.AddResourceSettings(vms, new[] { rasd.GetText(TextFormat.WmiDtd20) });
            }
        }

        public static void RemoveFromSyntheticDiskDrive(VM vm, string vhdPath, bool removeParent)
        {
            using (var vms = Utils.GetVirtualMachineSettings(vm.VmName, vm.Scope))
            using (var sasd = vms.GetRelated("Msvm_StorageAllocationSettingData", null, null, null, null, null, false, null))
            {
                ManagementBaseObject resourceSettings = null;

                foreach (var settings in sasd)
                {
                    if (settings == null) 
                        continue;

                    if (((string[])settings["HostResource"])[0] != vhdPath)
                        continue;

                    resourceSettings = settings;
                    break;
                }

                if (resourceSettings == null)
                    throw new ViridianException("Resource containing the vhd path not found!");

                VirtualSystemManagement.Instance.RemoveResourceSettings(new[] { resourceSettings });

                if (removeParent)
                    VirtualSystemManagement.Instance.RemoveResourceSettings(new[] { resourceSettings["Parent"] as ManagementBaseObject });
            }
        }

        public static bool IsVHDAttached(VM vm, int scsiIndex, int driveIndex)
        {
            using (var scsi = vm.GetScsiController(scsiIndex))
            using (var dvd = Utils.GetScsiControllerChildBySubtypeAndIndex(scsi, Utils.GetResourceSubType("SyntheticDisk"), driveIndex))
            using (var dvdChildren = dvd.GetRelated("Msvm_StorageAllocationSettingData", null, null, null, "Dependent", "Antecedent", false, null))
                foreach (var media in dvdChildren)
                    if (media["Caption"].ToString() == "Hard Disk Image")
                        return true;

            return false;
        }
    }
}
