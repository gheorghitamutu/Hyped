using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.Memory
{
    public class ElementAllocatedFromNumaNode : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(ElementAllocatedFromNumaNode)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public ElementAllocatedFromNumaNode() : base(ClassName) { }

        public ElementAllocatedFromNumaNode(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ElementAllocatedFromNumaNode(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ElementAllocatedFromNumaNode(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public ElementAllocatedFromNumaNode(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public ElementAllocatedFromNumaNode(ManagementPath path) : base(path, ClassName) { }

        public ElementAllocatedFromNumaNode(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public ElementAllocatedFromNumaNode(ManagementObject theObject) : base(theObject, ClassName) { }

        public ElementAllocatedFromNumaNode(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * The physical NUMA node.
         */
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

        /*
         * The allocated resource.
         */
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

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<ElementAllocatedFromNumaNode> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new ElementAllocatedFromNumaNode(mo)).ToList();

        public new static List<ElementAllocatedFromNumaNode> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ElementAllocatedFromNumaNode(mo)).ToList();

        public static List<ElementAllocatedFromNumaNode> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ElementAllocatedFromNumaNode(mo)).ToList();

        public static List<ElementAllocatedFromNumaNode> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ElementAllocatedFromNumaNode(mo)).ToList();

        public static List<ElementAllocatedFromNumaNode> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new ElementAllocatedFromNumaNode(mo)).ToList();

        public static List<ElementAllocatedFromNumaNode> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ElementAllocatedFromNumaNode(mo)).ToList();

        public static List<ElementAllocatedFromNumaNode> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ElementAllocatedFromNumaNode(mo)).ToList();

        public static List<ElementAllocatedFromNumaNode> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ElementAllocatedFromNumaNode(mo)).ToList();

        public static ElementAllocatedFromNumaNode CreateInstance() => new ElementAllocatedFromNumaNode(CreateInstance(ClassName));
    }
}
