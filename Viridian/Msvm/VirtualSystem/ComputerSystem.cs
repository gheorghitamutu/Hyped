using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Msvm.Metrics;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.VirtualSystemManagement;
using Viridian.Resources.Network;
using Viridian.Scopes;

namespace Viridian.Msvm.VirtualSystem
{
    public class ComputerSystem
    {
        private ManagementObject Msvm_ComputerSystem = null; // don't directly use it unless explicitly required (Name property)!
        public VirtualSystemSettingData VirtualSystemSettingData { get; set; }

        private string elementName = null;

        public ComputerSystem(string ElementName)
        {
            this.ElementName = ElementName;

            Define();

            VirtualSystemSettingData = new VirtualSystemSettingData(this, Properties.VirtualSystemSettingData.Default.Msvm_SettingsDefineState);
        }

        public ManagementObject MsvmComputerSystem
        {
            get
            {
                if (Name == null && Msvm_ComputerSystem == null)
                    return null;

                if (Name == null)
                    return QueryMsvm_ComputerSystem(nameof(ElementName), ElementName);

                return QueryMsvm_ComputerSystem(nameof(Name), Name);
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
                Msvm_ComputerSystem = QueryMsvm_ComputerSystem(nameof(ElementName), ElementName);

                if (MsvmComputerSystem == null)
                {
                    using (var vssd = GetMsvmObject("Msvm_VirtualSystemSettingData"))
                    {
                        vssd[nameof(ElementName)] = ElementName;
                        vssd[nameof(Properties.Environment.Default.ConfigurationDataRoot)] = Properties.Environment.Default.ConfigurationDataRoot;
                        vssd[nameof(Properties.Environment.Default.VirtualSystemSubtype)] = Properties.Environment.Default.VirtualSystemSubtype;

                        MsvmComputerSystem = VirtualSystemManagementService.Instance.DefineSystem(vssd.GetText(TextFormat.WmiDtd20), null, null);
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
        public string ElementName 
        {
            get { return Msvm_ComputerSystem != null ? MsvmComputerSystem[nameof(ElementName)].ToString() : elementName; }
            private set { elementName = value; }
        }
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
        VirtualSystemManagementService.RequestedStateVSM[] AvailableRequestedStates => (VirtualSystemManagementService.RequestedStateVSM[])MsvmComputerSystem[nameof(AvailableRequestedStates)];
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
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }
        public void RequestReplicationStateChange(ReplicationStateVM RequestedState, ulong TimeoutPeriod = 0)
        {
            using (var ip = MsvmComputerSystem.GetMethodParameters(nameof(RequestReplicationStateChange)))
            {
                ip[nameof(RequestedState)] = (ushort)RequestedState;
                ip[nameof(TimeoutPeriod)] = null; // CIM_DateTime

                using (var op = MsvmComputerSystem.InvokeMethod(nameof(RequestReplicationStateChange), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
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
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }
        public void RequestStateChange(VirtualSystemManagementService.RequestedStateVSM RequestedState, ulong TimeoutPeriod = 0)
        {
            using (var ip = MsvmComputerSystem.GetMethodParameters(nameof(RequestStateChange)))
            {
                ip[nameof(RequestedState)] = (ushort)RequestedState;
                ip[nameof(TimeoutPeriod)] = null; // CIM_DateTime

                using (var op = MsvmComputerSystem.InvokeMethod(nameof(RequestStateChange), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        #endregion

        #region Creation

        public void DestroySystem()
        {
            VirtualSystemManagementService.Instance.DestroySystem(MsvmComputerSystem);
            Msvm_ComputerSystem.Dispose();
            Msvm_ComputerSystem = null;
        }

        #endregion

        #region Snapshots

        public void CreateSnapshot(SnapshotType snapshotType, bool saveMachineState)
        {
            if (snapshotType == SnapshotType.Recovery && saveMachineState)
                throw new ViridianException("You cannot create a recovery snapshot while the machine is in saved state!");

            if (snapshotType == SnapshotType.Recovery && VirtualSystemSettingData.IncrementalBackupEnabled == false)
                VirtualSystemSettingData.ModifySystemSettings(new Dictionary<string, object>() { { nameof(VirtualSystemSettingData.IncrementalBackupEnabled), true } });

            if (saveMachineState)
                RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Saved);

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

            VirtualSystemSnapshotService.Instance.CreateSnapshot(MsvmComputerSystem.Path.Path, snapshotSettings, (ushort)snapshotType);

            if (saveMachineState)
                RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Running);
        }

        public void ApplySnapshot(string ElementName)
        {
            // In order to apply a snapshot, the virtual machine must first be saved/off
            if (EnabledState != EnabledStateVM.Disabled)
                RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Off);

            VirtualSystemSnapshotService.Instance.ApplySnapshot(VirtualSystemSettingData.GetSnapshot(ElementName).MsvmVirtualSystemSettingData);
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

        #region Network

        public void DisconnectVmFromSwitch(string switchName)
        {
            using (var ves = NetSwitch.FindVirtualEthernetSwitch(Scope.Virtualization.SpecificScope, switchName))
                foreach (var connection in NetSwitch.FindConnectionsToSwitch(this, ves))
                {
                    connection["EnabledState"] = 3;

                    VirtualSystemManagementService.Instance.ModifyResourceSettings(new string[] { connection.GetText(TextFormat.WmiDtd20) });
                }
        }

        public void ModifyConnection(string currentSwitchName, string newSwitchName)
        {
            using (var ves = NetSwitch.FindVirtualEthernetSwitch(Scope.Virtualization.SpecificScope, currentSwitchName))
            using (var newVes = NetSwitch.FindVirtualEthernetSwitch(Scope.Virtualization.SpecificScope, newSwitchName))
                foreach (var connection in NetSwitch.FindConnectionsToSwitch(this, ves))
                {
                    connection["HostResource"] = new string[] { newVes.Path.Path };

                    VirtualSystemManagementService.Instance.ModifyResourceSettings(new string[] { connection.GetText(TextFormat.WmiDtd20) });
                }
        }

        public List<ManagementObject> GetSyntheticAdapterCollection()
        {
            return MsvmComputerSystem.GetRelated("Msvm_SyntheticEthernetPort").Cast<ManagementObject>().ToList();
        }

        #endregion

        #region Metrics

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

        public void SetBaseMetricsForEthernetSwitchPortAclSettingData(MetricService.MetricCollectionEnabled operation)
        {
            VirtualSystemSettingData.MsvmVirtualSystemSettingData.GetRelated("Msvm_SyntheticEthernetPortSettingData")
                    .Cast<ManagementObject>()
                    .ToList()
                    .ForEach(
                        (sepsd) =>
                            SyntheticEthernetAdapter.GetEthernetSwitchPortAclSettingData(
                                SyntheticEthernetAdapter.GetEthernetPortAllocationSettingData(sepsd, Scope.Virtualization.SpecificScope))
                                    .ForEach(
                                        (espasd) =>
                                            MetricService.GetAllBaseMetricDefinitions(espasd)
                                            .ForEach(
                                                (baseMetricDef) =>
                                                    MetricService.Instance.SetBaseMetric(espasd, baseMetricDef, operation))));
        }

        public void SetAggregationMetricsForEthernetSwitchPortAclSettingData(MetricService.MetricCollectionEnabled operation)
        {
                VirtualSystemSettingData.MsvmVirtualSystemSettingData.GetRelated("Msvm_SyntheticEthernetPortSettingData")
                    .Cast<ManagementObject>()
                    .ToList()
                    .ForEach(
                        (sepsd) =>
                            SyntheticEthernetAdapter.GetEthernetSwitchPortAclSettingData(
                                SyntheticEthernetAdapter.GetEthernetPortAllocationSettingData(sepsd, Scope.Virtualization.SpecificScope))
                                    .ForEach(
                                        (espasd) =>
                                            MetricService.Instance.SetAllMetrics(espasd, operation)));
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

        #endregion

        #region Utils

        public static ManagementObject QueryMsvm_ComputerSystem(string name, string value)
        {
            using (var mos = new ManagementObjectSearcher(Scope.Virtualization.SpecificScope, new ObjectQuery("SELECT * FROM Msvm_ComputerSystem")))
                return mos.Get().Cast<ManagementObject>().Where((c) => c[name]?.ToString() == value).FirstOrDefault();
        }

        private static ManagementObject GetMsvmObject(string serviceName)
        {
            using (var serviceClass = new ManagementClass(Scope.Virtualization.SpecificScope, new ManagementPath(serviceName), null))
                return serviceClass.GetInstances().Cast<ManagementObject>().First();
        }

        #endregion

        ~ComputerSystem()
        {
            if (Msvm_ComputerSystem != null)
                Msvm_ComputerSystem.Dispose();
        }
    }
}
