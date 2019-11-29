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
            using (var pool = ResourcePool.GetPool(ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceSubType))
            using (var rasd = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(pool))
            {
                rasd["Parent"] = null;

                VirtualSystemManagement.Instance.AddResourceSettings(vms, new[] { rasd.GetText(TextFormat.WmiDtd20) });
            }
        }
    }
}
