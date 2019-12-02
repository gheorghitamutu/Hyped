﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;

namespace ViridianTester.Machine
{
    [TestClass]
    public class ComputerSystemTest
    {
        [TestMethod]
        public void ViridianMachineComputerSystem_CreateVm()
        {
            // Arrange
            var vmName = "vm_test_create";

            // Act
            var sut = new ComputerSystem(vmName);

            // Assert
            Assert.IsNotNull(sut.MsvmComputerSystem);
            sut.DestroySystem();
        }

        [TestMethod]
        public void ViridianMachineComputerSystem_RemoveVm()
        {
            // Arrange
            var vmName = "vm_test_remove";

            // Act
            var sut = new ComputerSystem(vmName);
            sut.DestroySystem();

            // Assert
            Assert.AreEqual(sut.MsvmComputerSystem, null);
        }

        [TestMethod]
        public void ViridianMachineComputerSystem_SetIncrementalBackup()
        {
            // Arrange
            var vmName = "vm_test_incremental_backup";
            var status = true;

            // Act
            var sut = new ComputerSystem(vmName);
            sut.SetIncrementalBackup(status);

            // Assert
            Assert.IsTrue(sut.GetIncrementalBackup());
            sut.DestroySystem();
        }

        [TestMethod]
        public void ViridianMachineComputerSystem_CreateSnapshot()
        {
            // Arrange
            var vmName = "vm_test_create_snapshot";

            // Act
            var sut = new ComputerSystem(vmName);
            sut.CreateSnapshot(ComputerSystem.SnapshotType.Full, false);

            // Assert
            Assert.AreEqual(sut.GetSnapshotList().Count, 1);
            sut.DestroySystem();
        }

        [TestMethod]
        public void ViridianMachineComputerSystem_ApplySnapshot()
        {
            // Arrange
            var vmName = "vm_test_apply_snapshot";

            // Act
            var sut = new ComputerSystem(vmName);
            sut.CreateSnapshot(ComputerSystem.SnapshotType.Full, false);
            var lcs = sut.GetLastCreatedSnapshot();
            var snapshotNameCreated = (string)lcs["ElementName"];
            sut.ApplySnapshot(snapshotNameCreated);

            // Assert
            Assert.AreEqual(snapshotNameCreated, sut.GetLastAppliedSnapshot()["ElementName"]);
            sut.DestroySystem();
        }

        [TestMethod]
        public void ViridianMachineComputerSystem_RequestStateChange()
        {
            // Arrange
            var vmName = "vm_test_request_state_change";

            // Act
            var sut = new ComputerSystem(vmName);
            sut.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Running);

            // Assert
            Assert.AreEqual(sut.EnabledState, ComputerSystem.EnabledStateVM.Enabled);
            sut.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Off);
            sut.DestroySystem();
        }

        [TestMethod]
        public void ViridianMachineComputerSystem_SetBootOrderFromDevicePath()
        {
            // Arrange
            var vmName = "vm_test_set_boot_order_from_device_path";

            // Act
            var sut = new ComputerSystem(vmName);
            var bsoList = sut.GetBootSourceOrderedList();
            // SetBootOrderByDevicePath()

            // Assert -> TODO: change the assertion when you add Storage related implementation (boot order list will be empty until then)
            Assert.AreEqual(bsoList.Length, 0);
            sut.DestroySystem();
        }

        [TestMethod]
        public void ViridianMachineComputerSystem_SetBootOrderByIndex()
        {
            // Arrange
            var vmName = "vm_test_set_boot_order_by_index";

            // Act
            var sut = new ComputerSystem(vmName);
            var bsoList = sut.GetBootSourceOrderedList();
            // SetBootOrderByIndex()           

            // Assert -> TODO: change the assertion when you add Storage related implementation (boot order list will be empty until then)
            Assert.AreEqual(bsoList.Length, 0);
            sut.DestroySystem();
        }

        [TestMethod]
        public void ViridianMachineComputerSystem_SetNetworkBootPreferredProtocol()
        {
            // Arrange
            var vmName = "vm_test_set_network_boot_preferred_protocol";

            // Act
            var sut = new ComputerSystem(vmName);
            sut.SetNetworkBootPreferredProtocol(ComputerSystem.NetworkBootPreferredProtocol.IPv6);
            var nbpp = sut.GetNetworkBootPreferredProtocol();

            // Assert
            Assert.AreEqual(nbpp, ComputerSystem.NetworkBootPreferredProtocol.IPv6);
            sut.DestroySystem();
        }

        [TestMethod]
        public void ViridianMachineComputerSystem_SetPauseAfterBootFailure()
        {
            // Arrange
            var vmName = "vm_test_set_pause_after_boot_failure";

            // Act
            var sut = new ComputerSystem(vmName);
            sut.SetPauseAfterBootFailure(true);

            // Assert
            Assert.AreEqual(sut.GetPauseAfterBootFailure(), true);
            sut.DestroySystem();
        }

        [TestMethod]
        public void ViridianMachineComputerSystem_SetSecureBoot()
        {
            // Arrange
            var vmName = "vm_test_set_secure_boot";

            // Act
            var sut = new ComputerSystem(vmName);
            sut.SetSecureBoot(false);

            // Assert
            Assert.AreEqual(sut.GetSecureBoot(), false);
            sut.DestroySystem();
        }

        [TestMethod]
        public void ViridianMachineComputerSystem_GetSummaryInfo()
        {
            // Arrange
            var vmName = "vm_test_get_summary_info";

            // Act
            var sut = new ComputerSystem(vmName);
            sut.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Running);
            var info = sut.GetSummaryInformation();

            // Assert
            Assert.AreEqual(1, info.Length);
            Assert.AreEqual(vmName, info[0]["ElementName"]);
            Assert.AreEqual(1024UL, info[0]["MemoryUsage"]);
            sut.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Off);
            sut.DestroySystem();
        }

        [TestMethod]
        public void ViridianMachineComputerSystem_GetMemorySettingData()
        {
            // Arrange
            var vmName = "vm_test_get_memory_setting_data";

            // Act
            var sut = new ComputerSystem(vmName);
            var memory = sut.GetMemorySettingData();

            // Assert
            Assert.AreEqual("Memory", memory["ElementName"]);
            sut.DestroySystem();
        }
    }
}