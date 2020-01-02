using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Storage
{
    public class StorageJob : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(StorageJob)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public StorageJob() : base(ClassName) { }

        public StorageJob(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public StorageJob(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public StorageJob(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public StorageJob(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public StorageJob(ManagementPath path) : base(path, ClassName) { }

        public StorageJob(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public StorageJob(ManagementObject theObject) : base(theObject, ClassName) { }

        public StorageJob(ManagementBaseObject theObject) : base(theObject, ClassName) { }
        
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
         * The type of asynchronous operation being tracked by this instance of Msvm_StorageJob.
         * Values are:
         * Unknown
         * VHD Creation: Creating a virtual hard __Disk image (VHD).
         * Floppy Creation: Creating a virtual floppy __Disk image (VFD).
         * Compaction: Compacting the size of a virtual hard __Disk image.
         * Expansion: Expanding the size of a virtual hard __Disk image.
         * Merging: Merging multiple virtual hard __Disk images into a single image.
         * Conversion: Converting the type of a virtual hard __Disk image.
         * Loopback Mount: Mounting the virtual hard __Disk on the parent partition.
         * Get VHD Info: Retrieving information about a virtual hard __Disk image (VHD).
         * Validate VHD Image: Validating a virtual hard __Disk image (VHD).
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
        public static List<StorageJob> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new StorageJob(mo)).ToList();

        public new static List<StorageJob> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new StorageJob(mo)).ToList();

        public static List<StorageJob> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new StorageJob(mo)).ToList();

        public static List<StorageJob> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new StorageJob(mo)).ToList();

        public static List<StorageJob> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new StorageJob(mo)).ToList();

        public static List<StorageJob> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new StorageJob(mo)).ToList();

        public static List<StorageJob> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new StorageJob(mo)).ToList();

        public static List<StorageJob> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new StorageJob(mo)).ToList();

        public static StorageJob CreateInstance() => new StorageJob(CreateInstance(ClassName));

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
            VHD_Creation = 1,
            Floppy_Creation = 2,
            Compaction = 3,
            Expansion = 4,
            Merging = 5,
            Conversion = 6,
            Loopback_Mount = 7,
            Get_VHD_Info = 8,
            Validate_VHD_Image = 9,
            NULL_ENUM_VALUE = 10,
        }
    }
}
