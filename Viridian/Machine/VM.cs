using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Resources.Msvm;
using Viridian.Resources.Network;
using Viridian.Service.Msvm;
using Viridian.Statistics;
using Viridian.Utilities;

namespace Viridian.Machine
{
    public class VM
    {
        public string ServerName { get; private set; }
        public string ScopePath { get; private set; }
        public ManagementScope Scope { get; private set; }
        public string VmName { get; private set; }
        public string VirtualSystemSubtype { get; private set; }

        public ManagementObject GetComputerSystemByName()
        {
            using (var mos = new ManagementObjectSearcher(Scope, new ObjectQuery("SELECT * FROM Msvm_ComputerSystem")))
                return mos
                    .Get()
                    .Cast<ManagementObject>()
                    .Where((c) => c["ElementName"]?.ToString() == VmName)
                    .First();
        }

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
            using (var vssd = Utils.GetServiceObject(Scope, "Msvm_VirtualSystemSettingData"))
            { 
                vssd["ElementName"] = VmName;
                vssd["ConfigurationDataRoot"] = @"C:\ProgramData\Microsoft\Windows\Hyper-V\";
                vssd["VirtualSystemSubtype"] = VirtualSystemSubtype;

                VirtualSystemManagement.Instance.DefineSystem(vssd.GetText(TextFormat.WmiDtd20), null, null);
            }
        }

        public void RemoveVm()
        {
            using (var vm = GetComputerSystemByName())
                VirtualSystemManagement.Instance.DestroySystem(vm);
        }

        #endregion

        #region Backup

        public void SetIncrementalBackup(bool incrementalBackupEnabled)
        {
            using (var vm = GetVirtualMachineSettings(VmName, Scope))
            {
                if ((bool)vm["IncrementalBackupEnabled"] != incrementalBackupEnabled)
                {
                    vm["IncrementalBackupEnabled"] = incrementalBackupEnabled;

                    VirtualSystemManagement.Instance.ModifySystemSettings(vm.GetText(TextFormat.CimDtd20));
                }
            }
        }

