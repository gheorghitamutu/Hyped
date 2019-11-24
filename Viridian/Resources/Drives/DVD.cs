using System.Management;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Service.Msvm;
using Viridian.Utilities;

namespace Viridian.Resources.Drives
{
    public class DVD
    {
        public void AddToScsi(VM vm, int controllerSlot, int driveSlot)
        {
            using (var vms = Utils.GetVirtualMachineSettings(vm.VmName, vm.Scope))
            using (var dvd = Utils.GetWmiObject(vm.Scope, "Msvm_ResourcePool", "ResourceSubType = 'Microsoft:Hyper-V:Synthetic DVD Drive'"))
            using (var rasd = ResourceAllocationSettingData.GetDefaultAllocationSettings(dvd))
            using (var rasdClone = rasd.Clone() as ManagementObject)
            {
                using (var parent = vm.GetScsiController(controllerSlot))
                {
                    rasdClone["Parent"] = parent;
                    rasdClone["AddressOnParent"] = driveSlot;

                    VirtualSystemManagement.Instance.AddResourceSettings(vms, new[] { rasdClone.GetText(TextFormat.WmiDtd20) });
                }
            }
        }

        public bool IsISOAttached(VM vm, int scsiIndex, int driveIndex)
        {
            using (var scsi = vm.GetScsiController(scsiIndex))
            using (var dvd = Utils.GetScsiControllerChildBySubtypeAndIndex(scsi, Utils.GetResourceSubType("SyntheticDVD"), driveIndex))
            using (var dvdChildren = dvd.GetRelated("Msvm_StorageAllocationSettingData", null, null, null, "Dependent", "Antecedent", false, null))
                foreach (var media in dvdChildren)
                    if (media["Caption"].ToString() == "ISO Disk Image")
                        return true;

            return false;
        }
    }
}
