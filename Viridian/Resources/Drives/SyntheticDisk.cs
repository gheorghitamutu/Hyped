using System.Management;
using Viridian.Exceptions;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Service.Msvm;

namespace Viridian.Resources.Drives
{
    public class SyntheticDisk
    {
        public void AddToScsi(ComputerSystem vm, int scsiIndex, int addressOnParent)
        {
            using (var pool = ResourcePool.GetPool(ResourcePool.ResourceTypeInfo.SyntheticDiskDrive.ResourceSubType))
            using (var rasd = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(pool))
            using (var vms = ComputerSystem.GetVirtualMachineSettings(vm.ElementName))
            using (var scsiController = vm.GetScsiController(scsiIndex))
            {
                rasd["Parent"] = scsiController ?? throw new ViridianException("Failure retrieving SCSI Controller class!");
                rasd["AddressOnParent"] = addressOnParent;

                VirtualSystemManagement.Instance.AddResourceSettings(vms, new[] { rasd.GetText(TextFormat.WmiDtd20) });
            }
        }
    }
}
