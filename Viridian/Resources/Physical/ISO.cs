using System.Management;
using Viridian.Exceptions;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Service.Msvm;
using Viridian.Utilities;

namespace Viridian.Resources.Physical
{
    public class ISO
    {
        public void AddIso(VM vm, string hostResource, int scsiIndex, int address)
        {
            using (var scsi = vm.GetScsiController(scsiIndex))
            using (var parent = Utils.GetScsiControllerChildBySubtypeAndIndex(scsi, Utils.GetResourceSubType("SyntheticDVD"), address))
            using (var dvd = Utils.GetWmiObject(vm.Scope, "Msvm_ResourcePool", "ResourceSubType = 'Microsoft:Hyper-V:Virtual CD/DVD Disk' and Primordial = True"))
            using (var sasd = ResourceAllocationSettingData.GetDefaultAllocationSettings(dvd))
            using (var sasdClone = sasd.Clone() as ManagementObject)
            {
                if (sasdClone == null)
                    throw new ViridianException("Failure retrieving default settings!");

                sasdClone["Address"] = address;
                sasdClone["Parent"] = parent ?? throw new ViridianException("Failure retrieving Virtual CD/DVD Disk class!");
                sasdClone["HostResource"] = new[] { hostResource };

                using (var vms = Utils.GetVirtualMachineSettings(vm.VmName, vm.Scope))
                    VirtualSystemManagement.Instance.AddResourceSettings(vms, new[] { sasdClone.GetText(TextFormat.WmiDtd20) });
            }
        }
    }
}
