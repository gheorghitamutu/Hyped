using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
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
    }
}
