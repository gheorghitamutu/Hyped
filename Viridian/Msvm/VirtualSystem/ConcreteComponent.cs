using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.VirtualSystem
{
    public class ConcreteComponent : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(ConcreteComponent)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public ConcreteComponent() : base(ClassName) { }

        public ConcreteComponent(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ConcreteComponent(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ConcreteComponent(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public ConcreteComponent(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public ConcreteComponent(ManagementPath path) : base(path, ClassName) { }

        public ConcreteComponent(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public ConcreteComponent(ManagementObject theObject) : base(theObject, ClassName) { }

        public ConcreteComponent(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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
        public static List<ConcreteComponent> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteComponent(mo)).ToList();

        public new static List<ConcreteComponent> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteComponent(mo)).ToList();

        public static List<ConcreteComponent> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteComponent(mo)).ToList();

        public static List<ConcreteComponent> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteComponent(mo)).ToList();

        public static List<ConcreteComponent> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteComponent(mo)).ToList();

        public static List<ConcreteComponent> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteComponent(mo)).ToList();

        public static List<ConcreteComponent> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteComponent(mo)).ToList();

        public static List<ConcreteComponent> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteComponent(mo)).ToList();

        public static ConcreteComponent CreateInstance() => new ConcreteComponent(CreateInstance(ClassName));
    }
}
