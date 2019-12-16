using System;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Job;
using Viridian.Msvm.Memory;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;

namespace ViridianTester.Msvm.Memory
{
    [TestClass]
    public class MemorySettingDataTest
    {
        [TestMethod]
        public void GettingMemorySettingDataListOfVirtualSystemSettingData_ExpectingOne()
        {
            using (var virtualSystemManagementService = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(GettingMemorySettingDataListOfVirtualSystemSettingData_ExpectingOne);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = virtualSystemManagementService.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                var computerSystem = new ComputerSystem(ResultingSystem);

                ReturnValue = computerSystem.RequestStateChange(2, null, out Job);

                using (ManagementObject JobObject = new ManagementObject(Job))
                {
                    while (Validator.IsJobEnded(JobObject?["JobState"]) == false) // TODO: maybe events cand be used here? -> https://wutils.com/wmi/root/virtualization/v2/msvm_computersystem
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        JobObject.Get();
                    }

                    computerSystem = ComputerSystem.GetInstances($"Name='{computerSystem.Name}'").Cast<ComputerSystem>().ToList().First();
                }

                var vssdCollection =
                        SettingsDefineState.GetInstances()
                            .Cast<SettingsDefineState>()
                            .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                            .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                            .ToList();

                var sut =
                    VirtualSystemSettingDataComponent.GetInstances()
                        .Cast<VirtualSystemSettingDataComponent>()
                        .Where((sds) =>
                            string.Compare(sds.GroupComponent.Path, vssdCollection.First().Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                            string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(MemorySettingData)}", true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new MemorySettingData(sds.PartComponent))
                        .ToList();

                Assert.AreEqual(1, vssdCollection.Count);
                Assert.AreEqual(1, sut.Count);

                virtualSystemManagementService.DestroySystem(ResultingSystem, out Job);
            }
        }
    }
}
