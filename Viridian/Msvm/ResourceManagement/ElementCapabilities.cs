using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Viridian.Msvm.ResourceManagement
{
    public class ElementCapabilities : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(ElementCapabilities)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public ElementCapabilities() : base(ClassName) { }

        public ElementCapabilities(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ElementCapabilities(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ElementCapabilities(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public ElementCapabilities(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public ElementCapabilities(ManagementPath path) : base(path, ClassName) { }

        public ElementCapabilities(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public ElementCapabilities(ManagementObject theObject) : base(theObject, ClassName) { }

        public ElementCapabilities(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public ManagementPath Capabilities
        {
            get
            {
                if (LateBoundObject[nameof(Capabilities)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(Capabilities)].ToString());
                }
                return null;
            }
        }

        public ushort[] Characteristics => ((ushort[])(LateBoundObject[nameof(Characteristics)]));

        public ManagementPath ManagedElement
        {
            get
            {
                if (LateBoundObject[nameof(ManagedElement)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(ManagedElement)].ToString());
                }
                return null;
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<ElementCapabilities> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new ElementCapabilities(mo)).ToList();

        public new static List<ElementCapabilities> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ElementCapabilities(mo)).ToList();

        public static List<ElementCapabilities> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ElementCapabilities(mo)).ToList();

        public static List<ElementCapabilities> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ElementCapabilities(mo)).ToList();

        public static List<ElementCapabilities> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new ElementCapabilities(mo)).ToList();

        public static List<ElementCapabilities> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ElementCapabilities(mo)).ToList();

        public static List<ElementCapabilities> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ElementCapabilities(mo)).ToList();

        public static List<ElementCapabilities> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ElementCapabilities(mo)).ToList();

        public static ElementCapabilities CreateInstance() => new ElementCapabilities(CreateInstance(ClassName));
    }
}
