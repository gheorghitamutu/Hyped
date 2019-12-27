using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Networking
{
    public sealed class VirtualEthernetSwitchSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(VirtualEthernetSwitchSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public VirtualEthernetSwitchSettingData() : base(ClassName) { }

        public VirtualEthernetSwitchSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualEthernetSwitchSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualEthernetSwitchSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public VirtualEthernetSwitchSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public VirtualEthernetSwitchSettingData(ManagementPath path) : base(path, ClassName) { }

        public VirtualEthernetSwitchSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public VirtualEthernetSwitchSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public VirtualEthernetSwitchSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string[] AssociatedResourcePool => (string[])LateBoundObject[nameof(AssociatedResourcePool)];

        public ushort AutomaticRecoveryAction
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticRecoveryAction)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(AutomaticRecoveryAction)];
            }
        }

        public ushort AutomaticShutdownAction
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticShutdownAction)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(AutomaticShutdownAction)];
            }
        }

        public ushort AutomaticStartupAction
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticStartupAction)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(AutomaticStartupAction)];
            }
        }

        public DateTime AutomaticStartupActionDelay
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticStartupActionDelay)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(AutomaticStartupActionDelay)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public ushort AutomaticStartupActionSequenceNumber
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticStartupActionSequenceNumber)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(AutomaticStartupActionSequenceNumber)];
            }
        }

        /*
         * The bandwidth reservation mode.
         */
        public BandwidthReservationModeValues BandwidthReservationMode
        {
            get
            {
                if (LateBoundObject[nameof(BandwidthReservationMode)] == null)
                {
                    return (BandwidthReservationModeValues)Convert.ToInt32(4);
                }
                return (BandwidthReservationModeValues)Convert.ToInt32(LateBoundObject[nameof(BandwidthReservationMode)]);
            }
            set
            {
                if (BandwidthReservationModeValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(BandwidthReservationMode)] = null;
                }
                else
                {
                    LateBoundObject[nameof(BandwidthReservationMode)] = value;
                }
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public string ConfigurationDataRoot => (string)LateBoundObject[nameof(ConfigurationDataRoot)];

        public string ConfigurationFile => (string)LateBoundObject[nameof(ConfigurationFile)];

        public string ConfigurationID => (string)LateBoundObject[nameof(ConfigurationID)];

        public DateTime CreationTime
        {
            get
            {
                if (LateBoundObject[nameof(CreationTime)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(CreationTime)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        /*
         * Contains references to the switch extensions bound to this switch, in the order in which they are applied.
         * Each string should be the full path to an Msvm_EthernetSwitchExtension instance.
         */
        public string[] ExtensionOrder
        {
            get
            {
                return (string[])LateBoundObject[nameof(ExtensionOrder)];
            }
            set
            {
                LateBoundObject[nameof(ExtensionOrder)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        /*
         * Specifies whether SR-IOV is preferred or not, if available on the underlying adapter.
         */
        public bool IOVPreferred
        {
            get
            {
                if (LateBoundObject[nameof(IOVPreferred)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IOVPreferred)];
            }
            set
            {
                LateBoundObject[nameof(IOVPreferred)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string LogDataRoot => (string)LateBoundObject[nameof(LogDataRoot)];

        public uint MaxNumMACAddress
        {
            get
            {
                if (LateBoundObject[nameof(MaxNumMACAddress)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(MaxNumMACAddress)];
            }
        }

        public string[] Notes => (string[])LateBoundObject[nameof(Notes)];

        /*
         * Specifies whether PacketDirect should be used, if available.
         */
        public bool PacketDirectEnabled
        {
            get
            {
                if (LateBoundObject[nameof(PacketDirectEnabled)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(PacketDirectEnabled)];
            }
            set
            {
                LateBoundObject[nameof(PacketDirectEnabled)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string RecoveryFile => (string)LateBoundObject[nameof(RecoveryFile)];

        public string SnapshotDataRoot => (string)LateBoundObject[nameof(SnapshotDataRoot)];

        public string SuspendDataRoot => (string)LateBoundObject[nameof(SuspendDataRoot)];

        public string SwapFileDataRoot => (string)LateBoundObject[nameof(SwapFileDataRoot)];

        /*
         * Specifies whether NIC Teaming should be used.
         */
        public bool TeamingEnabled
        {
            get
            {
                if (LateBoundObject[nameof(TeamingEnabled)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(TeamingEnabled)];
            }
            set
            {
                LateBoundObject[nameof(TeamingEnabled)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string VirtualSystemIdentifier => (string)LateBoundObject[nameof(VirtualSystemIdentifier)];

        public string VirtualSystemType => (string)LateBoundObject[nameof(VirtualSystemType)];

        public string[] VLANConnection => (string[])LateBoundObject[nameof(VLANConnection)];
        
        private void ResetBandwidthReservationMode()
        {
            LateBoundObject[nameof(BandwidthReservationMode)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }
        
        private void ResetExtensionOrder()
        {
            LateBoundObject[nameof(ExtensionOrder)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetIOVPreferred()
        {
            LateBoundObject[nameof(IOVPreferred)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }
        
        private void ResetPacketDirectEnabled()
        {
            LateBoundObject[nameof(PacketDirectEnabled)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetTeamingEnabled()
        {
            LateBoundObject[nameof(TeamingEnabled)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<VirtualEthernetSwitchSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchSettingData(mo)).ToList();

        public new static List<VirtualEthernetSwitchSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchSettingData(mo)).ToList();

        public static List<VirtualEthernetSwitchSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchSettingData(mo)).ToList();

        public static List<VirtualEthernetSwitchSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchSettingData(mo)).ToList();

        public static List<VirtualEthernetSwitchSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchSettingData(mo)).ToList();

        public static List<VirtualEthernetSwitchSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchSettingData(mo)).ToList();

        public static List<VirtualEthernetSwitchSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchSettingData(mo)).ToList();

        public static List<VirtualEthernetSwitchSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchSettingData(mo)).ToList();

        public static VirtualEthernetSwitchSettingData CreateInstance() => new VirtualEthernetSwitchSettingData(CreateInstance(ClassName));

        public enum BandwidthReservationModeValues
        {
            Default = 0,
            Weight = 1,
            Absolute = 2,
            None = 3,
            NULL_ENUM_VALUE = 4,
        }
    }
}
