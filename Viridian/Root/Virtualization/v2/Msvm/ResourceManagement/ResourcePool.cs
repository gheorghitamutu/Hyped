using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.ResourceManagement
{
    public sealed class ResourcePool : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(ResourcePool)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public ResourcePool() : base(ClassName) { }

        public ResourcePool(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ResourcePool(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ResourcePool(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public ResourcePool(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public ResourcePool(ManagementPath path) : base(path, ClassName) { }

        public ResourcePool(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public ResourcePool(ManagementObject theObject) : base(theObject, ClassName) { }

        public ResourcePool(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string AllocationUnits => (string)LateBoundObject[nameof(AllocationUnits)];

        public ulong Capacity
        {
            get
            {
                if (LateBoundObject[nameof(Capacity)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(Capacity)];
            }
        }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public ushort CommunicationStatus
        {
            get
            {
                if (LateBoundObject[nameof(CommunicationStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(CommunicationStatus)];
            }
        }

        public string ConsumedResourceUnits => (string)LateBoundObject[nameof(ConsumedResourceUnits)];

        public ulong CurrentlyConsumedResource
        {
            get
            {
                if (LateBoundObject[nameof(CurrentlyConsumedResource)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(CurrentlyConsumedResource)];
            }
        }

        public string Description => (string)LateBoundObject[nameof(Description)];

        public ushort DetailedStatus
        {
            get
            {
                if (LateBoundObject[nameof(DetailedStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(DetailedStatus)];
            }
        }

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public ushort HealthState
        {
            get
            {
                if (LateBoundObject[nameof(HealthState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(HealthState)];
            }
        }

        public DateTime InstallDate
        {
            get
            {
                if (LateBoundObject[nameof(InstallDate)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(InstallDate)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        public ulong MaxConsumableResource
        {
            get
            {
                if (LateBoundObject[nameof(MaxConsumableResource)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(MaxConsumableResource)];
            }
        }

        public string Name => (string)LateBoundObject[nameof(Name)];

        public ushort OperatingStatus
        {
            get
            {
                if (LateBoundObject[nameof(OperatingStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(OperatingStatus)];
            }
        }

        public OperationalStatusValues[] OperationalStatus
        {
            get
            {
                Array arrEnumVals = (Array)LateBoundObject[nameof(OperationalStatus)];
                OperationalStatusValues[] enumToRet = new OperationalStatusValues[arrEnumVals.Length];
                int counter;
                for (counter = 0; counter < arrEnumVals.Length; counter += 1)
                {
                    enumToRet[counter] = (OperationalStatusValues)Convert.ToInt32(arrEnumVals.GetValue(counter));
                }
                return enumToRet;
            }
        }

        public string OtherResourceType => (string)LateBoundObject[nameof(OtherResourceType)];

        public string PoolID => (string)LateBoundObject[nameof(PoolID)];

        public ushort PrimaryStatus
        {
            get
            {
                if (LateBoundObject[nameof(PrimaryStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(PrimaryStatus)];
            }
        }

        public bool Primordial
        {
            get
            {
                if (LateBoundObject[nameof(Primordial)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(Primordial)];
            }
        }

        public ulong Reserved
        {
            get
            {
                if (LateBoundObject[nameof(Reserved)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(Reserved)];
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

        public string Status => (string)LateBoundObject[nameof(Status)];

        public string[] StatusDescriptions => (string[])LateBoundObject[nameof(StatusDescriptions)];

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<ResourcePool> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new ResourcePool(mo)).ToList();

        public new static List<ResourcePool> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ResourcePool(mo)).ToList();

        public static List<ResourcePool> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ResourcePool(mo)).ToList();

        public static List<ResourcePool> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ResourcePool(mo)).ToList();

        public static List<ResourcePool> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new ResourcePool(mo)).ToList();

        public static List<ResourcePool> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ResourcePool(mo)).ToList();

        public static List<ResourcePool> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ResourcePool(mo)).ToList();

        public static List<ResourcePool> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ResourcePool(mo)).ToList();

        public static ResourcePool CreateInstance() => new ResourcePool(CreateInstance(ClassName));
        
        public enum OperationalStatusValues
        {
            OK = 2,
            Degraded = 3,
            Non_Recoverable_Error = 7,
            No_Contact = 12,
            Lost_Communication = 13,
            Protocol_Mismatch = 32775,
            Insufficient_Throughput = 32788,
            NULL_ENUM_VALUE = 0,
        }
    }
}
