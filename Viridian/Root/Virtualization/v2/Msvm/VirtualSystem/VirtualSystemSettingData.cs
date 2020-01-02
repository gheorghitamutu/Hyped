using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;

namespace Viridian.Root.Virtualization.v2.Msvm.VirtualSystem
{
    public class VirtualSystemSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(VirtualSystemSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public VirtualSystemSettingData() : base(ClassName) { }

        public VirtualSystemSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualSystemSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualSystemSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public VirtualSystemSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public VirtualSystemSettingData(ManagementPath path) : base(path, ClassName) { }

        public VirtualSystemSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public VirtualSystemSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public VirtualSystemSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Indicates whether SCSI commands from the guest operating system are passed to pass-through __Disks.
         * If TRUE, SCSI commands emitted by the guest operating system to pass-through __Disks are not filtered.
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
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
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
                if ((IsEmbedded == false)
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
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
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
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
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
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
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
                if ((IsEmbedded == false)
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
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
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
                if ((IsEmbedded == false)
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
                if ((IsEmbedded == false)
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
         * 0 (Floppy): The virtual computer system will attempt to boot from the floppy __Disk within the floppy drive.
         * 1 (CD-ROM): The virtual computer system will attempt to boot from the first CD or DVD __Disk found with a boot sector.
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
                if ((IsEmbedded == false)
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
                if ((IsEmbedded == false)
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
                if ((IsEmbedded == false)
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
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string ConfigurationDataRoot => (string)LateBoundObject[nameof(ConfigurationDataRoot)];

        public string ConfigurationFile => (string)LateBoundObject[nameof(ConfigurationFile)];

        public string ConfigurationID => (string)LateBoundObject[nameof(ConfigurationID)];

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
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
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
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
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
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
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
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

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
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
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
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
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
                if ((IsEmbedded == false) && (AutoCommit == true))
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
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
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
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
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
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

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
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string LogDataRoot => (string)LateBoundObject[nameof(LogDataRoot)];

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
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
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
                if ((IsEmbedded == false)
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
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string RecoveryFile => (string)LateBoundObject[nameof(RecoveryFile)];

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
                if ((IsEmbedded == false)
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
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string SnapshotDataRoot => (string)LateBoundObject[nameof(SnapshotDataRoot)];

        public string SuspendDataRoot => (string)LateBoundObject[nameof(SuspendDataRoot)];

        public string SwapFileDataRoot => (string)LateBoundObject[nameof(SwapFileDataRoot)];

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
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
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
                if ((IsEmbedded == false) && (AutoCommit == true))
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
                if ((IsEmbedded == false)
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
               
        private void ResetAdditionalRecoveryInformation()
        {
            LateBoundObject[nameof(AdditionalRecoveryInformation)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetAllowFullSCSICommandSet()
        {
            LateBoundObject[nameof(AllowFullSCSICommandSet)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetAllowReducedFcRedundancy()
        {
            LateBoundObject[nameof(AllowReducedFcRedundancy)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetAutomaticCriticalErrorAction()
        {
            LateBoundObject[nameof(AutomaticCriticalErrorAction)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetAutomaticCriticalErrorActionTimeout()
        {
            LateBoundObject[nameof(AutomaticCriticalErrorActionTimeout)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetAutomaticSnapshotsEnabled()
        {
            LateBoundObject[nameof(AutomaticSnapshotsEnabled)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetBaseBoardSerialNumber()
        {
            LateBoundObject[nameof(BaseBoardSerialNumber)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetBIOSGUID()
        {
            LateBoundObject[nameof(BIOSGUID)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }
                
        private void ResetBIOSNumLock()
        {
            LateBoundObject[nameof(BIOSNumLock)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetBIOSSerialNumber()
        {
            LateBoundObject[nameof(BIOSSerialNumber)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetBootOrder()
        {
            LateBoundObject[nameof(BootOrder)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetBootSourceOrder()
        {
            LateBoundObject[nameof(BootSourceOrder)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetChassisAssetTag()
        {
            LateBoundObject[nameof(ChassisAssetTag)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetChassisSerialNumber()
        {
            LateBoundObject[nameof(ChassisSerialNumber)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetConsoleMode()
        {
            LateBoundObject[nameof(ConsoleMode)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetDebugChannelId()
        {
            LateBoundObject[nameof(DebugChannelId)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetDebugPort()
        {
            LateBoundObject[nameof(DebugPort)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetDebugPortEnabled()
        {
            LateBoundObject[nameof(DebugPortEnabled)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetEnableHibernation()
        {
            LateBoundObject[nameof(EnableHibernation)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetEnhancedSessionTransportType()
        {
            LateBoundObject[nameof(EnhancedSessionTransportType)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetGuestControlledCacheTypes()
        {
            LateBoundObject[nameof(GuestControlledCacheTypes)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetHighMmioGapBase()
        {
            LateBoundObject[nameof(HighMmioGapBase)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetHighMmioGapSize()
        {
            LateBoundObject[nameof(HighMmioGapSize)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetIncrementalBackupEnabled()
        {
            LateBoundObject[nameof(IncrementalBackupEnabled)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetLockOnDisconnect()
        {
            LateBoundObject[nameof(LockOnDisconnect)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetLowMmioGapSize()
        {
            LateBoundObject[nameof(LowMmioGapSize)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetNetworkBootPreferredProtocol()
        {
            LateBoundObject[nameof(NetworkBootPreferredProtocol)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetPauseAfterBootFailure()
        {
            LateBoundObject[nameof(PauseAfterBootFailure)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetSecureBootEnabled()
        {
            LateBoundObject[nameof(SecureBootEnabled)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetSecureBootTemplateId()
        {
            LateBoundObject[nameof(SecureBootTemplateId)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetTurnOffOnGuestRestart()
        {
            LateBoundObject[nameof(TurnOffOnGuestRestart)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }
        
        private void ResetUserSnapshotType()
        {
            LateBoundObject[nameof(UserSnapshotType)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetVirtualNumaEnabled()
        {
            LateBoundObject[nameof(VirtualNumaEnabled)] = null;
            if ((IsEmbedded == false)
                        && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<VirtualSystemSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingData(mo)).ToList();

        public new static List<VirtualSystemSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingData(mo)).ToList();

        public static List<VirtualSystemSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingData(mo)).ToList();

        public static List<VirtualSystemSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingData(mo)).ToList();

        public static List<VirtualSystemSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingData(mo)).ToList();

        public static List<VirtualSystemSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingData(mo)).ToList();

        public static List<VirtualSystemSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingData(mo)).ToList();

        public static List<VirtualSystemSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSettingData(mo)).ToList();

        public static VirtualSystemSettingData CreateInstance() => new VirtualSystemSettingData(CreateInstance(ClassName));

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
    }
}
