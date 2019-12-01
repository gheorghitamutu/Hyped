using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Resources.Drives;
using Viridian.Resources.Msvm;
using Viridian.Resources.Network;
using Viridian.Service.Msvm;
using Viridian.Statistics;
using Viridian.Utilities;

namespace Viridian.Machine
{
    public class VM
    {
        private const string serverName = ".";
        private const string scopePath = @"\Root\Virtualization\V2";
        private const string VirtualSystemSubtype = "Microsoft:Hyper-V:SubType:2";
        private ManagementObject Msvm_ComputerSystem = null; // don't directly use it unless explicitly required (Name property)!

        public ManagementScope Scope { get; private set; }

        public string VmName { get; private set; }

        public VM(string ElementName)
        {
            Scope = Utils.GetScope(serverName, scopePath);
            VmName = ElementName;

            Define();
        }

        public ManagementObject MsvmComputerSystem
        {
            get
            {
                if (Name == null && Msvm_ComputerSystem == null)
                    return null;

                if (Name == null)
                    using (var mos = new ManagementObjectSearcher(Scope, new ObjectQuery("SELECT * FROM Msvm_ComputerSystem")))
                        return mos.Get().Cast<ManagementObject>().Where((c) => c[nameof(ElementName)]?.ToString() == VmName).FirstOrDefault();

                using (var mos = new ManagementObjectSearcher(Scope, new ObjectQuery("SELECT * FROM Msvm_ComputerSystem")))
                    return mos.Get().Cast<ManagementObject>().Where((c) => c[nameof(Name)]?.ToString() == Name).FirstOrDefault();
            }

            set
            {
                if (Msvm_ComputerSystem != null)
                    Msvm_ComputerSystem.Dispose();

                Msvm_ComputerSystem = value;
            }
        }

        private void Define()
        {
            if (MsvmComputerSystem == null)
            {
                using (var mos = new ManagementObjectSearcher(Scope, new ObjectQuery("SELECT * FROM Msvm_ComputerSystem")))
                    Msvm_ComputerSystem = mos.Get().Cast<ManagementObject>().Where((c) => c[nameof(ElementName)]?.ToString() == VmName).FirstOrDefault();

                if (MsvmComputerSystem == null)
                {
                    using (var vssd = Utils.GetServiceObject(Scope, "Msvm_VirtualSystemSettingData"))
                    {
                        vssd[nameof(ElementName)] = VmName;
                        vssd["ConfigurationDataRoot"] = @"C:\ProgramData\Microsoft\Windows\Hyper-V\";
                        vssd[nameof(VirtualSystemSubtype)] = VirtualSystemSubtype;

                        MsvmComputerSystem = VirtualSystemManagement.Instance.DefineSystem(vssd.GetText(TextFormat.WmiDtd20), null, null);
                    }
                }
            }
        }

        #region Enums

        public enum EnabledStateVM : uint
        {
            Unknown = 0,
            Other = 1,
            Enabled = 2,
            Disabled = 3,
            ShuttingDown = 4,
            NotApplicable = 5,
            EnabledButOffline = 6,
            InTest = 7,
            Deferred = 8,
            Quiesce = 9,
            Starting = 10
        }
        public enum SnapshotType
        {
            Full = 2,
            Disk = 3,
            Recovery = 32768,
        }
        public static class VirtualSystemTypeName
        {
            public const string RealizedVm = "Microsoft:Hyper-V:System:Realized";
            public const string PlannedVm = "Microsoft:Hyper-V:System:Planned";
            public const string RealizedSnapshot = "Microsoft:Hyper-V:Snapshot:Realized";
            public const string RecoverySnapshot = "Microsoft:Hyper-V:Snapshot:Recovery";
            public const string PlannedSnapshot = "Microsoft:Hyper-V:Snapshot:Planned";
            public const string MissingSnapshot = "Microsoft:Hyper-V:Snapshot:Missing";
            public const string ReplicaStandardRecoverySnapshot = "Microsoft:Hyper-V:Snapshot:Replica:Standard";
            public const string ReplicaApplicationConsistentRecoverySnapshot = "Microsoft:Hyper-V:Snapshot:Replica:ApplicationConsistent";
            public const string ReplicaPlannedRecoverySnapshot = "Microsoft:Hyper-V:Snapshot:Replica:PlannedFailover";
            public const string ReplicaSettings = "Microsoft:Hyper-V:Replica";
        }
        public enum NetworkBootPreferredProtocol
        {
            IPv4 = 4096,
            IPv6 = 4097
        }
        public class DescriptionInfo
        {
            private DescriptionInfo(string Name)
            {
                this.Name = Name;
            }

