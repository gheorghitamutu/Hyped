﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading;
using Viridian.Root.Virtualization.v2.Msvm.Integration;
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

                    viridianUtils.Dispose(); // destroy VM

                    var ReferenceConfigurationInstances = ComputerSystem.GetInstances().Where((cs) => cs.Name == name).ToList();

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
                    var vssdCollection = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem);

                    var RequestedInformation = (uint[])Enum.GetValues(typeof(SummaryInformation.RequestedInformation));
                    var SettingData = new ManagementPath[] { vssdCollection.First().Path };
                    ReturnValue = viridianUtils.VSMS.GetSummaryInformation(RequestedInformation, SettingData, out ManagementBaseObject[] SummaryInformation);

                    using (var summaryInformation = new SummaryInformation(SummaryInformation.First()))
                    {
                        Assert.AreEqual(0U, ReturnValue);
                        Assert.AreEqual(1, vssdCollection.Count);
                        Assert.IsNotNull(SummaryInformation);
                        Assert.AreEqual(1, SummaryInformation.Length);
                        Assert.AreEqual(0U, summaryInformation.MemoryUsage);
                    }
                }
            }
        }

        [TestMethod]
        [Timeout(TestTimeout.Infinite)]
        public void AutomatedOSInstallationWindows10Version1903x64_ExpectingFileThatMarksTheJobAsFinishedInVM()
        {
            var vhdxName = $"{AppDomain.CurrentDomain.BaseDirectory}\\{ViridianUtils.GetCurrentMethod()}.vhdx";
            var isoName = $"{AppDomain.CurrentDomain.BaseDirectory}\\{ViridianUtils.GetCurrentMethod()}.iso";
            var uri = "https://filetransfer.io/data-package/AwILWW3y?do=download";
            var expectedSHA256 = "4bc7170baa665f4d92ba379843b47e83b384313fb6edf3fae09bc9dd42cd8426".ToUpperInvariant();
            var fileHashMatches = false;

            var computedSHA256 = ViridianUtils.GetFileSHA256HexUpperCase(isoName);

            Trace.WriteLine($"Expected SHA256 [{expectedSHA256}] Computed SHA256 [{computedSHA256}]");

            fileHashMatches = computedSHA256 == expectedSHA256;

            if (fileHashMatches == false)
            {
                ViridianUtils.DownloadFile(uri, isoName);

                computedSHA256 = ViridianUtils.GetFileSHA256HexUpperCase(isoName);

                Trace.WriteLine($"Expected SHA256 [{expectedSHA256}] Computed SHA256 [{computedSHA256}]");

                fileHashMatches = computedSHA256 == expectedSHA256;

                if (fileHashMatches == false)
                {
                    throw new Exception($"Invalid SHA256 for file [{isoName}]!");
                }
            }

            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                var primordialResourcePoolSCSI = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic SCSI Controller");
                var allocationCapabilitiesSCSIController = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolSCSI);
                var resourceAllocationSettingDataSCSIController = ViridianUtils.GetDefaultResourceAllocationSettingData(allocationCapabilitiesSCSIController);

                using (var computerSystem = new ComputerSystem(ResultingSystem))
                {
                    using (var virtualSystemSettingData = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem).First())
                    {
                        var AffectedConfiguration = virtualSystemSettingData.Path;
                        var ResourceSettings = new string[] { resourceAllocationSettingDataSCSIController.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                        ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettings);

                        using (var scsiController = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 6, "Microsoft:Hyper-V:Synthetic SCSI Controller").First())
                        {
                            using (var primordialResourcePoolDVDDrive = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic DVD Drive"))
                            using (var allocationCapabilitiesDVDDrive = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolDVDDrive))
                            using (var resourceAllocationSettingDataDVDDrive = ViridianUtils.GetDefaultResourceAllocationSettingData(allocationCapabilitiesDVDDrive))
                            {
                                resourceAllocationSettingDataDVDDrive.LateBoundObject["Parent"] = scsiController.Path;
                                resourceAllocationSettingDataDVDDrive.LateBoundObject["AddressOnParent"] = 0;

                                ResourceSettings = new string[] { resourceAllocationSettingDataDVDDrive.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                            }
                            ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                            using (var synthethicDVD = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 16, "Microsoft:Hyper-V:Synthetic DVD Drive").First())
                            using (var primordialResourcePoolVirtualCDDVD = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Virtual CD/DVD Disk"))
                            using (var allocationCapabilitiesVirtualCDDVD = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolVirtualCDDVD))
                            using (var resourceAllocationSettingVirtualCDDVD = ViridianUtils.GetDefaultStorageAllocationSettingData(allocationCapabilitiesVirtualCDDVD))
                            {
                                resourceAllocationSettingVirtualCDDVD.LateBoundObject["Address"] = 0;
                                resourceAllocationSettingVirtualCDDVD.LateBoundObject["Parent"] = synthethicDVD.Path;
                                resourceAllocationSettingVirtualCDDVD.LateBoundObject["HostResource"] = new[] { isoName };

                                ResourceSettings = new string[] { resourceAllocationSettingVirtualCDDVD.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                            }
                            ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                            using (var primordialResourcePoolDiskDrive = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic Disk Drive"))
                            using (var allocationCapabilitiesDiskDrive = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolDiskDrive))
                            using (var resourceAllocationSettingDataDiskDrive = ViridianUtils.GetDefaultResourceAllocationSettingData(allocationCapabilitiesDiskDrive))
                            {
                                resourceAllocationSettingDataDiskDrive.LateBoundObject["Parent"] = scsiController.Path;
                                resourceAllocationSettingDataDiskDrive.LateBoundObject["AddressOnParent"] = 1;

                                ResourceSettings = new string[] { resourceAllocationSettingDataDiskDrive.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                            }
                        }
                        ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                        using (var synthethicDiskDrive = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 17, "Microsoft:Hyper-V:Synthetic Disk Drive").First())
                        using (var primordialResourcePoolVirtualHardDisk = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Virtual Hard Disk"))
                        using (var allocationCapabilitiesVirtualHardDisk = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolVirtualHardDisk))
                        using (var resourceAllocationSettingVirtualHardDisk = ViridianUtils.GetDefaultStorageAllocationSettingData(allocationCapabilitiesVirtualHardDisk))
                        {
                            using (var vhdsd = VirtualHardDiskSettingData.CreateInstance())
                            {
                                vhdsd.LateBoundObject["Type"] = VirtualHardDiskSettingData.TypeValues.Dynamic;
                                vhdsd.LateBoundObject["Format"] = VirtualHardDiskSettingData.FormatValues.VHDX;
                                vhdsd.LateBoundObject["Path"] = vhdxName;
                                vhdsd.LateBoundObject["ParentPath"] = null;
                                vhdsd.LateBoundObject["MaxInternalSize"] = 1024UL * 1024 * 1024 * 100;

                                var VirtualDiskSettingData = vhdsd.LateBoundObject.GetText(TextFormat.WmiDtd20);
                                viridianUtils.IMS.CreateVirtualHardDisk(VirtualDiskSettingData, out Job);

                                ViridianUtils.WaitForStorageJobToEnd(Job);
                            }

                            resourceAllocationSettingVirtualHardDisk.LateBoundObject["Access"] = 3; // read/write
                            resourceAllocationSettingVirtualHardDisk.LateBoundObject["Address"] = 0;
                            resourceAllocationSettingVirtualHardDisk.LateBoundObject["Parent"] = synthethicDiskDrive.Path.Path;
                            resourceAllocationSettingVirtualHardDisk.LateBoundObject["HostResource"] = new[] { vhdxName };

                            ResourceSettings = new string[] { resourceAllocationSettingVirtualHardDisk.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                        }
                        ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                        ReturnValue = computerSystem.RequestStateChange(2, null, out Job);

                        ViridianUtils.WaitForConcreteJobToEnd(Job);

                        using (var guestServiceInterfaceComponentSettingData = ViridianUtils.GetGuestServiceInterfaceComponentSettingData(virtualSystemSettingData))
                        {
                            guestServiceInterfaceComponentSettingData.LateBoundObject["EnabledState"] = GuestServiceInterfaceComponentSettingData.EnabledStateValues.Enabled;

                            var GuestServiceSettings = new string[1] { guestServiceInterfaceComponentSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                            ReturnValue = viridianUtils.VSMS.ModifyGuestServiceSettings(GuestServiceSettings, out Job, out ManagementPath[] ResultingGuestServices);
                        }

                        ViridianUtils.WaitForConcreteJobToEnd(Job);

                        using (var guestServiceInterfaceComponent = ViridianUtils.GetGuestServiceInterfaceComponent(computerSystem))
                        using (var guestFileService = ViridianUtils.GetGuestFileService(guestServiceInterfaceComponent))
                        using (var copyFileToGuestSettingData = CopyFileToGuestSettingData.CreateInstance())
                        {
                            copyFileToGuestSettingData.DestinationPath = @"C:\finished";
                            copyFileToGuestSettingData.OverwriteExisting = false;
                            copyFileToGuestSettingData.SourcePath = @"C:\copyme";
                            copyFileToGuestSettingData.CreateFullPath = true;

                            // this check might still be a bit too early but.. heh
                            // right now I do not know a decent way to check if account set up finished
                            var retries = 20;
                            var retryWaitTime = 1; // minutes
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
                        }

                        ReturnValue = computerSystem.RequestStateChange(3, null, out Job);

                        ViridianUtils.WaitForConcreteJobToEnd(Job);

                        viridianUtils.Dispose(); // force destroy system

                        var vhdxMO = ResultingResourceSettings[0];

                        var jobsAffectingVHDX = 
                            AffectedJobElement.GetInstances()
                                .Where((aje) => string.Compare(aje.AffectedElement.Path, vhdxMO.Path, true, CultureInfo.InvariantCulture) == 0)
                                .Select((aje) => aje.AffectingElement)
                                .ToList();

                        jobsAffectingVHDX.ForEach((job) => ViridianUtils.WaitForConcreteJobToEnd(Job));

                        File.Delete(vhdxName);
                    }

                }

                File.Delete(isoName);
            }
        }
    }
}
