using System;
using System.Management;
using System.Threading;
using Viridian.Exceptions;

namespace Viridian.Machine
{
    // TODO: tests that are missing for the methods below

    public class Job
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

        public void CreateVm(string serverName, string scopePath, string elementName, string virtualSystemSubtype)
        {
            var path = new ManagementPath() { Server = serverName, NamespacePath = @"\Root\Virtualization\V2", ClassName = "Msvm_VirtualSystemSettingData" };

            var scope = new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);

            using (var virtualSystemSettingClass = new ManagementClass(path) { Scope = scope })
            {
                var virtualSystemSetting = virtualSystemSettingClass.CreateInstance();

                if (virtualSystemSetting == null) 
                    throw new ViridianException("Failed creating virtual system setting class instance!");

                virtualSystemSetting["ElementName"] = elementName;
                virtualSystemSetting["ConfigurationDataRoot"] = @"C:\ProgramData\Microsoft\Windows\Hyper-V\";
                virtualSystemSetting["VirtualSystemSubtype"] = virtualSystemSubtype;

                var systemSettings = virtualSystemSetting.GetText(TextFormat.WmiDtd20);

                using (var virtualSystemManagementServiceCollection = new ManagementClass("Msvm_VirtualSystemManagementService") { Scope = scope })
                {
                    ManagementObject service = GetFirstObjectFromCollection(virtualSystemManagementServiceCollection.GetInstances());

                    using (var inParams = service.GetMethodParameters("DefineSystem"))
                    {
                        inParams["SystemSettings"] = systemSettings;

                        using (var outParams = service.InvokeMethod("DefineSystem", inParams, null))
                        {
                            ValidateOutput(outParams, scope);
                        }
                    }
                }
            }
        }

        public void RemoveVm(string serverName, string scopePath, string elementName)
        {
            var scope = new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);

            using (ManagementObject pvm = GetVirtualMachine(elementName, scope))
            {
                using (ManagementObject vmms = GetVirtualMachineManagementService(scope))
                {
                    using (ManagementBaseObject inParams = vmms.GetMethodParameters("DestroySystem"))
                    {
                        inParams["AffectedSystem"] = pvm.Path;

                        using (ManagementBaseObject outParams = vmms.InvokeMethod("DestroySystem", inParams, null))
                        {
                            ValidateOutput(outParams, scope);
                        }
                    }
                }
            }
        }

        #region Utilities

        public void ValidateOutput(ManagementBaseObject outputParameters, ManagementScope scope)
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
                        throw new ViridianException(errorMessage, errors, new ManagementException());

                    else if ((uint)outputParameters["ReturnValue"] != ReturnCode.Completed)
                    {
                        errorMessage = $"The method call failed! Error code {(uint)outputParameters["ReturnValue"]}";
                        throw new ViridianException(errorMessage, new ManagementException());
                    }
                }
            }
        }

        private bool IsJobComplete(object jobStateObj)
        {
            var jobState = (JobState)((ushort)jobStateObj);

            return (jobState == JobState.Completed) || (jobState == JobState.CompletedWithWarnings) || (jobState == JobState.Terminated) || (jobState == JobState.Exception) || (jobState == JobState.Killed);
        }

        private bool IsJobSuccessful(object jobStateObj)
        {
            var jobState = (JobState)((ushort)jobStateObj);

            return (jobState == JobState.Completed) || (jobState == JobState.CompletedWithWarnings);
        }

        private string[] GetMsvmErrorsList(ManagementObject job)
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

        private ManagementObject GetVMFirstObject(string name, string className, ManagementScope scope)
        {
            var vmQueryWql = $"SELECT * FROM {className} WHERE ElementName=\"{name}\"";

            var vmQuery = new SelectQuery(vmQueryWql);

            using (var vmSearcher = new ManagementObjectSearcher(scope, vmQuery))
            {
                var vmCollection = vmSearcher.Get();

                if (vmCollection.Count == 0)
                    throw new ViridianException($"No {className} could be found with name \"{name}\"", new ManagementException());
                
                foreach (ManagementObject instance in vmCollection)      
                    return instance;                
            }

            throw new ViridianException("Failed quering VMs with ManagementObjectSearcher!", new ManagementException());
        }

        public ManagementObject GetVirtualMachine(string name, ManagementScope scope) => GetVMFirstObject(name, "Msvm_ComputerSystem", scope);

        public ManagementScope GetScope(string serverName, string scopePath) => new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);
        
        public ManagementObject GetVirtualMachineManagementService(ManagementScope scope)
        {
            using (ManagementClass managementServiceClass = new ManagementClass("Msvm_VirtualSystemManagementService"))
            {
                managementServiceClass.Scope = scope;

                ManagementObject managementService = GetFirstObjectFromCollection(managementServiceClass.GetInstances());

                return managementService;
            }
        }

        public ManagementObject GetFirstObjectFromCollection(ManagementObjectCollection collection)
        {
            if (collection.Count == 0)
            {
                throw new ViridianException("The collection contains no objects!");
            }

            foreach (ManagementObject managementObject in collection)
            {
                return managementObject;
            }

            return null;
        }

        #endregion
    }
}
