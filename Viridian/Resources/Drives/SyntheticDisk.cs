using System.Management;
using Viridian.Exceptions;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Utilities;

namespace Viridian.Resources.Drives
{
    public class SyntheticDisk
    {
        public void AddToScsi(VM vm, int scsiIndex, int addressOnParent)
        {
            using (var rp = Utils.GetWmiObject(vm.Scope, "Msvm_ResourcePool", "ResourceSubType = 'Microsoft:Hyper-V:Synthetic Disk Drive' and Primordial = True"))
            using (var rasd = ResourceAllocationSettingData.GetDefaultAllocationSettings(rp))
            using (var rasdClone = rasd.Clone() as ManagementObject)
            using (var vms = Utils.GetVirtualMachineSettings(vm.VmName, vm.Scope))
            using (var scsiController = vm.GetScsiController(scsiIndex))
            {
                if (rasdClone == null)
                    throw new ViridianException("Failure retrieving default settings!");

                rasdClone["Parent"] = scsiController ?? throw new ViridianException("Failure retrieving SCSI Controller class!");
                rasdClone["AddressOnParent"] = addressOnParent;

                using (var virtualMachineManagementService = Utils.GetVirtualMachineManagementService(vm.Scope))
                using (var inParams = virtualMachineManagementService.GetMethodParameters("AddResourceSettings"))
                {
                    inParams["AffectedConfiguration"] = vms;
                    inParams["ResourceSettings"] = new[] { rasdClone.GetText(TextFormat.WmiDtd20) };

                    using (var outParams = virtualMachineManagementService.InvokeMethod("AddResourceSettings", inParams, null))
                        Job.Validator.ValidateOutput(outParams, vm.Scope);
                }
            }
        }
    }
}
