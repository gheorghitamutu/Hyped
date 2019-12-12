using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;
using Viridian.Scopes;

namespace Viridian.Msvm.Networking
{
    public sealed class SyntheticEthernetPortSettingData
    {
        private ManagementObject Msvm_SyntheticEthernetPortSettingData = null;
        private VirtualSystemSettingData VirtualSystemSettingData { get; set; }
        private string[] virtualSystemIdentifiers = null;

        public SyntheticEthernetPortSettingData(ManagementObject SyntheticEthernetPortSettingData, VirtualSystemSettingData VirtualSystemSettingData = null)
        {
            MsvmSyntheticEthernetPortSettingData = SyntheticEthernetPortSettingData;
            this.VirtualSystemSettingData = VirtualSystemSettingData;
        }

        public SyntheticEthernetPortSettingData(string ResourceSubType, string ElementName, bool StaticMacAddress = false, VirtualSystemSettingData VirtualSystemSettingData = null)
        {
            using (var pool = ResourcePool.GetPool(ResourceSubType))
                Msvm_SyntheticEthernetPortSettingData = ResourceAllocationSettingData.GetDefaultResourceAllocationSettingDataForPool(pool);

            Msvm_SyntheticEthernetPortSettingData[nameof(ElementName)] = ElementName;
            Msvm_SyntheticEthernetPortSettingData[nameof(VirtualSystemIdentifiers)] = new string[] { Guid.NewGuid().ToString("B") };
            Msvm_SyntheticEthernetPortSettingData[nameof(StaticMacAddress)] = StaticMacAddress;

            this.VirtualSystemSettingData = VirtualSystemSettingData;
        }

        public ManagementObject MsvmSyntheticEthernetPortSettingData
        {
            get
            {
                virtualSystemIdentifiers = Msvm_SyntheticEthernetPortSettingData?[nameof(VirtualSystemIdentifiers)] as string[];

                if (Msvm_SyntheticEthernetPortSettingData != null && virtualSystemIdentifiers != null && virtualSystemIdentifiers?.Length > 0)
                    using (var mos = new ManagementObjectSearcher(Scope.Virtualization.ScopeObject, new ObjectQuery($"SELECT * FROM {nameof(Msvm_SyntheticEthernetPortSettingData)}")))
                        Msvm_SyntheticEthernetPortSettingData = mos.Get().Cast<ManagementObject>().Where((c) => (c[nameof(VirtualSystemIdentifiers)] as string[])?[0] == virtualSystemIdentifiers[0]).FirstOrDefault();

                return Msvm_SyntheticEthernetPortSettingData;
            }

            set
            {
                Msvm_SyntheticEthernetPortSettingData?.Dispose();
                Msvm_SyntheticEthernetPortSettingData = value;
            }
        }

        #region MsvmProperties

