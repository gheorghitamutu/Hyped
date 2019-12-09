using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.VirtualSystem;

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
            var sut = new ComputerSystem(vmName);
            sut.VirtualSystemSettingData.AddSCSIController();
            sut.VirtualSystemSettingData.ControllersSCSI[0].AddChild(0, ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType);

            using (var isoFile = File.Create(isoName))
            {
                isoFile.Close();

                sut.VirtualSystemSettingData.ControllersSCSI[0].RASDChildren
                    .Where((child) => child.ResourceSubType == ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType).First()
                    .AddChild(0, ResourcePool.ResourceTypeInfo.VirtualCDDVDDisk.ResourceSubType, isoName);

                var dvdDrives = 
                    sut.VirtualSystemSettingData.ControllersSCSI[0].RASDChildren
                        .Where((child) => child.ResourceSubType == ResourcePool.ResourceTypeInfo.SyntheticDVD.ResourceSubType)
                        .ToList();

                // Assert
                Assert.IsTrue(File.Exists(isoName));
                Assert.AreEqual(1, dvdDrives.Count);
                Assert.IsTrue(dvdDrives[0].SASDChildren.Where((child) => child.Caption == "ISO Disk Image").Any());
                sut.DestroySystem();
                File.Delete(isoName);
            }
        }
    }
}
