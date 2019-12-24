using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.WindowsStorageManagement.MSFT
{
    public class PartitionToVolume : MsftBase
    {
        public static string ClassName => $"MSFT_{nameof(PartitionToVolume)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public PartitionToVolume() : base(ClassName) { }

        public PartitionToVolume(string keyObjectId) : base(keyObjectId, ClassName) { }

        public PartitionToVolume(ManagementScope mgmtScope, string keyObjectId) : base(mgmtScope, keyObjectId, ClassName) { }

        public PartitionToVolume(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public PartitionToVolume(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public PartitionToVolume(ManagementPath path) : base(path, ClassName) { }

        public PartitionToVolume(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public PartitionToVolume(ManagementObject theObject) : base(theObject, ClassName) { }

        public PartitionToVolume(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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

        public ManagementPath Volume
        {
            get
            {
                if (LateBoundObject[nameof(Volume)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(Volume)].ToString());
                }
                return null;
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<PartitionToVolume> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new PartitionToVolume(mo)).ToList();

        public new static List<PartitionToVolume> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new PartitionToVolume(mo)).ToList();

        public static List<PartitionToVolume> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new PartitionToVolume(mo)).ToList();

        public static List<PartitionToVolume> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new PartitionToVolume(mo)).ToList();

        public static List<PartitionToVolume> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new PartitionToVolume(mo)).ToList();

        public static List<PartitionToVolume> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new PartitionToVolume(mo)).ToList();

        public static List<PartitionToVolume> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new PartitionToVolume(mo)).ToList();

        public static List<PartitionToVolume> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new PartitionToVolume(mo)).ToList();

        public static PartitionToVolume CreateInstance() => new PartitionToVolume(CreateInstance(ClassName));
    }
}
