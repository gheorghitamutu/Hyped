using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Storage
{
    public sealed class StorageAllocationSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(StorageAllocationSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public StorageAllocationSettingData() : base(ClassName) { }

        public StorageAllocationSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public StorageAllocationSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public StorageAllocationSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public StorageAllocationSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public StorageAllocationSettingData(ManagementPath path) : base(path, ClassName) { }

        public StorageAllocationSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public StorageAllocationSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public StorageAllocationSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public ushort Access
        {
            get
            {
                if (LateBoundObject[nameof(Access)] == null)
                {
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(Access)];
            }
        }

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

        /*
         * Indicates whether and how in-memory file caching should be used for this VHD
         * The default policy is set in the DefaultVirtualHard__DiskCachingMode field of the Msvm_VirtualSystemManagementServiceSettingData class.
         * This property is reserved for system use.
         */
        public CachingModeValues CachingMode
        {
            get
            {
                if (LateBoundObject[nameof(CachingMode)] == null)
                {
                    return (CachingModeValues)System.Convert.ToInt32(5);
                }
                return (CachingModeValues)System.Convert.ToInt32(LateBoundObject[nameof(CachingMode)]);
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

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public string HostExtentName => (string)LateBoundObject[nameof(HostExtentName)];

        public ushort HostExtentNameFormat
        {
            get
            {
                if (LateBoundObject[nameof(HostExtentNameFormat)] == null)
                {
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(HostExtentNameFormat)];
            }
        }

        public ushort HostExtentNameNamespace
        {
            get
            {
                if (LateBoundObject[nameof(HostExtentNameNamespace)] == null)
                {
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(HostExtentNameNamespace)];
            }
        }

        public ulong HostExtentStartingAddress
        {
            get
            {
                if (LateBoundObject[nameof(HostExtentStartingAddress)] == null)
                {
                    return System.Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(HostExtentStartingAddress)];
            }
        }

        public string[] HostResource => (string[])LateBoundObject[nameof(HostResource)];

        public ulong HostResourceBlockSize
        {
            get
            {
                if (LateBoundObject[nameof(HostResourceBlockSize)] == null)
                {
                    return System.Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(HostResourceBlockSize)];
            }
        }

        /*
         * Specifies whether requests from the VM to flush or write through the cache are respected.
         * If true, then the host ignores all flush or write through requests.
         */
        public bool IgnoreFlushes
        {
            get
            {
                if (LateBoundObject[nameof(IgnoreFlushes)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IgnoreFlushes)];
            }
        }

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        /*
         * Specifies the allocation units used by the IOPSLimit and IOPSReservation properties.
         */
        public string IOPSAllocationUnits => (string)LateBoundObject[nameof(IOPSAllocationUnits)];

        /*
         * The maximum number of I/O operations per second which will be serviced for this virtual storage extent.
         * If the value is not defined or 0 there is no limit to the number of IOPS that the device can issue.
         * This property is expressed in normalized I/Os per second.
         * Each I/O request is accounted for as 1 normalized I/O if the size of the request is less than or equal to a predefined base size (8KB).
         * Requests that are larger than the base size are accounted for as N I/Os, where N is the rounded-up value of the request size divided by the base size
         * (for example, if the base size is 8KB, a 32KB requests is counted as 4 normalized I/Os, a 60KB request as 8 normalized I/Os, etc...).
         */
        public ulong IOPSLimit
        {
            get
            {
                if (LateBoundObject[nameof(IOPSLimit)] == null)
                {
                    return System.Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(IOPSLimit)];
            }
        }

        /*
         * The minimum number of I/O operations per second which will be serviced for this virtual storage extent.
         * If both IOPSLimit and IOPSReservation are defined, the value of IOPSLimit must be greater or equal to IOPSReservation.
         * This property is expressed in normalized I/Os per second.
         * Each I/O request is accounted for as 1 normalized I/O if the size of the request is less than or equal to a predefined base size (8KB).
         * Requests that are larger than the base size are accounted for as N I/Os, where N is the rounded-up value of the request size divided by the base size
         * (for example, if the base size is 8KB, a 32KB requests is counted as 4 normalized I/Os, a 64KB request as 8 normalized I/Os, etc...).
         */
        public ulong IOPSReservation
        {
            get
            {
                if (LateBoundObject[nameof(IOPSReservation)] == null)
                {
                    return System.Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(IOPSReservation)];
            }
        }

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

        public string OtherHostExtentNameFormat => (string)LateBoundObject[nameof(OtherHostExtentNameFormat)];

        public string OtherHostExtentNameNamespace => (string)LateBoundObject[nameof(OtherHostExtentNameNamespace)];

        public string OtherResourceType => (string)LateBoundObject[nameof(OtherResourceType)];

        public string Parent => (string)LateBoundObject[nameof(Parent)];

        /*
         * Indicates whether the virtual hard __Disk supports SCSI-3 persistent reservations.
         */
        public bool PersistentReservationsSupported
        {
            get
            {
                if (LateBoundObject[nameof(PersistentReservationsSupported)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(PersistentReservationsSupported)];
            }
        }

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
         * A GUID representing which snapshot within the VHD Set file is to be attached.
         */
        public string SnapshotId => (string)LateBoundObject[nameof(SnapshotId)];

        /*
         * Specifies the unique identifier of the Storage QoS Policy to be applied to this virtual storage extent.
         */
        public string StorageQoSPolicyID => (string)LateBoundObject[nameof(StorageQoSPolicyID)];

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

        public ulong VirtualResourceBlockSize
        {
            get
            {
                if (LateBoundObject[nameof(VirtualResourceBlockSize)] == null)
                {
                    return System.Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(VirtualResourceBlockSize)];
            }
        }

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

        /*
         * Indicates what write hardening method is supported by the __Disk.
         */
        public WriteHardeningMethodValues WriteHardeningMethod
        {
            get
            {
                if (LateBoundObject[nameof(WriteHardeningMethod)] == null)
                {
                    return (WriteHardeningMethodValues)System.Convert.ToInt32(4);
                }
                return (WriteHardeningMethodValues)System.Convert.ToInt32(LateBoundObject[nameof(WriteHardeningMethod)]);
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<StorageAllocationSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new StorageAllocationSettingData(mo)).ToList();

        public new static List<StorageAllocationSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new StorageAllocationSettingData(mo)).ToList();

        public static List<StorageAllocationSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new StorageAllocationSettingData(mo)).ToList();

        public static List<StorageAllocationSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new StorageAllocationSettingData(mo)).ToList();

        public static List<StorageAllocationSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new StorageAllocationSettingData(mo)).ToList();

        public static List<StorageAllocationSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new StorageAllocationSettingData(mo)).ToList();

        public static List<StorageAllocationSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new StorageAllocationSettingData(mo)).ToList();

        public static List<StorageAllocationSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new StorageAllocationSettingData(mo)).ToList();

        public static StorageAllocationSettingData CreateInstance() => new StorageAllocationSettingData(CreateInstance(ClassName));

        public enum CachingModeValues
        {
            Unknown0 = 0,
            Default = 2,
            No_Caching = 3,
            Cache_Sharable_Parents = 4,
            NULL_ENUM_VALUE = 5,
        }

        public enum WriteHardeningMethodValues
        {
            Default = 0,
            WriteCacheEnabled = 1,
            WriteCacheandFUAEnabled = 2,
            WriteCacheDisabled = 3,
            NULL_ENUM_VALUE = 4,
        }
    }
}
