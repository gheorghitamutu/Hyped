using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Root.Microsoft.Windows.Storage.MSFT;
using Viridian.Root.Virtualization.v2.Msvm.Networking;
using Viridian.Root.Virtualization.v2.Msvm.Storage;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;

namespace ViridianTester.Msvm.ResourceManagement
{
    [TestClass]
    public class ResourceAllocationSettingDataTest
    {
        [TestMethod]
        public void CreatingSCSIController_ExpectingOneRASDOfTypeSCSIController()
        {
            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                using (var primordialResourcePool = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic SCSI Controller"))
                using (var allocationCapabilities = ViridianUtils.GetAllocationCapabilities(primordialResourcePool))
                using (var resourceAllocationSettingData = ViridianUtils.GetDefaultResourceAllocationSettingData(allocationCapabilities))
                using (var computerSystem = new ComputerSystem(ResultingSystem))
                using (var virtualSystemSettingData = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem).First())
                {
                    var AffectedConfiguration = virtualSystemSettingData.Path;
                    var ResourceSettings = new string[] { resourceAllocationSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                    ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettings);

                    var rasdCollection = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 6, "Microsoft:Hyper-V:Synthetic SCSI Controller");

                    Assert.AreEqual(0U, ReturnValue);
                    Assert.AreEqual(1, rasdCollection.Count);
                    Assert.AreEqual(1, ResultingResourceSettings.Length);
                }
            }
        }

        [TestMethod]
        public void CreatingSynthethicDVD_ExpectingOneRASDOfTypeSynthethicDVD()
        {
            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                using (var primordialResourcePoolSCSI = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic SCSI Controller"))
                using (var allocationCapabilitiesSCSIController = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolSCSI))
                using (var resourceAllocationSettingDataSCSIController = ViridianUtils.GetDefaultResourceAllocationSettingData(allocationCapabilitiesSCSIController))
                using (var computerSystem = new ComputerSystem(ResultingSystem))
                using (var virtualSystemSettingData = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem).First())
                {
                    var AffectedConfiguration = virtualSystemSettingData.Path;
                    var ResourceSettings = new string[] { resourceAllocationSettingDataSCSIController.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                    ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettings);

                    using (var scsiController = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 6, "Microsoft:Hyper-V:Synthetic SCSI Controller").First())
                    using (var primordialResourcePoolDVDDrive = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic DVD Drive"))
                    using (var allocationCapabilitiesDVDDrive = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolDVDDrive))
                    using (var resourceAllocationSettingDataDVDDrive = ViridianUtils.GetDefaultResourceAllocationSettingData(allocationCapabilitiesDVDDrive))
                    {
                        resourceAllocationSettingDataDVDDrive.LateBoundObject["Parent"] = scsiController.Path;
                        resourceAllocationSettingDataDVDDrive.LateBoundObject["AddressOnParent"] = 0;

                        ResourceSettings = new string[] { resourceAllocationSettingDataDVDDrive.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                    }
                    ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                    var synthethicDVDCollection = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 16, "Microsoft:Hyper-V:Synthetic DVD Drive");

                    Assert.AreEqual(0U, ReturnValue);
                    Assert.AreEqual(1, ResultingResourceSettings.Length);
                    Assert.AreEqual(1, synthethicDVDCollection.Count);
                }
            }
        }

        [TestMethod]
        public void CreatingVirtualCDDVD_ExpectingOneRASDOfTypeVirtualCDDVD()
        {
            var isoName = $"{AppDomain.CurrentDomain.BaseDirectory}\\{ViridianUtils.GetCurrentMethod()}.iso";

            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                using (var primordialResourcePoolSCSI = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic SCSI Controller"))
                using (var allocationCapabilitiesSCSIController = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolSCSI))
                using (var resourceAllocationSettingDataSCSIController = ViridianUtils.GetDefaultResourceAllocationSettingData(allocationCapabilitiesSCSIController))
                using (var computerSystem = new ComputerSystem(ResultingSystem))
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
                        using (var isoFile = File.Create(isoName))
                        {
                            isoFile.Close();

                            resourceAllocationSettingVirtualCDDVD.LateBoundObject["Address"] = 0;
                            resourceAllocationSettingVirtualCDDVD.LateBoundObject["Parent"] = synthethicDVD.Path;
                            resourceAllocationSettingVirtualCDDVD.LateBoundObject["HostResource"] = new[] { isoName };

                            ResourceSettings = new string[] { resourceAllocationSettingVirtualCDDVD.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                            ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                            var virtualCDDVDCollection =
                                ViridianUtils.GetStorageAllocationSettingDataList(virtualSystemSettingData, 31, "Microsoft:Hyper-V:Virtual CD/DVD Disk")
                                    .Where((sasd) => string.Compare(sasd.Caption, "ISO Disk Image", true, CultureInfo.InvariantCulture) == 0)
                                    .ToList();

                            Assert.AreEqual(0U, ReturnValue);
                            Assert.AreEqual(1, ResultingResourceSettings.Length);
                            Assert.AreEqual(1, virtualCDDVDCollection.Count);

                            // reverse resource removal

                            var RemoveResourceSettings = new ManagementPath[] { virtualCDDVDCollection.First().Path };
                            ReturnValue = viridianUtils.VSMS.RemoveResourceSettings(RemoveResourceSettings, out Job);

                            File.Delete(isoName);

                            RemoveResourceSettings = new ManagementPath[] { synthethicDVD.Path };
                            ReturnValue = viridianUtils.VSMS.RemoveResourceSettings(RemoveResourceSettings, out Job);

                            RemoveResourceSettings = new ManagementPath[] { scsiController.Path };
                            ReturnValue = viridianUtils.VSMS.RemoveResourceSettings(RemoveResourceSettings, out Job);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void CreatingSynthethicDiskDrive_ExpectingOneRASDOfTypeSynthethicDiskDrive()
        {
            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                using (var primordialResourcePoolSCSI = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic SCSI Controller"))
                using (var allocationCapabilitiesSCSIController = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolSCSI))
                using (var resourceAllocationSettingDataSCSIController = ViridianUtils.GetDefaultResourceAllocationSettingData(allocationCapabilitiesSCSIController))
                using (var computerSystem = new ComputerSystem(ResultingSystem))
                using (var virtualSystemSettingData = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem).First())
                {
                    var AffectedConfiguration = virtualSystemSettingData.Path;
                    var ResourceSettings = new string[] { resourceAllocationSettingDataSCSIController.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                    ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettings);

                    using (var scsiController = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 6, "Microsoft:Hyper-V:Synthetic SCSI Controller").First())
                    {
                        using (var primordialResourcePoolDiskDrive = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic Disk Drive"))
                        using (var allocationCapabilitiesDiskDrive = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolDiskDrive))
                        using (var resourceAllocationSettingDataDiskDrive = ViridianUtils.GetDefaultResourceAllocationSettingData(allocationCapabilitiesDiskDrive))
                        {
                            resourceAllocationSettingDataDiskDrive.LateBoundObject["Parent"] = scsiController.Path;
                            resourceAllocationSettingDataDiskDrive.LateBoundObject["AddressOnParent"] = 0;

                            ResourceSettings = new string[] { resourceAllocationSettingDataDiskDrive.LateBoundObject.GetText(TextFormat.WmiDtd20) };

                        }
                        ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                        var synthethicDiskDriveCollection = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 17, "Microsoft:Hyper-V:Synthetic Disk Drive");

                        Assert.AreEqual(0U, ReturnValue);
                        Assert.AreEqual(1, ResultingResourceSettings.Length);
                        Assert.AreEqual(1, synthethicDiskDriveCollection.Count);
                    }
                }
            }
        }

        [TestMethod]
        public void CreatingVirtualHardDisk_ExpectingOneRASDOfTypeVirtualHardDisk()
        {
            var vhdxName = $"{AppDomain.CurrentDomain.BaseDirectory}\\{nameof(CreatingVirtualCDDVD_ExpectingOneRASDOfTypeVirtualCDDVD)}.vhdx";

            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                using (var primordialResourcePoolSCSI = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic SCSI Controller"))
                using (var allocationCapabilitiesSCSIController = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolSCSI))
                using (var resourceAllocationSettingDataSCSIController = ViridianUtils.GetDefaultResourceAllocationSettingData(allocationCapabilitiesSCSIController))
                using (var computerSystem = new ComputerSystem(ResultingSystem))
                using (var virtualSystemSettingData = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem).First())
                {
                    var AffectedConfiguration = virtualSystemSettingData.Path;
                    var ResourceSettings = new string[] { resourceAllocationSettingDataSCSIController.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                    ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettings);

                    using (var scsiController = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 6, "Microsoft:Hyper-V:Synthetic SCSI Controller").First())
                    {
                        using (var primordialResourcePoolDiskDrive = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic Disk Drive"))
                        using (var allocationCapabilitiesDiskDrive = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolDiskDrive))
                        using (var resourceAllocationSettingDataDiskDrive = ViridianUtils.GetDefaultResourceAllocationSettingData(allocationCapabilitiesDiskDrive))
                        {
                            resourceAllocationSettingDataDiskDrive.LateBoundObject["Parent"] = scsiController.Path;
                            resourceAllocationSettingDataDiskDrive.LateBoundObject["AddressOnParent"] = 0;

                            ResourceSettings = new string[] { resourceAllocationSettingDataDiskDrive.LateBoundObject.GetText(TextFormat.WmiDtd20) };

                        }
                        ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                        using (var synthethicDiskDrive = ViridianUtils.GetResourceAllocationgSettingData(virtualSystemSettingData, 17, "Microsoft:Hyper-V:Synthetic Disk Drive").First())
                        {
                            using (var primordialResourcePoolVirtualHardDisk = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Virtual Hard Disk"))
                            using (var allocationCapabilitiesVirtualHardDisk = ViridianUtils.GetAllocationCapabilities(primordialResourcePoolVirtualHardDisk))
                            using (var resourceAllocationSettingVirtualHardDisk = ViridianUtils.GetDefaultStorageAllocationSettingData(allocationCapabilitiesVirtualHardDisk))
                            using (var vhdsd = VirtualHardDiskSettingData.CreateInstance()) // operations on the host
                            {
                                vhdsd.LateBoundObject["Type"] = VirtualHardDiskSettingData.TypeValues.Dynamic;
                                vhdsd.LateBoundObject["Format"] = VirtualHardDiskSettingData.FormatValues.VHDX;
                                vhdsd.LateBoundObject["Path"] = vhdxName;
                                vhdsd.LateBoundObject["ParentPath"] = null;
                                vhdsd.LateBoundObject["MaxInternalSize"] = 1024 * 1024 * 1024;

                                var VirtualDiskSettingData = vhdsd.LateBoundObject.GetText(TextFormat.WmiDtd20);
                                viridianUtils.IMS.CreateVirtualHardDisk(VirtualDiskSettingData, out Job);

                                ViridianUtils.WaitForStorageJobToEnd(Job);

                                var AssignDriveLetter = false;
                                var Path = vhdxName;
                                var ReadOnly = false;
                                ReturnValue = viridianUtils.IMS.AttachVirtualHardDisk(AssignDriveLetter, Path, ReadOnly, out Job);

                                ViridianUtils.WaitForStorageJobToEnd(Job);

                                using (var disk =
                                    Disk.GetInstances()
                                        .Where((d) => string.Compare(d.Location, vhdxName, true, CultureInfo.InvariantCulture) == 0)
                                        .ToList()
                                        .First())
                                {
                                    ushort PartitionStyle = (ushort)Disk.PartitionStyleValues.GPT;
                                    disk.Initialize(PartitionStyle, out ManagementBaseObject ExtendedStatus);

                                    var Alignment = 0U;
                                    AssignDriveLetter = false;
                                    var DriveLetter = '\0';
                                    var GptType = "{EBD0A0A2-B9E5-4433-87C0-68B6B72699C7}"; // https://en.wikipedia.org/wiki/GUID_Partition_Table
                                    var IsActive = false;
                                    var IsHidden = false;
                                    var MbrType = (ushort)Partition.MbrTypeValues.Huge;
                                    var Offset = 0U;
                                    var Size = 0UL;
                                    var UseMaximumSize = true;

                                    ReturnValue =
                                        disk.CreatePartition(
                                            Alignment,
                                            AssignDriveLetter,
                                            DriveLetter,
                                            GptType,
                                            IsActive,
                                            IsHidden,
                                            MbrType,
                                            Offset,
                                            Size,
                                            UseMaximumSize,
                                            out ManagementBaseObject CreatePartition,
                                            out ExtendedStatus);

                                    if (ExtendedStatus != null)
                                    {
                                        using (var storageExtendedStatus = new StorageExtendedStatus(ExtendedStatus))
                                        {
                                            Trace.WriteLine($"{nameof(storageExtendedStatus.CIMStatusCode)}[{storageExtendedStatus.CIMStatusCode}]");
                                            Trace.WriteLine($"{nameof(storageExtendedStatus.CIMStatusCodeDescription)}[{storageExtendedStatus.CIMStatusCodeDescription}]");
                                            Trace.WriteLine($"{nameof(storageExtendedStatus.ErrorSource)}[{storageExtendedStatus.ErrorSource}]");
                                            Trace.WriteLine($"{nameof(storageExtendedStatus.ErrorSourceFormat)}[{storageExtendedStatus.ErrorSourceFormat}]");
                                            Trace.WriteLine($"{nameof(storageExtendedStatus.Message)}[{storageExtendedStatus.Message}]");
                                            Trace.WriteLine($"{nameof(storageExtendedStatus.MessageArguments)}[{storageExtendedStatus.MessageArguments}]");
                                            Trace.WriteLine($"{nameof(storageExtendedStatus.PerceivedSeverity)}[{storageExtendedStatus.PerceivedSeverity}]");
                                            Trace.WriteLine($"{nameof(storageExtendedStatus.ProbableCause)}[{storageExtendedStatus.ProbableCause}]");
                                            Trace.WriteLine($"{nameof(storageExtendedStatus.ProbableCauseDescription)}[{storageExtendedStatus.ProbableCauseDescription}]");
                                            Trace.WriteLine($"{nameof(storageExtendedStatus.RecommendedActions)}[{storageExtendedStatus.RecommendedActions}]");
                                        }
                                    }

                                    // you could also use "out ManagementBaseObject CreatePartition" from CreatePartition call but this is used as example
                                    // filter Microsoft Reserved Partition (EBD0A0A2-B9E5-4433-87C0-68B6B72699C7) created when you call Disk.Initialize()
                                    // => get only Basic data partition (EBD0A0A2-B9E5-4433-87C0-68B6B72699C7)
                                    using (var partition =
                                        DiskToPartition.GetInstances()
                                            .Cast<DiskToPartition>()
                                            .Where((dtp) =>
                                            string.Compare(new Disk(dtp.Disk).ObjectId, disk.ObjectId, true, CultureInfo.InvariantCulture) == 0 &&
                                            string.Compare(new Partition(dtp.Partition).GptType, "{EBD0A0A2-B9E5-4433-87C0-68B6B72699C7}", true, CultureInfo.InvariantCulture) == 0)
                                            .Select((dtp) => new Partition(dtp.Partition))
                                            .ToList()
                                            .First())
                                    using (var volume =
                                        PartitionToVolume.GetInstances()
                                            .Cast<PartitionToVolume>()
                                            .Where((ptv) => string.Compare(new Partition(ptv.Partition).ObjectId, partition.ObjectId, true, CultureInfo.InvariantCulture) == 0)
                                            .Select((ptv) => new Volume(ptv.Volume))
                                            .ToList()
                                            .First())
                                    {
                                        uint AllocationUnitSize = 4096;
                                        bool Compress = true;
                                        bool DisableHeatGathering = false;
                                        string FileSystem = "NTFS";
                                        string FileSystemLabel = nameof(CreatingVirtualHardDisk_ExpectingOneRASDOfTypeVirtualHardDisk);
                                        bool Force = true;
                                        bool Full = true;
                                        bool IsDAX = false;
                                        bool RunAsJob = false;
                                        bool SetIntegrityStreams = false;
                                        bool ShortFileNameSupport = true;
                                        bool UseLargeFRS = false;

                                        volume.Format(
                                            AllocationUnitSize,
                                            Compress,
                                            DisableHeatGathering,
                                            FileSystem,
                                            FileSystemLabel,
                                            Force,
                                            Full,
                                            IsDAX,
                                            RunAsJob,
                                            SetIntegrityStreams,
                                            ShortFileNameSupport,
                                            UseLargeFRS,
                                            out ManagementPath CreatedStorageJob,
                                            out ExtendedStatus,
                                            out ManagementBaseObject FormattedVolume);
                                    }
                                }

                                ushort CriterionType = 2;
                                var SelectionCriterion = vhdxName;
                                viridianUtils.IMS.FindMountedStorageImageInstance(CriterionType, SelectionCriterion, out ManagementPath Image);

                                using (var mountedStorageImage = new MountedStorageImage(Image))
                                {
                                    mountedStorageImage.DetachVirtualHardDisk();
                                }

                                // end operations on the host

                                resourceAllocationSettingVirtualHardDisk.LateBoundObject["Access"] = 3; // read/write
                                resourceAllocationSettingVirtualHardDisk.LateBoundObject["Address"] = 0;
                                resourceAllocationSettingVirtualHardDisk.LateBoundObject["Parent"] = synthethicDiskDrive.Path.Path;
                                resourceAllocationSettingVirtualHardDisk.LateBoundObject["HostResource"] = new[] { vhdxName };

                                ResourceSettings = new string[] { resourceAllocationSettingVirtualHardDisk.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                            }
                        }
                        ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                        var virtualHardDiskCollection =
                            ViridianUtils.GetStorageAllocationSettingDataList(virtualSystemSettingData, 31, "Microsoft:Hyper-V:Virtual Hard Disk")
                                    .Where((sasd) => string.Compare(sasd.Caption, "Hard Disk Image", true, CultureInfo.InvariantCulture) == 0)
                                    .ToList();

                        Assert.IsNotNull(ResultingSystem);
                        Assert.AreEqual(0U, ReturnValue);
                        Assert.AreEqual(1, ResultingResourceSettings.Length);
                        Assert.AreEqual(1, virtualHardDiskCollection.Count);
                        File.Delete(vhdxName);
                    }
                }
            }
        }

        [TestMethod]
        public void AddEthernetConnectionToSyntheticEthernetPort_ExpectingOne()
        {
            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_VirtualEthernetSwitchSettingDataMO(
                    ViridianUtils.GetCurrentMethod(),
                    "notes",
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                using (var virtualEthernetSwitch = new VirtualEthernetSwitch(ResultingSystem))
                {
                    viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out ReturnValue,
                    out Job,
                    out ResultingSystem);

                    using (var primordialResourcePool = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Synthetic Ethernet Port"))
                    using (var allocationCapabilities = ViridianUtils.GetAllocationCapabilities(primordialResourcePool))
                    using (var syntheticEthernetPortSettingData = ViridianUtils.GetDefaultSyntheticEthernetPortSettingData(allocationCapabilities))
                    using (var computerSystem = new ComputerSystem(ResultingSystem))
                    using (var virtualSystemSettingData = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem).First())
                    {
                        syntheticEthernetPortSettingData.LateBoundObject["VirtualSystemIdentifiers"] = new string[] { Guid.NewGuid().ToString("B") };
                        syntheticEthernetPortSettingData.LateBoundObject["ElementName"] = ViridianUtils.GetCurrentMethod();
                        syntheticEthernetPortSettingData.LateBoundObject["StaticMacAddress"] = false;

                        var AffectedConfiguration = virtualSystemSettingData.Path;
                        var ResourceSettings = new string[] { syntheticEthernetPortSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                        ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettings);

                        using (var syntheticEthernetPortSettingDataResulted = new SyntheticEthernetPortSettingData(ResultingResourceSettings[0]))
                        using (var ethernetConnectionPrimordialPool = ViridianUtils.GetPrimordialResourcePool("Microsoft:Hyper-V:Ethernet Connection"))
                        using (var allocationCapabilitiesEthernetConnection = ViridianUtils.GetAllocationCapabilities(ethernetConnectionPrimordialPool))
                        using (var ethernetPortAllocationSettingData = ViridianUtils.GetDefaultEthernetPortAllocationSettingData(allocationCapabilitiesEthernetConnection))

                        {
                            ethernetPortAllocationSettingData.LateBoundObject["Parent"] = syntheticEthernetPortSettingDataResulted.Path.Path;
                            ethernetPortAllocationSettingData.LateBoundObject["HostResource"] = new string[] { virtualEthernetSwitch.Path.Path };

                            AffectedConfiguration = virtualSystemSettingData.Path;
                            ResourceSettings = new string[] { ethernetPortAllocationSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                            ReturnValue = viridianUtils.VSMS.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                            var sepsdCollection = ViridianUtils.GetSyntheticEthernetPortSettingData(virtualSystemSettingData, 10, "Microsoft:Hyper-V:Synthetic Ethernet Port");
                            var epsdCollection = ViridianUtils.GetEthernetPortAllocationSettingData(virtualSystemSettingData, 33, "Microsoft:Hyper-V:Ethernet Connection");
                            
                            Assert.AreEqual(0U, ReturnValue);
                            Assert.AreEqual(1, sepsdCollection.Count);
                            Assert.AreEqual(1, epsdCollection.Count);
                            Assert.AreEqual(1, ResultingResourceSettings.Length);
                            Assert.IsNotNull(virtualEthernetSwitch);
                        }
                    }
                }
            }
        }
    }
}