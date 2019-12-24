using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.WindowsStorageManagement.MSFT
{
    public class DiskToPartition : MsftBase
    {
        public static string ClassName => $"MSFT_{nameof(DiskToPartition)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public DiskToPartition() : base(ClassName) { }

        public DiskToPartition(string keyObjectId) : base(keyObjectId, ClassName) { }

        public DiskToPartition(ManagementScope mgmtScope, string keyObjectId) : base(mgmtScope, keyObjectId, ClassName) { }

        public DiskToPartition(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public DiskToPartition(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public DiskToPartition(ManagementPath path) : base(path, ClassName) { }

        public DiskToPartition(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public DiskToPartition(ManagementObject theObject) : base(theObject, ClassName) { }

        public DiskToPartition(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public ManagementPath Disk
        {
            get
            {
                if (LateBoundObject[nameof(Disk)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(Disk)].ToString());
                }
                return null;
            }
        }

        public ManagementPath Partition
        {
            get
            {
                if (LateBoundObject[nameof(Partition)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(Partition)].ToString());
                }
                return null;
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<DiskToPartition> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new DiskToPartition(mo)).ToList();

        public new static List<DiskToPartition> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new DiskToPartition(mo)).ToList();

        public static List<DiskToPartition> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new DiskToPartition(mo)).ToList();

        public static List<DiskToPartition> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new DiskToPartition(mo)).ToList();

        public static List<DiskToPartition> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new DiskToPartition(mo)).ToList();

        public static List<DiskToPartition> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new DiskToPartition(mo)).ToList();

        public static List<DiskToPartition> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new DiskToPartition(mo)).ToList();

        public static List<DiskToPartition> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new DiskToPartition(mo)).ToList();

        public static DiskToPartition CreateInstance() => new DiskToPartition(CreateInstance(ClassName));
    }
}
