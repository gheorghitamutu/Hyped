using System.Collections.Generic;
using System.Linq;
using System.Management;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.VirtualSystem;
using Viridian.Scopes;

namespace Viridian.Msvm.Storage
{
    public sealed class StorageAllocationSettingData
    {
        private ManagementObject Msvm_StorageAllocationSettingData = null;
        public ResourceAllocationSettingData ResourceAllocationSettingData { get; private set; }
        private string[] hostResource = null;

        public StorageAllocationSettingData(ManagementObject StorageAllocationSettingData, ResourceAllocationSettingData ResourceAllocationSettingData)
        {
            MsvmStorageAllocationSettingData = StorageAllocationSettingData;
            this.ResourceAllocationSettingData = ResourceAllocationSettingData;
        }

        public ManagementObject MsvmStorageAllocationSettingData
        {
            get
            {
                hostResource = Msvm_StorageAllocationSettingData?[nameof(HostResource)] as string[];

                if (Msvm_StorageAllocationSettingData != null && hostResource != null && hostResource?.Length > 0)
                    using (var mos = new ManagementObjectSearcher(Scope.Virtualization.ScopeObject, new ObjectQuery($"SELECT * FROM {nameof(Msvm_StorageAllocationSettingData)}")))
                        Msvm_StorageAllocationSettingData = mos.Get().Cast<ManagementObject>().Where((c) => (c[nameof(HostResource)] as string[])?[0] == hostResource[0]).FirstOrDefault();

                return Msvm_StorageAllocationSettingData;
            }

            set
            {
                Msvm_StorageAllocationSettingData?.Dispose();
                Msvm_StorageAllocationSettingData = value;
            }
        }

        public enum AccessSASD : ushort
        {
            Unknown = 0,
            Readable = 1,
            Writeable = 2,
            ReadWriteSupported = 3
        }
        public enum CachingModeSASD : ushort
        {
            Unknown = 0,
            Default = 2,
            NoCaching = 3,
            CacheSharableParents = 4
        }
        public enum ConsumerVisibilitySASD : ushort
        {
            Unknown = 0,
            PassedThrough = 2,
            Virtualized = 3,
            NotRepresented = 4
        }
        public enum HostExtentNameFormatSASD : ushort
        {
            Unknown = 0,
            Other = 1,
            SNVM = 7,
            NAA = 9,
            EUI64 = 10,
            T10VID = 11,
            OSDeviceName = 12
        }
        public enum HostExtentNameNamespaceSASD : ushort
        {
            Unknown = 0,
            Other = 1,
            VPD83Type3 = 2,
            VPD83Type2 = 3,
            VPD83Type1 = 4,
            VPD80 = 5,
            NodeWWN = 6,
            SNVM = 7,
            OSDeviceNamespace = 8
        }

        #region MsvmProperties

