﻿using System;
using System.Management;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;

namespace Viridian.Resources.Physical
{
    public class ISO
    {
        public void AddIso(ComputerSystem vm, string hostResource, int scsiIndex, int address)
        {
            using (var pool = ResourcePool.GetPool(ResourcePool.ResourceTypeInfo.VirtualCDDVDDisk.ResourceSubType))
            using (var sasd = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(pool))
            using (var scsi = vm.VirtualSystemSettingData.GetScsiController(scsiIndex))
            using (var parent = ResourceAllocationSettingData.GetRelatedResourceAllocationSettingData(scsi, ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType, address))
            {
                sasd["Address"] = address;
                sasd["Parent"] = parent ?? throw new NullReferenceException($"Failure retrieving Virtual CD/DVD Disk class [{parent}]!");
                sasd["HostResource"] = new[] { hostResource };

                VirtualSystemManagementService.Instance.AddResourceSettings(vm.VirtualSystemSettingData.MsvmVirtualSystemSettingData, new[] { sasd.GetText(TextFormat.WmiDtd20) });
            }
        }
    }
}
