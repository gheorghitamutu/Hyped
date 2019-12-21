using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Management;
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

        [TestMethod]
        public void CreateVirtualEthernetSwitch_ExpectingNotNullResultingSystem()
        {
            using (var sut = VirtualEthernetSwitchManagementService.GetInstances().First())
            {
                using (var virtualEthernetSwitchSettingData = VirtualEthernetSwitchSettingData.CreateInstance())
                {
                    virtualEthernetSwitchSettingData.LateBoundObject["ElementName"] = nameof(CreateVirtualEthernetSwitch_ExpectingNotNullResultingSystem);
                    virtualEthernetSwitchSettingData.LateBoundObject["Notes"] = new string[] { nameof(CreateVirtualEthernetSwitch_ExpectingNotNullResultingSystem) };

                    ManagementPath ReferenceConfiguration = null;
                    var SystemSettings = virtualEthernetSwitchSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);
                    var ResourceSettings = System.Array.Empty<string>();

                    sut.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                    Assert.IsNotNull(ResultingSystem);

                    sut.DestroySystem(ResultingSystem, out Job);
                }
            }
        }
    }
}
