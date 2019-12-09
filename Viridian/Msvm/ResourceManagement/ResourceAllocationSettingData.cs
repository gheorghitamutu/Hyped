using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;
using Viridian.Scopes;

namespace Viridian.Msvm.ResourceManagement
{
    public sealed class ResourceAllocationSettingData
    {
        private ManagementObject Msvm_ResourceAllocationSettingData = null;
        private VirtualSystemSettingData virtualSystemSettingData = null;
        private string[] virtualSystemIdentifiers = null;

        public ResourceAllocationSettingData(ushort ResourceType, string ResourceSubType, string PoolId, string[] HostResource)
        {
            using (var rpsdClass = new ManagementClass(nameof(Msvm_ResourceAllocationSettingData)) { Scope = Scope.Virtualization.ScopeObject })
            {
                Msvm_ResourceAllocationSettingData = rpsdClass.CreateInstance();

                Msvm_ResourceAllocationSettingData[nameof(ResourceType)] = ResourceType;
                Msvm_ResourceAllocationSettingData[nameof(ResourceSubType)] = ResourceType != 0 ? string.Empty : ResourceSubType;
                Msvm_ResourceAllocationSettingData[nameof(OtherResourceType)] = ResourceType == 0 ? string.Empty : ResourceSubType;
                Msvm_ResourceAllocationSettingData[nameof(PoolId)] = PoolId;
                Msvm_ResourceAllocationSettingData[nameof(HostResource)] = HostResource;
            }
        }

        public ResourceAllocationSettingData(ManagementObject ResourceAllocationSettingData, VirtualSystemSettingData virtualSystemSettingData = null)
        {
            MsvmResourceAllocationSettingData = ResourceAllocationSettingData;
            this.virtualSystemSettingData = virtualSystemSettingData;
        }

        public ManagementObject MsvmResourceAllocationSettingData
        {
            get
            {
                virtualSystemIdentifiers = Msvm_ResourceAllocationSettingData?[nameof(VirtualSystemIdentifiers)] as string[];

                if (Msvm_ResourceAllocationSettingData != null && virtualSystemIdentifiers != null && virtualSystemIdentifiers?.Length > 0)
                    using (var mos = new ManagementObjectSearcher(Scope.Virtualization.ScopeObject, new ObjectQuery($"SELECT * FROM {nameof(Msvm_ResourceAllocationSettingData)}")))
                        Msvm_ResourceAllocationSettingData = mos.Get().Cast<ManagementObject>().Where((c) => (c[nameof(VirtualSystemIdentifiers)] as string[])?[0] == virtualSystemIdentifiers[0]).FirstOrDefault();

                return Msvm_ResourceAllocationSettingData;
            }

            set
            {
                Msvm_ResourceAllocationSettingData?.Dispose();
                Msvm_ResourceAllocationSettingData = value;
            }
        }


        #region MsvmProperties

        public string InstanceID => MsvmResourceAllocationSettingData[nameof(InstanceID)].ToString();
        public string Caption => MsvmResourceAllocationSettingData[nameof(Caption)].ToString();
        public string Description => MsvmResourceAllocationSettingData[nameof(Description)].ToString();
        public string ElementName => MsvmResourceAllocationSettingData[nameof(ElementName)].ToString();
        public ResourcePoolSettingData.PoolResourceType ResourceType => (ResourcePoolSettingData.PoolResourceType)(ushort)MsvmResourceAllocationSettingData[nameof(ResourceType)];
        public string OtherResourceType => MsvmResourceAllocationSettingData[nameof(OtherResourceType)].ToString();
        public string ResourceSubType => MsvmResourceAllocationSettingData[nameof(ResourceSubType)].ToString();
        public string PoolID => MsvmResourceAllocationSettingData[nameof(PoolID)].ToString();
        public ushort ConsumerVisibility => (ushort)MsvmResourceAllocationSettingData[nameof(ConsumerVisibility)];
        public string[] HostResource => MsvmResourceAllocationSettingData[nameof(HostResource)] as string[];
        public string AllocationUnits => MsvmResourceAllocationSettingData[nameof(AllocationUnits)].ToString();
        public ulong VirtualQuantity => (ulong)MsvmResourceAllocationSettingData[nameof(VirtualQuantity)];
        public ulong Reservation => (ulong)MsvmResourceAllocationSettingData[nameof(Reservation)];
        public ulong Limit => (ulong)MsvmResourceAllocationSettingData[nameof(Limit)];
        public uint Weight => (uint)MsvmResourceAllocationSettingData[nameof(Weight)];
        public bool AutomaticAllocation => (bool)MsvmResourceAllocationSettingData[nameof(AutomaticAllocation)];
        public bool AutomaticDeallocation => (bool)MsvmResourceAllocationSettingData[nameof(AutomaticDeallocation)];
        public string Parent => MsvmResourceAllocationSettingData[nameof(Parent)].ToString();
        public string[] Connection => MsvmResourceAllocationSettingData[nameof(Connection)] as string[];
        public string Address => MsvmResourceAllocationSettingData[nameof(Address)].ToString();
        public ResourcePoolSettingData.PoolMappingBehavior MappingBehavior => (ResourcePoolSettingData.PoolMappingBehavior)(ushort)MsvmResourceAllocationSettingData[nameof(MappingBehavior)];
        public string AddressOnParent => MsvmResourceAllocationSettingData[nameof(AddressOnParent)].ToString();
        public string VirtualQuantityUnits => MsvmResourceAllocationSettingData[nameof(VirtualQuantityUnits)].ToString();
        public string[] VirtualSystemIdentifiers
        {
            get { return Msvm_ResourceAllocationSettingData != null ? MsvmResourceAllocationSettingData[nameof(VirtualSystemIdentifiers)] as string[] : virtualSystemIdentifiers; }
            private set { virtualSystemIdentifiers = value; }
        }

