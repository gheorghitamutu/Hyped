using System;
using System.Management;
using System.Globalization;

namespace Viridian.Msvm.VirtualSystem
{
    // Functions ShouldSerialize<PropertyName> are functions used by VS property browser to check if a particular property has to be serialized. These functions are added for all ValueType properties ( properties of type Int32, BOOL etc.. which cannot be set to null). These functions use Is<PropertyName>Null function. These functions are also used in the TypeConverter implementation for the properties to check for NULL value of property so that an empty value can be shown in Property browser in case of Drag and Drop in Visual studio.
    // Functions Is<PropertyName>Null() are used to check if a property is NULL.
    // Functions Reset<PropertyName> are added for Nullable Read/Write properties. These functions are used by VS designer in property browser to set a property to NULL.
    // Every property added to the class for WMI property has attributes set to define its behavior in Visual Studio designer and also to define a TypeConverter to be used.
    // Time interval functions  ToTimeSpan and ToDmtfTimeInterval are added to the class to convert DMTF Time Interval to  System.TimeSpan and vice-versa.
    // Datetime conversion functions ToDateTime and ToDmtfDateTime are added to the class to convert DMTF datetime to System.DateTime and vice-versa.
    // An Early Bound class generated for the WMI class.Msvm_VirtualSystemSettingData
    public class VirtualSystemSettingData : IDisposable
    {
        // Private property to hold the WMI namespace in which the class resides.
        private static string CreatedWmiNamespace = "root\\virtualization\\v2";

        // Private property to hold the name of WMI class which created this class.
        private static string CreatedClassName = "Msvm_VirtualSystemSettingData";

        // Underlying lateBound WMI object.
        private ManagementObject PrivateLateBoundObject;

        // Private variable to hold the embedded property representing the instance.
        private readonly ManagementBaseObject embeddedObj;

        // Flag to indicate if the instance is an embedded object.
        private bool isEmbedded;

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public VirtualSystemSettingData() => InitializeObject(null, null, null);

        public VirtualSystemSettingData(string keyInstanceID) => InitializeObject(null, new ManagementPath(ConstructPath(keyInstanceID)), null);

        public VirtualSystemSettingData(ManagementScope mgmtScope, string keyInstanceID) => InitializeObject(mgmtScope, new ManagementPath(ConstructPath(keyInstanceID)), null);

        public VirtualSystemSettingData(ManagementPath path, ObjectGetOptions getOptions) => InitializeObject(null, path, getOptions);

        public VirtualSystemSettingData(ManagementScope mgmtScope, ManagementPath path) => InitializeObject(mgmtScope, path, null);

        public VirtualSystemSettingData(ManagementPath path) => InitializeObject(null, path, null);

        public VirtualSystemSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) => InitializeObject(mgmtScope, path, getOptions);

        public VirtualSystemSettingData(ManagementObject theObject)
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

        public VirtualSystemSettingData(ManagementBaseObject theObject)
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

