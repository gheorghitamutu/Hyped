using System.Linq;
using System.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Root.Virtualization.v2.Msvm.Processor;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;

namespace ViridianTester.Msvm.Processor
{
    [TestClass]
    public class ProcessorSettingDataTest
    {
        [TestMethod]
        public void GettingProcessorSettingDataListOfVirtualSystemSettingData_ExpectingOne()
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

                    var sut = ViridianUtils.GetProcessorSettingDataList(vssdCollection.First());

                    Assert.AreEqual(1, vssdCollection.Count);
                    Assert.AreEqual(1, sut.Count);

                    ReturnValue = computerSystem.RequestStateChange(3, null, out Job);
                }

                ViridianUtils.WaitForConcreteJobToEnd(Job);
            }
        }

        [TestMethod]
        public void ModifyProcessorSettingDataVirtualQuantity_ExpectingTwoCores()
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

                    var processorSettingDataList = ViridianUtils.GetProcessorSettingDataList(vssdCollection.First());

                    var processorSettingData = processorSettingDataList.First();
                    processorSettingData.LateBoundObject["VirtualQuantity"] = 2;

                    ReturnValue = computerSystem.RequestStateChange(3, null, out Job);

                    ViridianUtils.WaitForConcreteJobToEnd(Job);

                    computerSystem.UpdateObject();

                    var ResourceSettings = new string[] { processorSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                    viridianUtils.VSMS.ModifyResourceSettings(ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettins);

                    using (var sut = new ProcessorSettingData(ResultingResourceSettins[0]))
                    {
                        Assert.AreEqual(1, vssdCollection.Count);
                        Assert.AreEqual(1, processorSettingDataList.Count);
                        Assert.AreEqual(2UL, sut.VirtualQuantity);
                    }
                }
            }
        }
    }
}