            public string Name { get; set; }

            public static DescriptionInfo MicrosoftVirtualComputerSystem => new DescriptionInfo("Microsoft Virtual Computer System");
            public static DescriptionInfo MicrosoftHostingComputerSystem => new DescriptionInfo("Microsoft Hosting Computer System");
        }

        public void AddToSyntheticDiskDrive(object vm, string vhdxName, int v1, int v2, VHD.HardDiskAccess readWrite)
        {
            throw new NotImplementedException();
        }

        public enum EnabledDefaultVM : ushort
        {
            Enabled = 2,
            Disabled = 3,
            EnabledButOffline = 6
        }
        public enum EnhancedSessionModeStateVM : ushort
        {
            AllowedAndAvailable = 2,
            NotAllowed = 3,
            AllowedButNotAvailable = 6
        }
        public enum FailedOverReplicationTypeVM : ushort
        {
            None = 0,
            Regular = 1,
            ApplicationConsistent = 2,
            Planned = 3
        }
        public enum HealthStateVM : ushort
        {
            OK = 5,
            MajorFailure = 20,
            CriticalFailure = 25
        }
        public enum LastReplicationTypeVM : ushort
        {
            None = 0,
            Regular = 1,
            ApplicationConsistent = 2,
            Planned = 3
        }
        public enum OperationalStatusVM : ushort
        {
            OK = 2,
            Degraded = 3,
            PredictiveFailure = 5,
            Stopped = 10,
            InService = 11,
            Dormant = 15,
            CreatingSnapshot = 32768, // values below are for OperationalStatus[1]
            ApplyingSnapshot = 32769,
            DeletingSnapshot = 32770,
            WaitingToStart = 32771,
            MergingDisks = 32772,
            ExportingVirtualMachine = 32773,
            MigratingVirtualMachine = 32774
        }
        public enum ReplicationHealthVM : ushort
        {
            NotApplicable = 0,
            Ok = 1,
            Warning = 2,
            Critical = 3
        }
        public enum ReplicationModeVM : ushort
        {
            None = 0,
            Primary = 1,
            Replica = 2,
            TestReplica = 3,
            ExtendedReplica = 4
        }
        public enum ReplicationStateVM : ushort
        {
            Disabled = 0,
            ReadyForReplication = 1,
            WaitingToCompleteInitialReplication = 2,
            Replicating = 3,
            SyncedReplicationComplete = 4,
            Recovered = 5,
            Committed = 6,
            Suspended = 7,
            Critical = 8,
            WaitingToStartResynchronization = 9,
            Resynchronizing = 10,
            ResynchronizationSuspended = 11,
            FailoverInProgress = 12,
            FailbackInProgress = 13,
            FailbackComplete = 14
        }

        #endregion

        #region MsvmProperties

