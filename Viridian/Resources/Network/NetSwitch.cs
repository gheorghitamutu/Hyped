using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Viridian.Job;
using Viridian.Msvm.Networking;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;
using Viridian.Scopes;

// TODO: change all <Operation>Custom<AppliedOn> to handle actual param settings

namespace Viridian.Resources.Network
{
    public static class NetSwitch
    {
        #region Constants

        const ushort CimEnabledStateEnabled = 2;
        const ushort CimEnabledStateDisabled = 3;
        const ushort CimEnabledStateNotApplicable = 5;

        #endregion

        #region Enums

        public enum PortFeatureType
        {
            Unknown,
            Acl,
            Bandwidth,
            Offload,
            Profile,
            Security,
            Vlan
        }
        public enum SwitchFeatureType
        {
            Unknown,
            Bandwidth
        }
        public enum SwitchConnectionType
        {
            Private,
            Internal,
            ExternalOnly,
            External
        }
        public enum PortConnectionType
        {
            Nothing,
            Internal,
            External,
            VirtualMachine
        }
        public struct PortInfo
        {
            public PortConnectionType Type;
            public string ConnectedName;
            public List<PortFeatureType> FeatureList;
        }
        public struct SwitchInfo
        {
            public string Name;
            public SwitchConnectionType Type;
            public List<PortInfo> PortList;
            public List<SwitchFeatureType> SwitchFeatureList;
        }

        #endregion

        #region Create

        public static void CreatePrivateSwitch(string ElementName, string[] Notes)
        {
            CreateSwitch(ElementName, Notes, null);
        }

        public static void CreateInternalSwitch(string ElementName, string[] Notes)
        {
            using (var host = ComputerSystem.QueryMsvmComputerSystem("Name", Environment.MachineName))
            using (var depasd = GetDefaultEthernetPortAllocationSettingData())
            {
                depasd["ElementName"] = ElementName + "_Internal";
                depasd["HostResource"] = new string[] { host.Path.Path };

                string[] ports = new string[] { depasd.GetText(TextFormat.WmiDtd20) };

                CreateSwitch(ElementName, Notes, ports);
            }
        }

        public static void CreateExternalOnlySwitch(string externalAdapterName, string ElementName, string[] Notes)
        {
            using (var eep = FindExternalAdapter(externalAdapterName))
            using (var depasd = GetDefaultEthernetPortAllocationSettingData())
            {
                depasd["ElementName"] = ElementName + "_External";
                depasd["HostResource"] = new string[] { eep.Path.Path };

                string[] ports = new string[] { depasd.GetText(TextFormat.WmiDtd20) };

                CreateSwitch(ElementName, Notes, ports);
            }
        }

        public static void CreateExternalSwitch(string externalAdapterName, string ElementName, string[] Notes)
        {
            using (var eep = FindExternalAdapter(externalAdapterName))
            using (var host = ComputerSystem.QueryMsvmComputerSystem("Name", Environment.MachineName))
            using (var depasdInternal = GetDefaultEthernetPortAllocationSettingData())
            using (var depasdExternal = GetDefaultEthernetPortAllocationSettingData())
            {
                depasdExternal["ElementName"] = ElementName + "_External";
                depasdExternal["HostResource"] = new string[] { eep.Path.Path };

                depasdInternal["ElementName"] = ElementName + "_Internal";
                depasdInternal["HostResource"] = new string[] { host.Path.Path };
                depasdInternal["Address"] = eep["PermanentAddress"];

                string[] ports = new string[] { depasdExternal.GetText(TextFormat.WmiDtd20), depasdInternal.GetText(TextFormat.WmiDtd20) };

                CreateSwitch(ElementName, Notes, ports);
            }
        }

        public static void CreateSwitch(string ElementName, string[] Notes, string[] ports)
        {
            var vessd = new VirtualEthernetSwitchSettingData(new Dictionary<string, object>()
            {
                { nameof(ElementName), ElementName },
                { nameof(Notes), Notes }

            });

            VirtualEthernetSwitchManagementService.Instance.DefineSystem(vessd.MsvmVirtualEthernetSwitchSettingData.GetText(TextFormat.WmiDtd20), ports, null);
        }

        #endregion

        #region Delete

