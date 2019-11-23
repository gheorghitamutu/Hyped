using System.Management;
using Viridian.Exceptions;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Storage.Virtual.Hard;
using Viridian.Utilities;

namespace Viridian.Resources.Drives
{
    public class VHD
    {
        public void AddToSyntheticDiskDrive(VM vm, string hostResource, int scsiIndex, int address, HardDiskAccess access)
        {
            using (var vmms = Utils.GetVirtualMachineManagementService(vm.Scope))
            using (var vms = Utils.GetVirtualMachineSettings(vm.VmName, vm.Scope))
            using (var scsiController = vm.GetScsiController(scsiIndex))
            using (var parent = Utils.GetScsiControllerChildBySubtypeAndIndex(scsiController, Utils.GetResourceSubType("SyntheticDisk"), address))
            using (var rp = Utils.GetWmiObject(vm.Scope, "Msvm_ResourcePool", "ResourceSubType = 'Microsoft:Hyper-V:Virtual Hard Disk' and Primordial = True"))
            using (var rasd = ResourceAllocationSettingData.GetDefaultAllocationSettings(rp))
            using (var rasdClone = rasd.Clone() as ManagementObject)
            {
                if (rasdClone == null)
                    throw new ViridianException("Failure retrieving default settings!");

                rasdClone["Access"] = (ushort)access;
                rasdClone["Address"] = address;
                rasdClone["Parent"] = parent ?? throw new ViridianException("Failure retrieving Syntethic Disk Drive class!");
                rasdClone["HostResource"] = new[] { hostResource };

                using (var ip = vmms.GetMethodParameters("AddResourceSettings"))
                {
                    ip["AffectedConfiguration"] = vms;
                    ip["ResourceSettings"] = new[] { rasdClone.GetText(TextFormat.WmiDtd20) };

                    using (var op = vmms.InvokeMethod("AddResourceSettings", ip, null))
                        Job.Validator.ValidateOutput(op, vm.Scope);
                }
            }
        }

        public static void RemoveFromSyntheticDiskDrive(VM vm, string vhdPath, bool removeParent)
        {
            using (var vmms = Utils.GetVirtualMachineManagementService(vm.Scope))
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

                using (var ip = vmms.GetMethodParameters("RemoveResourceSettings"))
                {
                    ip["ResourceSettings"] = new[] { resourceSettings };

                    using (var outParams = vmms.InvokeMethod("RemoveResourceSettings", ip, null))
                        Job.Validator.ValidateOutput(outParams, vm.Scope);

                    if (!removeParent)
                        return;

                    ip["ResourceSettings"] = new[] { resourceSettings["Parent"] };

                    using (var outParams = vmms.InvokeMethod("RemoveResourceSettings", ip, null))
                        Job.Validator.ValidateOutput(outParams, vm.Scope);
                }
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
