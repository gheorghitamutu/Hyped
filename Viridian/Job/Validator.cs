using System;
using System.Management;
using System.Threading;
using Viridian.Exceptions;

namespace Viridian.Job
{
    public static class Validator
    {
        public enum JobState : ushort
        {
            New = 2,
            Starting = 3,
            Running = 4,
            Suspended = 5,
            ShuttingDown = 6,
            Completed = 7,
            Terminated = 8,
            Killed = 9,
            Exception = 10,
            Service = 11,
            CompletedWithWarnings = 32768
        }

        public static class ReturnCode
        {
            public const uint Completed = 0;
            public const uint Started = 4096;
            public const uint Failed = 32768;
            public const uint AccessDenied = 32769;
            public const uint NotSupported = 32770;
            public const uint Unknown = 32771;
            public const uint Timeout = 32772;
            public const uint InvalidParameter = 32773;
            public const uint SystemInUse = 32774;
            public const uint InvalidState = 32775;
            public const uint IncorrectDataType = 32776;
            public const uint SystemNotAvailable = 32777;
            public const uint OutofMemory = 32778;
        }

        public static void ValidateOutput(ManagementBaseObject outputParameters, ManagementScope scope)
        {
            var errorMessage = "The method call failed!";

            if ((uint)outputParameters["ReturnValue"] == ReturnCode.Started)
            {
                // The method invoked an asynchronous operation. Get the Job object
                // and wait for it to complete. Then we can check its result.

                using (var job = new ManagementObject(outputParameters["Job"] as string) { Scope = scope })
                {
                    var jobState = IsJobComplete(job["JobState"]);

                    if (jobState && IsJobSuccessful(job["JobState"]))
                        return;

                    while (!jobState)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        job.Get();
                        jobState = IsJobComplete(job["JobState"]);
                    }

                    if (IsJobSuccessful(job["JobState"]))
                        return;

                    if (!string.IsNullOrEmpty(job["ErrorDescription"] as string))
                        errorMessage = (string)job["ErrorDescription"];

                    var errors = GetMsvmErrorsList(job);
                    if (errors.Length > 0)
                    {
                        throw new ViridianException(errorMessage, errors, new ManagementException());
                    }
                    else if ((uint)outputParameters["ReturnValue"] != ReturnCode.Completed)
                    {
                        errorMessage = $"The method call failed! Error code {(uint)outputParameters["ReturnValue"]}";
                        throw new ViridianException(errorMessage, new ManagementException());
                    }
                }
            }
        }

        public static bool IsJobComplete(object jobStateObj)
        {
            var jobState = (JobState)((ushort)jobStateObj);

            return (jobState == JobState.Completed) || (jobState == JobState.CompletedWithWarnings) || (jobState == JobState.Terminated) || (jobState == JobState.Exception) || (jobState == JobState.Killed);
        }

        public static bool IsJobSuccessful(object jobStateObj)
        {
            var jobState = (JobState)((ushort)jobStateObj);

            return (jobState == JobState.Completed) || (jobState == JobState.CompletedWithWarnings);
        }

        public static string[] GetMsvmErrorsList(ManagementObject job)
        {
            var inParams = job.GetMethodParameters("GetErrorEx");
            var outParams = job.InvokeMethod("GetErrorEx", inParams, null);

            if (outParams != null && (uint)outParams["ReturnValue"] != ReturnCode.Completed)
                throw new ViridianException("GetErrorEx() call on the job failed!", new ManagementException());

            if (outParams == null)
                return new string[0];

            if (outParams["Errors"] is string[] == false)
                return new string[0];

            return (string[])outParams["Errors"];
        }
    }
}
