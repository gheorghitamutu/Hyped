using System;
using System.ComponentModel;
using System.Management;
using System.Collections;
using System.Globalization;

namespace Viridian.Msvm.VirtualSystem
{
    // Functions ShouldSerialize<PropertyName> are functions used by VS property browser to check if a particular property has to be serialized. These functions are added for all ValueType properties ( properties of type Int32, BOOL etc.. which cannot be set to null). These functions use Is<PropertyName>Null function. These functions are also used in the TypeConverter implementation for the properties to check for NULL value of property so that an empty value can be shown in Property browser in case of Drag and Drop in Visual studio.
    // Functions Is<PropertyName>Null() are used to check if a property is NULL.
    // Functions Reset<PropertyName> are added for Nullable Read/Write properties. These functions are used by VS designer in property browser to set a property to NULL.
    // Every property added to the class for WMI property has attributes set to define its behavior in Visual Studio designer and also to define a TypeConverter to be used.
    // Datetime conversion functions ToDateTime and ToDmtfDateTime are added to the class to convert DMTF datetime to System.DateTime and vice-versa.
    // An Early Bound class generated for the WMI class.Msvm_ComputerSystem
    public class ComputerSystem : Component
    {

        // Private property to hold the WMI namespace in which the class resides.
        private static string CreatedWmiNamespace = "root\\virtualization\\v2";

        // Private property to hold the name of WMI class which created this class.
        private static string CreatedClassName = "Msvm_ComputerSystem";

        // Private member variable to hold the ManagementScope which is used by the various methods.
        private static ManagementScope statMgmtScope = null;

        private ManagementSystemProperties PrivateSystemProperties;

        // Underlying lateBound WMI object.
        private ManagementObject PrivateLateBoundObject;

        // Member variable to store the 'automatic commit' behavior for the class.
        private bool AutoCommitProp;

        // Private variable to hold the embedded property representing the instance.
        private ManagementBaseObject embeddedObj;

        // The current WMI object used
        private ManagementBaseObject curObj;

        // Flag to indicate if the instance is an embedded object.
        private bool isEmbedded;

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public ComputerSystem()
        {
            InitializeObject(null, null, null);
        }

        public ComputerSystem(string keyCreationClassName, string keyName)
        {
            InitializeObject(null, new ManagementPath(ConstructPath(keyCreationClassName, keyName)), null);
        }

        public ComputerSystem(ManagementScope mgmtScope, string keyCreationClassName, string keyName)
        {
            InitializeObject(mgmtScope, new ManagementPath(ConstructPath(keyCreationClassName, keyName)), null);
        }

        public ComputerSystem(ManagementPath path, ObjectGetOptions getOptions)
        {
            InitializeObject(null, path, getOptions);
        }

        public ComputerSystem(ManagementScope mgmtScope, ManagementPath path)
        {
            InitializeObject(mgmtScope, path, null);
        }

        public ComputerSystem(ManagementPath path)
        {
            InitializeObject(null, path, null);
        }

        public ComputerSystem(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions)
        {
            InitializeObject(mgmtScope, path, getOptions);
        }

        public ComputerSystem(ManagementObject theObject)
        {
            Initialize();
            if (CheckIfProperClass(theObject) == true)
            {
                PrivateLateBoundObject = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
                curObj = PrivateLateBoundObject;
            }
            else
            {
                throw new ArgumentException("Class name does not match.");
            }
        }

        public ComputerSystem(ManagementBaseObject theObject)
        {
            Initialize();
            if (CheckIfProperClass(theObject) == true)
            {
                embeddedObj = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(theObject);
                curObj = embeddedObj;
                isEmbedded = true;
            }
            else
            {
                throw new ArgumentException("Class name does not match.");
            }
        }

