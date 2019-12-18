using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.Networking
{
    public class SyntheticEthernetPortSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(SyntheticEthernetPortSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public SyntheticEthernetPortSettingData() : base(ClassName) { }

        public SyntheticEthernetPortSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public SyntheticEthernetPortSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public SyntheticEthernetPortSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public SyntheticEthernetPortSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public SyntheticEthernetPortSettingData(ManagementPath path) : base(path, ClassName) { }

        public SyntheticEthernetPortSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public SyntheticEthernetPortSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public SyntheticEthernetPortSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string Address => (string)LateBoundObject[nameof(Address)];

        public string AddressOnParent => (string)LateBoundObject[nameof(AddressOnParent)];

        public string AllocationUnits => (string)LateBoundObject[nameof(AllocationUnits)];

        /*
         * Indicates if PacketDirect projection is enabled for the VM.
         */
        public bool AllowPacketDirect
        {
            get
            {
                if (LateBoundObject[nameof(AllowPacketDirect)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(AllowPacketDirect)];
            }
            set
            {
                LateBoundObject[nameof(AllowPacketDirect)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool AutomaticAllocation
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticAllocation)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(AutomaticAllocation)];
            }
        }

        public bool AutomaticDeallocation
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticDeallocation)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(AutomaticDeallocation)];
            }
        }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        /*
         * Indicates whether this ethernet adapter is monitored by cluster.
         * This is a read-only property, but it can be changed using the ModifyVirtualSystemResources method of the Msvm_VirtualSystemManagementService class.
         */
        public bool ClusterMonitored
        {
            get
            {
                if (LateBoundObject[nameof(ClusterMonitored)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(ClusterMonitored)];
            }
        }

        public string[] Connection => (string[])LateBoundObject[nameof(Connection)];

        public ushort ConsumerVisibility
        {
            get
            {
                if (LateBoundObject[nameof(ConsumerVisibility)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ConsumerVisibility)];
            }
        }

        public string Description => (string)LateBoundObject[nameof(Description)];

        public ushort DesiredVLANEndpointMode
        {
            get
            {
                if (LateBoundObject[nameof(DesiredVLANEndpointMode)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(DesiredVLANEndpointMode)];
            }
        }

        /*
         * Indicates whether this ethernet adapter supports device naming.
         * This is a read-only property, but it can be changed using the ModifyVirtualSystemResources method of the Msvm_VirtualSystemManagementService class.
         */
        public bool DeviceNamingEnabled
        {
            get
            {
                if (LateBoundObject[nameof(DeviceNamingEnabled)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(DeviceNamingEnabled)];
            }
        }

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public string[] HostResource => (string[])LateBoundObject[nameof(HostResource)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        /*
         * Indicates if Interrupt Moderation is Enabled.
         * This is a read-only property, but it can be changed using the ModifyVirtualSystemResources method of the Msvm_VirtualSystemManagementService class.
         */
        public bool InterruptModeration
        {
            get
            {
                if (LateBoundObject[nameof(InterruptModeration)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(InterruptModeration)];
            }
        }

        public ulong Limit
        {
            get
            {
                if (LateBoundObject[nameof(Limit)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(Limit)];
            }
        }

        public ushort MappingBehavior
        {
            get
            {
                if (LateBoundObject[nameof(MappingBehavior)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(MappingBehavior)];
            }
        }

        /*
         * Indicates what type of network is supported by the NIC.
         * This is a read-only property, but it can be changed using the ModifyVirtualSystemResources method of theMsvm_VirtualSystemManagementService class.
         */
        public uint MediaType
        {
            get
            {
                if (LateBoundObject[nameof(MediaType)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(MediaType)];
            }
        }

        /*
         * Indicates if this ethernet adapter can influence VM placement using its NUMA proximity.
         */
        public bool NumaAwarePlacement
        {
            get
            {
                if (LateBoundObject[nameof(NumaAwarePlacement)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(NumaAwarePlacement)];
            }
            set
            {
                LateBoundObject[nameof(NumaAwarePlacement)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string OtherEndpointMode => (string)LateBoundObject[nameof(OtherEndpointMode)];

        public string OtherResourceType => (string)LateBoundObject[nameof(OtherResourceType)];

        public string Parent => (string)LateBoundObject[nameof(Parent)];

        public string PoolID => (string)LateBoundObject[nameof(PoolID)];

        public ulong Reservation
        {
            get
            {
                if (LateBoundObject[nameof(Reservation)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(Reservation)];
            }
        }

        public string ResourceSubType => (string)LateBoundObject[nameof(ResourceSubType)];

        public ushort ResourceType
        {
            get
            {
                if (LateBoundObject[nameof(ResourceType)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ResourceType)];
            }
        }

        /*
         * Indicates a static MAC address.
         * This is a read-only property, but it can be changed using the ModifyVirtualSystemResources method of the Msvm_VirtualSystemManagementService class.
         */
        public bool StaticMacAddress
        {
            get
            {
                if (LateBoundObject[nameof(StaticMacAddress)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(StaticMacAddress)];
            }
        }

        public ulong VirtualQuantity
        {
            get
            {
                if (LateBoundObject[nameof(VirtualQuantity)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(VirtualQuantity)];
            }
        }

        public string VirtualQuantityUnits => (string)LateBoundObject[nameof(VirtualQuantityUnits)];

        /*
         * A free-form string array of identifiers of this resource presented to the virtual computer system's operating system.
         * The indexes and values per index are defined on a per resource basis (that is, for each enumerated ResourceType value).
         * This property is set to "GUID".
         * This is a read-only property, but it can be changed using the ModifyVirtualSystemResources method of the sd class.
         */
        public string[] VirtualSystemIdentifiers => (string[])LateBoundObject[nameof(VirtualSystemIdentifiers)];

        public uint Weight
        {
            get
            {
                if (LateBoundObject[nameof(Weight)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(Weight)];
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<SyntheticEthernetPortSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new SyntheticEthernetPortSettingData(mo)).ToList();

        public new static List<SyntheticEthernetPortSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new SyntheticEthernetPortSettingData(mo)).ToList();

        public static List<SyntheticEthernetPortSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SyntheticEthernetPortSettingData(mo)).ToList();

        public static List<SyntheticEthernetPortSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SyntheticEthernetPortSettingData(mo)).ToList();

        public static List<SyntheticEthernetPortSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new SyntheticEthernetPortSettingData(mo)).ToList();

        public static List<SyntheticEthernetPortSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new SyntheticEthernetPortSettingData(mo)).ToList();

        public static List<SyntheticEthernetPortSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SyntheticEthernetPortSettingData(mo)).ToList();

        public static List<SyntheticEthernetPortSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SyntheticEthernetPortSettingData(mo)).ToList();

        public static SyntheticEthernetPortSettingData CreateInstance() => new SyntheticEthernetPortSettingData(CreateInstance(ClassName));
    }
}
