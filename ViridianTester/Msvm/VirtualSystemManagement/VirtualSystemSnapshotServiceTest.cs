using System.Globalization;
using System.Linq;
using System.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;

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

                    ComputerSystem computerSystem = new ComputerSystem(AffectedSystem);

                    var sovsCollection = SnapshotOfVirtualSystem.GetInstances()
                        .Cast<SnapshotOfVirtualSystem>()
                        .Where((sovs) => string.Compare(sovs.Antecedent.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .ToList();

                    var mcsibCollection = MostCurrentSnapshotInBranch.GetInstances()
                        .Cast<MostCurrentSnapshotInBranch>()
                        .Where((sovs) => string.Compare(sovs.Antecedent.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .ToList();

                    Assert.AreEqual(1, sovsCollection.Count);
                    Assert.AreEqual(1, mcsibCollection.Count);

                    vsms.DestroySystem(ResultingSystem, out Job);
                }
            }
        }
        [TestMethod]
        public void ApplySnapshot_ExpectingReturnLastSnapshotApplied()
        {
            using (var vsms = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var SystemSettingsObject = VirtualSystemSettingData.CreateInstance();

                SystemSettingsObject.LateBoundObject["ElementName"] = nameof(ApplySnapshot_ExpectingReturnLastSnapshotApplied);
                SystemSettingsObject.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                SystemSettingsObject.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = SystemSettingsObject.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = vsms.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                using (var sut = VirtualSystemSnapshotService.GetInstances().Cast<VirtualSystemSnapshotService>().ToList().First())
                {
                    var SnapshotSettingsInstance = VirtualSystemSettingData.CreateInstance();

                    SnapshotSettingsInstance.LateBoundObject["ElementName"] = $"{nameof(ApplySnapshot_ExpectingReturnLastSnapshotApplied)}";
                    SnapshotSettingsInstance.LateBoundObject["SnapshotDataRoot"] = @"SnapshotDataRoot";
                    SnapshotSettingsInstance.LateBoundObject["VirtualSystemType"] = 5;

                    ManagementPath AffectedSystem = ResultingSystem;
                    ManagementPath ResultingSnapshot = null;
                    string SnapshotSettings = SnapshotSettingsInstance.LateBoundObject.GetText(TextFormat.CimDtd20);
                    ushort SnapshotType = 2;
                    ReturnValue = sut.CreateSnapshot(AffectedSystem, ref ResultingSnapshot, SnapshotSettings, SnapshotType, out Job);

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

                    ComputerSystem cs = new ComputerSystem(AffectedSystem);

                    var vssdCollection = SnapshotOfVirtualSystem.GetInstances()
                        .Cast<SnapshotOfVirtualSystem>()
                        .Where((sovs) => string.Compare(sovs.Antecedent.Path, cs.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sovs) => new VirtualSystemSettingData(sovs.Dependent))
                        .ToList();

                    ReturnValue = sut.ApplySnapshot(vssdCollection.First().Path, out Job);

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

                    var lasCollection = LastAppliedSnapshot.GetInstances()
                                .Cast<LastAppliedSnapshot>()
                                .Where((las) => string.Compare(las.Antecedent.Path, cs.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                                .ToList();

                    Assert.AreEqual(1, vssdCollection.Count);
                    Assert.AreEqual(4096U, ReturnValue);

                    vsms.DestroySystem(ResultingSystem, out Job);
                }
            }
        }
    }
}
