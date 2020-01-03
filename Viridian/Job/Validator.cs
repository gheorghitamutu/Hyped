using System;
using System.Diagnostics;
using System.Management;
using System.Threading;

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
            public const uint NotSupportedVolume = 1;
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
            public const uint NotEnoughFreeSpace = 40000;
            public const uint InvalidPartitionParam = 41006;
            public const uint InvalidAccessPath = 41010;
            public const uint InvalidPartionType = 42007;
            public const uint InvalidClusterSize = 43000;
        }

        public static void ValidateOutput(ManagementBaseObject outputParameters, ManagementScope scope)
        {
            switch((uint)outputParameters?["ReturnValue"])
            {
                case ReturnCode.InvalidParameter:       throw new InvalidOperationException("Invalid parameter passed to function!");
                case ReturnCode.NotSupportedVolume:     throw new InvalidOperationException("Not Supported (MSFT_Volume)!");
                case ReturnCode.NotEnoughFreeSpace:     throw new InvalidOperationException("Not enough free space (MSFT_Partition)!");
                case ReturnCode.InvalidPartitionParam:  throw new InvalidOperationException("A parameter is not valid for this type of partition (MSFT_Partition)!");
                case ReturnCode.InvalidAccessPath:      throw new InvalidOperationException("The access path is not valid (MSFT_Partition)!");
                case ReturnCode.InvalidPartionType:     throw new InvalidOperationException("The partition type is not valid (MSFT_Partition)!");
                case ReturnCode.InvalidClusterSize:     throw new InvalidOperationException("The specified cluster size is invalid (MSFT_Volume)!");
            }

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
                        Trace.WriteLine(errors);
                        throw new InvalidOperationException(errorMessage);
                    }
                    else if ((uint)outputParameters["ReturnValue"] != ReturnCode.Completed)
                    {
                        errorMessage = $"The method call failed! Error code {(uint)outputParameters["ReturnValue"]}";
                        throw new InvalidOperationException(errorMessage);
                    }
                }
            }
        }

        public static bool IsJobEnded(object jobStateObj)
        {
            var jobState = (JobState)(ushort)jobStateObj;

            return
                jobState == JobState.Completed ||
                jobState == JobState.CompletedWithWarnings ||
                jobState == JobState.Terminated ||
                jobState == JobState.Exception ||
                jobState == JobState.Killed;
        }

        public static bool IsJobSuccessful(object jobStateObj)
        {
            var jobState = (JobState)(ushort)jobStateObj;

            return (jobState == JobState.Completed) || (jobState == JobState.CompletedWithWarnings);
        }

        public static string[] GetMsvmErrorsList(ManagementObject job)
        {
            using (var ip = job?.GetMethodParameters("GetErrorEx"))
            using (var op = job.InvokeMethod("GetErrorEx", ip, null))
            {
                if ((uint)op?["ReturnValue"] != ReturnCode.Completed)
                    throw new ManagementException("GetErrorEx() call on the job failed!");

                if (op == null)
                    return Array.Empty<string>();

                if (op["Errors"] is string[] == false)
                    return Array.Empty<string>();

                return (string[])op["Errors"];
            }
        }
    }
}
