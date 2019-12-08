using System.Management;
using Viridian.Scopes;

namespace Viridian.Msvm.ResourceManagement
{
    public sealed class ResourcePoolSettingData
    {
        private static ManagementObject Msvm_ResourcePoolSettingData = null;

        public enum PoolLoadBalancingBehavior : ushort
        {
            Unknown = 0,
            NotSupported = 2,
            None = 3,
            Distributed = 4,
            Consolidated = 5
        }

        public enum PoolMappingBehavior : ushort
        {
            Unknown = 0,
            NotSupported = 2,
            Dedicated = 3,
            SoftAffinity = 4,
            HardAffinity = 5
        }

        public enum PoolResourceType : ushort
        {
            Other = 1,
            ComputerSystem = 2,
            Processor = 3,
            Memory = 4,
            IdeController = 5,
            ParallelScsiHba = 6,
            FcHba = 7,
            ScsiHba = 8,
            IbHca = 9,
            EthernetAdapter = 10,
            OtherNetworkAdapter = 11,
            IoSlot = 12,
            IoDevice = 13,
            DisketteDrive = 14,
            CdDrive = 15,
            DvdDrive = 16,
            DiskDrive = 17,
            TapeDrive = 18,
            StorageExtent = 19,
            OtherStorageDevice = 20,
            SerialPort = 21,
            ParallelPort = 22,
            UsbController = 23,
            GraphicsController = 24,
            Ieee1394Controller = 25,
            PartitionableUnit = 26,
            BasePartitionableUnit = 27,
            PowerSupply = 28,
            CoolingDevice = 29,
            EthernetSwitchPort = 30,
            LogicalDisk = 31,
            StorageVolume = 32,
            EthernetConnection = 33
        }

        #region MsvmProperties

        string InstanceID => Msvm_ResourcePoolSettingData[nameof(InstanceID)].ToString();
        string Caption => Msvm_ResourcePoolSettingData[nameof(Caption)].ToString();
        string Description => Msvm_ResourcePoolSettingData[nameof(Description)].ToString();
        string ElementName => Msvm_ResourcePoolSettingData[nameof(ElementName)].ToString();
        string PoolID => Msvm_ResourcePoolSettingData[nameof(PoolID)].ToString();
        PoolResourceType ResourceType => (PoolResourceType)(ushort)Msvm_ResourcePoolSettingData[nameof(ResourceType)];
        string OtherResourceType => Msvm_ResourcePoolSettingData[nameof(OtherResourceType)].ToString();
        string ResourceSubType => Msvm_ResourcePoolSettingData[nameof(ResourceSubType)].ToString();
        PoolLoadBalancingBehavior LoadBalancingBehavior => (PoolLoadBalancingBehavior)(ushort)Msvm_ResourcePoolSettingData[nameof(LoadBalancingBehavior)];
        PoolMappingBehavior MappingBehavior => (PoolMappingBehavior)(ushort)Msvm_ResourcePoolSettingData[nameof(MappingBehavior)];
        string[] MappingOrder => Msvm_ResourcePoolSettingData[nameof(MappingOrder)] as string[];
        string Notes => Msvm_ResourcePoolSettingData[nameof(Notes)].ToString();

        #endregion

        public ResourcePoolSettingData(ushort ResourceType, string ResourceSubType, string PoolId, string ElementName)
        {
            using (var rpsdClass = new ManagementClass(nameof(Msvm_ResourcePoolSettingData)) { Scope = Scope.Virtualization.ScopeObject })
            {
                Msvm_ResourcePoolSettingData = rpsdClass.CreateInstance();

                Msvm_ResourcePoolSettingData[nameof(ResourceType)] = ResourceType;
                Msvm_ResourcePoolSettingData[nameof(ResourceSubType)] = ResourceType != 0 ? string.Empty : ResourceSubType;
                Msvm_ResourcePoolSettingData[nameof(OtherResourceType)] = ResourceType == 0 ? string.Empty : ResourceSubType;
                Msvm_ResourcePoolSettingData[nameof(PoolId)] = PoolId;
                Msvm_ResourcePoolSettingData[nameof(ElementName)] = ElementName;
            }
        }

        ~ResourcePoolSettingData()
        {
            Msvm_ResourcePoolSettingData.Dispose();
        }
    }
}
