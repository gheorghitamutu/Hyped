using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Viridian.Msvm.VirtualSystemManagement;

namespace Viridian.Msvm.VirtualSystem
{
    public sealed class VirtualSystemSettingData
    {
        private ManagementObject Msvm_VirtualSystemSettingData = null;
        private ComputerSystem ComputerSystem { set; get; }

        public ManagementObject MsvmVirtualSystemSettingData
        {
            get
            {
                Msvm_VirtualSystemSettingData =
                    ComputerSystem?
                        .MsvmComputerSystem.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null)
                        .Cast<ManagementObject>()
                        .First();

                return Msvm_VirtualSystemSettingData;
            }

            set
            {
                if (Msvm_VirtualSystemSettingData != null)
                    Msvm_VirtualSystemSettingData.Dispose();

                Msvm_VirtualSystemSettingData = value;
            }
        }

        public VirtualSystemSettingData(ComputerSystem ComputerSystem)
        {
            this.ComputerSystem = ComputerSystem;
        }

        public enum NetworkBootPreferredProtocolVSSD
        {
            IPv4 = 4096,
            IPv6 = 4097
        }

        #region MsvmProperties

        string InstanceID => MsvmVirtualSystemSettingData[nameof(InstanceID)] as string;
        string Caption => MsvmVirtualSystemSettingData[nameof(Caption)] as string;
        string Description => MsvmVirtualSystemSettingData[nameof(Description)] as string;
        string ElementName => MsvmVirtualSystemSettingData[nameof(ElementName)] as string;
        string VirtualSystemIdentifier => MsvmVirtualSystemSettingData[nameof(VirtualSystemIdentifier)] as string;
        string VirtualSystemType => MsvmVirtualSystemSettingData[nameof(VirtualSystemType)] as string;
        string[] Notes => MsvmVirtualSystemSettingData[nameof(Notes)] as string[];
        DateTime CreationTime => ManagementDateTimeConverter.ToDateTime(MsvmVirtualSystemSettingData[nameof(CreationTime)] as string);
        string ConfigurationID => MsvmVirtualSystemSettingData[nameof(ConfigurationID)] as string;
        string ConfigurationDataRoot => MsvmVirtualSystemSettingData[nameof(ConfigurationDataRoot)] as string;
        string ConfigurationFile => MsvmVirtualSystemSettingData[nameof(ConfigurationFile)] as string;
        string SnapshotDataRoot => MsvmVirtualSystemSettingData[nameof(SnapshotDataRoot)] as string;
        string SuspendDataRoot => MsvmVirtualSystemSettingData[nameof(SuspendDataRoot)] as string;
        string SwapFileDataRoot => MsvmVirtualSystemSettingData[nameof(SwapFileDataRoot)] as string;
        string LogDataRoot => MsvmVirtualSystemSettingData[nameof(LogDataRoot)] as string;
        ushort AutomaticStartupAction => (ushort)MsvmVirtualSystemSettingData[nameof(AutomaticStartupAction)];
        DateTime AutomaticStartupActionDelay => ManagementDateTimeConverter.ToDateTime(MsvmVirtualSystemSettingData[nameof(AutomaticStartupActionDelay)] as string);
        ushort AutomaticStartupActionSequenceNumber => (ushort)MsvmVirtualSystemSettingData[nameof(AutomaticStartupActionSequenceNumber)];
        ushort AutomaticShutdownAction => (ushort)MsvmVirtualSystemSettingData[nameof(AutomaticShutdownAction)];
        ushort AutomaticRecoveryAction => (ushort)MsvmVirtualSystemSettingData[nameof(AutomaticRecoveryAction)];
        string RecoveryFile => MsvmVirtualSystemSettingData[nameof(RecoveryFile)] as string;
        string BIOSGUID => MsvmVirtualSystemSettingData[nameof(BIOSGUID)] as string;
        string BIOSSerialNumber => MsvmVirtualSystemSettingData[nameof(BIOSSerialNumber)] as string;
        string BaseBoardSerialNumber => MsvmVirtualSystemSettingData[nameof(BaseBoardSerialNumber)] as string;
        string ChassisSerialNumber => MsvmVirtualSystemSettingData[nameof(ChassisSerialNumber)] as string;
        string Architecture => MsvmVirtualSystemSettingData[nameof(Architecture)] as string;
        string ChassisAssetTag => MsvmVirtualSystemSettingData[nameof(ChassisAssetTag)] as string;
        bool BIOSNumLock => (bool)MsvmVirtualSystemSettingData[nameof(BIOSNumLock)];
        ushort[] BootOrder => (ushort[])MsvmVirtualSystemSettingData[nameof(BootOrder)];
        string Parent => MsvmVirtualSystemSettingData[nameof(Parent)] as string;
        ushort UserSnapshotType => (ushort)MsvmVirtualSystemSettingData[nameof(UserSnapshotType)];
        bool IsSaved => (bool)MsvmVirtualSystemSettingData[nameof(IsSaved)];
        string AdditionalRecoveryInformation => MsvmVirtualSystemSettingData[nameof(AdditionalRecoveryInformation)] as string;
        bool AllowFullSCSICommandSet => (bool)MsvmVirtualSystemSettingData[nameof(AllowFullSCSICommandSet)];
        uint DebugChannelId => (uint)MsvmVirtualSystemSettingData[nameof(DebugChannelId)];
        ushort DebugPortEnabled => (ushort)MsvmVirtualSystemSettingData[nameof(DebugPortEnabled)];
        uint DebugPort => (uint)MsvmVirtualSystemSettingData[nameof(DebugPort)];
        string Version => MsvmVirtualSystemSettingData[nameof(Version)] as string;
        public bool IncrementalBackupEnabled => (bool)MsvmVirtualSystemSettingData[nameof(IncrementalBackupEnabled)];
        bool VirtualNumaEnabled => (bool)MsvmVirtualSystemSettingData[nameof(VirtualNumaEnabled)];
        bool AllowReducedFcRedundancy => (bool)MsvmVirtualSystemSettingData[nameof(AllowReducedFcRedundancy)];
        string VirtualSystemSubType => MsvmVirtualSystemSettingData[nameof(VirtualSystemSubType)] as string;
        public string[] BootSourceOrder => MsvmVirtualSystemSettingData[nameof(BootSourceOrder)] as string[];
        public bool PauseAfterBootFailure => (bool)MsvmVirtualSystemSettingData[nameof(PauseAfterBootFailure)];
        public NetworkBootPreferredProtocolVSSD NetworkBootPreferredProtocol => (NetworkBootPreferredProtocolVSSD)(ushort)MsvmVirtualSystemSettingData[nameof(NetworkBootPreferredProtocol)];
        bool GuestControlledCacheTypes => (bool)MsvmVirtualSystemSettingData[nameof(GuestControlledCacheTypes)];
        bool AutomaticSnapshotsEnabled => (bool)MsvmVirtualSystemSettingData[nameof(AutomaticSnapshotsEnabled)];
        bool IsAutomaticSnapshot => (bool)MsvmVirtualSystemSettingData[nameof(IsAutomaticSnapshot)];
        string GuestStateFile => MsvmVirtualSystemSettingData[nameof(GuestStateFile)] as string;
        string GuestStateDataRoot => MsvmVirtualSystemSettingData[nameof(GuestStateDataRoot)] as string;
        bool LockOnDisconnect => (bool)MsvmVirtualSystemSettingData[nameof(LockOnDisconnect)];
        string ParentPackage => MsvmVirtualSystemSettingData[nameof(ParentPackage)] as string;
        DateTime AutomaticCriticalErrorActionTimeout => ManagementDateTimeConverter.ToDateTime(MsvmVirtualSystemSettingData[nameof(AutomaticCriticalErrorActionTimeout)] as string);
        ushort AutomaticCriticalErrorAction => (ushort)MsvmVirtualSystemSettingData[nameof(AutomaticCriticalErrorAction)];
        ushort ConsoleMode => (ushort)MsvmVirtualSystemSettingData[nameof(ConsoleMode)];
        public bool SecureBootEnabled => (bool)MsvmVirtualSystemSettingData[nameof(SecureBootEnabled)];
        string SecureBootTemplateId => MsvmVirtualSystemSettingData[nameof(SecureBootTemplateId)] as string;
        ulong LowMmioGapSize => (ulong)MsvmVirtualSystemSettingData[nameof(LowMmioGapSize)];
        ulong HighMmioGapSize => (ulong)MsvmVirtualSystemSettingData[nameof(HighMmioGapSize)];
        ushort EnhancedSessionTransportType => (ushort)MsvmVirtualSystemSettingData[nameof(EnhancedSessionTransportType)];

        #endregion

        public void ModifySystemSettings(Dictionary<string, object> Properties)
        {
            var newMsvmVirtualSystemSettingData = MsvmVirtualSystemSettingData;

            Properties
                .ToList()
                .ForEach((p) => newMsvmVirtualSystemSettingData[p.Key] = p.Value);

            VirtualSystemManagementService.Instance.ModifySystemSettings(newMsvmVirtualSystemSettingData.GetText(TextFormat.WmiDtd20));
        }
    }
}
