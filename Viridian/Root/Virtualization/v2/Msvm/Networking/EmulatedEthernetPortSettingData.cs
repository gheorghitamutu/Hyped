using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Networking
{
    public class EmulatedEthernetPortSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(EmulatedEthernetPortSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public EmulatedEthernetPortSettingData() : base(ClassName) { }

        public EmulatedEthernetPortSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EmulatedEthernetPortSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EmulatedEthernetPortSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public EmulatedEthernetPortSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public EmulatedEthernetPortSettingData(ManagementPath path) : base(path, ClassName) { }

        public EmulatedEthernetPortSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public EmulatedEthernetPortSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public EmulatedEthernetPortSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string Address => (string)LateBoundObject[nameof(Address)];

        public string AddressOnParent => (string)LateBoundObject[nameof(AddressOnParent)];

        public string AllocationUnits => (string)LateBoundObject[nameof(AllocationUnits)];

        public bool AutomaticAllocation
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticAllocation)] == null)
                {
                    return System.Convert.ToBoolean(0);
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
                    return System.Convert.ToBoolean(0);
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
                    return System.Convert.ToBoolean(0);
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
                    return System.Convert.ToUInt16(0);
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
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(DesiredVLANEndpointMode)];
            }
        }

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public string[] HostResource => (string[])LateBoundObject[nameof(HostResource)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        public ulong Limit
        {
            get
            {
                if (LateBoundObject[nameof(Limit)] == null)
                {
                    return System.Convert.ToUInt64(0);
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
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(MappingBehavior)];
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
                    return System.Convert.ToUInt64(0);
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
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ResourceType)];
            }
        }

        /*
         * Indicates whether to use a static MAC address.
         * This is a read-only property, but it can be changed using the ModifyVirtualSystemResources method of the Msvm_VirtualSystemManagementService class.
         */
        public bool StaticMacAddress
        {
            get
            {
                if (LateBoundObject[nameof(StaticMacAddress)] == null)
                {
                    return System.Convert.ToBoolean(0);
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
                    return System.Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(VirtualQuantity)];
            }
        }

        public string VirtualQuantityUnits => (string)LateBoundObject[nameof(VirtualQuantityUnits)];

        public uint Weight
        {
            get
            {
                if (LateBoundObject[nameof(Weight)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(Weight)];
            }
        }



        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<EmulatedEthernetPortSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new EmulatedEthernetPortSettingData(mo)).ToList();

        public new static List<EmulatedEthernetPortSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EmulatedEthernetPortSettingData(mo)).ToList();

        public static List<EmulatedEthernetPortSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EmulatedEthernetPortSettingData(mo)).ToList();

        public static List<EmulatedEthernetPortSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EmulatedEthernetPortSettingData(mo)).ToList();

        public static List<EmulatedEthernetPortSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new EmulatedEthernetPortSettingData(mo)).ToList();

        public static List<EmulatedEthernetPortSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EmulatedEthernetPortSettingData(mo)).ToList();

        public static List<EmulatedEthernetPortSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EmulatedEthernetPortSettingData(mo)).ToList();

        public static List<EmulatedEthernetPortSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EmulatedEthernetPortSettingData(mo)).ToList();

        public static EmulatedEthernetPortSettingData CreateInstance() => new EmulatedEthernetPortSettingData(CreateInstance(ClassName));
    }
}
