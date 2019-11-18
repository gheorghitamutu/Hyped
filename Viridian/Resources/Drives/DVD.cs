using System.Management;
using Viridian.Exceptions;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Utilities;

namespace Viridian.Resources.Drives
{
    public class DVD
    {
        public void AddToScsi(VM vm, uint scsiIndex, uint addressOnParent)
        {
            using (var dvd = Utils.GetWmiObject(vm.Scope, "Msvm_ResourcePool", "ResourceSubType = 'Microsoft:Hyper-V:Synthetic DVD Drive' and Primordial = True"))
            using (var rasd = ResourceAllocationSettingData.GetDefaultAllocationSettings(dvd))
            using (var rasdClone = rasd.Clone() as ManagementObject)
            {
                if (rasdClone == null)
                    throw new ViridianException("Failure retrieving default settings!");

                using (var vms = Utils.GetVirtualMachineSettings(vm.VmName, vm.Scope))
                using (var parent = vm.GetScsiController(vms, scsiIndex))
                {
                    if (parent == null)
                        throw new ViridianException("Failure retrieving SCSI Controller class!");

                    rasdClone["Parent"] = parent;
                    rasdClone["AddressOnParent"] = addressOnParent;

                    using (var vmms = Utils.GetVirtualMachineManagementService(vm.Scope))
                    using (var ip = vmms.GetMethodParameters("AddResourceSettings"))
                    {
                        ip["AffectedConfiguration"] = vms;
                        ip["ResourceSettings"] = new[] { rasdClone.GetText(TextFormat.WmiDtd20) };

                        using (var op = vmms.InvokeMethod("AddResourceSettings", ip, null))
                            Job.Validator.ValidateOutput(op, vm.Scope);
                    }
                }
            }
        }
    }
}
