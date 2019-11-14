using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Machine;

namespace ViridianTester
{
    [TestClass]
    public class ViridianMachineJobTest
    {
        [TestMethod]
        public void ViridianMachineJob_CreateServer()
        {
            // Arrange
            string serverName = "."; // local
            string scopePath = @"\Root\Virtualization\V2"; // API v2 
            string elementName = "vm_test"; // vm name
            string virtualSystemSubType = "Microsoft:Hyper-V:SubType:2"; // Generation 2

            // Act
            var sut = new Job();
            sut.CreateServer(serverName, scopePath, elementName, virtualSystemSubType);

            // Assert
            Assert.IsNotNull(sut.GetVirtualMachine(elementName, sut.GetScope(serverName, scopePath)));
        }
    }
}
