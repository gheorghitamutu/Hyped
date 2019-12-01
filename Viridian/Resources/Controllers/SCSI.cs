using System.Linq;
using System.Management;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Service.Msvm;

namespace Viridian.Resources.Controllers
{
    public class SCSI
    {
        public void AddToVm(VM vm)
        {
            using(var vms = VM.GetVirtualMachineSettings(vm?.VmName))
            using (var pool = ResourcePool.GetPool(ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceSubType))
            using (var rasd = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(pool))
            {
                rasd["Parent"] = null;

                VirtualSystemManagement.Instance.AddResourceSettings(vms, new[] { rasd.GetText(TextFormat.WmiDtd20) });
            }
        }

        public static ManagementObject GetScsiControllerChildBySubtypeAndIndex(ManagementObject scsiController, string resourceSubType, int index)
        {
            return
                scsiController?.GetRelated("Msvm_ResourceAllocationSettingData", null, null, null, "Dependent", "Antecedent", false, null)
                    .Cast<ManagementObject>()
                    .Where((c) => (c["ResourceSubType"]?.ToString() == resourceSubType))
                    .Skip(index)
                    .First();
        }
    }
}
