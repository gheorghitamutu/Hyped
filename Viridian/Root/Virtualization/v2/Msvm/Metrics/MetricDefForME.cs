using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Metrics
{
    public class MetricDefForME : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(MetricDefForME)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public MetricDefForME() : base(ClassName) { }

        public MetricDefForME(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MetricDefForME(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MetricDefForME(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public MetricDefForME(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public MetricDefForME(ManagementPath path) : base(path, ClassName) { }

        public MetricDefForME(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public MetricDefForME(ManagementObject theObject) : base(theObject, ClassName) { }

        public MetricDefForME(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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

        public ushort MetricCollectionEnabled
        {
            get
            {
                if (LateBoundObject[nameof(MetricCollectionEnabled)] == null)
                {
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(MetricCollectionEnabled)];
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<MetricDefForME> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricDefForME(mo)).ToList();

        public new static List<MetricDefForME> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricDefForME(mo)).ToList();

        public static List<MetricDefForME> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricDefForME(mo)).ToList();

        public static List<MetricDefForME> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricDefForME(mo)).ToList();

        public static List<MetricDefForME> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new MetricDefForME(mo)).ToList();

        public static List<MetricDefForME> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricDefForME(mo)).ToList();

        public static List<MetricDefForME> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricDefForME(mo)).ToList();

        public static List<MetricDefForME> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricDefForME(mo)).ToList();

        public static MetricDefForME CreateInstance() => new MetricDefForME(CreateInstance(ClassName));
    }
}
