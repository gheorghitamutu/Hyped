using System;
using System.Linq;
using System.Globalization;
using System.Management;
using Viridian.Exceptions;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Service.Msvm;
using Viridian.Utilities;

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
            using (var rp = ResourcePool.GetResourcePool("33", "Microsoft:Hyper-V:Ethernet Connection", resourcePoolName, virtualMachine.Scope))
            using (var vms = VM.GetVirtualMachineSettings(vm))
            using (var syntheticAdapter = AddSyntheticAdapter(virtualMachine))
            using (var depasd = NetSwitch.GetDefaultEthernetPortAllocationSettingData())
            {
                depasd["Parent"] = syntheticAdapter.Path.Path;
                depasd["PoolId"] = resourcePoolName;

                VirtualSystemManagement.Instance.AddResourceSettings(vms, new string[] { depasd.GetText(TextFormat.WmiDtd20) });
            }
        }

        public static ManagementObject GetEthernetPortAllocationSettingData(ManagementObject parentPort, ManagementScope scope)
        {
            if (parentPort is null)
                throw new ViridianException("", new ArgumentNullException(nameof(parentPort)));

            var wqlQuery = string.Format(CultureInfo.InvariantCulture, "SELECT * FROM Msvm_EthernetPortAllocationSettingData WHERE Parent=\"{0}\"", Utils.EscapeObjectPath(parentPort.Path.Path));
            var query = new SelectQuery(wqlQuery);

            using (var mos = new ManagementObjectSearcher(scope, query))
            using (ManagementObjectCollection epasd = mos.Get())
            {
                if (epasd.Count != 1)
                    throw new ViridianException(string.Format(CultureInfo.CurrentCulture, "A single Msvm_EthernetPortAllocationSettingData could not be found for parent port \"{0}\"", parentPort.Path.Path));

                return epasd.Cast<ManagementObject>().First();
            }
        }

        public static ManagementObjectCollection GetEthernetSwitchPortAclSettingData(ManagementObject ethernetConnection)
        {
            if (ethernetConnection is null)
                throw new ViridianException("", new ArgumentNullException(nameof(ethernetConnection)));

            return ethernetConnection.GetRelated("Msvm_EthernetSwitchPortAclSettingData", "Msvm_EthernetPortSettingDataComponent", null, null, null, null, false, null);
        }
    }
}
