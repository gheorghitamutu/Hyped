using System.Management;
using Viridian.Utilities;
using static Viridian.Resources.Msvm.ResourcePoolSettingData;

namespace Viridian.Resources.Msvm
{
    public sealed class ResourceAllocationSettingData
    {
        private const string serverName = ".";
        private const string scopePath = @"\Root\Virtualization\V2";
        private static ManagementObject Msvm_ResourceAllocationSettingData = null;
        private static ManagementScope scope = null;

        #region MsvmProperties

        string InstanceID => Msvm_ResourceAllocationSettingData[nameof(InstanceID)].ToString();
        string Caption => Msvm_ResourceAllocationSettingData[nameof(Caption)].ToString();
        string Description => Msvm_ResourceAllocationSettingData[nameof(Description)].ToString();
        string ElementName => Msvm_ResourceAllocationSettingData[nameof(ElementName)].ToString();
        PoolResourceType ResourceType => (PoolResourceType)(ushort)Msvm_ResourceAllocationSettingData[nameof(ResourceType)];
        string OtherResourceType => Msvm_ResourceAllocationSettingData[nameof(OtherResourceType)].ToString();
        string ResourceSubType => Msvm_ResourceAllocationSettingData[nameof(ResourceSubType)].ToString();
        string PoolID => Msvm_ResourceAllocationSettingData[nameof(PoolID)].ToString();
        ushort ConsumerVisibility => (ushort)Msvm_ResourceAllocationSettingData[nameof(ConsumerVisibility)];
        string[] HostResource => Msvm_ResourceAllocationSettingData[nameof(HostResource)] as string[];
        string AllocationUnits => Msvm_ResourceAllocationSettingData[nameof(AllocationUnits)].ToString();
        ulong VirtualQuantity => (ulong)Msvm_ResourceAllocationSettingData[nameof(VirtualQuantity)];
        ulong Reservation => (ulong)Msvm_ResourceAllocationSettingData[nameof(Reservation)];
        ulong Limit => (ulong)Msvm_ResourceAllocationSettingData[nameof(Limit)];
        uint Weight => (uint)Msvm_ResourceAllocationSettingData[nameof(Weight)];
        bool AutomaticAllocation => (bool)Msvm_ResourceAllocationSettingData[nameof(AutomaticAllocation)];
        bool AutomaticDeallocation => (bool)Msvm_ResourceAllocationSettingData[nameof(AutomaticDeallocation)];
        string Parent => Msvm_ResourceAllocationSettingData[nameof(Parent)].ToString();
        string[] Connection => Msvm_ResourceAllocationSettingData[nameof(Connection)] as string[];
        string Address => Msvm_ResourceAllocationSettingData[nameof(Address)].ToString();
        PoolMappingBehavior MappingBehavior => (PoolMappingBehavior)(ushort)Msvm_ResourceAllocationSettingData[nameof(MappingBehavior)];
        string AddressOnParent => Msvm_ResourceAllocationSettingData[nameof(AddressOnParent)].ToString();
        string VirtualQuantityUnits => Msvm_ResourceAllocationSettingData[nameof(VirtualQuantityUnits)].ToString();
        string[] VirtualSystemIdentifiers => Msvm_ResourceAllocationSettingData[nameof(VirtualSystemIdentifiers)] as string[];

        #endregion

        public ResourceAllocationSettingData(ushort ResourceType, string ResourceSubType, string PoolId, string[] HostResource)
        {
            scope = Utils.GetScope(serverName, scopePath);

            using (var rpsdClass = new ManagementClass(nameof(Msvm_ResourceAllocationSettingData)) { Scope = scope })
            {
                Msvm_ResourceAllocationSettingData = rpsdClass.CreateInstance();

                Msvm_ResourceAllocationSettingData[nameof(ResourceType)] = ResourceType;
                Msvm_ResourceAllocationSettingData[nameof(ResourceSubType)] = ResourceType != 0 ? string.Empty : ResourceSubType;
                Msvm_ResourceAllocationSettingData[nameof(OtherResourceType)] = ResourceType == 0 ? string.Empty : ResourceSubType;
                Msvm_ResourceAllocationSettingData[nameof(PoolId)] = PoolId;
                Msvm_ResourceAllocationSettingData[nameof(HostResource)] = HostResource;
            }
        }

        public ResourceAllocationSettingData(ManagementObject ResourceAllocationSettingData)
        {
            scope = ResourceAllocationSettingData.Scope;

            Msvm_ResourceAllocationSettingData = ResourceAllocationSettingData;
        }

        #region Utils

        private static ManagementObject GetResourceAllocationSettingDataForPool(ManagementObject pool, ushort ValueRange, ushort ValueRole)
        {
            using (var capabilitiesCollection = pool.GetRelated("Msvm_AllocationCapabilities", "Msvm_ElementCapabilities", null, null, null, null, false, null))
                foreach (ManagementObject capability in capabilitiesCollection)
                {
                    using (var relationshipsCollection = capability.GetRelationships("Msvm_SettingsDefineCapabilities"))
                        foreach (ManagementObject relationship in relationshipsCollection)
                        {
                            if ((ushort)relationship[nameof(ValueRole)] != ValueRole || (ushort)relationship[nameof(ValueRange)] != ValueRange)
                            {
                                relationship.Dispose();
                                continue;
                            }

                            return new ManagementObject(pool.Scope, new ManagementPath(relationship["PartComponent"].ToString()), null);
                        }

                    capability.Dispose();
                }

            return null;
        }

        public static ManagementObject GetDefaultResourceAllocationSettingDataForPool(ManagementObject pool) => GetResourceAllocationSettingDataForPool(pool, 0, 0);

        public static ManagementObject GetMinimumResourceAllocationSettingDataForPool(ManagementObject pool) => GetResourceAllocationSettingDataForPool(pool, 1, 3);

        public static ManagementObject GetMaximumResourceAllocationSettingDataForPool(ManagementObject pool) => GetResourceAllocationSettingDataForPool(pool, 2, 3);

        public static ManagementObject GetIncrementalResourceAllocationSettingDataForPool(ManagementObject pool) => GetResourceAllocationSettingDataForPool(pool, 3, 3);

        #endregion
    }
}
