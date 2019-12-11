using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Viridian.Scopes;
using static Viridian.Msvm.VirtualSystem.VirtualSystemSettingData;

namespace Viridian.Msvm.Networking
{
    public sealed class VirtualEthernetSwitchSettingData
    {
        private ManagementObject Msvm_VirtualEthernetSwitchSettingData = null;
        private VirtualEthernetSwitch VirtualEthernetSwitch { set; get; }
        public string VSSDAssociation { private set; get; }
        private Dictionary<string, object> Attributes { set; get; }

        public VirtualEthernetSwitchSettingData(Dictionary<string, object> Properties)
        {
            using (var vessd = new ManagementClass(nameof(Msvm_VirtualEthernetSwitchSettingData)))
            {
                vessd.Scope = Scope.Virtualization.ScopeObject;

                Msvm_VirtualEthernetSwitchSettingData = vessd.CreateInstance();

                ModifyProperties(Properties);
            }
        }

        public VirtualEthernetSwitchSettingData(VirtualEthernetSwitch VirtualEthernetSwitch = null, string VSSDAssociation = null, Dictionary<string, object> Attributes = null)
        {
            this.VirtualEthernetSwitch = VirtualEthernetSwitch;
            this.VSSDAssociation = VSSDAssociation;
            this.Attributes = Attributes;
        }

        public ManagementObject MsvmVirtualEthernetSwitchSettingData
        {
            get
            {
                if (VirtualEthernetSwitch == null && Msvm_VirtualEthernetSwitchSettingData == null)
                    using (var serviceClass = new ManagementClass(Scope.Virtualization.ScopeObject, new ManagementPath(nameof(Msvm_VirtualEthernetSwitchSettingData)), null))
                        Msvm_VirtualEthernetSwitchSettingData = serviceClass.GetInstances().Cast<ManagementObject>().First();
                else if (Attributes == null)
                    Msvm_VirtualEthernetSwitchSettingData = GetMsvmVirtualSystemSettingDataCollection(VSSDAssociation).First();
                else
                    Msvm_VirtualEthernetSwitchSettingData =
                        GetMsvmVirtualSystemSettingDataCollection(VSSDAssociation)
                            .Where((vssd) => Attributes.Where((pair) => vssd.Properties[pair.Key].Value.Equals(pair.Value)).ToList().Count == Attributes.Count)
                            .First();

                return Msvm_VirtualEthernetSwitchSettingData;
            }

            private set
            {
                Msvm_VirtualEthernetSwitchSettingData?.Dispose();
                Msvm_VirtualEthernetSwitchSettingData = value;
            }
        }

        public enum BandwidthReservationModeVESSD : uint
        {
            Default = 0,
            Weight = 1,
            Absolute = 2,
            None = 3
        }

        #region MsvmProperties

