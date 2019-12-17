
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.VirtualSystem
{
    public class VirtualSystemSettingDataComponent : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(VirtualSystemSettingDataComponent)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public VirtualSystemSettingDataComponent() : base(ClassName) { }

        public VirtualSystemSettingDataComponent(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualSystemSettingDataComponent(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualSystemSettingDataComponent(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public VirtualSystemSettingDataComponent(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public VirtualSystemSettingDataComponent(ManagementPath path) : base(path, ClassName) { }

        public VirtualSystemSettingDataComponent(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public VirtualSystemSettingDataComponent(ManagementObject theObject) : base(theObject, ClassName) { }

        public VirtualSystemSettingDataComponent(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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
        public static List<VirtualSystemSettingDataComponent> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingDataComponent(mo)).ToList();

        public new static List<VirtualSystemSettingDataComponent> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingDataComponent(mo)).ToList();

        public static List<VirtualSystemSettingDataComponent> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingDataComponent(mo)).ToList();

        public static List<VirtualSystemSettingDataComponent> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingDataComponent(mo)).ToList();

        public static List<VirtualSystemSettingDataComponent> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingDataComponent(mo)).ToList();

        public static List<VirtualSystemSettingDataComponent> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingDataComponent(mo)).ToList();

        public static List<VirtualSystemSettingDataComponent> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingDataComponent(mo)).ToList();

        public static List<VirtualSystemSettingDataComponent> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingDataComponent(mo)).ToList();

        public static VirtualSystemSettingDataComponent CreateInstance() => new VirtualSystemSettingDataComponent(CreateInstance(ClassName));
    }
}
