using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Memory
{
    public class NumaNode : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(NumaNode)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public NumaNode() : base(ClassName) { }

        public NumaNode(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public NumaNode(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public NumaNode(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public NumaNode(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public NumaNode(ManagementPath path) : base(path, ClassName) { }

        public NumaNode(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public NumaNode(ManagementObject theObject) : base(theObject, ClassName) { }

        public NumaNode(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public ushort[] AvailableRequestedStates => (ushort[])LateBoundObject[nameof(AvailableRequestedStates)];

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

        /*
         * CreationClassName indicates the name of the class or the subclass used in the creation of an instance.
         * When used with the other key properties of this class, this property allows all instances of this class and its subclasses to be uniquely identified.
         */
        public string CreationClassName => (string)LateBoundObject[nameof(CreationClassName)];

        /*
         * The number of virtual processors currently assigned to this NUMA node.
         */
        public uint CurrentlyAssignedVirtualProcessors
        {
            get
            {
                if (LateBoundObject[nameof(CurrentlyAssignedVirtualProcessors)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(CurrentlyAssignedVirtualProcessors)];
            }
        }

        /*
         * "The number of memory blocks currently available for consumption by virtual machines.
         */
        public ulong CurrentlyConsumableMemoryBlocks
        {
            get
            {
                if (LateBoundObject[nameof(CurrentlyConsumableMemoryBlocks)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(CurrentlyConsumableMemoryBlocks)];
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

        public ushort EnabledDefault
        {
            get
            {
                if (LateBoundObject[nameof(EnabledDefault)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(EnabledDefault)];
            }
        }

        public ushort EnabledState
        {
            get
            {
                if (LateBoundObject[nameof(EnabledState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(EnabledState)];
            }
        }

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

        public string Name => (string)LateBoundObject[nameof(Name)];

        /*
         * The NUMA node identifier.
         */
        public string NodeID => (string)LateBoundObject[nameof(NodeID)];

        /*
         * The total number of logical processor cores in this node.
         */
        public uint NumberOfLogicalProcessors
        {
            get
            {
                if (LateBoundObject[nameof(NumberOfLogicalProcessors)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(NumberOfLogicalProcessors)];
            }
        }

        /*
         * The total number of processor cores in this NUMA node.
         * This may differ from the number of Msvm_Processor objects associated to this node if each processor core supports multiple compute threads.
         */
        public uint NumberOfProcessorCores
        {
            get
            {
                if (LateBoundObject[nameof(NumberOfProcessorCores)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(NumberOfProcessorCores)];
            }
        }

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

        public string OtherEnabledState => (string)LateBoundObject[nameof(OtherEnabledState)];

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

        public ushort RequestedState
        {
            get
            {
                if (LateBoundObject[nameof(RequestedState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(RequestedState)];
            }
        }

        public string Status => (string)LateBoundObject[nameof(Status)];

        public string[] StatusDescriptions => (string[])LateBoundObject[nameof(StatusDescriptions)];

        /*
         * The scoping System's CreationClassName.
         */
        public string SystemCreationClassName => (string)LateBoundObject[nameof(SystemCreationClassName)];

        /*
         * The scoping System's Name.
         */
        public string SystemName => (string)LateBoundObject[nameof(SystemName)];

        public DateTime TimeOfLastStateChange
        {
            get
            {
                if (LateBoundObject[nameof(TimeOfLastStateChange)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(TimeOfLastStateChange)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public ushort TransitioningToState
        {
            get
            {
                if (LateBoundObject[nameof(TransitioningToState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(TransitioningToState)];
            }
        }


        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<NumaNode> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new NumaNode(mo)).ToList();

        public new static List<NumaNode> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new NumaNode(mo)).ToList();

        public static List<NumaNode> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new NumaNode(mo)).ToList();

        public static List<NumaNode> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new NumaNode(mo)).ToList();

        public static List<NumaNode> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new NumaNode(mo)).ToList();

        public static List<NumaNode> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new NumaNode(mo)).ToList();

        public static List<NumaNode> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new NumaNode(mo)).ToList();

        public static List<NumaNode> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new NumaNode(mo)).ToList();

        public static NumaNode CreateInstance() => new NumaNode(CreateInstance(ClassName));

        public uint RequestStateChange(ushort RequestedState, DateTime TimeoutPeriod, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RequestStateChange");
                inParams[nameof(RequestedState)] = RequestedState;
                inParams["TimeoutPeriod"] = ToDmtfDateTime(TimeoutPeriod);
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RequestStateChange", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].ToString());
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }
    }
}
