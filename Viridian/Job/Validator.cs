using System;
using System.Management;
using System.Threading;
using Viridian.Exceptions;

namespace Viridian.Job
{
    public static class Validator
    {
        private enum JobState : ushort
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

        private static class ReturnCode
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
            if ((uint)outputParameters["ReturnValue"] == ReturnCode.InvalidParameter)
                throw new ViridianException("Invalid parameter passed to function!");

            var errorMessage = "The method call failed!";

            if ((uint)outputParameters["ReturnValue"] == ReturnCode.Started)
            {
                // The method invoked an asynchronous operation. Get the Job object
                // and wait for it to complete. Then we can check its result.

                using (var job = new ManagementObject(outputParameters["Job"] as string) { Scope = scope })
                {
                    var jobState = IsJobEnded(job["JobState"]);

                    if (jobState && IsJobSuccessful(job["JobState"]))
                        return;

                    while (!jobState)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        job.Get();
                        jobState = IsJobEnded(job["JobState"]);
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

        public static bool IsJobEnded(object jobStateObj)
        {
            var jobState = (JobState)((ushort)jobStateObj);

            return
                jobState == JobState.Completed ||
                jobState == JobState.CompletedWithWarnings ||
                jobState == JobState.Terminated ||
                jobState == JobState.Exception ||
                jobState == JobState.Killed;
        }

        public static bool IsJobSuccessful(object jobStateObj)
        {
            var jobState = (JobState)((ushort)jobStateObj);

            return (jobState == JobState.Completed) || (jobState == JobState.CompletedWithWarnings);
        }

        public static string[] GetMsvmErrorsList(ManagementObject job)
        {
            if (job == null)
                throw new ViridianException("Job object is null!");

            using (var ip = job.GetMethodParameters("GetErrorEx"))
            using (var op = job.InvokeMethod("GetErrorEx", ip, null))
            {
                if (op != null && (uint)op["ReturnValue"] != ReturnCode.Completed)
                    throw new ViridianException("GetErrorEx() call on the job failed!", new ManagementException());

                if (op == null)
                    return Array.Empty<string>();

                if (op["Errors"] is string[] == false)
                    return Array.Empty<string>();

                return (string[])op["Errors"];
            }
        }
    }
}
