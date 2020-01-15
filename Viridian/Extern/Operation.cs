using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using Viridian.Root.Virtualization.v2.Msvm;
using Viridian.Root.Virtualization.v2.Msvm.Networking;
using Viridian.Root.Virtualization.v2.Msvm.ResourceManagement;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;

namespace Viridian.Extern
{
    static public class Operation
    {
        #region Network
        static public void CreateVirtualEthernetSwitchSettingData(Dictionary<string, object> args, out uint ReturnValue, out ManagementPath Job, out ManagementPath ResultingSystem)
        {
            using(var VESMS = VirtualEthernetSwitchManagementService.GetInstances().First())
            using (var data = VirtualEthernetSwitchSettingData.CreateInstance())
            {
                args.ToList().ForEach(pair =>
                {
                var propertyName = pair.Key;
                var propertyValue = pair.Value;

                    switch (propertyName)
                    {
                        case nameof(data.ElementName):
                            data.LateBoundObject[nameof(data.ElementName)] = Convert.ToString(propertyValue);
                            break;
                        case nameof(data.Notes):
                            data.LateBoundObject[nameof(data.Notes)] = ToStringArray(propertyValue);
                            break;
                        default:
                            throw new Exception("Property value not handled or invalid!");
                    }
                });

                ManagementPath ReferenceConfiguration = null;
                var SystemSettings = data.LateBoundObject.GetText(TextFormat.WmiDtd20);
                var ResourceSettings = Array.Empty<string>();

                ReturnValue = VESMS.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out Job, out ResultingSystem);
            }
        }

        static public void GetComputerSystem(Dictionary<string, object> args, out ComputerSystem ComputerSystem)
        {
            ComputerSystem = ComputerSystem.GetInstances()
                .Where((cs) =>
                {
                    return args.ToList().All(pair =>
                    {
                        var propertyName = pair.Key;
                        var propertyValue = pair.Value;

                        switch (propertyName)
                        {
                            case nameof(cs.Name):
                                var computerSystemName = Convert.ToString(cs.LateBoundObject[nameof(cs.Name)]);
                                var Name = Convert.ToString(propertyValue);
                                return string.Compare(computerSystemName, Name, true, CultureInfo.InvariantCulture) == 0;
                            default:
                                throw new Exception("Property value not handled or invalid!");
                        }
                    });
                })
                .ToList()
                .First();
        }

        static public void GetVirtualSystemManagementService(out VirtualSystemManagementService VirtualSystemManagementService)
        {
            VirtualSystemManagementService = VirtualSystemManagementService.GetInstances().First();
        }

        static public void GetVirtualSystemSettingData(Dictionary<string, string> args, string AssociationObjectName, out VirtualSystemSettingData VirtualSystemSettingData)
        {
            switch(AssociationObjectName)
            {
                case nameof(SettingsDefineState):
                    VirtualSystemSettingData = SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where(sds =>
                        {
                            return args.ToList().All(pair =>
                            {
                                var propertyName = pair.Key;
                                var propertyValue = pair.Value;

                                switch (propertyName)
                                {
                                    case nameof(sds.ManagedElement):
                                        return string.Compare(sds.ManagedElement.Path, propertyValue, true, CultureInfo.InvariantCulture) == 0;
                                    default:
                                        throw new Exception("Property value not handled or invalid!");
                                }
                            });

                        })
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();
                    break;
                default:
                    throw new Exception("Property value not handled or invalid!");
            }
        }

        static public void GetDefaultSyntheticEthernetPortSettingData(Dictionary<string, object> args, out SyntheticEthernetPortSettingData SyntheticEthernetPortSettingData)
        {
            var primordialResourcePool = GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic Ethernet Port");
            var allocationCapabilities = GetAllocationCapabilities(primordialResourcePool);

            var data = GetDefaultSyntheticEthernetPortSettingData(allocationCapabilities);

            args.ToList().ForEach(pair =>
            {
                var propertyName = pair.Key;
                var propertyValue = pair.Value;

                switch (propertyName)
                {
                    case nameof(data.ElementName):
                        data.LateBoundObject[nameof(data.ElementName)] = Convert.ToString(propertyValue);
                        break;
                    case nameof(data.VirtualSystemIdentifiers):
                        data.LateBoundObject[nameof(data.VirtualSystemIdentifiers)] = ToStringArray(propertyValue);
                        break;
                    case nameof(data.StaticMacAddress):
                        data.LateBoundObject[nameof(data.StaticMacAddress)] = Convert.ToBoolean(propertyValue);
                        break;
                    default:
                        throw new Exception("Property value not handled or invalid!");
                }
            });

            SyntheticEthernetPortSettingData = data;
        }

