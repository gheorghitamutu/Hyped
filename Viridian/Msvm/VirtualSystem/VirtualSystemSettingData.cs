using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Viridian.Exceptions;
using Viridian.Msvm.VirtualSystemManagement;
using Viridian.Resources.Network;
using Viridian.Scopes;

namespace Viridian.Msvm.VirtualSystem
{
    public sealed class VirtualSystemSettingData
    {
        private ManagementObject Msvm_VirtualSystemSettingData = null;
        private ComputerSystem ComputerSystem { set; get; }
        public string VSSDAssociation { private set; get; }
        private Dictionary<string, object> Attributes { set; get; }

        public ManagementObject MsvmVirtualSystemSettingData
        {
            get
            {
                if (Attributes == null)
                    Msvm_VirtualSystemSettingData = GetMsvmVirtualSystemSettingDataCollection(VSSDAssociation).First();
                else
                    Msvm_VirtualSystemSettingData =
                        GetMsvmVirtualSystemSettingDataCollection(VSSDAssociation)
                            .Where((vssd) => Attributes.Where((pair) => vssd.Properties[pair.Key].Value.Equals(pair.Value)).ToList().Count == Attributes.Count)
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

        public VirtualSystemSettingData(ComputerSystem ComputerSystem, string VSSDAssociation, Dictionary<string, object> Attributes = null)
        {
            this.ComputerSystem = ComputerSystem;
            this.VSSDAssociation = VSSDAssociation;
            this.Attributes = Attributes;
        }

        public enum NetworkBootPreferredProtocolVSSD
        {
            IPv4 = 4096,
            IPv6 = 4097
        }

        #region MsvmProperties

        public string InstanceID => MsvmVirtualSystemSettingData[nameof(InstanceID)] as string;
        public string Caption => MsvmVirtualSystemSettingData[nameof(Caption)] as string;
        public string Description => MsvmVirtualSystemSettingData[nameof(Description)] as string;
        public string ElementName => MsvmVirtualSystemSettingData[nameof(ElementName)] as string;
        public string VirtualSystemIdentifier => MsvmVirtualSystemSettingData[nameof(VirtualSystemIdentifier)] as string;
        public string VirtualSystemType => MsvmVirtualSystemSettingData[nameof(VirtualSystemType)] as string;
        public string[] Notes => MsvmVirtualSystemSettingData[nameof(Notes)] as string[];
        public DateTime CreationTime => ManagementDateTimeConverter.ToDateTime(MsvmVirtualSystemSettingData[nameof(CreationTime)] as string);
        public string ConfigurationID => MsvmVirtualSystemSettingData[nameof(ConfigurationID)] as string;
        public string ConfigurationDataRoot => MsvmVirtualSystemSettingData[nameof(ConfigurationDataRoot)] as string;
        public string ConfigurationFile => MsvmVirtualSystemSettingData[nameof(ConfigurationFile)] as string;
        public string SnapshotDataRoot => MsvmVirtualSystemSettingData[nameof(SnapshotDataRoot)] as string;
        public string SuspendDataRoot => MsvmVirtualSystemSettingData[nameof(SuspendDataRoot)] as string;
        public string SwapFileDataRoot => MsvmVirtualSystemSettingData[nameof(SwapFileDataRoot)] as string;
        public string LogDataRoot => MsvmVirtualSystemSettingData[nameof(LogDataRoot)] as string;
        public ushort AutomaticStartupAction => (ushort)MsvmVirtualSystemSettingData[nameof(AutomaticStartupAction)];
        public DateTime AutomaticStartupActionDelay => ManagementDateTimeConverter.ToDateTime(MsvmVirtualSystemSettingData[nameof(AutomaticStartupActionDelay)] as string);
        public ushort AutomaticStartupActionSequenceNumber => (ushort)MsvmVirtualSystemSettingData[nameof(AutomaticStartupActionSequenceNumber)];
        public ushort AutomaticShutdownAction => (ushort)MsvmVirtualSystemSettingData[nameof(AutomaticShutdownAction)];
        public ushort AutomaticRecoveryAction => (ushort)MsvmVirtualSystemSettingData[nameof(AutomaticRecoveryAction)];
        public string RecoveryFile => MsvmVirtualSystemSettingData[nameof(RecoveryFile)] as string;
        public string BIOSGUID => MsvmVirtualSystemSettingData[nameof(BIOSGUID)] as string;
        public string BIOSSerialNumber => MsvmVirtualSystemSettingData[nameof(BIOSSerialNumber)] as string;
        public string BaseBoardSerialNumber => MsvmVirtualSystemSettingData[nameof(BaseBoardSerialNumber)] as string;
        public string ChassisSerialNumber => MsvmVirtualSystemSettingData[nameof(ChassisSerialNumber)] as string;
        public string Architecture => MsvmVirtualSystemSettingData[nameof(Architecture)] as string;
        public string ChassisAssetTag => MsvmVirtualSystemSettingData[nameof(ChassisAssetTag)] as string;
        public bool BIOSNumLock => (bool)MsvmVirtualSystemSettingData[nameof(BIOSNumLock)];
        public ushort[] BootOrder => (ushort[])MsvmVirtualSystemSettingData[nameof(BootOrder)];
        public string Parent => MsvmVirtualSystemSettingData[nameof(Parent)] as string;
        public ushort UserSnapshotType => (ushort)MsvmVirtualSystemSettingData[nameof(UserSnapshotType)];
        public bool IsSaved => (bool)MsvmVirtualSystemSettingData[nameof(IsSaved)];
        public string AdditionalRecoveryInformation => MsvmVirtualSystemSettingData[nameof(AdditionalRecoveryInformation)] as string;
        public bool AllowFullSCSICommandSet => (bool)MsvmVirtualSystemSettingData[nameof(AllowFullSCSICommandSet)];
        public uint DebugChannelId => (uint)MsvmVirtualSystemSettingData[nameof(DebugChannelId)];
        public ushort DebugPortEnabled => (ushort)MsvmVirtualSystemSettingData[nameof(DebugPortEnabled)];
        public uint DebugPort => (uint)MsvmVirtualSystemSettingData[nameof(DebugPort)];
        public string Version => MsvmVirtualSystemSettingData[nameof(Version)] as string;
        public bool IncrementalBackupEnabled => (bool)MsvmVirtualSystemSettingData[nameof(IncrementalBackupEnabled)];
        public bool VirtualNumaEnabled => (bool)MsvmVirtualSystemSettingData[nameof(VirtualNumaEnabled)];
        public bool AllowReducedFcRedundancy => (bool)MsvmVirtualSystemSettingData[nameof(AllowReducedFcRedundancy)];
        public string VirtualSystemSubType => MsvmVirtualSystemSettingData[nameof(VirtualSystemSubType)] as string;
        public string[] BootSourceOrder => MsvmVirtualSystemSettingData[nameof(BootSourceOrder)] as string[];
        public bool PauseAfterBootFailure => (bool)MsvmVirtualSystemSettingData[nameof(PauseAfterBootFailure)];
        public NetworkBootPreferredProtocolVSSD NetworkBootPreferredProtocol => (NetworkBootPreferredProtocolVSSD)(ushort)MsvmVirtualSystemSettingData[nameof(NetworkBootPreferredProtocol)];
        public bool GuestControlledCacheTypes => (bool)MsvmVirtualSystemSettingData[nameof(GuestControlledCacheTypes)];
        public bool AutomaticSnapshotsEnabled => (bool)MsvmVirtualSystemSettingData[nameof(AutomaticSnapshotsEnabled)];
        public bool IsAutomaticSnapshot => (bool)MsvmVirtualSystemSettingData[nameof(IsAutomaticSnapshot)];
        public string GuestStateFile => MsvmVirtualSystemSettingData[nameof(GuestStateFile)] as string;
        public string GuestStateDataRoot => MsvmVirtualSystemSettingData[nameof(GuestStateDataRoot)] as string;
        public bool LockOnDisconnect => (bool)MsvmVirtualSystemSettingData[nameof(LockOnDisconnect)];
        public string ParentPackage => MsvmVirtualSystemSettingData[nameof(ParentPackage)] as string;
        public DateTime AutomaticCriticalErrorActionTimeout => ManagementDateTimeConverter.ToDateTime(MsvmVirtualSystemSettingData[nameof(AutomaticCriticalErrorActionTimeout)] as string);
        public ushort AutomaticCriticalErrorAction => (ushort)MsvmVirtualSystemSettingData[nameof(AutomaticCriticalErrorAction)];
        public ushort ConsoleMode => (ushort)MsvmVirtualSystemSettingData[nameof(ConsoleMode)];
        public bool SecureBootEnabled => (bool)MsvmVirtualSystemSettingData[nameof(SecureBootEnabled)];
        public string SecureBootTemplateId => MsvmVirtualSystemSettingData[nameof(SecureBootTemplateId)] as string;
        public ulong LowMmioGapSize => (ulong)MsvmVirtualSystemSettingData[nameof(LowMmioGapSize)];
        public ulong HighMmioGapSize => (ulong)MsvmVirtualSystemSettingData[nameof(HighMmioGapSize)];
        public ushort EnhancedSessionTransportType => (ushort)MsvmVirtualSystemSettingData[nameof(EnhancedSessionTransportType)];

        #endregion

        public void ModifySystemSettings(Dictionary<string, object> Properties)
        {
            var newMsvmVirtualSystemSettingData = MsvmVirtualSystemSettingData;

            Properties
                .ToList()
                .ForEach((p) => newMsvmVirtualSystemSettingData[p.Key] = p.Value);

            VirtualSystemManagementService.Instance.ModifySystemSettings(newMsvmVirtualSystemSettingData.GetText(TextFormat.WmiDtd20));
        }
        public VirtualSystemSettingData GetSnapshot(string ElementName)
        {
            return 
                new VirtualSystemSettingData(
                    ComputerSystem, 
                    Properties.VirtualSystemSettingData.Default.Msvm_SnapshotOfVirtualSystem,
                    new Dictionary<string, object>() { { nameof(ElementName), ElementName } });
        }
        public VirtualSystemSettingData GetLastAppliedSnapshot()
        {
            return new VirtualSystemSettingData(ComputerSystem, Properties.VirtualSystemSettingData.Default.Msvm_LastAppliedSnapshot, null);
        }
        public VirtualSystemSettingData GetLastCreatedSnapshot()
        {
            return new VirtualSystemSettingData(ComputerSystem, Properties.VirtualSystemSettingData.Default.Msvm_SnapshotOfVirtualSystem, null);
        }
        public List<ManagementObject> GetSnapshotList()
        {
            return GetMsvmVirtualSystemSettingDataCollection(Properties.VirtualSystemSettingData.Default.Msvm_SnapshotOfVirtualSystem);
        }
        public List<ManagementObject> GetMsvmVirtualSystemSettingDataCollection(string VSSDAssociation)
        {
            return
                ComputerSystem?
                    .MsvmComputerSystem.GetRelated(nameof(Msvm_VirtualSystemSettingData), VSSDAssociation, null, null, null, null, false, null)
                    .Cast<ManagementObject>()
                    .ToList();
        }
        public ManagementObject GetMemorySettingData()
        {
            using (var vssd = ComputerSystem.MsvmComputerSystem.GetRelated(nameof(Msvm_VirtualSystemSettingData), Properties.VirtualSystemSettingData.Default.Msvm_SettingsDefineState, null, null, "SettingData", "ManagedElement", false, null).Cast<ManagementObject>().First())
                return vssd.GetRelated("Msvm_MemorySettingData").Cast<ManagementObject>().First();
        }

        public void SetBootOrderFromDevicePath(string devicePath)
        {
            if (BootSourceOrder is string[] prevBootOrder)
            {
                var bso = new string[prevBootOrder.Length];

                var index = 1;
                foreach (var bs in prevBootOrder)
                    using (var entry = new ManagementObject(new ManagementPath(bs)))
                    {
                        var fdp = entry["FirmwareDevicePath"] as string;

                        if (string.Equals(devicePath, fdp, StringComparison.OrdinalIgnoreCase))
                            bso[0] = bs;
                        else
                            bso[index++] = bs;
                    }

                ModifySystemSettings(new Dictionary<string, object>() { { nameof(BootSourceOrder), bso } });
            }
        }
        public void SetBootOrderByIndex(uint[] bootSourceOrder)
        {
            var previousBso = BootSourceOrder as string[];

            if (previousBso != null && bootSourceOrder.Length > previousBso.Length)
                throw new ViridianException("Too many boot devices specified!");

            if (bootSourceOrder.Any(indexBso => previousBso != null && indexBso > previousBso.Length))
                throw new ViridianException("Invalid boot device index specified!");

            if (previousBso != null)
            {
                var newBso = new string[previousBso.Length];

                uint countReorderedBootSources = 0;

                foreach (var i in bootSourceOrder)
                    newBso[countReorderedBootSources++] = previousBso[i];

                for (uint i = 0; i < previousBso.Length; i++)
                {
                    var isReordered = bootSourceOrder.Any(reorderedIndex => i == reorderedIndex);

                    if (!isReordered)
                        newBso[countReorderedBootSources++] = previousBso[i];
                }

                Msvm_VirtualSystemSettingData[nameof(BootSourceOrder)] = newBso;
            }

            VirtualSystemManagementService.Instance.ModifySystemSettings(Msvm_VirtualSystemSettingData.GetText(TextFormat.WmiDtd20));
        }
        public ManagementBaseObject[] GetSummaryInformation()
        {
            var requestedInformation = new int[]
            {
                    0,      // Name
                    1,      // ElementName
                    2,      // CreationTime
                    3,      // Notes
                    4,      // NumberOfProcessors
                    5,      // ThumbnailImage
                    6,      // ThumbnailImageHeight
                    7,      // ThumbnailImageWidth
                    8,      // AllocatedGPU
                    9,      // VirtualSwitchNames 
                    10,     // Version | Added in Windows 10 and Windows Server 2016.
                    11,     // Shielded | Added in Windows 10, version 1703 and Windows Server 2016.
                    100,    // EnabledState
                    101,    // ProcessorLoad
                    102,    // ProcessorLoadHistory
                    103,    // MemoryUsage
                    104,    // Heartbeat
                    105,    // UpTime
                    106,    // GuestOperatingSystem
                    107,    // Snapshots
                    108,    // AsynchronousTasks
                    109,    // HealthState
                    110,    // OperationalStatus
                    111,    // StatusDescriptions
                    112,    // MemoryAvailable
                    113,    // AvailableMemoryBuffer
                    114,    // Replication Mode
                    115,    // Replication State
                    116,    // Replication HealthTest Replica System
                    117,    // Application Health 
                    118,    // ReplicationStateEx 
                    119,    // ReplicationHealthEx 
                    120,    // SwapFilesInUse 
                    121,    // IntegrationServicesVersionState 
                    122,    // ReplicationProviderId 
                    123     // MemorySpansPhysicalNumaNodes 
            };

            return VirtualSystemManagementService.Instance.GetSummaryInformation(new[] { MsvmVirtualSystemSettingData }, requestedInformation);
        }
        public void ConnectVmToSwitch(string switchName, string adapterName)
        {
            using (var ves = NetSwitch.FindVirtualEthernetSwitch(Scope.Virtualization.SpecificScope, switchName))
            using (var syntheticAdapter = SyntheticEthernetAdapter.AddSyntheticAdapter(ComputerSystem, adapterName))
            using (var epasd = NetSwitch.GetDefaultEthernetPortAllocationSettingData())
            {
                epasd["Parent"] = syntheticAdapter.Path.Path;
                epasd["HostResource"] = new string[] { ves.Path.Path };

                VirtualSystemManagementService.Instance.AddResourceSettings(MsvmVirtualSystemSettingData, new string[] { epasd.GetText(TextFormat.WmiDtd20) });
            }
        }
        public List<ManagementObject> GetEthernetSwitchPortAclSettingDatas()
        {
            var aclSettingDataList = new List<ManagementObject>();

            MsvmVirtualSystemSettingData.GetRelated("Msvm_SyntheticEthernetPortSettingData")
                    .Cast<ManagementObject>()
                    .ToList()
                    .ForEach((sepsd) =>
                        aclSettingDataList.AddRange(
                            SyntheticEthernetAdapter.GetEthernetSwitchPortAclSettingData(
                                SyntheticEthernetAdapter.GetEthernetPortAllocationSettingData(sepsd, Scope.Virtualization.SpecificScope))
                                    .Cast<ManagementObject>()
                                    .ToList()));

            return aclSettingDataList;
        }
    }
}
