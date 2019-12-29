using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Integration
{
    public class GuestServiceInterfaceComponentSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(GuestServiceInterfaceComponentSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public GuestServiceInterfaceComponentSettingData() : base(ClassName) { }

        public GuestServiceInterfaceComponentSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public GuestServiceInterfaceComponentSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public GuestServiceInterfaceComponentSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public GuestServiceInterfaceComponentSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public GuestServiceInterfaceComponentSettingData(ManagementPath path) : base(path, ClassName) { }

        public GuestServiceInterfaceComponentSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public GuestServiceInterfaceComponentSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public GuestServiceInterfaceComponentSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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

        /*
         * The enabled and disabled states of guest communication services by default.
         * Valid values are 2 (enabled) and 3 (disabled).
         * The default value is 2 (enabled).
         * This is a read-only property, but it can be changed using the ModifyResourceSettings method of the Msvm_VirtualSystemManagementService class.
         */
        public DefaultEnabledStatePolicyValues DefaultEnabledStatePolicy
        {
            get
            {
                if (LateBoundObject[nameof(DefaultEnabledStatePolicy)] == null)
                {
                    return (DefaultEnabledStatePolicyValues)System.Convert.ToInt32(0);
                }
                return (DefaultEnabledStatePolicyValues)System.Convert.ToInt32(LateBoundObject[nameof(DefaultEnabledStatePolicy)]);
            }
        }

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        /*
         * The enabled and disabled states of an element.
         * Valid values are 2 (enabled) and 3 (disabled).
         * The default value is 3 (disabled).
         * This is a read-only property, but it can be changed using the ModifyResourceSettings method of the Msvm_VirtualSystemManagementService class.
         */
        public EnabledStateValues EnabledState
        {
            get
            {
                if (LateBoundObject[nameof(EnabledState)] == null)
                {
                    return (EnabledStateValues)System.Convert.ToInt32(0);
                }
                return (EnabledStateValues)System.Convert.ToInt32(LateBoundObject[nameof(EnabledState)]);
            }
        }

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
        public static List<GuestServiceInterfaceComponentSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponentSettingData(mo)).ToList();

        public new static List<GuestServiceInterfaceComponentSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponentSettingData(mo)).ToList();

        public static List<GuestServiceInterfaceComponentSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponentSettingData(mo)).ToList();

        public static List<GuestServiceInterfaceComponentSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponentSettingData(mo)).ToList();

        public static List<GuestServiceInterfaceComponentSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponentSettingData(mo)).ToList();

        public static List<GuestServiceInterfaceComponentSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponentSettingData(mo)).ToList();

        public static List<GuestServiceInterfaceComponentSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponentSettingData(mo)).ToList();

        public static List<GuestServiceInterfaceComponentSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponentSettingData(mo)).ToList();

        public static GuestServiceInterfaceComponentSettingData CreateInstance() => new GuestServiceInterfaceComponentSettingData(CreateInstance(ClassName));

        public enum DefaultEnabledStatePolicyValues
        {
            Enabled = 2,
            Disabled = 3,
            NULL_ENUM_VALUE = 0,
        }

        public enum EnabledStateValues
        {
            Enabled = 2,
            Disabled = 3,
            NULL_ENUM_VALUE = 0,
        }
    }
}
