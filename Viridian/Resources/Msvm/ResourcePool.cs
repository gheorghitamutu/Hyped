using System;
using System.Linq;
using System.Management;
using Viridian.Utilities;

namespace Viridian.Resources.Msvm
{
    public sealed class ResourcePool
    {
        private ManagementObject Msvm_ResourcePool = null;
        private ManagementScope scope = null;

        public enum CommunicationStatusRP : ushort
        {
            Unknown = 0,
            NotAvailable = 1,
            CommunicationOK = 2,
            LostCommunication = 3,
            NoContact = 4
        }
        public enum DetailedStatusRP : ushort
        {
            NotAvailable = 0,
            NoAdditionalInformation = 1,
            Stressed = 2,
            PredictiveFailure = 3,
            NonRecoverableError = 4,
            SupportingEntityInError = 5
        }
        public enum OperatingStatusRP : ushort
        {
            Unknown = 0,
            NotAvailable = 1,
            Servicing = 2,
            Starting = 3,
            Stopping = 4,
            Stopped = 5,
            Aborted = 6,
            Dormant = 7,
            Completed = 8,
            Migrating = 9,
            Emigrating = 10,
            Immigrating = 11,
            Snapshotting = 12,
            ShuttingDown = 13,
            InTest = 14,
            Transitioning = 15,
            InService = 16
        }

        public enum OperationalStatusRP : ushort
        {
            OK = 2,
            Degraded = 3,
            NonRecoverableError = 7,
            NoContact = 12,
            LostCommunication = 13,
            ProtocolMismatch = 32775,
            InsufficientThroughput = 32788
        }
        public enum PrimaryStatusRP : ushort
        {
            Unknown = 0,
            OK = 1,
            Degraded = 2,
            Error = 3
        }

        public class ResourceTypeInfo
        {
            private ResourceTypeInfo(string DisplayName, string ResourceType, string ResourceSubType)
            {
                this.DisplayName = DisplayName;
                this.ResourceType = ResourceType;
                ResourcePoolID = ResourceType;
                this.ResourceSubType = ResourceSubType;
            }

            public string DisplayName { get; set; }
            public string ResourceType { get; set; }
            public string ResourcePoolID { get; set; }
            public string ResourceSubType { get; set; }

