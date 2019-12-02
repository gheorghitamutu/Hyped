using System.Linq;
using System.Management;
using Viridian.Exceptions;
using Viridian.Scopes;

namespace Viridian.WindowsStorageManagement.MSFT
{
    public sealed class Disk
    {
        private ManagementObject MSFT_Disk = null;

        public enum DiskBusType : ushort
        {
            Unknown = 0,
            SCSI = 1,
            ATAPI = 2,
            ATA = 3,
            _1394 = 4,
            SSA = 5,
            FibreChannel = 6,
            USB = 7,
            RAID = 8,
            iSCSI = 9,
            SAS = 10,
            SATA = 11,
            SD = 12,
            MMC = 13,
            Virtual = 14,
            FileBackedVirtual = 15,
            StorageSpaces = 16,
            NVMe = 17
        }
        public enum DiskHealthStatus : ushort
        {
            Healthy = 0,
            Warning = 1,
            Unhealthy = 2
        }
        public enum DiskOfflineReason : ushort
        {
            Policy = 1,
            RedundantPath = 2,
            Snapshot = 3,
            Collision = 4,
            ResourceExhaustion = 5,
            CriticalWriteFailures = 6,
            DataIntegrityScanRequired = 7,
        }
        public enum DiskOperationalStatus : ushort
        {
            Unknown = 0,
            Other = 1,
            OK = 2,
            Degraded = 3,
            Stressed = 4,
            PredictiveFailure = 5,
            Error = 6,
            NonRecoverableError = 7,
            Starting = 8,
            Stopping = 9,
            Stopped = 10,
            InService = 11,
            NoContact = 12,
            LostCommunication = 13,
            Aborted = 14,
            Dormant = 15,
            SupportingEntityInError = 16,
            Completed = 17,
            Online = 0xD010,
            NotReady = 0xD011,
            NoMedia = 0xD012,
            Offline = 0xD013,
            Failed = 0xD014
        }
        public enum DiskPartitionStyle : ushort
        {
            Unknown = 0,
            MBR = 1,
            GPT = 2,
        }
        public enum DiskProvisioningType : ushort
        {
            Unknown = 0,
            Thin = 1,
            Fixed = 2
        }

        public Disk(string Location)
        {
            using (var mos = new ManagementObjectSearcher(Scope.Storage.SpecificScope, new ObjectQuery("SELECT * FROM MSFT_Disk")))
                MSFT_Disk = mos.Get().Cast<ManagementObject>().Where((c) => c[nameof(Location)]?.ToString() == Location).First();
        }

        public ManagementObject MSFTDisk => MSFT_Disk;

        #region MsftProperties

        string Path => MSFT_Disk[nameof(Path)].ToString();
        string Location => MSFT_Disk[nameof(Location)].ToString();
        string FriendlyName => MSFT_Disk[nameof(FriendlyName)].ToString();
        string UniqueId => MSFT_Disk[nameof(UniqueId)].ToString();
        ushort UniqueIdFormat => (ushort)MSFT_Disk[nameof(UniqueIdFormat)];
        uint Number => (uint)MSFT_Disk[nameof(Number)];
        string SerialNumber => MSFT_Disk[nameof(SerialNumber)].ToString();
        string FirmwareVersion => MSFT_Disk[nameof(FirmwareVersion)].ToString();
        string Manufacturer => MSFT_Disk[nameof(Manufacturer)].ToString();
        string Model => MSFT_Disk[nameof(Model)].ToString();
        ulong Size => (ulong)MSFT_Disk[nameof(Size)];
        ulong AllocatedSize => (ulong)MSFT_Disk[nameof(AllocatedSize)];
        uint LogicalSectorSize => (uint)MSFT_Disk[nameof(LogicalSectorSize)];
        uint PhysicalSectorSize => (uint)MSFT_Disk[nameof(PhysicalSectorSize)];
        ulong LargestFreeExtent => (ulong)MSFT_Disk[nameof(LargestFreeExtent)];
        uint NumberOfPartitions => (uint)MSFT_Disk[nameof(NumberOfPartitions)];
        DiskProvisioningType ProvisioningType => (DiskProvisioningType)(ushort)MSFT_Disk[nameof(ProvisioningType)];
        DiskOperationalStatus OperationalStatus => (DiskOperationalStatus)(ushort)MSFT_Disk[nameof(OperationalStatus)];
        ushort HealthStatus => (ushort)MSFT_Disk[nameof(HealthStatus)];
        DiskBusType BusType => (DiskBusType)(ushort)MSFT_Disk[nameof(BusType)];
        DiskPartitionStyle PartitionStyle => (DiskPartitionStyle)(ushort)MSFT_Disk[nameof(PartitionStyle)];
        uint Signature => (uint)MSFT_Disk[nameof(Signature)];
        string Guid => MSFT_Disk[nameof(Guid)].ToString();
        bool IsOffline => (bool)MSFT_Disk[nameof(IsOffline)];
        DiskOfflineReason OfflineReason => (DiskOfflineReason)(ushort)MSFT_Disk[nameof(OfflineReason)];
        bool IsReadOnly => (bool)MSFT_Disk[nameof(IsReadOnly)];
        bool IsSystem => (bool)MSFT_Disk[nameof(IsSystem)];
        bool IsClustered => (bool)MSFT_Disk[nameof(IsClustered)];
        bool IsBoot => (bool)MSFT_Disk[nameof(IsBoot)];
        bool BootFromDisk => (bool)MSFT_Disk[nameof(BootFromDisk)];

