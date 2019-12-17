using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Threading;
using Viridian.Job;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;

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
            using (var sut = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(DefineSystem_ExpectingNotNullResultingSystemAndReturnValueIsZero);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = sut.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                using (ManagementObject JobObject = new ManagementObject(Job))
                {
                    Assert.IsNotNull(ResultingSystem);
                    Assert.AreEqual(0U, ReturnValue);
                }

                sut.DestroySystem(ResultingSystem, out Job);
            }
        }
        [TestMethod]
        public void DestroySystem_ExpectingGetInstancesWithElementNameConditionCountZero()
        {
            using (var sut = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(DestroySystem_ExpectingGetInstancesWithElementNameConditionCountZero);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = sut.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                using (var computerSystem = new ComputerSystem(ResultingSystem))
                {
                    var name = computerSystem.Name;

                    sut.DestroySystem(ResultingSystem, out Job);

                    var ReferenceConfigurationInstances = ComputerSystem.GetInstances($"Name='{name}'");

                    using (ManagementObject JobObject = new ManagementObject(Job))
                    {
                        Assert.IsNotNull(ResultingSystem);
                        Assert.AreEqual(0U, ReturnValue);
                        Assert.AreEqual(0, ReferenceConfigurationInstances.Count);
                    }
                }
            }
        }

        [TestMethod]
        public void RequestStateChange_ExpectingEnabledStateIsTwo()
        {
            using (var sut = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(RequestStateChange_ExpectingEnabledStateIsTwo);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = sut.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

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

                    Assert.IsNotNull(ResultingSystem);
                    Assert.AreEqual(4096U, ReturnValue);
                    Assert.AreEqual(2U, computerSystem.EnabledState);
                }

                ReturnValue = computerSystem.RequestStateChange(3, null, out Job); 

                using (ManagementObject JobObject = new ManagementObject(Job))
                {
                    while (Validator.IsJobEnded(JobObject?["JobState"]) == false) // TODO: maybe events cand be used here? -> https://wutils.com/wmi/root/virtualization/v2/msvm_computersystem
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        JobObject.Get();
                    }
                }
                    
                sut.DestroySystem(ResultingSystem, out Job);
            }
        }

        [TestMethod]
        public void GetVirtualSystemThumbnailImage_ExpectingNotNullImage()
        {
            using (var sut = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(GetVirtualSystemThumbnailImage_ExpectingNotNullImage);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = sut.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                var computerSystem = new ComputerSystem(ResultingSystem);
                ReturnValue = computerSystem.RequestStateChange(2, null, out Job);

                using (ManagementObject JobObject = new ManagementObject(Job))
                {
                    while (Validator.IsJobEnded(JobObject?["JobState"]) == false) // maybe events cand be used here?
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        JobObject.Get();
                    }

                    ReturnValue = sut.GetVirtualSystemThumbnailImage(1000, ResultingSystem, 1000, out byte[] ImageData);

                    Assert.IsNotNull(ResultingSystem);
                    Assert.AreEqual(0U, ReturnValue);
                    Assert.IsNotNull(ImageData);
                }

                ReturnValue = computerSystem.RequestStateChange(3, null, out Job);

                using (ManagementObject JobObject = new ManagementObject(Job))
                {
                    while (Validator.IsJobEnded(JobObject?["JobState"]) == false) // maybe events cand be used here?
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        JobObject.Get();
                    }
                }

                sut.DestroySystem(ResultingSystem, out Job);
            }
        }

        [TestMethod]
        public void GetSummaryInformation_ExpectingMemoryAvailable1024()
        {
            using (var sut = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(GetSummaryInformation_ExpectingMemoryAvailable1024);
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

                uint[] RequestedInformation = (uint[])Enum.GetValues(typeof(SummaryInformation.RequestedInformation));
                ManagementPath[] SettingData = new ManagementPath[] { vssdCollection.First().Path };

                ReturnValue = sut.GetSummaryInformation(RequestedInformation, SettingData, out ManagementBaseObject[] SummaryInformation);

                SummaryInformation summaryInformation = new SummaryInformation(SummaryInformation.First());

                Assert.IsNotNull(ResultingSystem);
                Assert.AreEqual(0U, ReturnValue);
                Assert.AreEqual(1, vssdCollection.Count);
                Assert.AreEqual(1, SummaryInformation.Length);
                Assert.IsNotNull(SummaryInformation);
                Assert.AreEqual(0U, summaryInformation.MemoryUsage);

                sut.DestroySystem(ResultingSystem, out Job);
            }
        }
    }
}
