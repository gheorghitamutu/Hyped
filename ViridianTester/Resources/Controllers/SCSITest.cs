using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Machine;
using Viridian.Resources.Controllers;
using Viridian.Utilities;

namespace ViridianTester
{
    [TestClass]
    public class SCSITest
    {
        const string serverName = "."; // local
        const string scopePath = @"\Root\Virtualization\V2"; // API v2 
        const string virtualSystemSubType = "Microsoft:Hyper-V:SubType:2"; // Generation 2

        [TestMethod]
        public void ViridianResourcesSCSI_AddToVm()
        {
            // Arrange
            var vmName = "vm_test_add_scsi_controller_to_vm";

            // Act
            var vm = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            vm.CreateVm();

            var sut = new SCSI();
            sut.AddToVm(vm);

            var scope = Utils.GetScope(serverName, scopePath);
            var rt = Utils.GetResourceType("ScsiHBA");
            var rst = Utils.GetResourceSubType("ScsiHBA");
            var scsiControllers = Utils.GetResourcesByTypeAndSubtype(vmName, scope, rt, rst);

            // Assert
            Assert.AreEqual(scsiControllers.Count, 1);
            vm.RemoveVm();
        }
    }
}