        public string InstanceID => MsvmStorageAllocationSettingData[nameof(InstanceID)] as string;
        public string Caption => MsvmStorageAllocationSettingData[nameof(Caption)] as string;
        public string Description => MsvmStorageAllocationSettingData[nameof(Description)] as string;
        public string ElementName => MsvmStorageAllocationSettingData[nameof(ElementName)] as string;
        public ResourcePoolSettingData.PoolResourceType ResourceType => (ResourcePoolSettingData.PoolResourceType)(ushort)MsvmStorageAllocationSettingData[nameof(ResourceType)];
        public string OtherResourceType => MsvmStorageAllocationSettingData[nameof(OtherResourceType)] as string;
        public string ResourceSubType => MsvmStorageAllocationSettingData[nameof(ResourceSubType)] as string;
        public string PoolID => MsvmStorageAllocationSettingData[nameof(PoolID)] as string;
        public ConsumerVisibilitySASD ConsumerVisibility => (ConsumerVisibilitySASD)(ushort)MsvmStorageAllocationSettingData[nameof(ConsumerVisibility)];
        public string[] HostResource
        {
            get { return Msvm_StorageAllocationSettingData != null ? MsvmStorageAllocationSettingData[nameof(HostResource)] as string[] : hostResource; }
            private set { hostResource = value; }
        }
        public string AllocationUnits => MsvmStorageAllocationSettingData[nameof(AllocationUnits)] as string;
        public ulong VirtualQuantity => (ulong)MsvmStorageAllocationSettingData[nameof(VirtualQuantity)];
        public ulong Limit => (ulong)MsvmStorageAllocationSettingData[nameof(Limit)];
        public uint Weight => (uint)MsvmStorageAllocationSettingData[nameof(Weight)];
        public string StorageQoSPolicyID => MsvmStorageAllocationSettingData[nameof(StorageQoSPolicyID)] as string;
        public bool AutomaticAllocation => (bool)MsvmStorageAllocationSettingData[nameof(AutomaticAllocation)];
        public bool AutomaticDeallocation => (bool)MsvmStorageAllocationSettingData[nameof(AutomaticDeallocation)];
        public string Parent => MsvmStorageAllocationSettingData[nameof(Parent)] as string;
        public string[] Connection => MsvmStorageAllocationSettingData[nameof(Connection)] as string[];
        public string Address => MsvmStorageAllocationSettingData[nameof(Address)] as string;
        public ResourcePoolSettingData.PoolMappingBehavior MappingBehavior => (ResourcePoolSettingData.PoolMappingBehavior)(ushort)MsvmStorageAllocationSettingData[nameof(MappingBehavior)];
        public string AddressOnParent => MsvmStorageAllocationSettingData[nameof(AddressOnParent)] as string;
        public ulong VirtualResourceBlockSize => (ulong)MsvmStorageAllocationSettingData[nameof(VirtualResourceBlockSize)];
        public string VirtualQuantityUnits => MsvmStorageAllocationSettingData[nameof(VirtualQuantityUnits)] as string;
        public AccessSASD Access => (AccessSASD)(ushort)MsvmStorageAllocationSettingData[nameof(Access)];
        public ulong HostResourceBlockSize => (ulong)MsvmStorageAllocationSettingData[nameof(HostResourceBlockSize)];
        public ulong Reservation => (ulong)MsvmStorageAllocationSettingData[nameof(Reservation)];
        public ulong HostExtentStartingAddress => (ulong)MsvmStorageAllocationSettingData[nameof(HostExtentStartingAddress)];
        public string HostExtentName => MsvmStorageAllocationSettingData[nameof(HostExtentName)] as string;
        public HostExtentNameFormatSASD HostExtentNameFormat => (HostExtentNameFormatSASD)(ushort)MsvmStorageAllocationSettingData[nameof(HostExtentNameFormat)];
        public string OtherHostExtentNameFormat => MsvmStorageAllocationSettingData[nameof(OtherHostExtentNameFormat)] as string;
        public HostExtentNameNamespaceSASD HostExtentNameNamespace => (HostExtentNameNamespaceSASD)(ushort)MsvmStorageAllocationSettingData[nameof(HostExtentNameNamespace)];
        public string OtherHostExtentNameNamespace => MsvmStorageAllocationSettingData[nameof(OtherHostExtentNameNamespace)] as string;
        public ulong IOPSLimit => (ulong)MsvmStorageAllocationSettingData[nameof(IOPSLimit)];
        public ulong IOPSReservation => (ulong)MsvmStorageAllocationSettingData[nameof(IOPSReservation)];
        public string IOPSAllocationUnits => MsvmStorageAllocationSettingData[nameof(IOPSAllocationUnits)] as string;
        public bool PersistentReservationsSupported => (bool)MsvmStorageAllocationSettingData[nameof(PersistentReservationsSupported)];
        public CachingModeSASD CachingMode => (CachingModeSASD)(ushort)MsvmStorageAllocationSettingData[nameof(CachingMode)];
        public string SnapshotId => MsvmStorageAllocationSettingData[nameof(SnapshotId)] as string;
        public bool IgnoreFlushes => (bool)MsvmStorageAllocationSettingData[nameof(IgnoreFlushes)];
        public ushort WriteHardeningMethod => (ushort)MsvmStorageAllocationSettingData[nameof(WriteHardeningMethod)];

        #endregion

        public static List<StorageAllocationSettingData> GetRelatedSASD(ResourceAllocationSettingData ResourceAllocationSettingData)
        {
            return
                ResourceAllocationSettingData?.MsvmResourceAllocationSettingData?
                    .GetRelated(nameof(Msvm_StorageAllocationSettingData))
                    .Cast<ManagementObject>()
                    .Select((child) => new StorageAllocationSettingData(child, ResourceAllocationSettingData))
                    .ToList();
        }

        ~StorageAllocationSettingData()
        {
            Msvm_StorageAllocationSettingData?.Dispose();
        }
    }
}
