using System;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Job;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;

namespace ViridianTester.Msvm.VirtualSystemManagement
{
    [TestClass]
    public class VirtualSystemSnapshotServiceTest
    {
        [TestMethod]
        public void CreateSnapshot_ExpectingOneSnapshot()
        {
            using (var vsms = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(CreateSnapshot_ExpectingOneSnapshot);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = vsms.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                using (var sut = VirtualSystemSnapshotService.GetInstances().Cast<VirtualSystemSnapshotService>().ToList().First())
                {
                    var SnapshotSettingsInstance = VirtualSystemSettingData.CreateInstance();

                    SnapshotSettingsInstance.LateBoundObject["ElementName"] = $"{nameof(CreateSnapshot_ExpectingOneSnapshot)}";
                    SnapshotSettingsInstance.LateBoundObject["SnapshotDataRoot"] = @"SnapshotDataRoot";
                    SnapshotSettingsInstance.LateBoundObject["VirtualSystemType"] = 5;

                    ManagementPath AffectedSystem = ResultingSystem;
                    ManagementPath ResultingSnapshot = null;
                    string SnapshotSettings = SnapshotSettingsInstance.LateBoundObject.GetText(TextFormat.CimDtd20);
                    ushort SnapshotType = 2;
                    ReturnValue = sut.CreateSnapshot(AffectedSystem, ref ResultingSnapshot, SnapshotSettings, SnapshotType, out Job);

                    using (ManagementObject JobObject = new ManagementObject(Job))
                    {
                        while (Validator.IsJobEnded(JobObject?["JobState"]) == false) // TODO: maybe events cand be used here?
                        {
                            Thread.Sleep(TimeSpan.FromSeconds(1));
                            JobObject.Get();
                        }

                        ComputerSystem cs = new ComputerSystem(AffectedSystem);

                        var sovsCollection = SnapshotOfVirtualSystem.GetInstances()
                            .Cast<SnapshotOfVirtualSystem>()
                            .Where((sovs) => string.Compare(sovs.Antecedent.Path, cs.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                            .ToList();

                        var mcsibCollection = MostCurrentSnapshotInBranch.GetInstances()
                            .Cast<MostCurrentSnapshotInBranch>()
                            .Where((sovs) => string.Compare(sovs.Antecedent.Path, cs.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                            .ToList();

                        Assert.IsTrue(Validator.IsJobSuccessful(JobObject?["JobState"]));
                        Assert.AreEqual(1, sovsCollection.Count);
                        Assert.AreEqual(1, mcsibCollection.Count);
                    }

                    vsms.DestroySystem(ResultingSystem, out Job);
                }
            }
        }
        [TestMethod]
        public void ApplySnapshot_ExpectingReturnZero()
        {
            using (var vsms = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(ApplySnapshot_ExpectingReturnZero);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = vsms.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                using (var sut = VirtualSystemSnapshotService.GetInstances().Cast<VirtualSystemSnapshotService>().ToList().First())
                {
                    var SnapshotSettingsInstance = VirtualSystemSettingData.CreateInstance();

                    SnapshotSettingsInstance.LateBoundObject["ElementName"] = $"{nameof(ApplySnapshot_ExpectingReturnZero)}";
                    SnapshotSettingsInstance.LateBoundObject["SnapshotDataRoot"] = @"SnapshotDataRoot";
                    SnapshotSettingsInstance.LateBoundObject["VirtualSystemType"] = 5;

                    ManagementPath AffectedSystem = ResultingSystem;
                    ManagementPath ResultingSnapshot = null;
                    string SnapshotSettings = SnapshotSettingsInstance.LateBoundObject.GetText(TextFormat.CimDtd20);
                    ushort SnapshotType = 2;
                    ReturnValue = sut.CreateSnapshot(AffectedSystem, ref ResultingSnapshot, SnapshotSettings, SnapshotType, out Job);

                    using (ManagementObject JobObject = new ManagementObject(Job))
                    {
                        while (Validator.IsJobEnded(JobObject?["JobState"]) == false) // TODO: maybe events cand be used here?
                        {
                            Thread.Sleep(TimeSpan.FromSeconds(1));
                            JobObject.Get();
                        }

                        ComputerSystem cs = new ComputerSystem(AffectedSystem);

                        var sovsCollection = SnapshotOfVirtualSystem.GetInstances()
                            .Cast<SnapshotOfVirtualSystem>()
                            .Where((sovs) => string.Compare(sovs.Antecedent.Path, cs.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                            .Select((sovs) => new VirtualSystemSettingData(sovs.Dependent))
                            .ToList();

                        ReturnValue = sut.ApplySnapshot(sovsCollection.First().Path, out Job);

                        using (ManagementObject JobObjectApplySnapshot = new ManagementObject(Job))
                        {
                            while (Validator.IsJobEnded(JobObjectApplySnapshot?["JobState"]) == false) // TODO: maybe events cand be used here?
                            {
                                Thread.Sleep(TimeSpan.FromSeconds(1));
                                JobObjectApplySnapshot.Get();
                            }

                            var lasCollection = LastAppliedSnapshot.GetInstances()
                                .Cast<LastAppliedSnapshot>()
                                .Where((las) => string.Compare(las.Antecedent.Path, cs.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                                .ToList();

                            Assert.IsTrue(Validator.IsJobSuccessful(JobObjectApplySnapshot?["JobState"]));
                            Assert.AreEqual(1, sovsCollection.Count);
                            Assert.AreEqual(4096U, ReturnValue);
                        }
                    }

                    vsms.DestroySystem(ResultingSystem, out Job);
                }
            }
        }
    }
}
