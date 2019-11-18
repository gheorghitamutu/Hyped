using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Utilities;

namespace Viridian.Resources.Controllers
{
    public class SCSI
    {
        public void AddToVm(VM vm)
        {
            using (var scsi = Utils.GetWmiObject(vm.Scope, "Msvm_ResourcePool", "ResourceSubType = 'Microsoft:Hyper-V:Synthetic SCSI Controller' and Primordial = True"))
            using (var rasd = ResourceAllocationSettingData.GetDefaultAllocationSettings(scsi))
            using (var rasdClone = rasd.Clone() as ManagementObject)
            {
                rasdClone["Parent"] = null;

                using (var vmms = Utils.GetVirtualMachineManagementService(vm.Scope))
                using (var ip = vmms.GetMethodParameters("AddResourceSettings"))
                {
                    ip["AffectedConfiguration"] = Utils.GetVirtualMachineManagementService(vm.Scope);
                    ip["ResourceSettings"] = new[] { rasdClone.GetText(TextFormat.WmiDtd20) };

                    using (var op = vmms.InvokeMethod("AddResourceSettings", ip, null))
                        Validator.ValidateOutput(op, vm.Scope);
                }
            }
        }
    }
}
