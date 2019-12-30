using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Networking
{
    public class EthernetSwitchFeatureCapabilities : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(EthernetSwitchFeatureCapabilities)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public EthernetSwitchFeatureCapabilities() : base(ClassName) { }

        public EthernetSwitchFeatureCapabilities(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchFeatureCapabilities(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchFeatureCapabilities(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public EthernetSwitchFeatureCapabilities(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public EthernetSwitchFeatureCapabilities(ManagementPath path) : base(path, ClassName) { }

        public EthernetSwitchFeatureCapabilities(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public EthernetSwitchFeatureCapabilities(ManagementObject theObject) : base(theObject, ClassName) { }

        public EthernetSwitchFeatureCapabilities(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * Describes whether this feature is applied to an Ethernet switch or a particular Ethernet switch port.
         */
        public ApplicabilityValues Applicability
        {
            get
            {
                if (LateBoundObject[nameof(Applicability)] == null)
                {
                    return (ApplicabilityValues)System.Convert.ToInt32(3);
                }
                return (ApplicabilityValues)System.Convert.ToInt32(LateBoundObject[nameof(Applicability)]);
            }
        }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        /*
         * The identifier of the feature this object provides capabilities information for.
         */
        public string FeatureId => (string)LateBoundObject[nameof(FeatureId)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        /*
         * The version of the feature in a format of "major.minor", for example "2.0".
         */
        public string Version => (string)LateBoundObject[nameof(Version)];

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<EthernetSwitchFeatureCapabilities> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchFeatureCapabilities(mo)).ToList();

        public new static List<EthernetSwitchFeatureCapabilities> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchFeatureCapabilities(mo)).ToList();

        public static List<EthernetSwitchFeatureCapabilities> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchFeatureCapabilities(mo)).ToList();

        public static List<EthernetSwitchFeatureCapabilities> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchFeatureCapabilities(mo)).ToList();

        public static List<EthernetSwitchFeatureCapabilities> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchFeatureCapabilities(mo)).ToList();

        public static List<EthernetSwitchFeatureCapabilities> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchFeatureCapabilities(mo)).ToList();

        public static List<EthernetSwitchFeatureCapabilities> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchFeatureCapabilities(mo)).ToList();

        public static List<EthernetSwitchFeatureCapabilities> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchFeatureCapabilities(mo)).ToList();

        public static EthernetSwitchFeatureCapabilities CreateInstance() => new EthernetSwitchFeatureCapabilities(CreateInstance(ClassName));

        public enum ApplicabilityValues
        {
            Unknown0 = 0,
            Port = 1,
            Switch = 2,
            NULL_ENUM_VALUE = 3,
        }
    }
}
