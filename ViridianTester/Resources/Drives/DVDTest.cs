using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Machine;
using Viridian.Resources.Controllers;
using Viridian.Resources.Drives;
using Viridian.Resources.Msvm;

namespace ViridianTester.Resources.Drives
{
    [TestClass]
    public class DVDTest
    {
        [TestMethod]
        public void ViridianDVD_AddToVmSCSI()
        {
            // Arrange
            var vmName = "vm_test_add_dvd_drive_to_scsi_of_vm";

            // Act
            var vm = new VM(vmName);

            var scsi = new SCSI();
            scsi.AddToVm(vm);

            var sut = new DVD();
            sut.AddToScsi(vm, 0, 0);

            var dvdDrives = vm.GetResourceAllocationSettingData(ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceType, ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType);

            // Assert
            Assert.AreEqual(1, dvdDrives.Count);
            vm.DestroySystem();
        }
    }
}
