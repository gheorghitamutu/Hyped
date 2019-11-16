using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            var scsiControllers = Utils.GetResourcesByTypeAndSubtype(vmName, Utils.GetScope(serverName, scopePath), Utils.GetResourceType("ScsiHBA"), Utils.GetResourceSubType("ScsiHBA"));

            // Assert
            Assert.AreEqual(scsiControllers.Count, 1);
            vm.RemoveVm(serverName, scopePath, vmName);
        }
    }
}