        #endregion

        public string Clear(bool RemoveData, bool RemoveOEM, bool ZeroOutEntireDisk)
        {
            using (var ip = MSFT_Disk.GetMethodParameters(nameof(Clear)))
            {
                ip[nameof(RemoveData)] = RemoveData;
                ip[nameof(RemoveOEM)] = RemoveOEM;
                ip[nameof(ZeroOutEntireDisk)] = ZeroOutEntireDisk;

                using (var op = MSFT_Disk.InvokeMethod(nameof(Clear), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                    return op["ExtendedStatus"] as string;
                }
            }
        }

        public string ConvertStyle(DiskPartitionStyle PartitionStyle)
        {
            using (var ip = MSFT_Disk.GetMethodParameters(nameof(ConvertStyle)))
            {
                ip[nameof(PartitionStyle)] = PartitionStyle;

                using (var op = MSFT_Disk.InvokeMethod(nameof(ConvertStyle), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                    return op["ExtendedStatus"] as string;
                }
            }
        }

        public ManagementObject CreatePartition(ulong Size, bool UseMaximumSize, ulong Offset, uint Alignment, char DriveLetter, bool AssignDriveLetter, Partition.PartitionMBRType MbrType, string GptType, bool IsHidden, bool IsActive)
        {
            using (var ip = MSFT_Disk.GetMethodParameters(nameof(CreatePartition)))
            {
                if (UseMaximumSize == false) 
                    ip[nameof(Size)] = Size;
                ip[nameof(UseMaximumSize)] = UseMaximumSize;
                if (MbrType != Partition.PartitionMBRType.None)
                    ip[nameof(Offset)] = Offset;
                ip[nameof(Alignment)] = Alignment;
                if (AssignDriveLetter)
                    ip[nameof(DriveLetter)] = DriveLetter;
                ip[nameof(AssignDriveLetter)] = AssignDriveLetter;
                if (MbrType != Partition.PartitionMBRType.None)
                    ip[nameof(MbrType)] = MbrType;
                if (MbrType == Partition.PartitionMBRType.None)
                    ip[nameof(GptType)] = GptType;
                ip[nameof(IsHidden)] = IsHidden;
                if (MbrType != Partition.PartitionMBRType.None)
                    ip[nameof(IsActive)] = IsActive;

                using (var op = MSFT_Disk.InvokeMethod(nameof(CreatePartition), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);
                    
                    return new ManagementObject(Properties.Environment.Default.Storage, ((ManagementBaseObject)op["CreatedPartition"])["__PATH"] as string, null);
                }
            }
        }

        public string Initialize(DiskPartitionStyle PartitionStyle)
        {
            using (var ip = MSFT_Disk.GetMethodParameters(nameof(Initialize)))
            {
                ip[nameof(PartitionStyle)] = PartitionStyle;

                using (var op = MSFT_Disk.InvokeMethod(nameof(Initialize), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                    return op["ExtendedStatus"] as string;
                }
            }
        }

        public string Offline()
        {
            using (var ip = MSFT_Disk.GetMethodParameters(nameof(Offline)))
            using (var op = MSFT_Disk.InvokeMethod(nameof(Offline), ip, null))
            {
                Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                return op["ExtendedStatus"] as string;
            }
        }

        public string Online()
        {
            using (var ip = MSFT_Disk.GetMethodParameters(nameof(Online)))
            using (var op = MSFT_Disk.InvokeMethod(nameof(Online), ip, null))
            {
                Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                return op["ExtendedStatus"] as string;
            }
        }

        public string Refresh()
        {
            using (var ip = MSFT_Disk.GetMethodParameters(nameof(Refresh)))
            using (var op = MSFT_Disk.InvokeMethod(nameof(Refresh), ip, null))
            {
                Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                return op["ExtendedStatus"] as string;
            }
        }

        public string SetAttributes(bool IsReadOnly, uint Signature, string Guid)
        {
            using (var ip = MSFT_Disk.GetMethodParameters(nameof(Refresh)))
            {
                ip[nameof(IsReadOnly)] = IsReadOnly;
                ip[nameof(Signature)] = Signature;
                ip[nameof(Guid)] = Guid ?? throw new ViridianException($"{nameof(Guid)} is null!");

                using (var op = MSFT_Disk.InvokeMethod(nameof(Refresh), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                    return op["ExtendedStatus"] as string;
                }
            }
        }

        ~Disk()
        {
            MSFT_Disk.Dispose();
        }
    }
}
