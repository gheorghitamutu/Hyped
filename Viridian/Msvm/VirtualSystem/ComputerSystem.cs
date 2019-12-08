using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Viridian.Job;
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
                    return QueryMsvmComputerSystem(nameof(ElementName), ElementName);

                return QueryMsvmComputerSystem(nameof(Name), Name);
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
                Msvm_ComputerSystem = QueryMsvmComputerSystem(nameof(ElementName), ElementName);

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

        public string InstanceID => MsvmComputerSystem[nameof(InstanceID)].ToString();
        public string Caption => MsvmComputerSystem[nameof(Caption)].ToString();
        public string Description => MsvmComputerSystem[nameof(Description)].ToString();
        public string ElementName 
        {
            get { return Msvm_ComputerSystem != null ? MsvmComputerSystem[nameof(ElementName)].ToString() : elementName; }
            private set { elementName = value; }
        }
        public DateTime InstallDate => ManagementDateTimeConverter.ToDateTime(MsvmComputerSystem[nameof(InstallDate)].ToString());
        public OperationalStatusVM[] OperationalStatus => (OperationalStatusVM[])MsvmComputerSystem[nameof(OperationalStatus)];
        public string[] StatusDescriptions => (string[])MsvmComputerSystem[nameof(StatusDescriptions)];
        public string Status => MsvmComputerSystem[nameof(Status)].ToString();
        public HealthStateVM HealthState => (HealthStateVM)MsvmComputerSystem[nameof(HealthState)];
        public ushort CommunicationStatus => (ushort)MsvmComputerSystem[nameof(CommunicationStatus)];
        public ushort DetailedStatus => (ushort)MsvmComputerSystem[nameof(DetailedStatus)];
        public ushort OperatingStatus => (ushort)MsvmComputerSystem[nameof(OperatingStatus)];
        public ushort PrimaryStatus => (ushort)MsvmComputerSystem[nameof(PrimaryStatus)];
        public EnabledStateVM EnabledState => (EnabledStateVM)(ushort)MsvmComputerSystem[nameof(EnabledState)];
        public string OtherEnabledState => MsvmComputerSystem[nameof(OtherEnabledState)].ToString();
        public VirtualSystemManagementService.RequestedStateVSM RequestedState => (VirtualSystemManagementService.RequestedStateVSM)(ushort)MsvmComputerSystem[nameof(RequestedState)];
        public EnabledDefaultVM EnabledDefault => (EnabledDefaultVM)MsvmComputerSystem[nameof(EnabledDefault)];
        public DateTime TimeOfLastStateChange => ManagementDateTimeConverter.ToDateTime(MsvmComputerSystem[nameof(TimeOfLastStateChange)].ToString());
        public VirtualSystemManagementService.RequestedStateVSM[] AvailableRequestedStates => (VirtualSystemManagementService.RequestedStateVSM[])MsvmComputerSystem[nameof(AvailableRequestedStates)];
        public ushort TransitioningToState => (ushort)MsvmComputerSystem[nameof(TransitioningToState)];
        public string CreationClassName => MsvmComputerSystem[nameof(CreationClassName)].ToString();
        public string Name => Msvm_ComputerSystem?[nameof(Name)].ToString();
        public string PrimaryOwnerName => MsvmComputerSystem[nameof(PrimaryOwnerName)].ToString();
        public string PrimaryOwnerContact => MsvmComputerSystem[nameof(PrimaryOwnerContact)].ToString();
        public string[] Roles => (string[])MsvmComputerSystem[nameof(Roles)];
        public string NameFormat => MsvmComputerSystem[nameof(NameFormat)].ToString();
        public string[] OtherIdentifyingInfo => (string[])MsvmComputerSystem[nameof(OtherIdentifyingInfo)];
        public string[] IdentifyingDescriptions => (string[])MsvmComputerSystem[nameof(IdentifyingDescriptions)];
        public ushort[] Dedicated => (ushort[])MsvmComputerSystem[nameof(Dedicated)];
        public string[] OtherDedicatedDescriptions => (string[])MsvmComputerSystem[nameof(OtherDedicatedDescriptions)];
        public ushort ResetCapability => (ushort)MsvmComputerSystem[nameof(ResetCapability)];
        public ushort[] PowerManagementCapabilities => (ushort[])MsvmComputerSystem[nameof(PowerManagementCapabilities)];
        public ulong OnTimeInMilliseconds => (ulong)MsvmComputerSystem[nameof(OnTimeInMilliseconds)];
        public uint ProcessID => (uint)MsvmComputerSystem[nameof(ProcessID)];
        public DateTime TimeOfLastConfigurationChange => ManagementDateTimeConverter.ToDateTime(MsvmComputerSystem[nameof(TimeOfLastConfigurationChange)].ToString());
        public ushort NumberOfNumaNodes => (ushort)MsvmComputerSystem[nameof(NumberOfNumaNodes)];
        public ReplicationStateVM ReplicationState => (ReplicationStateVM)MsvmComputerSystem[nameof(ReplicationState)];
        public ReplicationHealthVM ReplicationHealth => (ReplicationHealthVM)MsvmComputerSystem[nameof(ReplicationHealth)];
        public ReplicationModeVM ReplicationMode => (ReplicationModeVM)MsvmComputerSystem[nameof(ReplicationMode)];
        public FailedOverReplicationTypeVM FailedOverReplicationType => (FailedOverReplicationTypeVM)MsvmComputerSystem[nameof(FailedOverReplicationType)];
        public LastReplicationTypeVM LastReplicationType => (LastReplicationTypeVM)MsvmComputerSystem[nameof(LastReplicationType)];
        public DateTime LastApplicationConsistentReplicationTime => ManagementDateTimeConverter.ToDateTime(MsvmComputerSystem[nameof(LastApplicationConsistentReplicationTime)].ToString());
        public DateTime LastReplicationTime => ManagementDateTimeConverter.ToDateTime(MsvmComputerSystem[nameof(LastReplicationTime)].ToString());
        public DateTime LastSuccessfulBackupTime => ManagementDateTimeConverter.ToDateTime(MsvmComputerSystem[nameof(LastSuccessfulBackupTime)].ToString());
        public EnhancedSessionModeStateVM EnhancedSessionModeState => (EnhancedSessionModeStateVM)MsvmComputerSystem[nameof(EnhancedSessionModeState)];

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
                ip[nameof(ReplicationRelationship)] = ReplicationRelationship ?? throw new ArgumentNullException(nameof(ReplicationRelationship));
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

        public void DestroySystem()
        {
            VirtualSystemManagementService.Instance.DestroySystem(MsvmComputerSystem);
            Msvm_ComputerSystem.Dispose();
            Msvm_ComputerSystem = null;
        }

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

        public static ManagementObject QueryMsvmComputerSystem(string name, string value)
        {
            using (var mos = new ManagementObjectSearcher(Scope.Virtualization.SpecificScope, new ObjectQuery($"SELECT * FROM {nameof(Msvm_ComputerSystem)}")))
                return mos.Get().Cast<ManagementObject>().Where((c) => c[name]?.ToString() == value).FirstOrDefault();
        }
        private static ManagementObject GetMsvmObject(string serviceName)
        {
            using (var serviceClass = new ManagementClass(Scope.Virtualization.SpecificScope, new ManagementPath(serviceName), null))
                return serviceClass.GetInstances().Cast<ManagementObject>().First();
        }

        ~ComputerSystem()
        {
            if (Msvm_ComputerSystem != null)
                Msvm_ComputerSystem.Dispose();
        }
    }
}