        string InstanceID => MsvmComputerSystem[nameof(InstanceID)].ToString();
        string Caption => MsvmComputerSystem[nameof(Caption)].ToString();
        string Description => MsvmComputerSystem[nameof(Description)].ToString();
        string ElementName => MsvmComputerSystem[nameof(ElementName)].ToString();
        DateTime InstallDate => ManagementDateTimeConverter.ToDateTime(MsvmComputerSystem[nameof(InstallDate)].ToString());
        OperationalStatusVM[] OperationalStatus => (OperationalStatusVM[])MsvmComputerSystem[nameof(OperationalStatus)];
        string[] StatusDescriptions => (string[])MsvmComputerSystem[nameof(StatusDescriptions)];
        string Status => MsvmComputerSystem[nameof(Status)].ToString();
        HealthStateVM HealthState => (HealthStateVM)MsvmComputerSystem[nameof(HealthState)];
        ushort CommunicationStatus => (ushort)MsvmComputerSystem[nameof(CommunicationStatus)];
        ushort DetailedStatus => (ushort)MsvmComputerSystem[nameof(DetailedStatus)];
        ushort OperatingStatus => (ushort)MsvmComputerSystem[nameof(OperatingStatus)];
        ushort PrimaryStatus => (ushort)MsvmComputerSystem[nameof(PrimaryStatus)];
        public EnabledStateVM EnabledState => (EnabledStateVM)(ushort)MsvmComputerSystem[nameof(EnabledState)];
        string OtherEnabledState => MsvmComputerSystem[nameof(OtherEnabledState)].ToString();
        ushort RequestedState => (ushort)MsvmComputerSystem[nameof(RequestedState)];
        EnabledDefaultVM EnabledDefault => (EnabledDefaultVM)MsvmComputerSystem[nameof(EnabledDefault)];
        DateTime TimeOfLastStateChange => ManagementDateTimeConverter.ToDateTime(MsvmComputerSystem[nameof(TimeOfLastStateChange)].ToString());
        VirtualSystemManagement.RequestedStateVSM[] AvailableRequestedStates => (VirtualSystemManagement.RequestedStateVSM[])MsvmComputerSystem[nameof(AvailableRequestedStates)];
        ushort TransitioningToState => (ushort)MsvmComputerSystem[nameof(TransitioningToState)];
        string CreationClassName => MsvmComputerSystem[nameof(CreationClassName)].ToString();
        string Name => Msvm_ComputerSystem?[nameof(Name)].ToString();
        string PrimaryOwnerName => MsvmComputerSystem[nameof(PrimaryOwnerName)].ToString();
        string PrimaryOwnerContact => MsvmComputerSystem[nameof(PrimaryOwnerContact)].ToString();
        string[] Roles => (string[])MsvmComputerSystem[nameof(Roles)];
        string NameFormat => MsvmComputerSystem[nameof(NameFormat)].ToString();
        string[] OtherIdentifyingInfo => (string[])MsvmComputerSystem[nameof(OtherIdentifyingInfo)];
        string[] IdentifyingDescriptions => (string[])MsvmComputerSystem[nameof(IdentifyingDescriptions)];
        ushort[] Dedicated => (ushort[])MsvmComputerSystem[nameof(Dedicated)];
        string[] OtherDedicatedDescriptions => (string[])MsvmComputerSystem[nameof(OtherDedicatedDescriptions)];
        ushort ResetCapability => (ushort)MsvmComputerSystem[nameof(ResetCapability)];
        ushort[] PowerManagementCapabilities => (ushort[])MsvmComputerSystem[nameof(PowerManagementCapabilities)];
        ulong OnTimeInMilliseconds => (ulong)MsvmComputerSystem[nameof(OnTimeInMilliseconds)];
        uint ProcessID => (uint)MsvmComputerSystem[nameof(ProcessID)];
        DateTime TimeOfLastConfigurationChange => ManagementDateTimeConverter.ToDateTime(MsvmComputerSystem[nameof(TimeOfLastConfigurationChange)].ToString());
        ushort NumberOfNumaNodes => (ushort)MsvmComputerSystem[nameof(NumberOfNumaNodes)];
        ReplicationStateVM ReplicationState => (ReplicationStateVM)MsvmComputerSystem[nameof(ReplicationState)];
        ReplicationHealthVM ReplicationHealth => (ReplicationHealthVM)MsvmComputerSystem[nameof(ReplicationHealth)];
        ReplicationModeVM ReplicationMode => (ReplicationModeVM)MsvmComputerSystem[nameof(ReplicationMode)];
        FailedOverReplicationTypeVM FailedOverReplicationType => (FailedOverReplicationTypeVM)MsvmComputerSystem[nameof(FailedOverReplicationType)];
        LastReplicationTypeVM LastReplicationType => (LastReplicationTypeVM)MsvmComputerSystem[nameof(LastReplicationType)];
        DateTime LastApplicationConsistentReplicationTime => ManagementDateTimeConverter.ToDateTime(MsvmComputerSystem[nameof(LastApplicationConsistentReplicationTime)].ToString());
        DateTime LastReplicationTime => ManagementDateTimeConverter.ToDateTime(MsvmComputerSystem[nameof(LastReplicationTime)].ToString());
        DateTime LastSuccessfulBackupTime => ManagementDateTimeConverter.ToDateTime(MsvmComputerSystem[nameof(LastSuccessfulBackupTime)].ToString());
        EnhancedSessionModeStateVM EnhancedSessionModeState => (EnhancedSessionModeStateVM)MsvmComputerSystem[nameof(EnhancedSessionModeState)];

