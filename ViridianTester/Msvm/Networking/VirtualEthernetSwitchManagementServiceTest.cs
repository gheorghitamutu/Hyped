using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management;
using Viridian.Root.Virtualization.v2.Msvm.Networking;

namespace ViridianTester.Msvm.Networking
{
    [TestClass]
    public class VirtualEthernetSwitchManagementServiceTest
    {
        [TestMethod]
        public void CreateInstance_ExpectingNotNullLateBoundObject()
        {
            using (var sut = VirtualEthernetSwitchManagementService.CreateInstance())
            {
                Assert.IsNotNull(sut.LateBoundObject);
            }
        }

        [TestMethod]
        public void CreateVirtualEthernetSwitch_ExpectingNotNullResultingSystem()
        {
            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_VirtualEthernetSwitchSettingDataMO(
                    ViridianUtils.GetCurrentMethod(),
                    "notes",
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                Assert.IsNotNull(ResultingSystem);
            }
        }        
    }
}
