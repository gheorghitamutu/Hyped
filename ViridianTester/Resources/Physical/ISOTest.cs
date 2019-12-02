using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.VirtualSystem;
using Viridian.Resources.Controllers;
using Viridian.Resources.Drives;
using Viridian.Resources.Physical;

namespace ViridianTester.Resources.Physical
{
    [TestClass]
    public class ISOTest
    {
        [TestMethod]
        public void ViridianDVD_AddToVmSCSIDVD()
        {
            // Arrange
            var vmName = "vm_test_add_iso_to_dvd_drive_of_scsi_of_vm";
            var isoName = AppDomain.CurrentDomain.BaseDirectory + "\\dummyPath.iso";

            // Act
            var vm = new ComputerSystem(vmName);

            var scsi = new SCSI();
            scsi.AddToVm(vm);

            var dvd = new DVD();
            dvd.AddToScsi(vm, 0, 0);

            using (var isoFile = File.Create(isoName))
            {
                isoFile.Close();

                var sut = new ISO();
                sut.AddIso(vm, isoName, 0, 0);

                var dvdDrives = vm.GetResourceAllocationSettingData(ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceType, ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType);

                // Assert
                Assert.IsTrue(File.Exists(isoName));
                Assert.AreEqual(1, dvdDrives.Count);
                Assert.IsTrue(dvd.IsISOAttached(vm, 0, 0));
                vm.DestroySystem();
                File.Delete(isoName);
            }
        }
    }
}