        #endregion

        #region MsvmMethods

        public void InjectNonMaskableInterrupt()
        {
            using (var ip = MsvmComputerSystem.GetMethodParameters(nameof(InjectNonMaskableInterrupt)))
            {
                using (var op = MsvmComputerSystem.InvokeMethod(nameof(InjectNonMaskableInterrupt), ip, null))
                    Validator.ValidateOutput(op, Scope);
            }
        }
        public void RequestReplicationStateChange(ReplicationStateVM RequestedState, ulong TimeoutPeriod = 0)
        {
            using (var ip = MsvmComputerSystem.GetMethodParameters(nameof(RequestReplicationStateChange)))
            {
                ip[nameof(RequestedState)] = (ushort)RequestedState;
                ip[nameof(TimeoutPeriod)] = null; // CIM_DateTime

                using (var op = MsvmComputerSystem.InvokeMethod(nameof(RequestReplicationStateChange), ip, null))
                    Validator.ValidateOutput(op, Scope);
            }
        }
        public void RequestReplicationStateChangeEx(string ReplicationRelationship, ReplicationStateVM RequestedState, ulong TimeoutPeriod = 0)
        {
            using (var ip = MsvmComputerSystem.GetMethodParameters(nameof(RequestReplicationStateChangeEx)))
            {
                ip[nameof(ReplicationRelationship)] = ReplicationRelationship ?? throw new ViridianException($"{nameof(ReplicationRelationship)} is null!"); ;
                ip[nameof(RequestedState)] = (ushort)RequestedState;
                ip[nameof(TimeoutPeriod)] = null; // CIM_DateTime

                using (var op = MsvmComputerSystem.InvokeMethod(nameof(RequestReplicationStateChangeEx), ip, null))
                    Validator.ValidateOutput(op, Scope);
            }
        }
        public void RequestStateChange(VirtualSystemManagement.RequestedStateVSM RequestedState, ulong TimeoutPeriod = 0)
        {
            using (var ip = MsvmComputerSystem.GetMethodParameters(nameof(RequestStateChange)))
            {
                ip[nameof(RequestedState)] = (ushort)RequestedState;
                ip[nameof(TimeoutPeriod)] = null; // CIM_DateTime

                using (var op = MsvmComputerSystem.InvokeMethod(nameof(RequestStateChange), ip, null))
                    Validator.ValidateOutput(op, Scope);
            }
        }

        #endregion

        #region Creation

        public void DestroySystem()
        {
            VirtualSystemManagement.Instance.DestroySystem(MsvmComputerSystem);
            Msvm_ComputerSystem.Dispose();
            Msvm_ComputerSystem = null;
        }

        #endregion

        #region Backup

        public void SetIncrementalBackup(bool incrementalBackupEnabled)
        {
            using (var vm = GetVirtualMachineSettings(VmName))
            {
                if ((bool)vm["IncrementalBackupEnabled"] != incrementalBackupEnabled)
                {
                    vm["IncrementalBackupEnabled"] = incrementalBackupEnabled;

                    VirtualSystemManagement.Instance.ModifySystemSettings(vm.GetText(TextFormat.CimDtd20));
                }
            }
        }

        public bool GetIncrementalBackup()
        {
            using (var vms = GetVirtualMachineSettings(VmName))
                return (bool)vms["IncrementalBackupEnabled"];
        }

        #endregion

        #region Snapshots

        public void CreateSnapshot(SnapshotType snapshotType, bool saveMachineState)
        {
            if (snapshotType == SnapshotType.Recovery && saveMachineState)
                throw new ViridianException("You cannot create a recovery snapshot while the machine is in saved state!");

            if (snapshotType == SnapshotType.Recovery)
                SetIncrementalBackup(true);

            if (saveMachineState)
                RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Saved);

            string snapshotSettings = "";

            // Set the SnapshotSettings property. Backup/Recovery snapshots require special settings.
            if (snapshotType == SnapshotType.Recovery)
            {
                using (var vsssd = Utils.GetServiceObject(Scope, "Msvm_VirtualSystemSnapshotSettingData"))
                    snapshotSettings = vsssd.GetText(TextFormat.CimDtd20);

                // Make sure you activate Volume Shadow Copy service on Guest and install KB3063109.
                // https://support.microsoft.com/en-us/help/3063109/hyper-v-integration-components-update-for-windows-virtual-machines
                // https://thewincentral.com/how-to-install-cab-files-on-windows-10-for-cumulative-updates

                // Time Synchronization The protocol version of the component installed in the virtual machine does not match the version expected by the hosting system.
                // https://support.microsoft.com/en-us/help/4014894/vm-integration-services-status-reports-protocol-version-mismatch-on-pr

                // You cannot save actual ram state of the machine with backup/recovery checkpoints; State Saved doesn't make sense then.
            }

