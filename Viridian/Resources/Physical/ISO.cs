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
            using (var pool = ResourcePool.GetPool(ResourcePool.ResourceTypeInfo.VirtualCDDVDDisk.ResourceSubType))
            using (var sasd = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(pool))
            using (var scsi = vm.GetScsiController(scsiIndex))
            using (var parent = Utils.GetScsiControllerChildBySubtypeAndIndex(scsi, ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType, address))
            {
                sasd["Address"] = address;
                sasd["Parent"] = parent ?? throw new ViridianException("Failure retrieving Virtual CD/DVD Disk class!");
                sasd["HostResource"] = new[] { hostResource };

                using (var vms = Utils.GetVirtualMachineSettings(vm.VmName, vm.Scope))
                    VirtualSystemManagement.Instance.AddResourceSettings(vms, new[] { sasd.GetText(TextFormat.WmiDtd20) });
            }
        }
    }
}
