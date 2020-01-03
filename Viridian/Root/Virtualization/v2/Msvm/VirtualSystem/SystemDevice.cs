using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.VirtualSystem
{
    public class SystemDevice : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(SystemDevice)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public SystemDevice() : base(ClassName) { }

        public SystemDevice(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public SystemDevice(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public SystemDevice(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public SystemDevice(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public SystemDevice(ManagementPath path) : base(path, ClassName) { }

        public SystemDevice(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public SystemDevice(ManagementObject theObject) : base(theObject, ClassName) { }

        public SystemDevice(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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
        public static List<SystemDevice> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new SystemDevice(mo)).ToList();

        public new static List<SystemDevice> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new SystemDevice(mo)).ToList();

        public static List<SystemDevice> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SystemDevice(mo)).ToList();

        public static List<SystemDevice> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SystemDevice(mo)).ToList();

        public static List<SystemDevice> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new SystemDevice(mo)).ToList();

        public static List<SystemDevice> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new SystemDevice(mo)).ToList();

        public static List<SystemDevice> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SystemDevice(mo)).ToList();

        public static List<SystemDevice> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SystemDevice(mo)).ToList();

        public static SystemDevice CreateInstance() => new SystemDevice(CreateInstance(ClassName));
    }
}