        public string InstanceID => Msvm_VirtualEthernetSwitchSettingData[nameof(InstanceID)] as string;
        public string Caption => Msvm_VirtualEthernetSwitchSettingData[nameof(Caption)] as string;
        public string Description => Msvm_VirtualEthernetSwitchSettingData[nameof(Description)] as string;
        public string ElementName => Msvm_VirtualEthernetSwitchSettingData[nameof(ElementName)] as string;
        public string VirtualSystemIdentifier => Msvm_VirtualEthernetSwitchSettingData[nameof(VirtualSystemIdentifier)] as string;
        public string VirtualSystemType => Msvm_VirtualEthernetSwitchSettingData[nameof(VirtualSystemType)] as string;
        public string[] Notes => Msvm_VirtualEthernetSwitchSettingData[nameof(Notes)] as string[];
        public DateTime CreationTime => ManagementDateTimeConverter.ToDateTime(Msvm_VirtualEthernetSwitchSettingData[nameof(CreationTime)] as string);
        public string ConfigurationID => Msvm_VirtualEthernetSwitchSettingData[nameof(ConfigurationID)] as string;
        public string ConfigurationDataRoot => Msvm_VirtualEthernetSwitchSettingData[nameof(ConfigurationDataRoot)] as string;
        public string ConfigurationFile => Msvm_VirtualEthernetSwitchSettingData[nameof(ConfigurationFile)] as string;
        public string SnapshotDataRoot => Msvm_VirtualEthernetSwitchSettingData[nameof(SnapshotDataRoot)] as string;
        public string SuspendDataRoot => Msvm_VirtualEthernetSwitchSettingData[nameof(SuspendDataRoot)] as string;
        public string SwapFileDataRoot => Msvm_VirtualEthernetSwitchSettingData[nameof(SwapFileDataRoot)] as string;
        public string LogDataRoot => Msvm_VirtualEthernetSwitchSettingData[nameof(LogDataRoot)] as string;
        public AutomaticStartupActionVSSD AutomaticStartupAction => (AutomaticStartupActionVSSD)(ushort)Msvm_VirtualEthernetSwitchSettingData[nameof(AutomaticStartupAction)];
        public DateTime AutomaticStartupActionDelay => ManagementDateTimeConverter.ToDateTime(Msvm_VirtualEthernetSwitchSettingData[nameof(AutomaticStartupActionDelay)] as string);
        public ushort AutomaticStartupActionSequenceNumber => (ushort)Msvm_VirtualEthernetSwitchSettingData[nameof(AutomaticStartupActionSequenceNumber)];
        public AutomaticShutdownActionVSSD AutomaticShutdownAction => (AutomaticShutdownActionVSSD)(ushort)Msvm_VirtualEthernetSwitchSettingData[nameof(AutomaticShutdownAction)];
        public AutomaticRecoveryActionVSSD AutomaticRecoveryAction => (AutomaticRecoveryActionVSSD)(ushort)Msvm_VirtualEthernetSwitchSettingData[nameof(AutomaticRecoveryAction)];
        public string RecoveryFile => Msvm_VirtualEthernetSwitchSettingData[nameof(RecoveryFile)] as string;
        public string[] VLANConnection => Msvm_VirtualEthernetSwitchSettingData[nameof(VLANConnection)] as string[];
        public string[] AssociatedResourcePool => Msvm_VirtualEthernetSwitchSettingData[nameof(AssociatedResourcePool)] as string[];
        public uint MaxNumMACAddress => (uint)Msvm_VirtualEthernetSwitchSettingData[nameof(MaxNumMACAddress)];
        public bool IOVPreferred => (bool)Msvm_VirtualEthernetSwitchSettingData[nameof(IOVPreferred)];
        public string[] ExtensionOrder => Msvm_VirtualEthernetSwitchSettingData[nameof(ExtensionOrder)] as string[];
        public BandwidthReservationModeVESSD BandwidthReservationMode => (BandwidthReservationModeVESSD)(uint)Msvm_VirtualEthernetSwitchSettingData[nameof(BandwidthReservationMode)];
        public bool TeamingEnabled => (bool)Msvm_VirtualEthernetSwitchSettingData[nameof(TeamingEnabled)];
        public bool PacketDirectEnabled => (bool)Msvm_VirtualEthernetSwitchSettingData[nameof(PacketDirectEnabled)];

        #endregion

        public ManagementObject ModifyProperties(Dictionary<string, object> Properties)
        {
            var modifiedMsvmVirtualEthernetSwitchSettingData = MsvmVirtualEthernetSwitchSettingData;

            Properties
                .ToList()
                .ForEach((p) => modifiedMsvmVirtualEthernetSwitchSettingData[p.Key] = p.Value);

            return modifiedMsvmVirtualEthernetSwitchSettingData;
        }
        public List<ManagementObject> GetMsvmVirtualSystemSettingDataCollection(string VSSDAssociation)
        {
            return
                VirtualEthernetSwitch?
                    .MsvmVirtualEthernetSwitch.GetRelated(nameof(Msvm_VirtualEthernetSwitchSettingData), VSSDAssociation, null, null, null, null, false, null)
                    .Cast<ManagementObject>()
                    .ToList();
        }
    }
}
