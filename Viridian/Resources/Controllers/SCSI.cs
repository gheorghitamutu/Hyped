using System.Linq;
using System.Management;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;

namespace Viridian.Resources.Controllers
{
    public class SCSI
    {
        public void AddToVm(ComputerSystem vm)
        {
            using (var pool = ResourcePool.GetPool(ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceSubType))
            using (var rasd = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(pool))
            {
                rasd["Parent"] = null;

                VirtualSystemManagementService.Instance.AddResourceSettings(vm?.VirtualSystemSettingData.MsvmVirtualSystemSettingData, new[] { rasd.GetText(TextFormat.WmiDtd20) });
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
