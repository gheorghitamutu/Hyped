using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.Networking
{
    public class EthernetPortSettingDataComponent : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(EthernetPortSettingDataComponent)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public EthernetPortSettingDataComponent() : base(ClassName) { }

        public EthernetPortSettingDataComponent(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetPortSettingDataComponent(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetPortSettingDataComponent(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public EthernetPortSettingDataComponent(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public EthernetPortSettingDataComponent(ManagementPath path) : base(path, ClassName) { }

        public EthernetPortSettingDataComponent(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public EthernetPortSettingDataComponent(ManagementObject theObject) : base(theObject, ClassName) { }

        public EthernetPortSettingDataComponent(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * Reference to an instance of Msvm_EthernetPortAllocationSettingData representing an Ethernet port.
         */
        public ManagementPath GroupComponent
        {
            get
            {
                if (LateBoundObject[nameof(GroupComponent)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(GroupComponent)].ToString());
                }
                return null;
            }
        }

        /*
         * Reference to an instance of Msvm_EthernetSwitchPortFeatureSettingData representing feature settings applied to the port.
         */
        public ManagementPath PartComponent
        {
            get
            {
                if (LateBoundObject[nameof(PartComponent)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(PartComponent)].ToString());
                }
                return null;
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<EthernetPortSettingDataComponent> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortSettingDataComponent(mo)).ToList();

        public new static List<EthernetPortSettingDataComponent> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortSettingDataComponent(mo)).ToList();

        public static List<EthernetPortSettingDataComponent> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortSettingDataComponent(mo)).ToList();

        public static List<EthernetPortSettingDataComponent> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortSettingDataComponent(mo)).ToList();

        public static List<EthernetPortSettingDataComponent> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortSettingDataComponent(mo)).ToList();

        public static List<EthernetPortSettingDataComponent> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortSettingDataComponent(mo)).ToList();

        public static List<EthernetPortSettingDataComponent> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortSettingDataComponent(mo)).ToList();

        public static List<EthernetPortSettingDataComponent> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetPortSettingDataComponent(mo)).ToList();

        public static EthernetPortSettingDataComponent CreateInstance() => new EthernetPortSettingDataComponent(CreateInstance(ClassName));
    }
}
