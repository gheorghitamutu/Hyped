using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Microsoft.Windows.Storage.MSFT
{
    public class Partition : MsftBase
    {
        public static string ClassName => $"MSFT_{nameof(Partition)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public Partition() : base(ClassName) { }

        public Partition(string keyObjectId) : base(keyObjectId, ClassName) { }

        public Partition(ManagementScope mgmtScope, string keyObjectId) : base(mgmtScope, keyObjectId, ClassName) { }

        public Partition(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public Partition(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public Partition(ManagementPath path) : base(path, ClassName) { }

        public Partition(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public Partition(ManagementObject theObject) : base(theObject, ClassName) { }

        public Partition(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * This property is an array of all the various mount points for the partition.
         * This list includes drive letters, as well as mounted folders.
         */
        public string[] AccessPaths => (string[])LateBoundObject[nameof(AccessPaths)];

        /*
         * This property is identical to the ObjectId field of the Disk object that contains this partition.
         */
        public string DiskId => (string)LateBoundObject[nameof(DiskId)];

        /*
         * The operating system's number for the Disk that contains this partition.
         * Disk numbers may not necessarily remain the same across reboots.
         */
        public uint DiskNumber
        {
            get
            {
                if (LateBoundObject[nameof(DiskNumber)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(DiskNumber)];
            }
        }

        /*
         * The currently assigned drive letter to the partition.
         * This property is NULL if no drive letter has been assigned.
         */
        public char DriveLetter
        {
            get
            {
                if (LateBoundObject[nameof(DriveLetter)] == null)
                {
                    return Convert.ToChar(0);
                }
                return (char)LateBoundObject[nameof(DriveLetter)];
            }
        }

        /*
         * This property indicates the partition's GPT type.
         * This property is only valid when the Disk's PartitionStyle property is set to 2 - 'GPT' and will be NULL for all other partition styles.
         */
        public string GptType => (string)LateBoundObject[nameof(GptType)];

        /*
         * This property is a string representation of the partition's GPT GUID.
         * This property is only valid if the Disk's PartitionStyle property is set to 2 - 'GPT' and will be NULL for all other partition styles.
         */
        public string Guid => (string)LateBoundObject[nameof(Guid)];

        /*
         * Signifies whether or not the partition is active and can be booted.
         * This property is only relevant for MBR Disks.
         */
        public bool IsActive
        {
            get
            {
                if (LateBoundObject[nameof(IsActive)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsActive)];
            }
        }

        public bool IsBoot
        {
            get
            {
                if (LateBoundObject[nameof(IsBoot)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsBoot)];
            }
        }

        /*
         * If this property is set to TRUE, the partition is in direct access mode.
         * In this mode a memory mapped file doesn't reside in RAM, instead it is mapped directly onto the Storage Class Memory device and IOs bypass the storage stack.
         * If set to FALSE, the partiton is in the standard block mode.
         */
        public bool IsDAX
        {
            get
            {
                if (LateBoundObject[nameof(IsDAX)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsDAX)];
            }
        }

        /*
         * If this property is set to TRUE, the partition is not detected by the mount manager.
         * As a result, the partition does not receive a drive letter, does not receive a volume GUID path, does not host volume mount points, and is not enumerated
         * by calls to FindFirstVolume and FindNextVolume. This ensures that applications such as Disk defragmenter do not access the partition.
         * The Volume Shadow Copy Service (VSS) uses this attribute on its shadow copies.
         */
        public bool IsHidden
        {
            get
            {
                if (LateBoundObject[nameof(IsHidden)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsHidden)];
            }
        }
        public bool IsOffline
        {
            get
            {
                if (LateBoundObject[nameof(IsOffline)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsOffline)];
            }
        }

        public bool IsReadOnly
        {
            get
            {
                if (LateBoundObject[nameof(IsReadOnly)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsReadOnly)];
            }
        }

        /*
         * If this property is set to TRUE, the partition is a shadow copy of another partition.
         * This attribute is used by the Volume Shadow Copy service (VSS).
         * This attribute is an indication for file system filter driver-based software (such as antivirus programs) to avoid attaching to the volume.
         * An application can use this attribute to differentiate a shadow copy partition from a production partition.
         * For example, an application that performs a fast recovery will break a shadow copy virtual Disk by clearing the read-only and hidden attributes and this attribute.
         * This attribute is set when the shadow copy is created and cleared when the shadow copy is broken.
         */
        public bool IsShadowCopy
        {
            get
            {
                if (LateBoundObject[nameof(IsShadowCopy)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsShadowCopy)];
            }
        }
        public bool IsSystem
        {
            get
            {
                if (LateBoundObject[nameof(IsSystem)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsSystem)];
            }
        }

        /*
         * This property indicates the partition's MBR type.
         * This property is only valid when the Disk's PartitionStyle property is set to 1 - 'MBR' and will be NULL for all other partition styles.
         */
        public MbrTypeValues MbrType
        {
            get
            {
                if (LateBoundObject[nameof(MbrType)] == null)
                {
                    return (MbrTypeValues)Convert.ToInt32(0);
                }
                return (MbrTypeValues)Convert.ToInt32(LateBoundObject[nameof(MbrType)]);
            }
        }

        /*
         * If this property is set to TRUE, the operating system does not assign a drive letter automatically when the partition is discovered.
         * This is only honored for GPT Disks and is assumed to be FALSE for MBR Disks.
         * This attribute is useful in storage area network (SAN) environments.
         */
        public bool NoDefaultDriveLetter
        {
            get
            {
                if (LateBoundObject[nameof(NoDefaultDriveLetter)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(NoDefaultDriveLetter)];
            }
        }

        /*
         * ObjectId is a mandatory property that is used to opaquely and uniquely identify an instance of a class.
         * ObjectIds must be unique within the scope of the management server (which is hosting the provider).
         * The ObjectId is created and maintained for use of the Storage Management Providers and their clients to track instances of objects.
         * If an object is visible through two different paths (for example: there are two separate Storage Management Providers that point to the same storage subsystem) 
         * then the same object may appear with two different ObjectIds.
         * For determining if two object instances are the same object, refer to the UniqueId property.
         */
        public string ObjectId => (string)LateBoundObject[nameof(ObjectId)];

        /*
         * This property indicates the partition's offset from the beginning of the Disk, measured in bytes.
         */
        public ulong Offset
        {
            get
            {
                if (LateBoundObject[nameof(Offset)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(Offset)];
            }
        }

        public OperationalStatusValues OperationalStatus
        {
            get
            {
                if (LateBoundObject[nameof(OperationalStatus)] == null)
                {
                    return (OperationalStatusValues)Convert.ToInt32(6);
                }
                return (OperationalStatusValues)Convert.ToInt32(LateBoundObject[nameof(OperationalStatus)]);
            }
        }

        /*
         * The operating system's number for the partition.
         * Ordering is based on the partition's offset, relative to other partitions.
         * This means that the value for this property may change based off of the partition configuration in the offset range preceding this partition.
         */
        public uint PartitionNumber
        {
            get
            {
                if (LateBoundObject[nameof(PartitionNumber)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(PartitionNumber)];
            }
        }

        /*
         * PassThroughClass is the WBEM class name of the proprietary storage provider object.
         */
        public string PassThroughClass => (string)LateBoundObject[nameof(PassThroughClass)];

        /*
         * PassThroughIds is a comma-separated list of all implementation specific keys.
         * It is used by storage management applications to access the vendor proprietary object model.
         * This field should be in the form: key1='value1',key2='value2'.
         */
        public string PassThroughIds => (string)LateBoundObject[nameof(PassThroughIds)];

        /*
         * "PassThroughNamespace is the WBEM namespace that contains the proprietary storage provider classes.
         */
        public string PassThroughNamespace => (string)LateBoundObject[nameof(PassThroughNamespace)];

        /*
         * PassThroughServer is the name or address of the computer system hosting the proprietary storage provider classes.
         */
        public string PassThroughServer => (string)LateBoundObject[nameof(PassThroughServer)];

        /*
         * Total size of the partition, measured in bytes.
         */
        public ulong Size
        {
            get
            {
                if (LateBoundObject[nameof(Size)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(Size)];
            }
        }

        public ushort TransitionState
        {
            get
            {
                if (LateBoundObject[nameof(TransitionState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(TransitionState)];
            }
        }

        /*
         * UniqueId is a mandatory property that is used to uniquely identify a logical instance of a storage subsystem's object.
         * This value must be the same for an object viewed by two or more provider instances (even if they are running on seperate management servers).
         * UniqueId can be any globally unique, opaque value unless otherwise specified by a derived class.
         */
        public string UniqueId => (string)LateBoundObject[nameof(UniqueId)];

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<Partition> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new Partition(mo)).ToList();

        public new static List<Partition> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new Partition(mo)).ToList();

        public static List<Partition> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Partition(mo)).ToList();

        public static List<Partition> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Partition(mo)).ToList();

        public static List<Partition> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new Partition(mo)).ToList();

        public static List<Partition> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new Partition(mo)).ToList();

        public static List<Partition> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Partition(mo)).ToList();

        public static List<Partition> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Partition(mo)).ToList();

        public static Partition CreateInstance() => new Partition(CreateInstance(ClassName));

        public uint AddAccessPath(string AccessPath, bool AssignDriveLetter, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("AddAccessPath");
                inParams["AccessPath"] = AccessPath;
                inParams["AssignDriveLetter"] = AssignDriveLetter;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("AddAccessPath", inParams, null);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint DeleteObject(out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DeleteObject", inParams, null);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetAccessPaths(out string[] AccessPaths, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetAccessPaths", inParams, null);
                AccessPaths = (string[])outParams.Properties[nameof(AccessPaths)].Value;
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                AccessPaths = null;
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetSupportedSize(out ManagementBaseObject ExtendedStatus, out ulong SizeMax, out ulong SizeMin)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetSupportedSize", inParams, null);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                SizeMax = Convert.ToUInt64(outParams.Properties["SizeMax"].Value);
                SizeMin = Convert.ToUInt64(outParams.Properties["SizeMin"].Value);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ExtendedStatus = null;
                SizeMax = Convert.ToUInt64(0);
                SizeMin = Convert.ToUInt64(0);
                return Convert.ToUInt32(0);
            }
        }

        public uint Offline(out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Offline", inParams, null);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint Online(out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Online", inParams, null);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RemoveAccessPath(string AccessPath, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RemoveAccessPath");
                inParams["AccessPath"] = AccessPath;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RemoveAccessPath", inParams, null);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint Resize(ulong Size, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("Resize");
                inParams[nameof(Size)] = Size;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Resize", inParams, null);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint SetAttributes(string GptType, bool IsActive, bool IsDAX, bool IsHidden, bool IsReadOnly, bool IsShadowCopy, ushort MbrType, bool NoDefaultDriveLetter, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("SetAttributes");
                inParams[nameof(GptType)] = GptType;
                inParams[nameof(IsActive)] = IsActive;
                inParams[nameof(IsDAX)] = IsDAX;
                inParams[nameof(IsHidden)] = IsHidden;
                inParams[nameof(IsReadOnly)] = IsReadOnly;
                inParams[nameof(IsShadowCopy)] = IsShadowCopy;
                inParams[nameof(MbrType)] = MbrType;
                inParams[nameof(NoDefaultDriveLetter)] = NoDefaultDriveLetter;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetAttributes", inParams, null);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public enum MbrTypeValues
        {
            FAT12 = 1,
            FAT16 = 4,
            Extended = 5,
            Huge = 6,
            IFS = 7,
            FAT32 = 12,
            NULL_ENUM_VALUE = 0,
        }

        public enum OperationalStatusValues
        {
            Unknown0 = 0,
            Online0 = 1,
            No_Media = 3,
            Failed = 5,
            Offline0 = 4,
            NULL_ENUM_VALUE = 6,
        }
    }
}
