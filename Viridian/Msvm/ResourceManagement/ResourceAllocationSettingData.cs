using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.ResourceManagement
{
    public class ResourceAllocationSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(ResourceAllocationSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public ResourceAllocationSettingData() : base(ClassName) { }

        public ResourceAllocationSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ResourceAllocationSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ResourceAllocationSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public ResourceAllocationSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public ResourceAllocationSettingData(ManagementPath path) : base(path, ClassName) { }

        public ResourceAllocationSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public ResourceAllocationSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public ResourceAllocationSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public string[] HostResource => (string[])LateBoundObject[nameof(HostResource)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

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
         * These values are only used if the ResourceType property is set to 6 (Parallel SCSI HBA) and the ResourceSubType property is set to "Microsoft Synthetic SCSI Controller".
         * This property is set to "GUID".
         * This is a read-only property, but it can be changed using the ModifyVirtualSystemResources method of the Msvm_VirtualSystemManagementService class.
         * Windows Server 2008:  The VirtualSystemIdentifiers property is not supported until Windows Server 2008 R2.
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
        public static List<ResourceAllocationSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new ResourceAllocationSettingData(mo)).ToList();

        public new static List<ResourceAllocationSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ResourceAllocationSettingData(mo)).ToList();

        public static List<ResourceAllocationSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ResourceAllocationSettingData(mo)).ToList();

        public static List<ResourceAllocationSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ResourceAllocationSettingData(mo)).ToList();

        public static List<ResourceAllocationSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new ResourceAllocationSettingData(mo)).ToList();

        public static List<ResourceAllocationSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ResourceAllocationSettingData(mo)).ToList();

        public static List<ResourceAllocationSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ResourceAllocationSettingData(mo)).ToList();

        public static List<ResourceAllocationSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ResourceAllocationSettingData(mo)).ToList();

        public static ResourceAllocationSettingData CreateInstance() => new ResourceAllocationSettingData(CreateInstance(ClassName));
    }
}
