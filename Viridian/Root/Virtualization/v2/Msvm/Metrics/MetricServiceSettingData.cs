using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Metrics
{
    public class MetricServiceSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(MetricServiceSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public MetricServiceSettingData() : base(ClassName) { }

        public MetricServiceSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MetricServiceSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MetricServiceSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public MetricServiceSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public MetricServiceSettingData(ManagementPath path) : base(path, ClassName) { }

        public MetricServiceSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public MetricServiceSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public MetricServiceSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];
        
        /*
         * Defines the interval at which metrics should be flushed to __Disk.
         */
        public DateTime MetricsFlushInterval
        {
            get
            {
                if (LateBoundObject[nameof(MetricsFlushInterval)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(MetricsFlushInterval)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<MetricServiceSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceSettingData(mo)).ToList();

        public new static List<MetricServiceSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceSettingData(mo)).ToList();

        public static List<MetricServiceSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceSettingData(mo)).ToList();

        public static List<MetricServiceSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceSettingData(mo)).ToList();

        public static List<MetricServiceSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceSettingData(mo)).ToList();

        public static List<MetricServiceSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceSettingData(mo)).ToList();

        public static List<MetricServiceSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceSettingData(mo)).ToList();

        public static List<MetricServiceSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceSettingData(mo)).ToList();

        public static MetricServiceSettingData CreateInstance() => new MetricServiceSettingData(CreateInstance(ClassName));
    }
}
