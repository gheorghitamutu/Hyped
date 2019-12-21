using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.Metrics
{
    public class AggregationMetricDefinition : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(AggregationMetricDefinition)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public AggregationMetricDefinition() : base(ClassName) { }

        public AggregationMetricDefinition(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public AggregationMetricDefinition(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public AggregationMetricDefinition(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public AggregationMetricDefinition(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public AggregationMetricDefinition(ManagementPath path) : base(path, ClassName) { }

        public AggregationMetricDefinition(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public AggregationMetricDefinition(ManagementObject theObject) : base(theObject, ClassName) { }

        public AggregationMetricDefinition(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string[] BreakdownDimensions => (string[])LateBoundObject[nameof(BreakdownDimensions)];

        public ushort Calculable
        {
            get
            {
                if (LateBoundObject[nameof(Calculable)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(Calculable)];
            }
        }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public ushort ChangeType
        {
            get
            {
                if (LateBoundObject[nameof(ChangeType)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ChangeType)];
            }
        }

        public ushort DataType
        {
            get
            {
                if (LateBoundObject[nameof(DataType)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(DataType)];
            }
        }

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public ushort GatheringType
        {
            get
            {
                if (LateBoundObject[nameof(GatheringType)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(GatheringType)];
            }
        }

        public string Id => (string)LateBoundObject[nameof(Id)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        public bool IsContinuous
        {
            get
            {
                if (LateBoundObject[nameof(IsContinuous)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsContinuous)];
            }
        }

        public string Name => (string)LateBoundObject[nameof(Name)];

        public string ProgrammaticUnits => (string)LateBoundObject[nameof(ProgrammaticUnits)];

        public ushort SimpleFunction
        {
            get
            {
                if (LateBoundObject[nameof(SimpleFunction)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(SimpleFunction)];
            }
        }

        public ushort TimeScope
        {
            get
            {
                if (LateBoundObject[nameof(TimeScope)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(TimeScope)];
            }
        }

        public string Units => (string)LateBoundObject[nameof(Units)];

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<AggregationMetricDefinition> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new AggregationMetricDefinition(mo)).ToList();

        public new static List<AggregationMetricDefinition> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new AggregationMetricDefinition(mo)).ToList();

        public static List<AggregationMetricDefinition> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new AggregationMetricDefinition(mo)).ToList();

        public static List<AggregationMetricDefinition> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new AggregationMetricDefinition(mo)).ToList();

        public static List<AggregationMetricDefinition> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new AggregationMetricDefinition(mo)).ToList();

        public static List<AggregationMetricDefinition> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new AggregationMetricDefinition(mo)).ToList();

        public static List<AggregationMetricDefinition> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new AggregationMetricDefinition(mo)).ToList();

        public static List<AggregationMetricDefinition> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new AggregationMetricDefinition(mo)).ToList();

        public static AggregationMetricDefinition CreateInstance() => new AggregationMetricDefinition(CreateInstance(ClassName));
    }
}
