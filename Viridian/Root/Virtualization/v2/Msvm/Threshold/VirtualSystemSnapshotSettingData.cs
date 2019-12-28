using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Threshold
{
    public class VirtualSystemSnapshotSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(VirtualSystemSnapshotSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public VirtualSystemSnapshotSettingData() : base(ClassName) { }

        public VirtualSystemSnapshotSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualSystemSnapshotSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualSystemSnapshotSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public VirtualSystemSnapshotSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public VirtualSystemSnapshotSettingData(ManagementPath path) : base(path, ClassName) { }

        public VirtualSystemSnapshotSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public VirtualSystemSnapshotSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public VirtualSystemSnapshotSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string Caption
        {
            get
            {
                return (string)LateBoundObject[nameof(Caption)];
            }
            set
            {
                LateBoundObject[nameof(Caption)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Consistency level of the snapshot.
         */
        public ConsistencyLevelValues ConsistencyLevel
        {
            get
            {
                if (LateBoundObject[nameof(ConsistencyLevel)] == null)
                {
                    return (ConsistencyLevelValues)System.Convert.ToInt32(3);
                }
                return (ConsistencyLevelValues)System.Convert.ToInt32(LateBoundObject[nameof(ConsistencyLevel)]);
            }
            set
            {
                if (ConsistencyLevelValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(ConsistencyLevel)] = null;
                }
                else
                {
                    LateBoundObject[nameof(ConsistencyLevel)] = value;
                }
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string Description
        {
            get
            {
                return (string)LateBoundObject[nameof(Description)];
            }
            set
            {
                LateBoundObject[nameof(Description)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string ElementName
        {
            get
            {
                return (string)LateBoundObject[nameof(ElementName)];
            }
            set
            {
                LateBoundObject[nameof(ElementName)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Backup type to be used inside the guest.
         */
        public GuestBackupTypeValues GuestBackupType
        {
            get
            {
                if (LateBoundObject[nameof(GuestBackupType)] == null)
                {
                    return (GuestBackupTypeValues)System.Convert.ToInt32(3);
                }
                return (GuestBackupTypeValues)System.Convert.ToInt32(LateBoundObject[nameof(GuestBackupType)]);
            }
            set
            {
                if (GuestBackupTypeValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(GuestBackupType)] = null;
                }
                else
                {
                    LateBoundObject[nameof(GuestBackupType)] = value;
                }
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Specifies if non-snapshottable __Disks like passthrough __Disks and Fibre Channel __Disks are to be ignored while creating the snapshot.
         */
        public bool IgnoreNonSnapshottable__Disks
        {
            get
            {
                if (LateBoundObject[nameof(IgnoreNonSnapshottable__Disks)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IgnoreNonSnapshottable__Disks)];
            }
            set
            {
                LateBoundObject[nameof(IgnoreNonSnapshottable__Disks)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string InstanceID
        {
            get
            {
                return (string)LateBoundObject[nameof(InstanceID)];
            }
            set
            {
                LateBoundObject[nameof(InstanceID)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public static List<VirtualSystemSnapshotSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotSettingData(mo)).ToList();

        public new static List<VirtualSystemSnapshotSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotSettingData(mo)).ToList();

        public static List<VirtualSystemSnapshotSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotSettingData(mo)).ToList();

        public static List<VirtualSystemSnapshotSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotSettingData(mo)).ToList();

        public static List<VirtualSystemSnapshotSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotSettingData(mo)).ToList();

        public static List<VirtualSystemSnapshotSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotSettingData(mo)).ToList();

        public static List<VirtualSystemSnapshotSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotSettingData(mo)).ToList();

        public static List<VirtualSystemSnapshotSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotSettingData(mo)).ToList();

        public static VirtualSystemSnapshotSettingData CreateInstance() => new VirtualSystemSnapshotSettingData(CreateInstance(ClassName));

        public enum ConsistencyLevelValues
        {
            Unknown0 = 0,
            Application_Consistent = 1,
            Crash_Consistent = 2,
            NULL_ENUM_VALUE = 3,
        }

        public enum GuestBackupTypeValues
        {
            Undefined = 0,
            Full = 1,
            Copy = 2,
            NULL_ENUM_VALUE = 3,
        }
    }
}
