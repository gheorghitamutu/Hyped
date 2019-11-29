using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Machine;
using Viridian.Resources.Controllers;
using Viridian.Resources.Drives;
using Viridian.Resources.Msvm;
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
        public void ViridianDVD_AddToVmSCSI()
        {
            // Arrange
            var vmName = "vm_test_add_dvd_drive_to_scsi_of_vm";

            // Act
            var vm = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            vm.CreateVm();

            var scsi = new SCSI();
            scsi.AddToVm(vm);

            var sut = new DVD();
            sut.AddToScsi(vm, 0, 0);

            var scope = Utils.GetScope(serverName, scopePath);
            var dvdDrives = Utils.GetResourceAllocationSettingDataResourcesByTypeAndSubtype(vmName, scope, ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceType, ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType);

            // Assert
            Assert.AreEqual(1, dvdDrives.Count);
            vm.RemoveVm();
        }
    }
}
