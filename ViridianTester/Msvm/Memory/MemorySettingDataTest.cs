using System.Linq;
using System.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Root.Virtualization.v2.Msvm.Memory;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;

namespace ViridianTester.Msvm.Memory
{
    [TestClass]
    public class MemorySettingDataTest
    {
        [TestMethod]
        public void GettingMemorySettingDataListOfVirtualSystemSettingData_ExpectingOne()
        {
            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                using (var computerSystem = new ComputerSystem(ResultingSystem))
                {
                    ReturnValue = computerSystem.RequestStateChange(2, null, out Job);

                    ViridianUtils.WaitForConcreteJobToEnd(Job);

                    computerSystem.UpdateObject();

                    var vssdCollection = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem);

                    var sut = ViridianUtils.GetMemorySettingDataList(vssdCollection.First());

                    Assert.AreEqual(1, vssdCollection.Count);
                    Assert.AreEqual(1, sut.Count);

                    ReturnValue = computerSystem.RequestStateChange(3, null, out Job);
                }

                ViridianUtils.WaitForConcreteJobToEnd(Job);
            }
        }

        [TestMethod]
        public void ModifyMemorySettingDataVirtualQuantity_Expecting2048()
        {
            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                using (var computerSystem = new ComputerSystem(ResultingSystem))
                {
                    ReturnValue = computerSystem.RequestStateChange(2, null, out Job);

                    ViridianUtils.WaitForConcreteJobToEnd(Job);

                    computerSystem.UpdateObject();

                    var vssdCollection = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem);

                    var memorySettingDataList = ViridianUtils.GetMemorySettingDataList(vssdCollection.First());

                    using (var memorySettingData = memorySettingDataList.First())
                    {
                        memorySettingData.LateBoundObject["VirtualQuantity"] = 2048;

                        ReturnValue = computerSystem.RequestStateChange(3, null, out Job);

                        ViridianUtils.WaitForConcreteJobToEnd(Job);

                        computerSystem.UpdateObject();

                        var ResourceSettings = new string[] { memorySettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                        viridianUtils.VSMS.ModifyResourceSettings(ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettins);

                        using (var sut = new MemorySettingData(ResultingResourceSettins[0]))
                        {
                            Assert.AreEqual(1, vssdCollection.Count);
                            Assert.AreEqual(1, memorySettingDataList.Count);
                            Assert.AreEqual(2048UL, sut.VirtualQuantity);
                        }
                    }
                }
            }
        }
    }
}
