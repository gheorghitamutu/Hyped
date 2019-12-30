using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using Viridian.Root.Virtualization.v2.Msvm.Integration;
using Viridian.Root.Virtualization.v2.Msvm.ResourceManagement;
using Viridian.Root.Virtualization.v2.Msvm.Storage;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;

namespace ViridianTester.Msvm.VirtualSystemManagement
{
    [TestClass]
    public class VirtualSystemManagementServiceTest
    {
        [TestMethod]
        public void CreateInstance_ExpectingNotNullLateBoundObject()
        {
            using (var sut = VirtualSystemManagementService.CreateInstance())
            {
                Assert.IsNotNull(sut.LateBoundObject);
            }
        }

        [TestMethod]
        public void CreateInstance_ExpectingGetInstancesCountIsOne()
        {
            var sut = VirtualSystemManagementService.GetInstances();

            Assert.AreEqual(sut.Count, 1);
        }

        [TestMethod]
        public void DefineSystem_ExpectingNotNullResultingSystemAndReturnValueIsZero()
        {
            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                ViridianUtils.GetCurrentMethod(),
                out uint ReturnValue,
                out ManagementPath Job,
                out ManagementPath ResultingSystem);

                Assert.IsNotNull(ResultingSystem);
                Assert.AreEqual(0U, ReturnValue);
            }
        }

        [TestMethod]
        public void DestroySystem_ExpectingGetInstancesWithElementNameConditionCountZero()
        {
            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                ViridianUtils.GetCurrentMethod(),
                out uint ReturnValue,
                out ManagementPath Job,
                out ManagementPath ResultingSystem);

                using (var computerSystem = new ComputerSystem(ResultingSystem))
                {
                    var name = computerSystem.Name;

                    viridianUtils.Dispose();

                    var ReferenceConfigurationInstances = ComputerSystem.GetInstances().Where((cs) => cs.Name == name).ToList();

                    Assert.IsNotNull(ResultingSystem);
                    Assert.AreEqual(0U, ReturnValue);
                    Assert.AreEqual(0, ReferenceConfigurationInstances.Count);
                }
            }
        }

        [TestMethod]
        public void RequestStateChange_ExpectingEnabledStateIsTwo()
        {
            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                using (var computerSystem = new ComputerSystem(ResultingSystem))
                {
                    ReturnValue = computerSystem.RequestStateChange(2, null, out Job);

                    ViridianUtils.WaitForConcreteJobToEnd(Job);

                    computerSystem.UpdateObject();

                    Assert.IsNotNull(ResultingSystem);
                    Assert.AreEqual(4096U, ReturnValue);
                    Assert.AreEqual(2U, computerSystem.EnabledState);

                    ReturnValue = computerSystem.RequestStateChange(3, null, out Job);

                    ViridianUtils.WaitForConcreteJobToEnd(Job);
                }
            }
        }

        [TestMethod]
        public void GetVirtualSystemThumbnailImage_ExpectingNotNullImage()
        {
            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                using (var computerSystem = new ComputerSystem(ResultingSystem))
                {
                    ReturnValue = computerSystem.RequestStateChange(2, null, out Job);

                    ViridianUtils.WaitForConcreteJobToEnd(Job);

                    ReturnValue = viridianUtils.VSMS.GetVirtualSystemThumbnailImage(1000, ResultingSystem, 1000, out byte[] ImageData);

                    Assert.IsNotNull(ResultingSystem);
                    Assert.AreEqual(0U, ReturnValue);
                    Assert.IsNotNull(ImageData);

                    ReturnValue = computerSystem.RequestStateChange(3, null, out Job);
                }

                ViridianUtils.WaitForConcreteJobToEnd(Job);
            }
        }

        [TestMethod]
        public void GetSummaryInformation_ExpectingMemoryAvailable1024()
        {
            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                using (var computerSystem = new ComputerSystem(ResultingSystem))
                {
                    var vssdCollection =
                        SettingsDefineState.GetInstances()
                            .Cast<SettingsDefineState>()
                            .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                            .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                            .ToList();

                    var RequestedInformation = (uint[])Enum.GetValues(typeof(SummaryInformation.RequestedInformation));
                    var SettingData = new ManagementPath[] { vssdCollection.First().Path };

                    ReturnValue = viridianUtils.VSMS.GetSummaryInformation(RequestedInformation, SettingData, out ManagementBaseObject[] SummaryInformation);

                    using (SummaryInformation summaryInformation = new SummaryInformation(SummaryInformation.First()))
                    {
                        Assert.IsNotNull(ResultingSystem);
                        Assert.AreEqual(0U, ReturnValue);
                        Assert.AreEqual(1, vssdCollection.Count);
                        Assert.AreEqual(1, SummaryInformation.Length);
                        Assert.IsNotNull(SummaryInformation);
                        Assert.AreEqual(0U, summaryInformation.MemoryUsage);
                    }
                }
            }
        }

        // use it in download async handler event below
        private static int LastFileProgressMet { get; set; } = -1;

        [TestMethod]
        [Timeout(TestTimeout.Infinite)]
        public void AutomatedOSInstallationWindows10Version1903x64_ExpectingFileThatMarksTheJobAsFinishedInVM()
        {
            var isoName = $"{AppDomain.CurrentDomain.BaseDirectory}\\{nameof(AutomatedOSInstallationWindows10Version1903x64_ExpectingFileThatMarksTheJobAsFinishedInVM)}.iso";
            var uri = "https://filetransfer.io/data-package/AwILWW3y?do=download";
            var expectedSHA256 = "4bc7170baa665f4d92ba379843b47e83b384313fb6edf3fae09bc9dd42cd8426";
            var fileHashMatches = false;

            if (File.Exists(isoName))
            {
                using (var sha256 = SHA256.Create())
                {
                    using (FileStream fileStream = File.OpenRead(isoName))
                    {
                        var computedSHA256 = BitConverter.ToString(sha256.ComputeHash(fileStream)).Replace("-", string.Empty).ToLower();

                        Trace.WriteLine($"Expected SHA256 [{expectedSHA256}] Computed SHA256 [{computedSHA256}]");

                        fileHashMatches = (computedSHA256 == expectedSHA256);
                    }
                }
            }

            if (fileHashMatches == false)
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

                        Trace.WriteLine($"[{DateTime.Now}] Finished downloading [{isoName}] from [{uri}]");
                    };

                    client.DownloadFileAsync(new Uri(uri), isoName);

                    while (client.IsBusy)
                    {
                        Thread.Sleep(1000);
                    }
                }

                using (var sha256 = SHA256.Create())
                {
                    using (FileStream fileStream = File.OpenRead(isoName))
                    {
                        var computedSHA256 = BitConverter.ToString(sha256.ComputeHash(fileStream)).Replace("-", string.Empty).ToLower();

                        Trace.WriteLine($"Expected SHA256 [{expectedSHA256}] Computed SHA256 [{computedSHA256}]");

                        fileHashMatches = (computedSHA256 == expectedSHA256);
                    }
                }
            }

            if (fileHashMatches == false)
            {
                throw new Exception($"Invalid SHA256 for file [{isoName}]!");
            }

            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                var primordialResourcePoolSCSI =
                    ResourcePool.GetInstances()
                        .Where((rp) =>
                            rp.Primordial == true &&
                            string.Compare(rp.ResourceSubType, "Microsoft:Hyper-V:Synthetic SCSI Controller", true, CultureInfo.InvariantCulture) == 0)
                        .First();

                var allocationCapabilitiesSCSIController =
                    ElementCapabilities.GetInstances()
                        .Cast<ElementCapabilities>()
                        .Where((ec) => string.Compare(ec.ManagedElement.Path, primordialResourcePoolSCSI.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((ec) => new AllocationCapabilities(ec.Capabilities))
                        .ToList()
                        .First();

                var resourceAllocationSettingDataSCSIController =
                    SettingsDefineCapabilities.GetInstances()
                        .Cast<SettingsDefineCapabilities>()
                        .Where((sdc) =>
                            string.Compare(sdc.GroupComponent.Path, allocationCapabilitiesSCSIController.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                            sdc.ValueRange == 0 &&
                            sdc.ValueRole == 0)
                        .Select((sdc) => new ResourceAllocationSettingData(sdc.PartComponent))
                        .ToList()
                        .First();

                var computerSystem = new ComputerSystem(ResultingSystem);

                var virtualSystemSettingData =
                    SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();

                var AffectedConfiguration = virtualSystemSettingData.Path;
                var ResourceSettings = new string[] { resourceAllocationSettingDataSCSIController.LateBoundObject.GetText(TextFormat.WmiDtd20) };

                ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettings);

                var scsiController =
                VirtualSystemSettingDataComponent.GetInstances()
                    .Cast<VirtualSystemSettingDataComponent>()
                    .Where((sds) =>
                        string.Compare(sds.GroupComponent.Path, virtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(ResourceAllocationSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new ResourceAllocationSettingData(sds.PartComponent))
                    .ToList()
                    .Where((rasd) =>
                        rasd.ResourceType == 6 &&
                        string.Compare(rasd.ResourceSubType, "Microsoft:Hyper-V:Synthetic SCSI Controller", true, CultureInfo.InvariantCulture) == 0)
                    .ToList()
                    .First();

                var primordialResourcePoolDVDDrive =
                   ResourcePool.GetInstances()
                       .Where((rp) =>
                           rp.Primordial == true &&
                           string.Compare(rp.ResourceSubType, "Microsoft:Hyper-V:Synthetic DVD Drive", true, CultureInfo.InvariantCulture) == 0)
                       .First();

                var allocationCapabilitiesDVDDrive =
                    ElementCapabilities.GetInstances()
                        .Cast<ElementCapabilities>()
                        .Where((ec) => string.Compare(ec.ManagedElement.Path, primordialResourcePoolDVDDrive.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((ec) => new AllocationCapabilities(ec.Capabilities))
                        .ToList()
                        .First();

                var resourceAllocationSettingDataDVDDrive =
                    SettingsDefineCapabilities.GetInstances()
                        .Cast<SettingsDefineCapabilities>()
                        .Where((sdc) =>
                            string.Compare(sdc.GroupComponent.Path, allocationCapabilitiesDVDDrive.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                            sdc.ValueRange == 0 &&
                            sdc.ValueRole == 0)
                        .Select((sdc) => new ResourceAllocationSettingData(sdc.PartComponent))
                        .ToList()
                        .First();

                resourceAllocationSettingDataDVDDrive.LateBoundObject["Parent"] = scsiController.Path;
                resourceAllocationSettingDataDVDDrive.LateBoundObject["AddressOnParent"] = 0;

                ResourceSettings = new string[] { resourceAllocationSettingDataDVDDrive.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                var synthethicDVD =
                VirtualSystemSettingDataComponent.GetInstances()
                    .Cast<VirtualSystemSettingDataComponent>()
                    .Where((sds) =>
                        string.Compare(sds.GroupComponent.Path, virtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(ResourceAllocationSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new ResourceAllocationSettingData(sds.PartComponent))
                    .ToList()
                    .Where((rasd) =>
                        rasd.ResourceType == 16 &&
                        string.Compare(rasd.ResourceSubType, "Microsoft:Hyper-V:Synthetic DVD Drive", true, CultureInfo.InvariantCulture) == 0)
                    .First();

                var primordialResourcePoolVirtualCDDVD =
                    ResourcePool.GetInstances()
                        .Where((rp) =>
                            rp.Primordial == true &&
                            string.Compare(rp.ResourceSubType, "Microsoft:Hyper-V:Virtual CD/DVD Disk", true, CultureInfo.InvariantCulture) == 0)
                        .First();

                var allocationCapabilitiesVirtualCDDVD =
                    ElementCapabilities.GetInstances()
                        .Cast<ElementCapabilities>()
                        .Where((ec) => string.Compare(ec.ManagedElement.Path, primordialResourcePoolVirtualCDDVD.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((ec) => new AllocationCapabilities(ec.Capabilities))
                        .ToList()
                        .First();

                var resourceAllocationSettingVirtualCDDVD =
                    SettingsDefineCapabilities.GetInstances()
                        .Cast<SettingsDefineCapabilities>()
                        .Where((sdc) =>
                            string.Compare(sdc.GroupComponent.Path, allocationCapabilitiesVirtualCDDVD.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                            sdc.ValueRange == 0 &&
                            sdc.ValueRole == 0)
                        .Select((sdc) => new StorageAllocationSettingData(sdc.PartComponent))
                        .ToList()
                        .First();

                resourceAllocationSettingVirtualCDDVD.LateBoundObject["Address"] = 0;
                resourceAllocationSettingVirtualCDDVD.LateBoundObject["Parent"] = synthethicDVD.Path;
                resourceAllocationSettingVirtualCDDVD.LateBoundObject["HostResource"] = new[] { isoName };

                ResourceSettings = new string[] { resourceAllocationSettingVirtualCDDVD.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                var virtualCDDVDCollection =
                VirtualSystemSettingDataComponent.GetInstances()
                    .Cast<VirtualSystemSettingDataComponent>()
                    .Where((sds) =>
                        string.Compare(sds.GroupComponent.Path, virtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(StorageAllocationSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new StorageAllocationSettingData(sds.PartComponent))
                    .ToList()
                    .Where((rasd) =>
                        rasd.ResourceType == 31 &&
                        string.Compare(rasd.ResourceSubType, "Microsoft:Hyper-V:Virtual CD/DVD Disk", true, CultureInfo.InvariantCulture) == 0 &&
                    string.Compare(rasd.Caption, "ISO Disk Image", true, CultureInfo.InvariantCulture) == 0)
                    .ToList();

                var primordialResourcePoolDiskDrive =
                    ResourcePool.GetInstances()
                        .Where((rp) =>
                            rp.Primordial == true &&
                            string.Compare(rp.ResourceSubType, "Microsoft:Hyper-V:Synthetic Disk Drive", true, CultureInfo.InvariantCulture) == 0)
                        .First();

                var allocationCapabilitiesDiskDrive =
                    ElementCapabilities.GetInstances()
                        .Cast<ElementCapabilities>()
                        .Where((ec) => string.Compare(ec.ManagedElement.Path, primordialResourcePoolDiskDrive.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((ec) => new AllocationCapabilities(ec.Capabilities))
                        .ToList()
                        .First();

                var resourceAllocationSettingDataDiskDrive =
                    SettingsDefineCapabilities.GetInstances()
                        .Cast<SettingsDefineCapabilities>()
                        .Where((sdc) =>
                            string.Compare(sdc.GroupComponent.Path, allocationCapabilitiesDiskDrive.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                            sdc.ValueRange == 0 &&
                            sdc.ValueRole == 0)
                        .Select((sdc) => new ResourceAllocationSettingData(sdc.PartComponent))
                        .ToList()
                        .First();

                resourceAllocationSettingDataDiskDrive.LateBoundObject["Parent"] = scsiController.Path;
                resourceAllocationSettingDataDiskDrive.LateBoundObject["AddressOnParent"] = 1;

                ResourceSettings = new string[] { resourceAllocationSettingDataDiskDrive.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                var synthethicDiskDrive =
                VirtualSystemSettingDataComponent.GetInstances()
                    .Cast<VirtualSystemSettingDataComponent>()
                    .Where((sds) =>
                        string.Compare(sds.GroupComponent.Path, virtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(ResourceAllocationSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new ResourceAllocationSettingData(sds.PartComponent))
                    .ToList()
                    .Where((rasd) =>
                        rasd.ResourceType == 17 &&
                        string.Compare(rasd.ResourceSubType, "Microsoft:Hyper-V:Synthetic Disk Drive", true, CultureInfo.InvariantCulture) == 0)
                    .First();

                var primordialResourcePoolVirtualHardDisk =
                    ResourcePool.GetInstances()
                        .Where((rp) =>
                            rp.Primordial == true &&
                            string.Compare(rp.ResourceSubType, "Microsoft:Hyper-V:Virtual Hard Disk", true, CultureInfo.InvariantCulture) == 0)
                        .First();

                var allocationCapabilitiesVirtualHardDisk =
                    ElementCapabilities.GetInstances()
                        .Cast<ElementCapabilities>()
                        .Where((ec) => string.Compare(ec.ManagedElement.Path, primordialResourcePoolVirtualHardDisk.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((ec) => new AllocationCapabilities(ec.Capabilities))
                        .ToList()
                        .First();

                var resourceAllocationSettingVirtualHardDisk =
                    SettingsDefineCapabilities.GetInstances()
                        .Cast<SettingsDefineCapabilities>()
                        .Where((sdc) =>
                            string.Compare(sdc.GroupComponent.Path, allocationCapabilitiesVirtualHardDisk.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                            sdc.ValueRange == 0 &&
                            sdc.ValueRole == 0)
                        .Select((sdc) => new StorageAllocationSettingData(sdc.PartComponent))
                        .ToList()
                        .First();

                var vhdxName = $"{AppDomain.CurrentDomain.BaseDirectory}\\{nameof(AutomatedOSInstallationWindows10Version1903x64_ExpectingFileThatMarksTheJobAsFinishedInVM)}.vhdx";

                // operations on the host
                var vhdsd = VirtualHardDiskSettingData.CreateInstance();
                vhdsd.LateBoundObject["Type"] = VirtualHardDiskSettingData.TypeValues.Dynamic;
                vhdsd.LateBoundObject["Format"] = VirtualHardDiskSettingData.FormatValues.VHDX;
                vhdsd.LateBoundObject["Path"] = vhdxName;
                vhdsd.LateBoundObject["ParentPath"] = null;
                vhdsd.LateBoundObject["MaxInternalSize"] = 1024UL * 1024 * 1024 * 100;

                using (var ims = ImageManagementService.GetInstances().First())
                {
                    var VirtualDiskSettingData = vhdsd.LateBoundObject.GetText(TextFormat.WmiDtd20);

                    ims.CreateVirtualHardDisk(VirtualDiskSettingData, out Job);

                    ViridianUtils.WaitForStorageJobToEnd(Job);
                }

                resourceAllocationSettingVirtualHardDisk.LateBoundObject["Access"] = 3; // read/write
                resourceAllocationSettingVirtualHardDisk.LateBoundObject["Address"] = 0;
                resourceAllocationSettingVirtualHardDisk.LateBoundObject["Parent"] = synthethicDiskDrive.Path.Path;
                resourceAllocationSettingVirtualHardDisk.LateBoundObject["HostResource"] = new[] { vhdxName };

                ResourceSettings = new string[] { resourceAllocationSettingVirtualHardDisk.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);
                
                ReturnValue = computerSystem.RequestStateChange(2, null, out Job);

                ViridianUtils.WaitForConcreteJobToEnd(Job);

                var guestServiceInterfaceComponentSettingData = VirtualSystemSettingDataComponent.GetInstances()
                        .Cast<VirtualSystemSettingDataComponent>()
                        .Where((sds) =>
                            string.Compare(sds.GroupComponent.Path, virtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                            string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(GuestServiceInterfaceComponentSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new GuestServiceInterfaceComponentSettingData(sds.PartComponent))
                        .ToList()
                        .First();

                guestServiceInterfaceComponentSettingData.LateBoundObject["EnabledState"] = GuestServiceInterfaceComponentSettingData.EnabledStateValues.Enabled;

                var virtualSystemManagementService = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First();

                var GuestServiceSettings = new string[1] { guestServiceInterfaceComponentSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                ReturnValue = virtualSystemManagementService.ModifyGuestServiceSettings(GuestServiceSettings, out Job, out ManagementPath[] ResultingGuestServices);

                ViridianUtils.WaitForConcreteJobToEnd(Job);

                var guestServiceInterfaceComponent =
                SystemDevice.GetInstances()
                    .Cast<SystemDevice>()
                    .Where((sd) =>
                        string.Compare(sd.GroupComponent.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(sd.PartComponent.ClassName, $"Msvm_{nameof(GuestServiceInterfaceComponent)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new GuestServiceInterfaceComponent(sds.PartComponent))
                    .ToList()
                    .First();

                var guestFileService =
                    RegisteredGuestService.GetInstances()
                        .Cast<RegisteredGuestService>()
                        .Where((rgs) =>
                            string.Compare(rgs.Antecedent.Path, guestServiceInterfaceComponent.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                            string.Compare(rgs.Dependent.ClassName, $"Msvm_{nameof(GuestFileService)}", true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new GuestFileService(sds.Dependent))
                        .ToList()
                        .First();

                var copyFileToGuestSettingData = CopyFileToGuestSettingData.CreateInstance();
                copyFileToGuestSettingData.DestinationPath = @"C:\finished";
                copyFileToGuestSettingData.OverwriteExisting = false;
                copyFileToGuestSettingData.SourcePath = @"C:\copyme";
                copyFileToGuestSettingData.CreateFullPath = true;

                // this check might still be a bit too early but.. heh
                // right now I do not know a decent way to check if account set up finished
                var retries = 4;
                var retryWaitTime = 10; // minutes
                var lastErrorcode = 0;
                var fileExists = false;

                for (int i = 0; i < retries; i++)
                {
                    var CopyFileToGuestSettings = new string[1] { copyFileToGuestSettingData.LateBoundObject.GetText(TextFormat.CimDtd20) };
                    ReturnValue = guestFileService.CopyFilesToGuest(CopyFileToGuestSettings, out Job);

                    using (var copyFileToGuestJob = new CopyFileToGuestJob(Job))
                    {
                        ViridianUtils.WaitForCopyFileToGuestJobToEnd(Job);

                        copyFileToGuestJob.LateBoundObject.Properties.Cast<PropertyData>().ToList().ForEach((p) => Trace.WriteLine($"{p.Name} [{p.Value?.ToString()}]"));

                        lastErrorcode = copyFileToGuestJob.ErrorCode;
                        fileExists = copyFileToGuestJob.ErrorDescription.Contains("The file exists. (0x80070050)");

                        if (fileExists)
                        {
                            break;
                        }

                        Thread.Sleep(new TimeSpan(0, retryWaitTime, 0));
                    }
                }

                Assert.AreEqual(32768, lastErrorcode);
                Assert.IsTrue(fileExists);

                ReturnValue = computerSystem.RequestStateChange(3, null, out Job);

                ViridianUtils.WaitForConcreteJobToEnd(Job);

                viridianUtils.Dispose(); // force destroy system

                try // the vhdx might still be under a merge operation that keeps on handle on it
                {   // could try to check all "merge jobs" and pick the one that handles this and wait for it to finish
                    File.Delete(vhdxName);
                }

                catch(IOException e)
                {
                    Trace.WriteLine($"Deleting .vhdx file [{vhdxName}] failed: [{e.Message}]!");
                }

                File.Delete(isoName);
            }
        }
    }
}