        public string InstanceID => MsvmSyntheticEthernetPortSettingData[nameof(InstanceID)] as string;
        public string Caption => MsvmSyntheticEthernetPortSettingData[nameof(Caption)] as string;
        public string Description => MsvmSyntheticEthernetPortSettingData[nameof(Description)] as string;
        public string ElementName => MsvmSyntheticEthernetPortSettingData[nameof(ElementName)] as string;
        public ResourcePoolSettingData.PoolResourceType ResourceType => (ResourcePoolSettingData.PoolResourceType)(ushort)MsvmSyntheticEthernetPortSettingData[nameof(ResourceType)];
        public string OtherResourceType => MsvmSyntheticEthernetPortSettingData[nameof(OtherResourceType)] as string;
        public string ResourceSubType => MsvmSyntheticEthernetPortSettingData[nameof(ResourceSubType)] as string;
        public string PoolID => MsvmSyntheticEthernetPortSettingData[nameof(PoolID)] as string;
        public ushort ConsumerVisibility => (ushort)MsvmSyntheticEthernetPortSettingData[nameof(ConsumerVisibility)];
        public string[] HostResource => MsvmSyntheticEthernetPortSettingData[nameof(HostResource)] as string[];
        public string AllocationUnits => MsvmSyntheticEthernetPortSettingData[nameof(AllocationUnits)] as string;
        public ulong VirtualQuantity => (ulong)MsvmSyntheticEthernetPortSettingData[nameof(VirtualQuantity)];
        public ulong Reservation => (ulong)MsvmSyntheticEthernetPortSettingData[nameof(Reservation)];
        public ulong Limit => (ulong)MsvmSyntheticEthernetPortSettingData[nameof(Limit)];
        public uint Weight => (uint)MsvmSyntheticEthernetPortSettingData[nameof(Weight)];
        public bool AutomaticAllocation => (bool)MsvmSyntheticEthernetPortSettingData[nameof(AutomaticAllocation)];
        public bool AutomaticDeallocation => (bool)MsvmSyntheticEthernetPortSettingData[nameof(AutomaticDeallocation)];
        public string Parent => MsvmSyntheticEthernetPortSettingData[nameof(Parent)] as string;
        public string[] Connection => MsvmSyntheticEthernetPortSettingData[nameof(Connection)] as string[];
        public string Address => MsvmSyntheticEthernetPortSettingData[nameof(Address)] as string;
        public ResourcePoolSettingData.PoolMappingBehavior MappingBehavior => (ResourcePoolSettingData.PoolMappingBehavior)(ushort)MsvmSyntheticEthernetPortSettingData[nameof(MappingBehavior)];
        public string AddressOnParent => MsvmSyntheticEthernetPortSettingData[nameof(AddressOnParent)] as string;
        public string VirtualQuantityUnits => MsvmSyntheticEthernetPortSettingData[nameof(VirtualQuantityUnits)] as string;
        public ushort DesiredVLANEndpointMode => (ushort)MsvmSyntheticEthernetPortSettingData[nameof(DesiredVLANEndpointMode)];
        public string OtherEndpointMode => MsvmSyntheticEthernetPortSettingData[nameof(OtherEndpointMode)] as string;
        public string[] VirtualSystemIdentifiers
        {
            get { return Msvm_SyntheticEthernetPortSettingData != null ? MsvmSyntheticEthernetPortSettingData[nameof(VirtualSystemIdentifiers)] as string[] : virtualSystemIdentifiers; }
            private set { virtualSystemIdentifiers = value; }
        }
        public bool DeviceNamingEnabled => (bool)MsvmSyntheticEthernetPortSettingData[nameof(DeviceNamingEnabled)];
        public bool AllowPacketDirect => (bool)MsvmSyntheticEthernetPortSettingData[nameof(AllowPacketDirect)];
        public bool StaticMacAddress => (bool)MsvmSyntheticEthernetPortSettingData[nameof(StaticMacAddress)];
        public bool ClusterMonitored => (bool)MsvmSyntheticEthernetPortSettingData[nameof(ClusterMonitored)];

        #endregion

        public void AddAsChild()
        {
            // TODO: fix this!
            // VirtualSystemManagementService.Instance.AddResourceSettings(VirtualSystemSettingData.MsvmVirtualSystemSettingData, new string[] { Msvm_SyntheticEthernetPortSettingData.GetText(TextFormat.WmiDtd20) });
        }
        public static List<ManagementObject> GetRelatedSyntheticEthernetPortSettingDataCollection(ManagementObject msvmObjectRelatedTo, string ResourceType, string ResourceSubType)
        {
            return
                msvmObjectRelatedTo?
                    .GetRelated(nameof(Msvm_SyntheticEthernetPortSettingData))
                        .Cast<ManagementObject>()
                        .Where((c) =>
                            c[nameof(ResourceType)]?.ToString() == ResourceType &&
                            c[nameof(ResourceSubType)]?.ToString() == ResourceSubType)
                        .ToList();
        }
        public static ManagementObject GetEthernetPortAllocationSettingData(ManagementObject Parent, ManagementScope scope)
        {
            using (var mos = new ManagementObjectSearcher(scope, new ObjectQuery("SELECT * FROM Msvm_EthernetPortAllocationSettingData")))
                return mos
                    .Get()
                    .Cast<ManagementObject>()
                    .Where((c) => string.Equals((string)c?["Parent"], Parent.Path.Path, StringComparison.OrdinalIgnoreCase))
                    .First();
        }

        public static List<ManagementObject> GetEthernetSwitchPortAclSettingData(ManagementObject ethernetConnection)
        {
            return ethernetConnection?.GetRelated("Msvm_EthernetSwitchPortAclSettingData", "Msvm_EthernetPortSettingDataComponent", null, null, null, null, false, null).Cast<ManagementObject>().ToList();
        }
    }
}