        #endregion


        #region Utils

        public string[] AddChild(int AddressOnParent, string ResourceSubType)
        {
            using (var pool = ResourcePool.GetPool(ResourceSubType))
            using (var RASD = GetDefaultResourceAllocationSettingDataForPool(pool))
            {
                RASD[nameof(Parent)] = MsvmResourceAllocationSettingData;
                RASD[nameof(AddressOnParent)] = AddressOnParent;

                return VirtualSystemManagementService.Instance.AddResourceSettings(virtualSystemSettingData.MsvmVirtualSystemSettingData, new[] { RASD.GetText(TextFormat.WmiDtd20) });
            }
        }

        public List<ResourceAllocationSettingData> RASDChildren
        {
            get
            {
                return
                    MsvmResourceAllocationSettingData?
                        .GetRelated(nameof(Msvm_ResourceAllocationSettingData))
                        .Cast<ManagementObject>()
                        .Select((child) => new ResourceAllocationSettingData(child, virtualSystemSettingData))
                        .ToList();
            }

            private set
            {
                // nothing
            }
        }

        public List<ManagementObject> SASDChildren
        {
            get
            {
                return
                    MsvmResourceAllocationSettingData?
                        .GetRelated("Msvm_StorageAllocationSettingData")
                        .Cast<ManagementObject>()
                        .ToList();
            }

            private set
            {
                // nothing
            }
        }

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

        public static ManagementObject GetRelatedResourceAllocationSettingData(ManagementObject msvmObjectRelatedTo, string ResourceSubtype, int index = 0)
        {
            var Parent = msvmObjectRelatedTo?.Path.Path;

            return
                msvmObjectRelatedTo?.GetRelated(nameof(Msvm_ResourceAllocationSettingData))
                    .Cast<ManagementObject>()
                    .Where((c) =>
                        c[nameof(ResourceSubtype)]?.ToString() == ResourceSubtype &&
                        string.Equals(c[nameof(Parent)]?.ToString(), Parent, StringComparison.InvariantCultureIgnoreCase))
                    .Skip(index)
                    .First();
        }
        public static List<ManagementObject> GetRelatedResourceAllocationSettingDataCollection(ManagementObject msvmObjectRelatedTo)
        {
            var Parent = msvmObjectRelatedTo?.Path.Path;

            return
                msvmObjectRelatedTo?.GetRelated(nameof(Msvm_ResourceAllocationSettingData))
                    .Cast<ManagementObject>()
                    .Where((c) => string.Equals(c[nameof(Parent)]?.ToString(), Parent, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();
        }
        public static List<ManagementObject> GetRelatedResourceAllocationSettingDataCollection(ManagementObject msvmObjectRelatedTo, string ResourceType, string ResourceSubType)
        {
            return
                msvmObjectRelatedTo?
                    .GetRelated(nameof(Msvm_ResourceAllocationSettingData))
                        .Cast<ManagementObject>()
                        .Where((c) =>
                            c[nameof(ResourceType)]?.ToString() == ResourceType &&
                            c[nameof(ResourceSubType)]?.ToString() == ResourceSubType)
                        .ToList();
        }

        #endregion
    }
}
