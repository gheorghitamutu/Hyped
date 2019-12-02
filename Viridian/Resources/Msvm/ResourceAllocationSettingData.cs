﻿using System.Linq;
using System.Management;
using Viridian.Scopes;
using static Viridian.Resources.Msvm.ResourcePoolSettingData;

namespace Viridian.Resources.Msvm
{
    public sealed class ResourceAllocationSettingData
    {
        private static ManagementObject Msvm_ResourceAllocationSettingData = null;

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
            using (var rpsdClass = new ManagementClass(nameof(Msvm_ResourceAllocationSettingData)) { Scope = Scope.Virtualization.SpecificScope })
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
            Msvm_ResourceAllocationSettingData = ResourceAllocationSettingData;
        }

        #region Utils

        private static ManagementObject GetResourceAllocationSettingDataForPool(ManagementObject pool, ushort ValueRange, ushort ValueRole)
        {
            return
                new ManagementObject(
                        pool.Scope,
                        new ManagementPath(
                            pool?.GetRelated("Msvm_AllocationCapabilities", "Msvm_ElementCapabilities", null, null, null, null, false, null)
                                .Cast<ManagementObject>()
                                .Where(
                                    (c) =>
                                        c.GetRelationships("Msvm_SettingsDefineCapabilities")
                                            .Cast<ManagementObject>()
                                            .Where((r) => (ushort)r[nameof(ValueRole)] == ValueRole && (ushort)r[nameof(ValueRange)] == ValueRange)
                                                .Any())
                            .First()
                            .GetRelationships("Msvm_SettingsDefineCapabilities")
                                .Cast<ManagementObject>()
                                .Where((r) => (ushort)r[nameof(ValueRole)] == ValueRole && (ushort)r[nameof(ValueRange)] == ValueRange)
                                .First()["PartComponent"].ToString()),
                        null);
        }

        public static ManagementObject GetDefaultResourceAllocationSettingDataForPool(ManagementObject pool) => GetResourceAllocationSettingDataForPool(pool, 0, 0);

        public static ManagementObject GetMinimumResourceAllocationSettingDataForPool(ManagementObject pool) => GetResourceAllocationSettingDataForPool(pool, 1, 3);

        public static ManagementObject GetMaximumResourceAllocationSettingDataForPool(ManagementObject pool) => GetResourceAllocationSettingDataForPool(pool, 2, 3);

        public static ManagementObject GetIncrementalResourceAllocationSettingDataForPool(ManagementObject pool) => GetResourceAllocationSettingDataForPool(pool, 3, 3);

        #endregion
    }
}
