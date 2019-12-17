using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.Metrics
{
    public class BaseMetricValue : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(BaseMetricValue)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public BaseMetricValue() : base(ClassName) { }

        public BaseMetricValue(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public BaseMetricValue(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public BaseMetricValue(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public BaseMetricValue(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public BaseMetricValue(ManagementPath path) : base(path, ClassName) { }

        public BaseMetricValue(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public BaseMetricValue(ManagementObject theObject) : base(theObject, ClassName) { }

        public BaseMetricValue(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string BreakdownDimension => (string)LateBoundObject[nameof(BreakdownDimension)];

        public string BreakdownValue => (string)LateBoundObject[nameof(BreakdownValue)];

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public string Description => (string)LateBoundObject[nameof(Description)];

        public DateTime Duration
        {
            get
            {
                if (LateBoundObject[nameof(Duration)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(Duration)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        public string MeasuredElementName => (string)LateBoundObject[nameof(MeasuredElementName)];

        public string MetricDefinitionId => (string)LateBoundObject[nameof(MetricDefinitionId)];

        public string MetricValue => (string)LateBoundObject[nameof(MetricValue)];

        public DateTime TimeStamp
        {
            get
            {
                if (LateBoundObject[nameof(TimeStamp)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(TimeStamp)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public bool @volatile
        {
            get
            {
#pragma warning disable CA1507 // Use nameof to express symbol names
                if (LateBoundObject["volatile"] == null)
#pragma warning restore CA1507 // Use nameof to express symbol names
                {
                    return Convert.ToBoolean(0);
                }
#pragma warning disable CA1507 // Use nameof to express symbol names
                return (bool)LateBoundObject["volatile"];
#pragma warning restore CA1507 // Use nameof to express symbol names
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<BaseMetricValue> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricValue(mo)).ToList();

        public new static List<BaseMetricValue> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricValue(mo)).ToList();

        public static List<BaseMetricValue> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricValue(mo)).ToList();

        public static List<BaseMetricValue> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricValue(mo)).ToList();

        public static List<BaseMetricValue> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricValue(mo)).ToList();

        public static List<BaseMetricValue> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricValue(mo)).ToList();

        public static List<BaseMetricValue> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricValue(mo)).ToList();

        public static List<BaseMetricValue> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BaseMetricValue(mo)).ToList();

        public static BaseMetricValue CreateInstance() => new BaseMetricValue(CreateInstance(ClassName));
    }
}
