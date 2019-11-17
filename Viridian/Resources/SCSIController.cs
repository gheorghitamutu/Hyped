using System.Management;
using Viridian.Job;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Utilities;

namespace Viridian.Resources
{
    public class SCSIController
    {
        public void AddToVm(VM vm)
        {
            var scope = Utils.GetScope(vm.ServerName, vm.ScopePath);

            using (var vmms = Utils.GetVirtualMachineManagementService(scope))
            using (var resourcePool = Utils.GetWmiObject(scope, "Msvm_ResourcePool", "ResourceSubType = 'Microsoft:Hyper-V:Synthetic SCSI Controller' and Primordial = True"))
            using (var rasd = ResourceAllocationSettingData.GetPrototypeAllocationSettings(resourcePool, "0", "0"))
            using (var rasdClone = rasd.Clone() as ManagementObject)
            {
                rasdClone["Parent"] = null;

                using (var vms = Utils.GetVirtualMachineSettings(vm.VmName, scope))
                using (var ip = vmms.GetMethodParameters("AddResourceSettings"))
                {
                    ip["AffectedConfiguration"] = vms;
                    ip["ResourceSettings"] = new[] { rasdClone.GetText(TextFormat.WmiDtd20) };

                    using (var op = vmms.InvokeMethod("AddResourceSettings", ip, null))
                        Validator.ValidateOutput(op, scope);
                }
            }
        }
    }
}
