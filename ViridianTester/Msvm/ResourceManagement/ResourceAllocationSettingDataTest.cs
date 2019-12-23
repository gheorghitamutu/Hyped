using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Msvm.Networking;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.Storage;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;
using Viridian.WindowsStorageManagement.MSFT;

namespace ViridianTester.Msvm.ResourceManagement
{
    [TestClass]
    public class ResourceAllocationSettingDataTest
    {
        [TestMethod]
        public void CreatingSCSIController_ExpectingOneRASDOfTypeSCSIController()
        {
            using (var sut = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(CreatingSCSIController_ExpectingOneRASDOfTypeSCSIController);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = sut.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                var primordialResourcePool =
                    ResourcePool.GetInstances()
                        .Where((rp) =>
                            rp.Primordial == true &&
                            string.Compare(rp.ResourceSubType, "Microsoft:Hyper-V:Synthetic SCSI Controller", true, CultureInfo.InvariantCulture) == 0)
                        .First();

                var primordialResourcePools =
                    ResourcePool.GetInstances()
                        .Where((rp) =>
                            rp.Primordial == true)
                        .ToList();

                primordialResourcePools.ForEach((prp) => Trace.Write(prp.ResourceSubType + "\n"));

                var allocationCapabilities =
                    ElementCapabilities.GetInstances()
                        .Cast<ElementCapabilities>()
                        .Where((ec) => string.Compare(ec.ManagedElement.Path, primordialResourcePool.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((ec) => new AllocationCapabilities(ec.Capabilities))
                        .ToList()
                        .First();

                var resourceAllocationSettingData =
                    SettingsDefineCapabilities.GetInstances()
                        .Cast<SettingsDefineCapabilities>()
                        .Where((sdc) =>
                            string.Compare(sdc.GroupComponent.Path, allocationCapabilities.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                            sdc.ValueRange == 0 &&
                            sdc.ValueRole == 0)
                        .Select((sdc) => new ResourceAllocationSettingData(sdc.PartComponent))
                        .ToList()
                        .First();

                var computerSystem = new ComputerSystem(ResultingSystem);

                virtualSystemSettingData =
                    SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();

                var AffectedConfiguration = virtualSystemSettingData.Path;
                ResourceSettings = new string[] { resourceAllocationSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };

                ReturnValue = sut.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettings);

                var rasdCollection =
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
                    .ToList();

                Assert.IsNotNull(ResultingSystem);
                Assert.AreEqual(0U, ReturnValue);
                Assert.AreEqual(1, rasdCollection.Count);
                Assert.AreEqual(1, ResultingResourceSettings.Length);

                sut.DestroySystem(ResultingSystem, out Job);
            }
        }

        [TestMethod]
        public void CreatingSynthethicDVD_ExpectingOneRASDOfTypeSynthethicDVD()
        {
            using (var sut = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(CreatingSynthethicDVD_ExpectingOneRASDOfTypeSynthethicDVD);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = sut.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

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

                virtualSystemSettingData =
                    SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();

                var AffectedConfiguration = virtualSystemSettingData.Path;
                ResourceSettings = new string[] { resourceAllocationSettingDataSCSIController.LateBoundObject.GetText(TextFormat.WmiDtd20) };

                ReturnValue = sut.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettings);

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
                ReturnValue = sut.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                var synthethicDVDCollection =
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
                    .ToList();

                Assert.IsNotNull(ResultingSystem);
                Assert.AreEqual(0U, ReturnValue);
                Assert.AreEqual(1, ResultingResourceSettings.Length);
                Assert.AreEqual(1, synthethicDVDCollection.Count);
                sut.DestroySystem(ResultingSystem, out Job);
            }
        }

        [TestMethod]
        public void CreatingVirtualCDDVD_ExpectingOneRASDOfTypeVirtualCDDVD()
        {
            using (var sut = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(CreatingVirtualCDDVD_ExpectingOneRASDOfTypeVirtualCDDVD);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = sut.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

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

                virtualSystemSettingData =
                    SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();

                var AffectedConfiguration = virtualSystemSettingData.Path;
                ResourceSettings = new string[] { resourceAllocationSettingDataSCSIController.LateBoundObject.GetText(TextFormat.WmiDtd20) };

                ReturnValue = sut.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettings);

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
                ReturnValue = sut.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

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

                var isoName = $"{AppDomain.CurrentDomain.BaseDirectory}\\{nameof(CreatingVirtualCDDVD_ExpectingOneRASDOfTypeVirtualCDDVD)}.iso";
                using (var isoFile = File.Create(isoName))
                {
                    isoFile.Close();

                    resourceAllocationSettingVirtualCDDVD.LateBoundObject["Address"] = 0;
                    resourceAllocationSettingVirtualCDDVD.LateBoundObject["Parent"] = synthethicDVD.Path;
                    resourceAllocationSettingVirtualCDDVD.LateBoundObject["HostResource"] = new[] { isoName };

                    ResourceSettings = new string[] { resourceAllocationSettingVirtualCDDVD.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                    ReturnValue = sut.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);
                    
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

                    Assert.IsNotNull(ResultingSystem);
                    Assert.AreEqual(0U, ReturnValue);
                    Assert.AreEqual(1, ResultingResourceSettings.Length);
                    Assert.AreEqual(1, virtualCDDVDCollection.Count);

                    // reverse resource removal

                    var RemoveResourceSettings = new ManagementPath[] { virtualCDDVDCollection.First().Path };
                    ReturnValue = sut.RemoveResourceSettings(RemoveResourceSettings, out Job);

                    File.Delete(isoName);

                    RemoveResourceSettings = new ManagementPath[] { synthethicDVD.Path };
                    ReturnValue = sut.RemoveResourceSettings(RemoveResourceSettings, out Job);

                    RemoveResourceSettings = new ManagementPath[] { scsiController.Path };
                    ReturnValue = sut.RemoveResourceSettings(RemoveResourceSettings, out Job);

                    sut.DestroySystem(ResultingSystem, out Job);
                }
            }
        }

