using System;
using System.Linq;
using System.Management;
using Viridian.Exceptions;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Service.Msvm;
using Viridian.Utilities;
using System.Collections.Generic;

namespace Viridian.Resources.Network
{
    public static class SyntheticEthernetAdapter
    {
        public static ManagementObject AddSyntheticAdapter(VM virtualMachine, string adapterName = "Network Adapter")
        {
            if (virtualMachine is null)
                throw new ViridianException("", new ArgumentNullException(nameof(virtualMachine)));

            using (var vms = VM.GetVirtualMachineSettings(virtualMachine.VmName, virtualMachine.Scope))
            using (var adapterToAdd = GetDefaultSyntheticAdapter())
            {
                adapterToAdd["VirtualSystemIdentifiers"] = new string[] { Guid.NewGuid().ToString("B") };
                adapterToAdd["ElementName"] = adapterName;
                adapterToAdd["StaticMacAddress"] = false;

                var resultingResourceSettings = VirtualSystemManagement.Instance.AddResourceSettings(vms, new string[] { adapterToAdd.GetText(TextFormat.WmiDtd20) });

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

        public static void ConnectVmUsingResourcePool(VM virtualMachine, string resourcePoolName)
        {
            if (virtualMachine is null)
                throw new ViridianException("", new ArgumentNullException(nameof(virtualMachine)));

            using (var vm = virtualMachine.GetComputerSystemByName())
            using (var rp = ResourcePool.GetResourcePool(ResourcePool.ResourceTypeInfo.EthernetConnection.ResourceType, ResourcePool.ResourceTypeInfo.EthernetConnection.ResourceSubType, resourcePoolName, virtualMachine.Scope))
            using (var vms = VM.GetVirtualMachineSettings(vm))
            using (var syntheticAdapter = AddSyntheticAdapter(virtualMachine))
            using (var depasd = NetSwitch.GetDefaultEthernetPortAllocationSettingData())
            {
                depasd["Parent"] = syntheticAdapter.Path.Path;
                depasd["PoolId"] = resourcePoolName;

                VirtualSystemManagement.Instance.AddResourceSettings(vms, new string[] { depasd.GetText(TextFormat.WmiDtd20) });
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
