using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.VirtualSystem;
using Viridian.Resources.Drives;

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
            var vm = new ComputerSystem(vmName);

            vm.VirtualSystemSettingData.AddSCSIController();

            var sut = new DVD();
            sut.AddToScsi(vm, 0, 0);

            var dvdDrives = ResourceAllocationSettingData.GetRelatedResourceAllocationSettingDataCollection(vm.VirtualSystemSettingData.MsvmVirtualSystemSettingData, ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceType, ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType);

            // Assert
            Assert.AreEqual(1, dvdDrives.Count);
            vm.DestroySystem();
        }
    }
}