            VirtualSystemSnapshot.Instance.CreateSnapshot(MsvmComputerSystem.Path.Path, snapshotSettings, (ushort)snapshotType);

            if (saveMachineState)
                RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Running);
        }

        public List<ManagementObject> GetSnapshotList(string VirtualSystemType)
        {
                return
                    MsvmComputerSystem.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SnapshotOfVirtualSystem", null, null, null, null, false, null)
                        .Cast<ManagementObject>()
                        .Where((c) => (c[nameof(VirtualSystemType)]?.ToString() == VirtualSystemType))
                        .ToList();
        }

        public void ApplySnapshot(string ElementName)
        {
            // In order to apply a snapshot, the virtual machine must first be saved/off
            if (EnabledState != EnabledStateVM.Disabled)
                RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Off);

            VirtualSystemSnapshot.Instance.ApplySnapshot(
                MsvmComputerSystem.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SnapshotOfVirtualSystem", null, null, null, null, false, null)
                    .Cast<ManagementObject>()
                    .Where((c) => (c[nameof(ElementName)]?.ToString() == ElementName))
                    .First());
        }

        public ManagementObject GetLastAppliedSnapshot()
        {
            return MsvmComputerSystem.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_LastAppliedSnapshot", null, null, null, null, false, null).Cast<ManagementObject>().First();
        }

        public ManagementObject GetLastCreatedSnapshot()
        {
            return
                MsvmComputerSystem.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SnapshotOfVirtualSystem", null, null, null, null, false, null)
                    .Cast<ManagementObject>()
                    .OrderByDescending(x => (ManagementDateTimeConverter.ToDateTime(x["CreationTime"].ToString())))
                    .First();
        }

        #endregion
        
        #region Boot

        public string[] GetBootSourceOrderedList()
        {
            using (var vms = GetVirtualMachineSettings(VmName))
                return (string[])vms["BootSourceOrder"];
        }

        public void SetBootOrderFromDevicePath(string devicePath)
        {
            using (var vms = GetVirtualMachineSettings(VmName))
            {
                if (vms["BootSourceOrder"] is string[] prevBootOrder)
                {
                    var bso = new string[prevBootOrder.Length];

                    var index = 1;
                    foreach (var bs in prevBootOrder)
                        using (var entry = new ManagementObject(new ManagementPath(bs)))
                        {
                            var fdp = entry["FirmwareDevicePath"].ToString();

                            if (string.Equals(devicePath, fdp, StringComparison.OrdinalIgnoreCase))
                                bso[0] = bs;
                            else
                                bso[index++] = bs;
                        }

                    vms["BootSourceOrder"] = bso;
                }

                VirtualSystemManagement.Instance.ModifySystemSettings(vms.GetText(TextFormat.WmiDtd20));
            }
        }

        public void SetBootOrderByIndex(uint[] bootSourceOrder)
        {
            if (bootSourceOrder == null)
                throw new ViridianException("Boot Sources Array is null!");

            using (var vms = GetVirtualMachineSettings(VmName))
            {
                var previousBso = vms["BootSourceOrder"] as string[];

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

                    vms["BootSourceOrder"] = newBso;
                }

                VirtualSystemManagement.Instance.ModifySystemSettings(vms.GetText(TextFormat.WmiDtd20));
            }
        }

        public NetworkBootPreferredProtocol GetNetworkBootPreferredProtocol()
        {
            using (var vms = GetVirtualMachineSettings(VmName))
                return (NetworkBootPreferredProtocol)Enum.ToObject(typeof(NetworkBootPreferredProtocol), vms["NetworkBootPreferredProtocol"]);
        }

        public void SetNetworkBootPreferredProtocol(NetworkBootPreferredProtocol networkBootPreferredProtocol)
        {
            using (var vms = GetVirtualMachineSettings(VmName))
            {
                vms["NetworkBootPreferredProtocol"] = networkBootPreferredProtocol;

                VirtualSystemManagement.Instance.ModifySystemSettings(vms.GetText(TextFormat.WmiDtd20));
            }
        }

        public bool GetPauseAfterBootFailure()
        {
            using (var vms = GetVirtualMachineSettings(VmName))
                return (bool)vms["PauseAfterBootFailure"];
        }

        public void SetPauseAfterBootFailure(bool pauseAfterBootFailure)
        {
            using (var vms = GetVirtualMachineSettings(VmName))
            {
                vms["PauseAfterBootFailure"] = pauseAfterBootFailure;

                VirtualSystemManagement.Instance.ModifySystemSettings(vms.GetText(TextFormat.WmiDtd20));
            }
        }

        public bool GetSecureBoot()
        {
            using (var vms = GetVirtualMachineSettings(VmName))
                return (bool)vms["SecureBootEnabled"];
        }

        public void SetSecureBoot(bool secureBootEnabled)
        {
            using (var vms = GetVirtualMachineSettings(VmName))
            {
                vms["SecureBootEnabled"] = secureBootEnabled;

                VirtualSystemManagement.Instance.ModifySystemSettings(vms.GetText(TextFormat.WmiDtd20));
            }
        }

        #endregion

        #region Controllers

        public ManagementObject GetScsiController(int index)
        {
            return
                GetResourceAllocationSettingData(ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceType, ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceSubType)
                    .Skip(index)
                    .First();
        }

        #endregion

        #region Stats

        public ManagementObject GetMemorySettingData()
        {
            using (var vssd = MsvmComputerSystem.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SettingsDefineState", null, null, "SettingData", "ManagedElement", false, null).Cast<ManagementObject>().First())
                return vssd.GetRelated("Msvm_MemorySettingData").Cast<ManagementObject>().First();
        }

        public ManagementBaseObject[] GetSummaryInformation()
        {
            using (var vms = GetVirtualMachineSettings(VmName))
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

                return VirtualSystemManagement.Instance.GetSummaryInformation(new[] { vms }, requestedInformation);
            }

        }

        #endregion

        #region Network

        public void ConnectVmToSwitch(string switchName, string adapterName)
        {
            using (var ves = NetSwitch.FindVirtualEthernetSwitch(Scope, switchName))
            using (var vms = GetVirtualMachineSettings(VmName))
            using (var syntheticAdapter = SyntheticEthernetAdapter.AddSyntheticAdapter(this, adapterName))
            using (var epasd = NetSwitch.GetDefaultEthernetPortAllocationSettingData())
            {
                epasd["Parent"] = syntheticAdapter.Path.Path;
                epasd["HostResource"] = new string[] { ves.Path.Path };

                VirtualSystemManagement.Instance.AddResourceSettings(vms, new string[] { epasd.GetText(TextFormat.WmiDtd20) });
            }
        }

        public void DisconnectVmFromSwitch(string switchName)
        {
            using (var ves = NetSwitch.FindVirtualEthernetSwitch(Scope, switchName))
                foreach (var connection in NetSwitch.FindConnectionsToSwitch(MsvmComputerSystem, ves))
                {
                    connection["EnabledState"] = 3;

                    VirtualSystemManagement.Instance.ModifyResourceSettings(new string[] { connection.GetText(TextFormat.WmiDtd20) });
                }
        }

        public void ModifyConnection(string currentSwitchName, string newSwitchName)
        {
            using (var ves = NetSwitch.FindVirtualEthernetSwitch(Scope, currentSwitchName))
            using (var newVes = NetSwitch.FindVirtualEthernetSwitch(Scope, newSwitchName))
                foreach (var connection in NetSwitch.FindConnectionsToSwitch(MsvmComputerSystem, ves))
                {
                    connection["HostResource"] = new string[] { newVes.Path.Path };

                    VirtualSystemManagement.Instance.ModifyResourceSettings(new string[] { connection.GetText(TextFormat.WmiDtd20) });
                }
        }

        public List<ManagementObject> GetSyntheticAdapterCollection()
        {
            return MsvmComputerSystem.GetRelated("Msvm_SyntheticEthernetPort").Cast<ManagementObject>().ToList();
        }

        public List<ManagementObject> GetEthernetSwitchPortAclSettingDatas()
        {
            var aclSettingDataList = new List<ManagementObject>();

            using (var vms = GetVirtualMachineSettings(MsvmComputerSystem))
                vms.GetRelated("Msvm_SyntheticEthernetPortSettingData")
                    .Cast<ManagementObject>()
                    .ToList()
                    .ForEach((sepsd) =>
                        aclSettingDataList.AddRange(
                            SyntheticEthernetAdapter.GetEthernetSwitchPortAclSettingData(
                                SyntheticEthernetAdapter.GetEthernetPortAllocationSettingData(sepsd, Scope))
                                    .Cast<ManagementObject>()
                                    .ToList()));

            return aclSettingDataList;
        }

        #endregion

        #region Metrics

        public void SetAggregationMetricsForDrives(Metric.MetricCollectionEnabled operation)
        {
            GetResourceAllocationSettingData(ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceType, ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceSubType)
                .Cast<ManagementObject>()
                .ToList()
                .ForEach(
                    (controller) =>
                        controller.GetRelated("Msvm_ResourceAllocationSettingData", null, null, null, "Dependent", "Antecedent", false, null)
                            .Cast<ManagementObject>()
                            .ToList()
                            .ForEach((setting) => Metric.Instance.SetAllMetrics(setting, operation)));
        }

        public void SetBaseMetricsForEthernetSwitchPortAclSettingData(Metric.MetricCollectionEnabled operation)
        {
            using (var vms = GetVirtualMachineSettings(MsvmComputerSystem))
                vms.GetRelated("Msvm_SyntheticEthernetPortSettingData")
                    .Cast<ManagementObject>()
                    .ToList()
                    .ForEach(
                        (sepsd) =>
                            SyntheticEthernetAdapter.GetEthernetSwitchPortAclSettingData(
                                SyntheticEthernetAdapter.GetEthernetPortAllocationSettingData(sepsd, Scope))
                                    .ForEach(
                                        (espasd) =>
                                            Metric.GetAllBaseMetricDefinitions(espasd)
                                            .ForEach(
                                                (baseMetricDef) =>
                                                    Metric.Instance.SetBaseMetric(espasd, baseMetricDef, operation))));
        }

        public void SetAggregationMetricsForEthernetSwitchPortAclSettingData(Metric.MetricCollectionEnabled operation)
        {
            using (var vms = GetVirtualMachineSettings(MsvmComputerSystem))
                vms.GetRelated("Msvm_SyntheticEthernetPortSettingData")
                    .Cast<ManagementObject>()
                    .ToList()
                    .ForEach(
                        (sepsd) =>
                            SyntheticEthernetAdapter.GetEthernetSwitchPortAclSettingData(
                                SyntheticEthernetAdapter.GetEthernetPortAllocationSettingData(sepsd, Scope))
                                    .ForEach(
                                        (espasd) =>
                                            Metric.Instance.SetAllMetrics(espasd, operation)));
        }

        public List<ManagementObject> GetResourceAllocationSettingData(string ResourceType, string ResourceSubType)
        {
            return
                MsvmComputerSystem.GetRelated("Msvm_VirtualSystemSettingData").Cast<ManagementObject>().First()
                    .GetRelated("Msvm_ResourceAllocationSettingData")
                        .Cast<ManagementObject>()
                        .Where((c) =>
                            c[nameof(ResourceType)]?.ToString() == ResourceType &&
                            c[nameof(ResourceSubType)]?.ToString() == ResourceSubType)
                        .ToList();
        }

        public static ManagementObject GetVirtualMachineSettings(string vmName)
        {
            return GetVirtualMachineSettings(new VM(vmName).MsvmComputerSystem);
        }

        public static ManagementObject GetVirtualMachineSettings(ManagementObject virtualMachine)
        {
            return virtualMachine?.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null).Cast<ManagementObject>().First();
        }

        #endregion

        #region Utils

        public static ManagementObject GetVirtualMachine(ManagementScope scope)
        {
            using (var mos = new ManagementObjectSearcher(scope, new ObjectQuery("SELECT * FROM Msvm_ComputerSystem")))
                return mos.Get().Cast<ManagementObject>().Where((c) => c["Name"]?.ToString() == Environment.MachineName).FirstOrDefault();
        }

        #endregion

        ~VM()
        {
            if (Msvm_ComputerSystem != null)
                Msvm_ComputerSystem.Dispose();
        }
    }
}
