using System;
using System.Linq;
using System.Management;
using Viridian.Exceptions;
using System.Collections.Generic;
using Viridian.Scopes;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;

namespace Viridian.Resources.Network
{
    public static class SyntheticEthernetAdapter
    {
        public static ManagementObject AddSyntheticAdapter(ComputerSystem vm, string adapterName = "Network Adapter")
        {
            if (vm is null)
                throw new ViridianException("", new ArgumentNullException(nameof(vm)));

            using (var adapterToAdd = GetDefaultSyntheticAdapter())
            {
                adapterToAdd["VirtualSystemIdentifiers"] = new string[] { Guid.NewGuid().ToString("B") };
                adapterToAdd["ElementName"] = adapterName;
                adapterToAdd["StaticMacAddress"] = false;

                var resultingResourceSettings = VirtualSystemManagementService.Instance.AddResourceSettings(vm.VirtualSystemSettingData.MsvmVirtualSystemSettingData, new string[] { adapterToAdd.GetText(TextFormat.WmiDtd20) });

                ManagementObject addedAdapter = new ManagementObject(resultingResourceSettings[0]);
                addedAdapter.Get();                

                return addedAdapter;
            }
        }

        public static ManagementObject GetDefaultSyntheticAdapter()
        {
            using (var rp = ResourcePool.GetPool(ResourcePool.ResourceTypeInfo.SyntheticEthernetPort.ResourceSubType))
                return ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(rp);
        }

        public static void ConnectVmUsingResourcePool(ComputerSystem vm, string resourcePoolName)
        {
            if (vm is null)
                throw new ViridianException("", new ArgumentNullException(nameof(vm)));

            using (var rp = ResourcePool.GetResourcePool(ResourcePool.ResourceTypeInfo.EthernetConnection.ResourceType, ResourcePool.ResourceTypeInfo.EthernetConnection.ResourceSubType, resourcePoolName, Scope.Virtualization.SpecificScope))
            using (var syntheticAdapter = AddSyntheticAdapter(vm))
            using (var depasd = NetSwitch.GetDefaultEthernetPortAllocationSettingData())
            {
                depasd["Parent"] = syntheticAdapter.Path.Path;
                depasd["PoolId"] = resourcePoolName;

                VirtualSystemManagementService.Instance.AddResourceSettings(vm.VirtualSystemSettingData.MsvmVirtualSystemSettingData, new string[] { depasd.GetText(TextFormat.WmiDtd20) });
            }
        }

        public static ManagementObject GetEthernetPortAllocationSettingData(ManagementObject Parent, ManagementScope scope)
        {
            using (var mos = new ManagementObjectSearcher(scope, new ObjectQuery("SELECT * FROM Msvm_EthernetPortAllocationSettingData")))
                return mos
                    .Get()
                    .Cast<ManagementObject>()
                    .Where((c) => string.Equals((string)c?["Parent"], Parent.Path.Path, StringComparison.OrdinalIgnoreCase))
                    .First();
        }

        public static List<ManagementObject> GetEthernetSwitchPortAclSettingData(ManagementObject ethernetConnection)
        {
            return ethernetConnection?.GetRelated("Msvm_EthernetSwitchPortAclSettingData", "Msvm_EthernetPortSettingDataComponent", null, null, null, null, false, null).Cast<ManagementObject>().ToList();
        }
    }
}