        [TestMethod]
        public void CreatingSynthethicDiskDrive_ExpectingOneRASDOfTypeSynthethicDiskDrive()
        {
            using (var sut = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(CreatingSynthethicDiskDrive_ExpectingOneRASDOfTypeSynthethicDiskDrive);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = sut.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

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

                virtualSystemSettingData =
                    SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();

                var AffectedConfiguration = virtualSystemSettingData.Path;
                ResourceSettings = new string[] { resourceAllocationSettingDataSCSIController.LateBoundObject.GetText(TextFormat.WmiDtd20) };

                ReturnValue = sut.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettings);

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
                resourceAllocationSettingDataDiskDrive.LateBoundObject["AddressOnParent"] = 0;

                ResourceSettings = new string[] { resourceAllocationSettingDataDiskDrive.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                ReturnValue = sut.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                var synthethicDiskDriveCollection =
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
                    .ToList();

                Assert.IsNotNull(ResultingSystem);
                Assert.AreEqual(0U, ReturnValue);
                Assert.AreEqual(1, ResultingResourceSettings.Length);
                Assert.AreEqual(1, synthethicDiskDriveCollection.Count);
                sut.DestroySystem(ResultingSystem, out Job);
            }
        }

        [TestMethod]
        public void CreatingVirtualHardDisk_ExpectingOneRASDOfTypeVirtualHardDisk()
        {
            using (var sut = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(CreatingVirtualHardDisk_ExpectingOneRASDOfTypeVirtualHardDisk);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = sut.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

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

                virtualSystemSettingData =
                    SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();

                var AffectedConfiguration = virtualSystemSettingData.Path;
                ResourceSettings = new string[] { resourceAllocationSettingDataSCSIController.LateBoundObject.GetText(TextFormat.WmiDtd20) };

                ReturnValue = sut.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettings);

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
                resourceAllocationSettingDataDiskDrive.LateBoundObject["AddressOnParent"] = 0;

                ResourceSettings = new string[] { resourceAllocationSettingDataDiskDrive.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                ReturnValue = sut.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

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

                var vhdxName = $"{AppDomain.CurrentDomain.BaseDirectory}\\{nameof(CreatingVirtualCDDVD_ExpectingOneRASDOfTypeVirtualCDDVD)}.vhdx";

                // operations on the host
                var vhdsd = VirtualHardDiskSettingData.CreateInstance();
                vhdsd.LateBoundObject["Type"] = VirtualHardDiskSettingData.TypeValues.Dynamic;
                vhdsd.LateBoundObject["Format"] = VirtualHardDiskSettingData.FormatValues.VHDX;
                vhdsd.LateBoundObject["Path"] = vhdxName;
                vhdsd.LateBoundObject["ParentPath"] = null;
                vhdsd.LateBoundObject["MaxInternalSize"] = 1024 * 1024 * 1024;

                using (var ims = ImageManagementService.GetInstances().First())
                {
                    var VirtualDiskSettingData = vhdsd.LateBoundObject.GetText(TextFormat.WmiDtd20);

                    ims.CreateVirtualHardDisk(VirtualDiskSettingData, out Job); // this may fail sometimes, failing the entire test (slow disk write) -> TODO: add Msvm_StorageJob and fix it

                    var AssignDriveLetter = false;
                    var Path = vhdxName;
                    var ReadOnly = false;
                    ReturnValue = ims.AttachVirtualHardDisk(AssignDriveLetter, Path, ReadOnly, out Job);

                    using (ManagementObject JobObject = new ManagementObject(Job))
                    {
                        var msftDisk = new Disk(vhdxName);
                        msftDisk.Initialize(Disk.DiskPartitionStyle.GPT);

                        var partition = new Partition(msftDisk.CreatePartition(0, true, 0, 0, ' ', false, Partition.PartitionMBRType.None, Partition.PartitionGPTType.BasicData.Value, false, true));
                        var volume = new Volume(partition.GetMsftVolume(0));

                        volume.Format(Volume.VolumeFileSystem.NTFS.Value, nameof(CreatingVirtualHardDisk_ExpectingOneRASDOfTypeVirtualHardDisk), 4096, true, true, true, true, true, false, false);

                        ushort CriterionType = 2;
                        var SelectionCriterion = vhdxName;
                        ims.FindMountedStorageImageInstance(CriterionType, SelectionCriterion, out ManagementPath Image);

                        var mountedStorageImage = new MountedStorageImage(Image);
                        mountedStorageImage.DetachVirtualHardDisk();
                    }
                }
                // end operations on the host

                resourceAllocationSettingVirtualHardDisk.LateBoundObject["Access"] = 3; // read/write
                resourceAllocationSettingVirtualHardDisk.LateBoundObject["Address"] = 0;
                resourceAllocationSettingVirtualHardDisk.LateBoundObject["Parent"] = synthethicDiskDrive.Path.Path;
                resourceAllocationSettingVirtualHardDisk.LateBoundObject["HostResource"] = new[] { vhdxName };

                ResourceSettings = new string[] { resourceAllocationSettingVirtualHardDisk.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                ReturnValue = sut.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                var virtualHardDiskCollection =
                VirtualSystemSettingDataComponent.GetInstances()
                    .Cast<VirtualSystemSettingDataComponent>()
                    .Where((sds) =>
                        string.Compare(sds.GroupComponent.Path, virtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(StorageAllocationSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                    .Select((sds) => new StorageAllocationSettingData(sds.PartComponent))
                    .ToList()
                    .Where((rasd) =>
                        rasd.ResourceType == 31 &&
                        string.Compare(rasd.ResourceSubType, "Microsoft:Hyper-V:Virtual Hard Disk", true, CultureInfo.InvariantCulture) == 0 &&
                        string.Compare(rasd.Caption, "Hard Disk Image", true, CultureInfo.InvariantCulture) == 0)
                    .ToList();

                Assert.IsNotNull(ResultingSystem);
                Assert.AreEqual(0U, ReturnValue);
                Assert.AreEqual(1, ResultingResourceSettings.Length);
                Assert.AreEqual(1, virtualHardDiskCollection.Count);
                sut.DestroySystem(ResultingSystem, out Job);
                File.Delete(vhdxName);
            }
        }

        [TestMethod]
        public void AddEthernetConnectionToSyntheticEthernetPort_ExpectingOne()
        {
            using (var virtualEthernetSwitchManagementService = VirtualEthernetSwitchManagementService.GetInstances().First())
            {
                using (var virtualEthernetSwitchSettingData = VirtualEthernetSwitchSettingData.CreateInstance())
                {
                    virtualEthernetSwitchSettingData.LateBoundObject["ElementName"] = nameof(AddEthernetConnectionToSyntheticEthernetPort_ExpectingOne);
                    virtualEthernetSwitchSettingData.LateBoundObject["Notes"] = new string[] { nameof(AddEthernetConnectionToSyntheticEthernetPort_ExpectingOne) };

                    ManagementPath ReferenceConfiguration = null;
                    var SystemSettings = virtualEthernetSwitchSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);
                    var ResourceSettings = System.Array.Empty<string>();

                    virtualEthernetSwitchManagementService.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                    var virtualEthernetSwitch = new VirtualEthernetSwitch(ResultingSystem);

                    using (var sut = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
                    {
                        var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                        virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(AddEthernetConnectionToSyntheticEthernetPort_ExpectingOne);
                        virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                        virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                        ReferenceConfiguration = null;
                        ResourceSettings = null;
                        SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                        var ReturnValue = sut.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out Job, out ResultingSystem);

                        var primordialResourcePool =
                            ResourcePool.GetInstances()
                                .Where((rp) =>
                                    rp.Primordial == true &&
                                    string.Compare(rp.ResourceSubType, "Microsoft:Hyper-V:Synthetic Ethernet Port", true, CultureInfo.InvariantCulture) == 0)
                                .First();
                        
                        var allocationCapabilities =
                            ElementCapabilities.GetInstances()
                                .Cast<ElementCapabilities>()
                                .Where((ec) => string.Compare(ec.ManagedElement.Path, primordialResourcePool.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                                .Select((ec) => new AllocationCapabilities(ec.Capabilities))
                                .ToList()
                                .First();

                        var syntheticEthernetPortSettingData =
                            SettingsDefineCapabilities.GetInstances()
                                .Cast<SettingsDefineCapabilities>()
                                .Where((sdc) =>
                                    string.Compare(sdc.GroupComponent.Path, allocationCapabilities.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                                    sdc.ValueRange == 0 &&
                                    sdc.ValueRole == 0)
                                .Select((sdc) => new SyntheticEthernetPortSettingData(sdc.PartComponent))
                                .ToList()
                                .First();

                        var computerSystem = new ComputerSystem(ResultingSystem);

                        virtualSystemSettingData =
                            SettingsDefineState.GetInstances()
                                .Cast<SettingsDefineState>()
                                .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                                .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                                .ToList()
                                .First();

                        syntheticEthernetPortSettingData.LateBoundObject["VirtualSystemIdentifiers"] = new string[] { Guid.NewGuid().ToString("B") };
                        syntheticEthernetPortSettingData.LateBoundObject["ElementName"] = nameof(AddEthernetConnectionToSyntheticEthernetPort_ExpectingOne);
                        syntheticEthernetPortSettingData.LateBoundObject["StaticMacAddress"] = false;

                        var AffectedConfiguration = virtualSystemSettingData.Path;
                        ResourceSettings = new string[] { syntheticEthernetPortSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };

                        ReturnValue = sut.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettings);

                        syntheticEthernetPortSettingData = new SyntheticEthernetPortSettingData(ResultingResourceSettings[0]);

                        var ethernetConnectionPrimordialPool = 
                            ResourcePool.GetInstances()
                                .Where((rp) =>
                                    rp.Primordial == true &&
                                    string.Compare(rp.ResourceSubType, "Microsoft:Hyper-V:Ethernet Connection", true, CultureInfo.InvariantCulture) == 0)
                                .First();

                        allocationCapabilities =
                            ElementCapabilities.GetInstances()
                                .Cast<ElementCapabilities>()
                                .Where((ec) => string.Compare(ec.ManagedElement.Path, ethernetConnectionPrimordialPool.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                                .Select((ec) => new AllocationCapabilities(ec.Capabilities))
                                .ToList()
                                .First();

                        var ethernetPortAllocationSettingData =
                            SettingsDefineCapabilities.GetInstances()
                                .Cast<SettingsDefineCapabilities>()
                                .Where((sdc) =>
                                    string.Compare(sdc.GroupComponent.Path, allocationCapabilities.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                                    sdc.ValueRange == 0 &&
                                    sdc.ValueRole == 0)
                                .Select((sdc) => new EthernetPortAllocationSettingData(sdc.PartComponent))
                                .ToList()
                                .First();

                        ethernetPortAllocationSettingData.LateBoundObject["Parent"] = syntheticEthernetPortSettingData.Path.Path;
                        ethernetPortAllocationSettingData.LateBoundObject["HostResource"] = new string[] { virtualEthernetSwitch.Path.Path };

                        AffectedConfiguration = virtualSystemSettingData.Path;
                        ResourceSettings = new string[] { ethernetPortAllocationSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };

                        ReturnValue = sut.AddResourceSettings(AffectedConfiguration, ResourceSettings, out Job, out ResultingResourceSettings);

                        var sepsdCollection =
                        VirtualSystemSettingDataComponent.GetInstances()
                            .Cast<VirtualSystemSettingDataComponent>()
                            .Where((sds) =>
                                string.Compare(sds.GroupComponent.Path, virtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                                string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(SyntheticEthernetPortSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                            .Select((sds) => new SyntheticEthernetPortSettingData(sds.PartComponent))
                            .ToList()
                            .Where((rasd) =>
                                rasd.ResourceType == 10 &&
                                string.Compare(rasd.ResourceSubType, "Microsoft:Hyper-V:Synthetic Ethernet Port", true, CultureInfo.InvariantCulture) == 0)
                            .ToList();

                        var epsdCollection =
                        VirtualSystemSettingDataComponent.GetInstances()
                            .Cast<VirtualSystemSettingDataComponent>()
                            .Where((sds) =>
                                string.Compare(sds.GroupComponent.Path, virtualSystemSettingData.Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                                string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(EthernetPortAllocationSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                            .Select((sds) => new EthernetPortAllocationSettingData(sds.PartComponent))
                            .ToList()
                            .Where((rasd) =>
                                rasd.ResourceType == 33 &&
                                string.Compare(rasd.ResourceSubType, "Microsoft:Hyper-V:Ethernet Connection", true, CultureInfo.InvariantCulture) == 0)
                            .ToList();

                        Assert.IsNotNull(ResultingSystem);
                        Assert.AreEqual(0U, ReturnValue);
                        Assert.AreEqual(1, sepsdCollection.Count);
                        Assert.AreEqual(1, epsdCollection.Count);
                        Assert.AreEqual(1, ResultingResourceSettings.Length);

                        sut.DestroySystem(ResultingSystem, out Job);
                    }

                    Assert.IsNotNull(virtualEthernetSwitch);

                    virtualEthernetSwitchManagementService.DestroySystem(virtualEthernetSwitch.Path, out Job);
                }
            }
        }
    }
}
