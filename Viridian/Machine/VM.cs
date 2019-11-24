using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Resources.Network;
using Viridian.Service.Msvm;
using Viridian.Statistics;
using Viridian.Utilities;

namespace Viridian.Machine
{
    // TODO: tests that are missing for the methods below
    // TODO: add metrics control

    public class VM
    {
        public string ServerName { get; private set; }
        public string ScopePath { get; private set; }
        public ManagementScope Scope { get; private set; }
        public string VmName { get; private set; }
        public string VirtualSystemSubtype { get; private set; }

        public ManagementObject GetComputerSystemByName() => Utils.GetFirstObjectFromWqlQueryByClassAndName(VmName, "Msvm_ComputerSystem", Scope);

        public VM(string serverName, string scopePath, string elementName, string virtualSystemSubtype)
        {
            ServerName = serverName;
            ScopePath = scopePath;
            Scope = Utils.GetScope(serverName, scopePath);
            VmName = elementName;
            VirtualSystemSubtype = virtualSystemSubtype;
        }

        #region Enums

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

        public enum NetworkBootPreferredProtocol
        {
            IPv4 = 4096,
            IPv6 = 4097
        }

        #endregion

        #region Creation

        public void CreateVm()
        {
            var vssdPath = new ManagementPath()
            {
                Server = ServerName,
                NamespacePath = @"\Root\Virtualization\V2",
                ClassName = "Msvm_VirtualSystemSettingData"
            };

            using (var vssdClass = new ManagementClass(vssdPath) { Scope = Scope })
            {
                var vssd = vssdClass.CreateInstance();

                if (vssd == null)
                    throw new ViridianException("Failed creating virtual system setting class instance!");

                vssd["ElementName"] = VmName;
                vssd["ConfigurationDataRoot"] = @"C:\ProgramData\Microsoft\Windows\Hyper-V\";
                vssd["VirtualSystemSubtype"] = VirtualSystemSubtype;

                using (var vsmsCollection = new ManagementClass("Msvm_VirtualSystemManagementService") { Scope = Scope })
                using (var vsms = Utils.GetFirstObjectFromCollection(vsmsCollection.GetInstances()))
                using (var ip = vsms.GetMethodParameters("DefineSystem"))
                {
                    ip["SystemSettings"] = vssd.GetText(TextFormat.WmiDtd20);

                    using (var op = vsms.InvokeMethod("DefineSystem", ip, null))
                        Validator.ValidateOutput(op, Scope);
                }
            }
        }

        public void RemoveVm()
        {
            using (ManagementObject vm = GetComputerSystemByName())
            using (ManagementObject vmms = Utils.GetVirtualMachineManagementService(Scope))
            using (ManagementBaseObject ip = vmms.GetMethodParameters("DestroySystem"))
            {
                ip["AffectedSystem"] = vm.Path;

                using (ManagementBaseObject op = vmms.InvokeMethod("DestroySystem", ip, null))
                    Validator.ValidateOutput(op, Scope);
            }
        }

        #endregion

        #region Backup

        public void SetIncrementalBackup(bool incrementalBackupEnabled)
        {
            using (var vm = Utils.GetVirtualMachineSettings(VmName, Scope))
            using (var vmms = Utils.GetVirtualMachineManagementService(Scope))
            {
                if ((bool)vm["IncrementalBackupEnabled"] != incrementalBackupEnabled)
                {
                    vm["IncrementalBackupEnabled"] = incrementalBackupEnabled;

                    using (var ip = vmms.GetMethodParameters("ModifySystemSettings"))
                    {
                        ip["SystemSettings"] = vm.GetText(TextFormat.CimDtd20);

                        using (var op = vmms.InvokeMethod("ModifySystemSettings", ip, null))
                            Validator.ValidateOutput(op, Scope);
                    }
                }
            }
        }

        public bool GetIncrementalBackup()
        {
            using (var vms = Utils.GetVirtualMachineSettings(VmName, Scope))
                return (bool)vms["IncrementalBackupEnabled"];
        }

        #endregion

        #region Snapshots

