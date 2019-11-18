using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Machine;
using Viridian.Resources.Controllers;
using Viridian.Resources.Drives;
using Viridian.Utilities;

namespace ViridianTester.Resources.Drives
{
    [TestClass]
    public class DVDTest
    {
        const string serverName = "."; // local
        const string scopePath = @"\Root\Virtualization\V2"; // API v2 
        const string virtualSystemSubType = "Microsoft:Hyper-V:SubType:2"; // Generation 2

        [TestMethod]
        public void ViridianDVD_AddToVm()
        {
            // Arrange
            var vmName = "vm_test_add_scsi_controller_to_vm";

            // Act
            var vm = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            vm.CreateVm();

            var scsi = new SCSI();
            scsi.AddToVm(vm);

            var sut = new DVD();
            sut.AddToScsi(vm, 0, 0);

            var scope = Utils.GetScope(serverName, scopePath);
            var rt = Utils.GetResourceType("SyntheticDVD");
            var rst = Utils.GetResourceSubType("SyntheticDVD");
            var dvdDrives = Utils.GetResourcesByTypeAndSubtype(vmName, scope, rt, rst);

            // Assert
            Assert.AreEqual(dvdDrives.Count, 1);
            vm.RemoveVm();
        }
    }
}
