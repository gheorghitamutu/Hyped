using System.Management;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Service.Msvm;
using Viridian.Utilities;

namespace Viridian.Resources.Controllers
{
    public class SCSI
    {
        public void AddToVm(VM vm)
        {
            using(var vms = Utils.GetVirtualMachineSettings(vm.VmName, vm.Scope))
            using (var scsi = Utils.GetWmiObject(vm.Scope, "Msvm_ResourcePool", "ResourceSubType = 'Microsoft:Hyper-V:Synthetic SCSI Controller' and Primordial = True"))
            using (var rasd = ResourceAllocationSettingData.GetDefaultAllocationSettings(scsi))
            using (var rasdClone = rasd.Clone() as ManagementObject)
            {
                rasdClone["Parent"] = null;

                VirtualSystemManagement.Instance.AddResourceSettings(vms, new[] { rasdClone.GetText(TextFormat.WmiDtd20) });
            }
        }
    }
}
