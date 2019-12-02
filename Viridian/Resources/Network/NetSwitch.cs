using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Machine;
using Viridian.Resources.Msvm;
using Viridian.Service.Msvm;

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

        public static void CreatePrivateSwitch(ManagementScope scope, string switchName, string switchNotes)
        {
            CreateSwitch(scope, switchName, switchNotes, null);
        }

        public static void CreateInternalSwitch(ManagementScope scope, string switchName, string switchNotes)
        {
            using (var host = ComputerSystem.GetVirtualMachine(scope))
            using (var depasd = GetDefaultEthernetPortAllocationSettingData())
            {
                depasd["ElementName"] = switchName + "_Internal";
                depasd["HostResource"] = new string[] { host.Path.Path };

                string[] ports = new string[] { depasd.GetText(TextFormat.WmiDtd20) };

                CreateSwitch(scope, switchName, switchNotes, ports);
            }
        }

        public static void CreateExternalOnlySwitch(ManagementScope scope, string externalAdapterName, string switchName, string switchNotes)
        {
            using (var eep = FindExternalAdapter(scope, externalAdapterName))
            using (var depasd = GetDefaultEthernetPortAllocationSettingData())
            {
                depasd["ElementName"] = switchName + "_External";
                depasd["HostResource"] = new string[] { eep.Path.Path };

                string[] ports = new string[] { depasd.GetText(TextFormat.WmiDtd20) };

                CreateSwitch(scope, switchName, switchNotes, ports);
            }
        }

        public static void CreateExternalSwitch(ManagementScope scope, string externalAdapterName, string switchName, string switchNotes)
        {
            using (var eep = FindExternalAdapter(scope, externalAdapterName))
            using (var host = ComputerSystem.GetVirtualMachine(scope))
            using (var depasdInternal = GetDefaultEthernetPortAllocationSettingData())
            using (var depasdExternal = GetDefaultEthernetPortAllocationSettingData())
            {
                depasdExternal["ElementName"] = switchName + "_External";
                depasdExternal["HostResource"] = new string[] { eep.Path.Path };

                depasdInternal["ElementName"] = switchName + "_Internal";
                depasdInternal["HostResource"] = new string[] { host.Path.Path };
                depasdInternal["Address"] = eep["PermanentAddress"];

                string[] ports = new string[] { depasdExternal.GetText(TextFormat.WmiDtd20), depasdInternal.GetText(TextFormat.WmiDtd20) };

                CreateSwitch(scope, switchName, switchNotes, ports);
            }
        }

        public static void CreateSwitch(ManagementScope scope, string switchName, string switchNotes, string[] ports)
        {
            using (var vessd = new ManagementClass("Msvm_VirtualEthernetSwitchSettingData"))
            {
                vessd.Scope = scope;
                using (var vess = vessd.CreateInstance())
                {
                    vess["ElementName"] = switchName;
                    vess["Notes"] = new string[] { switchNotes };

                    VirtualEthernetSwitchManagement.Instance.DefineSystem(vess.GetText(TextFormat.WmiDtd20), ports, null);
                }
            }
        }

        #endregion

        #region Delete

        public static void DeleteSwitchWithPorts(ManagementScope scope, string switchName)
        {
            using (var ves = FindVirtualEthernetSwitch(scope, switchName))
                VirtualEthernetSwitchManagement.Instance.DestroySystem(ves.Path.Path);
        }

        #endregion

        #region Modify

        public static void ModifySwitchName(ManagementScope scope, string existingSwitchName, string newSwitchName, string newNotes)
        {
            using (var ves = FindVirtualEthernetSwitch(scope, existingSwitchName))
            using (var vessd = ves.GetRelated("Msvm_VirtualEthernetSwitchSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null).Cast<ManagementObject>().First())
            {
                vessd["ElementName"] = newSwitchName;
                vessd["Notes"] = new string[] { newNotes };

                VirtualEthernetSwitchManagement.Instance.ModifySystemSettings(vessd.GetText(TextFormat.WmiDtd20));
            }
        }

        public static void AddPorts(ManagementScope scope, string switchName, string Name)
        {
            using (var ves = FindVirtualEthernetSwitch(scope, switchName))
            using (var vessd = ves.GetRelated("Msvm_VirtualEthernetSwitchSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null).Cast<ManagementObject>().First())
            {
                using (var mos = new ManagementObjectSearcher(scope, new ObjectQuery("SELECT * FROM Msvm_ExternalEthernetPort")))
                using (var eep = mos.Get().Cast<ManagementObject>().Where((c) => c[nameof(Name)]?.ToString() == Name).First())
                using (var depasd = GetDefaultEthernetPortAllocationSettingData())
                {
                    depasd["ElementName"] = switchName + "_External";
                    depasd["HostResource"] = new string[] { eep.Path.Path };

                    VirtualEthernetSwitchManagement.Instance.AddResourceSettings(vessd.Path.Path, new string[] { depasd.GetText(TextFormat.WmiDtd20) });
                }
            }
        }

        public static void RemovePorts(ManagementScope scope, string switchName)
        {
            var searchedClassNames = new List<string>()
            {
                "Msvm_ComputerSystem",
                "Msvm_ExternalEthernetPort"
            };

            var portsToDelete = new List<string>();

            using (var ves = FindVirtualEthernetSwitch(scope, switchName))
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
                    throw new ViridianException(string.Format(CultureInfo.InvariantCulture, "The switch '{0}' does not have any internal or external ports to remove.", switchName));

                VirtualEthernetSwitchManagement.Instance.RemoveResourceSettings(portsToDelete.ToArray());
        }

        public static void ModifyPorts(ManagementScope scope, string switchName, string Name)
        {
            using (var mos = new ManagementObjectSearcher(scope, new ObjectQuery("SELECT * FROM Msvm_ExternalEthernetPort")))
            using (var eep = mos.Get().Cast<ManagementObject>().Where((c) => c[nameof(Name)]?.ToString() == Name).First())
            using (var ves = FindVirtualEthernetSwitch(scope, switchName))
            {
                var sdList = 
                    ves.GetRelated("Msvm_EthernetSwitchPort", "Msvm_SystemDevice", null, null, null, null, false, null)
                        .Cast<ManagementObject>()
                        .Where((port) =>
                        new ManagementPath(
                            ((string[])(port.GetRelated("Msvm_EthernetPortAllocationSettingData", "Msvm_ElementSettingData", null, null, null, null, false, null)
                                .Cast<ManagementObject>()
                                .First())
                                .GetPropertyValue("HostResource"))
                                .First())
                                .ClassName == "Msvm_ExternalEthernetPort" &&

                                new ManagementPath(
                                    ((string[])(port.GetRelated("Msvm_EthernetPortAllocationSettingData", "Msvm_ElementSettingData", null, null, null, null, false, null)
                                    .Cast<ManagementObject>()
                                    .First())
                                    .GetPropertyValue("HostResource"))
                                    .First())
                                    .Path == eep.Path.Path
                                )
                        .ToList();

                sdList
                    .ForEach(
                        (port) =>
                            port.GetRelated("Msvm_EthernetPortAllocationSettingData", "Msvm_ElementSettingData", null, null, null, null, false, null)
                            .Cast<ManagementObject>()
                            .First()
                            ["HostResource"] = new string[] { eep.Path.Path });

                sdList
                    .ForEach(
                        (port) =>
                            VirtualEthernetSwitchManagement.Instance.ModifyResourceSettings(
                                new string[] 
                                {
                                    port.GetRelated("Msvm_EthernetPortAllocationSettingData", "Msvm_ElementSettingData", null, null, null, null, false, null)
                                        .Cast<ManagementObject>()
                                        .First()
                                        .GetText(TextFormat.WmiDtd20)
                                }));
            }
        }

        public static void AddCustomFeatureSettings(ComputerSystem virtualMachine, PortFeatureType featureType)
        {
            string featureId = GetPortFeatureId(featureType);

            using (var connections = FindConnections(virtualMachine?.MsvmComputerSystem))
            using (var defaultFeatureSetting = GetDefaultFeatureSetting(featureId, virtualMachine.Scope))
            {
                switch (featureType)
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
                        throw new ViridianException("", new ArgumentOutOfRangeException(featureType.ToString()));
                }

                connections
                    .Cast<ManagementObject>()
                    .ToList()
                    .ForEach((connection) => VirtualSystemManagement.Instance.AddFeatureSettings(connection.Path.Path, new string[] { defaultFeatureSetting.GetText(TextFormat.WmiDtd20) }));
            }
        }

        public static void ModifyCustomFeatureSettings(ComputerSystem vm)
        {
            // featureSetting["AllowMacSpoofing"] | featureSetting["IOVQueuePairsRequested"]
            // Msvm_EthernetSwitchPortSecuritySettingData | Msvm_EthernetSwitchPortOffloadSettingData

            using (var connections = FindConnections(vm?.MsvmComputerSystem))
            {
                connections
                    .Cast<ManagementObject>()
                    .ToList()
                    .ForEach((connection) =>
                        connection.GetRelated("Msvm_EthernetSwitchPortSecuritySettingData", "Msvm_EthernetPortSettingDataComponent", null, null, null, null, false, null)
                            .Cast<ManagementObject>()
                            .ToList()
                            .ForEach((epsdc) => epsdc["AllowMacSpoofing"] = true));

                connections
                    .Cast<ManagementObject>()
                    .ToList()
                    .ForEach((connection) =>
                        connection.GetRelated("Msvm_EthernetSwitchPortSecuritySettingData", "Msvm_EthernetPortSettingDataComponent", null, null, null, null, false, null)
                            .Cast<ManagementObject>()
                            .ToList()
                            .ForEach((epsdc) => VirtualSystemManagement.Instance.ModifyFeatureSettings(new string[] { epsdc.GetText(TextFormat.WmiDtd20) })));
            }
        }

        public static void RemoveFeatureSettings(ComputerSystem vm, PortFeatureType featureType)
        {
            using (var connections = FindConnections(vm?.MsvmComputerSystem))
            {
                string featureSettingClass;

                switch (featureType)
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
                        throw new ViridianException("", new ArgumentOutOfRangeException(featureType.ToString()));
                }

                foreach (ManagementObject ethernetConnectionSetting in connections)
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

                    VirtualSystemManagement.Instance.RemoveFeatureSettings(featureSettingPaths.ToArray());
                }
            }
        }

        public static void SetExtensionEnabledState(ManagementScope scope, string switchName, string extensionName, bool enabled)
        {
            bool found = false;

            using (var ves = FindVirtualEthernetSwitch(scope, switchName))
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
                                Validator.ValidateOutput(op, scope);
                        }
                    }
            }

            if (!found)
                throw new ViridianException(string.Format(CultureInfo.CurrentCulture, "Could not find extension '{0}' on switch '{1}'.", extensionName, switchName));
        }

        public static void MoveExtension(ManagementScope scope, string switchName, string extensionName, int offset)
        {
            using (var ves = FindVirtualEthernetSwitch(scope, switchName))
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
                    throw new ViridianException(string.Format(CultureInfo.CurrentCulture, "Could not find extension '{0}' on switch '{1}'.", extensionName, switchName));

                int newExtensionIndex = extensionIndex + offset;

                if ((newExtensionIndex < 0) || (newExtensionIndex >= extensionOrder.Length))
                    throw new ViridianException("Invalid move operation.");

                if (extensionTypes[newExtensionIndex] != extensionTypes[extensionIndex])
                    throw new ViridianException("Invalid move operation.");

                string temp = extensionOrder[newExtensionIndex];
                extensionOrder[newExtensionIndex] = extensionOrder[extensionIndex];
                extensionOrder[extensionIndex] = temp;

                vessd["ExtensionOrder"] = extensionOrder;

                VirtualEthernetSwitchManagement.Instance.ModifySystemSettings(vessd.GetText(TextFormat.WmiDtd20));
            }
        }

        public static void SetRequiredFeature(ComputerSystem vm, string featureName, bool required)
        {
            var connectionsToModify = new List<string>();

            using (var feature = FindFeatureByName(featureName, vm?.Scope))
            using (var connectionCollection = FindConnections(vm.MsvmComputerSystem))
            {
                foreach (ManagementObject connection in connectionCollection)
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
            }

            if (connectionsToModify.Count > 0)
                VirtualSystemManagement.Instance.ModifyResourceSettings(connectionsToModify.ToArray());
        }

        public static void AddBandwithSettings(ManagementScope scope, string switchName, ulong bytesPerSecond)
        {
            string featureId = GetSwitchFeatureId(SwitchFeatureType.Bandwidth);

            using (var ves = FindVirtualEthernetSwitch(scope, switchName))
            using (var vessd = ves.GetRelated("Msvm_VirtualEthernetSwitchSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null).Cast<ManagementObject>().First())
            using (ManagementObject bandwidthSetting = GetDefaultFeatureSetting(featureId, scope))
            {
                bandwidthSetting["DefaultFlowReservation"] = bytesPerSecond;

                VirtualEthernetSwitchManagement.Instance.AddFeatureSettings(vessd.Path.Path, new string[] { bandwidthSetting.GetText(TextFormat.WmiDtd20) });
            }
        }

        public static void ModifyFeatureSettings(ManagementScope scope, string switchName, ulong bytesPerSecond)
        {
            using (var ves = FindVirtualEthernetSwitch(scope,  switchName))
            using (var vessd = ves.GetRelated("Msvm_VirtualEthernetSwitchSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null).Cast<ManagementObject>().First())
            using (var bandwidthSetting = vessd.GetRelated("Msvm_VirtualEthernetSwitchBandwidthSettingData","Msvm_VirtualEthernetSwitchSettingDataComponent", null, null, null, null, false, null).Cast<ManagementObject>().First())
            {
                bandwidthSetting["DefaultFlowReservation"] = bytesPerSecond;

                VirtualEthernetSwitchManagement.Instance.ModifyResourceSettings(new string[] { bandwidthSetting.GetText(TextFormat.WmiDtd20) });
            }
        }

        public static void RemoveFeatureSettings(ManagementScope scope, string switchName)
        {
            using (var ves = FindVirtualEthernetSwitch(scope, switchName))
            using (var vessd = ves.GetRelated("Msvm_VirtualEthernetSwitchSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null).Cast<ManagementObject>().First())
            using (var vesbsd = vessd.GetRelated("Msvm_VirtualEthernetSwitchBandwidthSettingData", "Msvm_VirtualEthernetSwitchSettingDataComponent", null, null, null, null, false, null).Cast<ManagementObject>().First())
                VirtualEthernetSwitchManagement.Instance.RemoveFeatureSettings(new string[] { vesbsd.Path.Path });
        }

        public static void ModifyClusterMonitored(ComputerSystem vm, bool onOff)
        {
            using (var virtualMachineSettings = ComputerSystem.GetVirtualMachineSettings(vm?.MsvmComputerSystem))
            using (var syntheticPortSettings = virtualMachineSettings.GetRelated("Msvm_SyntheticEthernetPortSettingData", "Msvm_VirtualSystemSettingDataComponent", null, null, null, null, false, null))
                foreach (ManagementObject syntheticEthernetPortSetting in syntheticPortSettings)
                {
                    syntheticEthernetPortSetting["ClusterMonitored"] = onOff;

                    VirtualSystemManagement.Instance.ModifyResourceSettings(new string[] { syntheticEthernetPortSetting.GetText(TextFormat.CimDtd20) });
                }
        }

        #endregion

        #region Info

        public static SwitchInfo GetSwitch(ManagementScope scope, string switchName)
        {
            SwitchInfo ethernetSwitchInfo = new SwitchInfo
            {
                Name = switchName,
                Type = SwitchConnectionType.Private,
                PortList = new List<PortInfo>(),
                SwitchFeatureList = new List<SwitchFeatureType>()
            };

            using (ManagementObject ves = FindVirtualEthernetSwitch(scope, switchName))
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
                throw new ViridianException("", new ArgumentNullException(nameof(portSettings)));

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
                throw new ViridianException("", new ArgumentNullException(nameof(portFeature)));

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
                throw new ViridianException("", new ArgumentNullException(nameof(switchFeature)));

            switch (switchFeature.Path.ClassName)
            {
                case "Msvm_VirtualEthernetSwitchBandwidthSettingData":  return SwitchFeatureType.Bandwidth;
                default:                                                return SwitchFeatureType.Unknown;
            }
        }

        public static SwitchConnectionType DetermineSwitchConnectionType(List<PortInfo> switchPorts)
        {
            if (switchPorts is null)
                throw new ViridianException("", new ArgumentNullException(nameof(switchPorts)));

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
                throw new ViridianException("", new ArgumentNullException(nameof(portList)));

            var virtualMachineNames = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (PortInfo portInfo in portList)
                if (portInfo.Type == PortConnectionType.VirtualMachine && !string.IsNullOrEmpty(portInfo.ConnectedName))
                    virtualMachineNames.Add(portInfo.ConnectedName);

            return virtualMachineNames;
        }

        public static bool SupportsTrunkMode(ManagementScope scope, string externalAdapterName)
        {
            using (var eep = FindExternalAdapter(scope, externalAdapterName))
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

        public static ManagementObject FindExternalAdapter(ManagementScope scope, string Name)
        {
            using (var mos = new ManagementObjectSearcher(scope, new ObjectQuery("SELECT * FROM Msvm_ExternalEthernetPort")))
                return mos.Get().Cast<ManagementObject>().Where((c) => c[nameof(Name)]?.ToString() == Name).First();
        }

        public static ManagementObject FindVirtualEthernetSwitch(ManagementScope scope, string ElementName)
        {
            using (var mos = new ManagementObjectSearcher(scope, new ObjectQuery("SELECT * FROM Msvm_VirtualEthernetSwitch")))
                return mos.Get().Cast<ManagementObject>().Where((c) => c[nameof(ElementName)]?.ToString() == ElementName).First();
        }

        public static List<ManagementObject> FindConnectionsToSwitch(ManagementObject virtualMachine, ManagementObject ethernetSwitch)
        {
            if (virtualMachine is null)
                throw new ViridianException("", new ArgumentNullException(nameof(virtualMachine)));
            if (ethernetSwitch is null)
                throw new ViridianException("", new ArgumentNullException(nameof(ethernetSwitch)));

            var connectionsToSwitch = new List<ManagementObject>();

            using (var epasdCollection = FindConnections(virtualMachine))
                foreach (ManagementObject epasd in epasdCollection)
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

        public static ManagementObjectCollection FindConnections(ManagementObject virtualMachine)
        {
            using (ManagementObject vms = ComputerSystem.GetVirtualMachineSettings(virtualMachine))
                return vms.GetRelated("Msvm_EthernetPortAllocationSettingData", "Msvm_VirtualSystemSettingDataComponent", null, null, null, null, false, null);
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
                default:                        throw new ViridianException("The given port feature type is unrecognized.");
            }
        }

        public static ManagementObject GetDefaultFeatureSetting(string featureId, ManagementScope scope)
        {
            string defaultFeatureSettingPath = null;

            using (var esfcClass = new ManagementClass("Msvm_EthernetSwitchFeatureCapabilities"))
            {
                esfcClass.Scope = scope;

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
                throw new ViridianException("Unable to find the default feature settings!");

            var defaultFeatureSetting = new ManagementObject(defaultFeatureSettingPath)
            {
                Scope = scope
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
                default:                            throw new ViridianException("The given switch feature type is unrecognized.");
            }
        }

        #endregion
    }
}
