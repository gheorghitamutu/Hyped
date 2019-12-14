using System.Management;
using System.Collections.Generic;
using System.Linq;

namespace Viridian.Msvm.VirtualSystem
{
    public class MostCurrentSnapshotInBranch : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(MostCurrentSnapshotInBranch)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public MostCurrentSnapshotInBranch() : base(ClassName) { }

        public MostCurrentSnapshotInBranch(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MostCurrentSnapshotInBranch(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MostCurrentSnapshotInBranch(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public MostCurrentSnapshotInBranch(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public MostCurrentSnapshotInBranch(ManagementPath path) : base(path, ClassName) { }

        public MostCurrentSnapshotInBranch(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public MostCurrentSnapshotInBranch(ManagementObject theObject) : base(theObject, ClassName) { }

        public MostCurrentSnapshotInBranch(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * Reference to the instance of the Msvm_ComputerSystem or 
         * Msvm_PlannedComputerSystem class representing a virtual system.
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
         * Reference to the instance of the Msvm_VirtualSystemSettingData class representing
         * the virtual system snapshot that is the most current snapshot in a branch of dependent snapshots.
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
        public static List<MostCurrentSnapshotInBranch> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new MostCurrentSnapshotInBranch(mo)).ToList();

        public new static List<MostCurrentSnapshotInBranch> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MostCurrentSnapshotInBranch(mo)).ToList();

        public static List<MostCurrentSnapshotInBranch> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MostCurrentSnapshotInBranch(mo)).ToList();

        public static List<MostCurrentSnapshotInBranch> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MostCurrentSnapshotInBranch(mo)).ToList();

        public static List<MostCurrentSnapshotInBranch> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new MostCurrentSnapshotInBranch(mo)).ToList();

        public static List<MostCurrentSnapshotInBranch> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MostCurrentSnapshotInBranch(mo)).ToList();

        public static List<MostCurrentSnapshotInBranch> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MostCurrentSnapshotInBranch(mo)).ToList();

        public static List<MostCurrentSnapshotInBranch> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MostCurrentSnapshotInBranch(mo)).ToList();

        public static MostCurrentSnapshotInBranch CreateInstance() => new MostCurrentSnapshotInBranch(CreateInstance(ClassName));
    }
}
