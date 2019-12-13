using System;
using System.Management;
using System.Globalization;

namespace Viridian.Msvm.VirtualSystem
{
    // Functions ShouldSerialize<PropertyName> are functions used by VS property browser to check if a particular property has to be serialized. These functions are added for all ValueType properties ( properties of type Int32, BOOL etc.. which cannot be set to null). These functions use Is<PropertyName>Null function. These functions are also used in the TypeConverter implementation for the properties to check for NULL value of property so that an empty value can be shown in Property browser in case of Drag and Drop in Visual studio.
    // Functions Is<PropertyName>Null() are used to check if a property is NULL.
    // Functions Reset<PropertyName> are added for Nullable Read/Write properties. These functions are used by VS designer in property browser to set a property to NULL.
    // Every property added to the class for WMI property has attributes set to define its behavior in Visual Studio designer and also to define a TypeConverter to be used.
    // Datetime conversion functions ToDateTime and ToDmtfDateTime are added to the class to convert DMTF datetime to System.DateTime and vice-versa.
    // An Early Bound class generated for the WMI class.Msvm_ComputerSystem
    public class ComputerSystem : IDisposable
    {

        // Private property to hold the WMI namespace in which the class resides.
        private static string CreatedWmiNamespace = "root\\virtualization\\v2";

        // Private property to hold the name of WMI class which created this class.
        private static string CreatedClassName = "Msvm_ComputerSystem";

        // Underlying lateBound WMI object.
        private ManagementObject PrivateLateBoundObject;

        // Private variable to hold the embedded property representing the instance.
        private readonly ManagementBaseObject embeddedObj;

        // Flag to indicate if the instance is an embedded object.
        private bool isEmbedded;

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public ComputerSystem() => InitializeObject(null, null, null);

        public ComputerSystem(string keyCreationClassName, string keyName) => InitializeObject(null, new ManagementPath(ConstructPath(keyCreationClassName, keyName)), null);

        public ComputerSystem(ManagementScope mgmtScope, string keyCreationClassName, string keyName) => InitializeObject(mgmtScope, new ManagementPath(ConstructPath(keyCreationClassName, keyName)), null);

        public ComputerSystem(ManagementPath path, ObjectGetOptions getOptions) => InitializeObject(null, path, getOptions);

        public ComputerSystem(ManagementScope mgmtScope, ManagementPath path) => InitializeObject(mgmtScope, path, null);

        public ComputerSystem(ManagementPath path) => InitializeObject(null, path, null);

        public ComputerSystem(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) => InitializeObject(mgmtScope, path, getOptions);

        public ComputerSystem(ManagementObject theObject)
        {
            Initialize();
            if (theObject != null && CheckIfProperClass(theObject) == true)
            {
                PrivateLateBoundObject = theObject;
                SystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
                LateBoundObject = PrivateLateBoundObject;
            }
            else
            {
                throw new ArgumentException("Class name does not match.");
            }
        }

        public ComputerSystem(ManagementBaseObject theObject)
        {
            Initialize();
            if (theObject != null && CheckIfProperClass(theObject) == true)
            {
                embeddedObj = theObject;
                SystemProperties = new ManagementSystemProperties(theObject);
                LateBoundObject = embeddedObj;
                isEmbedded = true;
            }
            else
            {
                throw new ArgumentException("Class name does not match.");
            }
        }

        // Property returns the namespace of the WMI class.
        public string OriginatingNamespace => "root\\virtualization\\v2";

        public string ManagementClassName
        {
            get
            {
                string strRet = CreatedClassName;
                if (LateBoundObject != null)
                {
                    if (LateBoundObject.ClassPath != null)
                    {
                        strRet = (string)LateBoundObject["__CLASS"];
                        if (string.IsNullOrEmpty(strRet))
                        {
                            strRet = CreatedClassName;
                        }
                    }
                }
                return strRet;
            }
        }

        // Property pointing to an embedded object to get System properties of the WMI object.
        public ManagementSystemProperties SystemProperties { get; private set; }

        // Property returning the underlying lateBound object.
        public ManagementBaseObject LateBoundObject { get; private set; }

