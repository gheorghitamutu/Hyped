using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Viridian.Msvm.Metrics;
using Viridian.Msvm.ResourceManagement;
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
        public enum SnapshotType
        {
            Full = 2,
            Disk = 3,
            Recovery = 32768,
        }
        public enum RequestedInformation      : int
        {
            Name                               = 0,
            ElementName                        = 1,  
            CreationTime                       = 2,  
            Notes                              = 3,  
            NumberOfProcessors                 = 4,  
            ThumbnailImage                     = 5,  
            ThumbnailImageHeight               = 6,  
            ThumbnailImageWidth                = 7,  
            AllocatedGPU                       = 8,  
            VirtualSwitchNames                 = 9,  
            Version                            = 10,   // Added in Windows 10 and Windows Server 2016.
            Shielded                           = 11,   // Added in Windows 10, version 1703 and Windows Server 2016.
            EnabledState                       = 100,
            ProcessorLoad                      = 101,
            ProcessorLoadHistory               = 102,
            MemoryUsage                        = 103,
            Heartbeat                          = 104,
            UpTime                             = 105,
            GuestOperatingSystem               = 106,
            Snapshots                          = 107,
            AsynchronousTasks                  = 108,
            HealthState                        = 109,
            OperationalStatus                  = 110,
            StatusDescriptions                 = 111,
            MemoryAvailable                    = 112,
            AvailableMemoryBuffer              = 113,
            ReplicationMode                    = 114,
            ReplicationState                   = 115,
            ReplicationHealthTestReplicaSystem = 116,
            ApplicationHealth                  = 117,
            ReplicationStateEx                 = 118,
            ReplicationHealthEx                = 119,
            SwapFilesInUse                     = 120,
            IntegrationServicesVersionState    = 121,
            ReplicationProviderId              = 122,
            MemorySpansPhysicalNumaNodes       = 123 
        }
        public enum AutomaticCriticalErrorActionVSSD : ushort
        {
            None = 0,
            PauseResume = 1
        }
        public enum AutomaticRecoveryActionVSSD : ushort
        {
            None = 2,
            Restart = 3,
            RevertToSnapshot = 4
        }
        public enum AutomaticShutdownActionVSSD : ushort
        {
            TurnOff = 2,
            SaveState = 3,
            Shutdown = 4
        }
        public enum AutomaticStartupActionVSSD : ushort
        {
            None = 2,
            RestartIfPreviouslyActive = 3,
            AlwaysStart = 4
        }
        public enum BootOrderVSSD : ushort
        {
            Floppy = 0,
            CDROM = 1,
            IDEHardDrive = 2,
            PXEBoot = 3,
            SCSIHardDrive = 4
        }
        public enum ConsoleModeVSSD : ushort
        {
            Default = 0,
            COM1 = 1,
            COM2 = 2,
            None = 3
        }
        public enum DebugPortEnabledVSSD : ushort
        {
            Off = 0,
            On = 1,
            OnAutoAssigned = 2
        }
        public enum EnhancedSessionTransportTypeVSSD : ushort // This property was added in Windows 10, version 1803.
        {
            VMBusPipe = 0,
            HyperVSocket = 1
        }
        public enum UserSnapshotTypeVSSD : ushort // This property was added in Windows 10, version 1803.
        {
            Disable = 2,
            ProductionFallbackToTest = 3,
            ProductionNoFallback = 4,
            Test = 5
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
        public AutomaticStartupActionVSSD AutomaticStartupAction => (AutomaticStartupActionVSSD)(ushort)MsvmVirtualSystemSettingData[nameof(AutomaticStartupAction)];
        public DateTime AutomaticStartupActionDelay => ManagementDateTimeConverter.ToDateTime(MsvmVirtualSystemSettingData[nameof(AutomaticStartupActionDelay)] as string);
        public ushort AutomaticStartupActionSequenceNumber => (ushort)MsvmVirtualSystemSettingData[nameof(AutomaticStartupActionSequenceNumber)];
        public AutomaticShutdownActionVSSD AutomaticShutdownAction => (AutomaticShutdownActionVSSD)(ushort)MsvmVirtualSystemSettingData[nameof(AutomaticShutdownAction)];
        public AutomaticRecoveryActionVSSD AutomaticRecoveryAction => (AutomaticRecoveryActionVSSD)(ushort)MsvmVirtualSystemSettingData[nameof(AutomaticRecoveryAction)];
        public string RecoveryFile => MsvmVirtualSystemSettingData[nameof(RecoveryFile)] as string;
        public string BIOSGUID => MsvmVirtualSystemSettingData[nameof(BIOSGUID)] as string;
        public string BIOSSerialNumber => MsvmVirtualSystemSettingData[nameof(BIOSSerialNumber)] as string;
        public string BaseBoardSerialNumber => MsvmVirtualSystemSettingData[nameof(BaseBoardSerialNumber)] as string;
        public string ChassisSerialNumber => MsvmVirtualSystemSettingData[nameof(ChassisSerialNumber)] as string;
        public string Architecture => MsvmVirtualSystemSettingData[nameof(Architecture)] as string;
        public string ChassisAssetTag => MsvmVirtualSystemSettingData[nameof(ChassisAssetTag)] as string;
        public bool BIOSNumLock => (bool)MsvmVirtualSystemSettingData[nameof(BIOSNumLock)];
        public BootOrderVSSD[] BootOrder => (BootOrderVSSD[])MsvmVirtualSystemSettingData[nameof(BootOrder)];
        public string Parent => MsvmVirtualSystemSettingData[nameof(Parent)] as string;
        public UserSnapshotTypeVSSD UserSnapshotType => (UserSnapshotTypeVSSD)MsvmVirtualSystemSettingData[nameof(UserSnapshotType)];
        public bool IsSaved => (bool)MsvmVirtualSystemSettingData[nameof(IsSaved)];
        public string AdditionalRecoveryInformation => MsvmVirtualSystemSettingData[nameof(AdditionalRecoveryInformation)] as string;
        public bool AllowFullSCSICommandSet => (bool)MsvmVirtualSystemSettingData[nameof(AllowFullSCSICommandSet)];
        public uint DebugChannelId => (uint)MsvmVirtualSystemSettingData[nameof(DebugChannelId)];
        public DebugPortEnabledVSSD DebugPortEnabled => (DebugPortEnabledVSSD)(ushort)MsvmVirtualSystemSettingData[nameof(DebugPortEnabled)];
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
        public AutomaticCriticalErrorActionVSSD AutomaticCriticalErrorAction => (AutomaticCriticalErrorActionVSSD)(ushort)MsvmVirtualSystemSettingData[nameof(AutomaticCriticalErrorAction)];
        public ConsoleModeVSSD ConsoleMode => (ConsoleModeVSSD)MsvmVirtualSystemSettingData[nameof(ConsoleMode)];
        public bool SecureBootEnabled => (bool)MsvmVirtualSystemSettingData[nameof(SecureBootEnabled)];
        public string SecureBootTemplateId => MsvmVirtualSystemSettingData[nameof(SecureBootTemplateId)] as string;
        public ulong LowMmioGapSize => (ulong)MsvmVirtualSystemSettingData[nameof(LowMmioGapSize)];
        public ulong HighMmioGapSize => (ulong)MsvmVirtualSystemSettingData[nameof(HighMmioGapSize)];
        public EnhancedSessionTransportTypeVSSD EnhancedSessionTransportType => (EnhancedSessionTransportTypeVSSD)MsvmVirtualSystemSettingData[nameof(EnhancedSessionTransportType)];

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

            if (previousBso != null && bootSourceOrder?.Length > previousBso.Length)
                throw new InvalidOperationException("Too many boot devices specified!");

            if (bootSourceOrder.Any(indexBso => previousBso != null && indexBso > previousBso.Length))
                throw new ArgumentOutOfRangeException("Invalid boot device index specified!");

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
            return VirtualSystemManagementService.Instance.GetSummaryInformation(new[] { MsvmVirtualSystemSettingData }, (int[])Enum.GetValues(typeof(RequestedInformation)));
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
        public void CreateSnapshot(SnapshotType snapshotType, bool saveMachineState)
        {
            if (snapshotType == SnapshotType.Recovery && saveMachineState)
                throw new InvalidOperationException("You cannot create a recovery snapshot while the machine is in saved state!");

            var initialState = ComputerSystem.RequestedState;

            if (snapshotType == SnapshotType.Recovery && IncrementalBackupEnabled == false)
                ModifySystemSettings(new Dictionary<string, object>() { { nameof(IncrementalBackupEnabled), true } });

            if (saveMachineState && initialState != VirtualSystemManagementService.RequestedStateVSM.Saved)
                ComputerSystem.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Saved);

            string snapshotSettings = "";

            // Set the SnapshotSettings property. Backup/Recovery snapshots require special settings.
            if (snapshotType == SnapshotType.Recovery)
            {
                using (var vsssd = GetMsvmObject("Msvm_VirtualSystemSnapshotSettingData"))
                    snapshotSettings = vsssd.GetText(TextFormat.CimDtd20);

                // Make sure you activate Volume Shadow Copy service on Guest and install KB3063109.
                // https://support.microsoft.com/en-us/help/3063109/hyper-v-integration-components-update-for-windows-virtual-machines
                // https://thewincentral.com/how-to-install-cab-files-on-windows-10-for-cumulative-updates

                // Time Synchronization The protocol version of the component installed in the virtual machine does not match the version expected by the hosting system.
                // https://support.microsoft.com/en-us/help/4014894/vm-integration-services-status-reports-protocol-version-mismatch-on-pr

                // You cannot save actual ram state of the machine with backup/recovery checkpoints; State Saved doesn't make sense then.
            }

            VirtualSystemSnapshotService.Instance.CreateSnapshot(ComputerSystem.MsvmComputerSystem.Path.Path, snapshotSettings, (ushort)snapshotType);

            if (saveMachineState && initialState != VirtualSystemManagementService.RequestedStateVSM.Saved)
                ComputerSystem.RequestStateChange(initialState);
        }
        public void SetBaseMetricsForEthernetSwitchPortAclSettingData(MetricService.MetricCollectionEnabled operation)
        {
            MsvmVirtualSystemSettingData.GetRelated("Msvm_SyntheticEthernetPortSettingData")
                    .Cast<ManagementObject>()
                    .ToList()
                    .ForEach((sepsd) =>
                        SyntheticEthernetAdapter.GetEthernetSwitchPortAclSettingData(
                            SyntheticEthernetAdapter.GetEthernetPortAllocationSettingData(sepsd, Scope.Virtualization.SpecificScope))
                                .ForEach((espasd) =>
                                    MetricService.GetAllBaseMetricDefinitions(espasd)
                                        .ForEach((baseMetricDef) =>
                                            MetricService.Instance.SetBaseMetric(espasd, baseMetricDef, operation))));
        }
        public void SetAggregationMetricsForEthernetSwitchPortAclSettingData(MetricService.MetricCollectionEnabled operation)
        {
            MsvmVirtualSystemSettingData.GetRelated("Msvm_SyntheticEthernetPortSettingData")
                .Cast<ManagementObject>()
                .ToList()
                .ForEach((sepsd) =>
                    SyntheticEthernetAdapter.GetEthernetSwitchPortAclSettingData(
                        SyntheticEthernetAdapter.GetEthernetPortAllocationSettingData(sepsd, Scope.Virtualization.SpecificScope))
                            .ForEach((espasd) =>
                                MetricService.Instance.SetAllMetrics(espasd, operation)));
        }
        public List<ManagementObject> GetResourceAllocationSettingData(string ResourceType, string ResourceSubType)
        {
            return
                MsvmVirtualSystemSettingData
                    .GetRelated("Msvm_ResourceAllocationSettingData")
                        .Cast<ManagementObject>()
                        .Where((c) =>
                            c[nameof(ResourceType)]?.ToString() == ResourceType &&
                            c[nameof(ResourceSubType)]?.ToString() == ResourceSubType)
                        .ToList();
        }
        public ManagementObject GetScsiController(int index)
        {
            return
                GetResourceAllocationSettingData(ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceType, ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceSubType)
                    .Skip(index)
                    .First();
        }
        public void SetAggregationMetricsForDrives(MetricService.MetricCollectionEnabled operation)
        {
            GetResourceAllocationSettingData(ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceType, ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceSubType)
                .Cast<ManagementObject>()
                .ToList()
                .ForEach(
                    (controller) =>
                        controller.GetRelated("Msvm_ResourceAllocationSettingData", null, null, null, "Dependent", "Antecedent", false, null)
                            .Cast<ManagementObject>()
                            .ToList()
                            .ForEach((setting) => MetricService.Instance.SetAllMetrics(setting, operation)));
        }
        public void ApplySnapshot(string ElementName)
        {
            // In order to apply a snapshot, the virtual machine must first be saved/off
            if (ComputerSystem.EnabledState != ComputerSystem.EnabledStateVM.Disabled)
                ComputerSystem.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Off);

            VirtualSystemSnapshotService.Instance.ApplySnapshot(GetSnapshot(ElementName).MsvmVirtualSystemSettingData);
        }
        public List<ManagementObject> GetEthernetPortAllocationSettingDataList()
        {
            return 
                MsvmVirtualSystemSettingData.GetRelated("Msvm_EthernetPortAllocationSettingData", "Msvm_VirtualSystemSettingDataComponent", null, null, null, null, false, null)
                .Cast<ManagementObject>()
                .ToList();
        }
        private static ManagementObject GetMsvmObject(string serviceName)
        {
            using (var serviceClass = new ManagementClass(Scope.Virtualization.SpecificScope, new ManagementPath(serviceName), null))
                return serviceClass.GetInstances().Cast<ManagementObject>().First();
        }
    }
}