            public static ResourceTypeInfo Other => new ResourceTypeInfo(nameof(Other), "1", "");
            public static ResourceTypeInfo ComputerSystem => new ResourceTypeInfo(nameof(ComputerSystem), "2", ""); // TODO: subtype?
            public static ResourceTypeInfo Processor => new ResourceTypeInfo(nameof(Processor), "3", "Microsoft:Hyper-V:Processor");
            public static ResourceTypeInfo Memory => new ResourceTypeInfo(nameof(Memory), "4", "Microsoft:Hyper-V:Memory");
            public static ResourceTypeInfo IDEController => new ResourceTypeInfo(nameof(IDEController), "5", ""); // TODO: subtype?
            public static ResourceTypeInfo SyntheticSCSIController => new ResourceTypeInfo(nameof(SyntheticSCSIController), "6", "Microsoft:Hyper-V:Synthetic SCSI Controller");
            public static ResourceTypeInfo SyntheticFiberChannelPort => new ResourceTypeInfo(nameof(SyntheticFiberChannelPort), "7", "Microsoft:Hyper-V:Synthetic FiberChannel Port");
            public static ResourceTypeInfo ISCSIHBA => new ResourceTypeInfo(nameof(ISCSIHBA), "8", ""); // TODO: subtype?
            public static ResourceTypeInfo IBHCA => new ResourceTypeInfo(nameof(IBHCA), "9", ""); // TODO: subtype?
            public static ResourceTypeInfo EmulatedEthernetPort => new ResourceTypeInfo(nameof(EmulatedEthernetPort), "10", "Microsoft:Hyper-V:Emulated Ethernet Port");
            public static ResourceTypeInfo SyntheticEthernetPort => new ResourceTypeInfo(nameof(SyntheticEthernetPort), "10", "Microsoft:Hyper-V:Synthetic Ethernet Port");
            public static ResourceTypeInfo OtherNetworkAdapter => new ResourceTypeInfo(nameof(OtherNetworkAdapter), "11", ""); // TODO: subtype?
            public static ResourceTypeInfo IOSlot => new ResourceTypeInfo(nameof(IOSlot), "12", ""); // TODO: subtype?
            public static ResourceTypeInfo SerialController => new ResourceTypeInfo(nameof(SerialController), "13", "Microsoft:Hyper-V:Serial Controller");
            public static ResourceTypeInfo SyntheticMouse => new ResourceTypeInfo(nameof(SyntheticMouse), "13", "Microsoft:Hyper-V:Synthetic Mouse");
            public static ResourceTypeInfo SyntheticKeyboard => new ResourceTypeInfo(nameof(SyntheticKeyboard), "13", "Microsoft:Hyper-V:Synthetic Keyboard");
            public static ResourceTypeInfo FloppyDrive => new ResourceTypeInfo(nameof(FloppyDrive), "14", ""); // TODO: subtype?
            public static ResourceTypeInfo CDDrive => new ResourceTypeInfo(nameof(CDDrive), "15", ""); // TODO: subtype?
            public static ResourceTypeInfo SyntheticDVD => new ResourceTypeInfo(nameof(SyntheticDVD), "16", "Microsoft:Hyper-V:Synthetic DVD Drive");
            public static ResourceTypeInfo PhysicalDiskDrive => new ResourceTypeInfo(nameof(PhysicalDiskDrive), "17", "Microsoft:Hyper-V:Physical Disk Drive");
            public static ResourceTypeInfo SyntheticDiskDrive => new ResourceTypeInfo(nameof(SyntheticDiskDrive), "17", "Microsoft:Hyper-V:Synthetic Disk Drive");
            public static ResourceTypeInfo ParallelPort => new ResourceTypeInfo(nameof(ParallelPort), "18", ""); // TODO: subtype?
            public static ResourceTypeInfo USBController => new ResourceTypeInfo(nameof(USBController), "19", ""); // TODO: subtype?
            public static ResourceTypeInfo GraphicsController => new ResourceTypeInfo(nameof(GraphicsController), "20", ""); // TODO: subtype?
            public static ResourceTypeInfo StorageExtent => new ResourceTypeInfo(nameof(StorageExtent), "21", ""); // TODO: subtype?
            public static ResourceTypeInfo Disk => new ResourceTypeInfo(nameof(Disk), "22", ""); // TODO: subtype?
            public static ResourceTypeInfo Tape => new ResourceTypeInfo(nameof(Tape), "23", ""); // TODO: subtype?
            public static ResourceTypeInfo Synthetic3DDisplayController => new ResourceTypeInfo(nameof(Synthetic3DDisplayController), "24", "Microsoft:Hyper-V:Synthetic 3D Display Controller");
            public static ResourceTypeInfo SyntheticDisplayController => new ResourceTypeInfo(nameof(SyntheticDisplayController), "24", "Microsoft:Hyper-V:Synthetic Display Controller");
            public static ResourceTypeInfo FirewireController => new ResourceTypeInfo(nameof(FirewireController), "25", ""); // TODO: subtype?
            public static ResourceTypeInfo PartitionableUnit => new ResourceTypeInfo(nameof(PartitionableUnit), "26", ""); // TODO: subtype?
            public static ResourceTypeInfo BasePartitionableUnit => new ResourceTypeInfo(nameof(BasePartitionableUnit), "27", ""); // TODO: subtype?
            public static ResourceTypeInfo PowerSupply => new ResourceTypeInfo(nameof(PowerSupply), "28", ""); // TODO: subtype?
            public static ResourceTypeInfo CoolingDevice => new ResourceTypeInfo(nameof(CoolingDevice), "29", ""); // TODO: subtype?
            public static ResourceTypeInfo Unknown30 => new ResourceTypeInfo(nameof(Unknown30), "30", ""); // TODO: subtype?
            public static ResourceTypeInfo VirtualCDDVDDisk => new ResourceTypeInfo(nameof(VirtualCDDVDDisk), "31", "Microsoft:Hyper-V:Virtual CD/DVD Disk");
            public static ResourceTypeInfo VirtualHardDisk => new ResourceTypeInfo(nameof(VirtualHardDisk), "31", "Microsoft:Hyper-V:Virtual Hard Disk");
            public static ResourceTypeInfo VirtualFloppyDisk => new ResourceTypeInfo(nameof(VirtualFloppyDisk), "31", "Microsoft:Hyper-V:Virtual Floppy Disk");
            public static ResourceTypeInfo Unknown32 => new ResourceTypeInfo(nameof(Unknown32), "32", ""); // TODO: subtype?
            public static ResourceTypeInfo EthernetConnection => new ResourceTypeInfo(nameof(EthernetConnection), "33", "Microsoft:Hyper-V:Ethernet Connection");
            public static ResourceTypeInfo StorageLogicalUnit => new ResourceTypeInfo(nameof(StorageLogicalUnit), "32768", "Microsoft:Hyper-V:Storage Logical Unit");
            public static ResourceTypeInfo VirtualPciExpressPort => new ResourceTypeInfo(nameof(VirtualPciExpressPort), "32769", "Microsoft:Hyper-V:Virtual Pci Express Port");
            public static ResourceTypeInfo GPUPartition => new ResourceTypeInfo(nameof(GPUPartition), "32770", "Microsoft:Hyper-V:GPU Partition");
            public static ResourceTypeInfo PersistentMemoryController => new ResourceTypeInfo(nameof(PersistentMemoryController), "32771", "Microsoft:Hyper-V:Persistent Memory Controller");
            public static ResourceTypeInfo FlexibleIODevice => new ResourceTypeInfo(nameof(FlexibleIODevice), "32772", "Microsoft:Hyper-V:Flexible IO Device");
            public static ResourceTypeInfo FiberChannelConnection => new ResourceTypeInfo(nameof(EthernetConnection), "64764", "Microsoft:Hyper-V:FiberChannel Connection");
        }

