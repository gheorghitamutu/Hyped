using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Metrics
{
    public class MetricForME : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(MetricForME)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public MetricForME() : base(ClassName) { }

        public MetricForME(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MetricForME(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MetricForME(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public MetricForME(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public MetricForME(ManagementPath path) : base(path, ClassName) { }

        public MetricForME(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public MetricForME(ManagementObject theObject) : base(theObject, ClassName) { }

        public MetricForME(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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
        public static List<MetricForME> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricForME(mo)).ToList();

        public new static List<MetricForME> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricForME(mo)).ToList();

        public static List<MetricForME> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricForME(mo)).ToList();

        public static List<MetricForME> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricForME(mo)).ToList();

        public static List<MetricForME> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new MetricForME(mo)).ToList();

        public static List<MetricForME> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricForME(mo)).ToList();

        public static List<MetricForME> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricForME(mo)).ToList();

        public static List<MetricForME> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricForME(mo)).ToList();

        public static MetricForME CreateInstance() => new MetricForME(CreateInstance(ClassName));
    }
}
