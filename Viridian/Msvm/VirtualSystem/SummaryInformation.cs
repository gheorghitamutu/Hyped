using System;
using System.Management;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

namespace Viridian.Msvm.VirtualSystem
{
    public class SummaryInformation : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(SummaryInformation)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public SummaryInformation() : base(ClassName) { }

        public SummaryInformation(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public SummaryInformation(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public SummaryInformation(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public SummaryInformation(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public SummaryInformation(ManagementPath path) : base(path, ClassName) { }

        public SummaryInformation(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public SummaryInformation(ManagementObject theObject) : base(theObject, ClassName) { }

        public SummaryInformation(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * The identifier of the physical graphics processing unit (GPU) allocated to this virtual machine (VM). This property only applies to VMs that use RemoteFX.
         */
        public string AllocatedGPU => (string)LateBoundObject[nameof(AllocatedGPU)];

        /*
         * The current application health status for the virtual system.
         * This property may be one of the following values: 
         * "OK"; "Application Critical"; "Disabled".
         * For more information, see the documentation for the StatusDescriptions property of the Msvm_HeartbeatComponent class. 
         * This property is not valid for instances of Msvm_SummaryInformation representing a virtual system snapshot. 
         */
        public ApplicationHealthValues ApplicationHealth
        {
            get
            {
                if (LateBoundObject[nameof(ApplicationHealth)] == null)
                {
                    return (ApplicationHealthValues)Convert.ToInt32(0);
                }
                return (ApplicationHealthValues)Convert.ToInt32(LateBoundObject[nameof(ApplicationHealth)]);
            }
        }

        /*
         * An array of Msvm_ConcreteJob instances representing any asynchronous operations related to the virtual system which are currently executing.
         * This property is not valid for instances of Msvm_SummaryInformation representing a virtual system snapshot.
         */
        public ManagementBaseObject[] AsynchronousTasks => (ManagementBaseObject[])LateBoundObject[nameof(AsynchronousTasks)];

        /*
         * The available memory buffer percentage in the virtual system.
         */
        public int AvailableMemoryBuffer
        {
            get
            {
                if (LateBoundObject[nameof(AvailableMemoryBuffer)] == null)
                {
                    return Convert.ToInt32(0);
                }
                return (int)LateBoundObject[nameof(AvailableMemoryBuffer)];
            }
        }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

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

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

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

        public ushort EnhancedSessionModeState
        {
            get
            {
                if (LateBoundObject[nameof(EnhancedSessionModeState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(EnhancedSessionModeState)];
            }
        }

        /*
         * The name of the guest operating system, if available. If this information is not available,
         * the value of this property is NULL. This property is not valid for 
         * instances of Msvm_SummaryInformation representing a virtual system snapshot.
         */
        public string GuestOperatingSystem => (string)LateBoundObject[nameof(GuestOperatingSystem)];

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
         * The current heartbeat status for the virtual system. 
         * This property may be one of the following values: 
         * "OK"; "Error"; "No Contact"; or "Lost Communication". 
         * For more information, see the documentation for the StatusDescriptions property of the Msvm_HeartbeatComponent class. 
         * This property is not valid for instances of Msvm_SummaryInformation representing a virtual system snapshot.
         */
        public ushort Heartbeat
        {
            get
            {
                if (LateBoundObject[nameof(Heartbeat)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(Heartbeat)];
            }
        }

        public string HostComputerSystemName => (string)LateBoundObject[nameof(HostComputerSystemName)];

        /*
         * The unique identifier of the hypervisor partition used by the virtual system.
         */
        public ulong HypervisorPartitionId
        {
            get
            {
                if (LateBoundObject[nameof(HypervisorPartitionId)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(HypervisorPartitionId)];
            }
        }

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        /*
         * Whether or not the integration services installed in the virtual machine are up to date.
         */
        public IntegrationServicesVersionStateValues IntegrationServicesVersionState
        {
            get
            {
                if (LateBoundObject[nameof(IntegrationServicesVersionState)] == null)
                {
                    return (IntegrationServicesVersionStateValues)Convert.ToInt32(3);
                }
                return (IntegrationServicesVersionStateValues)Convert.ToInt32(LateBoundObject[nameof(IntegrationServicesVersionState)]);
            }
        }

        /*
         * The memory available percentage in the virtual system.
         */
        public int MemoryAvailable
        {
            get
            {
                if (LateBoundObject[nameof(MemoryAvailable)] == null)
                {
                    return Convert.ToInt32(0);
                }
                return (int)LateBoundObject[nameof(MemoryAvailable)];
            }
        }

        /*
         * Indicates whether or not the memory of the one or more of the virtual non-uniform
         * memory access (NUMA) nodes of the virtual machine spans multiple physical NUMA nodes of the hosting computer system.
         */
        public bool MemorySpansPhysicalNumaNodes
        {
            get
            {
                if (LateBoundObject[nameof(MemorySpansPhysicalNumaNodes)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(MemorySpansPhysicalNumaNodes)];
            }
        }

        /*
         * The current memory usage of the virtual system. This property is not valid for instances
         * of Msvm_SummaryInformation representing a virtual system snapshot.
         */
        public ulong MemoryUsage
        {
            get
            {
                if (LateBoundObject[nameof(MemoryUsage)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(MemoryUsage)];
            }
        }

        public string Name => (string)LateBoundObject[nameof(Name)];

        public string Notes => (string)LateBoundObject[nameof(Notes)];

        public ushort NumberOfProcessors
        {
            get
            {
                if (LateBoundObject[nameof(NumberOfProcessors)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(NumberOfProcessors)];
            }
        }

        public ushort[] OperationalStatus => (ushort[])LateBoundObject[nameof(OperationalStatus)];

        public string OtherEnabledState => (string)LateBoundObject[nameof(OtherEnabledState)];

        /*
         * The current processor usage of the virtual system. This property is not valid for
         * instances of Msvm_SummaryInformation representing a virtual system snapshot.
         */
        public ushort ProcessorLoad
        {
            get
            {
                if (LateBoundObject[nameof(ProcessorLoad)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ProcessorLoad)];
            }
        }

        /*
         * An array of the previous 100 samples of the processor usage for the virtual system.
         * This property is not valid for instances of Msvm_SummaryInformation representing a virtual system snapshot. 
         */
        public ushort[] ProcessorLoadHistory => (ushort[])LateBoundObject[nameof(ProcessorLoadHistory)];

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
         * The array of Replication health values for the various replication relationships of the virtual machine.
         */
        public ReplicationHealthExValues[] ReplicationHealthEx
        {
            get
            {
                Array arrEnumVals = (Array)LateBoundObject[nameof(ReplicationHealthEx)];
                ReplicationHealthExValues[] enumToRet = new ReplicationHealthExValues[arrEnumVals.Length];
                int counter;
                for (counter = 0; counter < arrEnumVals.Length; counter = counter + 1)
                {
                    enumToRet[counter] = (ReplicationHealthExValues)Convert.ToInt32(arrEnumVals.GetValue(counter));
                }
                return enumToRet;
            }
        }

        /*
         * "Identifies replication type for the virtual machine.
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
         * Globally unique identifier of provider that identifies the endpoint provider. 
         * For Hyper-V host to another Hyper-V host, provider id is fixed as 22391CDC-272C-4DDF-BA88-9BEFB1A0975C.
         * In case of external provider this is CLSID of provider COM class object.
         */
        public string[] ReplicationProviderId => (string[])LateBoundObject[nameof(ReplicationProviderId)];

        /*
         * Replication state for the virtual machine.
         */
        public ReplicationStateValues ReplicationState
        {
            get
            {
                if (LateBoundObject[nameof(ReplicationState)] == null)
                {
                    return (ReplicationStateValues)Convert.ToInt32(13);
                }
                return (ReplicationStateValues)Convert.ToInt32(LateBoundObject[nameof(ReplicationState)]);
            }
        }

        /*
         * The array of Replication state values for the various replication relationships of the virtual machine.
         */
        public ReplicationStateExValues[] ReplicationStateEx
        {
            get
            {
                Array arrEnumVals = (Array)LateBoundObject[nameof(ReplicationStateEx)];
                ReplicationStateExValues[] enumToRet = new ReplicationStateExValues[arrEnumVals.Length];
                int counter;
                for (counter = 0; counter < arrEnumVals.Length; counter = counter + 1)
                {
                    enumToRet[counter] = (ReplicationStateExValues)Convert.ToInt32(arrEnumVals.GetValue(counter));
                }
                return enumToRet;
            }
        }

        /*
         * Indicates whether or not shielding is configured for the virtual machine.
         */
        public bool Shielded
        {
            get
            {
                if (LateBoundObject[nameof(Shielded)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(Shielded)];
            }
        }

        /*
         * An array of Msvm_VirtualSystemSettingData instances representing the snapshots for the virtual system.
         * This property is not valid for instances of Msvm_SummaryInformation representing a virtual system snapshot.
         */
        public ManagementBaseObject[] Snapshots => (ManagementBaseObject[])LateBoundObject[nameof(Snapshots)];

        public string[] StatusDescriptions => (string[])LateBoundObject[nameof(StatusDescriptions)];

        /*
         * Indicates if Smart Paging is active.
         */
        public bool SwapFilesInUse
        {
            get
            {
                if (LateBoundObject[nameof(SwapFilesInUse)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(SwapFilesInUse)];
            }
        }

        /*
         * Reference to the CIM_ComputerSystem instance representing the test replica virtual
         * system for the virtual machine. This property is not valid for instances of Msvm_SummaryInformation
         * representing a virtual system snapshot.
         */
        public ManagementPath TestReplicaSystem
        {
            get
            {
                if (LateBoundObject[nameof(TestReplicaSystem)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(TestReplicaSystem)].ToString());
                }
                return null;
            }
        }

        /*
         * An array containing a small, thumbnail-sized image of the desktop for the virtual system or snapshot in RGB565 format.
         */
        public byte[] ThumbnailImage => (byte[])LateBoundObject[nameof(ThumbnailImage)];

        /*
         * The height in pixels of the image in the ThumbnailImage property.
         */
        public ushort ThumbnailImageHeight
        {
            get
            {
                if (LateBoundObject[nameof(ThumbnailImageHeight)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ThumbnailImageHeight)];
            }
        }

        /*
         * The width in pixels of the image in the ThumbnailImage property.
         */
        public ushort ThumbnailImageWidth
        {
            get
            {
                if (LateBoundObject[nameof(ThumbnailImageWidth)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ThumbnailImageWidth)];
            }
        }

        public ulong UpTime
        {
            get
            {
                if (LateBoundObject[nameof(UpTime)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(UpTime)];
            }
        }

        public string Version => (string)LateBoundObject[nameof(Version)];

        public string[] VirtualSwitchNames => (string[])LateBoundObject[nameof(VirtualSwitchNames)];

        public string VirtualSystemSubType => (string)LateBoundObject[nameof(VirtualSystemSubType)];

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<SummaryInformation> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new SummaryInformation(mo)).ToList();

        public new static List<SummaryInformation> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new SummaryInformation(mo)).ToList();

        public static List<SummaryInformation> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SummaryInformation(mo)).ToList();

        public static List<SummaryInformation> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SummaryInformation(mo)).ToList();

        public static List<SummaryInformation> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new SummaryInformation(mo)).ToList();

        public static List<SummaryInformation> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new SummaryInformation(mo)).ToList();

        public static List<SummaryInformation> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SummaryInformation(mo)).ToList();

        public static List<SummaryInformation> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SummaryInformation(mo)).ToList();

        public static SummaryInformation CreateInstance() => new SummaryInformation(CreateInstance(ClassName));

        public enum ApplicationHealthValues
        {
            OK = 2,
            Application_Critical = 32782,
            Disabled = 32896,
            NULL_ENUM_VALUE = 0,
        }

        public enum IntegrationServicesVersionStateValues
        {
            Unknown0 = 0,
            UpToDate = 1,
            Mismatch = 2,
            NULL_ENUM_VALUE = 3,
        }

        public enum ReplicationHealthValues
        {
            Not_applicable = 0,
            Ok = 1,
            Warning = 2,
            Critical = 3,
            NULL_ENUM_VALUE = 4,
        }

        public enum ReplicationHealthExValues
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
            NULL_ENUM_VALUE = 13,
        }

        public enum ReplicationStateExValues
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
            Disk_update_in_progress = 15,
            Disk_update_critical = 16,
            Unknown0 = 17,
            Repurpose_replication_in_progress = 18,
            Prepared_for_sync_replication = 19,
            Prepared_for_group_reverse_replication = 20,
            Firedrill_in_progress = 21,
            NULL_ENUM_VALUE = 22,
        }
        
        public enum RequestedInformation      : uint
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
    }
}
