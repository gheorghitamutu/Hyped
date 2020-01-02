using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Microsoft.Windows.Storage.MSFT
{
    public class Disk : MsftBase
    {
        public static string ClassName => $"MSFT_{nameof(Disk)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public Disk() : base(ClassName) { }

        public Disk(string keyObjectId) : base(keyObjectId, ClassName) { }

        public Disk(ManagementScope mgmtScope, string keyObjectId) : base(mgmtScope, keyObjectId, ClassName) { }

        public Disk(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public Disk(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public Disk(ManagementPath path) : base(path, ClassName) { }

        public Disk(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public Disk(ManagementObject theObject) : base(theObject, ClassName) { }

        public Disk(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string AdapterSerialNumber => (string)LateBoundObject[nameof(AdapterSerialNumber)];
        
        /*
         * The amount of space currently used on the disk.
         */
        public ulong AllocatedSize
        {
            get
            {
                if (LateBoundObject[nameof(AllocatedSize)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(AllocatedSize)];
            }
        }

        /*
         * This property indicates that the computer is configured to start off of this disk.
         * On computers with BIOS firmware, this is the first disk that the firmware detects during startup.
         * On computers that use EFI firmware, this is the disk that contains the EFI System Partition (ESP).
         * If there are no disks or multiple disks with an ESP partition, this flag is not set for any disk.
         */
        public bool BootFromDisk
        {
            get
            {
                if (LateBoundObject[nameof(BootFromDisk)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(BootFromDisk)];
            }
        }

        /*
         * Denotes the I/O bus type used by this disk.
         */
        public BusTypeValues BusType
        {
            get
            {
                if (LateBoundObject[nameof(BusType)] == null)
                {
                    return (BusTypeValues)Convert.ToInt32(20);
                }
                return (BusTypeValues)Convert.ToInt32(LateBoundObject[nameof(BusType)]);
            }
        }

        /*
         * A string representation of the disk's firmware version.
         */
        public string FirmwareVersion => (string)LateBoundObject[nameof(FirmwareVersion)];

        /*
         * FriendlyName is a user-friendly, display-oriented string to identify the disk.
         */
        public string FriendlyName => (string)LateBoundObject[nameof(FriendlyName)];

        /*
         * The GPT guid of the disk.
         * This property is only valid on GPT disks and will be NULL for all other disk types.
         */
        public string Guid => (string)LateBoundObject[nameof(Guid)];

        /*
         * The health status of the Volume.
         * 0 - 'Healthy': The disk is functioning normally.
         * 1 - 'Warning': The disk is still functioning, but has detected errors or issues that require administrator intervention.
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
         * This property indicates that the computer has booted off of this disk.
         */
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
         * If IsClustered is TRUE, this disk is used in a clustered environment.
         */
        public bool IsClustered
        {
            get
            {
                if (LateBoundObject[nameof(IsClustered)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsClustered)];
            }
        }

        /*
         * If IsHighlyAvailable is TRUE, the disk is highly available.
         */
        public bool IsHighlyAvailable
        {
            get
            {
                if (LateBoundObject[nameof(IsHighlyAvailable)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsHighlyAvailable)];
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
         * If IsScaleOut is TRUE, the disk is scaled out.
         */
        public bool IsScaleOut
        {
            get
            {
                if (LateBoundObject[nameof(IsScaleOut)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsScaleOut)];
            }
        }

        /*
         * If IsSystem is TRUE, this disk contains the system partition.
         */
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
         * This field indicates the largest contiguous block of free space on the disk.
         * This is also the largest size of a partition which can be created on the disk.
         */
        public ulong LargestFreeExtent
        {
            get
            {
                if (LateBoundObject[nameof(LargestFreeExtent)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(LargestFreeExtent)];
            }
        }

        /*
         * Location contains the PnP location path of the disk.
         * The format of this string depends on the bus type.
         * If the bus type is SCSI, SAS, or PCI RAID, the format is <AdapterPnpLocationPath>#<BusType>(P<PathId>T<TargetId>L<LunId>).
         * If the bus type is IDE, ATA, PATA, or SATA, the format is <AdapterPnpLocationPath>#<BusType>(C<PathId>T<TargetId>L<LunId>).
         * For example, a SCSI location may look like: PCIROOT(0)#PCI(1C00)#PCI(0000)#SCSI(P00T01L01).
         * Note: For Hyper-V and VHD images, this member is NULL because the virtual controller does not return the location path.
         */
        public string Location => (string)LateBoundObject[nameof(Location)];
        
        /*
         * This field indicates the logical sector size of the disk in bytes.
         * For example: a 4K native disk will report 4096, while a 512 emulated disk will report 512.
         */
        public uint LogicalSectorSize
        {
            get
            {
                if (LateBoundObject[nameof(LogicalSectorSize)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(LogicalSectorSize)];
            }
        }

        /*
         * A string representation of the disk's hardware manufacturer.
         */
        public string Manufacturer => (string)LateBoundObject[nameof(Manufacturer)];

        /*
         * A string representation of the disk's model.
         */
        public string Model => (string)LateBoundObject[nameof(Model)];

        /*
         * The operating system's number for the disk.
         * Disk 0 is typically the boot device.
         * Disk numbers may not necessarily remain the same across reboots.
         */
        public uint Number
        {
            get
            {
                if (LateBoundObject[nameof(Number)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(Number)];
            }
        }

        public uint NumberOfPartitions
        {
            get
            {
                if (LateBoundObject[nameof(NumberOfPartitions)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(NumberOfPartitions)];
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
         * If IsOffline is TRUE, this property informs the user of the specific reason for the disk being offline. 
         * 1 - 'Policy': The user requested the disk to be offline. 
         * 2 - 'Redundant Path': The disk is used for multi-path I/O. 
         * 3 - 'Snapshot': The disk is a snapshot disk. 
         * 4 - 'Collision': There was a signature or identifier collision with another disk. 
         * 5 - 'Resource Exhaustion': There were insufficient resources to bring the disk online. 
         * 6 - 'Critical Write Failures': There were critical write failures on the disk. 
         * 7 - 'Data Integrity Scan Required': A data integrity scan is required.
         */
        public OfflineReasonValues OfflineReason
        {
            get
            {
                if (LateBoundObject[nameof(OfflineReason)] == null)
                {
                    return (OfflineReasonValues)Convert.ToInt32(0);
                }
                return (OfflineReasonValues)Convert.ToInt32(LateBoundObject[nameof(OfflineReason)]);
            }
        }

        /*
         * An array of values that denote the current operational status of the volume.
         * 0 - 'Unknown': The operational status is unknown.
         * 1 - 'Other': A vendor-specific OperationalStatus has been specified by setting the OtherOperationalStatusDescription property.
         * 2 - 'OK': The disk is responding to commands and is in a normal operating state.
         * 3 - 'Degraded': The disk is responding to commands, but is not running in an optimal operating state.
         * 4 - 'Stressed': The disk is functioning, but needs attention. For example, the disk might be overloaded or overheated.
         * 5 - 'Predictive Failure': The disk is functioning, but a failure is likely to occur in the near future.
         * 6 - 'Error': An error has occurred.
         * 7 - 'Non-Recoverable Error': A non-recoverable error has occurred.
         * 8 - 'Starting': The disk is in the process of starting.
         * 9 - 'Stopping': The disk is in the process of stopping.
         * 10 - Stopped': The disk was stopped or shut down in a clean and orderly fashion.
         * 11 - 'In Service': The disk is being configured, maintained, cleaned, or otherwise administered.
         * 12 - 'No Contact': The storage provider has knowledge of the disk, but has never been able to establish communication with it.
         * 13 - 'Lost Communication': The storage provider has knowledge of the disk and has contacted it successfully in the past, but the disk is currently unreachable.
         * 14 - 'Aborted': Similar to Stopped, except that the disk stopped abruptly and may require configuration or maintenance.
         * 15 - 'Dormant': The disk is reachable, but it is inactive.
         * 16 - 'Supporting Entity in Error': This status value does not necessarily indicate trouble with the disk, but it does indicate that another device or connection that the disk depends on may need attention.
         * 17 - 'Completed': The disk has completed an operation. This status value should be combined with OK, Error, or Degraded, depending on the outcome of the operation.
         * 0xD010 - 'Online': In Windows-based storage subsystems, this indicates that the object is online.
         * 0xD011 - 'Not Ready': In Windows-based storage subsystems, this indicates that the object is not ready.
         * 0xD012 - 'No Media': In Windows-based storage subsystems, this indicates that the object has no media present.
         * 0xD013 - 'Offline': In Windows-based storage subsystems, this indicates that the object is offline.
         * 0xD014 - 'Failed': In Windows-based storage subsystems, this indicates that the object is in a failed state.
         */
        public ushort[] OperationalStatus => (ushort[])LateBoundObject[nameof(OperationalStatus)];
        
        public PartitionStyleValues PartitionStyle
        {
            get
            {
                if (LateBoundObject[nameof(PartitionStyle)] == null)
                {
                    return (PartitionStyleValues)Convert.ToInt32(3);
                }
                return (PartitionStyleValues)Convert.ToInt32(LateBoundObject[nameof(PartitionStyle)]);
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
         * PassThroughNamespace is the WBEM namespace that contains the proprietary storage provider classes.
         */
        public string PassThroughNamespace => (string)LateBoundObject[nameof(PassThroughNamespace)];

        /*
         * "PassThroughServer is the name or address of the computer system hosting the proprietary storage provider classes.
         */
        public string PassThroughServer => (string)LateBoundObject[nameof(PassThroughServer)];

        /*
         * Path can be used to open an operating system handle to the disk device.
         */
        public string Path0 => (string)LateBoundObject["Path"];


        /*
         * This field indicates the physical sector size of the disk in bytes.
         * For example: both 4K native disks and 512 emulated disks will report 4096.
         */
        public uint PhysicalSectorSize
        {
            get
            {
                if (LateBoundObject[nameof(PhysicalSectorSize)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(PhysicalSectorSize)];
            }
        }

        /*
         * Denotes the provisioning type of the disk device.
         * 1 - 'Thin' means that the storage for the disk is allocated on-demand.
         * 2 - 'Fixed' means that the storage is allocated up front.
         */
        public ProvisioningTypeValues ProvisioningType
        {
            get
            {
                if (LateBoundObject[nameof(ProvisioningType)] == null)
                {
                    return (ProvisioningTypeValues)Convert.ToInt32(3);
                }
                return (ProvisioningTypeValues)Convert.ToInt32(LateBoundObject[nameof(ProvisioningType)]);
            }
        }

        /*
         * A string representation of the disk's serial number.
         */
        public string SerialNumber => (string)LateBoundObject[nameof(SerialNumber)];

        /*
         * The MBR signature of the disk.
         * This property is only valid on MBR disks and will be NULL for all other disk types.
         */
        public uint Signature
        {
            get
            {
                if (LateBoundObject[nameof(Signature)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(Signature)];
            }
        }

        /*
         * The total size of the disk, measured in bytes.
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
         * UniqueId of a disk contains the VPD Page 0x83 information that uniquely identifies this disk.
         * The following types are accepted (in order of precedence):
         * 8 - SCSI Name String
         * 3 - FCPH Name
         * 2 - EUI64
         * 1 - Vendor Id
         * 0 - Vendor Specific
         * If the disk is an exposed VirtualDisk, UniqueId is used map the association between the two objects.
         */
        public string UniqueId => (string)LateBoundObject[nameof(UniqueId)];

        /*
         * UniqueIdFormat informs the user what VPD Page 0x83 descriptor type was used to populate the UniqueId field.
         */
        public UniqueIdFormatValues UniqueIdFormat
        {
            get
            {
                if (LateBoundObject[nameof(UniqueIdFormat)] == null)
                {
                    return (UniqueIdFormatValues)Convert.ToInt32(9);
                }
                return (UniqueIdFormatValues)Convert.ToInt32(LateBoundObject[nameof(UniqueIdFormat)]);
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<Disk> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new Disk(mo)).ToList();

        public new static List<Disk> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new Disk(mo)).ToList();

        public static List<Disk> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Disk(mo)).ToList();

        public static List<Disk> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Disk(mo)).ToList();

        public static List<Disk> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new Disk(mo)).ToList();

        public static List<Disk> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new Disk(mo)).ToList();

        public static List<Disk> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Disk(mo)).ToList();

        public static List<Disk> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Disk(mo)).ToList();

        public static Disk CreateInstance() => new Disk(CreateInstance(ClassName));

        public uint Clear(bool RemoveData, bool RemoveOEM, bool RunAsJob, bool Sanitize, bool ZeroOutEntireDisk, out ManagementPath CreatedStorageJob, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("Clear");
                inParams["RemoveData"] = RemoveData;
                inParams["RemoveOEM"] = RemoveOEM;
                inParams["RunAsJob"] = RunAsJob;
                inParams["Sanitize"] = Sanitize;
                inParams["ZeroOutEntireDisk"] = ZeroOutEntireDisk;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Clear", inParams, null);
                CreatedStorageJob = null;
                if (outParams.Properties["CreatedStorageJob"] != null)
                {
                    CreatedStorageJob = new ManagementPath(outParams.Properties["CreatedStorageJob"].ToString());
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

        public uint ConvertStyle(ushort PartitionStyle, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ConvertStyle");
                inParams[nameof(PartitionStyle)] = PartitionStyle;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ConvertStyle", inParams, null);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint CreatePartition(uint Alignment, bool AssignDriveLetter, char DriveLetter, string GptType, bool IsActive, bool IsHidden, ushort MbrType, ulong Offset, ulong Size, bool UseMaximumSize, out ManagementBaseObject CreatedPartition, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("CreatePartition");
                inParams["Alignment"] = Alignment;
                inParams["AssignDriveLetter"] = AssignDriveLetter;
                if (AssignDriveLetter)
                    inParams["DriveLetter"] = DriveLetter;
                inParams["GptType"] = GptType;
                inParams["IsActive"] = IsActive;
                inParams["IsHidden"] = IsHidden;
                if (string.IsNullOrEmpty(GptType))
                    inParams["MbrType"] = MbrType;
                if (string.IsNullOrEmpty(GptType))
                    inParams["Offset"] = Offset;
                if (UseMaximumSize == false)
                    inParams[nameof(Size)] = Size;
                inParams["UseMaximumSize"] = UseMaximumSize;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("CreatePartition", inParams, null);
                CreatedPartition = (ManagementBaseObject)outParams.Properties["CreatedPartition"].Value;
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                CreatedPartition = null;
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint CreateVolume(string AccessPath, uint AllocationUnitSize, ushort FileSystem, string FriendlyName, bool RunAsJob, out ManagementPath CreatedStorageJob, out ManagementBaseObject CreatedVolume, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("CreateVolume");
                inParams["AccessPath"] = AccessPath;
                inParams["AllocationUnitSize"] = AllocationUnitSize;
                inParams["FileSystem"] = FileSystem;
                inParams[nameof(FriendlyName)] = FriendlyName;
                inParams["RunAsJob"] = RunAsJob;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("CreateVolume", inParams, null);
                CreatedStorageJob = null;
                if (outParams.Properties["CreatedStorageJob"] != null)
                {
                    CreatedStorageJob = new ManagementPath(outParams.Properties["CreatedStorageJob"].ToString());
                }
                CreatedVolume = (ManagementBaseObject)outParams.Properties["CreatedVolume"].Value;
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                CreatedStorageJob = null;
                CreatedVolume = null;
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint DisableHighAvailability(bool RunAsJob, out ManagementPath CreatedStorageJob, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("DisableHighAvailability");
                inParams["RunAsJob"] = RunAsJob;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DisableHighAvailability", inParams, null);
                CreatedStorageJob = null;
                if (outParams.Properties["CreatedStorageJob"] != null)
                {
                    CreatedStorageJob = new ManagementPath(outParams.Properties["CreatedStorageJob"].ToString());
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

        public uint EnableHighAvailability(bool RunAsJob, bool ScaleOut, out ManagementPath CreatedStorageJob, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("EnableHighAvailability");
                inParams["RunAsJob"] = RunAsJob;
                inParams["ScaleOut"] = ScaleOut;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("EnableHighAvailability", inParams, null);
                CreatedStorageJob = null;
                if (outParams.Properties["CreatedStorageJob"] != null)
                {
                    CreatedStorageJob = new ManagementPath(outParams.Properties["CreatedStorageJob"].ToString());
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

        public uint Initialize(ushort PartitionStyle, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("Initialize");
                inParams[nameof(PartitionStyle)] = PartitionStyle;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Initialize", inParams, null);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ExtendedStatus = null;
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

        public uint Refresh(out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Refresh", inParams, null);
                ExtendedStatus = (ManagementBaseObject)outParams.Properties["ExtendedStatus"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ExtendedStatus = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint SetAttributes(string Guid, bool IsReadOnly, uint Signature, out ManagementBaseObject ExtendedStatus)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("SetAttributes");
                inParams[nameof(Guid)] = Guid;
                inParams[nameof(IsReadOnly)] = IsReadOnly;
                inParams[nameof(Signature)] = Signature;
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

        public enum BusTypeValues
        {
            Unknown0 = 0,
            SCSI = 1,
            ATAPI = 2,
            ATA = 3,
            Val_1394 = 4,
            SSA = 5,
            Fibre_Channel = 6,
            USB = 7,
            RAID = 8,
            ISCSI = 9,
            SAS = 10,
            SATA = 11,
            SD = 12,
            MMC = 13,
            Virtual = 14,
            File_Backed_Virtual = 15,
            Storage_Spaces = 16,
            NVMe = 17,
            SCM = 18,
            UFS = 19,
            NULL_ENUM_VALUE = 20,
        }

        public enum HealthStatusValues
        {
            Healthy = 0,
            Warning = 1,
            Unhealthy = 2,
            NULL_ENUM_VALUE = 3,
        }

        public enum OfflineReasonValues
        {
            Policy = 1,
            Redundant_Path = 2,
            Snapshot = 3,
            Collision = 4,
            Resource_Exhaustion = 5,
            Critical_Write_Failures = 6,
            Data_Integrity_Scan_Required = 7,
            NULL_ENUM_VALUE = 0,
        }

        public enum PartitionStyleValues
        {
            Unknown0 = 0,
            MBR = 1,
            GPT = 2,
            NULL_ENUM_VALUE = 3,
        }

        public enum ProvisioningTypeValues
        {
            Unknown0 = 0,
            Thin = 1,
            Fixed = 2,
            NULL_ENUM_VALUE = 3,
        }

        public enum UniqueIdFormatValues
        {
            Vendor_Specific = 0,
            Vendor_Id = 1,
            EUI64 = 2,
            FCPH_Name = 3,
            SCSI_Name_String = 8,
            NULL_ENUM_VALUE = 9,
        }
    }
}