        #region MsvmProperties

        string InstanceID => Msvm_ResourcePool[nameof(InstanceID)].ToString();
        string Caption => Msvm_ResourcePool[nameof(Caption)].ToString();
        string Description => Msvm_ResourcePool[nameof(Description)].ToString();
        string ElementName => Msvm_ResourcePool[nameof(ElementName)].ToString();
        DateTime InstallDate => ManagementDateTimeConverter.ToDateTime(Msvm_ResourcePool[nameof(InstallDate)].ToString());
        string Name => Msvm_ResourcePool[nameof(Name)].ToString();
        OperationalStatusRP[] OperationalStatus => (OperationalStatusRP[])Msvm_ResourcePool[nameof(OperationalStatus)];
        string[] StatusDescriptions => (string[])Msvm_ResourcePool[nameof(StatusDescriptions)];
        string Status => Msvm_ResourcePool[nameof(Status)].ToString();
        ushort HealthState => (ushort)Msvm_ResourcePool[nameof(HealthState)];
        CommunicationStatusRP CommunicationStatus => (CommunicationStatusRP)Msvm_ResourcePool[nameof(CommunicationStatus)];
        DetailedStatusRP DetailedStatus => (DetailedStatusRP)Msvm_ResourcePool[nameof(DetailedStatus)];
        OperatingStatusRP OperatingStatus => (OperatingStatusRP)Msvm_ResourcePool[nameof(OperatingStatus)];
        PrimaryStatusRP PrimaryStatus => (PrimaryStatusRP)Msvm_ResourcePool[nameof(PrimaryStatus)];
        string PoolID => Msvm_ResourcePool[nameof(PoolID)].ToString();
        bool Primordial => (bool)Msvm_ResourcePool[nameof(Primordial)];
        ulong Capacity => (ulong)Msvm_ResourcePool[nameof(Capacity)];
        ulong Reserved => (ulong)Msvm_ResourcePool[nameof(Reserved)];
        ushort ResourceType => (ushort)Msvm_ResourcePool[nameof(ResourceType)];
        string OtherResourceType => Msvm_ResourcePool[nameof(OtherResourceType)].ToString();
        string ResourceSubType => Msvm_ResourcePool[nameof(ResourceSubType)].ToString();
        string AllocationUnits => Msvm_ResourcePool[nameof(AllocationUnits)].ToString();
        string ConsumedResourceUnits => Msvm_ResourcePool[nameof(ConsumedResourceUnits)].ToString();
        ulong CurrentlyConsumedResource => (ulong)Msvm_ResourcePool[nameof(CurrentlyConsumedResource)];
        ulong MaxConsumableResource => (ulong)Msvm_ResourcePool[nameof(MaxConsumableResource)];

        #endregion
        
        public ResourcePool(ManagementObject MsvmResourcePool)
        {
            scope = Utils.GetScope(Properties.Environment.Default.Server, Properties.Environment.Default.Virtualization);
            Msvm_ResourcePool = MsvmResourcePool;
        }

        public ManagementObject MsvmResourcePool => Msvm_ResourcePool;

        ~ResourcePool()
        {
            Msvm_ResourcePool.Dispose();
        }

        #region Utils

        public static ManagementObject GetPool(string ResourceSubType, bool Primordial = true)
        {
            using (var mos = new ManagementObjectSearcher(Utils.GetScope(Properties.Environment.Default.Server, Properties.Environment.Default.Virtualization), new ObjectQuery("SELECT * FROM Msvm_ResourcePool")))
                return mos
                    .Get()
                    .Cast<ManagementObject>()
                    .Where((c) => 
                        c[nameof(ResourceSubType)]?.ToString() == ResourceSubType &&
                        (bool)c[nameof(Primordial)] == Primordial)
                    .First();
        }

        public static ManagementObject GetResourcePool(string ResourceType, string ResourceSubType, string PoolId, ManagementScope scope)
        {
            using (var mos = new ManagementObjectSearcher(scope, new ObjectQuery("SELECT * FROM Msvm_ResourcePool")))
                return mos
                    .Get()
                    .Cast<ManagementObject>()
                    .Where((c) =>
                        c[nameof(ResourceType)]?.ToString() == ResourceType &&
                        (ResourceType == ResourceTypeInfo.Other.ResourceType ?
                            c["OtherResourceType"]?.ToString() == ResourceSubType :
                            c[nameof(ResourceSubType)]?.ToString() == ResourceSubType) &&
                        c[nameof(PoolId)]?.ToString() == PoolId)
                    .First();
        }

        #endregion
    }
}
