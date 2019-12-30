using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Networking
{
    public class EthernetSwitchPortSecuritySettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(EthernetSwitchPortSecuritySettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public EthernetSwitchPortSecuritySettingData() : base(ClassName) { }

        public EthernetSwitchPortSecuritySettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchPortSecuritySettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchPortSecuritySettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public EthernetSwitchPortSecuritySettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public EthernetSwitchPortSecuritySettingData(ManagementPath path) : base(path, ClassName) { }

        public EthernetSwitchPortSecuritySettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public EthernetSwitchPortSecuritySettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public EthernetSwitchPortSecuritySettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * Set to TRUE if traffic to/from port retains 802.1P info.
         */
        public bool AllowIeeePriorityTag
        {
            get
            {
                if (LateBoundObject[nameof(AllowIeeePriorityTag)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(AllowIeeePriorityTag)];
            }
            set
            {
                LateBoundObject[nameof(AllowIeeePriorityTag)] = value;
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Indicates whether the port will allow MAC spoofing.
         * TRUE: The port will allow MAC addresses to be spoofed. All valid unicast MAC address values are allowed.
         * FALSE: The port will allow only MAC addresses configured within Hyper-V management to be used. Default value is FALSE.
         */
        public bool AllowMacSpoofing
        {
            get
            {
                if (LateBoundObject[nameof(AllowMacSpoofing)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(AllowMacSpoofing)];
            }
            set
            {
                LateBoundObject[nameof(AllowMacSpoofing)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Indicates whether the NICs connected to the port can be part of a team.
         * This applies only to NICs connected to virtual machines.
         */
        public bool AllowTeaming
        {
            get
            {
                if (LateBoundObject[nameof(AllowTeaming)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(AllowTeaming)];
            }
            set
            {
                LateBoundObject[nameof(AllowTeaming)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public string Description => (string)LateBoundObject[nameof(Description)];

        /*
         * Defines the limit for number of Dynamic IP addresses learned.
         * Default is none.
         */
        public uint DynamicIPAddressLimit
        {
            get
            {
                if (LateBoundObject[nameof(DynamicIPAddressLimit)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(DynamicIPAddressLimit)];
            }
            set
            {
                LateBoundObject[nameof(DynamicIPAddressLimit)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        /*
         * Set to TRUE if Dhcp Offers are blocked from the port else FALSE.
         * Default value is FALSE.
         */
        public bool EnableDhcpGuard
        {
            get
            {
                if (LateBoundObject[nameof(EnableDhcpGuard)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(EnableDhcpGuard)];
            }
            set
            {
                LateBoundObject[nameof(EnableDhcpGuard)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Set to TRUE if Fixed Speed 10G is enabled else FALSE.
         * Default value is FALSE.
         */
        public bool EnableFixSpeed10G
        {
            get
            {
                if (LateBoundObject[nameof(EnableFixSpeed10G)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(EnableFixSpeed10G)];
            }
            set
            {
                LateBoundObject[nameof(EnableFixSpeed10G)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Set to TRUE if Router Advertisements and Router Redirects are blocked from the port else FALSE.
         * Default value is FALSE.
         */
        public bool EnableRouterGuard
        {
            get
            {
                if (LateBoundObject[nameof(EnableRouterGuard)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(EnableRouterGuard)];
            }
            set
            {
                LateBoundObject[nameof(EnableRouterGuard)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        /*
         * This indicates the monitor mode of the port.
         */
        public MonitorModeValues MonitorMode
        {
            get
            {
                if (LateBoundObject[nameof(MonitorMode)] == null)
                {
                    return (MonitorModeValues)System.Convert.ToInt32(3);
                }
                return (MonitorModeValues)System.Convert.ToInt32(LateBoundObject[nameof(MonitorMode)]);
            }
            set
            {
                if (MonitorModeValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(MonitorMode)] = null;
                }
                else
                {
                    LateBoundObject[nameof(MonitorMode)] = value;
                }
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * This gives the monitor session ID this port belongs to.
         */
        public byte MonitorSession
        {
            get
            {
                if (LateBoundObject[nameof(MonitorSession)] == null)
                {
                    return System.Convert.ToByte(0);
                }
                return (byte)LateBoundObject[nameof(MonitorSession)];
            }
            set
            {
                LateBoundObject[nameof(MonitorSession)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Reserved.
         */
        public bool Reserved
        {
            get
            {
                if (LateBoundObject[nameof(Reserved)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(Reserved)];
            }
            set
            {
                LateBoundObject[nameof(Reserved)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Defines the packets per second limit for broadcast and multicast traffic.
         * Default is none.
         */
        public uint StormLimit
        {
            get
            {
                if (LateBoundObject[nameof(StormLimit)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(StormLimit)];
            }
            set
            {
                LateBoundObject[nameof(StormLimit)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Reserved.
         */
        public string TeamName
        {
            get
            {
                return (string)LateBoundObject[nameof(TeamName)];
            }
            set
            {
                LateBoundObject[nameof(TeamName)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Reserved.
         */
        public uint TeamNumber
        {
            get
            {
                if (LateBoundObject[nameof(TeamNumber)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(TeamNumber)];
            }
            set
            {
                LateBoundObject[nameof(TeamNumber)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }
         
        /*
         * Defines the Virtual Subnet membership of the Port.
         * Default is none.
         */
        public uint VirtualSubnetId
        {
            get
            {
                if (LateBoundObject[nameof(VirtualSubnetId)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(VirtualSubnetId)];
            }
            set
            {
                LateBoundObject[nameof(VirtualSubnetId)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        private void ResetAllowIeeePriorityTag()
        {
            LateBoundObject[nameof(AllowIeeePriorityTag)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetAllowMacSpoofing()
        {
            LateBoundObject[nameof(AllowMacSpoofing)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetAllowTeaming()
        {
            LateBoundObject[nameof(AllowTeaming)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetDynamicIPAddressLimit()
        {
            LateBoundObject[nameof(DynamicIPAddressLimit)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetEnableDhcpGuard()
        {
            LateBoundObject[nameof(EnableDhcpGuard)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetEnableFixSpeed10G()
        {
            LateBoundObject[nameof(EnableFixSpeed10G)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetEnableRouterGuard()
        {
            LateBoundObject[nameof(EnableRouterGuard)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetMonitorMode()
        {
            LateBoundObject[nameof(MonitorMode)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetMonitorSession()
        {
            LateBoundObject[nameof(MonitorSession)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetReserved()
        {
            LateBoundObject[nameof(Reserved)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetStormLimit()
        {
            LateBoundObject[nameof(StormLimit)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetTeamName()
        {
            LateBoundObject[nameof(TeamName)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetTeamNumber()
        {
            LateBoundObject[nameof(TeamNumber)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetVirtualSubnetId()
        {
            LateBoundObject[nameof(VirtualSubnetId)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<EthernetSwitchPortSecuritySettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortSecuritySettingData(mo)).ToList();

        public new static List<EthernetSwitchPortSecuritySettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortSecuritySettingData(mo)).ToList();

        public static List<EthernetSwitchPortSecuritySettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortSecuritySettingData(mo)).ToList();

        public static List<EthernetSwitchPortSecuritySettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortSecuritySettingData(mo)).ToList();

        public static List<EthernetSwitchPortSecuritySettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortSecuritySettingData(mo)).ToList();

        public static List<EthernetSwitchPortSecuritySettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortSecuritySettingData(mo)).ToList();

        public static List<EthernetSwitchPortSecuritySettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortSecuritySettingData(mo)).ToList();

        public static List<EthernetSwitchPortSecuritySettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortSecuritySettingData(mo)).ToList();

        public static EthernetSwitchPortSecuritySettingData CreateInstance() => new EthernetSwitchPortSecuritySettingData(CreateInstance(ClassName));

        public enum MonitorModeValues
        {
            None = 0,
            Destination = 1,
            Source = 2,
            NULL_ENUM_VALUE = 3,
        }
    }
}
