using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Viridian.Machine;
using Viridian.Resources.Controllers;
using Viridian.Resources.Drives;
using Viridian.Storage.Virtual.Hard;
using Viridian.Utilities;
using static Viridian.Storage.Native.NativeAPI;

namespace ViridianTester.Resources.Physical
{
    [TestClass]
    public class VHDTest
    {
        const string serverName = "."; // local
        const string v2ScopePath = @"\Root\Virtualization\V2"; // API v2 
        const string storageScopePath = @"\Root\Microsoft\Windows\Storage"; // API v2 
        const string virtualSystemSubType = "Microsoft:Hyper-V:SubType:2"; // Generation 2

        [TestMethod]
        public void ViridianDVD_AddToVmSCSIDVD()
        {
            // Arrange
            var vmName = "vm_test_add_vhdx_to_synthetic_disk_drive_of_scsi_of_vm";
            var vhdxName = AppDomain.CurrentDomain.BaseDirectory + "\\dummyPath.vhdx";

            // Act
            var vm = new VM(serverName, v2ScopePath, vmName, virtualSystemSubType);
            vm.CreateVm();

            var scsi = new SCSI();
            scsi.AddToVm(vm);

            var disk = new SyntheticDisk();
            disk.AddToScsi(vm, 0, 0);

            // operations on the host

            Disk.Create(vm, vhdxName, null, HardDiskType.DynamicallyExpanding, StorageDeviceType.Vhdx, 1024 * 1024 * 1024, 0, 0, 0);
            Disk.Attach(vm, vhdxName, false, false);
            var msftDisk = Disk.GetMsftDiskFromPath(serverName, storageScopePath, vhdxName);
            Disk.Initialize(msftDisk, PartitionStyle.Gpt, serverName, v2ScopePath);
            Disk.CreatePartition(msftDisk, 0, serverName, storageScopePath, Disk.GptType.BasicData, useMaximumSize:true);
            var volume = Disk.GetMsftDiskVolume(msftDisk, 0, 0);
            Disk.FormatMsftVolume(volume, serverName, storageScopePath);
            Disk.Detach(vm, vhdxName);

            // end operations on the host

            var sut = new VHD();
            sut.AddToSyntheticDiskDrive(vm, vhdxName, 0, 0, HardDiskAccess.ReadWrite);

            // Assert
            Assert.IsTrue(File.Exists(vhdxName));
            Assert.IsTrue(VHD.IsVHDAttached(vm, 0, 0));
            vm.RemoveVm();
            File.Delete(vhdxName);
        }
    }
}
