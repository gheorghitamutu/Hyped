using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Viridian.Msvm.Networking
{
    public class EthernetPortAllocationSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(EthernetPortAllocationSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public EthernetPortAllocationSettingData() : base(ClassName) { }

        public EthernetPortAllocationSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetPortAllocationSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetPortAllocationSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public EthernetPortAllocationSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public EthernetPortAllocationSettingData(ManagementPath path) : base(path, ClassName) { }

        public EthernetPortAllocationSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public EthernetPortAllocationSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public EthernetPortAllocationSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string Address => (string)LateBoundObject[nameof(Address)];

        public string AddressOnParent => (string)LateBoundObject[nameof(AddressOnParent)];

        public string AllocationUnits => (string)LateBoundObject[nameof(AllocationUnits)];

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
         * This property specifies the target network compartment for the port.
         * It is only supported for internal adapters.
         */
        public string CompartmentGuid
        {
            get
            {
                return (string)LateBoundObject[nameof(CompartmentGuid)];
            }
            set
            {
                LateBoundObject[nameof(CompartmentGuid)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
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

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        /*
         * EnabledState is an integer enumeration that indicates whether the allocation request is enabled or disabled.
         * When an allocation request is marked as Disabled (3), then the allocation is not processed.
         * The EnabledState for an active configuration is always marked as Enabled (2).
         */
        public EnabledStateValues EnabledState
        {
            get
            {
                if (LateBoundObject[nameof(EnabledState)] == null)
                {
                    return (EnabledStateValues)Convert.ToInt32(0);
                }
                return (EnabledStateValues)Convert.ToInt32(LateBoundObject[nameof(EnabledState)]);
            }
        }

        public string[] HostResource => (string[])LateBoundObject[nameof(HostResource)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        /*
         * The last known friendly name of the switch this port had a hard affinity to, if any.
         */
        public string LastKnownSwitchName => (string)LateBoundObject[nameof(LastKnownSwitchName)];

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

        public string OtherEndpointMode => (string)LateBoundObject[nameof(OtherEndpointMode)];

        public string OtherResourceType => (string)LateBoundObject[nameof(OtherResourceType)];

        public string Parent => (string)LateBoundObject[nameof(Parent)];

        public string PoolID => (string)LateBoundObject[nameof(PoolID)];

        /*
         * The list of friendly names corresponding to each entry in the RequiredFeatures.
         */
        public string[] RequiredFeatureHints => (string[])LateBoundObject[nameof(RequiredFeatureHints)];

        /*
         * The list of feature identifers representing all the features that are required for a port.
         */
        public string[] RequiredFeatures
        {
            get
            {
                return (string[])LateBoundObject[nameof(RequiredFeatures)];
            }
            set
            {
                LateBoundObject[nameof(RequiredFeatures)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

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
         * This property specifies the network resource pool from which a connection will be allocated to test replica system when it is created.
         */
        public string TestReplicaPoolID
        {
            get
            {
                return (string)LateBoundObject[nameof(TestReplicaPoolID)];
            }
            set
            {
                LateBoundObject[nameof(TestReplicaPoolID)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * This property specifies the friendly name of the virtual network switch to which a connection will be allocated for the test replica system when it is created.
         */
        public string TestReplicaSwitchName
        {
            get
            {
                return (string)LateBoundObject[nameof(TestReplicaSwitchName)];
            }
            set
            {
                LateBoundObject[nameof(TestReplicaSwitchName)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
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

        private void ResetCompartmentGuid()
        {
            LateBoundObject[nameof(CompartmentGuid)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }        

        private void ResetRequiredFeatures()
        {
            LateBoundObject[nameof(RequiredFeatures)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetTestReplicaPoolID()
        {
            LateBoundObject[nameof(TestReplicaPoolID)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetTestReplicaSwitchName()
        {
            LateBoundObject[nameof(TestReplicaSwitchName)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<EthernetPortAllocationSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortAllocationSettingData(mo)).ToList();

        public new static List<EthernetPortAllocationSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortAllocationSettingData(mo)).ToList();

        public static List<EthernetPortAllocationSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortAllocationSettingData(mo)).ToList();

        public static List<EthernetPortAllocationSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortAllocationSettingData(mo)).ToList();

        public static List<EthernetPortAllocationSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortAllocationSettingData(mo)).ToList();

        public static List<EthernetPortAllocationSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortAllocationSettingData(mo)).ToList();

        public static List<EthernetPortAllocationSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortAllocationSettingData(mo)).ToList();

        public static List<EthernetPortAllocationSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortAllocationSettingData(mo)).ToList();

        public static EthernetPortAllocationSettingData CreateInstance() => new EthernetPortAllocationSettingData(CreateInstance(ClassName));

        public enum EnabledStateValues
        {
            Enabled = 2,
            Disabled = 3,
            NULL_ENUM_VALUE = 0,
        }
    }
}
