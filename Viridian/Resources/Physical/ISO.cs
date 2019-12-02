﻿using System.Management;
using Viridian.Exceptions;
using Viridian.Machine;
using Viridian.Resources.Controllers;
using Viridian.Resources.Msvm;
using Viridian.Service.Msvm;

namespace Viridian.Resources.Physical
{
    public class ISO
    {
        public void AddIso(ComputerSystem vm, string hostResource, int scsiIndex, int address)
        {
            using (var pool = ResourcePool.GetPool(ResourcePool.ResourceTypeInfo.VirtualCDDVDDisk.ResourceSubType))
            using (var sasd = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(pool))
            using (var scsi = vm.GetScsiController(scsiIndex))
            using (var parent = SCSI.GetScsiControllerChildBySubtypeAndIndex(scsi, ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType, address))
            {
                sasd["Address"] = address;
                sasd["Parent"] = parent ?? throw new ViridianException("Failure retrieving Virtual CD/DVD Disk class!");
                sasd["HostResource"] = new[] { hostResource };

                using (var vms = ComputerSystem.GetVirtualMachineSettings(vm.ElementName))
                    VirtualSystemManagement.Instance.AddResourceSettings(vms, new[] { sasd.GetText(TextFormat.WmiDtd20) });
            }
        }
    }
}
