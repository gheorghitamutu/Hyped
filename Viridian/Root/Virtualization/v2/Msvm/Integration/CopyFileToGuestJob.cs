using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Integration
{
    public class CopyFileToGuestJob : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(CopyFileToGuestJob)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public CopyFileToGuestJob() : base(ClassName) { }

        public CopyFileToGuestJob(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public CopyFileToGuestJob(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public CopyFileToGuestJob(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public CopyFileToGuestJob(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public CopyFileToGuestJob(ManagementPath path) : base(path, ClassName) { }

        public CopyFileToGuestJob(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public CopyFileToGuestJob(ManagementObject theObject) : base(theObject, ClassName) { }

        public CopyFileToGuestJob(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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

        /*
         * Copy file setting data.
         */
        public string[] CopyFileToGuestSettingData => (string[])LateBoundObject[nameof(CopyFileToGuestSettingData)];

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

        /*
         * GUID of the affected virtual system.
         */
        public string VirtualSystemName => (string)LateBoundObject[nameof(VirtualSystemName)];

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<CopyFileToGuestJob> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestJob(mo)).ToList();

        public new static List<CopyFileToGuestJob> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestJob(mo)).ToList();

        public static List<CopyFileToGuestJob> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestJob(mo)).ToList();

        public static List<CopyFileToGuestJob> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestJob(mo)).ToList();

        public static List<CopyFileToGuestJob> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestJob(mo)).ToList();

        public static List<CopyFileToGuestJob> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestJob(mo)).ToList();

        public static List<CopyFileToGuestJob> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestJob(mo)).ToList();

        public static List<CopyFileToGuestJob> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestJob(mo)).ToList();

        public static CopyFileToGuestJob CreateInstance() => new CopyFileToGuestJob(CreateInstance(ClassName));

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
    }
}
