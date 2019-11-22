using System;
using System.Globalization;
using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Utilities;

namespace Viridian.Resources.Network
{
    public static class SyntheticEthernetAdapter
    {
        public static ManagementObject AddSyntheticAdapter(VM virtualMachine, string adapterName = "Network Adapter")
        {
            if (virtualMachine is null)
                throw new ViridianException("", new ArgumentNullException(nameof(virtualMachine)));

            using (var vmms = Utils.GetVirtualMachineManagementService(virtualMachine.Scope))
            using (var vms = Utils.GetVirtualMachineSettings(virtualMachine.VmName, virtualMachine.Scope))
            using (var adapterToAdd = GetDefaultSyntheticAdapter(virtualMachine.Scope))
            {
                adapterToAdd["VirtualSystemIdentifiers"] = new string[] { Guid.NewGuid().ToString("B") };
                adapterToAdd["ElementName"] = adapterName;
                adapterToAdd["StaticMacAddress"] = false;

                using (var ip = vmms.GetMethodParameters("AddResourceSettings"))
                {
                    ip["AffectedConfiguration"] = vms.Path.Path;
                    ip["ResourceSettings"] = new string[] { adapterToAdd.GetText(TextFormat.WmiDtd20) };

                    using (var op = vmms.InvokeMethod("AddResourceSettings", ip, null))
                    {
                        Validator.ValidateOutput(op, virtualMachine.Scope);

                        ManagementObject addedAdapter;
                        if (op["ResultingResourceSettings"] != null)
                        {
                            addedAdapter = new ManagementObject(((string[])op["ResultingResourceSettings"])[0]);
                            addedAdapter.Get();
                        }
                        else
                        {
                            using (var job = new ManagementObject((string)op["Job"]))
                                addedAdapter = Utils.GetFirstObjectFromCollection(job.GetRelated(null, "Msvm_AffectedJobElement", null, null, null, null, false, null));
                        }

                        return addedAdapter;
                    }
                }
            }
        }

        public static ManagementObject GetDefaultSyntheticAdapter(ManagementScope scope)
        {
            var wqlQuery = "Select  * from Msvm_ResourcePool where ResourceSubType = 'Microsoft:Hyper-V:Synthetic Ethernet Port' and Primordial = True";
            var query = new ObjectQuery(wqlQuery);

            using (var mos = new ManagementObjectSearcher(scope, query))
            using (var rp = Utils.GetFirstObjectFromCollection(mos.Get()))
                return ResourceAllocationSettingData.GetDefaultAllocationSettings(rp);
        }

        public static void ConnectVmUsingResourcePool(VM virtualMachine, string resourcePoolName)
        {
            if (virtualMachine is null)
                throw new ViridianException("", new ArgumentNullException(nameof(virtualMachine)));

            using (var vmms = Utils.GetVirtualMachineManagementService(virtualMachine.Scope))
            using (var vm = virtualMachine.GetComputerSystemByName())
            using (var rp = Utils.GetResourcePool("33", "Microsoft:Hyper-V:Ethernet Connection", resourcePoolName, virtualMachine.Scope))
            using (var vms = Utils.GetVirtualMachineSettings(vm))
            using (var syntheticAdapter = AddSyntheticAdapter(virtualMachine))
            using (var depasd = NetSwitch.GetDefaultEthernetPortAllocationSettingData(virtualMachine.Scope))
            {
                depasd["Parent"] = syntheticAdapter.Path.Path;
                depasd["PoolId"] = resourcePoolName;

                using (var ip = vmms.GetMethodParameters("AddResourceSettings"))
                {
                    ip["AffectedConfiguration"] = vms.Path.Path;
                    ip["ResourceSettings"] = new string[] { depasd.GetText(TextFormat.WmiDtd20) };

                    using (var op = vmms.InvokeMethod("AddResourceSettings", ip, null))
                        Validator.ValidateOutput(op, virtualMachine.Scope);
                }
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

                return Utils.GetFirstObjectFromCollection(epasd);
            }
        }

        public static ManagementObject GetEthernetSwitchPortAclSettingData(ManagementObject ethernetConnection)
        {
            if (ethernetConnection is null)
                throw new ViridianException("", new ArgumentNullException(nameof(ethernetConnection)));

            using (var espasdCollection = ethernetConnection.GetRelated("Msvm_EthernetSwitchPortAclSettingData", "Msvm_EthernetPortSettingDataComponent", null, null, null, null, false, null))
                return Utils.GetFirstObjectFromCollection(espasdCollection);
        }
    }
}
