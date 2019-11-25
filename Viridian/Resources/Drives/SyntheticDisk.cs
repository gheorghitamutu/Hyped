using System.Management;
using Viridian.Exceptions;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Service.Msvm;
using Viridian.Utilities;

namespace Viridian.Resources.Drives
{
    public class SyntheticDisk
    {
        public void AddToScsi(VM vm, int scsiIndex, int addressOnParent)
        {
            using (var rp = Utils.GetWmiObject(vm.Scope, "Msvm_ResourcePool", "ResourceSubType = 'Microsoft:Hyper-V:Synthetic Disk Drive' and Primordial = True"))
            using (var rasd = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(rp))
            using (var vms = Utils.GetVirtualMachineSettings(vm.VmName, vm.Scope))
            using (var scsiController = vm.GetScsiController(scsiIndex))
            {
                rasd["Parent"] = scsiController ?? throw new ViridianException("Failure retrieving SCSI Controller class!");
                rasd["AddressOnParent"] = addressOnParent;

                VirtualSystemManagement.Instance.AddResourceSettings(vms, new[] { rasd.GetText(TextFormat.WmiDtd20) });
            }
        }
    }
}
