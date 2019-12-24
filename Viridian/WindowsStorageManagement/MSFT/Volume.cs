using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.WindowsStorageManagement.MSFT
{
    public class Volume : MsftBase
    {
        public static string ClassName => $"MSFT_{nameof(Volume)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public Volume() : base(ClassName) { }

        public Volume(string keyObjectId) : base(keyObjectId, ClassName) { }

        public Volume(ManagementScope mgmtScope, string keyObjectId) : base(mgmtScope, keyObjectId, ClassName) { }

        public Volume(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public Volume(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public Volume(ManagementPath path) : base(path, ClassName) { }

        public Volume(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public Volume(ManagementObject theObject) : base(theObject, ClassName) { }

        public Volume(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * The allocation unit size of the volume.
         */
        public uint AllocationUnitSize
        {
            get
            {
                if (LateBoundObject[nameof(AllocationUnitSize)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(AllocationUnitSize)];
            }
        }

        /*
         * Indicates the deduplication mode of the volume.
         */
        public DedupModeValues DedupMode
        {
            get
            {
                if (LateBoundObject[nameof(DedupMode)] == null)
                {
                    return (DedupModeValues)Convert.ToInt32(5);
                }
                return (DedupModeValues)Convert.ToInt32(LateBoundObject[nameof(DedupMode)]);
            }
        }

        /*
         * Drive letter assigned to the volume.
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
         * Denotes the type of the volume.
         */
        public DriveTypeValues DriveType
        {
            get
            {
                if (LateBoundObject[nameof(DriveType)] == null)
                {
                    return (DriveTypeValues)Convert.ToInt32(7);
                }
                return (DriveTypeValues)Convert.ToInt32(LateBoundObject[nameof(DriveType)]);
            }
        }

        /*
         * File system on the volume.
         */
        public string FileSystem => (string)LateBoundObject[nameof(FileSystem)];

        /*
         * File system label of the volume.
         */
        public string FileSystemLabel => (string)LateBoundObject[nameof(FileSystemLabel)];

        /*
         * The underlying file system type of the volume.
         */
        public ushort FileSystemType
        {
            get
            {
                if (LateBoundObject[nameof(FileSystemType)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(FileSystemType)];
            }
        }

        /*
         * The health status of the Volume.
         * 0 - 'Healthy': The volume is functioning normally.
         * 1 - 'Warning': The volume is still functioning, but has detected errors or issues that require administrator intervention.
         * 2 - 'Unhealthy': The volume is not functioning, due to errors or failures. The volume needs immediate attention from an administrator.
         */
        public HealthStatusValues HealthStatus
        {
            get
            {
                if (LateBoundObject[nameof(HealthStatus)] == null)
                {
                    return (HealthStatusValues)Convert.ToInt32(3);
                }
                return (HealthStatusValues)Convert.ToInt32(LateBoundObject[nameof(HealthStatus)]);
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
         * An array of values that denote the current operational status of the volume.
         * 0 - 'Unknown': The operational status is unknown.
         * 1 - 'Other': A vendor-specific OperationalStatus has been specified by setting the OtherOperationalStatusDescription property.
         * 2 - 'OK': The volume is responding to commands and is in a normal operating state.
         * 3 - 'Degraded': The volume is responding to commands, but is not running in an optimal operating state.
         * 4 - 'Stressed': The volume is functioning, but needs attention. For example, the volume might be overloaded or overheated.
         * 5 - 'Predictive Failure': The volume is functioning, but a failure is likely to occur in the near future.
         * 6 - 'Error': An error has occurred.
         * 7 - 'Non-Recoverable Error': A non-recoverable error has occurred.
         * 8 - 'Starting': The volume is in the process of starting.
         * 9 - 'Stopping': The volume is in the process of stopping.
         * 10 - 'Stopped': The volume was stopped or shut down in a clean and orderly fashion.
         * 11 - 'In Service': The volume is being configured, maintained, cleaned, or otherwise administered.
         * 12 - 'No Contact': The storage provider has knowledge of the volume, but has never been able to establish communication with it.
         * 13 - 'Lost Communication': The storage provider has knowledge of the volume and has contacted it successfully in the past, but the volume is currently unreachable.
         * 14 - 'Aborted': Similar to Stopped, except that the volume stopped abruptly and may require configuration or maintenance.
         * 15 - 'Dormant': The volume is reachable, but it is inactive.
         * 16 - 'Supporting Entity in Error': This status value does not necessarily indicate trouble with the volume, but it does indicate that another device or connection that the volume depends on may need attention.
         * 17 - 'Completed': The volume has completed an operation. This status value should be combined with OK, Error, or Degraded, depending on the outcome of the operation.
         * 0xD00D - 'Scan Needed': In Windows-based storage subsystems, this indicates a scan is needed but not repair.
         * 0xD00E - 'Spot Fix Needed': In Windows-based storage subsystems, this indicates limited repair is needed.
         * 0xD00F - 'Full Repair Needed': In Windows-based storage subsystems, this indicates full repair is needed.
         * 0xD013 - 'Offline': In Windows-based storage subsystems, this indicates that the object is offline.
         */
        public ushort[] OperationalStatus => (ushort[])LateBoundObject[nameof(OperationalStatus)];

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
         * PassThroughNamespace is the WBEM namespace that contains the proprietary storage provider classes.
         */
        public string PassThroughNamespace => (string)LateBoundObject[nameof(PassThroughNamespace)];

        /*
         * PassThroughServer is the name or address of the computer system hosting the proprietary storage provider classes.
         */
        public string PassThroughServer => (string)LateBoundObject[nameof(PassThroughServer)];

        /*
         * Guid path of the volume.
         */
        public string Path0 => (string)LateBoundObject["Path"];

        /*
         * Total size of the volume.
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

        /*
         * Available space on the volume.
         */
        public ulong SizeRemaining
        {
            get
            {
                if (LateBoundObject[nameof(SizeRemaining)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(SizeRemaining)];
            }
        }

        /*
         * UniqueId is a mandatory property that is used to uniquely identify a logical instance of a storage subsystem's object.
         * This value must be the same for an object viewed by two or more provider instances (even if they are running on separate management servers).
         * UniqueId can be any globally unique, opaque value unless otherwise specified by a derived class.
         */
        public string UniqueId => (string)LateBoundObject[nameof(UniqueId)];

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<Volume> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new Volume(mo)).ToList();

        public new static List<Volume> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new Volume(mo)).ToList();

        public static List<Volume> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Volume(mo)).ToList();

        public static List<Volume> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Volume(mo)).ToList();

        public static List<Volume> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new Volume(mo)).ToList();

        public static List<Volume> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new Volume(mo)).ToList();

        public static List<Volume> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Volume(mo)).ToList();

        public static List<Volume> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Volume(mo)).ToList();

        public static Volume CreateInstance() => new Volume(CreateInstance(ClassName));

        public uint DeleteObject(bool RunAsJob, out ManagementPath CreatedStorageJob, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("DeleteObject");
                inParams["RunAsJob"] = RunAsJob;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DeleteObject", inParams, null);
                CreatedStorageJob = null;
                if (outParams.Properties["CreatedStorageJob"] != null)
                {
                    CreatedStorageJob = new ManagementPath(outParams.Properties["CreatedStorageJob"].Value as string);
                }
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                CreatedStorageJob = null;
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint Diagnose(out ManagementBaseObject[] DiagnoseResults, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Diagnose", inParams, null);
                DiagnoseResults = (ManagementBaseObject[])outParams.Properties["DiagnoseResults"].Value;
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                DiagnoseResults = null;
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint Flush()
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Flush", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint Format(uint AllocationUnitSize, bool Compress, bool DisableHeatGathering, string FileSystem, string FileSystemLabel, bool Force, bool Full, bool IsDAX, bool RunAsJob, bool SetIntegrityStreams, bool ShortFileNameSupport, bool UseLargeFRS, out ManagementPath CreatedStorageJob, out ManagementBaseObject ExtendedStatus, out ManagementBaseObject FormattedVolume)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("Format");
                inParams[nameof(AllocationUnitSize)] = AllocationUnitSize;
                inParams["Compress"] = Compress;
                inParams["DisableHeatGathering"] = DisableHeatGathering;
                inParams[nameof(FileSystem)] = FileSystem;
                inParams[nameof(FileSystemLabel)] = FileSystemLabel;
                inParams["Force"] = Force;
                inParams["Full"] = Full;
                inParams["IsDAX"] = IsDAX;
                inParams["RunAsJob"] = RunAsJob;
                inParams["SetIntegrityStreams"] = SetIntegrityStreams;
                inParams["ShortFileNameSupport"] = ShortFileNameSupport;
                inParams["UseLargeFRS"] = UseLargeFRS;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Format", inParams, null);
                CreatedStorageJob = null;
                if (outParams.Properties["CreatedStorageJob"] != null)
                {
                    CreatedStorageJob = new ManagementPath(outParams.Properties["CreatedStorageJob"].Value as string);
                }
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                FormattedVolume = (ManagementBaseObject)outParams.Properties["FormattedVolume"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                CreatedStorageJob = null;
                ExtendedStatus = null;
                FormattedVolume = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetActions(out ManagementBaseObject[] ActionResults, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetActions", inParams, null);
                ActionResults = (ManagementBaseObject[])outParams.Properties["ActionResults"].Value;
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ActionResults = null;
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetAttributes(out bool VolumeScrubEnabled)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetAttributes", inParams, null);
                VolumeScrubEnabled = Convert.ToBoolean(outParams.Properties["VolumeScrubEnabled"].Value);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                VolumeScrubEnabled = Convert.ToBoolean(0);
                return Convert.ToUInt32(0);
            }
        }

        public uint GetCorruptionCount(out uint CorruptionCount, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetCorruptionCount", inParams, null);
                CorruptionCount = Convert.ToUInt32(outParams.Properties["CorruptionCount"].Value);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                CorruptionCount = Convert.ToUInt32(0);
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetDedupProperties(out ManagementBaseObject DedupProperties, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetDedupProperties", inParams, null);
                DedupProperties = (ManagementBaseObject)outParams.Properties["DedupProperties"].Value;
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                DedupProperties = null;
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetSupportedClusterSizes(string FileSystem, out ManagementBaseObject ExtendedStatus, out uint[] SupportedClusterSizes)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("GetSupportedClusterSizes");
                inParams[nameof(FileSystem)] = FileSystem;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetSupportedClusterSizes", inParams, null);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                SupportedClusterSizes = (uint[])outParams.Properties["SupportedClusterSizes"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ExtendedStatus = null;
                SupportedClusterSizes = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetSupportedFileSystems(out ManagementBaseObject ExtendedStatus, out string[] SupportedFileSystems)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetSupportedFileSystems", inParams, null);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                SupportedFileSystems = (string[])outParams.Properties["SupportedFileSystems"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ExtendedStatus = null;
                SupportedFileSystems = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint Optimize(bool Analyze, bool Defrag, bool NormalPriority, bool ReTrim, bool RunAsJob, bool SlabConsolidate, bool TierOptimize, out ManagementPath CreatedStorageJob, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("Optimize");
                inParams["Analyze"] = Analyze;
                inParams["Defrag"] = Defrag;
                inParams["NormalPriority"] = NormalPriority;
                inParams["ReTrim"] = ReTrim;
                inParams["RunAsJob"] = RunAsJob;
                inParams["SlabConsolidate"] = SlabConsolidate;
                inParams["TierOptimize"] = TierOptimize;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Optimize", inParams, null);
                CreatedStorageJob = null;
                if (outParams.Properties["CreatedStorageJob"] != null)
                {
                    CreatedStorageJob = new ManagementPath(outParams.Properties["CreatedStorageJob"].Value as string);
                }
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                CreatedStorageJob = null;
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint Repair(bool OfflineScanAndFix, bool RunAsJob, bool Scan, bool SpotFix, out ManagementPath CreatedStorageJob, out ManagementBaseObject ExtendedStatus, out uint Output)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("Repair");
                inParams["OfflineScanAndFix"] = OfflineScanAndFix;
                inParams["RunAsJob"] = RunAsJob;
                inParams["Scan"] = Scan;
                inParams["SpotFix"] = SpotFix;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Repair", inParams, null);
                CreatedStorageJob = null;
                if (outParams.Properties["CreatedStorageJob"] != null)
                {
                    CreatedStorageJob = new ManagementPath(outParams.Properties["CreatedStorageJob"].Value as string);
                }
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                Output = Convert.ToUInt32(outParams.Properties["Output"].Value);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                CreatedStorageJob = null;
                ExtendedStatus = null;
                Output = Convert.ToUInt32(0);
                return Convert.ToUInt32(0);
            }
        }

        public uint Resize(bool RunAsJob, ulong Size, out ManagementPath CreatedStorageJob, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("Resize");
                inParams["RunAsJob"] = RunAsJob;
                inParams[nameof(Size)] = Size;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Resize", inParams, null);
                CreatedStorageJob = null;
                if (outParams.Properties["CreatedStorageJob"] != null)
                {
                    CreatedStorageJob = new ManagementPath(outParams.Properties["CreatedStorageJob"].Value as string);
                }
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                CreatedStorageJob = null;
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint SetAttributes(bool EnableVolumeScrub, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("SetAttributes");
                inParams["EnableVolumeScrub"] = EnableVolumeScrub;
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

        public uint SetDedupMode(uint DedupMode, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("SetDedupMode");
                inParams[nameof(DedupMode)] = DedupMode;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetDedupMode", inParams, null);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint SetFileSystemLabel(string FileSystemLabel, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("SetFileSystemLabel");
                inParams[nameof(FileSystemLabel)] = FileSystemLabel;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetFileSystemLabel", inParams, null);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public enum DedupModeValues
        {
            Disabled = 0,
            GeneralPurpose = 1,
            HyperV = 2,
            Backup = 3,
            NotAvailable = 4,
            NULL_ENUM_VALUE = 5,
        }

        public enum DriveTypeValues
        {
            Unknown0 = 0,
            Invalid_Root_Path = 1,
            Removable = 2,
            Fixed = 3,
            Remote = 4,
            CD_ROM = 5,
            RAM_Disk = 6,
            NULL_ENUM_VALUE = 7,
        }

        public enum HealthStatusValues
        {
            Healthy = 0,
            Warning = 1,
            Unhealthy = 2,
            NULL_ENUM_VALUE = 3,
        }
    }
}
