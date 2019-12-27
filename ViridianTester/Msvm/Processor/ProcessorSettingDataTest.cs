using System.Globalization;
using System.Linq;
using System.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Root.Virtualization.v2.Msvm.Processor;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;

namespace ViridianTester.Msvm.Processor
{
    [TestClass]
    public class ProcessorSettingDataTest
    {
        [TestMethod]
        public void GettingProcessorSettingDataListOfVirtualSystemSettingData_ExpectingOne()
        {
            using (var virtualSystemManagementService = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(GettingProcessorSettingDataListOfVirtualSystemSettingData_ExpectingOne);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = virtualSystemManagementService.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                var computerSystem = new ComputerSystem(ResultingSystem);

                ReturnValue = computerSystem.RequestStateChange(2, null, out Job);

                using (ConcreteJob concreteJob = new ConcreteJob(Job))
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

                computerSystem = ComputerSystem.GetInstances($"Name='{computerSystem.Name}'").Cast<ComputerSystem>().ToList().First();

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
                            string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(ProcessorSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new ProcessorSettingData(sds.PartComponent))
                        .ToList();

                Assert.AreEqual(1, vssdCollection.Count);
                Assert.AreEqual(1, sut.Count);

                ReturnValue = computerSystem.RequestStateChange(3, null, out Job);

                using (ConcreteJob concreteJob = new ConcreteJob(Job))
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

                virtualSystemManagementService.DestroySystem(ResultingSystem, out Job);
            }
        }

        [TestMethod]
        public void ModifyProcessorSettingDataVirtualQuantity_ExpectingTwoCores()
        {
            using (var virtualSystemManagementService = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(ModifyProcessorSettingDataVirtualQuantity_ExpectingTwoCores);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = virtualSystemManagementService.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                var computerSystem = new ComputerSystem(ResultingSystem);

                ReturnValue = computerSystem.RequestStateChange(2, null, out Job);

                using (ConcreteJob concreteJob = new ConcreteJob(Job))
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

                computerSystem = ComputerSystem.GetInstances($"Name='{computerSystem.Name}'").Cast<ComputerSystem>().ToList().First();

                var vssdCollection =
                        SettingsDefineState.GetInstances()
                            .Cast<SettingsDefineState>()
                            .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                            .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                            .ToList();

                var processorSettingDataList =
                    VirtualSystemSettingDataComponent.GetInstances()
                        .Cast<VirtualSystemSettingDataComponent>()
                        .Where((sds) =>
                            string.Compare(sds.GroupComponent.Path, vssdCollection.First().Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                            string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(ProcessorSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new ProcessorSettingData(sds.PartComponent))
                        .ToList();

                var sut = processorSettingDataList.First();
                sut.LateBoundObject["VirtualQuantity"] = 2;

                ReturnValue = computerSystem.RequestStateChange(3, null, out Job);

                using (ConcreteJob concreteJob = new ConcreteJob(Job))
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

                computerSystem = ComputerSystem.GetInstances($"Name='{computerSystem.Name}'").Cast<ComputerSystem>().ToList().First();

                ResourceSettings = new string[] { sut.LateBoundObject.GetText(TextFormat.WmiDtd20)};
                virtualSystemManagementService.ModifyResourceSettings(ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettins);

                sut = new ProcessorSettingData(ResultingResourceSettins[0]);

                Assert.AreEqual(1, vssdCollection.Count);
                Assert.AreEqual(1, processorSettingDataList.Count);
                Assert.AreEqual(2UL, sut.VirtualQuantity);

                virtualSystemManagementService.DestroySystem(ResultingSystem, out Job);
            }
        }
    }
}