        public bool GetIncrementalBackup()
        {
            using (var vms = GetVirtualMachineSettings(VmName, Scope))
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
                    RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Saved);

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
                    RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Running);
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
                        RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Off);

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
                return lasCollection.Cast<ManagementObject>().First();
        }

        public ManagementObject GetLastCreatedSnapshot()
        {
            using (var vm = GetComputerSystemByName())
            using (var sovfsCollection = vm.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SnapshotOfVirtualSystem", null, null, null, null, false, null))
            {
                var lastSnapshotApplied = sovfsCollection.Cast<ManagementObject>().First();

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

        public void RequestStateChange(VirtualSystemManagement.RequestedStateVSM RequestedState, ulong TimeoutPeriod = 0)
        {
            using (var vm = GetComputerSystemByName())
            using (var ip = vm.GetMethodParameters(nameof(RequestStateChange)))
            {
                ip[nameof(RequestedState)] = (uint)RequestedState;
                ip[nameof(TimeoutPeriod)] = null; // CIM_DateTime

                using (var op = vm.InvokeMethod(nameof(RequestStateChange), ip, null))
                    Validator.ValidateOutput(op, vm.Scope);
            }
        }

        public VirtualSystemManagement.RequestedStateVSM GetCurrentState()
        {
            using (var vm = GetComputerSystemByName())
                return (VirtualSystemManagement.RequestedStateVSM)Enum.ToObject(typeof(VirtualSystemManagement.RequestedStateVSM), vm["EnabledState"]);
        }

        #endregion

        #region Boot

        public string[] GetBootSourceOrderedList()
        {
            using (var vms = GetVirtualMachineSettings(VmName, Scope))
                return (string[])vms["BootSourceOrder"];
        }

        public void SetBootOrderFromDevicePath(string devicePath)
        {
            using (var vms = GetVirtualMachineSettings(VmName, Scope))
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

                VirtualSystemManagement.Instance.ModifySystemSettings(vms.GetText(TextFormat.WmiDtd20));
            }
        }

        public void SetBootOrderByIndex(uint[] bootSourceOrder)
        {
            if (bootSourceOrder == null)
                throw new ViridianException("Boot Sources Array is null!");

            using (var vms = GetVirtualMachineSettings(VmName, Scope))
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

                VirtualSystemManagement.Instance.ModifySystemSettings(vms.GetText(TextFormat.WmiDtd20));
            }
        }

        public NetworkBootPreferredProtocol GetNetworkBootPreferredProtocol()
        {
            using (var vms = GetVirtualMachineSettings(VmName, Scope))
                return (NetworkBootPreferredProtocol)Enum.ToObject(typeof(NetworkBootPreferredProtocol), vms["NetworkBootPreferredProtocol"]);
        }

        public void SetNetworkBootPreferredProtocol(NetworkBootPreferredProtocol networkBootPreferredProtocol)
        {
            using (var vms = GetVirtualMachineSettings(VmName, Scope))
            {
                vms["NetworkBootPreferredProtocol"] = networkBootPreferredProtocol;

                VirtualSystemManagement.Instance.ModifySystemSettings(vms.GetText(TextFormat.WmiDtd20));
            }
        }

        public bool GetPauseAfterBootFailure()
        {
            using (var vms = GetVirtualMachineSettings(VmName, Scope))
                return (bool)vms["PauseAfterBootFailure"];
        }

        public void SetPauseAfterBootFailure(bool pauseAfterBootFailure)
        {
            using (var vms = GetVirtualMachineSettings(VmName, Scope))
            {
                vms["PauseAfterBootFailure"] = pauseAfterBootFailure;

                VirtualSystemManagement.Instance.ModifySystemSettings(vms.GetText(TextFormat.WmiDtd20));
            }
        }

        public bool GetSecureBoot()
        {
            using (var vms = GetVirtualMachineSettings(VmName, Scope))
                return (bool)vms["SecureBootEnabled"];
        }

        public void SetSecureBoot(bool secureBootEnabled)
        {
            using (var vms = GetVirtualMachineSettings(VmName, Scope))
            {
                vms["SecureBootEnabled"] = secureBootEnabled;

                VirtualSystemManagement.Instance.ModifySystemSettings(vms.GetText(TextFormat.WmiDtd20));
            }
        }

        #endregion

        #region Controllers

        public ManagementObject GetScsiController(int index)
        {
            var scsiControllers = GetResourceAllocationSettingData(ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceType, ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceSubType);

            if (scsiControllers.Count < index)
                throw new ViridianException("Invalid SCSI controller index specified!");

            return scsiControllers[index];
        }

        #endregion

        #region Stats

        public ManagementObject GetMemorySettingData()
        {
            using (var vm = GetComputerSystemByName())
            using (var vssd = vm.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SettingsDefineState", null, null, "SettingData", "ManagedElement", false, null).Cast<ManagementObject>().First())
            using (var memoryCollection = vssd.GetRelated("Msvm_MemorySettingData"))
                return memoryCollection.Cast<ManagementObject>().First();
        }

        public ManagementBaseObject[] GetSummaryInformation()
        {
            using (var vms = GetVirtualMachineSettings(VmName, Scope))
            {
                var requestedInformation = new int[]
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

                return VirtualSystemManagement.Instance.GetSummaryInformation(new[] { vms }, requestedInformation);
            }

        }

        #endregion

        #region Network

        public void ConnectVmToSwitch(string switchName, string adapterName)
        {
            using (var ves = NetSwitch.FindVirtualEthernetSwitch(Scope, switchName))
            using (var vms = GetVirtualMachineSettings(VmName, Scope))
            using (var syntheticAdapter = SyntheticEthernetAdapter.AddSyntheticAdapter(this, adapterName))
            using (var epasd = NetSwitch.GetDefaultEthernetPortAllocationSettingData())
            {
                epasd["Parent"] = syntheticAdapter.Path.Path;
                epasd["HostResource"] = new string[] { ves.Path.Path };

                VirtualSystemManagement.Instance.AddResourceSettings(vms, new string[] { epasd.GetText(TextFormat.WmiDtd20) });
            }
        }

        public void DisconnectVmFromSwitch(string switchName)
        {
            using (var ves = NetSwitch.FindVirtualEthernetSwitch(Scope, switchName))
            using (var vm = GetComputerSystemByName())
            {
                IList<ManagementObject> connectionsToSwitch = NetSwitch.FindConnectionsToSwitch(vm, ves);

                foreach (var connection in connectionsToSwitch)
                {
                    connection["EnabledState"] = 3;

                    VirtualSystemManagement.Instance.ModifyResourceSettings(new string[] { connection.GetText(TextFormat.WmiDtd20) });
                }
            }
        }

        public void ModifyConnection(string currentSwitchName, string newSwitchName)
        {
            using (var ves = NetSwitch.FindVirtualEthernetSwitch(Scope, currentSwitchName))
            using (var newVes = NetSwitch.FindVirtualEthernetSwitch(Scope, newSwitchName))
            using (var virtualMachine = GetComputerSystemByName())
            {
                IList<ManagementObject> currentConnections = NetSwitch.FindConnectionsToSwitch(virtualMachine, ves);

                foreach (var connection in currentConnections)
                {
                    connection["HostResource"] = new string[] { newVes.Path.Path };

                    VirtualSystemManagement.Instance.ModifyResourceSettings(new string[] { connection.GetText(TextFormat.WmiDtd20) });
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
            using (var vms = GetVirtualMachineSettings(vm))
                foreach (ManagementObject sepsd in vms.GetRelated("Msvm_SyntheticEthernetPortSettingData"))
                    using (var epasd = SyntheticEthernetAdapter.GetEthernetPortAllocationSettingData(sepsd, Scope))
                        foreach(ManagementObject espasd in SyntheticEthernetAdapter.GetEthernetSwitchPortAclSettingData(epasd))
                            list.Add(espasd);

            return list;
        }

        #endregion

        #region Metrics

        public void SetAggregationMetricsForDrives(Metric.MetricCollectionEnabled operation)
        {
            using (var vm = GetComputerSystemByName())
            {
                var scsiControllers = GetResourceAllocationSettingData(ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceType, ResourcePool.ResourceTypeInfo.SyntheticSCSIController.ResourceSubType);

                foreach (var scsiController in scsiControllers)
                    using (var driveCollection = scsiController.GetRelated("Msvm_ResourceAllocationSettingData", null, null, null, "Dependent", "Antecedent", false, null))
                    foreach (ManagementObject drive in driveCollection)
                            Metric.Instance.SetAllMetrics(drive, operation);
            }
        }

        public void SetBaseMetricsForEthernetSwitchPortAclSettingData(Metric.MetricCollectionEnabled operation)
        {
            using (var vm = GetComputerSystemByName())
            using (var vms = GetVirtualMachineSettings(vm))
            foreach (ManagementObject sepsd in vms.GetRelated("Msvm_SyntheticEthernetPortSettingData"))
                using (var epasd = SyntheticEthernetAdapter.GetEthernetPortAllocationSettingData(sepsd, Scope))
                using (var espasdCollection = SyntheticEthernetAdapter.GetEthernetSwitchPortAclSettingData(epasd))
                        foreach (ManagementObject espasd in espasdCollection)
                            foreach (ManagementObject baseMetricDef in Metric.GetAllBaseMetricDefinitions(espasd))
                                Metric.Instance.SetBaseMetric(espasd, baseMetricDef, operation);
        }

        public void SetAggregationMetricsForEthernetSwitchPortAclSettingData(Metric.MetricCollectionEnabled operation)
        {
            using (var vm = GetComputerSystemByName())
            using (var vms = GetVirtualMachineSettings(vm))
                foreach (ManagementObject sepsd in vms.GetRelated("Msvm_SyntheticEthernetPortSettingData"))
                    using (var epasd = SyntheticEthernetAdapter.GetEthernetPortAllocationSettingData(sepsd, Scope))
                    using (var espasdCollection = SyntheticEthernetAdapter.GetEthernetSwitchPortAclSettingData(epasd))
                        foreach (ManagementObject espasd in espasdCollection)
                                Metric.Instance.SetAllMetrics(espasd, operation);
        }

        public List<ManagementObject> GetResourceAllocationSettingData(string ResourceType, string ResourceSubType)
        {
            using (var vm = GetComputerSystemByName())
            using (var vssdCollection = vm.GetRelated("Msvm_VirtualSystemSettingData"))
            {
                var settings = vssdCollection.Cast<ManagementObject>().First();

                using (var rasdCollection = settings.GetRelated("Msvm_ResourceAllocationSettingData"))
                    return rasdCollection
                        .Cast<ManagementObject>()
                        .Where((c) =>
                            c[nameof(ResourceType)]?.ToString() == ResourceType &&
                            c[nameof(ResourceSubType)]?.ToString() == ResourceSubType)
                        .ToList();
            }
        }

        public static ManagementObject GetVirtualMachine(string ElementName, ManagementScope scope)
        {
            using (var mos = new ManagementObjectSearcher(scope, new ObjectQuery("SELECT * FROM Msvm_ComputerSystem")))
                return mos.Get().Cast<ManagementObject>().Where((c) => c[nameof(ElementName)]?.ToString() == ElementName).First();
        }

        public static ManagementObject GetVirtualMachineSettings(string vmName, ManagementScope scope)
        {
            using (var vm = GetVirtualMachine(vmName, scope))
                return GetVirtualMachineSettings(vm);
        }

        public static ManagementObject GetVirtualMachineSettings(ManagementObject virtualMachine)
        {
            if (virtualMachine is null)
                throw new ViridianException("", new ArgumentNullException(nameof(virtualMachine)));

            using (var vssd = virtualMachine.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null))
                return vssd.Cast<ManagementObject>().First();
        }

        #endregion
    }
}
