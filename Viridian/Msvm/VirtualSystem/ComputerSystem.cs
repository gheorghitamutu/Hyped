using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;

namespace Viridian.Msvm.VirtualSystem
{
    public class ComputerSystem : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(ComputerSystem)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public ComputerSystem() : base(ClassName) { }

        public ComputerSystem(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ComputerSystem(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ComputerSystem(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public ComputerSystem(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public ComputerSystem(ManagementPath path) : base(path, ClassName) { }

        public ComputerSystem(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public ComputerSystem(ManagementObject theObject) : base(theObject, ClassName) { }

        public ComputerSystem(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public ushort[] AvailableRequestedStates => (ushort[])LateBoundObject[nameof(AvailableRequestedStates)];

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public ushort CommunicationStatus
        {
            get
            {
                if (LateBoundObject[nameof(CommunicationStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(CommunicationStatus)];
            }
        }

        public string CreationClassName => (string)LateBoundObject[nameof(CreationClassName)];

        public ushort[] Dedicated => (ushort[])LateBoundObject[nameof(Dedicated)];

        public string Description => (string)LateBoundObject[nameof(Description)];

        public ushort DetailedStatus
        {
            get
            {
                if (LateBoundObject[nameof(DetailedStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(DetailedStatus)];
            }
        }

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public ushort EnabledDefault
        {
            get
            {
                if (LateBoundObject[nameof(EnabledDefault)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(EnabledDefault)];
            }
        }

        public ushort EnabledState
        {
            get
            {
                if (LateBoundObject[nameof(EnabledState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(EnabledState)];
            }
        }

        /* 
         * Indicates whether or not enhanced mode connections are allowed by the host
         * and if allowed, whether or not they are available to the virtual machine.
         */
        public EnhancedSessionModeStateValues EnhancedSessionModeState
        {
            get
            {
                if (LateBoundObject[nameof(EnhancedSessionModeState)] == null)
                {
                    return (EnhancedSessionModeStateValues)Convert.ToInt32(0);
                }
                return (EnhancedSessionModeStateValues)Convert.ToInt32(LateBoundObject[nameof(EnhancedSessionModeState)]);
            }
        }

        /*
         * Type of failover that was performed for the virtual machine.
         */
        public FailedOverReplicationTypeValues FailedOverReplicationType
        {
            get
            {
                if (LateBoundObject[nameof(FailedOverReplicationType)] == null)
                {
                    return (FailedOverReplicationTypeValues)Convert.ToInt32(4);
                }
                return (FailedOverReplicationTypeValues)Convert.ToInt32(LateBoundObject[nameof(FailedOverReplicationType)]);
            }
        }

        public ushort HealthState
        {
            get
            {
                if (LateBoundObject[nameof(HealthState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(HealthState)];
            }
        }

        /*
         * Indicates the number of SMT threads per core reported to the guest.  This reporting
         * is independent of whether the hardware for SMT is present.
         */
        public uint HwThreadsPerCoreRealized
        {
            get
            {
                if (LateBoundObject[nameof(HwThreadsPerCoreRealized)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(HwThreadsPerCoreRealized)];
            }
        }

        public string[] IdentifyingDescriptions => (string[])LateBoundObject[nameof(IdentifyingDescriptions)];

        public DateTime InstallDate
        {
            get
            {
                if (LateBoundObject[nameof(InstallDate)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(InstallDate)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        /*
         * The time at which the last application consistent replication is received on recovery for the virtual machine.
         */
        public DateTime LastApplicationConsistentReplicationTime
        {
            get
            {
                if (LateBoundObject[nameof(LastApplicationConsistentReplicationTime)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(LastApplicationConsistentReplicationTime)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        /*
         * The time at which the last replication is received on recovery for the virtual machine.
         */
        public DateTime LastReplicationTime
        {
            get
            {
                if (LateBoundObject[nameof(LastReplicationTime)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(LastReplicationTime)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        /*
         * Type of the last replication that was received for the virtual machine.
         */
        public LastReplicationTypeValues LastReplicationType
        {
            get
            {
                if (LateBoundObject[nameof(LastReplicationType)] == null)
                {
                    return (LastReplicationTypeValues)Convert.ToInt32(4);
                }
                return (LastReplicationTypeValues)Convert.ToInt32(LateBoundObject[nameof(LastReplicationType)]);
            }
        }

        /*
         * The time at which the last successful backup has completed for the virtual machine.
         */
        public DateTime LastSuccessfulBackupTime
        {
            get
            {
                if (LateBoundObject[nameof(LastSuccessfulBackupTime)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(LastSuccessfulBackupTime)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public string Name => (string)LateBoundObject[nameof(Name)];

        public string NameFormat => (string)LateBoundObject[nameof(NameFormat)];

        /*
         * The number of non-uniform memory access (NUMA) nodes of the computer system. 
         * When Msvm_ComputerSystem represents the hosting computer system, this property contains the count of physical NUMA nodes.
         * When Msvm_ComputerSystem represents a virtual computer system, 
         * this property contains the number of virtual NUMA nodes that are presented to the guest OS through the ACPI System Resource Affinity Table (SRAT).
         */
        public ushort NumberOfNumaNodes
        {
            get
            {
                if (LateBoundObject[nameof(NumberOfNumaNodes)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(NumberOfNumaNodes)];
            }
        }

        /*
         * For the virtual system, this property describes the total up time, in milliseconds, since the machine was last turned on, reset, or restored.
         * This time excludes the time the virtual system was in the paused state. For the host system, this property is set to NULL.
         */
        public ulong OnTimeInMilliseconds
        {
            get
            {
                if (LateBoundObject[nameof(OnTimeInMilliseconds)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(OnTimeInMilliseconds)];
            }
        }

        public ushort OperatingStatus
        {
            get
            {
                if (LateBoundObject[nameof(OperatingStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(OperatingStatus)];
            }
        }

        public ushort[] OperationalStatus => (ushort[])LateBoundObject[nameof(OperationalStatus)];

        public string[] OtherDedicatedDescriptions => (string[])LateBoundObject[nameof(OtherDedicatedDescriptions)];

        public string OtherEnabledState => (string)LateBoundObject[nameof(OtherEnabledState)];

        public string[] OtherIdentifyingInfo => (string[])LateBoundObject[nameof(OtherIdentifyingInfo)];

        public ushort[] PowerManagementCapabilities => (ushort[])LateBoundObject[nameof(PowerManagementCapabilities)];

        public string PrimaryOwnerContact => (string)LateBoundObject[nameof(PrimaryOwnerContact)];

        public string PrimaryOwnerName => (string)LateBoundObject[nameof(PrimaryOwnerName)];

        public ushort PrimaryStatus
        {
            get
            {
                if (LateBoundObject[nameof(PrimaryStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(PrimaryStatus)];
            }
        }

        /*
         * The identifier of the process under which this virtual machine is running.
         * This value can be used to uniquely identify the instance of Vmwp.exe on the system that is running the virtual machine.
         */
        public uint ProcessID
        {
            get
            {
                if (LateBoundObject[nameof(ProcessID)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(ProcessID)];
            }
        }

        /*
         * Replication health for the virtual machine.
         */
        public ReplicationHealthValues ReplicationHealth
        {
            get
            {
                if (LateBoundObject[nameof(ReplicationHealth)] == null)
                {
                    return (ReplicationHealthValues)Convert.ToInt32(4);
                }
                return (ReplicationHealthValues)Convert.ToInt32(LateBoundObject[nameof(ReplicationHealth)]);
            }
        }

        /*
         * Identifies replication type for the virtual machine.
         */
        public ReplicationModeValues ReplicationMode
        {
            get
            {
                if (LateBoundObject[nameof(ReplicationMode)] == null)
                {
                    return (ReplicationModeValues)Convert.ToInt32(5);
                }
                return (ReplicationModeValues)Convert.ToInt32(LateBoundObject[nameof(ReplicationMode)]);
            }
        }

        /*
         * Replication state for the virtual machine.
         */
        public ReplicationStateValues ReplicationState
        {
            get
            {
                if (LateBoundObject[nameof(ReplicationState)] == null)
                {
                    return (ReplicationStateValues)Convert.ToInt32(15);
                }
                return (ReplicationStateValues)Convert.ToInt32(LateBoundObject[nameof(ReplicationState)]);
            }
        }

        public ushort RequestedState
        {
            get
            {
                if (LateBoundObject[nameof(RequestedState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(RequestedState)];
            }
        }

        public ushort ResetCapability
        {
            get
            {
                if (LateBoundObject[nameof(ResetCapability)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ResetCapability)];
            }
        }

        public string[] Roles => (string[])LateBoundObject[nameof(Roles)];

        public string Status => (string)LateBoundObject[nameof(Status)];

        public string[] StatusDescriptions => (string[])LateBoundObject[nameof(StatusDescriptions)];

        /*
         * The date and time when the virtual machine configuration file was last modified.
         * The configuration file is modified during certain virtual machine operations,
         * as well as when any of the virtual machine or device settings are added, modified, or removed.
         */
        public DateTime TimeOfLastConfigurationChange
        {
            get
            {
                if (LateBoundObject[nameof(TimeOfLastConfigurationChange)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(TimeOfLastConfigurationChange)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public DateTime TimeOfLastStateChange
        {
            get
            {
                if (LateBoundObject[nameof(TimeOfLastStateChange)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(TimeOfLastStateChange)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public ushort TransitioningToState
        {
            get
            {
                if (LateBoundObject[nameof(TransitioningToState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(TransitioningToState)];
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<ComputerSystem> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new ComputerSystem(mo)).ToList();

        public new static List<ComputerSystem> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ComputerSystem(mo)).ToList();

        public static List<ComputerSystem> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ComputerSystem(mo)).ToList();

        public static List<ComputerSystem> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ComputerSystem(mo)).ToList();

        public static List<ComputerSystem> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new ComputerSystem(mo)).ToList();

        public static List<ComputerSystem> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ComputerSystem(mo)).ToList();

        public static List<ComputerSystem> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ComputerSystem(mo)).ToList();

        public static List<ComputerSystem> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ComputerSystem(mo)).ToList();

        public static ComputerSystem CreateInstance() => new ComputerSystem(CreateInstance(ClassName));

        public uint InjectNonMaskableInterrupt(out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("InjectNonMaskableInterrupt", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RequestReplicationStateChange(ushort RequestedState, DateTime TimeoutPeriod, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RequestReplicationStateChange");
                inParams[nameof(RequestedState)] = RequestedState;
                inParams["TimeoutPeriod"] = ToDmtfDateTime(TimeoutPeriod);
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RequestReplicationStateChange", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RequestReplicationStateChangeEx(string ReplicationRelationship, ushort RequestedState, DateTime TimeoutPeriod, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RequestReplicationStateChangeEx");
                inParams["ReplicationRelationship"] = ReplicationRelationship;
                inParams[nameof(RequestedState)] = RequestedState;
                inParams["TimeoutPeriod"] = ToDmtfDateTime(TimeoutPeriod);
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RequestReplicationStateChangeEx", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RequestStateChange(ushort RequestedState, DateTime? TimeoutPeriod, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RequestStateChange");
                inParams[nameof(RequestedState)] = RequestedState;
                inParams["TimeoutPeriod"] = TimeoutPeriod != null ? ToDmtfDateTime(TimeoutPeriod.Value) : null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RequestStateChange", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint SetPowerState(uint PowerState, DateTime Time)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("SetPowerState");
                inParams["PowerState"] = PowerState;
                inParams["Time"] = ToDmtfDateTime(Time);
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetPowerState", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public enum EnhancedSessionModeStateValues
        {
            Allowed_and_available = 2,
            Not_allowed = 3,
            Allowed_but_not_available = 6,
            NULL_ENUM_VALUE = 0,
        }

        public enum FailedOverReplicationTypeValues
        {
            None = 0,
            Regular = 1,
            Application_consistent = 2,
            Planned = 3,
            NULL_ENUM_VALUE = 4,
        }

        public enum LastReplicationTypeValues
        {
            None = 0,
            Regular = 1,
            Application_consistent = 2,
            Planned = 3,
            NULL_ENUM_VALUE = 4,
        }

        public enum ReplicationHealthValues
        {
            Not_applicable = 0,
            Ok = 1,
            Warning = 2,
            Critical = 3,
            NULL_ENUM_VALUE = 4,
        }

        public enum ReplicationModeValues
        {
            None = 0,
            Primary = 1,
            Replica = 2,
            Test_Replica = 3,
            Extended_Replica = 4,
            NULL_ENUM_VALUE = 5,
        }

        public enum ReplicationStateValues
        {
            Disabled = 0,
            Ready_for_replication = 1,
            Waiting_to_complete_initial_replication = 2,
            Replicating = 3,
            Synced_replication_complete = 4,
            Recovered = 5,
            Committed = 6,
            Suspended = 7,
            Critical = 8,
            Waiting_to_start_resynchronization = 9,
            Resynchronizing = 10,
            Resynchronization_suspended = 11,
            Failover_in_progress = 12,
            Failback_in_progress = 13,
            Failback_complete = 14,
            NULL_ENUM_VALUE = 15,
        }
    }
}
