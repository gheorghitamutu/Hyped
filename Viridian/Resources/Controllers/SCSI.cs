using System.Management;
using Viridian.Exceptions;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Service.Msvm;

namespace Viridian.Resources.Controllers
{
    public class SCSI
    {
        public void AddToVm(VM vm)
        {
            using(var vms = VM.GetVirtualMachineSettings(vm.VmName, vm.Scope))
            using (var pool = ResourcePool.GetPool(ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceSubType))
            using (var rasd = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(pool))
            {
                rasd["Parent"] = null;

                VirtualSystemManagement.Instance.AddResourceSettings(vms, new[] { rasd.GetText(TextFormat.WmiDtd20) });
            }
        }

        public static ManagementObject GetScsiControllerChildBySubtypeAndIndex(ManagementObject scsiController, string resourceSubType, int index)
        {
            if (scsiController == null)
                throw new ViridianException("Null SCSI Controller class!");

            using (var scsiControllerChildren = scsiController.GetRelated("Msvm_ResourceAllocationSettingData", null, null, null, "Dependent", "Antecedent", false, null))
            {
                if (scsiControllerChildren.Count < index)
                    throw new ViridianException("Invalid SCSI child address/index specified!");

                uint count = 0;

                foreach (ManagementObject drive in scsiControllerChildren)
                {
                    if (count == index && drive["ResourceSubType"].ToString() == resourceSubType)
                        return drive;

                    count++;
                }

                throw new ViridianException("Invalid SCSI child subtype specified!");
            }
        }
    }
}
