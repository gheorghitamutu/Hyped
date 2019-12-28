using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Memory
{
    public class MemorySettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(MemorySettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public MemorySettingData() : base(ClassName) { }

        public MemorySettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MemorySettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MemorySettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public MemorySettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public MemorySettingData(ManagementPath path) : base(path, ClassName) { }

        public MemorySettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public MemorySettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public MemorySettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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

        public string Description => (string)LateBoundObject[nameof(Description)];

        /*
         * Indicates if dynamic memory is enabled.
         */
        public bool DynamicMemoryEnabled
        {
            get
            {
                if (LateBoundObject[nameof(DynamicMemoryEnabled)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(DynamicMemoryEnabled)];
            }
        }

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public string[] HostResource => (string[])LateBoundObject[nameof(HostResource)];

        /*
         * Indicates if huge pages are enabled.
         */
        public bool HugePagesEnabled
        {
            get
            {
                if (LateBoundObject[nameof(HugePagesEnabled)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(HugePagesEnabled)];
            }
        }

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        /*
         * Indicates whether this device is virtualized or passed through, possibly using partitioning.
         * When set to False, the underlying or host resource is utilized.
         * At least one item shall be present in the DeviceID property.
         * When set to True, the resource is virtualized and may not map directly to an underlying/host resource.
         * Some implementations may support specific assignment for virtualized resources, in which case the host resource(s) are exposed using the DeviceID property.
         * This property is always set to True.
         */
        public bool IsVirtualized
        {
            get
            {
                if (LateBoundObject[nameof(IsVirtualized)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsVirtualized)];
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

        /*
         * The maximum amount of memory that can be observed within the VM as belonging to a single NUMA node.
         */
        public ulong MaxMemoryBlocksPerNumaNode
        {
            get
            {
                if (LateBoundObject[nameof(MaxMemoryBlocksPerNumaNode)] == null)
                {
                    return System.Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(MaxMemoryBlocksPerNumaNode)];
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

        /*
         * Indicates if SGX is enabled.
         */
        public bool SgxEnabled
        {
            get
            {
                if (LateBoundObject[nameof(SgxEnabled)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(SgxEnabled)];
            }
        }

        /*
         * The default SGX launch control MSR values for the VM.
         */
        public string SgxLaunchControlDefault => (string)LateBoundObject[nameof(SgxLaunchControlDefault)];

        /*
         * The SGX launch control mode for the VM.
         */
        public SgxLaunchControlModeValues SgxLaunchControlMode
        {
            get
            {
                if (LateBoundObject[nameof(SgxLaunchControlMode)] == null)
                {
                    return (SgxLaunchControlModeValues)System.Convert.ToInt32(3);
                }
                return (SgxLaunchControlModeValues)System.Convert.ToInt32(LateBoundObject[nameof(SgxLaunchControlMode)]);
            }
        }

        /*
         * The amount of SGX memory to allocate for the VM, in MB.
         */
        public ulong SgxSize
        {
            get
            {
                if (LateBoundObject[nameof(SgxSize)] == null)
                {
                    return System.Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(SgxSize)];
            }
        }

        /*
         * Indicates if Smart Paging is active.
         */
        public bool SwapFilesInUse
        {
            get
            {
                if (LateBoundObject[nameof(SwapFilesInUse)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(SwapFilesInUse)];
            }
        }

        public uint TargetMemoryBuffer
        {
            get
            {
                if (LateBoundObject[nameof(TargetMemoryBuffer)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(TargetMemoryBuffer)];
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
        public static List<MemorySettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new MemorySettingData(mo)).ToList();

        public new static List<MemorySettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MemorySettingData(mo)).ToList();

        public static List<MemorySettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MemorySettingData(mo)).ToList();

        public static List<MemorySettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MemorySettingData(mo)).ToList();

        public static List<MemorySettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new MemorySettingData(mo)).ToList();

        public static List<MemorySettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MemorySettingData(mo)).ToList();

        public static List<MemorySettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MemorySettingData(mo)).ToList();

        public static List<MemorySettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MemorySettingData(mo)).ToList();

        public static MemorySettingData CreateInstance() => new MemorySettingData(CreateInstance(ClassName));

        public enum SgxLaunchControlModeValues
        {
            No_access = 0,
            Read_only = 1,
            Read_write = 2,
            NULL_ENUM_VALUE = 3,
        }
    }
}
