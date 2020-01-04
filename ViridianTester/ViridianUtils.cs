using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using Viridian.Root.Virtualization.v2.Msvm.Integration;
using Viridian.Root.Virtualization.v2.Msvm.Memory;
using Viridian.Root.Virtualization.v2.Msvm.Metrics;
using Viridian.Root.Virtualization.v2.Msvm.Networking;
using Viridian.Root.Virtualization.v2.Msvm.Processor;
using Viridian.Root.Virtualization.v2.Msvm.ResourceManagement;
using Viridian.Root.Virtualization.v2.Msvm.Storage;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;

namespace ViridianTester
{
    public class ViridianUtils : IDisposable
    {
        public string VSSD_ConfigurationDataRoot { get; private set; } = @"ConfigurationDataRoot";
        public string VSSD_VirtualSystemSubtype { get; private set; } = "Microsoft:Hyper-V:SubType:2";
        public string VSSS_SnapshotDataRoot { get; private set; } = @"SnapshotDataRoot";

        public VirtualSystemManagementService VSMS { get; private set; }
        public ImageManagementService IMS { get; private set; }
        public VirtualSystemSnapshotService VSSS { get; private set; }
        public MetricService MS { get; private set; }
        public VirtualEthernetSwitchManagementService VESMS { get; private set; }

       public  List<ManagementPath> VSSD_ToDestroy { get; private set; } = new List<ManagementPath>();
       public  List<ManagementPath> VESSD_ToDestroy { get; private set; } = new List<ManagementPath>();

        public ViridianUtils()
        {
            VSMS = VirtualSystemManagementService.GetInstances().First();
            IMS = ImageManagementService.GetInstances().First();
            VSSS = VirtualSystemSnapshotService.GetInstances().First();
            MS = MetricService.GetInstances().First();
            VESMS = VirtualEthernetSwitchManagementService.GetInstances().First();
        }

        public void SUT_ComputerSystemMO(string ElementName, out uint ReturnValue, out ManagementPath Job, out ManagementPath ResultingSystem)
        {
            using (var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance())
            {
                virtualSystemSettingData.LateBoundObject["ElementName"] = ElementName;
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = VSSD_ConfigurationDataRoot;
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = VSSD_VirtualSystemSubtype;

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                ReturnValue = VSMS.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out Job, out ResultingSystem);

                VSSD_ToDestroy.Add(ResultingSystem);
            }
        }

        public static List<VirtualSystemSettingData> GetVirtualSystemSettingDataListThroughSettingsDefineState(ComputerSystem computerSystem)
        {
            return
                SettingsDefineState.GetInstances()
                    .Cast<SettingsDefineState>()
                    .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                    .ToList();
        }

        public static ResourcePool GetPrimordialResourcePool(string ResourceSubType)
        {
            return
                ResourcePool.GetInstances()
                        .Where((rp) =>
                            rp.Primordial == true &&
                            string.Compare(rp.ResourceSubType, ResourceSubType, true, CultureInfo.InvariantCulture) == 0)
                        .First();
        }

