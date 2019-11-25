using System;
using System.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Exceptions;
using Viridian.Machine;
using Viridian.Service.Msvm;
using Viridian.Utilities;

namespace ViridianTester.Machine
{
    [TestClass]
    public class VMTest
    {
        const string serverName = "."; // local
        const string scopePath = @"\Root\Virtualization\V2"; // API v2 
        const string virtualSystemSubType = "Microsoft:Hyper-V:SubType:2"; // Generation 2

        [TestMethod]
        public void ViridianMachineVM_CreateVm()
        {
            // Arrange
            var vmName = "vm_test_create";

            // Act
            var sut = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            sut.CreateVm();
            sut.GetSummaryInformation();

            var vmCollection = Utils.GetVmCollection(serverName, scopePath);
            var createdVmExists = false;
            foreach (var vm in vmCollection)
            {
                if (string.Compare((string)(((ManagementObject)vm)["ElementName"]), vmName) != 0)
                    continue;

                createdVmExists = true;
                break;
            }

            // Assert
            Assert.IsTrue(createdVmExists);
            sut.RemoveVm();
        }

        [TestMethod]
        public void ViridianMachineVM_RemoveVm()
        {
            // Arrange
            var vmName = "vm_test_remove";

            // Act
            var sut = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            sut.CreateVm();
            sut.RemoveVm();

            // Assert
            Assert.ThrowsException<ViridianException>(() => sut.RemoveVm());
            Assert.ThrowsException<ViridianException>(() => sut.GetComputerSystemByName());
        }

        [TestMethod]
        public void ViridianMachineVM_SetIncrementalBackup()
        {
            // Arrange
            var vmName = "vm_test_incremental_backup";
            var status = true;

            // Act
            var sut = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            sut.CreateVm();
            sut.SetIncrementalBackup(status);

            // Assert
            Assert.IsTrue(sut.GetIncrementalBackup());
            sut.RemoveVm();
        }

        [TestMethod]
        public void ViridianMachineVM_CreateSnapshot()
        {
            // Arrange
            var vmName = "vm_test_create_snapshot";

            // Act
            var sut = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            sut.CreateVm();
            sut.CreateSnapshot(VM.SnapshotType.Full, false);

            // Assert
            Assert.AreEqual(sut.GetSnapshotList(VM.VirtualSystemTypeName.RealizedSnapshot).Count, 1);
            sut.RemoveVm();
        }

        [TestMethod]
        public void ViridianMachineVM_ApplySnapshot()
        {
            // Arrange
            var vmName = "vm_test_apply_snapshot";

            // Act
            var sut = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            sut.CreateVm();
            sut.CreateSnapshot(VM.SnapshotType.Full, false);
            var lcs = sut.GetLastCreatedSnapshot();
            var snapshotNameCreated = (string)lcs["ElementName"];
            sut.ApplySnapshot(snapshotNameCreated);

            // Assert
            Assert.AreEqual(snapshotNameCreated, sut.GetLastAppliedSnapshot()["ElementName"]);
            sut.RemoveVm();
        }

        [TestMethod]
        public void ViridianMachineVM_RequestStateChange()
        {
            // Arrange
            var vmName = "vm_test_request_state_change";
            var expectedVmState = VirtualSystemManagement.RequestedStateVM.Running;

            // Act
            var sut = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            sut.CreateVm();
            sut.RequestStateChange(expectedVmState);

            // Assert
            Assert.AreEqual(sut.GetCurrentState(), expectedVmState);
            sut.RequestStateChange(VirtualSystemManagement.RequestedStateVM.Off);
            sut.RemoveVm();
        }

        [TestMethod]
        public void ViridianMachineVM_SetBootOrderFromDevicePath()
        {
            // Arrange
            var vmName = "vm_test_set_boot_order_from_device_path";

            // Act
            var sut = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            sut.CreateVm();
            var bsoList = sut.GetBootSourceOrderedList();
            // SetBootOrderByDevicePath()

            // Assert -> TODO: change the assertion when you add Storage related implementation (boot order list will be empty until then)
            Assert.AreEqual(bsoList.Length, 0);
            sut.RemoveVm();
        }

        [TestMethod]
        public void ViridianMachineVM_SetBootOrderByIndex()
        {
            // Arrange
            var vmName = "vm_test_set_boot_order_by_index";

            // Act
            var sut = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            sut.CreateVm();
            var bsoList = sut.GetBootSourceOrderedList();
            // SetBootOrderByIndex()           

            // Assert -> TODO: change the assertion when you add Storage related implementation (boot order list will be empty until then)
            Assert.AreEqual(bsoList.Length, 0);
            sut.RemoveVm();
        }

        [TestMethod]
        public void ViridianMachineVM_SetNetworkBootPreferredProtocol()
        {
            // Arrange
            var vmName = "vm_test_set_network_boot_preferred_protocol";

            // Act
            var sut = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            sut.CreateVm();
            sut.SetNetworkBootPreferredProtocol(VM.NetworkBootPreferredProtocol.IPv6);
            var nbpp = sut.GetNetworkBootPreferredProtocol();

            // Assert
            Assert.AreEqual(nbpp, VM.NetworkBootPreferredProtocol.IPv6);
            sut.RemoveVm();
        }

        [TestMethod]
        public void ViridianMachineVM_SetPauseAfterBootFailure()
        {
            // Arrange
            var vmName = "vm_test_set_pause_after_boot_failure";

            // Act
            var sut = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            sut.CreateVm();            
            sut.SetPauseAfterBootFailure(true);

            // Assert
            Assert.AreEqual(sut.GetPauseAfterBootFailure(), true);
            sut.RemoveVm();
        }

        [TestMethod]
        public void ViridianMachineVM_SetSecureBoot()
        {
            // Arrange
            var vmName = "vm_test_set_secure_boot";

            // Act
            var sut = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            sut.CreateVm();
            sut.SetSecureBoot(false);

            // Assert
            Assert.AreEqual(sut.GetSecureBoot(), false);
            sut.RemoveVm();
        }

        [TestMethod]
        public void ViridianMachineVM_GetSummaryInfo()
        {
            // Arrange
            var vmName = "vm_test_get_summary_info";

            // Act
            var sut = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            sut.CreateVm();
            sut.RequestStateChange(VirtualSystemManagement.RequestedStateVM.Running);
            var info = sut.GetSummaryInformation();

            // Assert
            Assert.AreEqual(1, info.Length);
            Assert.AreEqual(vmName, info[0]["ElementName"]);
            Assert.AreEqual(1024UL, info[0]["MemoryUsage"]);
            sut.RequestStateChange(VirtualSystemManagement.RequestedStateVM.Off);
            sut.RemoveVm();
        }

        [TestMethod]
        public void ViridianMachineVM_GetMemorySettingData()
        {
            // Arrange
            var vmName = "vm_test_get_memory_setting_data";

            // Act
            var sut = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            sut.CreateVm();
            var memory = sut.GetMemorySettingData();

            // Assert
            Assert.AreEqual("Memory", memory["ElementName"]);
            sut.RemoveVm();
        }
    }
}
