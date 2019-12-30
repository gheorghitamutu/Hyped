using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Threshold
{
    public class GuestServiceInterfaceSettingDataComponent : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(GuestServiceInterfaceSettingDataComponent)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public GuestServiceInterfaceSettingDataComponent() : base(ClassName) { }

        public GuestServiceInterfaceSettingDataComponent(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public GuestServiceInterfaceSettingDataComponent(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public GuestServiceInterfaceSettingDataComponent(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public GuestServiceInterfaceSettingDataComponent(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public GuestServiceInterfaceSettingDataComponent(ManagementPath path) : base(path, ClassName) { }

        public GuestServiceInterfaceSettingDataComponent(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public GuestServiceInterfaceSettingDataComponent(ManagementObject theObject) : base(theObject, ClassName) { }

        public GuestServiceInterfaceSettingDataComponent(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * A reference to the guest service interface component.
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
         * A reference to the guest service resource.
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
        public static List<GuestServiceInterfaceSettingDataComponent> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceSettingDataComponent(mo)).ToList();

        public new static List<GuestServiceInterfaceSettingDataComponent> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceSettingDataComponent(mo)).ToList();

        public static List<GuestServiceInterfaceSettingDataComponent> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceSettingDataComponent(mo)).ToList();

        public static List<GuestServiceInterfaceSettingDataComponent> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceSettingDataComponent(mo)).ToList();

        public static List<GuestServiceInterfaceSettingDataComponent> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceSettingDataComponent(mo)).ToList();

        public static List<GuestServiceInterfaceSettingDataComponent> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceSettingDataComponent(mo)).ToList();

        public static List<GuestServiceInterfaceSettingDataComponent> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceSettingDataComponent(mo)).ToList();

        public static List<GuestServiceInterfaceSettingDataComponent> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceSettingDataComponent(mo)).ToList();

        public static GuestServiceInterfaceSettingDataComponent CreateInstance() => new GuestServiceInterfaceSettingDataComponent(CreateInstance(ClassName));
    }
}
