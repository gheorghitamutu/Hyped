using System.Linq;
using System.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;

namespace ViridianTester.Msvm.VirtualSystemManagement
{
    [TestClass]
    public class VirtualSystemSnapshotServiceTest
    {
        [TestMethod]
        public void CreateSnapshot_ExpectingOneSnapshot()
        {
            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                viridianUtils.SUT_VirtualSystemSettingDataMO(
                    ViridianUtils.GetCurrentMethod(),
                    ResultingSystem,
                    out ReturnValue,
                    out Job,
                    out ManagementPath ResultingSnapshot);

                ViridianUtils.WaitForConcreteJobToEnd(Job);

                using (var computerSystem = new ComputerSystem(ResultingSystem))
                {
                    var sovsCollection = ViridianUtils.GetSnapshotOfVirtualSystemList(computerSystem);
                    var mcsibCollection = ViridianUtils.GetMostCurrentSnapshotInBranchList(computerSystem);

                    Assert.AreEqual(1, sovsCollection.Count);
                    Assert.AreEqual(1, mcsibCollection.Count);
                }
            }
        }

        [TestMethod]
        public void ApplySnapshot_ExpectingReturnLastSnapshotApplied()
        {
            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                viridianUtils.SUT_VirtualSystemSettingDataMO(
                    ViridianUtils.GetCurrentMethod(),
                    ResultingSystem,
                    out ReturnValue,
                    out Job,
                    out ManagementPath ResultingSnapshot);

                ViridianUtils.WaitForConcreteJobToEnd(Job);

                using (var computerSystem = new ComputerSystem(ResultingSystem))
                {
                    var vssdCollection = ViridianUtils.GetVirtualSystemSettingDataListThroughSnapshotOfVirtualSystem(computerSystem);

                    ReturnValue = viridianUtils.VSSS.ApplySnapshot(vssdCollection.First().Path, out Job);

                    ViridianUtils.WaitForConcreteJobToEnd(Job);

                    var lasCollection = ViridianUtils.GetLastAppliedSnapshotList(computerSystem);

                    Assert.AreEqual(1, vssdCollection.Count);
                }

                Assert.AreEqual(4096U, ReturnValue);
            }
        }
    }
}
