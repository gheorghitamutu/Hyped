using System;
using System.Collections.Generic;
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

        public enum SnapshotType
        {
            Full = 2,
            Disk = 3,
            Recovery = 32768,
        }

        public enum RequestedState : uint
        {
            Other = 1,
            Running = 2,
            Off = 3,
            Saved = 6,
            Paused = 9,
            Starting = 10,
            Reset = 11,
            Saving = 32773,
            Pausing = 32776,
            Resuming = 32777,
            FastSaved = 32779,
            FastSaving = 32780,
        }

        public enum EnabledState : uint
        {
            Unknown = 0,
            Other = 1,
            Enabled = 2,
            Disabled = 3,
            ShuttingDown = 4,
            NotApplicable = 5,
            EnabledButOffline = 6,
            InTest = 7,
            Deferred = 8,
            Quiesce = 9,
            Starting = 10
        }

        public static class VirtualSystemTypeName
        {
            public const string RealizedVm = "Microsoft:Hyper-V:System:Realized";
            public const string PlannedVm = "Microsoft:Hyper-V:System:Planned";
            public const string RealizedSnapshot = "Microsoft:Hyper-V:Snapshot:Realized";
            public const string RecoverySnapshot = "Microsoft:Hyper-V:Snapshot:Recovery";
            public const string PlannedSnapshot = "Microsoft:Hyper-V:Snapshot:Planned";
            public const string MissingSnapshot = "Microsoft:Hyper-V:Snapshot:Missing";
            public const string ReplicaStandardRecoverySnapshot = "Microsoft:Hyper-V:Snapshot:Replica:Standard";
            public const string ReplicaApplicationConsistentRecoverySnapshot = "Microsoft:Hyper-V:Snapshot:Replica:ApplicationConsistent";
            public const string ReplicaPlannedRecoverySnapshot = "Microsoft:Hyper-V:Snapshot:Replica:PlannedFailover";
            public const string ReplicaSettings = "Microsoft:Hyper-V:Replica";
        }

        public void CreateVm(string serverName, string scopePath, string elementName, string virtualSystemSubtype)
        {
            var path = new ManagementPath() { Server = serverName, NamespacePath = @"\Root\Virtualization\V2", ClassName = "Msvm_VirtualSystemSettingData" };

            var scope = GetScope(serverName, scopePath);

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

        public ManagementObjectCollection GetVmCollection(string serverName, string scopePath)
        {
            var scope = GetScope(serverName, scopePath);

            var vmQueryWql = $"SELECT * FROM Msvm_ComputerSystem";

            var vmQuery = new SelectQuery(vmQueryWql);

            using (var vmSearcher = new ManagementObjectSearcher(scope, vmQuery))
            {
                return vmSearcher.Get();
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

        public void SetIncrementalBackup(string serverName, string scopePath, string vmName, bool status)
        {
            var scope = new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);

            using (var systemSettings = GetVirtualMachineSettings(vmName, scope))
            {
                using (var service = GetVirtualMachineManagementService(scope))
                { 
                    var isIncrementalBackupEnabled = (bool)systemSettings["IncrementalBackupEnabled"];

                    if (isIncrementalBackupEnabled == status)                    
                        return;                    

                    systemSettings["IncrementalBackupEnabled"] = !isIncrementalBackupEnabled;

                    using (var inParams = service.GetMethodParameters("ModifySystemSettings"))
                    {
                        inParams["SystemSettings"] = systemSettings.GetText(TextFormat.CimDtd20);

                        using (var outParams = service.InvokeMethod("ModifySystemSettings", inParams, null))
                        {
                            ValidateOutput(outParams, scope);
                        }
                    }
                }
            }
        }

        public bool GetIncrementalBackup(string serverName, string scopePath, string vmName)
        {
            var scope = new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);

            using (var systemSettings = GetVirtualMachineSettings(vmName, scope))
            {
                using (var service = GetVirtualMachineManagementService(scope))
                {
                    return (bool)systemSettings["IncrementalBackupEnabled"];
                }
            }
        }

        public void CreateSnapshot(string serverName, string scopePath, string vmName, SnapshotType snapshotType, bool saveMachineState)
        {
            if (snapshotType == SnapshotType.Recovery && saveMachineState)
                throw new ViridianException("You cannot create a recovery snapshot while the machine is in saved state!");

            var scope = new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);

            if (snapshotType == SnapshotType.Recovery) 
                SetIncrementalBackup(serverName, scopePath, vmName, true);
 
            using (var affectedSystem = GetVirtualMachine(vmName, scope))
            {
                using (var service = GetVirtualMachineSnapshotService(scope))
                {
                    using (var inParams = service.GetMethodParameters("CreateSnapshot"))
                    {
                        if (saveMachineState)
                            RequestStateChange(serverName, scopePath, vmName, RequestedState.Saved);

                        inParams["AffectedSystem"] = affectedSystem.Path.Path;

                        // Set the SnapshotSettings property. Backup/Recovery snapshots require special settings.
                        if (snapshotType == SnapshotType.Recovery)
                        {
                            // querying this class -> a collection that has just one element with ConsistencyLevel = 1
                            var snapshotSettings = GetServiceObject(scope, "Msvm_VirtualSystemSnapshotSettingData");
                            if (snapshotSettings != null)
                                inParams["SnapshotSettings"] = snapshotSettings.GetText(TextFormat.CimDtd20);

                            // also make sure you activate Volume Shadow Copy service on Guest and install KB3063109
                            // https://support.microsoft.com/en-us/help/3063109/hyper-v-integration-components-update-for-windows-virtual-machines
                            // https://thewincentral.com/how-to-install-cab-files-on-windows-10-for-cumulative-updates

                            // Time Synchronization The protocol version of the component installed in the virtual machine does not match the version expected by the hosting system
                            // https://support.microsoft.com/en-us/help/4014894/vm-integration-services-status-reports-protocol-version-mismatch-on-pr

                            // you cannot save actual ram state of the machine with backup/recovery checkpoints -> State Saved doesn't make sense then
                        }
                        else
                        {
                            inParams["SnapshotSettings"] = "";
                        }

                        inParams["SnapshotType"] = snapshotType;

                        using (var outParams = service.InvokeMethod("CreateSnapshot", inParams, null))
                        {
                            ValidateOutput(outParams, scope);

                            if (saveMachineState)
                                RequestStateChange(serverName, scopePath, vmName, RequestedState.Running);
                        }
                    }
                }
            }
        }

        public List<ManagementObject> GetSnapshotList(string serverName, string scopePath, string vmName, string virtualSystemTypeName)
        {
            var scope = GetScope(serverName, scopePath);

            using (var vm = GetVirtualMachine(vmName, scope))
            {
                using (var settingsCollection = vm.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SnapshotOfVirtualSystem", null, null, null, null, false, null))
                {
                    var queriedSnapshots = new List<ManagementObject>();

                    foreach (var settings in settingsCollection)
                    {
                        if (string.Compare((string)(((ManagementObject)settings)["VirtualSystemType"]), virtualSystemTypeName) != 0)
                            continue;

                        queriedSnapshots.Add((ManagementObject)settings);
                    }

                    return queriedSnapshots;
                }
            }
        }

        public void ApplySnapshot(string serverName, string scopePath, string vmName, string snapshotName)
        {
            var scope = GetScope(serverName, scopePath);

            using (var vm = GetVirtualMachine(vmName, scope))
            {
                using (var virtualSystemSnapshotService = GetVirtualMachineSnapshotService(scope))
                {
                    using (var snapshotCollection = vm.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SnapshotOfVirtualSystem", null, null, null, null, false, null))
                    {
                        ManagementObject snapshotToApply = null;

                        foreach (var snapshotObject in snapshotCollection)
                        {
                            var snapshot = (ManagementObject)snapshotObject;
                            
                            if (snapshot == null || !Equals(snapshot["ElementName"], snapshotName)) 
                                continue;

                            snapshotToApply = snapshot;
                            break;
                        }

                        if (snapshotToApply == null) 
                            throw new ViridianException("Snapshot not found!");

                        using (var inParams = virtualSystemSnapshotService.GetMethodParameters("ApplySnapshot"))
                        {
                            inParams["Snapshot"] = snapshotToApply;

                            // In order to apply a snapshot, the virtual machine must first be saved/off
                            if ((ushort)vm["EnabledState"] != (ushort)EnabledState.Disabled)
                            {
                                RequestStateChange(serverName, scopePath, vmName, RequestedState.Off);
                            }

                            using (var outParams = virtualSystemSnapshotService.InvokeMethod("ApplySnapshot", inParams, null))
                            {
                                ValidateOutput(outParams, scope);
                            }
                        }
                    }
                }
            }
        }

        public ManagementObject GetLastAppliedSnapshot(string serverName, string scopePath, string vmName)
        {
            var scope = GetScope(serverName, scopePath);

            using (var vm = GetVirtualMachine(vmName, scope))
            {
                using (var virtualSystemSnapshotService = GetVirtualMachineSnapshotService(scope))
                {
                    using (var lastAppliedSnapshotCollection = vm.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_LastAppliedSnapshot", null, null, null, null, false, null))
                    {
                        return GetFirstObjectFromCollection(lastAppliedSnapshotCollection);
                    }
                }
            }
        }

        public ManagementObject GetLastCreatedSnapshot(string serverName, string scopePath, string vmName)
        {
            var scope = GetScope(serverName, scopePath);

            using (var vm = GetVirtualMachine(vmName, scope))
            {
                using (var virtualSystemSnapshotService = GetVirtualMachineSnapshotService(scope))
                {
                    using (var snapshotCollection = vm.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SnapshotOfVirtualSystem", null, null, null, null, false, null))
                    {
                        var lastSnapshotApplied = GetFirstObjectFromCollection(snapshotCollection); 

                        foreach (var snapshot in snapshotCollection)
                        {
                            var datetimeSnapshot = ManagementDateTimeConverter.ToDateTime(snapshot["CreationTime"].ToString());
                            var dateTimeLastSnapshotApplied = ManagementDateTimeConverter.ToDateTime(lastSnapshotApplied["CreationTime"].ToString());

                            if (DateTime.Compare(dateTimeLastSnapshotApplied, datetimeSnapshot) < 0)
                                lastSnapshotApplied = (ManagementObject)snapshot;
                        }

                        return lastSnapshotApplied;
                    }
                }
            }
        }

        public void RequestStateChange(string serverName, string scopePath, string vmName, RequestedState requestedState)
        {
            var scope = new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);

            using (var managementService = GetVirtualMachineManagementService(scope))
            {
                using (var inParams = managementService.GetMethodParameters("RequestStateChange"))
                {
                    inParams["RequestedState"] = (uint)requestedState;

                    using (var virtualMachine = GetVirtualMachine(vmName, scope))
                    {
                        using (var outParams = virtualMachine.InvokeMethod("RequestStateChange", inParams, null))
                        {
                            ValidateOutput(outParams, scope);
                        }
                    }
                }
            }
        }

        public RequestedState GetCurrentState(string serverName, string scopePath, string vmName)
        {
            var scope = new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);

            using (var virtualMachine = GetVirtualMachine(vmName, scope))
            {
                return (RequestedState)Enum.ToObject(typeof(RequestedState), virtualMachine["EnabledState"]);
            }
        }

        #region Utilities

        private void ValidateOutput(ManagementBaseObject outputParameters, ManagementScope scope)
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
                return GetFirstObjectFromCollection(vmSearcher.Get());
            }
        }

        private ManagementObject GetVirtualMachine(string name, ManagementScope scope) => GetVMFirstObject(name, "Msvm_ComputerSystem", scope);

        private ManagementScope GetScope(string serverName, string scopePath) => new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);
        
        private ManagementObject GetVirtualMachineManagementService(ManagementScope scope)
        {
            using (ManagementClass managementServiceClass = new ManagementClass("Msvm_VirtualSystemManagementService"))
            {
                managementServiceClass.Scope = scope;

                ManagementObject managementService = GetFirstObjectFromCollection(managementServiceClass.GetInstances());

                return managementService;
            }
        }

        private ManagementObject GetFirstObjectFromCollection(ManagementObjectCollection collection)
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

        private ManagementObject GetVirtualMachineSettings(string vmName, ManagementScope scope)
        {
            using (var virtualMachine = GetVirtualMachine(vmName, scope))
            {
                using (var settingsCollection = virtualMachine.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null))
                {
                    return GetFirstObjectFromCollection(settingsCollection);
                }
            }
        }

        private ManagementObject GetVirtualMachineSnapshotService(ManagementScope scope)
        {
            using (var virtualSystemSnapshotServiceCollection = new ManagementClass("Msvm_VirtualSystemSnapshotService") { Scope = scope })
            {
                return GetFirstObjectFromCollection(virtualSystemSnapshotServiceCollection.GetInstances());
            }
        }

        private ManagementObject GetServiceObject(ManagementScope scope, string serviceName)
        {
            var wmiPath = new ManagementPath(serviceName);
            using (var serviceClass = new ManagementClass(scope, wmiPath, null))
            {
                return GetFirstObjectFromCollection(serviceClass.GetInstances());
            }
        }

        #endregion
    }
}
