using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Linq;
using System.Management;
using Viridian.Root.Virtualization.v2.Msvm.Memory;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;

namespace ViridianTester.Msvm.Memory
{
    [TestClass]
    public class MemoryTest
    {
        [TestMethod]
        public void Constructor_ExpectingDefaultVirtualQuantity1024()
        {
            using (var sut = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(Constructor_ExpectingDefaultVirtualQuantity1024);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = sut.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                var computerSystem = new ComputerSystem(ResultingSystem);

                var vssdCollection =
                    SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList();

                var msdCollection =
                    VirtualSystemSettingDataComponent.GetInstances()
                        .Cast<VirtualSystemSettingDataComponent>()
                        .Where((sds) =>
                            string.Compare(sds.GroupComponent.Path, vssdCollection.First().Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                            string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(MemorySettingData)}", true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new MemorySettingData(sds.PartComponent))
                        .ToList();

                Assert.IsNotNull(ResultingSystem);
                Assert.AreEqual(0U, ReturnValue);
                Assert.AreEqual(1, vssdCollection.Count);
                Assert.AreEqual(1, msdCollection.Count);
                Assert.AreEqual(1024U, msdCollection.First().VirtualQuantity);

                sut.DestroySystem(ResultingSystem, out Job);
            }
        }
    }
}
