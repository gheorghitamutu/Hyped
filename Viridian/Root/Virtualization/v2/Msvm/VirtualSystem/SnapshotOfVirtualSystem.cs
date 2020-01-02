using System.Management;
using System.Linq;
using System.Collections.Generic;

namespace Viridian.Root.Virtualization.v2.Msvm.VirtualSystem
{
    public class SnapshotOfVirtualSystem : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(SnapshotOfVirtualSystem)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public SnapshotOfVirtualSystem() : base(ClassName) { }

        public SnapshotOfVirtualSystem(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public SnapshotOfVirtualSystem(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public SnapshotOfVirtualSystem(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public SnapshotOfVirtualSystem(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public SnapshotOfVirtualSystem(ManagementPath path) : base(path, ClassName) { }

        public SnapshotOfVirtualSystem(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public SnapshotOfVirtualSystem(ManagementObject theObject) : base(theObject, ClassName) { }

        public SnapshotOfVirtualSystem(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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
        public static List<SnapshotOfVirtualSystem> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new SnapshotOfVirtualSystem(mo)).ToList();

        public new static List<SnapshotOfVirtualSystem> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new SnapshotOfVirtualSystem(mo)).ToList();

        public static List<SnapshotOfVirtualSystem> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SnapshotOfVirtualSystem(mo)).ToList();

        public static List<SnapshotOfVirtualSystem> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SnapshotOfVirtualSystem(mo)).ToList();

        public static List<SnapshotOfVirtualSystem> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new SnapshotOfVirtualSystem(mo)).ToList();

        public static List<SnapshotOfVirtualSystem> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new SnapshotOfVirtualSystem(mo)).ToList();

        public static List<SnapshotOfVirtualSystem> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SnapshotOfVirtualSystem(mo)).ToList();

        public static List<SnapshotOfVirtualSystem> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SnapshotOfVirtualSystem(mo)).ToList();

        public static SnapshotOfVirtualSystem CreateInstance() => new SnapshotOfVirtualSystem(CreateInstance(ClassName));

    }
}
