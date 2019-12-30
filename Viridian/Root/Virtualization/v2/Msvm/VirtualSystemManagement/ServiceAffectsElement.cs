using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement
{
    public class ServiceAffectsElement : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(ServiceAffectsElement)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public ServiceAffectsElement() : base(ClassName) { }

        public ServiceAffectsElement(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ServiceAffectsElement(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ServiceAffectsElement(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public ServiceAffectsElement(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public ServiceAffectsElement(ManagementPath path) : base(path, ClassName) { }

        public ServiceAffectsElement(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public ServiceAffectsElement(ManagementObject theObject) : base(theObject, ClassName) { }

        public ServiceAffectsElement(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public ManagementPath AffectedElement
        {
            get
            {
                if (LateBoundObject[nameof(AffectedElement)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(AffectedElement)].ToString());
                }
                return null;
            }
        }

        public ManagementPath AffectingElement
        {
            get
            {
                if (LateBoundObject[nameof(AffectingElement)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(AffectingElement)].ToString());
                }
                return null;
            }
        }

        public ushort[] ElementEffects => (ushort[])LateBoundObject[nameof(ElementEffects)];

        public string[] OtherElementEffectsDescriptions => (string[])LateBoundObject[nameof(OtherElementEffectsDescriptions)];

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<ServiceAffectsElement> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new ServiceAffectsElement(mo)).ToList();

        public new static List<ServiceAffectsElement> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ServiceAffectsElement(mo)).ToList();

        public static List<ServiceAffectsElement> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ServiceAffectsElement(mo)).ToList();

        public static List<ServiceAffectsElement> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ServiceAffectsElement(mo)).ToList();

        public static List<ServiceAffectsElement> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new ServiceAffectsElement(mo)).ToList();

        public static List<ServiceAffectsElement> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ServiceAffectsElement(mo)).ToList();

        public static List<ServiceAffectsElement> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ServiceAffectsElement(mo)).ToList();

        public static List<ServiceAffectsElement> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ServiceAffectsElement(mo)).ToList();

        public static ServiceAffectsElement CreateInstance() => new ServiceAffectsElement(CreateInstance(ClassName));

    }
}