        public static void DeleteSwitchWithPorts(string switchName)
        {
            using (var ves = FindVirtualEthernetSwitch(switchName))
                VirtualEthernetSwitchManagementService.Instance.DestroySystem(ves.Path.Path);
        }

        #endregion

        #region Modify

        public static void ModifySwitchName(string existingSwitchName, string newSwitchName, string newNotes)
        {
            using (var ves = FindVirtualEthernetSwitch(existingSwitchName))
            using (var vessd = ves.GetRelated("Msvm_VirtualEthernetSwitchSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null).Cast<ManagementObject>().First())
            {
                vessd["ElementName"] = newSwitchName;
                vessd["Notes"] = new string[] { newNotes };

                VirtualEthernetSwitchManagementService.Instance.ModifySystemSettings(vessd.GetText(TextFormat.WmiDtd20));
            }
        }

        public static void AddPorts(string switchName, string Name)
        {
            using (var ves = FindVirtualEthernetSwitch(switchName))
            using (var vessd = ves.GetRelated("Msvm_VirtualEthernetSwitchSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null).Cast<ManagementObject>().First())
            {
                using (var mos = new ManagementObjectSearcher(Scope.Virtualization.ScopeObject, new ObjectQuery("SELECT * FROM Msvm_ExternalEthernetPort")))
                using (var eep = mos.Get().Cast<ManagementObject>().Where((c) => c[nameof(Name)]?.ToString() == Name).First())
                using (var depasd = GetDefaultEthernetPortAllocationSettingData())
                {
                    depasd["ElementName"] = switchName + "_External";
                    depasd["HostResource"] = new string[] { eep.Path.Path };

                    VirtualEthernetSwitchManagementService.Instance.AddResourceSettings(vessd.Path.Path, new string[] { depasd.GetText(TextFormat.WmiDtd20) });
                }
            }
        }

        public static void RemovePorts(string switchName)
        {
            var searchedClassNames = new List<string>()
            {
                "Msvm_ComputerSystem",
                "Msvm_ExternalEthernetPort"
            };

            var portsToDelete = new List<string>();

            using (var ves = FindVirtualEthernetSwitch(switchName))
                    ves.GetRelated("Msvm_EthernetSwitchPort", "Msvm_SystemDevice", null, null, null, null, false, null)
                       .Cast<ManagementObject>()
                       .Where((sd) =>
                               searchedClassNames
                                    .Contains(
                                        new ManagementPath(
                                            ((string[])sd.GetRelated("Msvm_EthernetPortAllocationSettingData", "Msvm_ElementSettingData", null, null, null, null, false, null)
                                                    .Cast<ManagementObject>()
                                                    .First()?
                                                    .GetPropertyValue("HostResource"))
                                                .First())
                                            .ClassName))
                       .ToList()
                       .ForEach((sd) =>
                            portsToDelete.Add(
                                    sd.GetRelated("Msvm_EthernetPortAllocationSettingData", "Msvm_ElementSettingData", null, null, null, null, false, null)
                                        .Cast<ManagementObject>()
                                        .First()
                                        .Path
                                        .Path));

                if (portsToDelete.Count == 0)
                    throw new InvalidOperationException($"The switch [{switchName}] does not have any internal or external ports to remove!");

                VirtualEthernetSwitchManagementService.Instance.RemoveResourceSettings(portsToDelete.ToArray());
        }

        public static void ModifyPorts(string switchName, string Name)
        {
            using (var mos = new ManagementObjectSearcher(Scope.Virtualization.ScopeObject, new ObjectQuery("SELECT * FROM Msvm_ExternalEthernetPort")))
            using (var eep = mos.Get().Cast<ManagementObject>().Where((c) => c[nameof(Name)]?.ToString() == Name).First())
            using (var ves = FindVirtualEthernetSwitch(switchName))
            {
                ves.GetRelated("Msvm_EthernetSwitchPort", "Msvm_SystemDevice", null, null, null, null, false, null)
                    .Cast<ManagementObject>()
                    .Where((port) =>
                        {
                            var path = new ManagementPath(
                                ((string[])(port.GetRelated("Msvm_EthernetPortAllocationSettingData", "Msvm_ElementSettingData", null, null, null, null, false, null)
                                    .Cast<ManagementObject>()
                                    .First())
                                    .GetPropertyValue("HostResource"))
                                    .First());

                            return path.ClassName == "Msvm_ExternalEthernetPort" && path.Path == eep.Path.Path;
                        })
                    .ToList()
                    .ForEach((port) =>
                        {
                            var hostResource =
                                port.GetRelated("Msvm_EthernetPortAllocationSettingData", "Msvm_ElementSettingData", null, null, null, null, false, null)
                                .Cast<ManagementObject>()
                                .First();

                            hostResource["HostResource"] = new string[] { eep.Path.Path };

                            VirtualEthernetSwitchManagementService.Instance.ModifyResourceSettings(new string[] { hostResource.GetText(TextFormat.WmiDtd20) });
                        });
            }
        }

        public static void AddCustomFeatureSettings(ComputerSystem vm, PortFeatureType FeatureType)
        {
            string featureId = GetPortFeatureId(FeatureType);

            using (var defaultFeatureSetting = GetDefaultFeatureSetting(featureId))
            {
                switch (FeatureType)
                {
                    case PortFeatureType.Security:
                        defaultFeatureSetting["AllowMacSpoofing"] = false;
                        break;
                    case PortFeatureType.Acl:
                        defaultFeatureSetting["Action"] = 3;
                        defaultFeatureSetting["Direction"] = 2;
                        defaultFeatureSetting["Applicability"] = 1;
                        defaultFeatureSetting["AclType"] = 2;
                        defaultFeatureSetting["RemoteAddress"] = "*.*";
                        defaultFeatureSetting["RemoteAddressPrefixLength"] = 32;
                        break;
                    case PortFeatureType.Offload:
                        defaultFeatureSetting["IOVOffloadWeight"] = 100;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(FeatureType));
                }

                vm?.VirtualSystemSettingData.GetEthernetPortAllocationSettingDataList()
                    .Cast<ManagementObject>()
                    .ToList()
                    .ForEach((connection) => VirtualSystemManagementService.Instance.AddFeatureSettings(connection.Path.Path, new string[] { defaultFeatureSetting.GetText(TextFormat.WmiDtd20) }));
            }
        }

        public static void ModifyCustomFeatureSettings(ComputerSystem vm)
        {
            // featureSetting["AllowMacSpoofing"] | featureSetting["IOVQueuePairsRequested"]
            // Msvm_EthernetSwitchPortSecuritySettingData | Msvm_EthernetSwitchPortOffloadSettingData

            vm?.VirtualSystemSettingData.GetEthernetPortAllocationSettingDataList()
                .Cast<ManagementObject>()
                .ToList()
                .ForEach((connection) =>
                    connection.GetRelated("Msvm_EthernetSwitchPortSecuritySettingData", "Msvm_EthernetPortSettingDataComponent", null, null, null, null, false, null)
                        .Cast<ManagementObject>()
                        .ToList()
                        .ForEach((epsdc) =>
                        {
                            epsdc["AllowMacSpoofing"] = true;
                            VirtualSystemManagementService.Instance.ModifyFeatureSettings(new string[] { epsdc.GetText(TextFormat.WmiDtd20) });
                        }));
        }

        public static void RemoveFeatureSettings(ComputerSystem vm, PortFeatureType FeatureType)
        {
            string featureSettingClass;

            switch (FeatureType)
            {
                case PortFeatureType.Security:
                    featureSettingClass = "Msvm_EthernetSwitchPortSecuritySettingData";
                    break;
                case PortFeatureType.Offload:
                    featureSettingClass = "Msvm_EthernetSwitchPortOffloadSettingData";
                    break;
                case PortFeatureType.Acl:
                    featureSettingClass = "Msvm_EthernetSwitchPortAclSettingData";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(FeatureType));
            }

            foreach (var ethernetConnectionSetting in vm?.VirtualSystemSettingData.GetEthernetPortAllocationSettingDataList())
            {
                var featureSettingPaths = new List<string>();

                ethernetConnectionSetting.GetRelated(featureSettingClass, "Msvm_EthernetPortSettingDataComponent", null, null, null, null, false, null)
                    .Cast<ManagementObjectCollection>()
                    .ToList()
                    .ForEach((epsdcCollection) =>
                        epsdcCollection
                            .Cast<ManagementObject>()
                            .ToList()
                            .ForEach((epsdc) => featureSettingPaths.Add(epsdc.Path.Path)));

                VirtualSystemManagementService.Instance.RemoveFeatureSettings(featureSettingPaths.ToArray());
            }
        }

        public static void SetExtensionEnabledState(string switchName, string extensionName, bool enabled)
        {
            bool found = false;

            using (var ves = FindVirtualEthernetSwitch(switchName))
            using (var eseCollection = ves.GetRelated("Msvm_EthernetSwitchExtension", "Msvm_HostedEthernetSwitchExtension", null, null, null, null, false, null))
            {
                foreach (ManagementObject ese in eseCollection)
                    using (ese)
                    {
                        if (!string.Equals((string)ese["ElementName"], extensionName, StringComparison.OrdinalIgnoreCase))
                            continue;

                        found = true;

                        if ((ushort)ese["EnabledState"] == CimEnabledStateNotApplicable)
                            continue;

                        if (((ushort)ese["EnabledState"] == CimEnabledStateEnabled) && enabled)
                            continue;

                        if (((ushort)ese["EnabledState"] == CimEnabledStateDisabled) && !enabled)
                            continue;

                        using (var ip = ese.GetMethodParameters("RequestStateChange"))
                        {
                            ip["RequestedState"] = enabled ? CimEnabledStateEnabled : CimEnabledStateDisabled;

                            using (var op = ese.InvokeMethod("RequestStateChange", ip, null))
                                Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);
                        }
                    }
            }

            if (!found)
                throw new InvalidOperationException($"Could not find extension [{extensionName}] on switch [{switchName}]!");
        }

        public static void MoveExtension(string switchName, string extensionName, int offset)
        {
            using (var ves = FindVirtualEthernetSwitch(switchName))
            using (var vessd = ves.GetRelated("Msvm_VirtualEthernetSwitchSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null).Cast<ManagementObject>().First())
            {
                string[] extensionOrder = (string[])vessd["ExtensionOrder"];
                byte[] extensionTypes = new byte[extensionOrder.Length];
                int extensionIndex = -1;

                for (int idx = 0; idx < extensionOrder.Length; ++idx)
                {
                    using (var extension = new ManagementObject(extensionOrder[idx]))
                    {
                        extension.Get();

                        extensionTypes[idx] = (byte)extension["ExtensionType"];

                        if (string.Equals((string)extension["ElementName"], extensionName, StringComparison.OrdinalIgnoreCase))
                            extensionIndex = idx;
                    }
                }

                if (extensionIndex == -1)
                    throw new InvalidOperationException($"Could not find extension [{extensionName}] on switch [{switchName}]!");

                int newExtensionIndex = extensionIndex + offset;

                if ((newExtensionIndex < 0) || (newExtensionIndex >= extensionOrder.Length))
                    throw new InvalidOperationException("Invalid move operation.");
                if (extensionTypes[newExtensionIndex] != extensionTypes[extensionIndex])
                    throw new InvalidOperationException("Invalid move operation.");

                string temp = extensionOrder[newExtensionIndex];
                extensionOrder[newExtensionIndex] = extensionOrder[extensionIndex];
                extensionOrder[extensionIndex] = temp;

                vessd["ExtensionOrder"] = extensionOrder;

                VirtualEthernetSwitchManagementService.Instance.ModifySystemSettings(vessd.GetText(TextFormat.WmiDtd20));
            }
        }

        public static void SetRequiredFeature(ComputerSystem vm, string featureName, bool required)
        {
            var connectionsToModify = new List<string>();

            using (var feature = FindFeatureByName(featureName, Scope.Virtualization.ScopeObject))
                foreach (ManagementObject connection in vm.VirtualSystemSettingData.GetEthernetPortAllocationSettingDataList())
                    using (connection)
                    {
                        string[] requiredFeatures = (string[])connection["RequiredFeatures"];
                        int featureIndex = -1;

                        for (int idx = 0; idx < requiredFeatures.Length; ++idx)
                            using (var requiredFeature = new ManagementObject(requiredFeatures[idx]))
                            {
                                requiredFeature.Get();

                                if (string.Equals((string)requiredFeature["ElementName"], featureName, StringComparison.OrdinalIgnoreCase))
                                {
                                    featureIndex = idx;
                                    break;
                                }
                            }

                        if (((featureIndex == -1) && !required) || ((featureIndex != -1) && required))
                            continue;

                        string[] newRequiredFeatures = null;

                        if (required)
                        {
                            newRequiredFeatures = new string[requiredFeatures.Length + 1];

                            for (int idx = 0; idx < requiredFeatures.Length; ++idx)
                                newRequiredFeatures[idx] = requiredFeatures[idx];

                            newRequiredFeatures[newRequiredFeatures.Length - 1] = feature.Path.Path;
                        }
                        else
                        {
                            newRequiredFeatures = new string[requiredFeatures.Length - 1];

                            for (int idx = 0; idx < featureIndex; ++idx)
                                newRequiredFeatures[idx] = requiredFeatures[idx];

                            for (int idx = featureIndex; idx < newRequiredFeatures.Length; ++idx)
                                newRequiredFeatures[idx] = requiredFeatures[idx + 1];
                        }

                        connection["RequiredFeatures"] = newRequiredFeatures;
                        connectionsToModify.Add(connection.GetText(TextFormat.WmiDtd20));
                    }

            if (connectionsToModify.Count > 0)
                VirtualSystemManagementService.Instance.ModifyResourceSettings(connectionsToModify.ToArray());
        }

        public static void AddBandwithSettings(string switchName, ulong bytesPerSecond)
        {
            string featureId = GetSwitchFeatureId(SwitchFeatureType.Bandwidth);

            using (var ves = FindVirtualEthernetSwitch(switchName))
            using (var vessd = ves.GetRelated("Msvm_VirtualEthernetSwitchSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null).Cast<ManagementObject>().First())
            using (ManagementObject bandwidthSetting = GetDefaultFeatureSetting(featureId))
            {
                bandwidthSetting["DefaultFlowReservation"] = bytesPerSecond;

                VirtualEthernetSwitchManagementService.Instance.AddFeatureSettings(vessd.Path.Path, new string[] { bandwidthSetting.GetText(TextFormat.WmiDtd20) });
            }
        }

        public static void ModifyFeatureSettings(string switchName, ulong bytesPerSecond)
        {
            using (var ves = FindVirtualEthernetSwitch(switchName))
            using (var vessd = ves.GetRelated("Msvm_VirtualEthernetSwitchSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null).Cast<ManagementObject>().First())
            using (var bandwidthSetting = vessd.GetRelated("Msvm_VirtualEthernetSwitchBandwidthSettingData","Msvm_VirtualEthernetSwitchSettingDataComponent", null, null, null, null, false, null).Cast<ManagementObject>().First())
            {
                bandwidthSetting["DefaultFlowReservation"] = bytesPerSecond;

                VirtualEthernetSwitchManagementService.Instance.ModifyResourceSettings(new string[] { bandwidthSetting.GetText(TextFormat.WmiDtd20) });
            }
        }

        public static void RemoveFeatureSettings(string switchName)
        {
            using (var ves = FindVirtualEthernetSwitch(switchName))
            using (var vessd = ves.GetRelated("Msvm_VirtualEthernetSwitchSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null).Cast<ManagementObject>().First())
            using (var vesbsd = vessd.GetRelated("Msvm_VirtualEthernetSwitchBandwidthSettingData", "Msvm_VirtualEthernetSwitchSettingDataComponent", null, null, null, null, false, null).Cast<ManagementObject>().First())
                VirtualEthernetSwitchManagementService.Instance.RemoveFeatureSettings(new string[] { vesbsd.Path.Path });
        }

        public static void ModifyClusterMonitored(ComputerSystem vm, bool onOff)
        {
            using (var syntheticPortSettings = vm?.VirtualSystemSettingData.MsvmVirtualSystemSettingData.GetRelated("Msvm_SyntheticEthernetPortSettingData", "Msvm_VirtualSystemSettingDataComponent", null, null, null, null, false, null))
                foreach (ManagementObject syntheticEthernetPortSetting in syntheticPortSettings)
                {
                    syntheticEthernetPortSetting["ClusterMonitored"] = onOff;

                    VirtualSystemManagementService.Instance.ModifyResourceSettings(new string[] { syntheticEthernetPortSetting.GetText(TextFormat.CimDtd20) });
                }
        }

        #endregion

        #region Info

        public static SwitchInfo GetSwitch(string switchName)
        {
            SwitchInfo ethernetSwitchInfo = new SwitchInfo
            {
                Name = switchName,
                Type = SwitchConnectionType.Private,
                PortList = new List<PortInfo>(),
                SwitchFeatureList = new List<SwitchFeatureType>()
            };

            using (ManagementObject ves = FindVirtualEthernetSwitch(switchName))
            {
                using (var espCollection = ves.GetRelated("Msvm_EthernetSwitchPort", "Msvm_SystemDevice", null, null, null, null, false, null))
                {
                    foreach (ManagementObject esp in espCollection)
                        using (esp)
                        {
                            PortInfo portInfo = new PortInfo
                            {
                                Type = PortConnectionType.Nothing,
                                FeatureList = new List<PortFeatureType>()
                            };

                            using (ManagementObject epasd = esp.GetRelated("Msvm_EthernetPortAllocationSettingData", "Msvm_ElementSettingData", null, null, null, null, false, null).Cast<ManagementObject>().First())
                            {
                                portInfo.Type = DeterminePortType(epasd);

                                if (portInfo.Type == PortConnectionType.VirtualMachine)
                                    using (var vssd = epasd.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_VirtualSystemSettingDataComponent", null, null, null, null, false, null).Cast<ManagementObject>().First())
                                        portInfo.ConnectedName = (string)vssd["ElementName"];
                                else if (portInfo.Type == PortConnectionType.External)
                                    using (var externalAdapter = new ManagementObject(((string[])epasd["HostResource"])[0]))
                                        portInfo.ConnectedName = (string)externalAdapter["ElementName"];

                                using (var portFeatureCollection = epasd.GetRelated("Msvm_EthernetSwitchPortFeatureSettingData", "Msvm_EthernetPortSettingDataComponent", null, null, null, null, false, null))
                                    foreach (ManagementObject portFeature in portFeatureCollection)
                                        using (portFeature)
                                            portInfo.FeatureList.Add(DeterminePortFeatureType(portFeature));
                            }

                            ethernetSwitchInfo.PortList.Add(portInfo);
                        }

                    using (var vessd = ves.GetRelated("Msvm_VirtualEthernetSwitchSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null).Cast<ManagementObject>().First())
                    using (var esfsd = vessd.GetRelated("Msvm_EthernetSwitchFeatureSettingData", "Msvm_VirtualEthernetSwitchSettingDataComponent", null, null, null, null, false, null))
                        foreach (ManagementObject switchFeature in esfsd)
                            using (switchFeature)
                                ethernetSwitchInfo.SwitchFeatureList.Add(DetermineSwitchFeatureType(switchFeature));

                    ethernetSwitchInfo.Type = DetermineSwitchConnectionType(ethernetSwitchInfo.PortList);

                    return ethernetSwitchInfo;
                }
            }
        }

        public static PortConnectionType DeterminePortType(ManagementObject portSettings)
        {
            if (portSettings is null)
                throw new ArgumentNullException(nameof(portSettings));

            string[] hostResource = (string[])portSettings["HostResource"];

            if (hostResource != null && hostResource.Length > 0)
            {
                var hostResourcePath = new ManagementPath(hostResource[0]);

                if (string.Equals(hostResourcePath.ClassName, "Msvm_ComputerSystem", StringComparison.OrdinalIgnoreCase))
                    return PortConnectionType.Internal;
                else if (string.Equals(hostResourcePath.ClassName, "Msvm_ExternalEthernetPort", StringComparison.OrdinalIgnoreCase))
                    return PortConnectionType.External;
            }

            string parent = (string)portSettings["Parent"];

            if (!string.IsNullOrEmpty(parent))
            {
                var parentPath = new ManagementPath(parent);

                if (string.Equals(parentPath.ClassName, "Msvm_SyntheticEthernetPortSettingData", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(parentPath.ClassName, "Msvm_EmulatedEthernetPortSettingData", StringComparison.OrdinalIgnoreCase))
                    return PortConnectionType.VirtualMachine;
            }

            return PortConnectionType.Nothing;
        }

        public static PortFeatureType DeterminePortFeatureType(ManagementObject portFeature)
        {
            if (portFeature is null)
                throw new ArgumentNullException(nameof(portFeature));

            switch (portFeature.Path.ClassName)
            {
                case "Msvm_EthernetSwitchPortOffloadSettingData":   return PortFeatureType.Offload;
                case "Msvm_EthernetSwitchPortVlanSettingData":      return PortFeatureType.Vlan;
                case "Msvm_EthernetSwitchPortAclSettingData":       return PortFeatureType.Acl;
                case "Msvm_EthernetSwitchPortBandwidthSettingData": return PortFeatureType.Bandwidth;
                case "Msvm_EthernetSwitchPortSecuritySettingData":  return PortFeatureType.Security;
                case "Msvm_EthernetSwitchPortProfileSettingData":   return PortFeatureType.Profile;
                default:                                            return PortFeatureType.Unknown;
            }
        }

        public static SwitchFeatureType DetermineSwitchFeatureType(ManagementObject switchFeature)
        {
            if (switchFeature is null)
                throw new ArgumentNullException(nameof(switchFeature));

            switch (switchFeature.Path.ClassName)
            {
                case "Msvm_VirtualEthernetSwitchBandwidthSettingData":  return SwitchFeatureType.Bandwidth;
                default:                                                return SwitchFeatureType.Unknown;
            }
        }

        public static SwitchConnectionType DetermineSwitchConnectionType(List<PortInfo> switchPorts)
        {
            if (switchPorts is null)
                throw new ArgumentNullException(nameof(switchPorts));

            SwitchConnectionType type = SwitchConnectionType.Private;

            bool internallyConnected = false;
            bool externallyConnected = false;

            foreach (PortInfo portInfo in switchPorts)
            {
                if (portInfo.Type == PortConnectionType.Internal)
                    internallyConnected = true;
                else if (portInfo.Type == PortConnectionType.External)
                    externallyConnected = true;
            }

            if (internallyConnected && externallyConnected)
                type = SwitchConnectionType.External;
            else if (internallyConnected)
                type = SwitchConnectionType.Internal;
            else if (externallyConnected)
                type = SwitchConnectionType.ExternalOnly;

            return type;
        }

        public static IEnumerable<string> GetConnectedVirtualMachineList(List<PortInfo> portList)
        {
            if (portList is null)
                throw new ArgumentNullException(nameof(portList));

            var virtualMachineNames = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (PortInfo portInfo in portList)
                if (portInfo.Type == PortConnectionType.VirtualMachine && !string.IsNullOrEmpty(portInfo.ConnectedName))
                    virtualMachineNames.Add(portInfo.ConnectedName);

            return virtualMachineNames;
        }

        public static bool SupportsTrunkMode(string externalAdapterName)
        {
            using (var eep = FindExternalAdapter(externalAdapterName))
            using (var leCollection = eep.GetRelated("Msvm_LanEndpoint"))
            {
                using (var le = leCollection.Cast<ManagementObject>().First())
                using (var otherLe = le.GetRelated("Msvm_LanEndpoint").Cast<ManagementObject>().First())
                using (var vlane = otherLe.GetRelated("Msvm_VLANEndpoint").Cast<ManagementObject>().First())
                    return
                        ((ushort[])vlane["SupportedEndpointModes"])
                            .Cast<ushort>()
                            .Where((supportedMode) => supportedMode == 5)
                            .Any();
            }
        }

        #endregion

        #region Utils

        public static ManagementObject GetDefaultEthernetPortAllocationSettingData()
        {
            using (var rp = ResourcePool.GetPool(ResourcePool.ResourceTypeInfo.EthernetConnection.ResourceSubType))
                return ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(rp);
        }

        public static ManagementObject FindExternalAdapter(string Name)
        {
            using (var mos = new ManagementObjectSearcher(Scope.Virtualization.ScopeObject, new ObjectQuery("SELECT * FROM Msvm_ExternalEthernetPort")))
                return mos.Get().Cast<ManagementObject>().Where((c) => c[nameof(Name)]?.ToString() == Name).First();
        }

        public static ManagementObject FindVirtualEthernetSwitch(string ElementName)
        {
            using (var mos = new ManagementObjectSearcher(Scope.Virtualization.ScopeObject, new ObjectQuery("SELECT * FROM Msvm_VirtualEthernetSwitch")))
                return mos.Get().Cast<ManagementObject>().Where((c) => c[nameof(ElementName)]?.ToString() == ElementName).First();
        }

        public static List<ManagementObject> FindConnectionsToSwitch(ComputerSystem vm, ManagementObject ethernetSwitch)
        {
            if (vm is null)
                throw new ArgumentNullException(nameof(vm));
            if (ethernetSwitch is null)
                throw new ArgumentNullException(nameof(ethernetSwitch));

            var connectionsToSwitch = new List<ManagementObject>();
            foreach (ManagementObject epasd in vm.VirtualSystemSettingData.GetEthernetPortAllocationSettingDataList())
            {
                string[] hostResource = (string[])epasd["HostResource"];
                if (hostResource != null && hostResource.Length > 0 && string.Equals(hostResource[0], ethernetSwitch.Path.Path, StringComparison.OrdinalIgnoreCase))
                {
                    connectionsToSwitch.Add(epasd);
                    continue;
                }

                epasd.GetRelated("Msvm_EthernetSwitchPort", "Msvm_ElementSettingData", null, null, null, null, false, null)
                    .Cast<ManagementObject>()
                    .Where((esp) => esp["SystemName"].ToString() == ethernetSwitch["Name"].ToString())
                    .ToList()
                    .ForEach((esd) => connectionsToSwitch.Add(esd));
            }

            return connectionsToSwitch;
        }

        public static string GetPortFeatureId(PortFeatureType featureType)
        {
            switch (featureType)
            {
                case PortFeatureType.Acl:       return "998BEF4A-5D55-492A-9C43-8B2F5EAE9F2B";
                case PortFeatureType.Bandwidth: return "24AD3CE1-69BD-4978-B2AC-DAAD389D699C";
                case PortFeatureType.Offload:   return "C885BFD1-ABB7-418F-8163-9F379C9F7166";
                case PortFeatureType.Profile:   return "9940CD46-8B06-43BB-B9D5-93D50381FD56";
                case PortFeatureType.Security:  return "776E0BA7-94A1-41C8-8F28-951F524251B5";
                case PortFeatureType.Vlan:      return "952C5004-4465-451C-8CB8-FA9AB382B773";
                default:                        throw new ArgumentOutOfRangeException($"The given port feature type is unrecognized [{featureType}]!");
            }
        }

        public static ManagementObject GetDefaultFeatureSetting(string featureId)
        {
            string defaultFeatureSettingPath = null;

            using (var esfcClass = new ManagementClass("Msvm_EthernetSwitchFeatureCapabilities"))
            {
                esfcClass.Scope = Scope.Virtualization.ScopeObject;

                esfcClass.GetInstances()
                    .Cast<ManagementObject>()
                    .Where((featureCapabilities) => string.Equals((string)featureCapabilities["FeatureId"], featureId, StringComparison.OrdinalIgnoreCase))
                    .ToList()
                    .ForEach((featureCapabilities) =>
                        featureCapabilities.GetRelationships("Msvm_FeatureSettingsDefineCapabilities")
                            .Cast<ManagementObject>()
                            .Where((fsdcAssociation) => (ushort)fsdcAssociation["ValueRole"] == 0)
                            .ToList()
                            .ForEach((fsdcAssociation) => defaultFeatureSettingPath = (string)fsdcAssociation["PartComponent"]));
            }

            if (defaultFeatureSettingPath == null)
                throw new InvalidOperationException($"Unable to find the default feature settings [{defaultFeatureSettingPath}]!");

            var defaultFeatureSetting = new ManagementObject(defaultFeatureSettingPath)
            {
                Scope = Scope.Virtualization.ScopeObject
            };

            defaultFeatureSetting.Get();

            return defaultFeatureSetting;
        }

        public static ManagementObject FindFeatureByName(string ElementName, ManagementScope scope)
        {
            using (var mos = new ManagementObjectSearcher(scope, new ObjectQuery("SELECT * FROM Msvm_EthernetSwitchFeatureCapabilities")))
                return mos.Get().Cast<ManagementObject>().Where((c) => c[nameof(ElementName)]?.ToString() == ElementName).First();
        }

        public static string GetSwitchFeatureId(SwitchFeatureType featureType)
        {
            switch (featureType)
            {
                case SwitchFeatureType.Bandwidth:   return "3EB2B8E8-4ABF-4DBF-9071-16DD47481FBE";
                default:                            throw new ArgumentOutOfRangeException($"The given switch feature type is unrecognized [{featureType}]!");
            }
        }

        #endregion
    }
}
