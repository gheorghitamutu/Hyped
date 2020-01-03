using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.VirtualSystem
{
    public class BootSourceComponent : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(BootSourceComponent)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public BootSourceComponent() : base(ClassName) { }

        public BootSourceComponent(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public BootSourceComponent(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public BootSourceComponent(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public BootSourceComponent(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public BootSourceComponent(ManagementPath path) : base(path, ClassName) { }

        public BootSourceComponent(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public BootSourceComponent(ManagementObject theObject) : base(theObject, ClassName) { }

        public BootSourceComponent(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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
        public static List<BootSourceComponent> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceComponent(mo)).ToList();

        public new static List<BootSourceComponent> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceComponent(mo)).ToList();

        public static List<BootSourceComponent> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceComponent(mo)).ToList();

        public static List<BootSourceComponent> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceComponent(mo)).ToList();

        public static List<BootSourceComponent> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceComponent(mo)).ToList();

        public static List<BootSourceComponent> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceComponent(mo)).ToList();

        public static List<BootSourceComponent> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceComponent(mo)).ToList();

        public static List<BootSourceComponent> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceComponent(mo)).ToList();

        public static BootSourceComponent CreateInstance() => new BootSourceComponent(CreateInstance(ClassName));
    }
}
