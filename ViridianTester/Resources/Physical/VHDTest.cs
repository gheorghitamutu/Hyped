using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Management;
using Viridian.Msvm.ResourceManagement;
using Viridian.Msvm.Storage;
using Viridian.Msvm.VirtualSystem;
using Viridian.WindowsStorageManagement.MSFT;
using static Viridian.Msvm.Storage.StorageAllocationSettingData;

namespace ViridianTester.Resources.Physical
{
    [TestClass]
    public class VHDTest
    {
        [TestMethod]
        public void ViridianDVD_AddVHDXtoSyntheticDiskDriveOfSCSIofVM()
        {
            // Arrange
            var vmName = "vm_test_add_vhdx_to_synthetic_disk_drive_of_scsi_of_vm";
            var vhdxName = AppDomain.CurrentDomain.BaseDirectory + "\\dummyPath.vhdx";

            // Act
            var sut = new ComputerSystem(vmName);
            sut.VirtualSystemSettingData.AddSCSIController();
            sut.VirtualSystemSettingData.ControllersSCSI[0].AddChild(0, ResourcePool.ResourceTypeInfo.SyntheticDiskDrive.ResourceSubType);

            // operations on the host
            using (var vhdsd = ImageManagementService.CreateVirtualHardDiskSettingData(ImageManagementService.VirtualHardDiskType.Dynamic, ImageManagementService.VirtualDiskFormat.VHDX, vhdxName, null, 1024 * 1024 * 1024))
                ImageManagementService.Instance.CreateVirtualHardDisk(vhdsd.GetText(TextFormat.WmiDtd20));

            ImageManagementService.Instance.AttachVirtualHardDisk(vhdxName, false, false);

            var msftDisk = new Disk(vhdxName);
            msftDisk.Initialize(Disk.DiskPartitionStyle.GPT);

            var partition = new Partition(msftDisk.CreatePartition(0, true, 0, 0, ' ', false, Partition.PartitionMBRType.None, Partition.PartitionGPTType.BasicData.Value, false, true));            
            var volume = new Volume(partition.GetMsftVolume(0));

            volume.Format(Volume.VolumeFileSystem.NTFS.Value, "Test", 4096, true, true, true, true, true, false, false);

            var msi = new MountedStorageImage(ImageManagementService.Instance.FindMountedStorageImageInstance(vhdxName, ImageManagementService.CriterionType.Path));
            msi.DetachVirtualHardDisk();
            // end operations on the host

            sut.VirtualSystemSettingData.ControllersSCSI[0].RASDChildren
                    .Where((child) => child.ResourceSubType == ResourcePool.ResourceTypeInfo.SyntheticDiskDrive.ResourceSubType).First()
                    .AddChild(AccessSASD.ReadWriteSupported, 0, ResourcePool.ResourceTypeInfo.VirtualHardDisk.ResourceSubType, vhdxName);

            var diskDrives =
                   sut.VirtualSystemSettingData.ControllersSCSI[0].RASDChildren
                       .Where((child) => child.ResourceSubType == ResourcePool.ResourceTypeInfo.SyntheticDiskDrive.ResourceSubType)
                       .ToList();

            // Assert
            Assert.IsTrue(File.Exists(vhdxName));
            Assert.IsTrue(diskDrives[0].SASDChildren.Where((child) => child.Caption == "Hard Disk Image").Any());
            sut.DestroySystem();
            File.Delete(vhdxName);
        }
    }
}
