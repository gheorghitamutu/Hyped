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
    // Time interval functions  ToTimeSpan and ToDmtfTimeInterval are added to the class to convert DMTF Time Interval to  System.TimeSpan and vice-versa.
    // Datetime conversion functions ToDateTime and ToDmtfDateTime are added to the class to convert DMTF datetime to System.DateTime and vice-versa.
    // An Early Bound class generated for the WMI class.Msvm_VirtualSystemSettingData
    public class VirtualSystemSettingData : Component
    {

        // Private property to hold the WMI namespace in which the class resides.
        private static string CreatedWmiNamespace = "root\\virtualization\\v2";

        // Private property to hold the name of WMI class which created this class.
        private static string CreatedClassName = "Msvm_VirtualSystemSettingData";

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
        public VirtualSystemSettingData()
        {
            InitializeObject(null, null, null);
        }

        public VirtualSystemSettingData(string keyInstanceID)
        {
            InitializeObject(null, new ManagementPath(ConstructPath(keyInstanceID)), null);
        }

        public VirtualSystemSettingData(ManagementScope mgmtScope, string keyInstanceID)
        {
            InitializeObject(mgmtScope, new ManagementPath(ConstructPath(keyInstanceID)), null);
        }

        public VirtualSystemSettingData(ManagementPath path, ObjectGetOptions getOptions)
        {
            InitializeObject(null, path, getOptions);
        }

        public VirtualSystemSettingData(ManagementScope mgmtScope, ManagementPath path)
        {
            InitializeObject(mgmtScope, path, null);
        }

        public VirtualSystemSettingData(ManagementPath path)
        {
            InitializeObject(null, path, null);
        }

        public VirtualSystemSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions)
        {
            InitializeObject(mgmtScope, path, getOptions);
        }

        public VirtualSystemSettingData(ManagementObject theObject)
        {
            Initialize();
            if ((CheckIfProperClass(theObject) == true))
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

        public VirtualSystemSettingData(ManagementBaseObject theObject)
        {
            Initialize();
            if ((CheckIfProperClass(theObject) == true))
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
                if ((curObj != null))
                {
                    if ((curObj.ClassPath != null))
                    {
                        strRet = ((string)(curObj["__CLASS"]));
                        if (((strRet == null)
                                    || (strRet == string.Empty)))
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
                if ((isEmbedded == false))
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
                if ((isEmbedded == false))
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
                if ((isEmbedded == false))
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
                if ((isEmbedded == false))
                {
                    if ((CheckIfProperClass(null, value, null) != true))
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
        [Description(@"Any additional information provided to the recovery action. The meaning of this property is defined by the action in AutomaticRecoveryAction. If AutomaticRecoveryAction is 0 (""None"") or 1 (""Restart""), this value is NULL. If AutomaticRecoveryAction is 2 (""Revert to Snapshot""), this is the object path to a snapshot that should be applied on failure of the virtual machine worker process.
This is a read-only property, but it can be changed using the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class.")]
        public string AdditionalRecoveryInformation
        {
            get
            {
                return ((string)(curObj[nameof(AdditionalRecoveryInformation)]));
            }
            set
            {
                curObj[nameof(AdditionalRecoveryInformation)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAllowFullSCSICommandSetNull
        {
            get
            {
                if ((curObj[nameof(AllowFullSCSICommandSet)] == null))
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
        [Description(@"Indicates whether SCSI commands from the guest operating system are passed to pass-through disks. If TRUE, SCSI commands emitted by the guest operating system to pass-through disks are not filtered.It is recommended that SCSI filtering remains enabled for production deployments.
This is a read-only property, but it can be changed using the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class.
Windows Server 2008:  The AllowFullSCSICommandSet property is not supported.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool AllowFullSCSICommandSet
        {
            get
            {
                if ((curObj[nameof(AllowFullSCSICommandSet)] == null))
                {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(AllowFullSCSICommandSet)]));
            }
            set
            {
                curObj[nameof(AllowFullSCSICommandSet)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAllowReducedFcRedundancyNull
        {
            get
            {
                if ((curObj[nameof(AllowReducedFcRedundancy)] == null))
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
        [Description(@"Indicates whether live migration of a virtual machine that is configured with a virtual FC adapter is allowed to a destination machine, without doing any checks for the existence of paths to the storage devices on the destination 
The default value of this property is FALSE. If set to TRUE, the VM can be live migrated to a target machine which may have no or reduced paths to the target FC devices. The guest operating system may lose connectivity to storage and may behave in an unpredictable manner. 
 This property should be cleared after a live migration")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool AllowReducedFcRedundancy
        {
            get
            {
                if ((curObj[nameof(AllowReducedFcRedundancy)] == null))
                {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(AllowReducedFcRedundancy)]));
            }
            set
            {
                curObj[nameof(AllowReducedFcRedundancy)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The architecture of this system.")]
        public string Architecture
        {
            get
            {
                return ((string)(curObj[nameof(Architecture)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAutomaticCriticalErrorActionNull
        {
            get
            {
                if ((curObj[nameof(AutomaticCriticalErrorAction)] == null))
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
        [Description(@"Identifies the action to be taken on VM, when a critical error happens, like storage disconnect.The value of 1, causes the VM to be paused and automatically resumed when the critical error condition is resolved. This behavior can be overridden by setting the value to 0 upon which no specific action will be taken for critical error conditions. ")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public AutomaticCriticalErrorActionValues AutomaticCriticalErrorAction
        {
            get
            {
                if ((curObj[nameof(AutomaticCriticalErrorAction)] == null))
                {
                    return ((AutomaticCriticalErrorActionValues)(Convert.ToInt32(2)));
                }
                return ((AutomaticCriticalErrorActionValues)(Convert.ToInt32(curObj[nameof(AutomaticCriticalErrorAction)])));
            }
            set
            {
                if ((AutomaticCriticalErrorActionValues.NULL_ENUM_VALUE == value))
                {
                    curObj[nameof(AutomaticCriticalErrorAction)] = null;
                }
                else
                {
                    curObj[nameof(AutomaticCriticalErrorAction)] = value;
                }
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAutomaticCriticalErrorActionTimeoutNull
        {
            get
            {
                if ((curObj[nameof(AutomaticCriticalErrorActionTimeout)] == null))
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
        [Description(@"Identifies the maximum duration for which the AutomaticCriticalErrorAction will be performed to resolve the critical error. This is applicable only when the value of the AutomaticCriticalErrorAction property is not 0 (None). Once the timeout expires, the VM will be powered off.The value will be rounded up to the nearest minute. A value of 0 implies that the VM should be powered off immediately when it encounters a critical error condition. ")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public TimeSpan AutomaticCriticalErrorActionTimeout
        {
            get
            {
                if ((curObj[nameof(AutomaticCriticalErrorActionTimeout)] != null))
                {
                    return ToTimeSpan(((string)(curObj[nameof(AutomaticCriticalErrorActionTimeout)])));
                }
                else
                {
                    return new TimeSpan(0, 0, 0, 0, 0);
                }
            }
            set
            {
                curObj[nameof(AutomaticCriticalErrorActionTimeout)] = ToDmtfTimeInterval(value);
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAutomaticRecoveryActionNull
        {
            get
            {
                if ((curObj[nameof(AutomaticRecoveryAction)] == null))
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
        public ushort AutomaticRecoveryAction
        {
            get
            {
                if ((curObj[nameof(AutomaticRecoveryAction)] == null))
                {
                    return Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(AutomaticRecoveryAction)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAutomaticShutdownActionNull
        {
            get
            {
                if ((curObj[nameof(AutomaticShutdownAction)] == null))
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
        public ushort AutomaticShutdownAction
        {
            get
            {
                if ((curObj[nameof(AutomaticShutdownAction)] == null))
                {
                    return Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(AutomaticShutdownAction)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAutomaticSnapshotsEnabledNull
        {
            get
            {
                if ((curObj[nameof(AutomaticSnapshotsEnabled)] == null))
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
        [Description("Indicates whether this virtual machine should have automatic snapshots enabled.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool AutomaticSnapshotsEnabled
        {
            get
            {
                if ((curObj[nameof(AutomaticSnapshotsEnabled)] == null))
                {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(AutomaticSnapshotsEnabled)]));
            }
            set
            {
                curObj[nameof(AutomaticSnapshotsEnabled)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAutomaticStartupActionNull
        {
            get
            {
                if ((curObj[nameof(AutomaticStartupAction)] == null))
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
        public ushort AutomaticStartupAction
        {
            get
            {
                if ((curObj[nameof(AutomaticStartupAction)] == null))
                {
                    return Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(AutomaticStartupAction)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAutomaticStartupActionDelayNull
        {
            get
            {
                if ((curObj[nameof(AutomaticStartupActionDelay)] == null))
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
        public DateTime AutomaticStartupActionDelay
        {
            get
            {
                if ((curObj[nameof(AutomaticStartupActionDelay)] != null))
                {
                    return ToDateTime(((string)(curObj[nameof(AutomaticStartupActionDelay)])));
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAutomaticStartupActionSequenceNumberNull
        {
            get
            {
                if ((curObj[nameof(AutomaticStartupActionSequenceNumber)] == null))
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
        public ushort AutomaticStartupActionSequenceNumber
        {
            get
            {
                if ((curObj[nameof(AutomaticStartupActionSequenceNumber)] == null))
                {
                    return Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(AutomaticStartupActionSequenceNumber)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The serial number of the base board for the virtual computer system.\nThis is a re" +
            "ad-only property, but it can be changed using the ModifyVirtualSystem method of " +
            "the Msvm_VirtualSystemManagementService class.")]
        public string BaseBoardSerialNumber
        {
            get
            {
                return ((string)(curObj[nameof(BaseBoardSerialNumber)]));
            }
            set
            {
                curObj[nameof(BaseBoardSerialNumber)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The globally-unique identifier for the BIOS of the virtual computer system.\nThis " +
            "is a read-only property, but it can be changed using the ModifyVirtualSystem met" +
            "hod of the Msvm_VirtualSystemManagementService class.")]
        public string BIOSGUID
        {
            get
            {
                return ((string)(curObj[nameof(BIOSGUID)]));
            }
            set
            {
                curObj[nameof(BIOSGUID)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsBIOSNumLockNull
        {
            get
            {
                if ((curObj[nameof(BIOSNumLock)] == null))
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
        [Description(@"This property is set to TRUE if the num lock key is set to on by the BIOS, FALSE if the num lock key is set to off by the BIOS.
This is a read-only property, but it can be changed using the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool BIOSNumLock
        {
            get
            {
                if ((curObj[nameof(BIOSNumLock)] == null))
                {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(BIOSNumLock)]));
            }
            set
            {
                curObj[nameof(BIOSNumLock)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The serial number of the BIOS for the virtual computer system.\nThis is a read-onl" +
            "y property, but it can be changed using the ModifyVirtualSystem method of the Ms" +
            "vm_VirtualSystemManagementService class.")]
        public string BIOSSerialNumber
        {
            get
            {
                return ((string)(curObj[nameof(BIOSSerialNumber)]));
            }
            set
            {
                curObj[nameof(BIOSSerialNumber)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"The boot order set within the BIOS of the virtual computer system. This property is an array of values, max length 4, where each value indicates a device to boot from. The virtual computer system will first attempt to boot from the device indicated by the first value within the array. If that device does not contain a boot sector, the virtual computer system will attempt to boot from the next device specified by the BootOrder property and so on. If no device specified within the BootOrder contains a boot sector the virtual computer system will fail to boot. The default value for a virtual computer system is [0, 1, 2, 3, 4].
Value definitions:
0 (Floppy): The virtual computer system will attempt to boot from the floppy disk within the floppy drive.
1 (CD-ROM): The virtual computer system will attempt to boot from the first CD or DVD disk found with a boot sector.
2 (IDE Hard Drive): The virtual computer system will attempt to boot from the first hard drive found attached to an IDE controller with a boot sector.
3 (PXE Boot): The virtual computer system will attempt to PXE boot from the network.
4 (SCSI Hard Drive): The virtual computer system will attempt to boot from the first hard drive found attached to a SCSI controller with a boot sector.
5-65535: Reserved
This is a read-only property, but it can be changed using the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class.")]
        public ushort[] BootOrder
        {
            get
            {
                return ((ushort[])(curObj[nameof(BootOrder)]));
            }
            set
            {
                curObj[nameof(BootOrder)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Contains references to the boot source entries of this virtual computer system. T" +
            "his property is an array of strings, where each string is a full path to a Msvm_" +
            "BootSourceSettingData instance indicating a device, file, or other boot source t" +
            "o boot from.")]
        public string[] BootSourceOrder
        {
            get
            {
                return ((string[])(curObj[nameof(BootSourceOrder)]));
            }
            set
            {
                curObj[nameof(BootSourceOrder)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Caption
        {
            get
            {
                return ((string)(curObj[nameof(Caption)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The asset tag of the chassis for the virtual computer system.\nThis is a read-only" +
            " property, but it can be changed using the ModifyVirtualSystem method of the Msv" +
            "m_VirtualSystemManagementService class.")]
        public string ChassisAssetTag
        {
            get
            {
                return ((string)(curObj[nameof(ChassisAssetTag)]));
            }
            set
            {
                curObj[nameof(ChassisAssetTag)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The serial number of the chassis for the virtual computer system.\nThis is a read-" +
            "only property, but it can be changed using the ModifyVirtualSystem method of the" +
            " Msvm_VirtualSystemManagementService class.")]
        public string ChassisSerialNumber
        {
            get
            {
                return ((string)(curObj[nameof(ChassisSerialNumber)]));
            }
            set
            {
                curObj[nameof(ChassisSerialNumber)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ConfigurationDataRoot
        {
            get
            {
                return ((string)(curObj[nameof(ConfigurationDataRoot)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ConfigurationFile
        {
            get
            {
                return ((string)(curObj[nameof(ConfigurationFile)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ConfigurationID
        {
            get
            {
                return ((string)(curObj[nameof(ConfigurationID)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsConsoleModeNull
        {
            get
            {
                if ((curObj[nameof(ConsoleMode)] == null))
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
        [Description("Identifies the Console Mode for the VM")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ConsoleModeValues ConsoleMode
        {
            get
            {
                if ((curObj[nameof(ConsoleMode)] == null))
                {
                    return ((ConsoleModeValues)(Convert.ToInt32(4)));
                }
                return ((ConsoleModeValues)(Convert.ToInt32(curObj[nameof(ConsoleMode)])));
            }
            set
            {
                if ((ConsoleModeValues.NULL_ENUM_VALUE == value))
                {
                    curObj[nameof(ConsoleMode)] = null;
                }
                else
                {
                    curObj[nameof(ConsoleMode)] = value;
                }
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsCreationTimeNull
        {
            get
            {
                if ((curObj[nameof(CreationTime)] == null))
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
        public DateTime CreationTime
        {
            get
            {
                if ((curObj[nameof(CreationTime)] != null))
                {
                    return ToDateTime(((string)(curObj[nameof(CreationTime)])));
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDebugChannelIdNull
        {
            get
            {
                if ((curObj[nameof(DebugChannelId)] == null))
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
        [Description("The channel identifier used to debug the virtual system using the VUD unified deb" +
            "ugger.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint DebugChannelId
        {
            get
            {
                if ((curObj[nameof(DebugChannelId)] == null))
                {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj[nameof(DebugChannelId)]));
            }
            set
            {
                curObj[nameof(DebugChannelId)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDebugPortNull
        {
            get
            {
                if ((curObj[nameof(DebugPort)] == null))
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
        [Description("The tcpip port used to debug the virtual system using synthetic debugging.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public uint DebugPort
        {
            get
            {
                if ((curObj[nameof(DebugPort)] == null))
                {
                    return Convert.ToUInt32(0);
                }
                return ((uint)(curObj[nameof(DebugPort)]));
            }
            set
            {
                curObj[nameof(DebugPort)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDebugPortEnabledNull
        {
            get
            {
                if ((curObj[nameof(DebugPortEnabled)] == null))
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
        [Description("Whether the virtual system is using synthetic debugging.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public DebugPortEnabledValues DebugPortEnabled
        {
            get
            {
                if ((curObj[nameof(DebugPortEnabled)] == null))
                {
                    return ((DebugPortEnabledValues)(Convert.ToInt32(3)));
                }
                return ((DebugPortEnabledValues)(Convert.ToInt32(curObj[nameof(DebugPortEnabled)])));
            }
            set
            {
                if ((DebugPortEnabledValues.NULL_ENUM_VALUE == value))
                {
                    curObj[nameof(DebugPortEnabled)] = null;
                }
                else
                {
                    curObj[nameof(DebugPortEnabled)] = value;
                }
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Description
        {
            get
            {
                return ((string)(curObj[nameof(Description)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ElementName
        {
            get
            {
                return ((string)(curObj[nameof(ElementName)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEnableHibernationNull
        {
            get
            {
                if ((curObj[nameof(EnableHibernation)] == null))
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
        [Description("This property is set to TRUE if hibernate (S4) is enabled by the BIOS, FALSE if h" +
            "ibernate is disabled.\nThis is a read-only property, but it can be changed using " +
            "the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class." +
            "")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool EnableHibernation
        {
            get
            {
                if ((curObj[nameof(EnableHibernation)] == null))
                {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(EnableHibernation)]));
            }
            set
            {
                curObj[nameof(EnableHibernation)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEnhancedSessionTransportTypeNull
        {
            get
            {
                if ((curObj[nameof(EnhancedSessionTransportType)] == null))
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
        [Description("Indicates the transport type to use when connecting to an enhanced session.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public EnhancedSessionTransportTypeValues EnhancedSessionTransportType
        {
            get
            {
                if ((curObj[nameof(EnhancedSessionTransportType)] == null))
                {
                    return ((EnhancedSessionTransportTypeValues)(Convert.ToInt32(2)));
                }
                return ((EnhancedSessionTransportTypeValues)(Convert.ToInt32(curObj[nameof(EnhancedSessionTransportType)])));
            }
            set
            {
                if ((EnhancedSessionTransportTypeValues.NULL_ENUM_VALUE == value))
                {
                    curObj[nameof(EnhancedSessionTransportType)] = null;
                }
                else
                {
                    curObj[nameof(EnhancedSessionTransportType)] = value;
                }
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsGuestControlledCacheTypesNull
        {
            get
            {
                if ((curObj[nameof(GuestControlledCacheTypes)] == null))
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
        [Description("Indicates whether the guest can control cache types.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool GuestControlledCacheTypes
        {
            get
            {
                if ((curObj[nameof(GuestControlledCacheTypes)] == null))
                {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(GuestControlledCacheTypes)]));
            }
            set
            {
                curObj[nameof(GuestControlledCacheTypes)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Filepath of a directory where information about the guest runtime state is stored" +
            ".")]
        public string GuestStateDataRoot
        {
            get
            {
                return ((string)(curObj[nameof(GuestStateDataRoot)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Filepath of a file where information about the guest runtime state is stored. A r" +
            "elative path appends to the value of the GuestStateDataRoot property.")]
        public string GuestStateFile
        {
            get
            {
                return ((string)(curObj[nameof(GuestStateFile)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsHighMmioGapBaseNull
        {
            get
            {
                if ((curObj[nameof(HighMmioGapBase)] == null))
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
        [Description("The base address of the High (above 4GB) Memory-Mapped IO Gap in MB\n")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong HighMmioGapBase
        {
            get
            {
                if ((curObj[nameof(HighMmioGapBase)] == null))
                {
                    return Convert.ToUInt64(0);
                }
                return ((ulong)(curObj[nameof(HighMmioGapBase)]));
            }
            set
            {
                curObj[nameof(HighMmioGapBase)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsHighMmioGapSizeNull
        {
            get
            {
                if ((curObj[nameof(HighMmioGapSize)] == null))
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
        [Description("The size of the High (above 4GB) Memory-Mapped IO Gap in MB\n")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong HighMmioGapSize
        {
            get
            {
                if ((curObj[nameof(HighMmioGapSize)] == null))
                {
                    return Convert.ToUInt64(0);
                }
                return ((ulong)(curObj[nameof(HighMmioGapSize)]));
            }
            set
            {
                curObj[nameof(HighMmioGapSize)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIncrementalBackupEnabledNull
        {
            get
            {
                if ((curObj[nameof(IncrementalBackupEnabled)] == null))
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
        [Description("Indicates whether the Hyper-V VSS writer supports taking incremental backup of th" +
            "is Virtual machine.\nThis is a read-write property.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool IncrementalBackupEnabled
        {
            get
            {
                if ((curObj[nameof(IncrementalBackupEnabled)] == null))
                {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(IncrementalBackupEnabled)]));
            }
            set
            {
                curObj[nameof(IncrementalBackupEnabled)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InstanceID
        {
            get
            {
                return ((string)(curObj[nameof(InstanceID)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIsAutomaticSnapshotNull
        {
            get
            {
                if ((curObj[nameof(IsAutomaticSnapshot)] == null))
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
        [Description("Indicates whether this is a snapshot created automatically for the user.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool IsAutomaticSnapshot
        {
            get
            {
                if ((curObj[nameof(IsAutomaticSnapshot)] == null))
                {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(IsAutomaticSnapshot)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsIsSavedNull
        {
            get
            {
                if ((curObj[nameof(IsSaved)] == null))
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
        [Description(@"This property is set to TRUE if the configuration has a reference to a saved state file, FALSE if not. Note that this does not indicate the presence of such a file, only that the configuration specifies one.
This is a read-only property, it cannot be changed.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool IsSaved
        {
            get
            {
                if ((curObj[nameof(IsSaved)] == null))
                {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(IsSaved)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLockOnDisconnectNull
        {
            get
            {
                if ((curObj[nameof(LockOnDisconnect)] == null))
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
        [Description("Lock the console when disconnecting from vmconnect.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool LockOnDisconnect
        {
            get
            {
                if ((curObj[nameof(LockOnDisconnect)] == null))
                {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(LockOnDisconnect)]));
            }
            set
            {
                curObj[nameof(LockOnDisconnect)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string LogDataRoot
        {
            get
            {
                return ((string)(curObj[nameof(LogDataRoot)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLowMmioGapSizeNull
        {
            get
            {
                if ((curObj[nameof(LowMmioGapSize)] == null))
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
        [Description("The size of the Low (below 4GB) Memory-Mapped IO Gap in MB\n")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ulong LowMmioGapSize
        {
            get
            {
                if ((curObj[nameof(LowMmioGapSize)] == null))
                {
                    return Convert.ToUInt64(0);
                }
                return ((ulong)(curObj[nameof(LowMmioGapSize)]));
            }
            set
            {
                curObj[nameof(LowMmioGapSize)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNetworkBootPreferredProtocolNull
        {
            get
            {
                if ((curObj[nameof(NetworkBootPreferredProtocol)] == null))
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
        [Description("Identifies the IP protocol used for network boot.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public NetworkBootPreferredProtocolValues NetworkBootPreferredProtocol
        {
            get
            {
                if ((curObj[nameof(NetworkBootPreferredProtocol)] == null))
                {
                    return ((NetworkBootPreferredProtocolValues)(Convert.ToInt32(0)));
                }
                return ((NetworkBootPreferredProtocolValues)(Convert.ToInt32(curObj[nameof(NetworkBootPreferredProtocol)])));
            }
            set
            {
                if ((NetworkBootPreferredProtocolValues.NULL_ENUM_VALUE == value))
                {
                    curObj[nameof(NetworkBootPreferredProtocol)] = null;
                }
                else
                {
                    curObj[nameof(NetworkBootPreferredProtocol)] = value;
                }
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] Notes
        {
            get
            {
                return ((string[])(curObj[nameof(Notes)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The object path for the snapshot Msvm_VirtualSystemSettingData from which this ob" +
            "ject is based. This property will be NULL if this object is not based off a snap" +
            "shot.")]
        public string Parent
        {
            get
            {
                return ((string)(curObj[nameof(Parent)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPauseAfterBootFailureNull
        {
            get
            {
                if ((curObj[nameof(PauseAfterBootFailure)] == null))
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
        [Description("Pause after boot failure setting for the VM")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool PauseAfterBootFailure
        {
            get
            {
                if ((curObj[nameof(PauseAfterBootFailure)] == null))
                {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(PauseAfterBootFailure)]));
            }
            set
            {
                curObj[nameof(PauseAfterBootFailure)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string RecoveryFile
        {
            get
            {
                return ((string)(curObj[nameof(RecoveryFile)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSecureBootEnabledNull
        {
            get
            {
                if ((curObj[nameof(SecureBootEnabled)] == null))
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
        [Description("Secure boot enabled setting for the VM")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool SecureBootEnabled
        {
            get
            {
                if ((curObj[nameof(SecureBootEnabled)] == null))
                {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(SecureBootEnabled)]));
            }
            set
            {
                curObj[nameof(SecureBootEnabled)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The globally-unique identifier of the template of intial values of UEFI Secure Bo" +
            "ot related variables.\nThis is a read-only property, but it can be changed using " +
            "the ModifyVirtualSystem method of the Msvm_VirtualSystemManagementService class." +
            "")]
        public string SecureBootTemplateId
        {
            get
            {
                return ((string)(curObj[nameof(SecureBootTemplateId)]));
            }
            set
            {
                curObj[nameof(SecureBootTemplateId)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SnapshotDataRoot
        {
            get
            {
                return ((string)(curObj[nameof(SnapshotDataRoot)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SuspendDataRoot
        {
            get
            {
                return ((string)(curObj[nameof(SuspendDataRoot)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SwapFileDataRoot
        {
            get
            {
                return ((string)(curObj[nameof(SwapFileDataRoot)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTurnOffOnGuestRestartNull
        {
            get
            {
                if ((curObj[nameof(TurnOffOnGuestRestart)] == null))
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
        [Description("Turn off the VM instead of restarting the VM if the guest operating system initia" +
            "ted a restart.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool TurnOffOnGuestRestart
        {
            get
            {
                if ((curObj[nameof(TurnOffOnGuestRestart)] == null))
                {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(TurnOffOnGuestRestart)]));
            }
            set
            {
                curObj[nameof(TurnOffOnGuestRestart)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsUserSnapshotTypeNull
        {
            get
            {
                if ((curObj[nameof(UserSnapshotType)] == null))
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
        [Description(@"Indicates the user-defined snapshot type. 
Disable: Disable the creation of any snapshot. 
ProductionFallbackToTest: Data-consistent snapshot for use in the production environment.Performs a snapshot with application state when the ability to create data consistent snapshot is not available. 
ProductionNoFallback: Data-consistent snapshot for use in the production environment.Does not create a snapshot with application state if it is not possible to create a data consistent snapshot. 
Test: Snapshot that contains memory and device information for test and development purpose. 
")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public UserSnapshotTypeValues UserSnapshotType
        {
            get
            {
                if ((curObj[nameof(UserSnapshotType)] == null))
                {
                    return ((UserSnapshotTypeValues)(Convert.ToInt32(0)));
                }
                return ((UserSnapshotTypeValues)(Convert.ToInt32(curObj[nameof(UserSnapshotType)])));
            }
            set
            {
                if ((UserSnapshotTypeValues.NULL_ENUM_VALUE == value))
                {
                    curObj[nameof(UserSnapshotType)] = null;
                }
                else
                {
                    curObj[nameof(UserSnapshotType)] = value;
                }
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The version of the virtual system in a format of \"major.minor\", for example \"2.0\"" +
            ".\nWindows Server 2008:  The Version property is not supported.")]
        public string Version
        {
            get
            {
                return ((string)(curObj[nameof(Version)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsVirtualNumaEnabledNull
        {
            get
            {
                if ((curObj[nameof(VirtualNumaEnabled)] == null))
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
        [Description(@"Indicates whether virtual non-uniform memory access (NUMA) nodes are projected into the virtual machine. If FALSE, the virtual machine will have a single node. If TRUE, the number of virtual NUMA nodes projected into the virtual machine is determined from the values of the Msvm_ProcessorSettingData.MaxProcessorsPerNumaNode and Msvm_MemorySettingData.MaxMemoryBlocksPerNumaNode properties.
")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool VirtualNumaEnabled
        {
            get
            {
                if ((curObj[nameof(VirtualNumaEnabled)] == null))
                {
                    return Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(VirtualNumaEnabled)]));
            }
            set
            {
                curObj[nameof(VirtualNumaEnabled)] = value;
                if (((isEmbedded == false)
                            && (AutoCommitProp == true)))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The name of the CIM_ComputerSystem to which this setting data belongs")]
        public string VirtualSystemIdentifier
        {
            get
            {
                return ((string)(curObj[nameof(VirtualSystemIdentifier)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The subtype of the virtual system.")]
        public string VirtualSystemSubType
        {
            get
            {
                return ((string)(curObj[nameof(VirtualSystemSubType)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string VirtualSystemType
        {
            get
            {
                return ((string)(curObj[nameof(VirtualSystemType)]));
            }
        }

        private bool CheckIfProperClass(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions OptionsParam)
        {
            if (((path != null)
                        && (string.Compare(path.ClassName, ManagementClassName, true, CultureInfo.InvariantCulture) == 0)))
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
            if (((theObj != null)
                        && (string.Compare(((string)(theObj["__CLASS"])), ManagementClassName, true, CultureInfo.InvariantCulture) == 0)))
            {
                return true;
            }
            else
            {
                Array parentClasses = ((Array)(theObj["__DERIVATION"]));
                if ((parentClasses != null))
                {
                    int count = 0;
                    for (count = 0; (count < parentClasses.Length); count = (count + 1))
                    {
                        if ((string.Compare(((string)(parentClasses.GetValue(count))), ManagementClassName, true, CultureInfo.InvariantCulture) == 0))
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
            curObj[nameof(AdditionalRecoveryInformation)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeAllowFullSCSICommandSet()
        {
            if ((IsAllowFullSCSICommandSetNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetAllowFullSCSICommandSet()
        {
            curObj[nameof(AllowFullSCSICommandSet)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeAllowReducedFcRedundancy()
        {
            if ((IsAllowReducedFcRedundancyNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetAllowReducedFcRedundancy()
        {
            curObj[nameof(AllowReducedFcRedundancy)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeAutomaticCriticalErrorAction()
        {
            if ((IsAutomaticCriticalErrorActionNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetAutomaticCriticalErrorAction()
        {
            curObj[nameof(AutomaticCriticalErrorAction)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Converts a given time interval in DMTF format to System.TimeSpan object.
        static TimeSpan ToTimeSpan(string dmtfTimespan)
        {
            int days = 0;
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            long ticks = 0;
            if ((dmtfTimespan == null))
            {
                throw new ArgumentOutOfRangeException();
            }
            if ((dmtfTimespan.Length == 0))
            {
                throw new ArgumentOutOfRangeException();
            }
            if ((dmtfTimespan.Length != 25))
            {
                throw new ArgumentOutOfRangeException();
            }
            if ((dmtfTimespan.Substring(21, 4) != ":000"))
            {
                throw new ArgumentOutOfRangeException();
            }
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
                ticks = (long.Parse(tempString) * (TimeSpan.TicksPerMillisecond / 1000));
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
            if ((timespan.Days > maxTimeSpan.Days))
            {
                throw new ArgumentOutOfRangeException();
            }
            TimeSpan minTimeSpan = TimeSpan.MinValue;
            if ((timespan.Days < minTimeSpan.Days))
            {
                throw new ArgumentOutOfRangeException();
            }
            dmtftimespan = string.Concat(dmtftimespan, timespan.Hours.ToString().PadLeft(2, '0'));
            dmtftimespan = string.Concat(dmtftimespan, timespan.Minutes.ToString().PadLeft(2, '0'));
            dmtftimespan = string.Concat(dmtftimespan, timespan.Seconds.ToString().PadLeft(2, '0'));
            dmtftimespan = string.Concat(dmtftimespan, ".");
            TimeSpan tsTemp = new TimeSpan(timespan.Days, timespan.Hours, timespan.Minutes, timespan.Seconds, 0);
            long microsec = (((timespan.Ticks - tsTemp.Ticks)
                        * 1000)
                        / TimeSpan.TicksPerMillisecond);
            string strMicroSec = microsec.ToString();
            if ((strMicroSec.Length > 6))
            {
                strMicroSec = strMicroSec.Substring(0, 6);
            }
            dmtftimespan = string.Concat(dmtftimespan, strMicroSec.PadLeft(6, '0'));
            dmtftimespan = string.Concat(dmtftimespan, ":000");
            return dmtftimespan;
        }

        private bool ShouldSerializeAutomaticCriticalErrorActionTimeout()
        {
            if ((IsAutomaticCriticalErrorActionTimeoutNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetAutomaticCriticalErrorActionTimeout()
        {
            curObj[nameof(AutomaticCriticalErrorActionTimeout)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeAutomaticRecoveryAction()
        {
            if ((IsAutomaticRecoveryActionNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeAutomaticShutdownAction()
        {
            if ((IsAutomaticShutdownActionNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeAutomaticSnapshotsEnabled()
        {
            if ((IsAutomaticSnapshotsEnabledNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetAutomaticSnapshotsEnabled()
        {
            curObj[nameof(AutomaticSnapshotsEnabled)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeAutomaticStartupAction()
        {
            if ((IsAutomaticStartupActionNull == false))
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
            DateTime datetime = DateTime.MinValue;
            string tempString = string.Empty;
            if ((dmtf == null))
            {
                throw new ArgumentOutOfRangeException();
            }
            if ((dmtf.Length == 0))
            {
                throw new ArgumentOutOfRangeException();
            }
            if ((dmtf.Length != 25))
            {
                throw new ArgumentOutOfRangeException();
            }
            try
            {
                tempString = dmtf.Substring(0, 4);
                if (("****" != tempString))
                {
                    year = int.Parse(tempString);
                }
                tempString = dmtf.Substring(4, 2);
                if (("**" != tempString))
                {
                    month = int.Parse(tempString);
                }
                tempString = dmtf.Substring(6, 2);
                if (("**" != tempString))
                {
                    day = int.Parse(tempString);
                }
                tempString = dmtf.Substring(8, 2);
                if (("**" != tempString))
                {
                    hour = int.Parse(tempString);
                }
                tempString = dmtf.Substring(10, 2);
                if (("**" != tempString))
                {
                    minute = int.Parse(tempString);
                }
                tempString = dmtf.Substring(12, 2);
                if (("**" != tempString))
                {
                    second = int.Parse(tempString);
                }
                tempString = dmtf.Substring(15, 6);
                if (("******" != tempString))
                {
                    ticks = (long.Parse(tempString) * (TimeSpan.TicksPerMillisecond / 1000));
                }
                if (((((((((year < 0)
                            || (month < 0))
                            || (day < 0))
                            || (hour < 0))
                            || (minute < 0))
                            || (minute < 0))
                            || (second < 0))
                            || (ticks < 0)))
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException(null, e.Message);
            }
            datetime = new DateTime(year, month, day, hour, minute, second, 0);
            datetime = datetime.AddTicks(ticks);
            TimeSpan tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(datetime);
            int UTCOffset = 0;
            int OffsetToBeAdjusted = 0;
            long OffsetMins = (tickOffset.Ticks / TimeSpan.TicksPerMinute);
            tempString = dmtf.Substring(22, 3);
            if ((tempString != "******"))
            {
                tempString = dmtf.Substring(21, 4);
                try
                {
                    UTCOffset = int.Parse(tempString);
                }
                catch (Exception e)
                {
                    throw new ArgumentOutOfRangeException(null, e.Message);
                }
                OffsetToBeAdjusted = ((int)((OffsetMins - UTCOffset)));
                datetime = datetime.AddMinutes(OffsetToBeAdjusted);
            }
            return datetime;
        }

        // Converts a given System.DateTime object to DMTF datetime format.
        static string ToDmtfDateTime(DateTime date)
        {
            string utcString = string.Empty;
            TimeSpan tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(date);
            long OffsetMins = (tickOffset.Ticks / TimeSpan.TicksPerMinute);
            if ((Math.Abs(OffsetMins) > 999))
            {
                date = date.ToUniversalTime();
                utcString = "+000";
            }
            else
            {
                if ((tickOffset.Ticks >= 0))
                {
                    utcString = string.Concat("+", (tickOffset.Ticks / TimeSpan.TicksPerMinute).ToString().PadLeft(3, '0'));
                }
                else
                {
                    string strTemp = OffsetMins.ToString();
                    utcString = string.Concat("-", strTemp.Substring(1, (strTemp.Length - 1)).PadLeft(3, '0'));
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
            long microsec = (((date.Ticks - dtTemp.Ticks)
                        * 1000)
                        / TimeSpan.TicksPerMillisecond);
            string strMicrosec = microsec.ToString();
            if ((strMicrosec.Length > 6))
            {
                strMicrosec = strMicrosec.Substring(0, 6);
            }
            dmtfDateTime = string.Concat(dmtfDateTime, strMicrosec.PadLeft(6, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, utcString);
            return dmtfDateTime;
        }

        private bool ShouldSerializeAutomaticStartupActionDelay()
        {
            if ((IsAutomaticStartupActionDelayNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeAutomaticStartupActionSequenceNumber()
        {
            if ((IsAutomaticStartupActionSequenceNumberNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetBaseBoardSerialNumber()
        {
            curObj[nameof(BaseBoardSerialNumber)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetBIOSGUID()
        {
            curObj[nameof(BIOSGUID)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeBIOSNumLock()
        {
            if ((IsBIOSNumLockNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetBIOSNumLock()
        {
            curObj[nameof(BIOSNumLock)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetBIOSSerialNumber()
        {
            curObj[nameof(BIOSSerialNumber)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetBootOrder()
        {
            curObj[nameof(BootOrder)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetBootSourceOrder()
        {
            curObj[nameof(BootSourceOrder)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetChassisAssetTag()
        {
            curObj[nameof(ChassisAssetTag)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetChassisSerialNumber()
        {
            curObj[nameof(ChassisSerialNumber)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeConsoleMode()
        {
            if ((IsConsoleModeNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetConsoleMode()
        {
            curObj[nameof(ConsoleMode)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeCreationTime()
        {
            if ((IsCreationTimeNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeDebugChannelId()
        {
            if ((IsDebugChannelIdNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetDebugChannelId()
        {
            curObj[nameof(DebugChannelId)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeDebugPort()
        {
            if ((IsDebugPortNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetDebugPort()
        {
            curObj[nameof(DebugPort)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeDebugPortEnabled()
        {
            if ((IsDebugPortEnabledNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetDebugPortEnabled()
        {
            curObj[nameof(DebugPortEnabled)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeEnableHibernation()
        {
            if ((IsEnableHibernationNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetEnableHibernation()
        {
            curObj[nameof(EnableHibernation)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeEnhancedSessionTransportType()
        {
            if ((IsEnhancedSessionTransportTypeNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetEnhancedSessionTransportType()
        {
            curObj[nameof(EnhancedSessionTransportType)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeGuestControlledCacheTypes()
        {
            if ((IsGuestControlledCacheTypesNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetGuestControlledCacheTypes()
        {
            curObj[nameof(GuestControlledCacheTypes)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeHighMmioGapBase()
        {
            if ((IsHighMmioGapBaseNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetHighMmioGapBase()
        {
            curObj[nameof(HighMmioGapBase)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeHighMmioGapSize()
        {
            if ((IsHighMmioGapSizeNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetHighMmioGapSize()
        {
            curObj[nameof(HighMmioGapSize)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeIncrementalBackupEnabled()
        {
            if ((IsIncrementalBackupEnabledNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetIncrementalBackupEnabled()
        {
            curObj[nameof(IncrementalBackupEnabled)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeIsAutomaticSnapshot()
        {
            if ((IsIsAutomaticSnapshotNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeIsSaved()
        {
            if ((IsIsSavedNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeLockOnDisconnect()
        {
            if ((IsLockOnDisconnectNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetLockOnDisconnect()
        {
            curObj[nameof(LockOnDisconnect)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeLowMmioGapSize()
        {
            if ((IsLowMmioGapSizeNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetLowMmioGapSize()
        {
            curObj[nameof(LowMmioGapSize)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeNetworkBootPreferredProtocol()
        {
            if ((IsNetworkBootPreferredProtocolNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetNetworkBootPreferredProtocol()
        {
            curObj[nameof(NetworkBootPreferredProtocol)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializePauseAfterBootFailure()
        {
            if ((IsPauseAfterBootFailureNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetPauseAfterBootFailure()
        {
            curObj[nameof(PauseAfterBootFailure)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeSecureBootEnabled()
        {
            if ((IsSecureBootEnabledNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetSecureBootEnabled()
        {
            curObj[nameof(SecureBootEnabled)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetSecureBootTemplateId()
        {
            curObj[nameof(SecureBootTemplateId)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeTurnOffOnGuestRestart()
        {
            if ((IsTurnOffOnGuestRestartNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetTurnOffOnGuestRestart()
        {
            curObj[nameof(TurnOffOnGuestRestart)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeUserSnapshotType()
        {
            if ((IsUserSnapshotTypeNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetUserSnapshotType()
        {
            curObj[nameof(UserSnapshotType)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private bool ShouldSerializeVirtualNumaEnabled()
        {
            if ((IsVirtualNumaEnabledNull == false))
            {
                return true;
            }
            return false;
        }

        private void ResetVirtualNumaEnabled()
        {
            curObj[nameof(VirtualNumaEnabled)] = null;
            if (((isEmbedded == false)
                        && (AutoCommitProp == true)))
            {
                PrivateLateBoundObject.Put();
            }
        }

        [Browsable(true)]
        public void CommitObject()
        {
            if ((isEmbedded == false))
            {
                PrivateLateBoundObject.Put();
            }
        }

        [Browsable(true)]
        public void CommitObject(PutOptions putOptions)
        {
            if ((isEmbedded == false))
            {
                PrivateLateBoundObject.Put(putOptions);
            }
        }

        private void Initialize()
        {
            AutoCommitProp = true;
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
            if ((path != null))
            {
                if ((CheckIfProperClass(mgmtScope, path, getOptions) != true))
                {
                    throw new ArgumentException("Class name does not match.");
                }
            }
            PrivateLateBoundObject = new ManagementObject(mgmtScope, path, getOptions);
            PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            curObj = PrivateLateBoundObject;
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static VirtualSystemSettingDataCollection GetInstances()
        {
            return GetInstances(null, null, null);
        }

        public static VirtualSystemSettingDataCollection GetInstances(string condition)
        {
            return GetInstances(null, condition, null);
        }

        public static VirtualSystemSettingDataCollection GetInstances(string[] selectedProperties)
        {
            return GetInstances(null, null, selectedProperties);
        }

        public static VirtualSystemSettingDataCollection GetInstances(string condition, string[] selectedProperties)
        {
            return GetInstances(null, condition, selectedProperties);
        }

        public static VirtualSystemSettingDataCollection GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions)
        {
            if ((mgmtScope == null))
            {
                if ((statMgmtScope == null))
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\virtualization\\v2";
                }
                else
                {
                    mgmtScope = statMgmtScope;
                }
            }
            ManagementPath pathObj = new ManagementPath();
            pathObj.ClassName = "Msvm_VirtualSystemSettingData";
            pathObj.NamespacePath = "root\\virtualization\\v2";
            ManagementClass clsObject = new ManagementClass(mgmtScope, pathObj, null);
            if ((enumOptions == null))
            {
                enumOptions = new EnumerationOptions();
                enumOptions.EnsureLocatable = true;
            }
            return new VirtualSystemSettingDataCollection(clsObject.GetInstances(enumOptions));
        }

        public static VirtualSystemSettingDataCollection GetInstances(ManagementScope mgmtScope, string condition)
        {
            return GetInstances(mgmtScope, condition, null);
        }

        public static VirtualSystemSettingDataCollection GetInstances(ManagementScope mgmtScope, string[] selectedProperties)
        {
            return GetInstances(mgmtScope, null, selectedProperties);
        }

        public static VirtualSystemSettingDataCollection GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties)
        {
            if ((mgmtScope == null))
            {
                if ((statMgmtScope == null))
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\virtualization\\v2";
                }
                else
                {
                    mgmtScope = statMgmtScope;
                }
            }
            ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery("Msvm_VirtualSystemSettingData", condition, selectedProperties));
            EnumerationOptions enumOptions = new EnumerationOptions();
            enumOptions.EnsureLocatable = true;
            ObjectSearcher.Options = enumOptions;
            return new VirtualSystemSettingDataCollection(ObjectSearcher.Get());
        }

        [Browsable(true)]
        public static VirtualSystemSettingData CreateInstance()
        {
            ManagementScope mgmtScope;
            if ((statMgmtScope == null))
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
                return new VirtualSystemSettingData(tmpMgmtClass.CreateInstance());
            }
        }

        [Browsable(true)]
        public void Delete()
        {
            PrivateLateBoundObject.Delete();
        }

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

        // Enumerator implementation for enumerating instances of the class.
        public class VirtualSystemSettingDataCollection : object, ICollection
        {

            private ManagementObjectCollection privColObj;

            public VirtualSystemSettingDataCollection(ManagementObjectCollection objCollection)
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
                for (nCtr = 0; (nCtr < array.Length); nCtr = (nCtr + 1))
                {
                    array.SetValue(new VirtualSystemSettingData(((ManagementObject)(array.GetValue(nCtr)))), nCtr);
                }
            }

            public virtual IEnumerator GetEnumerator()
            {
                return new VirtualSystemSettingDataEnumerator(privColObj.GetEnumerator());
            }

            public class VirtualSystemSettingDataEnumerator : object, IEnumerator
            {

                private ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;

                public VirtualSystemSettingDataEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum)
                {
                    privObjEnum = objEnum;
                }

                public virtual object Current
                {
                    get
                    {
                        return new VirtualSystemSettingData(((ManagementObject)(privObjEnum.Current)));
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
                if ((baseType.BaseType == typeof(Enum)))
                {
                    if ((value.GetType() == destinationType))
                    {
                        return value;
                    }
                    if ((((value == null)
                                && (context != null))
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false)))
                    {
                        return "NULL_ENUM_VALUE";
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (((baseType == typeof(bool))
                            && (baseType.BaseType == typeof(ValueType))))
                {
                    if ((((value == null)
                                && (context != null))
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false)))
                    {
                        return "";
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (((context != null)
                            && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false)))
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
                    return ((int)(PrivateLateBoundObject["__GENUS"]));
                }
            }

            [Browsable(true)]
            public string CLASS
            {
                get
                {
                    return ((string)(PrivateLateBoundObject["__CLASS"]));
                }
            }

            [Browsable(true)]
            public string SUPERCLASS
            {
                get
                {
                    return ((string)(PrivateLateBoundObject["__SUPERCLASS"]));
                }
            }

            [Browsable(true)]
            public string DYNASTY
            {
                get
                {
                    return ((string)(PrivateLateBoundObject["__DYNASTY"]));
                }
            }

            [Browsable(true)]
            public string RELPATH
            {
                get
                {
                    return ((string)(PrivateLateBoundObject["__RELPATH"]));
                }
            }

            [Browsable(true)]
            public int PROPERTY_COUNT
            {
                get
                {
                    return ((int)(PrivateLateBoundObject["__PROPERTY_COUNT"]));
                }
            }

            [Browsable(true)]
            public string[] DERIVATION
            {
                get
                {
                    return ((string[])(PrivateLateBoundObject["__DERIVATION"]));
                }
            }

            [Browsable(true)]
            public string SERVER
            {
                get
                {
                    return ((string)(PrivateLateBoundObject["__SERVER"]));
                }
            }

            [Browsable(true)]
            public string NAMESPACE
            {
                get
                {
                    return ((string)(PrivateLateBoundObject["__NAMESPACE"]));
                }
            }

            [Browsable(true)]
            public string PATH
            {
                get
                {
                    return ((string)(PrivateLateBoundObject["__PATH"]));
                }
            }
        }
    }
}
