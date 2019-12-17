using System.Globalization;
using System.Linq;
using System.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;

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
    }
}