        // Property returns the namespace of the WMI class.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OriginatingNamespace
        {
            get
            {
                return "root\\virtualization\\v2";
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ManagementClassName
        {
            get
            {
                string strRet = CreatedClassName;
                if (curObj != null)
                {
                    if (curObj.ClassPath != null)
                    {
                        strRet = (string)curObj["__CLASS"];
                        if ((strRet == null)
                                    || (strRet == string.Empty))
                        {
                            strRet = CreatedClassName;
                        }
                    }
                }
                return strRet;
            }
        }

        // Property pointing to an embedded object to get System properties of the WMI object.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementSystemProperties SystemProperties
        {
            get
            {
                return PrivateSystemProperties;
            }
        }

        // Property returning the underlying lateBound object.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementBaseObject LateBoundObject
        {
            get
            {
                return curObj;
            }
        }

        // ManagementScope of the object.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoCommit
        {
            get
            {
                return AutoCommitProp;
            }
            set
            {
                AutoCommitProp = value;
            }
        }

        // The ManagementPath of the underlying WMI object.
        [Browsable(true)]
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
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static ManagementScope StaticScope
        {
            get
            {
                return statMgmtScope;
            }
            set
            {
                statMgmtScope = value;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ushort[] AvailableRequestedStates
        {
            get
            {
                return (ushort[])curObj[nameof(AvailableRequestedStates)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Caption
        {
            get
            {
                return (string)curObj[nameof(Caption)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsCommunicationStatusNull
        {
            get
            {
                if (curObj[nameof(CommunicationStatus)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort CommunicationStatus
        {
            get
            {
                if (curObj[nameof(CommunicationStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)curObj[nameof(CommunicationStatus)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CreationClassName
        {
            get
            {
                return (string)curObj[nameof(CreationClassName)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ushort[] Dedicated
        {
            get
            {
                return (ushort[])curObj[nameof(Dedicated)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Description
        {
            get
            {
                return (string)curObj[nameof(Description)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDetailedStatusNull
        {
            get
            {
                if (curObj[nameof(DetailedStatus)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort DetailedStatus
        {
            get
            {
                if (curObj[nameof(DetailedStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)curObj[nameof(DetailedStatus)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ElementName
        {
            get
            {
                return (string)curObj[nameof(ElementName)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEnabledDefaultNull
        {
            get
            {
                if (curObj[nameof(EnabledDefault)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort EnabledDefault
        {
            get
            {
                if (curObj[nameof(EnabledDefault)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)curObj[nameof(EnabledDefault)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEnabledStateNull
        {
            get
            {
                if (curObj[nameof(EnabledState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort EnabledState
        {
            get
            {
                if (curObj[nameof(EnabledState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)curObj[nameof(EnabledState)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEnhancedSessionModeStateNull
        {
            get
            {
                if (curObj[nameof(EnhancedSessionModeState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Indicates whether or not enhanced mode connections are allowed by the host and if" +
            " allowed, whether or not they are available to the virtual machine.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public EnhancedSessionModeStateValues EnhancedSessionModeState
        {
            get
            {
                if (curObj[nameof(EnhancedSessionModeState)] == null)
                {
                    return (EnhancedSessionModeStateValues)Convert.ToInt32(0);
                }
                return (EnhancedSessionModeStateValues)Convert.ToInt32(curObj[nameof(EnhancedSessionModeState)]);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFailedOverReplicationTypeNull
        {
            get
            {
                if (curObj[nameof(FailedOverReplicationType)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Type of failover that was performed for the virtual machine.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public FailedOverReplicationTypeValues FailedOverReplicationType
        {
            get
            {
                if (curObj[nameof(FailedOverReplicationType)] == null)
                {
                    return (FailedOverReplicationTypeValues)Convert.ToInt32(4);
                }
                return (FailedOverReplicationTypeValues)Convert.ToInt32(curObj[nameof(FailedOverReplicationType)]);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsHealthStateNull
        {
            get
            {
                if (curObj[nameof(HealthState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort HealthState
        {
            get
            {
                if (curObj[nameof(HealthState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)curObj[nameof(HealthState)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsHwThreadsPerCoreRealizedNull
        {
            get
            {
                if (curObj[nameof(HwThreadsPerCoreRealized)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Indicates the number of SMT threads per core reported to the guest.  This reporti" +
            "ng is independent of whether the hardware for SMT is present. ")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint HwThreadsPerCoreRealized
        {
            get
            {
                if (curObj[nameof(HwThreadsPerCoreRealized)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)curObj[nameof(HwThreadsPerCoreRealized)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] IdentifyingDescriptions
        {
            get
            {
                return (string[])curObj[nameof(IdentifyingDescriptions)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInstallDateNull
        {
            get
            {
                if (curObj[nameof(InstallDate)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public DateTime InstallDate
        {
            get
            {
                if (curObj[nameof(InstallDate)] != null)
                {
                    return ToDateTime((string)curObj[nameof(InstallDate)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InstanceID
        {
            get
            {
                return (string)curObj[nameof(InstanceID)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLastApplicationConsistentReplicationTimeNull
        {
            get
            {
                if (curObj[nameof(LastApplicationConsistentReplicationTime)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The time at which the last application consistent replication is received on reco" +
            "very for the virtual machine.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public DateTime LastApplicationConsistentReplicationTime
        {
            get
            {
                if (curObj[nameof(LastApplicationConsistentReplicationTime)] != null)
                {
                    return ToDateTime((string)curObj[nameof(LastApplicationConsistentReplicationTime)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLastReplicationTimeNull
        {
            get
            {
                if (curObj[nameof(LastReplicationTime)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The time at which the last replication is received on recovery for the virtual ma" +
            "chine.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public DateTime LastReplicationTime
        {
            get
            {
                if (curObj[nameof(LastReplicationTime)] != null)
                {
                    return ToDateTime((string)curObj[nameof(LastReplicationTime)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLastReplicationTypeNull
        {
            get
            {
                if (curObj[nameof(LastReplicationType)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Type of the last replication that was received for the virtual machine.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public LastReplicationTypeValues LastReplicationType
        {
            get
            {
                if (curObj[nameof(LastReplicationType)] == null)
                {
                    return (LastReplicationTypeValues)Convert.ToInt32(4);
                }
                return (LastReplicationTypeValues)Convert.ToInt32(curObj[nameof(LastReplicationType)]);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLastSuccessfulBackupTimeNull
        {
            get
            {
                if (curObj[nameof(LastSuccessfulBackupTime)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The time at which the last successful backup has completed for the virtual machin" +
            "e.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public DateTime LastSuccessfulBackupTime
        {
            get
            {
                if (curObj[nameof(LastSuccessfulBackupTime)] != null)
                {
                    return ToDateTime((string)curObj[nameof(LastSuccessfulBackupTime)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Name
        {
            get
            {
                return (string)curObj[nameof(Name)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string NameFormat
        {
            get
            {
                return (string)curObj[nameof(NameFormat)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNumberOfNumaNodesNull
        {
            get
            {
                if (curObj[nameof(NumberOfNumaNodes)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The number of non-uniform memory access (NUMA) nodes of the computer system. When Msvm_ComputerSystem represents the hosting computer system, this property contains the count of physical NUMA nodes. When Msvm_ComputerSystem represents a virtual computer system, this property contains the number of virtual NUMA nodes that are presented to the guest OS through the ACPI System Resource Affinity Table (SRAT).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort NumberOfNumaNodes
        {
            get
            {
                if (curObj[nameof(NumberOfNumaNodes)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)curObj[nameof(NumberOfNumaNodes)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsOnTimeInMillisecondsNull
        {
            get
            {
                if (curObj[nameof(OnTimeInMilliseconds)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"For the virtual system, this property describes the total up time, in milliseconds, since the machine was last turned on, reset, or restored. This time excludes the time the virtual system was in the paused state. For the host system, this property is set to NULL.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong OnTimeInMilliseconds
        {
            get
            {
                if (curObj[nameof(OnTimeInMilliseconds)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)curObj[nameof(OnTimeInMilliseconds)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsOperatingStatusNull
        {
            get
            {
                if (curObj[nameof(OperatingStatus)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort OperatingStatus
        {
            get
            {
                if (curObj[nameof(OperatingStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)curObj[nameof(OperatingStatus)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ushort[] OperationalStatus
        {
            get
            {
                return (ushort[])curObj[nameof(OperationalStatus)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] OtherDedicatedDescriptions
        {
            get
            {
                return (string[])curObj[nameof(OtherDedicatedDescriptions)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OtherEnabledState
        {
            get
            {
                return (string)curObj[nameof(OtherEnabledState)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] OtherIdentifyingInfo
        {
            get
            {
                return (string[])curObj[nameof(OtherIdentifyingInfo)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ushort[] PowerManagementCapabilities
        {
            get
            {
                return (ushort[])curObj[nameof(PowerManagementCapabilities)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PrimaryOwnerContact
        {
            get
            {
                return (string)curObj[nameof(PrimaryOwnerContact)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PrimaryOwnerName
        {
            get
            {
                return (string)curObj[nameof(PrimaryOwnerName)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPrimaryStatusNull
        {
            get
            {
                if (curObj[nameof(PrimaryStatus)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort PrimaryStatus
        {
            get
            {
                if (curObj[nameof(PrimaryStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)curObj[nameof(PrimaryStatus)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsProcessIDNull
        {
            get
            {
                if (curObj[nameof(ProcessID)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The identifier of the process under which this virtual machine is running. This v" +
            "alue can be used to uniquely identify the instance of Vmwp.exe on the system tha" +
            "t is running the virtual machine.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint ProcessID
        {
            get
            {
                if (curObj[nameof(ProcessID)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)curObj[nameof(ProcessID)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsReplicationHealthNull
        {
            get
            {
                if (curObj[nameof(ReplicationHealth)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Replication health for the virtual machine.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ReplicationHealthValues ReplicationHealth
        {
            get
            {
                if (curObj[nameof(ReplicationHealth)] == null)
                {
                    return (ReplicationHealthValues)Convert.ToInt32(4);
                }
                return (ReplicationHealthValues)Convert.ToInt32(curObj[nameof(ReplicationHealth)]);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsReplicationModeNull
        {
            get
            {
                if (curObj[nameof(ReplicationMode)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Identifies replication type for the virtual machine.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ReplicationModeValues ReplicationMode
        {
            get
            {
                if (curObj[nameof(ReplicationMode)] == null)
                {
                    return (ReplicationModeValues)Convert.ToInt32(5);
                }
                return (ReplicationModeValues)Convert.ToInt32(curObj[nameof(ReplicationMode)]);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsReplicationStateNull
        {
            get
            {
                if (curObj[nameof(ReplicationState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Replication state for the virtual machine.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ReplicationStateValues ReplicationState
        {
            get
            {
                if (curObj[nameof(ReplicationState)] == null)
                {
                    return (ReplicationStateValues)Convert.ToInt32(15);
                }
                return (ReplicationStateValues)Convert.ToInt32(curObj[nameof(ReplicationState)]);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsRequestedStateNull
        {
            get
            {
                if (curObj[nameof(RequestedState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort RequestedState
        {
            get
            {
                if (curObj[nameof(RequestedState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)curObj[nameof(RequestedState)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsResetCapabilityNull
        {
            get
            {
                if (curObj[nameof(ResetCapability)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort ResetCapability
        {
            get
            {
                if (curObj[nameof(ResetCapability)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)curObj[nameof(ResetCapability)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] Roles
        {
            get
            {
                return (string[])curObj[nameof(Roles)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Status
        {
            get
            {
                return (string)curObj[nameof(Status)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] StatusDescriptions
        {
            get
            {
                return (string[])curObj[nameof(StatusDescriptions)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTimeOfLastConfigurationChangeNull
        {
            get
            {
                if (curObj[nameof(TimeOfLastConfigurationChange)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The date and time when the virtual machine configuration file was last modified. " +
            "The configuration file is modified during certain virtual machine operations, as" +
            " well as when any of the virtual machine or device settings are added, modified," +
            " or removed.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public DateTime TimeOfLastConfigurationChange
        {
            get
            {
                if (curObj[nameof(TimeOfLastConfigurationChange)] != null)
                {
                    return ToDateTime((string)curObj[nameof(TimeOfLastConfigurationChange)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTimeOfLastStateChangeNull
        {
            get
            {
                if (curObj[nameof(TimeOfLastStateChange)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public DateTime TimeOfLastStateChange
        {
            get
            {
                if (curObj[nameof(TimeOfLastStateChange)] != null)
                {
                    return ToDateTime((string)curObj[nameof(TimeOfLastStateChange)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTransitioningToStateNull
        {
            get
            {
                if (curObj[nameof(TransitioningToState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort TransitioningToState
        {
            get
            {
                if (curObj[nameof(TransitioningToState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)curObj[nameof(TransitioningToState)];
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

        [Browsable(true)]
        public void CommitObject()
        {
            if (isEmbedded == false)
            {
                PrivateLateBoundObject.Put();
            }
        }

        [Browsable(true)]
        public void CommitObject(PutOptions putOptions)
        {
            if (isEmbedded == false)
            {
                PrivateLateBoundObject.Put(putOptions);
            }
        }

        private void Initialize()
        {
            AutoCommitProp = true;
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
            PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            curObj = PrivateLateBoundObject;
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static ComputerSystemCollection GetInstances()
        {
            return GetInstances(null, null, null);
        }

        public static ComputerSystemCollection GetInstances(string condition)
        {
            return GetInstances(null, condition, null);
        }

        public static ComputerSystemCollection GetInstances(string[] selectedProperties)
        {
            return GetInstances(null, null, selectedProperties);
        }

        public static ComputerSystemCollection GetInstances(string condition, string[] selectedProperties)
        {
            return GetInstances(null, condition, selectedProperties);
        }

        public static ComputerSystemCollection GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions)
        {
            if (mgmtScope == null)
            {
                if (statMgmtScope == null)
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\virtualization\\v2";
                }
                else
                {
                    mgmtScope = statMgmtScope;
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
            return new ComputerSystemCollection(clsObject.GetInstances(enumOptions));
        }

        public static ComputerSystemCollection GetInstances(ManagementScope mgmtScope, string condition)
        {
            return GetInstances(mgmtScope, condition, null);
        }

        public static ComputerSystemCollection GetInstances(ManagementScope mgmtScope, string[] selectedProperties)
        {
            return GetInstances(mgmtScope, null, selectedProperties);
        }

        public static ComputerSystemCollection GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties)
        {
            if (mgmtScope == null)
            {
                if (statMgmtScope == null)
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\virtualization\\v2";
                }
                else
                {
                    mgmtScope = statMgmtScope;
                }
            }
            ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery("Msvm_ComputerSystem", condition, selectedProperties));
            EnumerationOptions enumOptions = new EnumerationOptions
            {
                EnsureLocatable = true
            };
            ObjectSearcher.Options = enumOptions;
            return new ComputerSystemCollection(ObjectSearcher.Get());
        }

        [Browsable(true)]
        public static ComputerSystem CreateInstance()
        {
            ManagementScope mgmtScope;
            if (statMgmtScope == null)
            {
                mgmtScope = new ManagementScope();
                mgmtScope.Path.NamespacePath = CreatedWmiNamespace;
            }
            else
            {
                mgmtScope = statMgmtScope;
            }
            ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
            using (ManagementClass tmpMgmtClass = new ManagementClass(mgmtScope, mgmtPath, null))
            {
                return new ComputerSystem(tmpMgmtClass.CreateInstance());
            }
        }

        [Browsable(true)]
        public void Delete()
        {
            PrivateLateBoundObject.Delete();
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

        // Enumerator implementation for enumerating instances of the class.
        public class ComputerSystemCollection : object, ICollection
        {

            private ManagementObjectCollection privColObj;

            public ComputerSystemCollection(ManagementObjectCollection objCollection)
            {
                privColObj = objCollection;
            }

            public virtual int Count
            {
                get
                {
                    return privColObj.Count;
                }
            }

            public virtual bool IsSynchronized
            {
                get
                {
                    return privColObj.IsSynchronized;
                }
            }

            public virtual object SyncRoot
            {
                get
                {
                    return this;
                }
            }

            public virtual void CopyTo(Array array, int index)
            {
                privColObj.CopyTo(array, index);
                int nCtr;
                for (nCtr = 0; nCtr < array.Length; nCtr = nCtr + 1)
                {
                    array.SetValue(new ComputerSystem((ManagementObject)array.GetValue(nCtr)), nCtr);
                }
            }

            public virtual IEnumerator GetEnumerator()
            {
                return new ComputerSystemEnumerator(privColObj.GetEnumerator());
            }

            public class ComputerSystemEnumerator : object, IEnumerator
            {

                private ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;

                public ComputerSystemEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum)
                {
                    privObjEnum = objEnum;
                }

                public virtual object Current
                {
                    get
                    {
                        return new ComputerSystem((ManagementObject)privObjEnum.Current);
                    }
                }

                public virtual bool MoveNext()
                {
                    return privObjEnum.MoveNext();
                }

                public virtual void Reset()
                {
                    privObjEnum.Reset();
                }
            }
        }

        // TypeConverter to handle null values for ValueType properties
        public class WMIValueTypeConverter : TypeConverter
        {

            private TypeConverter baseConverter;

            private Type baseType;

            public WMIValueTypeConverter(Type inBaseType)
            {
                baseConverter = TypeDescriptor.GetConverter(inBaseType);
                baseType = inBaseType;
            }

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type srcType)
            {
                return baseConverter.CanConvertFrom(context, srcType);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return baseConverter.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                return baseConverter.ConvertFrom(context, culture, value);
            }

            public override object CreateInstance(ITypeDescriptorContext context, IDictionary dictionary)
            {
                return baseConverter.CreateInstance(context, dictionary);
            }

            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                return baseConverter.GetCreateInstanceSupported(context);
            }

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributeVar)
            {
                return baseConverter.GetProperties(context, value, attributeVar);
            }

            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return baseConverter.GetPropertiesSupported(context);
            }

            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return baseConverter.GetStandardValues(context);
            }

            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return baseConverter.GetStandardValuesExclusive(context);
            }

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return baseConverter.GetStandardValuesSupported(context);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (baseType.BaseType == typeof(Enum))
                {
                    if (value.GetType() == destinationType)
                    {
                        return value;
                    }
                    if ((value == null)
                                && (context != null)
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))
                    {
                        return "NULL_ENUM_VALUE";
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if ((baseType == typeof(bool))
                            && (baseType.BaseType == typeof(ValueType)))
                {
                    if ((value == null)
                                && (context != null)
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))
                    {
                        return "";
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if ((context != null)
                            && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))
                {
                    return "";
                }
                return baseConverter.ConvertTo(context, culture, value, destinationType);
            }
        }

        // Embedded class to represent WMI system Properties.
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ManagementSystemProperties
        {

            private ManagementBaseObject PrivateLateBoundObject;

            public ManagementSystemProperties(ManagementBaseObject ManagedObject)
            {
                PrivateLateBoundObject = ManagedObject;
            }

            [Browsable(true)]
            public int GENUS
            {
                get
                {
                    return (int)PrivateLateBoundObject["__GENUS"];
                }
            }

            [Browsable(true)]
            public string CLASS
            {
                get
                {
                    return (string)PrivateLateBoundObject["__CLASS"];
                }
            }

            [Browsable(true)]
            public string SUPERCLASS
            {
                get
                {
                    return (string)PrivateLateBoundObject["__SUPERCLASS"];
                }
            }

            [Browsable(true)]
            public string DYNASTY
            {
                get
                {
                    return (string)PrivateLateBoundObject["__DYNASTY"];
                }
            }

            [Browsable(true)]
            public string RELPATH
            {
                get
                {
                    return (string)PrivateLateBoundObject["__RELPATH"];
                }
            }

            [Browsable(true)]
            public int PROPERTY_COUNT
            {
                get
                {
                    return (int)PrivateLateBoundObject["__PROPERTY_COUNT"];
                }
            }

            [Browsable(true)]
            public string[] DERIVATION
            {
                get
                {
                    return (string[])PrivateLateBoundObject["__DERIVATION"];
                }
            }

            [Browsable(true)]
            public string SERVER
            {
                get
                {
                    return (string)PrivateLateBoundObject["__SERVER"];
                }
            }

            [Browsable(true)]
            public string NAMESPACE
            {
                get
                {
                    return (string)PrivateLateBoundObject["__NAMESPACE"];
                }
            }

            [Browsable(true)]
            public string PATH
            {
                get
                {
                    return (string)PrivateLateBoundObject["__PATH"];
                }
            }
        }
    }
}