        /*
         * Any additional information provided to the recovery action. The meaning of this property is defined by the action in AutomaticRecoveryAction.
         * If AutomaticRecoveryAction is 0 ("None") or 1 ("Restart"), this value is NULL.
         * If AutomaticRecoveryAction is 2 (""Revert to Snapshot""), this is the object path to a snapshot that should be applied on failure of the virtual machine worker process
         * This is a read-only property, but it can be changed using the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class.
         */
        public string AdditionalRecoveryInformation
        {
            get
            {
                return (string)LateBoundObject[nameof(AdditionalRecoveryInformation)];
            }
            set
            {
                LateBoundObject[nameof(AdditionalRecoveryInformation)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool IsAllowFullSCSICommandSetNull
        {
            get
            {
                if (LateBoundObject[nameof(AllowFullSCSICommandSet)] == null)
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
         * Indicates whether SCSI commands from the guest operating system are passed to pass-through disks.
         * If TRUE, SCSI commands emitted by the guest operating system to pass-through disks are not filtered.
         * It is recommended that SCSI filtering remains enabled for production deployments.
         * This is a read-only property, but it can be changed using the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class.
         * Windows Server 2008:  The AllowFullSCSICommandSet property is not supported.
         */
        public bool AllowFullSCSICommandSet
        {
            get
            {
                if (LateBoundObject[nameof(AllowFullSCSICommandSet)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(AllowFullSCSICommandSet)];
            }
            set
            {
                LateBoundObject[nameof(AllowFullSCSICommandSet)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool IsAllowReducedFcRedundancyNull
        {
            get
            {
                if (LateBoundObject[nameof(AllowReducedFcRedundancy)] == null)
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
         * Indicates whether live migration of a virtual machine that is configured with a virtual FC adapter is allowed to a destination machine,
         * without doing any checks for the existence of paths to the storage devices on the destination.
         * The default value of this property is FALSE.
         * If set to TRUE, the VM can be live migrated to a target machine which may have no or reduced paths to the target FC devices.
         * The guest operating system may lose connectivity to storage and may behave in an unpredictable manner.
         * This property should be cleared after a live migration.
         */
        public bool AllowReducedFcRedundancy
        {
            get
            {
                if (LateBoundObject[nameof(AllowReducedFcRedundancy)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(AllowReducedFcRedundancy)];
            }
            set
            {
                LateBoundObject[nameof(AllowReducedFcRedundancy)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The architecture of this system.
         */
        public string Architecture => (string)LateBoundObject[nameof(Architecture)];

        public bool IsAutomaticCriticalErrorActionNull
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticCriticalErrorAction)] == null)
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
         * Identifies the action to be taken on VM, when a critical error happens, like storage disconnect.
         * The value of 1, causes the VM to be paused and automatically resumed when the critical error condition is resolved.
         * This behavior can be overridden by setting the value to 0 upon which no specific action will be taken for critical error conditions.
         */
        public AutomaticCriticalErrorActionValues AutomaticCriticalErrorAction
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticCriticalErrorAction)] == null)
                {
                    return (AutomaticCriticalErrorActionValues)Convert.ToInt32(2);
                }
                return (AutomaticCriticalErrorActionValues)Convert.ToInt32(LateBoundObject[nameof(AutomaticCriticalErrorAction)]);
            }
            set
            {
                if (AutomaticCriticalErrorActionValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(AutomaticCriticalErrorAction)] = null;
                }
                else
                {
                    LateBoundObject[nameof(AutomaticCriticalErrorAction)] = value;
                }
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool IsAutomaticCriticalErrorActionTimeoutNull
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticCriticalErrorActionTimeout)] == null)
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
         * Identifies the maximum duration for which the AutomaticCriticalErrorAction will be performed to resolve the critical error. 
         * This is applicable only when the value of the AutomaticCriticalErrorAction property is not 0 (None). 
         * Once the timeout expires, the VM will be powered off.The value will be rounded up to the nearest minute. 
         * A value of 0 implies that the VM should be powered off immediately when it encounters a critical error condition. 
         */
        public TimeSpan AutomaticCriticalErrorActionTimeout
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticCriticalErrorActionTimeout)] != null)
                {
                    return ToTimeSpan((string)LateBoundObject[nameof(AutomaticCriticalErrorActionTimeout)]);
                }
                else
                {
                    return new TimeSpan(0, 0, 0, 0, 0);
                }
            }
            set
            {
                LateBoundObject[nameof(AutomaticCriticalErrorActionTimeout)] = ToDmtfTimeInterval(value);
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool IsAutomaticRecoveryActionNull
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticRecoveryAction)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ushort AutomaticRecoveryAction
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticRecoveryAction)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(AutomaticRecoveryAction)];
            }
        }

        public bool IsAutomaticShutdownActionNull
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticShutdownAction)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ushort AutomaticShutdownAction
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticShutdownAction)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(AutomaticShutdownAction)];
            }
        }

        public bool IsAutomaticSnapshotsEnabledNull
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticSnapshotsEnabled)] == null)
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
         * Indicates whether this virtual machine should have automatic snapshots enabled.
         */
        public bool AutomaticSnapshotsEnabled
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticSnapshotsEnabled)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(AutomaticSnapshotsEnabled)];
            }
            set
            {
                LateBoundObject[nameof(AutomaticSnapshotsEnabled)] = value;
                if ((isEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool IsAutomaticStartupActionNull
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticStartupAction)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ushort AutomaticStartupAction
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticStartupAction)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(AutomaticStartupAction)];
            }
        }

        public bool IsAutomaticStartupActionDelayNull
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticStartupActionDelay)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public DateTime AutomaticStartupActionDelay
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticStartupActionDelay)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(AutomaticStartupActionDelay)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public bool IsAutomaticStartupActionSequenceNumberNull
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticStartupActionSequenceNumber)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ushort AutomaticStartupActionSequenceNumber
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticStartupActionSequenceNumber)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(AutomaticStartupActionSequenceNumber)];
            }
        }

        /*
         * The serial number of the base board for the virtual computer system.
         * This is a read-only property, but it can be changed using the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class.
         */
        public string BaseBoardSerialNumber
        {
            get
            {
                return (string)LateBoundObject[nameof(BaseBoardSerialNumber)];
            }
            set
            {
                LateBoundObject[nameof(BaseBoardSerialNumber)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The globally-unique identifier for the BIOS of the virtual computer system.
         * This is a read-only property, but it can be changed using the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class.
         */
        public string BIOSGUID
        {
            get
            {
                return (string)LateBoundObject[nameof(BIOSGUID)];
            }
            set
            {
                LateBoundObject[nameof(BIOSGUID)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool IsBIOSNumLockNull
        {
            get
            {
                if (LateBoundObject[nameof(BIOSNumLock)] == null)
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
         * This property is set to TRUE if the num lock key is set to on by the BIOS, FALSE if the num lock key is set to off by the BIOS.
         * This is a read-only property, but it can be changed using the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class.
         */
        public bool BIOSNumLock
        {
            get
            {
                if (LateBoundObject[nameof(BIOSNumLock)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(BIOSNumLock)];
            }
            set
            {
                LateBoundObject[nameof(BIOSNumLock)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The serial number of the BIOS for the virtual computer system.
         * This is a read-only property, but it can be changed using the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class.
         */
        public string BIOSSerialNumber
        {
            get
            {
                return (string)LateBoundObject[nameof(BIOSSerialNumber)];
            }
            set
            {
                LateBoundObject[nameof(BIOSSerialNumber)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The boot order set within the BIOS of the virtual computer system. 
         * This property is an array of values, max length 4, where each value indicates a device to boot from. 
         * The virtual computer system will first attempt to boot from the device indicated by the first value within the array. 
         * If that device does not contain a boot sector, the virtual computer system will attempt to boot from the next device specified by the BootOrder property and so on. 
         * If no device specified within the BootOrder contains a boot sector the virtual computer system will fail to boot.
         * The default value for a virtual computer system is [0, 1, 2, 3, 4].
         * 
         * Value definitions:
         * 0 (Floppy): The virtual computer system will attempt to boot from the floppy disk within the floppy drive.
         * 1 (CD-ROM): The virtual computer system will attempt to boot from the first CD or DVD disk found with a boot sector.
         * 2 (IDE Hard Drive): The virtual computer system will attempt to boot from the first hard drive found attached to an IDE controller with a boot sector.
         * 3 (PXE Boot): The virtual computer system will attempt to PXE boot from the network.
         * 4 (SCSI Hard Drive): The virtual computer system will attempt to boot from the first hard drive found attached to a SCSI controller with a boot sector.
         * 5-65535: Reserved
         * 
         * This is a read-only property, but it can be changed using the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class.
         */
        public ushort[] BootOrder
        {
            get
            {
                return (ushort[])LateBoundObject[nameof(BootOrder)];
            }
            set
            {
                LateBoundObject[nameof(BootOrder)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Contains references to the boot source entries of this virtual computer system.
         * This property is an array of strings, where each string is a full path to a Msvm_BootSourceSettingData instance
         * indicating a device, file, or other boot source to boot from.
         */
        public string[] BootSourceOrder
        {
            get
            {
                return (string[])LateBoundObject[nameof(BootSourceOrder)];
            }
            set
            {
                LateBoundObject[nameof(BootSourceOrder)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        /*
         * The asset tag of the chassis for the virtual computer system.
         * This is a read-only property, but it can be changed using the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class.
         */
        public string ChassisAssetTag
        {
            get
            {
                return (string)LateBoundObject[nameof(ChassisAssetTag)];
            }
            set
            {
                LateBoundObject[nameof(ChassisAssetTag)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The serial number of the chassis for the virtual computer system.
         * This is a read-only property, but it can be changed using the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class.
         */
        public string ChassisSerialNumber
        {
            get
            {
                return (string)LateBoundObject[nameof(ChassisSerialNumber)];
            }
            set
            {
                LateBoundObject[nameof(ChassisSerialNumber)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string ConfigurationDataRoot => (string)LateBoundObject[nameof(ConfigurationDataRoot)];

        public string ConfigurationFile => (string)LateBoundObject[nameof(ConfigurationFile)];

        public string ConfigurationID => (string)LateBoundObject[nameof(ConfigurationID)];

        public bool IsConsoleModeNull
        {
            get
            {
                if (LateBoundObject[nameof(ConsoleMode)] == null)
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
         * Identifies the Console Mode for the VM.
         */
        public ConsoleModeValues ConsoleMode
        {
            get
            {
                if (LateBoundObject[nameof(ConsoleMode)] == null)
                {
                    return (ConsoleModeValues)Convert.ToInt32(4);
                }
                return (ConsoleModeValues)Convert.ToInt32(LateBoundObject[nameof(ConsoleMode)]);
            }
            set
            {
                if (ConsoleModeValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(ConsoleMode)] = null;
                }
                else
                {
                    LateBoundObject[nameof(ConsoleMode)] = value;
                }
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool IsCreationTimeNull
        {
            get
            {
                if (LateBoundObject[nameof(CreationTime)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public DateTime CreationTime
        {
            get
            {
                if (LateBoundObject[nameof(CreationTime)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(CreationTime)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public bool IsDebugChannelIdNull
        {
            get
            {
                if (LateBoundObject[nameof(DebugChannelId)] == null)
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
         * The channel identifier used to debug the virtual system using the VUD unified debugger.
         */
        public uint DebugChannelId
        {
            get
            {
                if (LateBoundObject[nameof(DebugChannelId)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(DebugChannelId)];
            }
            set
            {
                LateBoundObject[nameof(DebugChannelId)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool IsDebugPortNull
        {
            get
            {
                if (LateBoundObject[nameof(DebugPort)] == null)
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
         * The tcpip port used to debug the virtual system using synthetic debugging.
         */
        public uint DebugPort
        {
            get
            {
                if (LateBoundObject[nameof(DebugPort)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(DebugPort)];
            }
            set
            {
                LateBoundObject[nameof(DebugPort)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool IsDebugPortEnabledNull
        {
            get
            {
                if (LateBoundObject[nameof(DebugPortEnabled)] == null)
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
         * Whether the virtual system is using synthetic debugging.
         */
        public DebugPortEnabledValues DebugPortEnabled
        {
            get
            {
                if (LateBoundObject[nameof(DebugPortEnabled)] == null)
                {
                    return (DebugPortEnabledValues)Convert.ToInt32(3);
                }
                return (DebugPortEnabledValues)Convert.ToInt32(LateBoundObject[nameof(DebugPortEnabled)]);
            }
            set
            {
                if (DebugPortEnabledValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(DebugPortEnabled)] = null;
                }
                else
                {
                    LateBoundObject[nameof(DebugPortEnabled)] = value;
                }
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public bool IsEnableHibernationNull
        {
            get
            {
                if (LateBoundObject[nameof(EnableHibernation)] == null)
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
         * This property is set to TRUE if hibernate (S4) is enabled by the BIOS, FALSE if hibernate is disabled.
         * This is a read-only property, but it can be changed using the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class.
         */
        public bool EnableHibernation
        {
            get
            {
                if (LateBoundObject[nameof(EnableHibernation)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(EnableHibernation)];
            }
            set
            {
                LateBoundObject[nameof(EnableHibernation)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool IsEnhancedSessionTransportTypeNull
        {
            get
            {
                if (LateBoundObject[nameof(EnhancedSessionTransportType)] == null)
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
         * Indicates the transport type to use when connecting to an enhanced session.
         */
        public EnhancedSessionTransportTypeValues EnhancedSessionTransportType
        {
            get
            {
                if (LateBoundObject[nameof(EnhancedSessionTransportType)] == null)
                {
                    return (EnhancedSessionTransportTypeValues)Convert.ToInt32(2);
                }
                return (EnhancedSessionTransportTypeValues)Convert.ToInt32(LateBoundObject[nameof(EnhancedSessionTransportType)]);
            }
            set
            {
                if (EnhancedSessionTransportTypeValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(EnhancedSessionTransportType)] = null;
                }
                else
                {
                    LateBoundObject[nameof(EnhancedSessionTransportType)] = value;
                }
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool IsGuestControlledCacheTypesNull
        {
            get
            {
                if (LateBoundObject[nameof(GuestControlledCacheTypes)] == null)
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
         * Indicates whether the guest can control cache types.
         */
        public bool GuestControlledCacheTypes
        {
            get
            {
                if (LateBoundObject[nameof(GuestControlledCacheTypes)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(GuestControlledCacheTypes)];
            }
            set
            {
                LateBoundObject[nameof(GuestControlledCacheTypes)] = value;
                if ((isEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Filepath of a directory where information about the guest runtime state is stored
         */
        public string GuestStateDataRoot => (string)LateBoundObject[nameof(GuestStateDataRoot)];

        /*
         * Filepath of a file where information about the guest runtime state is stored.
         * A relative path appends to the value of the GuestStateDataRoot property.
         */
        public string GuestStateFile => (string)LateBoundObject[nameof(GuestStateFile)];

        public bool IsHighMmioGapBaseNull
        {
            get
            {
                if (LateBoundObject[nameof(HighMmioGapBase)] == null)
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
         * The base address of the High (above 4GB) Memory-Mapped IO Gap in MB.
         */
        public ulong HighMmioGapBase
        {
            get
            {
                if (LateBoundObject[nameof(HighMmioGapBase)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(HighMmioGapBase)];
            }
            set
            {
                LateBoundObject[nameof(HighMmioGapBase)] = value;
                if ((isEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool IsHighMmioGapSizeNull
        {
            get
            {
                if (LateBoundObject[nameof(HighMmioGapSize)] == null)
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
         * The size of the High (above 4GB) Memory-Mapped IO Gap in MB.
         */
        public ulong HighMmioGapSize
        {
            get
            {
                if (LateBoundObject[nameof(HighMmioGapSize)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(HighMmioGapSize)];
            }
            set
            {
                LateBoundObject[nameof(HighMmioGapSize)] = value;
                if ((isEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool IsIncrementalBackupEnabledNull
        {
            get
            {
                if (LateBoundObject[nameof(IncrementalBackupEnabled)] == null)
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
         * Indicates whether the Hyper-V VSS writer supports taking incremental backup of this Virtual machine.
         * This is a read-write property.
         */
        public bool IncrementalBackupEnabled
        {
            get
            {
                if (LateBoundObject[nameof(IncrementalBackupEnabled)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IncrementalBackupEnabled)];
            }
            set
            {
                LateBoundObject[nameof(IncrementalBackupEnabled)] = value;
                if ((isEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        public bool IsIsAutomaticSnapshotNull
        {
            get
            {
                if (LateBoundObject[nameof(IsAutomaticSnapshot)] == null)
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
         * Indicates whether this is a snapshot created automatically for the user.
         */
        public bool IsAutomaticSnapshot
        {
            get
            {
                if (LateBoundObject[nameof(IsAutomaticSnapshot)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsAutomaticSnapshot)];
            }
        }

        public bool IsIsSavedNull
        {
            get
            {
                if (LateBoundObject[nameof(IsSaved)] == null)
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
         * This property is set to TRUE if the configuration has a reference to a saved state file, FALSE if not.
         * Note that this does not indicate the presence of such a file, only that the configuration specifies one.
         * This is a read-only property, it cannot be changed.
         */
        public bool IsSaved
        {
            get
            {
                if (LateBoundObject[nameof(IsSaved)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsSaved)];
            }
        }

        public bool IsLockOnDisconnectNull
        {
            get
            {
                if (LateBoundObject[nameof(LockOnDisconnect)] == null)
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
         * Lock the console when disconnecting from vmconnect.
         */
        public bool LockOnDisconnect
        {
            get
            {
                if (LateBoundObject[nameof(LockOnDisconnect)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(LockOnDisconnect)];
            }
            set
            {
                LateBoundObject[nameof(LockOnDisconnect)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string LogDataRoot => (string)LateBoundObject[nameof(LogDataRoot)];

        public bool IsLowMmioGapSizeNull
        {
            get
            {
                if (LateBoundObject[nameof(LowMmioGapSize)] == null)
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
         * The size of the Low (below 4GB) Memory-Mapped IO Gap in MB.
         */
        public ulong LowMmioGapSize
        {
            get
            {
                if (LateBoundObject[nameof(LowMmioGapSize)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(LowMmioGapSize)];
            }
            set
            {
                LateBoundObject[nameof(LowMmioGapSize)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool IsNetworkBootPreferredProtocolNull
        {
            get
            {
                if (LateBoundObject[nameof(NetworkBootPreferredProtocol)] == null)
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
         * Identifies the IP protocol used for network boot.
         */
        public NetworkBootPreferredProtocolValues NetworkBootPreferredProtocol
        {
            get
            {
                if (LateBoundObject[nameof(NetworkBootPreferredProtocol)] == null)
                {
                    return (NetworkBootPreferredProtocolValues)Convert.ToInt32(0);
                }
                return (NetworkBootPreferredProtocolValues)Convert.ToInt32(LateBoundObject[nameof(NetworkBootPreferredProtocol)]);
            }
            set
            {
                if (NetworkBootPreferredProtocolValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(NetworkBootPreferredProtocol)] = null;
                }
                else
                {
                    LateBoundObject[nameof(NetworkBootPreferredProtocol)] = value;
                }
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string[] Notes => (string[])LateBoundObject[nameof(Notes)];

        /*
         * The object path for the snapshot Msvm_VirtualSystemSettingData from which this object is based.
         * This property will be NULL if this object is not based off a snapshot.
         */
        public string Parent => (string)LateBoundObject[nameof(Parent)];

        public bool IsPauseAfterBootFailureNull
        {
            get
            {
                if (LateBoundObject[nameof(PauseAfterBootFailure)] == null)
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
         * Pause after boot failure setting for the VM.
         */
        public bool PauseAfterBootFailure
        {
            get
            {
                if (LateBoundObject[nameof(PauseAfterBootFailure)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(PauseAfterBootFailure)];
            }
            set
            {
                LateBoundObject[nameof(PauseAfterBootFailure)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string RecoveryFile => (string)LateBoundObject[nameof(RecoveryFile)];

        public bool IsSecureBootEnabledNull
        {
            get
            {
                if (LateBoundObject[nameof(SecureBootEnabled)] == null)
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
         * Secure boot enabled setting for the VM.
         */
        public bool SecureBootEnabled
        {
            get
            {
                if (LateBoundObject[nameof(SecureBootEnabled)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(SecureBootEnabled)];
            }
            set
            {
                LateBoundObject[nameof(SecureBootEnabled)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The globally-unique identifier of the template of intial values of UEFI Secure Boot related variables.
         * This is a read-only property, but it can be changed using the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class.
         */
        public string SecureBootTemplateId
        {
            get
            {
                return (string)LateBoundObject[nameof(SecureBootTemplateId)];
            }
            set
            {
                LateBoundObject[nameof(SecureBootTemplateId)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string SnapshotDataRoot => (string)LateBoundObject[nameof(SnapshotDataRoot)];

        public string SuspendDataRoot => (string)LateBoundObject[nameof(SuspendDataRoot)];

        public string SwapFileDataRoot => (string)LateBoundObject[nameof(SwapFileDataRoot)];

        public bool IsTurnOffOnGuestRestartNull
        {
            get
            {
                if (LateBoundObject[nameof(TurnOffOnGuestRestart)] == null)
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
         * Turn off the VM instead of restarting the VM if the guest operating system initiated a restart.
         */
        public bool TurnOffOnGuestRestart
        {
            get
            {
                if (LateBoundObject[nameof(TurnOffOnGuestRestart)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(TurnOffOnGuestRestart)];
            }
            set
            {
                LateBoundObject[nameof(TurnOffOnGuestRestart)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public bool IsUserSnapshotTypeNull
        {
            get
            {
                if (LateBoundObject[nameof(UserSnapshotType)] == null)
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
         * Indicates the user-defined snapshot type. 
         * Disable: Disable the creation of any snapshot.
         * ProductionFallbackToTest: Data-consistent snapshot for use in the production environment. Performs a snapshot with application state when the ability to create data consistent snapshot is not available.
         * ProductionNoFallback: Data-consistent snapshot for use in the production environment.Does not create a snapshot with application state if it is not possible to create a data consistent snapshot.
         * Test: Snapshot that contains memory and device information for test and development purpose. 
         */
        public UserSnapshotTypeValues UserSnapshotType
        {
            get
            {
                if (LateBoundObject[nameof(UserSnapshotType)] == null)
                {
                    return (UserSnapshotTypeValues)Convert.ToInt32(0);
                }
                return (UserSnapshotTypeValues)Convert.ToInt32(LateBoundObject[nameof(UserSnapshotType)]);
            }
            set
            {
                if (UserSnapshotTypeValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(UserSnapshotType)] = null;
                }
                else
                {
                    LateBoundObject[nameof(UserSnapshotType)] = value;
                }
                if ((isEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The version of the virtual system in a format of "major.minor", for example "2.0"
         * Windows Server 2008:  The Version property is not supported.
         */
        public string Version => (string)LateBoundObject[nameof(Version)];

        public bool IsVirtualNumaEnabledNull
        {
            get
            {
                if (LateBoundObject[nameof(VirtualNumaEnabled)] == null)
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
         * Indicates whether virtual non-uniform memory access (NUMA) nodes are projected into the virtual machine. 
         * If FALSE, the virtual machine will have a single node.
         * If TRUE, the number of virtual NUMA nodes projected into the virtual machine is determined
         * from the values of the Msvm_ProcessorSettingData.MaxProcessorsPerNumaNode and Msvm_MemorySettingData.MaxMemoryBlocksPerNumaNode properties.
         */
        public bool VirtualNumaEnabled
        {
            get
            {
                if (LateBoundObject[nameof(VirtualNumaEnabled)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(VirtualNumaEnabled)];
            }
            set
            {
                LateBoundObject[nameof(VirtualNumaEnabled)] = value;
                if ((isEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The name of the CIM_ComputerSystem to which this setting data belongs.
         */
        public string VirtualSystemIdentifier => (string)LateBoundObject[nameof(VirtualSystemIdentifier)];

        /*
         * The subtype of the virtual system.
         */
        public string VirtualSystemSubType => (string)LateBoundObject[nameof(VirtualSystemSubType)];

        public string VirtualSystemType => (string)LateBoundObject[nameof(VirtualSystemType)];

        private bool CheckIfProperClass(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions OptionsParam)
        {
            if ((path != null) && (string.Compare(path.ClassName, ManagementClassName, true, CultureInfo.InvariantCulture) == 0))
            {
                return true;
            }
            else
            {
                using (ManagementObject theObj = new ManagementObject(mgmtScope, path, OptionsParam))
                {
                    return CheckIfProperClass(theObj);
                }
            }
        }
        
        private bool CheckIfProperClass(ManagementBaseObject theObj)
        {
            if ((theObj != null) && (string.Compare((string)theObj["__CLASS"], ManagementClassName, true, CultureInfo.InvariantCulture) == 0))
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

        private void ResetAdditionalRecoveryInformation()
        {
            LateBoundObject[nameof(AdditionalRecoveryInformation)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeAllowFullSCSICommandSet()
        {
            if (IsAllowFullSCSICommandSetNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetAllowFullSCSICommandSet()
        {
            LateBoundObject[nameof(AllowFullSCSICommandSet)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeAllowReducedFcRedundancy()
        {
            if (IsAllowReducedFcRedundancyNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetAllowReducedFcRedundancy()
        {
            LateBoundObject[nameof(AllowReducedFcRedundancy)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeAutomaticCriticalErrorAction()
        {
            if (IsAutomaticCriticalErrorActionNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetAutomaticCriticalErrorAction()
        {
            LateBoundObject[nameof(AutomaticCriticalErrorAction)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Converts a given time interval in DMTF format to System.TimeSpan object.
        static TimeSpan ToTimeSpan(string dmtfTimespan)
        {
            if (dmtfTimespan == null)
            {
                throw new ArgumentOutOfRangeException(dmtfTimespan);
            }
            if (dmtfTimespan.Length == 0)
            {
                throw new ArgumentOutOfRangeException(dmtfTimespan);
            }
            if (dmtfTimespan.Length != 25)
            {
                throw new ArgumentOutOfRangeException(dmtfTimespan);
            }
            if (dmtfTimespan.Substring(21, 4) != ":000")
            {
                throw new ArgumentOutOfRangeException(dmtfTimespan);
            }
            int days;
            int hours;
            int minutes;
            int seconds;
            long ticks;
            try
            {
                string tempString = string.Empty;
                tempString = dmtfTimespan.Substring(0, 8);
                days = int.Parse(tempString);
                tempString = dmtfTimespan.Substring(8, 2);
                hours = int.Parse(tempString);
                tempString = dmtfTimespan.Substring(10, 2);
                minutes = int.Parse(tempString);
                tempString = dmtfTimespan.Substring(12, 2);
                seconds = int.Parse(tempString);
                tempString = dmtfTimespan.Substring(15, 6);
                ticks = long.Parse(tempString) * (TimeSpan.TicksPerMillisecond / 1000);
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException(null, e.Message);
            }
            TimeSpan timespan = new TimeSpan(days, hours, minutes, seconds, 0);
            TimeSpan tsTemp = TimeSpan.FromTicks(ticks);
            timespan = timespan.Add(tsTemp);
            return timespan;
        }

        // Converts a given System.TimeSpan object to DMTF Time interval format.
        static string ToDmtfTimeInterval(TimeSpan timespan)
        {
            string dmtftimespan = timespan.Days.ToString().PadLeft(8, '0');
            TimeSpan maxTimeSpan = TimeSpan.MaxValue;
            if (timespan.Days > maxTimeSpan.Days)
            {
                throw new ArgumentOutOfRangeException(timespan.ToString());
            }
            TimeSpan minTimeSpan = TimeSpan.MinValue;
            if (timespan.Days < minTimeSpan.Days)
            {
                throw new ArgumentOutOfRangeException(timespan.ToString());
            }
            dmtftimespan = string.Concat(dmtftimespan, timespan.Hours.ToString().PadLeft(2, '0'));
            dmtftimespan = string.Concat(dmtftimespan, timespan.Minutes.ToString().PadLeft(2, '0'));
            dmtftimespan = string.Concat(dmtftimespan, timespan.Seconds.ToString().PadLeft(2, '0'));
            dmtftimespan = string.Concat(dmtftimespan, ".");
            TimeSpan tsTemp = new TimeSpan(timespan.Days, timespan.Hours, timespan.Minutes, timespan.Seconds, 0);
            long microsec = (timespan.Ticks - tsTemp.Ticks)
                        * 1000
                        / TimeSpan.TicksPerMillisecond;
            string strMicroSec = microsec.ToString();
            if (strMicroSec.Length > 6)
            {
                strMicroSec = strMicroSec.Substring(0, 6);
            }
            dmtftimespan = string.Concat(dmtftimespan, strMicroSec.PadLeft(6, '0'));
            dmtftimespan = string.Concat(dmtftimespan, ":000");
            return dmtftimespan;
        }

        private bool ShouldSerializeAutomaticCriticalErrorActionTimeout()
        {
            if (IsAutomaticCriticalErrorActionTimeoutNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetAutomaticCriticalErrorActionTimeout()
        {
            LateBoundObject[nameof(AutomaticCriticalErrorActionTimeout)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeAutomaticRecoveryAction()
        {
            if (IsAutomaticRecoveryActionNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeAutomaticShutdownAction()
        {
            if (IsAutomaticShutdownActionNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeAutomaticSnapshotsEnabled()
        {
            if (IsAutomaticSnapshotsEnabledNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetAutomaticSnapshotsEnabled()
        {
            LateBoundObject[nameof(AutomaticSnapshotsEnabled)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeAutomaticStartupAction()
        {
            if (IsAutomaticStartupActionNull == false)
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

        private bool ShouldSerializeAutomaticStartupActionDelay()
        {
            if (IsAutomaticStartupActionDelayNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeAutomaticStartupActionSequenceNumber()
        {
            if (IsAutomaticStartupActionSequenceNumberNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetBaseBoardSerialNumber()
        {
            LateBoundObject[nameof(BaseBoardSerialNumber)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetBIOSGUID()
        {
            LateBoundObject[nameof(BIOSGUID)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeBIOSNumLock()
        {
            if (IsBIOSNumLockNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetBIOSNumLock()
        {
            LateBoundObject[nameof(BIOSNumLock)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetBIOSSerialNumber()
        {
            LateBoundObject[nameof(BIOSSerialNumber)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetBootOrder()
        {
            LateBoundObject[nameof(BootOrder)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetBootSourceOrder()
        {
            LateBoundObject[nameof(BootSourceOrder)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetChassisAssetTag()
        {
            LateBoundObject[nameof(ChassisAssetTag)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetChassisSerialNumber()
        {
            LateBoundObject[nameof(ChassisSerialNumber)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeConsoleMode()
        {
            if (IsConsoleModeNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetConsoleMode()
        {
            LateBoundObject[nameof(ConsoleMode)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeCreationTime()
        {
            if (IsCreationTimeNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeDebugChannelId()
        {
            if (IsDebugChannelIdNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetDebugChannelId()
        {
            LateBoundObject[nameof(DebugChannelId)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeDebugPort()
        {
            if (IsDebugPortNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetDebugPort()
        {
            LateBoundObject[nameof(DebugPort)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeDebugPortEnabled()
        {
            if (IsDebugPortEnabledNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetDebugPortEnabled()
        {
            LateBoundObject[nameof(DebugPortEnabled)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeEnableHibernation()
        {
            if (IsEnableHibernationNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetEnableHibernation()
        {
            LateBoundObject[nameof(EnableHibernation)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeEnhancedSessionTransportType()
        {
            if (IsEnhancedSessionTransportTypeNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetEnhancedSessionTransportType()
        {
            LateBoundObject[nameof(EnhancedSessionTransportType)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeGuestControlledCacheTypes()
        {
            if (IsGuestControlledCacheTypesNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetGuestControlledCacheTypes()
        {
            LateBoundObject[nameof(GuestControlledCacheTypes)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeHighMmioGapBase()
        {
            if (IsHighMmioGapBaseNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetHighMmioGapBase()
        {
            LateBoundObject[nameof(HighMmioGapBase)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeHighMmioGapSize()
        {
            if (IsHighMmioGapSizeNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetHighMmioGapSize()
        {
            LateBoundObject[nameof(HighMmioGapSize)] = null;
            if ((isEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeIncrementalBackupEnabled()
        {
            if (IsIncrementalBackupEnabledNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetIncrementalBackupEnabled()
        {
            LateBoundObject[nameof(IncrementalBackupEnabled)] = null;
            if ((isEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeIsAutomaticSnapshot()
        {
            if (IsIsAutomaticSnapshotNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeIsSaved()
        {
            if (IsIsSavedNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeLockOnDisconnect()
        {
            if (IsLockOnDisconnectNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetLockOnDisconnect()
        {
            LateBoundObject[nameof(LockOnDisconnect)] = null;
            if ((isEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeLowMmioGapSize()
        {
            if (IsLowMmioGapSizeNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetLowMmioGapSize()
        {
            LateBoundObject[nameof(LowMmioGapSize)] = null;
            if ((isEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeNetworkBootPreferredProtocol()
        {
            if (IsNetworkBootPreferredProtocolNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetNetworkBootPreferredProtocol()
        {
            LateBoundObject[nameof(NetworkBootPreferredProtocol)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializePauseAfterBootFailure()
        {
            if (IsPauseAfterBootFailureNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetPauseAfterBootFailure()
        {
            LateBoundObject[nameof(PauseAfterBootFailure)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeSecureBootEnabled()
        {
            if (IsSecureBootEnabledNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetSecureBootEnabled()
        {
            LateBoundObject[nameof(SecureBootEnabled)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetSecureBootTemplateId()
        {
            LateBoundObject[nameof(SecureBootTemplateId)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeTurnOffOnGuestRestart()
        {
            if (IsTurnOffOnGuestRestartNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetTurnOffOnGuestRestart()
        {
            LateBoundObject[nameof(TurnOffOnGuestRestart)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeUserSnapshotType()
        {
            if (IsUserSnapshotTypeNull == false)
            {
                return true;
            }
            return false;
        }
        private void ResetUserSnapshotType()
        {
            LateBoundObject[nameof(UserSnapshotType)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeVirtualNumaEnabled()
        {
            if (IsVirtualNumaEnabledNull == false)
            {
                return true;
            }
            return false;
        }

        private void ResetVirtualNumaEnabled()
        {
            LateBoundObject[nameof(VirtualNumaEnabled)] = null;
            if ((isEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
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

        private static string ConstructPath(string keyInstanceID)
        {
            string strPath = "root\\virtualization\\v2:Msvm_VirtualSystemSettingData";
            strPath = string.Concat(strPath, string.Concat(".InstanceID=", string.Concat("\"", string.Concat(keyInstanceID, "\""))));
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
        public static MsvmCollection<VirtualSystemSettingData> GetInstances() => GetInstances(null, null, null);

        public static MsvmCollection<VirtualSystemSettingData> GetInstances(string condition) => GetInstances(null, condition, null);

        public static MsvmCollection<VirtualSystemSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties);

        public static MsvmCollection<VirtualSystemSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties);

        public static MsvmCollection<VirtualSystemSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions)
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
                ClassName = "Msvm_VirtualSystemSettingData",
                NamespacePath = "root\\virtualization\\v2"
            };
            using (ManagementClass clsObject = new ManagementClass(mgmtScope, pathObj, null))
            {
                if (enumOptions == null)
                {
                    enumOptions = new EnumerationOptions
                    {
                        EnsureLocatable = true
                    };
                }
                return new MsvmCollection<VirtualSystemSettingData>(clsObject.GetInstances(enumOptions));
            }
        }

        public static MsvmCollection<VirtualSystemSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null);

        public static MsvmCollection<VirtualSystemSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties);

        public static MsvmCollection<VirtualSystemSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties)
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
            using (ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery("Msvm_VirtualSystemSettingData", condition, selectedProperties)))
            {
                EnumerationOptions enumOptions = new EnumerationOptions
                {
                    EnsureLocatable = true
                };
                ObjectSearcher.Options = enumOptions;
                return new MsvmCollection<VirtualSystemSettingData>(ObjectSearcher.Get());
            }
        }

        public static VirtualSystemSettingData CreateInstance()
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
                return new VirtualSystemSettingData(tmpMgmtClass.CreateInstance());
            }
        }

        public void Delete() => PrivateLateBoundObject.Delete();

        public enum AutomaticCriticalErrorActionValues
        {
            None = 0,
            Pause_Resume = 1,
            NULL_ENUM_VALUE = 2,
        }

        public enum ConsoleModeValues
        {
            Default = 0,
            COM1 = 1,
            COM2 = 2,
            None = 3,
            NULL_ENUM_VALUE = 4,
        }

        public enum DebugPortEnabledValues
        {
            Val__Off = 0,
            On = 1,
            OnAutoAssigned = 2,
            NULL_ENUM_VALUE = 3,
        }

        public enum EnhancedSessionTransportTypeValues
        {
            VMBus_Pipe = 0,
            Hyper_V_Socket = 1,
            NULL_ENUM_VALUE = 2,
        }

        public enum NetworkBootPreferredProtocolValues
        {
            IPv4 = 4096,
            IPv6 = 4097,
            NULL_ENUM_VALUE = 0,
        }

        public enum UserSnapshotTypeValues
        {
            Disable = 2,
            ProductionFallbackToTest = 3,
            ProductionNoFallback = 4,
            Test = 5,
            NULL_ENUM_VALUE = 0,
        }

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

        ~VirtualSystemSettingData()
        {
            Dispose(false);
        }
    }
}
