using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.Processor
{
    public class ProcessorPool : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(ProcessorPool)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public ProcessorPool() : base(ClassName) { }

        public ProcessorPool(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ProcessorPool(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ProcessorPool(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public ProcessorPool(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public ProcessorPool(ManagementPath path) : base(path, ClassName) { }

        public ProcessorPool(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public ProcessorPool(ManagementObject theObject) : base(theObject, ClassName) { }

        public ProcessorPool(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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

        public ushort[] OperationalStatus => (ushort[])LateBoundObject[nameof(OperationalStatus)];

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
        public static List<ProcessorPool> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorPool(mo)).ToList();

        public new static List<ProcessorPool> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorPool(mo)).ToList();

        public static List<ProcessorPool> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorPool(mo)).ToList();

        public static List<ProcessorPool> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorPool(mo)).ToList();

        public static List<ProcessorPool> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorPool(mo)).ToList();

        public static List<ProcessorPool> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorPool(mo)).ToList();

        public static List<ProcessorPool> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorPool(mo)).ToList();

        public static List<ProcessorPool> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorPool(mo)).ToList();

        public static ProcessorPool CreateInstance() => new ProcessorPool(CreateInstance(ClassName));

        public uint CalculatePossibleReserve(ushort ProcessorCount)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("CalculatePossibleReserve");
                inParams["ProcessorCount"] = ProcessorCount;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("CalculatePossibleReserve", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }
    }
}
