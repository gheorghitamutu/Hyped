using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.Metrics
{
    public class MetricInstance : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(MetricInstance)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public MetricInstance() : base(ClassName) { }

        public MetricInstance(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MetricInstance(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MetricInstance(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public MetricInstance(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public MetricInstance(ManagementPath path) : base(path, ClassName) { }

        public MetricInstance(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public MetricInstance(ManagementObject theObject) : base(theObject, ClassName) { }

        public MetricInstance(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public ManagementPath Antecedent
        {
            get
            {
                if (LateBoundObject[nameof(Antecedent)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(Antecedent)].ToString());
                }
                return null;
            }
        }

        public ManagementPath Dependent
        {
            get
            {
                if (LateBoundObject[nameof(Dependent)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(Dependent)].ToString());
                }
                return null;
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<MetricInstance> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricInstance(mo)).ToList();

        public new static List<MetricInstance> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricInstance(mo)).ToList();

        public static List<MetricInstance> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricInstance(mo)).ToList();

        public static List<MetricInstance> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricInstance(mo)).ToList();

        public static List<MetricInstance> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new MetricInstance(mo)).ToList();

        public static List<MetricInstance> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricInstance(mo)).ToList();

        public static List<MetricInstance> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricInstance(mo)).ToList();

        public static List<MetricInstance> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricInstance(mo)).ToList();

        public static MetricInstance CreateInstance() => new MetricInstance(CreateInstance(ClassName));
    }
}
