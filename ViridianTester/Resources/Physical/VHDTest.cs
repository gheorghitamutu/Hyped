﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Management;
using Viridian.Machine;
using Viridian.Resources.Controllers;
using Viridian.Resources.Drives;
using Viridian.Resources.MSFT;
using Viridian.Service.Msvm;
using Viridian.Storage.Virtual.Hard;

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
        public void ViridianDVD_AddVHDXtoSyntheticDiskDriveOfSCSIofVM()
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
            using (var vhdsd = ImageManagement.CreateVirtualHardDiskSettingData(ImageManagement.VirtualHardDiskType.Dynamic, ImageManagement.VirtualDiskFormat.VHDX, vhdxName, null, 1024 * 1024 * 1024))
                ImageManagement.Instance.CreateVirtualHardDisk(vhdsd.GetText(System.Management.TextFormat.WmiDtd20));

            ImageManagement.Instance.AttachVirtualHardDisk(vhdxName, false, false);

            var msftDisk = new Disk(vhdxName);
            msftDisk.Initialize(Disk.DiskPartitionStyle.GPT);

            var partition = new Partition(msftDisk.CreatePartition(0, true, 0, 0, ' ', false, Partition.PartitionMBRType.None, Partition.PartitionGPTType.BasicData.Value, false, true));            
            var volume = new Volume(partition.GetMsftVolume(0));

            volume.Format(Volume.VolumeFileSystem.NTFS.Value, "Test", 4096, true, true, true, true, true, false, false);

            using (var msi = ImageManagement.Instance.FindMountedStorageImageInstance(vhdxName, ImageManagement.CriterionType.Path))
            using (var op = msi.InvokeMethod("DetachVirtualHardDisk", null, null))
                Viridian.Job.Validator.ValidateOutput(op, vm.Scope);

            var sut = new VHD();
            sut.AddToSyntheticDiskDrive(vm, vhdxName, 0, 0, VHD.HardDiskAccess.ReadWrite);

            // end operations on the host

            // Assert
            Assert.IsTrue(File.Exists(vhdxName));
            Assert.IsTrue(VHD.IsVHDAttached(vm, 0, 0));
            vm.RemoveVm();
            File.Delete(vhdxName);
        }
    }
}