        public static AllocationCapabilities GetAllocationCapabilities(ResourcePool ResourcePool)
        {
            return
                ElementCapabilities.GetInstances()
                    .Cast<ElementCapabilities>()
                    .Where((ec) => string.Compare(ec.ManagedElement.Path, ResourcePool.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                    .Select((ec) => new AllocationCapabilities(ec.Capabilities))
                    .ToList()
                    .First();
        }

        public static ResourceAllocationSettingData GetDefaultResourceAllocationSettingData(AllocationCapabilities AllocationCapabilities)
        {
            return
                SettingsDefineCapabilities.GetInstances()
                    .Cast<SettingsDefineCapabilities>()
                    .Where((sdc) =>
                        string.Compare(sdc.GroupComponent.Path, AllocationCapabilities.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        sdc.ValueRange == 0 &&
                        sdc.ValueRole == 0)
                    .Select((sdc) => new ResourceAllocationSettingData(sdc.PartComponent))
                    .ToList()
                    .First();
        }

        public static StorageAllocationSettingData GetDefaultStorageAllocationSettingData(AllocationCapabilities AllocationCapabilities)
        {
            return
                SettingsDefineCapabilities.GetInstances()
                    .Cast<SettingsDefineCapabilities>()
                    .Where((sdc) =>
                        string.Compare(sdc.GroupComponent.Path, AllocationCapabilities.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        sdc.ValueRange == 0 &&
                        sdc.ValueRole == 0)
                    .Select((sdc) => new StorageAllocationSettingData(sdc.PartComponent))
                    .ToList()
                    .First();
        }

        public static List<ResourceAllocationSettingData> GetResourceAllocationgSettingData(VirtualSystemSettingData VirtualSystemSettingData, ushort ResourceType, string ResourceSubType)
        {
            return
                VirtualSystemSettingDataComponent.GetInstances()
                    .Cast<VirtualSystemSettingDataComponent>()
                    .Where((sds) =>
                        string.Compare(sds.GroupComponent.Path, VirtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(ResourceAllocationSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new ResourceAllocationSettingData(sds.PartComponent))
                    .ToList()
                    .Where((rasd) =>
                        rasd.ResourceType == ResourceType &&
                        string.Compare(rasd.ResourceSubType, ResourceSubType, true, CultureInfo.InvariantCulture) == 0)
                    .ToList();
        }

        public static GuestServiceInterfaceComponentSettingData GetGuestServiceInterfaceComponentSettingData(VirtualSystemSettingData VirtualSystemSettingData)
        {
            return
                VirtualSystemSettingDataComponent.GetInstances()
                    .Cast<VirtualSystemSettingDataComponent>()
                    .Where((sds) =>
                        string.Compare(sds.GroupComponent.Path, VirtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(GuestServiceInterfaceComponentSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new GuestServiceInterfaceComponentSettingData(sds.PartComponent))
                    .ToList()
                    .First();
        }

        public static GuestServiceInterfaceComponent GetGuestServiceInterfaceComponent(ComputerSystem ComputerSystem)
        {
            return
                SystemDevice.GetInstances()
                    .Cast<SystemDevice>()
                    .Where((sd) =>
                        string.Compare(sd.GroupComponent.Path, ComputerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(sd.PartComponent.ClassName, $"Msvm_{nameof(GuestServiceInterfaceComponent)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new GuestServiceInterfaceComponent(sds.PartComponent))
                    .ToList()
                    .First();
        }

        public static GuestFileService GetGuestFileService(GuestServiceInterfaceComponent GuestServiceInterfaceComponent)
        {
            return
                RegisteredGuestService.GetInstances()
                    .Cast<RegisteredGuestService>()
                    .Where((rgs) =>
                        string.Compare(rgs.Antecedent.Path, GuestServiceInterfaceComponent.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(rgs.Dependent.ClassName, $"Msvm_{nameof(GuestFileService)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new GuestFileService(sds.Dependent))
                    .ToList()
                    .First();
        }

        public void SUT_VirtualSystemSettingDataMO(string ElementName, ManagementPath AffectedSystem, out uint ReturnValue, out ManagementPath Job, out ManagementPath ResultingSnapshot)
        {
            using (var SnapshotSettingsInstance = VirtualSystemSettingData.CreateInstance())
            {
                SnapshotSettingsInstance.LateBoundObject["ElementName"] = ElementName;
                SnapshotSettingsInstance.LateBoundObject["SnapshotDataRoot"] = VSSS_SnapshotDataRoot;
                SnapshotSettingsInstance.LateBoundObject["VirtualSystemType"] = 5;

                ResultingSnapshot = null;
                string SnapshotSettings = SnapshotSettingsInstance.LateBoundObject.GetText(TextFormat.CimDtd20);
                ushort SnapshotType = 2;
                ReturnValue = VSSS.CreateSnapshot(AffectedSystem, ref ResultingSnapshot, SnapshotSettings, SnapshotType, out Job);
            }
        }

        public static List<SnapshotOfVirtualSystem> GetSnapshotOfVirtualSystemList(ComputerSystem ComputerSystem)
        {
            return
                SnapshotOfVirtualSystem.GetInstances()
                    .Cast<SnapshotOfVirtualSystem>()
                    .Where((sovs) => string.Compare(sovs.Antecedent.Path, ComputerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                    .ToList();
        }

        public static List<MostCurrentSnapshotInBranch> GetMostCurrentSnapshotInBranchList(ComputerSystem ComputerSystem)
        {
            return
                MostCurrentSnapshotInBranch.GetInstances()
                    .Cast<MostCurrentSnapshotInBranch>()
                    .Where((sovs) => string.Compare(sovs.Antecedent.Path, ComputerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                    .ToList();
        }

        public static List<VirtualSystemSettingData> GetVirtualSystemSettingDataListThroughSnapshotOfVirtualSystem(ComputerSystem ComputerSystem)
        {
            return 
                SnapshotOfVirtualSystem.GetInstances()
                    .Cast<SnapshotOfVirtualSystem>()
                    .Where((sovs) => string.Compare(sovs.Antecedent.Path, ComputerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                    .Select((sovs) => new VirtualSystemSettingData(sovs.Dependent))
                    .ToList();
        }

        public static List<LastAppliedSnapshot> GetLastAppliedSnapshotList(ComputerSystem ComputerSystem)
        {
            return
                LastAppliedSnapshot.GetInstances()
                    .Cast<LastAppliedSnapshot>()
                    .Where((las) => string.Compare(las.Antecedent.Path, ComputerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                    .ToList();
        }

        public static List<MemorySettingData> GetMemorySettingDataList(VirtualSystemSettingData VirtualSystemSettingData)
        {
            return
                VirtualSystemSettingDataComponent.GetInstances()
                    .Cast<VirtualSystemSettingDataComponent>()
                    .Where((sds) =>
                        string.Compare(sds.GroupComponent.Path, VirtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(MemorySettingData)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new MemorySettingData(sds.PartComponent))
                    .ToList();
        }

        public static List<ProcessorSettingData> GetProcessorSettingDataList(VirtualSystemSettingData VirtualSystemSettingData)
        {
            return
                VirtualSystemSettingDataComponent.GetInstances()
                    .Cast<VirtualSystemSettingDataComponent>()
                    .Where((sds) =>
                        string.Compare(sds.GroupComponent.Path, VirtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(ProcessorSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new ProcessorSettingData(sds.PartComponent))
                    .ToList();
        }

        public void SUT_VirtualEthernetSwitchSettingDataMO(string ElementName,string Notes, out uint ReturnValue, out ManagementPath Job, out ManagementPath ResultingSystem)
        {
            using (var virtualEthernetSwitchSettingData = VirtualEthernetSwitchSettingData.CreateInstance())
            {
                virtualEthernetSwitchSettingData.LateBoundObject["ElementName"] = ElementName;
                virtualEthernetSwitchSettingData.LateBoundObject["Notes"] = new string[] { Notes };

                ManagementPath ReferenceConfiguration = null;
                var SystemSettings = virtualEthernetSwitchSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);
                var ResourceSettings = Array.Empty<string>();

                ReturnValue = VESMS.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out Job, out ResultingSystem);

                VESSD_ToDestroy.Add(ResultingSystem);
            }
        }

        public static List<StorageAllocationSettingData> GetStorageAllocationSettingDataList(VirtualSystemSettingData VirtualSystemSettingData, ushort ResourceType, string ResourceSubType)
        {
            return
                VirtualSystemSettingDataComponent.GetInstances()
                    .Cast<VirtualSystemSettingDataComponent>()
                    .Where((sds) =>
                        string.Compare(sds.GroupComponent.Path, VirtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(StorageAllocationSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new StorageAllocationSettingData(sds.PartComponent))
                    .ToList()
                    .Where((rasd) =>
                        rasd.ResourceType == ResourceType &&
                        string.Compare(rasd.ResourceSubType, ResourceSubType, true, CultureInfo.InvariantCulture) == 0)
                    .ToList();
        }

        public static SyntheticEthernetPortSettingData GetDefaultSyntheticEthernetPortSettingData(AllocationCapabilities AllocationCapabilities)
        {
            return
                SettingsDefineCapabilities.GetInstances()
                    .Cast<SettingsDefineCapabilities>()
                    .Where((sdc) =>
                        string.Compare(sdc.GroupComponent.Path, AllocationCapabilities.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        sdc.ValueRange == 0 &&
                        sdc.ValueRole == 0)
                    .Select((sdc) => new SyntheticEthernetPortSettingData(sdc.PartComponent))
                    .ToList()
                    .First();
        }

        public static EthernetPortAllocationSettingData GetDefaultEthernetPortAllocationSettingData(AllocationCapabilities AllocationCapabilities)
        {
            return
                SettingsDefineCapabilities.GetInstances()
                    .Cast<SettingsDefineCapabilities>()
                    .Where((sdc) =>
                        string.Compare(sdc.GroupComponent.Path, AllocationCapabilities.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        sdc.ValueRange == 0 &&
                        sdc.ValueRole == 0)
                    .Select((sdc) => new EthernetPortAllocationSettingData(sdc.PartComponent))
                    .ToList()
                    .First();
        }

        public static List<SyntheticEthernetPortSettingData> GetSyntheticEthernetPortSettingData(VirtualSystemSettingData VirtualSystemSettingData, ushort ResourceType, string ResourceSubType)
        {
            return
                VirtualSystemSettingDataComponent.GetInstances()
                    .Cast<VirtualSystemSettingDataComponent>()
                    .Where((sds) =>
                        string.Compare(sds.GroupComponent.Path, VirtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(SyntheticEthernetPortSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new SyntheticEthernetPortSettingData(sds.PartComponent))
                    .ToList()
                    .Where((rasd) =>
                        rasd.ResourceType == ResourceType &&
                        string.Compare(rasd.ResourceSubType, ResourceSubType, true, CultureInfo.InvariantCulture) == 0)
                    .ToList();
        }

        public static List<EthernetPortAllocationSettingData> GetEthernetPortAllocationSettingData(VirtualSystemSettingData VirtualSystemSettingData, ushort ResourceType, string ResourceSubType)
        {
            return
                VirtualSystemSettingDataComponent.GetInstances()
                    .Cast<VirtualSystemSettingDataComponent>()
                    .Where((sds) =>
                        string.Compare(sds.GroupComponent.Path, VirtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(EthernetPortAllocationSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new EthernetPortAllocationSettingData(sds.PartComponent))
                    .ToList()
                    .Where((rasd) =>
                        rasd.ResourceType == ResourceType &&
                        string.Compare(rasd.ResourceSubType, ResourceSubType, true, CultureInfo.InvariantCulture) == 0)
                    .ToList();
        }

        #region Jobs

        public static void WaitForConcreteJobToEnd(ManagementPath Job)
        {
            if (string.IsNullOrEmpty(Job?.ClassName) == false)
            {
                using (var concreteJob = new ConcreteJob(Job))
                {
                    while (
                        concreteJob.JobState != 7 &&     // Completed
                        concreteJob.JobState != 8 &&     // Terminated
                        concreteJob.JobState != 9 &&     // Killed
                        concreteJob.JobState != 10 &&    // Exception
                        concreteJob.JobState != 32768)   // CompletedWithWarnings
                    {
                        ((ManagementObject)concreteJob.LateBoundObject).Get();
                    }
                }
            }
        }

        public static void WaitForStorageJobToEnd(ManagementPath Job)
        {
            if (string.IsNullOrEmpty(Job?.ClassName) == false)
            {
                using (var storageJob = new StorageJob(Job))
                {
                    while (
                        storageJob.JobState != 7 &&     // Completed
                        storageJob.JobState != 8 &&     // Terminated
                        storageJob.JobState != 9 &&     // Killed
                        storageJob.JobState != 10 &&    // Exception
                        storageJob.JobState != 32768)   // CompletedWithWarnings
                    {
                        ((ManagementObject)storageJob.LateBoundObject).Get();
                    }
                }
            }
        }

        public static void WaitForCopyFileToGuestJobToEnd(ManagementPath Job)
        {
            if (string.IsNullOrEmpty(Job?.ClassName) == false)
            {
                using (var copyFileToGuestJob = new CopyFileToGuestJob(Job))
                {
                    while (
                        copyFileToGuestJob.JobState != 7 &&     // Completed
                        copyFileToGuestJob.JobState != 8 &&     // Terminated
                        copyFileToGuestJob.JobState != 9 &&     // Killed
                        copyFileToGuestJob.JobState != 10 &&    // Exception
                        copyFileToGuestJob.JobState != 32768)   // CompletedWithWarnings
                    {
                        ((ManagementObject)copyFileToGuestJob.LateBoundObject).Get();
                    }
                }
            }
        }

        #endregion

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

        public static string GetFileSHA256HexUpperCase(string filename)
        {
            if (File.Exists(filename) == false)
            {
                return "";
            }

            using (var sha256 = SHA256.Create())
            {
                using (FileStream fileStream = File.OpenRead(filename))
                {
                    return BitConverter.ToString(sha256.ComputeHash(fileStream)).Replace("-", string.Empty).ToUpperInvariant();
                }
            }
            
        }

        #region Web

        // use it in download async handler event below
        private static int LastFileProgressMet { get; set; } = -1;

        public static void DownloadFile(string address, string filename)
        {
            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += (sender, e) =>
                {
                    if (e.ProgressPercentage % 5 == 0 && e.ProgressPercentage > LastFileProgressMet)
                    {
                        LastFileProgressMet = e.ProgressPercentage;

                        Trace.WriteLine($"[{DateTime.Now}] Percentage percentage [{e.ProgressPercentage}%] Bytes received [{e.BytesReceived}] out of [{e.TotalBytesToReceive}]");
                    }
                };

                client.DownloadFileCompleted += (sender, e) =>
                {
                    if (e.Cancelled)
                    {
                        Trace.WriteLine($"[{DateTime.Now}] The download has been cancelled!");
                        return;
                    }

                    if (e.Error != null)
                    {
                        Trace.WriteLine($"[{DateTime.Now}] An error ocurred while trying to download file!");
                        return;
                    }

                    Trace.WriteLine($"[{DateTime.Now}] Finished downloading [{filename}] from [{address}]");
                };

                client.DownloadFileAsync(new Uri(address), filename);

                while (client.IsBusy)
                {
                    Thread.Sleep(1000);
                }
            }
        }

        #endregion

        #region Cleanup

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                VSSD_ToDestroy?.ForEach((vssd) => VSMS?.DestroySystem(vssd, out ManagementPath Job));
            }
            catch (ManagementException e)
            {
                Trace.WriteLine($"[{DateTime.Now}] [{e.Message}]");
            }

            try
            {
                VESSD_ToDestroy?.ForEach((vessd) => VESMS?.DestroySystem(vessd, out ManagementPath Job));
            }
            catch (ManagementException e)
            {
                Trace.WriteLine($"[{DateTime.Now}] [{e.Message}]");
            }

            if (disposing)
            {
                VSMS?.Dispose();
                IMS?.Dispose();
            }

            VSSD_ToDestroy.Clear();
            VESSD_ToDestroy.Clear();
        }

        ~ViridianUtils()
        {
            Dispose(false);
        }

        #endregion
    }
}
