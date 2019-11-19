using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Viridian.Machine;
using Viridian.Resources.Controllers;
using Viridian.Resources.Drives;
using Viridian.Resources.Physical;
using Viridian.Utilities;

namespace ViridianTester.Resources.Physical
{
    [TestClass]
    public class ISOTest
    {
        const string serverName = "."; // local
        const string scopePath = @"\Root\Virtualization\V2"; // API v2 
        const string virtualSystemSubType = "Microsoft:Hyper-V:SubType:2"; // Generation 2

        [TestMethod]
        public void ViridianDVD_AddToVmSCSIDVD()
        {
            // Arrange
            var vmName = "vm_test_add_iso_to_dvd_drive_of_scsi_of_vm";
            var isoName = AppDomain.CurrentDomain.BaseDirectory + "\\dummyPath.iso";

            // Act
            var vm = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            vm.CreateVm();

            var scsi = new SCSI();
            scsi.AddToVm(vm);

            var dvd = new DVD();
            dvd.AddToScsi(vm, 0, 0);

            using (var isoFile = File.Create(isoName))
            {
                isoFile.Close();

                var sut = new ISO();
                sut.AddIso(vm, isoName, 0, 0);

                var scope = Utils.GetScope(serverName, scopePath);
                var rt = Utils.GetResourceType("SyntheticDVD");
                var rst = Utils.GetResourceSubType("SyntheticDVD");
                var dvdDrives = Utils.GetResourceAllocationSettingDataResourcesByTypeAndSubtype(vmName, scope, rt, rst);

                // Assert
                Assert.IsTrue(File.Exists(isoName));
                Assert.AreEqual(1, dvdDrives.Count);
                Assert.IsTrue(dvd.IsISOAttached(vm, 0, 0));
                vm.RemoveVm();
                File.Delete(isoName);
            }
        }
    }
}
