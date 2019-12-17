using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.Metrics
{
    public class BaseMetricDefinition : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(BaseMetricDefinition)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public BaseMetricDefinition() : base(ClassName) { }

        public BaseMetricDefinition(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public BaseMetricDefinition(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public BaseMetricDefinition(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public BaseMetricDefinition(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public BaseMetricDefinition(ManagementPath path) : base(path, ClassName) { }

        public BaseMetricDefinition(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public BaseMetricDefinition(ManagementObject theObject) : base(theObject, ClassName) { }

        public BaseMetricDefinition(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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
        public static List<BaseMetricDefinition> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricDefinition(mo)).ToList();

        public new static List<BaseMetricDefinition> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricDefinition(mo)).ToList();

        public static List<BaseMetricDefinition> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricDefinition(mo)).ToList();

        public static List<BaseMetricDefinition> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricDefinition(mo)).ToList();

        public static List<BaseMetricDefinition> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricDefinition(mo)).ToList();

        public static List<BaseMetricDefinition> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricDefinition(mo)).ToList();

        public static List<BaseMetricDefinition> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricDefinition(mo)).ToList();

        public static List<BaseMetricDefinition> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricDefinition(mo)).ToList();

        public static BaseMetricDefinition CreateInstance() => new BaseMetricDefinition(CreateInstance(ClassName));
    }
}