        static public void GetDefaultEthernetPortAllocationSettingData(Dictionary<string, object> args, out EthernetPortAllocationSettingData EthernetPortAllocationSettingData)
        {
            var primordialResourcePool = GetPrimordialResourcePool("Microsoft:Hyper-V:Ethernet Connection");
            var allocationCapabilities = GetAllocationCapabilities(primordialResourcePool);

            var data = GetDefaultEthernetPortAllocationSettingData(allocationCapabilities);

            args.ToList().ForEach(pair =>
            {
                var propertyName = pair.Key;
                var propertyValue = pair.Value;

                switch (propertyName)
                {
                    case nameof(data.Parent):
                        data.LateBoundObject[nameof(data.Parent)] = Convert.ToString(propertyValue);
                        break;
                    case nameof(data.HostResource):
                        data.LateBoundObject[nameof(data.HostResource)] = ToStringArray(propertyValue);
                        break;
                    default:
                        throw new Exception("Property value not handled or invalid!");
                }
            });

            EthernetPortAllocationSettingData = data;
        }

        #endregion

        #region Helpers

        static string[] ToStringArray(object arg)   // https://stackoverflow.com/questions/10745542/object-to-string-array
        {
            if (arg.GetType() != typeof(string))
            {
                var collection = arg as System.Collections.IEnumerable;
                if (collection != null) // do not split strings!
                {
                    return collection
                      .Cast<object>()
                      .Select(x => x.ToString())
                      .ToArray();
                }
            }

            if (arg == null)
            {
                return Array.Empty<string>();
            }

            return new string[] { arg.ToString() };
        }

        public static ResourcePool GetPrimordialResourcePool(string ResourceSubType)
        {
            // TODO: refactor this for general usage
            return
                ResourcePool.GetInstances()
                        .Where((rp) =>
                            rp.Primordial == true &&
                            string.Compare(rp.ResourceSubType, ResourceSubType, true, CultureInfo.InvariantCulture) == 0)
                        .First();
        }

        public static AllocationCapabilities GetAllocationCapabilities(ResourcePool ResourcePool)
        {
            // TODO: refactor this for general usage
            return
                ElementCapabilities.GetInstances()
                    .Cast<ElementCapabilities>()
                    .Where((ec) => string.Compare(ec.ManagedElement.Path, ResourcePool.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                    .Select((ec) => new AllocationCapabilities(ec.Capabilities))
                    .ToList()
                    .First();
        }

        public static SyntheticEthernetPortSettingData GetDefaultSyntheticEthernetPortSettingData(AllocationCapabilities AllocationCapabilities)
        {
            // TODO: refactor this for general usage
            return
                SettingsDefineCapabilities.GetInstances()
                    .Cast<SettingsDefineCapabilities>()
                    .Where((sdc) =>
                        string.Compare(sdc.GroupComponent.Path, AllocationCapabilities.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        sdc.ValueRange == 0 &&
                        sdc.ValueRole == 0)
                    .Select((sdc) => new SyntheticEthernetPortSettingData(sdc.PartComponent))
                    .ToList()
                    .First();
        }

        public static EthernetPortAllocationSettingData GetDefaultEthernetPortAllocationSettingData(AllocationCapabilities AllocationCapabilities)
        {
            // TODO: refactor this for general usage
            return
                SettingsDefineCapabilities.GetInstances()
                    .Cast<SettingsDefineCapabilities>()
                    .Where((sdc) =>
                        string.Compare(sdc.GroupComponent.Path, AllocationCapabilities.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        sdc.ValueRange == 0 &&
                        sdc.ValueRole == 0)
                    .Select((sdc) => new EthernetPortAllocationSettingData(sdc.PartComponent))
                    .ToList()
                    .First();
        }

        public static List<EthernetPortAllocationSettingData> GetEthernetPortAllocationSettingData(VirtualSystemSettingData VirtualSystemSettingData, ushort ResourceType, string ResourceSubType)
        {
            return
                VirtualSystemSettingDataComponent.GetInstances()
                    .Cast<VirtualSystemSettingDataComponent>()
                    .Where((sds) =>
                        string.Compare(sds.GroupComponent.Path, VirtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(EthernetPortAllocationSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new EthernetPortAllocationSettingData(sds.PartComponent))
                    .ToList()
                    .Where((rasd) =>
                        rasd.ResourceType == ResourceType &&
                        string.Compare(rasd.ResourceSubType, ResourceSubType, true, CultureInfo.InvariantCulture) == 0)
                    .ToList();
        }

        #endregion
    }
}