        public void CreateSnapshot(SnapshotType snapshotType, bool saveMachineState)
        {
            if (snapshotType == SnapshotType.Recovery && saveMachineState)
                throw new ViridianException("You cannot create a recovery snapshot while the machine is in saved state!");

            if (snapshotType == SnapshotType.Recovery)
                SetIncrementalBackup(true);

            using (var vm = GetComputerSystemByName())
            {
                if (saveMachineState)
                    RequestStateChange(RequestedState.Saved);

                string snapshotSettings = "";

                // Set the SnapshotSettings property. Backup/Recovery snapshots require special settings.
                if (snapshotType == SnapshotType.Recovery)
                {
                    using (var vsssd = Utils.GetServiceObject(Scope, "Msvm_VirtualSystemSnapshotSettingData"))
                        snapshotSettings = vsssd.GetText(TextFormat.CimDtd20);                    

                    // Make sure you activate Volume Shadow Copy service on Guest and install KB3063109.
                    // https://support.microsoft.com/en-us/help/3063109/hyper-v-integration-components-update-for-windows-virtual-machines
                    // https://thewincentral.com/how-to-install-cab-files-on-windows-10-for-cumulative-updates

                    // Time Synchronization The protocol version of the component installed in the virtual machine does not match the version expected by the hosting system.
                    // https://support.microsoft.com/en-us/help/4014894/vm-integration-services-status-reports-protocol-version-mismatch-on-pr

                    // You cannot save actual ram state of the machine with backup/recovery checkpoints; State Saved doesn't make sense then.
                }

                VirtualSystemSnapshot.Instance.CreateSnapshot(vm.Path.Path, snapshotSettings, (ushort)snapshotType);

                if (saveMachineState)
                    RequestStateChange(RequestedState.Running);                
            }
        }

