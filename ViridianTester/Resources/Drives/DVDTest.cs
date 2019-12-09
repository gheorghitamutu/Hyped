using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
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
            vm.VirtualSystemSettingData.ControllersSCSI[0].AddChild(0, ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType);

            var dvdDrives = vm.VirtualSystemSettingData.ControllersSCSI[0].RASDChildren.Where((child) => child.ResourceSubType == ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType).ToList();

            // Assert
            Assert.AreEqual(1, dvdDrives.Count);
            vm.DestroySystem();
        }
    }
}