        // ManagementScope of the object.
        public ManagementScope Scope
        {
            get
            {
                if (isEmbedded == false)
                {
                    return PrivateLateBoundObject.Scope;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (isEmbedded == false)
                {
                    PrivateLateBoundObject.Scope = value;
                }
            }
        }

        // Property to show the commit behavior for the WMI object. If true, WMI object will be automatically saved after each property modification.(ie. Put() is called after modification of a property).
        public bool AutoCommit { get; set; }

        // The ManagementPath of the underlying WMI object.
        public ManagementPath Path
        {
            get
            {
                if (isEmbedded == false)
                {
                    return PrivateLateBoundObject.Path;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (isEmbedded == false)
                {
                    if (CheckIfProperClass(null, value, null) != true)
                    {
                        throw new ArgumentException("Class name does not match.");
                    }
                    PrivateLateBoundObject.Path = value;
                }
            }
        }

        // Public static scope property which is used by the various methods.
        public static ManagementScope StaticScope { get; set; } = null;

        public ushort[] AvailableRequestedStates => (ushort[])LateBoundObject[nameof(AvailableRequestedStates)];

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public bool IsCommunicationStatusNull
        {
            get
            {
                if (LateBoundObject[nameof(CommunicationStatus)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

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

        public bool IsDetailedStatusNull
        {
            get
            {
                if (LateBoundObject[nameof(DetailedStatus)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

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

        public bool IsEnabledDefaultNull
        {
            get
            {
                if (LateBoundObject[nameof(EnabledDefault)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

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

        public bool IsEnabledStateNull
        {
            get
            {
                if (LateBoundObject[nameof(EnabledState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool IsEnhancedSessionModeStateNull
        {
            get
            {
                if (LateBoundObject[nameof(EnhancedSessionModeState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool IsFailedOverReplicationTypeNull
        {
            get
            {
                if (LateBoundObject[nameof(FailedOverReplicationType)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool IsHealthStateNull
        {
            get
            {
                if (LateBoundObject[nameof(HealthState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool IsHwThreadsPerCoreRealizedNull
        {
            get
            {
                if (LateBoundObject[nameof(HwThreadsPerCoreRealized)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool IsInstallDateNull
        {
            get
            {
                if (LateBoundObject[nameof(InstallDate)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

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

        public bool IsLastApplicationConsistentReplicationTimeNull
        {
            get
            {
                if (LateBoundObject[nameof(LastApplicationConsistentReplicationTime)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

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

        public bool IsLastReplicationTimeNull
        {
            get
            {
                if (LateBoundObject[nameof(LastReplicationTime)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
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

        public bool IsLastReplicationTypeNull
        {
            get
            {
                if (LateBoundObject[nameof(LastReplicationType)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
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

        public bool IsLastSuccessfulBackupTimeNull
        {
            get
            {
                if (LateBoundObject[nameof(LastSuccessfulBackupTime)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool IsNumberOfNumaNodesNull
        {
            get
            {
                if (LateBoundObject[nameof(NumberOfNumaNodes)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

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

        public bool IsOnTimeInMillisecondsNull
        {
            get
            {
                if (LateBoundObject[nameof(OnTimeInMilliseconds)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool IsOperatingStatusNull
        {
            get
            {
                if (LateBoundObject[nameof(OperatingStatus)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool IsPrimaryStatusNull
        {
            get
            {
                if (LateBoundObject[nameof(PrimaryStatus)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

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

        public bool IsProcessIDNull
        {
            get
            {
                if (LateBoundObject[nameof(ProcessID)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool IsReplicationHealthNull
        {
            get
            {
                if (LateBoundObject[nameof(ReplicationHealth)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool IsReplicationModeNull
        {
            get
            {
                if (LateBoundObject[nameof(ReplicationMode)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool IsReplicationStateNull
        {
            get
            {
                if (LateBoundObject[nameof(ReplicationState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool IsRequestedStateNull
        {
            get
            {
                if (LateBoundObject[nameof(RequestedState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool IsResetCapabilityNull
        {
            get
            {
                if (LateBoundObject[nameof(ResetCapability)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool IsTimeOfLastConfigurationChangeNull
        {
            get
            {
                if (LateBoundObject[nameof(TimeOfLastConfigurationChange)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

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

        public bool IsTimeOfLastStateChangeNull
        {
            get
            {
                if (LateBoundObject[nameof(TimeOfLastStateChange)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
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

        public bool IsTransitioningToStateNull
        {
            get
            {
                if (LateBoundObject[nameof(TransitioningToState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
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

        private bool CheckIfProperClass(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions OptionsParam)
        {
            if ((path != null)
                        && (string.Compare(path.ClassName, ManagementClassName, true, CultureInfo.InvariantCulture) == 0))
            {
                return true;
            }
            else
            {
                return CheckIfProperClass(new ManagementObject(mgmtScope, path, OptionsParam));
            }
        }

        private bool CheckIfProperClass(ManagementBaseObject theObj)
        {
            if ((theObj != null)
                        && (string.Compare((string)theObj["__CLASS"], ManagementClassName, true, CultureInfo.InvariantCulture) == 0))
            {
                return true;
            }
            else
            {
                Array parentClasses = (Array)theObj["__DERIVATION"];
                if (parentClasses != null)
                {
                    int count;
                    for (count = 0; count < parentClasses.Length; count = count + 1)
                    {
                        if (string.Compare((string)parentClasses.GetValue(count), ManagementClassName, true, CultureInfo.InvariantCulture) == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool ShouldSerializeCommunicationStatus()
        {
            if (IsCommunicationStatusNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeDetailedStatus()
        {
            if (IsDetailedStatusNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeEnabledDefault()
        {
            if (IsEnabledDefaultNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeEnabledState()
        {
            if (IsEnabledStateNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeEnhancedSessionModeState()
        {
            if (IsEnhancedSessionModeStateNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeFailedOverReplicationType()
        {
            if (IsFailedOverReplicationTypeNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeHealthState()
        {
            if (IsHealthStateNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeHwThreadsPerCoreRealized()
        {
            if (IsHwThreadsPerCoreRealizedNull == false)
            {
                return true;
            }
            return false;
        }

        // Converts a given datetime in DMTF format to System.DateTime object.
        static DateTime ToDateTime(string dmtfDate)
        {
            DateTime initializer = DateTime.MinValue;
            int year = initializer.Year;
            int month = initializer.Month;
            int day = initializer.Day;
            int hour = initializer.Hour;
            int minute = initializer.Minute;
            int second = initializer.Second;
            long ticks = 0;
            string dmtf = dmtfDate;
            if (dmtf == null)
            {
                throw new ArgumentOutOfRangeException(dmtf);
            }
            if (dmtf.Length == 0)
            {
                throw new ArgumentOutOfRangeException(dmtf);
            }
            if (dmtf.Length != 25)
            {
                throw new ArgumentOutOfRangeException(dmtf);
            }

            string tempString;
            try
            {
                tempString = dmtf.Substring(0, 4);
                if ("****" != tempString)
                {
                    year = int.Parse(tempString);
                }
                tempString = dmtf.Substring(4, 2);
                if ("**" != tempString)
                {
                    month = int.Parse(tempString);
                }
                tempString = dmtf.Substring(6, 2);
                if ("**" != tempString)
                {
                    day = int.Parse(tempString);
                }
                tempString = dmtf.Substring(8, 2);
                if ("**" != tempString)
                {
                    hour = int.Parse(tempString);
                }
                tempString = dmtf.Substring(10, 2);
                if ("**" != tempString)
                {
                    minute = int.Parse(tempString);
                }
                tempString = dmtf.Substring(12, 2);
                if ("**" != tempString)
                {
                    second = int.Parse(tempString);
                }
                tempString = dmtf.Substring(15, 6);
                if ("******" != tempString)
                {
                    ticks = long.Parse(tempString) * (TimeSpan.TicksPerMillisecond / 1000);
                }
                if ((year < 0)
                            || (month < 0)
                            || (day < 0)
                            || (hour < 0)
                            || (minute < 0)
                            || (minute < 0)
                            || (second < 0)
                            || (ticks < 0))
                {
                    throw new ArgumentOutOfRangeException(year.ToString());
                }
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException(null, e.Message);
            }
            DateTime datetime = new DateTime(year, month, day, hour, minute, second, 0);
            datetime = datetime.AddTicks(ticks);
            TimeSpan tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(datetime);
            long OffsetMins = tickOffset.Ticks / TimeSpan.TicksPerMinute;
            tempString = dmtf.Substring(22, 3);
            if (tempString != "******")
            {
                tempString = dmtf.Substring(21, 4);
                int UTCOffset;
                try
                {
                    UTCOffset = int.Parse(tempString);
                }
                catch (Exception e)
                {
                    throw new ArgumentOutOfRangeException(null, e.Message);
                }

                int OffsetToBeAdjusted = (int)(OffsetMins - UTCOffset);
                datetime = datetime.AddMinutes(OffsetToBeAdjusted);
            }
            return datetime;
        }

        // Converts a given System.DateTime object to DMTF datetime format.
        static string ToDmtfDateTime(DateTime date)
        {
            TimeSpan tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(date);
            long OffsetMins = tickOffset.Ticks / TimeSpan.TicksPerMinute;
            string utcString;
            if (Math.Abs(OffsetMins) > 999)
            {
                date = date.ToUniversalTime();
                utcString = "+000";
            }
            else
            {
                if (tickOffset.Ticks >= 0)
                {
                    utcString = string.Concat("+", (tickOffset.Ticks / TimeSpan.TicksPerMinute).ToString().PadLeft(3, '0'));
                }
                else
                {
                    string strTemp = OffsetMins.ToString();
                    utcString = string.Concat("-", strTemp.Substring(1, strTemp.Length - 1).PadLeft(3, '0'));
                }
            }
            string dmtfDateTime = date.Year.ToString().PadLeft(4, '0');
            dmtfDateTime = string.Concat(dmtfDateTime, date.Month.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Day.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Hour.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Minute.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Second.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ".");
            DateTime dtTemp = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
            long microsec = (date.Ticks - dtTemp.Ticks)
                        * 1000
                        / TimeSpan.TicksPerMillisecond;
            string strMicrosec = microsec.ToString();
            if (strMicrosec.Length > 6)
            {
                strMicrosec = strMicrosec.Substring(0, 6);
            }
            dmtfDateTime = string.Concat(dmtfDateTime, strMicrosec.PadLeft(6, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, utcString);
            return dmtfDateTime;
        }

        private bool ShouldSerializeInstallDate()
        {
            if (IsInstallDateNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeLastApplicationConsistentReplicationTime()
        {
            if (IsLastApplicationConsistentReplicationTimeNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeLastReplicationTime()
        {
            if (IsLastReplicationTimeNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeLastReplicationType()
        {
            if (IsLastReplicationTypeNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeLastSuccessfulBackupTime()
        {
            if (IsLastSuccessfulBackupTimeNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeNumberOfNumaNodes()
        {
            if (IsNumberOfNumaNodesNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeOnTimeInMilliseconds()
        {
            if (IsOnTimeInMillisecondsNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeOperatingStatus()
        {
            if (IsOperatingStatusNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializePrimaryStatus()
        {
            if (IsPrimaryStatusNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeProcessID()
        {
            if (IsProcessIDNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeReplicationHealth()
        {
            if (IsReplicationHealthNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeReplicationMode()
        {
            if (IsReplicationModeNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeReplicationState()
        {
            if (IsReplicationStateNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeRequestedState()
        {
            if (IsRequestedStateNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeResetCapability()
        {
            if (IsResetCapabilityNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeTimeOfLastConfigurationChange()
        {
            if (IsTimeOfLastConfigurationChangeNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeTimeOfLastStateChange()
        {
            if (IsTimeOfLastStateChangeNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeTransitioningToState()
        {
            if (IsTransitioningToStateNull == false)
            {
                return true;
            }
            return false;
        }

        public void CommitObject()
        {
            if (isEmbedded == false)
            {
                PrivateLateBoundObject.Put();
            }
        }

        public void CommitObject(PutOptions putOptions)
        {
            if (isEmbedded == false)
            {
                PrivateLateBoundObject.Put(putOptions);
            }
        }

        private void Initialize()
        {
            AutoCommit = true;
            isEmbedded = false;
        }

        private static string ConstructPath(string keyCreationClassName, string keyName)
        {
            string strPath = "root\\virtualization\\v2:Msvm_ComputerSystem";
            strPath = string.Concat(strPath, string.Concat(".CreationClassName=", string.Concat("\"", string.Concat(keyCreationClassName, "\""))));
            strPath = string.Concat(strPath, string.Concat(",Name=", string.Concat("\"", string.Concat(keyName, "\""))));
            return strPath;
        }

        private void InitializeObject(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions)
        {
            Initialize();
            if (path != null)
            {
                if (CheckIfProperClass(mgmtScope, path, getOptions) != true)
                {
                    throw new ArgumentException("Class name does not match.");
                }
            }
            PrivateLateBoundObject = new ManagementObject(mgmtScope, path, getOptions);
            SystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            LateBoundObject = PrivateLateBoundObject;
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static MsvmCollection<ComputerSystem> GetInstances() => GetInstances(null, null, null);

        public static MsvmCollection<ComputerSystem> GetInstances(string condition) => GetInstances(null, condition, null);

        public static MsvmCollection<ComputerSystem> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties);

        public static MsvmCollection<ComputerSystem> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties);

        public static MsvmCollection<ComputerSystem> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions)
        {
            if (mgmtScope == null)
            {
                if (StaticScope == null)
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\virtualization\\v2";
                }
                else
                {
                    mgmtScope = StaticScope;
                }
            }
            ManagementPath pathObj = new ManagementPath
            {
                ClassName = "Msvm_ComputerSystem",
                NamespacePath = "root\\virtualization\\v2"
            };
            ManagementClass clsObject = new ManagementClass(mgmtScope, pathObj, null);
            if (enumOptions == null)
            {
                enumOptions = new EnumerationOptions
                {
                    EnsureLocatable = true
                };
            }
            return new MsvmCollection<ComputerSystem>(clsObject.GetInstances(enumOptions));
        }

        public static MsvmCollection<ComputerSystem> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null);

        public static MsvmCollection<ComputerSystem> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties);

        public static MsvmCollection<ComputerSystem> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties)
        {
            if (mgmtScope == null)
            {
                if (StaticScope == null)
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\virtualization\\v2";
                }
                else
                {
                    mgmtScope = StaticScope;
                }
            }
            using (ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery("Msvm_ComputerSystem", condition, selectedProperties)))
            {
                EnumerationOptions enumOptions = new EnumerationOptions
                {
                    EnsureLocatable = true
                };
                ObjectSearcher.Options = enumOptions;
                return new MsvmCollection<ComputerSystem>(ObjectSearcher.Get());
            }
        }

        public static ComputerSystem CreateInstance()
        {
            ManagementScope mgmtScope;
            if (StaticScope == null)
            {
                mgmtScope = new ManagementScope();
                mgmtScope.Path.NamespacePath = CreatedWmiNamespace;
            }
            else
            {
                mgmtScope = StaticScope;
            }
            ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
            using (ManagementClass tmpMgmtClass = new ManagementClass(mgmtScope, mgmtPath, null))
            {
                return new ComputerSystem(tmpMgmtClass.CreateInstance());
            }
        }

        public void Delete() => PrivateLateBoundObject.Delete();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                PrivateLateBoundObject.Dispose();
            }
        }

        ~ComputerSystem()
        {
            Dispose(false);
        }

        public uint InjectNonMaskableInterrupt(out ManagementPath Job)
        {
            if (isEmbedded == false)
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
            if (isEmbedded == false)
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
            if (isEmbedded == false)
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
            if (isEmbedded == false)
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
            if (isEmbedded == false)
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