        public List<ManagementObject> GetSnapshotList(string virtualSystemType)
        {
            using (var vm = GetComputerSystemByName())
            using (var sofvsCollection = vm.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SnapshotOfVirtualSystem", null, null, null, null, false, null))
            {
                var queriedSnapshots = new List<ManagementObject>();

                foreach (ManagementObject settings in sofvsCollection)
                {
                    if (string.Compare(settings["VirtualSystemType"].ToString(), virtualSystemType) != 0)
                        continue;

                    queriedSnapshots.Add(settings);
                }

                return queriedSnapshots;
            }
        }

        public void ApplySnapshot(string snapshotName)
        {
            using (var vm = GetComputerSystemByName())
            using (var sofvsCollection = vm.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SnapshotOfVirtualSystem", null, null, null, null, false, null))
            {
                foreach (ManagementObject snapshot in sofvsCollection)
                {
                    if (snapshot == null || !Equals(snapshot["ElementName"], snapshotName))
                        continue;

                    // In order to apply a snapshot, the virtual machine must first be saved/off
                    if ((ushort)vm["EnabledState"] != (ushort)EnabledState.Disabled)
                        RequestStateChange(RequestedState.Off);

                    VirtualSystemSnapshot.Instance.ApplySnapshot(snapshot);

                    return;
                }

                throw new ViridianException("Snapshot not found!");
            }
        }

        public ManagementObject GetLastAppliedSnapshot()
        {
            using (var vm = GetComputerSystemByName())
            using (var lasCollection = vm.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_LastAppliedSnapshot", null, null, null, null, false, null))
                return Utils.GetFirstObjectFromCollection(lasCollection);
        }

        public ManagementObject GetLastCreatedSnapshot()
        {
            using (var vm = GetComputerSystemByName())
            using (var sovfsCollection = vm.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SnapshotOfVirtualSystem", null, null, null, null, false, null))
            {
                var lastSnapshotApplied = Utils.GetFirstObjectFromCollection(sovfsCollection);

                foreach (var snapshot in sovfsCollection)
                {
                    var dtSnapshot = ManagementDateTimeConverter.ToDateTime(snapshot["CreationTime"].ToString());
                    var dtLastSnapshotApplied = ManagementDateTimeConverter.ToDateTime(lastSnapshotApplied["CreationTime"].ToString());

                    if (DateTime.Compare(dtLastSnapshotApplied, dtSnapshot) < 0)
                        lastSnapshotApplied = (ManagementObject)snapshot;
                }

                return lastSnapshotApplied;
            }
        }

        #endregion

        #region State

        public void RequestStateChange(RequestedState requestedState)
        {
            using (var vmms = Utils.GetVirtualMachineManagementService(Scope))
            using (var ip = vmms.GetMethodParameters("RequestStateChange"))
            {
                ip["RequestedState"] = (uint)requestedState;

                using (var vm = GetComputerSystemByName())
                using (var op = vm.InvokeMethod("RequestStateChange", ip, null))
                    Validator.ValidateOutput(op, Scope);
            }
        }

        public RequestedState GetCurrentState()
        {
            using (var vm = GetComputerSystemByName())
                return (RequestedState)Enum.ToObject(typeof(RequestedState), vm["EnabledState"]);
        }

        #endregion

        #region Boot

        public string[] GetBootSourceOrderedList()
        {
            using (var vms = Utils.GetVirtualMachineSettings(VmName, Scope))
                return (string[])vms["BootSourceOrder"];
        }

        public void SetBootOrderFromDevicePath(string devicePath)
        {
            using (var vms = Utils.GetVirtualMachineSettings(VmName, Scope))
            {
                if (vms["BootSourceOrder"] is string[] prevBootOrder)
                {
                    var bso = new string[prevBootOrder.Length];

                    var index = 1;
                    foreach (var bs in prevBootOrder)
                        using (var entry = new ManagementObject(new ManagementPath(bs)))
                        {
                            var fdp = entry["FirmwareDevicePath"].ToString();

                            if (string.Equals(devicePath, fdp, StringComparison.OrdinalIgnoreCase))
                                bso[0] = bs;
                            else
                                bso[index++] = bs;
                        }

                    vms["BootSourceOrder"] = bso;
                }

                using (var service = Utils.GetVirtualMachineManagementService(Scope))
                using (var ip = service.GetMethodParameters("ModifySystemSettings"))
                {
                    ip["SystemSettings"] = vms.GetText(TextFormat.WmiDtd20);

                    using (var op = service.InvokeMethod("ModifySystemSettings", ip, null))
                        Validator.ValidateOutput(op, Scope);
                }
            }
        }

        public void SetBootOrderByIndex(uint[] bootSourceOrder)
        {
            if (bootSourceOrder == null)
                throw new ViridianException("Boot Sources Array is null!");

            using (var vms = Utils.GetVirtualMachineSettings(VmName, Scope))
            {
                var previousBso = vms["BootSourceOrder"] as string[];

                if (previousBso != null && bootSourceOrder.Length > previousBso.Length)
                    throw new ViridianException("Too many boot devices specified!");

                if (bootSourceOrder.Any(indexBso => previousBso != null && indexBso > previousBso.Length))
                    throw new ViridianException("Invalid boot device index specified!");

                if (previousBso != null)
                {
                    var newBso = new string[previousBso.Length];

                    uint countReorderedBootSources = 0;

                    foreach (var i in bootSourceOrder)
                        newBso[countReorderedBootSources++] = previousBso[i];

                    for (uint i = 0; i < previousBso.Length; i++)
                    {
                        var isReordered = bootSourceOrder.Any(reorderedIndex => i == reorderedIndex);

                        if (!isReordered)
                            newBso[countReorderedBootSources++] = previousBso[i];
                    }

                    vms["BootSourceOrder"] = newBso;
                }

                using (var vmms = Utils.GetVirtualMachineManagementService(Scope))
                using (var ip = vmms.GetMethodParameters("ModifySystemSettings"))
                {
                    ip["SystemSettings"] = vms.GetText(TextFormat.WmiDtd20);

                    using (var op = vmms.InvokeMethod("ModifySystemSettings", ip, null))
                        Validator.ValidateOutput(op, Scope);
                }
            }
        }

        public NetworkBootPreferredProtocol GetNetworkBootPreferredProtocol()
        {
            using (var vms = Utils.GetVirtualMachineSettings(VmName, Scope))
                return (NetworkBootPreferredProtocol)Enum.ToObject(typeof(NetworkBootPreferredProtocol), vms["NetworkBootPreferredProtocol"]);
        }

        public void SetNetworkBootPreferredProtocol(NetworkBootPreferredProtocol networkBootPreferredProtocol)
        {
            using (var vms = Utils.GetVirtualMachineSettings(VmName, Scope))
            {
                vms["NetworkBootPreferredProtocol"] = networkBootPreferredProtocol;

                using (var vmms = Utils.GetVirtualMachineManagementService(Scope))
                using (var ip = vmms.GetMethodParameters("ModifySystemSettings"))
                {
                    ip["SystemSettings"] = vms.GetText(TextFormat.WmiDtd20);

                    using (var op = vmms.InvokeMethod("ModifySystemSettings", ip, null))
                        Validator.ValidateOutput(op, Scope);
                }
            }
        }

        public bool GetPauseAfterBootFailure()
        {
            using (var vms = Utils.GetVirtualMachineSettings(VmName, Scope))
                return (bool)vms["PauseAfterBootFailure"];
        }

        public void SetPauseAfterBootFailure(bool pauseAfterBootFailure)
        {
            using (var vms = Utils.GetVirtualMachineSettings(VmName, Scope))
            {
                vms["PauseAfterBootFailure"] = pauseAfterBootFailure;

                using (var vmms = Utils.GetVirtualMachineManagementService(Scope))
                using (var ip = vmms.GetMethodParameters("ModifySystemSettings"))
                {
                    ip["SystemSettings"] = vms.GetText(TextFormat.WmiDtd20);

                    using (var op = vmms.InvokeMethod("ModifySystemSettings", ip, null))
                        Validator.ValidateOutput(op, Scope);
                }
            }
        }

        public bool GetSecureBoot()
        {
            using (var vms = Utils.GetVirtualMachineSettings(VmName, Scope))
                return (bool)vms["SecureBootEnabled"];
        }

        public void SetSecureBoot(bool secureBootEnabled)
        {
            using (var vms = Utils.GetVirtualMachineSettings(VmName, Scope))
            {
                vms["SecureBootEnabled"] = secureBootEnabled;

                using (var vmms = Utils.GetVirtualMachineManagementService(Scope))
                using (var ip = vmms.GetMethodParameters("ModifySystemSettings"))
                {
                    ip["SystemSettings"] = vms.GetText(TextFormat.WmiDtd20);

                    using (var op = vmms.InvokeMethod("ModifySystemSettings", ip, null))
                        Validator.ValidateOutput(op, Scope);
                }
            }
        }

        #endregion

        #region Controllers

        public ManagementObject GetScsiController(int index)
        {
            var rt = Utils.GetResourceType("ScsiHBA");
            var rst = Utils.GetResourceSubType("ScsiHBA");
            var scsiControllers = Utils.GetResourceAllocationSettingDataResourcesByTypeAndSubtype(VmName, Scope, rt, rst);

            if (scsiControllers.Count < index)
                throw new ViridianException("Invalid SCSI controller index specified!");

            return scsiControllers[index];
        }

        #endregion

        #region Stats

        public ManagementObject GetMemorySettingData()
        {
            using (var vm = GetComputerSystemByName())
            using (var vssd = Utils.GetFirstObjectFromCollection(vm.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SettingsDefineState", null, null, "SettingData", "ManagedElement", false, null)))
            using (var memoryCollection = vssd.GetRelated("Msvm_MemorySettingData ", null, null, null, null, null, false, null))
                return Utils.GetFirstObjectFromCollection(memoryCollection);
        }

        public ManagementBaseObject[] GetSummaryInformation()
        {
            using (var vsms = Utils.GetVirtualMachineManagementService(Scope))
            using (var vms = Utils.GetVirtualMachineSettings(VmName, Scope))
            using (var ip = vsms.GetMethodParameters("GetSummaryInformation"))
            {
                var requestedInformation = new uint[]
                {
                    0,      // Name
                    1,      // ElementName
                    2,      // CreationTime
                    3,      // Notes
                    4,      // NumberOfProcessors
                    5,      // ThumbnailImage
                    6,      // ThumbnailImageHeight
                    7,      // ThumbnailImageWidth
                    8,      // AllocatedGPU
                    9,      // VirtualSwitchNames 
                    10,     // Version | Added in Windows 10 and Windows Server 2016.
                    11,     // Shielded | Added in Windows 10, version 1703 and Windows Server 2016.
                    100,    // EnabledState
                    101,    // ProcessorLoad
                    102,    // ProcessorLoadHistory
                    103,    // MemoryUsage
                    104,    // Heartbeat
                    105,    // UpTime
                    106,    // GuestOperatingSystem
                    107,    // Snapshots
                    108,    // AsynchronousTasks
                    109,    // HealthState
                    110,    // OperationalStatus
                    111,    // StatusDescriptions
                    112,    // MemoryAvailable
                    113,    // AvailableMemoryBuffer
                    114,    // Replication Mode
                    115,    // Replication State
                    116,    // Replication HealthTest Replica System
                    117,    // Application Health 
                    118,    // ReplicationStateEx 
                    119,    // ReplicationHealthEx 
                    120,    // SwapFilesInUse 
                    121,    // IntegrationServicesVersionState 
                    122,    // ReplicationProviderId 
                    123     // MemorySpansPhysicalNumaNodes 
                };

                ip["SettingData"] = new[] { vms };
                ip["RequestedInformation"] = requestedInformation;

                using (var op = vsms.InvokeMethod("GetSummaryInformation", ip, null))
                {
                    Validator.ValidateOutput(op, Scope);

                    return (ManagementBaseObject[])op["SummaryInformation"];
                }
            }

        }

        #endregion

        #region Network

        public void ConnectVmToSwitch(string switchName, string adapterName)
        {
            using (var vmms = Utils.GetVirtualMachineManagementService(Scope))
            using (var ves = NetSwitch.FindVirtualEthernetSwitch(Scope, switchName))
            using (var vms = Utils.GetVirtualMachineSettings(VmName, Scope))
            using (var syntheticAdapter = SyntheticEthernetAdapter.AddSyntheticAdapter(this, adapterName))
            using (var epasd = NetSwitch.GetDefaultEthernetPortAllocationSettingData(Scope))
            {
                epasd["Parent"] = syntheticAdapter.Path.Path;
                epasd["HostResource"] = new string[] { ves.Path.Path };

                using (var ip = vmms.GetMethodParameters("AddResourceSettings"))
                {
                    ip["AffectedConfiguration"] = vms.Path.Path;
                    ip["ResourceSettings"] = new string[] { epasd.GetText(TextFormat.WmiDtd20) };

                    using (var op = vmms.InvokeMethod("AddResourceSettings", ip, null))
                        Validator.ValidateOutput(op, Scope);
                }
            }
        }

        public void DisconnectVmFromSwitch(string switchName)
        {
            using (var vmms = Utils.GetVirtualMachineManagementService(Scope))
            using (var ves = NetSwitch.FindVirtualEthernetSwitch(Scope, switchName))
            using (var vm = GetComputerSystemByName())
            {
                IList<ManagementObject> connectionsToSwitch = NetSwitch.FindConnectionsToSwitch(vm, ves);

                foreach (var connection in connectionsToSwitch)
                {
                    connection["EnabledState"] = 3;

                    using (var ip = vmms.GetMethodParameters("ModifyResourceSettings"))
                    {
                        ip["ResourceSettings"] = new string[] { connection.GetText(TextFormat.WmiDtd20) };

                        using (var op = vmms.InvokeMethod("ModifyResourceSettings", ip, null))
                            Validator.ValidateOutput(op, Scope);
                    }
                }
            }
        }

        public void ModifyConnection(string currentSwitchName, string newSwitchName)
        {
            using (var vmms = Utils.GetVirtualMachineManagementService(Scope))
            using (var ves = NetSwitch.FindVirtualEthernetSwitch(Scope, currentSwitchName))
            using (var newVes = NetSwitch.FindVirtualEthernetSwitch(Scope, newSwitchName))
            using (var virtualMachine = GetComputerSystemByName())
            {
                IList<ManagementObject> currentConnections = NetSwitch.FindConnectionsToSwitch(virtualMachine, ves);

                foreach (var connection in currentConnections)
                {
                    connection["HostResource"] = new string[] { newVes.Path.Path };

                    using (var ip = vmms.GetMethodParameters("ModifyResourceSettings"))
                    {
                        ip["ResourceSettings"] = new string[] { connection.GetText(TextFormat.WmiDtd20) };

                        using (var op = vmms.InvokeMethod("ModifyResourceSettings", ip, null))
                            Validator.ValidateOutput(op, Scope);
                    }
                }
            }
        }

        public ManagementObjectCollection GetSyntheticAdapterCollection()
        {
            return GetComputerSystemByName().GetRelated("Msvm_SyntheticEthernetPort");
        }

        public List<ManagementObject> GetEthernetSwitchPortAclSettingDatas()
        {
            var list = new List<ManagementObject>();

            using (var vm = GetComputerSystemByName())
            using (var vms = Utils.GetVirtualMachineSettings(vm))
                foreach (ManagementObject sepsd in vms.GetRelated("Msvm_SyntheticEthernetPortSettingData"))
                    using (var epasd = SyntheticEthernetAdapter.GetEthernetPortAllocationSettingData(sepsd, Scope))
                        foreach(ManagementObject espasd in SyntheticEthernetAdapter.GetEthernetSwitchPortAclSettingData(epasd))
                            list.Add(espasd);

            return list;
        }

        #endregion

        #region Metrics

        public List<ManagementObject> GetAllAggregationMetricDefinitions()
        {
            var definitionsList = new List<ManagementObject>();

            foreach (var def in Utils.AggregationMetricDefinitionCaptions)
                using (var definition = Metrics.GetBaseMetricDefForMEByName(GetComputerSystemByName(), def))
                    if (definition != null)
                        definitionsList.Add(definition);

            return definitionsList;
        }

        public void SetAggregationMetricsForDrives(Utils.MetricOperation operation)
        {
            using (var vm = GetComputerSystemByName())
            {
                var rt = Utils.GetResourceType("ScsiHBA");
                var rst = Utils.GetResourceSubType("ScsiHBA");
                var scsiControllers = Utils.GetResourceAllocationSettingDataResourcesByTypeAndSubtype(VmName, Scope, rt, rst);

                foreach (var scsiController in scsiControllers)
                    using (var driveCollection = scsiController.GetRelated("Msvm_ResourceAllocationSettingData", null, null, null, "Dependent", "Antecedent", false, null))
                    foreach (ManagementObject drive in driveCollection)
                            Metrics.SetAllMetrics(drive, operation);
            }
        }

        public void SetBaseMetricsForEthernetSwitchPortAclSettingData(Utils.MetricOperation operation)
        {
            using (var vm = GetComputerSystemByName())
            using (var vms = Utils.GetVirtualMachineSettings(vm))
            foreach (ManagementObject sepsd in vms.GetRelated("Msvm_SyntheticEthernetPortSettingData"))
                using (var epasd = SyntheticEthernetAdapter.GetEthernetPortAllocationSettingData(sepsd, Scope))
                using (var espasdCollection = SyntheticEthernetAdapter.GetEthernetSwitchPortAclSettingData(epasd))
                        foreach (ManagementObject espasd in espasdCollection)
                            foreach (ManagementObject baseMetricDef in Metrics.GetAllBaseMetricDefinitions(espasd))
                                Metrics.SetBaseMetric(espasd, baseMetricDef, operation);
        }

        public void SetAggregationMetricsForEthernetSwitchPortAclSettingData(Utils.MetricOperation operation)
        {
            using (var vm = GetComputerSystemByName())
            using (var vms = Utils.GetVirtualMachineSettings(vm))
                foreach (ManagementObject sepsd in vms.GetRelated("Msvm_SyntheticEthernetPortSettingData"))
                    using (var epasd = SyntheticEthernetAdapter.GetEthernetPortAllocationSettingData(sepsd, Scope))
                    using (var espasdCollection = SyntheticEthernetAdapter.GetEthernetSwitchPortAclSettingData(epasd))
                        foreach (ManagementObject espasd in espasdCollection)
                                Metrics.SetAllMetrics(espasd, operation);
        }

        #endregion
    }
}
