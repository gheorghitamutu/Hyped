using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.VirtualSystemManagement
{
    public class ConcreteJob : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(ConcreteJob)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public ConcreteJob() : base(ClassName) { }

        public ConcreteJob(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ConcreteJob(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ConcreteJob(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public ConcreteJob(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public ConcreteJob(ManagementPath path) : base(path, ClassName) { }

        public ConcreteJob(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public ConcreteJob(ManagementObject theObject) : base(theObject, ClassName) { }

        public ConcreteJob(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * Indicates whether the job can be cancelled.
         * The value of this property does not guarantee that a request to cancel the job will succeed.
         */
        public bool Cancellable
        {
            get
            {
                if (LateBoundObject[nameof(Cancellable)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(Cancellable)];
            }
        }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        /*
         * On failure of the asynchronous operation, this property contains the file path to the child of the virtual hard drive being affected by this operation.
         */
        public string Child => (string)LateBoundObject[nameof(Child)];

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

        public bool DeleteOnCompletion
        {
            get
            {
                if (LateBoundObject[nameof(DeleteOnCompletion)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(DeleteOnCompletion)];
            }
        }

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

        public DateTime ElapsedTime
        {
            get
            {
                if (LateBoundObject[nameof(ElapsedTime)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(ElapsedTime)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public ushort ErrorCode
        {
            get
            {
                if (LateBoundObject[nameof(ErrorCode)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ErrorCode)];
            }
        }

        public string ErrorDescription => (string)LateBoundObject[nameof(ErrorDescription)];

        public string ErrorSummaryDescription => (string)LateBoundObject[nameof(ErrorSummaryDescription)];

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
         * The HRESULT code that describes the completion status for the asynchronous operation.
         */
        public uint JobCompletionStatusCode
        {
            get
            {
                if (LateBoundObject[nameof(JobCompletionStatusCode)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(JobCompletionStatusCode)];
            }
        }

        public uint JobRunTimes
        {
            get
            {
                if (LateBoundObject[nameof(JobRunTimes)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(JobRunTimes)];
            }
        }

        public ushort JobState
        {
            get
            {
                if (LateBoundObject[nameof(JobState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(JobState)];
            }
        }

        public string JobStatus => (string)LateBoundObject[nameof(JobStatus)];

        /*
         * The type of asynchronous operation being tracked by this instance of Msvm_ConcreteJob.
         * Values are:
         * Unknown
         * VHD Creation: Creating a virtual hard disk image (VHD).
         * Floppy Creation: Creating a virtual floppy disk image (VFD).
         * Compaction: Compacting the size of a virtual hard disk image.
         * Expansion: Expanding the size of a virtual hard disk image.
         * Merging: Merging multiple virtual hard disk images into a single image.
         * Conversion: Converting the type of a virtual hard disk image.
         * Loopback Mount: Mounting the virtual hard disk on the parent partition.
         * Get VHD Info: Retrieving information about a virtual hard disk image (VHD).
         * Validate VHD Image: Validating a virtual hard disk image (VHD).
         */
        public JobTypeValues JobType
        {
            get
            {
                if (LateBoundObject[nameof(JobType)] == null)
                {
                    return (JobTypeValues)Convert.ToInt32(10);
                }
                return (JobTypeValues)Convert.ToInt32(LateBoundObject[nameof(JobType)]);
            }
        }

        public ushort LocalOrUtcTime
        {
            get
            {
                if (LateBoundObject[nameof(LocalOrUtcTime)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(LocalOrUtcTime)];
            }
        }

        public string Name => (string)LateBoundObject[nameof(Name)];

        public string Notify => (string)LateBoundObject[nameof(Notify)];

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

        public string OtherRecoveryAction => (string)LateBoundObject[nameof(OtherRecoveryAction)];

        public string Owner => (string)LateBoundObject[nameof(Owner)];

        /*
         * On failure of the asynchronous operation, this property contains the file path to the parent of the virtual hard drive being affected by this operation.
         */
        public string Parent => (string)LateBoundObject[nameof(Parent)];

        public ushort PercentComplete
        {
            get
            {
                if (LateBoundObject[nameof(PercentComplete)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(PercentComplete)];
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

        public uint Priority
        {
            get
            {
                if (LateBoundObject[nameof(Priority)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(Priority)];
            }
        }

        public ushort RecoveryAction
        {
            get
            {
                if (LateBoundObject[nameof(RecoveryAction)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(RecoveryAction)];
            }
        }

        public sbyte RunDay
        {
            get
            {
                if (LateBoundObject[nameof(RunDay)] == null)
                {
                    return Convert.ToSByte(0);
                }
                return (sbyte)LateBoundObject[nameof(RunDay)];
            }
        }

        public sbyte RunDayOfWeek
        {
            get
            {
                if (LateBoundObject[nameof(RunDayOfWeek)] == null)
                {
                    return Convert.ToSByte(0);
                }
                return (sbyte)LateBoundObject[nameof(RunDayOfWeek)];
            }
        }

        public byte RunMonth
        {
            get
            {
                if (LateBoundObject[nameof(RunMonth)] == null)
                {
                    return Convert.ToByte(0);
                }
                return (byte)LateBoundObject[nameof(RunMonth)];
            }
        }

        public DateTime RunStartInterval
        {
            get
            {
                if (LateBoundObject[nameof(RunStartInterval)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(RunStartInterval)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public DateTime ScheduledStartTime
        {
            get
            {
                if (LateBoundObject[nameof(ScheduledStartTime)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(ScheduledStartTime)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public DateTime StartTime
        {
            get
            {
                if (LateBoundObject[nameof(StartTime)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(StartTime)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public string Status => (string)LateBoundObject[nameof(Status)];

        public string[] StatusDescriptions => (string[])LateBoundObject[nameof(StatusDescriptions)];

        public DateTime TimeBeforeRemoval
        {
            get
            {
                if (LateBoundObject[nameof(TimeBeforeRemoval)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(TimeBeforeRemoval)]);
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

        public DateTime TimeSubmitted
        {
            get
            {
                if (LateBoundObject[nameof(TimeSubmitted)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(TimeSubmitted)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public DateTime UntilTime
        {
            get
            {
                if (LateBoundObject[nameof(UntilTime)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(UntilTime)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<ConcreteJob> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteJob(mo)).ToList();

        public new static List<ConcreteJob> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteJob(mo)).ToList();

        public static List<ConcreteJob> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteJob(mo)).ToList();

        public static List<ConcreteJob> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteJob(mo)).ToList();

        public static List<ConcreteJob> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteJob(mo)).ToList();

        public static List<ConcreteJob> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteJob(mo)).ToList();

        public static List<ConcreteJob> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteJob(mo)).ToList();

        public static List<ConcreteJob> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ConcreteJob(mo)).ToList();

        public static ConcreteJob CreateInstance() => new ConcreteJob(CreateInstance(ClassName));

        public uint GetError(out string Error)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetError", inParams, null);
                Error = Convert.ToString(outParams.Properties["Error"].Value);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Error = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetErrorEx(out string[] Errors)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetErrorEx", inParams, null);
                Errors = (string[])outParams.Properties["Errors"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Errors = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint KillJob(bool DeleteOnKill)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("KillJob");
                inParams["DeleteOnKill"] = DeleteOnKill;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("KillJob", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint RequestStateChange(ushort RequestedState, DateTime TimeoutPeriod)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RequestStateChange");
                inParams["RequestedState"] = RequestedState;
                inParams["TimeoutPeriod"] = ToDmtfDateTime(TimeoutPeriod);
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RequestStateChange", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public enum JobTypeValues
        {
            Unknown0 = 0,
            Define_Virtual_Machine = 1,
            Modify_Virtual_Machine = 2,
            Destroy_Virtual_Machine = 3,
            Modify_Management_Service_Settings = 4,
            Initialize_Virtual_Machine = 10,
            Waiting_to_Start_Virtual_Machine = 11,
            Start_Virtual_Machine = 12,
            Power_Off_Virtual_Machine = 13,
            Save_Virtual_Machine = 14,
            Restore_Virtual_Machine = 15,
            Shut_Down_Virtual_Machine = 16,
            Pause_Virtual_Machine = 26,
            Resume_Virtual_Machine = 27,
            Reset_Virtual_Machine = 28,
            Reboot_Virtual_Machine = 29,
            Add_Virtual_Machine_Resources = 30,
            Modify_Virtual_Machine_Resources = 31,
            Remove_Virtual_Machine_Resources = 32,
            Request_Initial_Virtual_Machine_Memory = 40,
            Add_Memory_to_Virtual_Machine = 41,
            Remove_Memory_from_Virtual_Machine = 42,
            Merging_VHD_Disks = 50,
            Create_VSS_Snapshot_inside_Virtual_Machine = 51,
            Get_Import_Setting_Data = 60,
            Import_Virtual_Machine = 61,
            Export_Virtual_Machine = 62,
            Register_Configuration = 63,
            Unregister_Configuration = 64,
            Snapshot_Virtual_Machine = 70,
            Apply_Virtual_Machine_Snapshot = 71,
            Delete_Virtual_Machine_Snapshot = 72,
            Clear_Virtual_Machine_Snapshot_State = 73,
            Add_Resources_to_Resource_Pool = 80,
            Remove_Resources_from_Resource_Pool = 81,
            Modify_Replication_Server_Settings = 90,
            Create_Replication_Relationship = 91,
            Modify_Replication_Relationship_Settings = 92,
            Remove_Replication_Relationship = 93,
            Start_Inband_Initial_Replication = 94,
            Import_Replication = 95,
            Replicate_State_Change = 96,
            Initiate_Failover = 97,
            Revert_Failover = 98,
            Commit_Failover = 99,
            Inititate_Synced_Replication = 100,
            Cancel_Synced_Replication = 101,
            Initiate_Test_Replica = 102,
            Remove_Test_Replica = 103,
            Reverse_Replication = 104,
            Replication_Sending_Delta = 105,
            Replication_Receiving_Delta = 106,
            Resynchronizing = 107,
            Apply_change_log = 108,
            Stop_Initial_Replication = 109,
            Stop_Resynchronizing = 110,
            Get_Replica_statistics = 111,
            Prepare_for_Consistency_Checker = 112,
            Consistency_Checker = 113,
            Stop_Consistency_Checker = 114,
            Test_Replication_Connection = 115,
            Sending_Initial_Replica = 116,
            Start_Resync_Initial_Replication = 117,
            Start_Export_Initial_Replication = 118,
            Reset_Replica_Statistics = 119,
            Apply_Registered_Deltas = 120,
            Resynchronizing_Extended_Replication = 121,
            Reading_Test_Replica_Configuration = 122,
            Change_the_replication_mode_to_primary = 123,
            Initiate_Failback = 124,
            Update_Disk_Set = 125,
            Define_Ethernet_Switch = 130,
            Modify_Ethernet_Switch_Settings = 131,
            Destroy_Ethernet_Switch = 132,
            Add_Ethernet_Switch_Resources = 133,
            Modify_Ethernet_Switch_Resources = 134,
            Remove_Ethernet_Switch_Resources = 135,
            Validate_Planned_Virtual_Machine = 140,
            Realizing_Virtual_Machine = 141,
            Creating_a_Resource_Pool = 150,
            Changing_the_Parent_Resources_of_a_Resource_Pool = 151,
            Changing_the_Non_alloction_Settings_of_a_Resource_Pool = 152,
            Deleting_a_Resource_Pool = 153,
            Enable_RemoteFx_GPU = 160,
            Disable_RemoteFx_GPU = 161,
            Modify_3D_Service_Settings = 162,
            Backup_Virtual_Machine = 170,
            Guest_Service_Interface = 180,
            Query_Guest_Cluster_Information = 181,
            Define_Collection = 190,
            Destroy_Collection = 191,
            Rename_Collection = 192,
            Add_Member_to_Collection = 193,
            Remove_Member_from_Collection = 194,
            Add_Setting_to_Collection = 195,
            Remove_Setting_from_Collection = 196,
            Modify_Setting_on_Collection = 197,
            Snapshot_Collection = 198,
            Convert_Snapshot_to_Reference_Point = 200,
            Create_Reference_Point = 201,
            Delete_Reference_Point = 202,
            Export_Reference_Point = 203,
            Remove_Associated_Data_from_Reference_Point = 204,
            Create_Reference_Point_on_Collection = 205,
            Export_Reference_Point_on_Collection = 206,
            Remove_Associated_Data_from_Reference_Point_on_Collection = 207,
            Delete_Reference_Point_on_Collection = 208,
            Import_Reference_Point_metadata = 209,
            Mount_or_Dismount_Assignable_Device = 260,
            NULL_ENUM_VALUE = 261,
        }
    }
}
