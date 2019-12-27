using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.ResourceManagement
{
    public class AllocationCapabilities : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(AllocationCapabilities)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public AllocationCapabilities() : base(ClassName) { }

        public AllocationCapabilities(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public AllocationCapabilities(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public AllocationCapabilities(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public AllocationCapabilities(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public AllocationCapabilities(ManagementPath path) : base(path, ClassName) { }

        public AllocationCapabilities(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public AllocationCapabilities(ManagementObject theObject) : base(theObject, ClassName) { }

        public AllocationCapabilities(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        public string OtherResourceType => (string)LateBoundObject[nameof(OtherResourceType)];

        public ushort RequestTypesSupported
        {
            get
            {
                if (LateBoundObject[nameof(RequestTypesSupported)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(RequestTypesSupported)];
            }
        }

        public string ResourceSubType => (string)LateBoundObject[nameof(ResourceSubType)];

        public ushort ResourceType
        {
            get
            {
                if (LateBoundObject[nameof(ResourceType)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ResourceType)];
            }
        }

        public ushort SharingMode
        {
            get
            {
                if (LateBoundObject[nameof(SharingMode)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(SharingMode)];
            }
        }

        public ushort[] SupportedAddStates => (ushort[])LateBoundObject[nameof(SupportedAddStates)];

        public ushort[] SupportedRemoveStates => (ushort[])LateBoundObject[nameof(SupportedRemoveStates)];

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<AllocationCapabilities> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new AllocationCapabilities(mo)).ToList();

        public new static List<AllocationCapabilities> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new AllocationCapabilities(mo)).ToList();

        public static List<AllocationCapabilities> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new AllocationCapabilities(mo)).ToList();

        public static List<AllocationCapabilities> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new AllocationCapabilities(mo)).ToList();

        public static List<AllocationCapabilities> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new AllocationCapabilities(mo)).ToList();

        public static List<AllocationCapabilities> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new AllocationCapabilities(mo)).ToList();

        public static List<AllocationCapabilities> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new AllocationCapabilities(mo)).ToList();

        public static List<AllocationCapabilities> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new AllocationCapabilities(mo)).ToList();

        public static AllocationCapabilities CreateInstance() => new AllocationCapabilities(CreateInstance(ClassName));
    }
}
