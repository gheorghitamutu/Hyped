using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Msvm.Networking;

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
    }
}
