using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.Metrics
{
    public class MetricServiceCapabilities : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(MetricServiceCapabilities)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public MetricServiceCapabilities() : base(ClassName) { }

        public MetricServiceCapabilities(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MetricServiceCapabilities(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MetricServiceCapabilities(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public MetricServiceCapabilities(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public MetricServiceCapabilities(ManagementPath path) : base(path, ClassName) { }

        public MetricServiceCapabilities(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public MetricServiceCapabilities(ManagementObject theObject) : base(theObject, ClassName) { }

        public MetricServiceCapabilities(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public string[] ControllableManagedElements => (string[])LateBoundObject[nameof(ControllableManagedElements)];

        public string[] ControllableMetrics => (string[])LateBoundObject[nameof(ControllableMetrics)];

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public bool ElementNameEditSupported
        {
            get
            {
                if (LateBoundObject[nameof(ElementNameEditSupported)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(ElementNameEditSupported)];
            }
        }

        public string ElementNameMask => (string)LateBoundObject[nameof(ElementNameMask)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        public ushort[] ManagedElementControlTypes => (ushort[])LateBoundObject[nameof(ManagedElementControlTypes)];

        public ushort MaxElementNameLen
        {
            get
            {
                if (LateBoundObject[nameof(MaxElementNameLen)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(MaxElementNameLen)];
            }
        }

        public ushort[] MetricsControlTypes => (ushort[])LateBoundObject[nameof(MetricsControlTypes)];

        public ushort[] RequestedStatesSupported => (ushort[])LateBoundObject[nameof(RequestedStatesSupported)];

        public ushort[] SupportedMethods => (ushort[])LateBoundObject[nameof(SupportedMethods)];

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<MetricServiceCapabilities> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceCapabilities(mo)).ToList();

        public new static List<MetricServiceCapabilities> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceCapabilities(mo)).ToList();

        public static List<MetricServiceCapabilities> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceCapabilities(mo)).ToList();

        public static List<MetricServiceCapabilities> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceCapabilities(mo)).ToList();

        public static List<MetricServiceCapabilities> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceCapabilities(mo)).ToList();

        public static List<MetricServiceCapabilities> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceCapabilities(mo)).ToList();

        public static List<MetricServiceCapabilities> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceCapabilities(mo)).ToList();

        public static List<MetricServiceCapabilities> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricServiceCapabilities(mo)).ToList();

        public static MetricServiceCapabilities CreateInstance() => new MetricServiceCapabilities(CreateInstance(ClassName));
    }
}
