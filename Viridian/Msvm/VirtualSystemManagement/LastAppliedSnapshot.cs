using System.Management;
using System.Collections.Generic;
using System.Linq;

namespace Viridian.Msvm.VirtualSystemManagement
{
    public class LastAppliedSnapshot : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(LastAppliedSnapshot)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public LastAppliedSnapshot() : base(ClassName) { }

        public LastAppliedSnapshot(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public LastAppliedSnapshot(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public LastAppliedSnapshot(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public LastAppliedSnapshot(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public LastAppliedSnapshot(ManagementPath path) : base(path, ClassName) { }

        public LastAppliedSnapshot(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public LastAppliedSnapshot(ManagementObject theObject) : base(theObject, ClassName) { }

        public LastAppliedSnapshot(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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
        public static List<LastAppliedSnapshot> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new LastAppliedSnapshot(mo)).ToList();

        public new static List<LastAppliedSnapshot> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new LastAppliedSnapshot(mo)).ToList();

        public static List<LastAppliedSnapshot> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new LastAppliedSnapshot(mo)).ToList();

        public static List<LastAppliedSnapshot> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new LastAppliedSnapshot(mo)).ToList();

        public static List<LastAppliedSnapshot> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new LastAppliedSnapshot(mo)).ToList();

        public static List<LastAppliedSnapshot> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new LastAppliedSnapshot(mo)).ToList();

        public static List<LastAppliedSnapshot> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new LastAppliedSnapshot(mo)).ToList();

        public static List<LastAppliedSnapshot> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new LastAppliedSnapshot(mo)).ToList();

        public static LastAppliedSnapshot CreateInstance() => new LastAppliedSnapshot(CreateInstance(ClassName));
    }
}
