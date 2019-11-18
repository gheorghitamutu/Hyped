using System.Management;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Utilities;

namespace Viridian.Resources.Drives
{
    public class DVD
    {
        public void AddToScsi(VM vm, int controllerSlot, int driveSlot)
        {
            using (var dvd = Utils.GetWmiObject(vm.Scope, "Msvm_ResourcePool", "ResourceSubType = 'Microsoft:Hyper-V:Synthetic DVD Drive'"))
            using (var rasd = ResourceAllocationSettingData.GetDefaultAllocationSettings(dvd))
            using (var rasdClone = rasd.Clone() as ManagementObject)
            {
                using (var parent = vm.GetScsiController(controllerSlot))
                {
                    rasdClone["Parent"] = parent;
                    rasdClone["AddressOnParent"] = driveSlot;

                    using (var vmms = Utils.GetVirtualMachineManagementService(vm.Scope))
                    using (var ip = vmms.GetMethodParameters("AddResourceSettings"))
                    {
                        ip["AffectedConfiguration"] = Utils.GetVirtualMachineSettings(vm.VmName, vm.Scope);
                        ip["ResourceSettings"] = new[] { rasdClone.GetText(TextFormat.WmiDtd20) };

                        using (var op = vmms.InvokeMethod("AddResourceSettings", ip, null))
                            Job.Validator.ValidateOutput(op, vm.Scope);
                    }
                }
            }
        }
    }
}
