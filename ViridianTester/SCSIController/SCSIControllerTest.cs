using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management;
using Viridian.Machine;
using Viridian.Resources;
using Viridian.Utilities;

namespace ViridianTester
{
    [TestClass]
    public class SCSIControllerTest
    {
        const string serverName = "."; // local
        const string scopePath = @"\Root\Virtualization\V2"; // API v2 
        const string virtualSystemSubType = "Microsoft:Hyper-V:SubType:2"; // Generation 2

        [TestMethod]
        public void ViridianResourcesSCSIController_AddToVm()
        {
            // Arrange
            var vmName = "vm_test_add_scsi_controller_to_vm";

            // Act
            var vm = new VM();
            vm.CreateVm(serverName, scopePath, vmName, virtualSystemSubType);

            var sut = new SCSIController();
            sut.AddToVm(serverName, scopePath, vmName);

            // Assert
            Assert.AreEqual(Utils.GetResourcePools("6", "Microsoft:Hyper-V:Synthetic SCSI Controller", Utils.GetScope(serverName, scopePath)).Count, 1);
            vm.RemoveVm(serverName, scopePath, vmName);
        }
    }
}
