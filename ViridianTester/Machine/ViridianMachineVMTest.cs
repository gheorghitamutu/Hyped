using System.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Machine;

namespace ViridianTester
{
    [TestClass]
    public class ViridianMachineVMTest
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
            var sut = new VM();
            sut.CreateVm(serverName, scopePath, vmName, virtualSystemSubType);
            var vms = sut.GetVmCollection(serverName, scopePath);
            var createdVmExists = false;
            foreach (var vm in vms)
            {
                if (string.Compare((string)(((ManagementObject)vm)["ElementName"]), vmName) == 0)
                {
                    createdVmExists = true;
                    break;
                }
            }

            // Assert
            Assert.IsTrue(createdVmExists);
            sut.RemoveVm(serverName, scopePath, vmName);
        }

        [TestMethod]
        public void ViridianMachineVM_RemoveVm()
        {
            // Arrange
            var vmName = "vm_test_remove";

            // Act
            var sut = new VM();
            sut.CreateVm(serverName, scopePath, vmName, virtualSystemSubType);
            sut.RemoveVm(serverName, scopePath, vmName);

            var vms = sut.GetVmCollection(serverName, scopePath);
            var createdVmExists = false;
            foreach (var vm in vms)
            {
                if (string.Compare((string)(((ManagementObject)vm)["ElementName"]), vmName) == 0)
                {
                    createdVmExists = true;
                    break;
                }
            }

            // Assert
            Assert.IsFalse(createdVmExists);
        }

        [TestMethod]
        public void ViridianMachineVM_SetIncrementalBackup()
        {
            // Arrange
            var vmName = "vm_test_incremental_backup";
            var status = true;

            // Act
            var sut = new VM();
            sut.CreateVm(serverName, scopePath, vmName, virtualSystemSubType);
            sut.SetIncrementalBackup(serverName, scopePath, vmName, status);

            // Assert
            Assert.IsTrue(sut.GetIncrementalBackup(serverName, scopePath, vmName));
            sut.RemoveVm(serverName, scopePath, vmName);
        }

        [TestMethod]
        public void ViridianMachineVM_CreateSnapshot()
        {
            // Arrange
            var vmName = "vm_test_create_snapshot";

            // Act
            var sut = new VM();
            sut.CreateVm(serverName, scopePath, vmName, virtualSystemSubType);
            sut.CreateSnapshot(serverName, scopePath, vmName, VM.SnapshotType.Full, false);

            // Assert
            Assert.AreEqual(sut.GetSnapshotList(serverName, scopePath, vmName, VM.VirtualSystemTypeName.RealizedSnapshot).Count, 1);
            sut.RemoveVm(serverName, scopePath, vmName);
        }

        [TestMethod]
        public void ViridianMachineVM_ApplySnapshot()
        {
            // Arrange
            var vmName = "vm_test_apply_snapshot";

            // Act
            var sut = new VM();
            sut.CreateVm(serverName, scopePath, vmName, virtualSystemSubType);
            sut.CreateSnapshot(serverName, scopePath, vmName, VM.SnapshotType.Full, false);
            var sCreated = sut.GetLastCreatedSnapshot(serverName, scopePath, vmName);
            var snapshotNameCreated = (string)sCreated["ElementName"];
            sut.ApplySnapshot(serverName, scopePath, vmName, snapshotNameCreated);

            // Assert
            Assert.AreEqual(snapshotNameCreated, sut.GetLastAppliedSnapshot(serverName, scopePath, vmName)["ElementName"]);
            sut.RemoveVm(serverName, scopePath, vmName);
        }

        [TestMethod]
        public void ViridianMachineVM_RequestStateChange()
        {
            // Arrange
            var vmName = "vm_test_request_state_change";
            var vmState = VM.RequestedState.Running;

            // Act
            var sut = new VM();
            sut.CreateVm(serverName, scopePath, vmName, virtualSystemSubType);
            sut.RequestStateChange(serverName, scopePath, vmName, VM.RequestedState.Running);

            // Assert
            Assert.AreEqual(sut.GetCurrentState(serverName, scopePath, vmName), vmState);
            sut.RequestStateChange(serverName, scopePath, vmName, VM.RequestedState.Off);
            sut.RemoveVm(serverName, scopePath, vmName);
        }

        [TestMethod]
        public void ViridianMachineVM_SetBootOrderFromDevicePath()
        {
            // Arrange
            var vmName = "vm_test_set_boot_order_from_device_path";

            // Act
            var sut = new VM();
            sut.CreateVm(serverName, scopePath, vmName, virtualSystemSubType);
            var bootOrderList = sut.GetBootSourceOrderedList(serverName, scopePath, vmName);
            // SetBootOrderByDevicePath()           

            // Assert -> TODO: change the assertion when you add Storage related implementation (boot order list will be empty until then)
            Assert.AreEqual(bootOrderList.Length, 0);
            sut.RemoveVm(serverName, scopePath, vmName);
        }

        [TestMethod]
        public void ViridianMachineVM_SetBootOrderByIndex()
        {
            // Arrange
            var vmName = "vm_test_set_boot_order_by_index";

            // Act
            var sut = new VM();
            sut.CreateVm(serverName, scopePath, vmName, virtualSystemSubType);
            var bootOrderList = sut.GetBootSourceOrderedList(serverName, scopePath, vmName);
            // SetBootOrderByIndex()           

            // Assert -> TODO: change the assertion when you add Storage related implementation (boot order list will be empty until then)
            Assert.AreEqual(bootOrderList.Length, 0);
            sut.RemoveVm(serverName, scopePath, vmName);
        }

        [TestMethod]
        public void ViridianMachineVM_SetNetworkBootPreferredProtocol()
        {
            // Arrange
            var vmName = "vm_test_set_network_boot_preferred_protocol";

            // Act
            var sut = new VM();
            sut.CreateVm(serverName, scopePath, vmName, virtualSystemSubType);
            sut.SetNetworkBootPreferredProtocol(serverName, scopePath, vmName, VM.NetworkBootPreferredProtocol.IPv6);
            var networkBootPreferredProtocol = sut.GetNetworkBootPreferredProtocol(serverName, scopePath, vmName);

            // Assert
            Assert.AreEqual(networkBootPreferredProtocol, VM.NetworkBootPreferredProtocol.IPv6);
            sut.RemoveVm(serverName, scopePath, vmName);
        }

        [TestMethod]
        public void ViridianMachineVM_SetPauseAfterBootFailure()
        {
            // Arrange
            var vmName = "vm_test_set_pause_after_boot_failure";

            // Act
            var sut = new VM();
            sut.CreateVm(serverName, scopePath, vmName, virtualSystemSubType);            
            sut.SetPauseAfterBootFailure(serverName, scopePath, vmName, true);

            // Assert
            Assert.AreEqual(sut.GetPauseAfterBootFailure(serverName, scopePath, vmName), true);
            sut.RemoveVm(serverName, scopePath, vmName);
        }

        [TestMethod]
        public void ViridianMachineVM_SetSecureBoot()
        {
            // Arrange
            var vmName = "vm_test_set_secure_boot";

            // Act
            var sut = new VM();
            sut.CreateVm(serverName, scopePath, vmName, virtualSystemSubType);
            sut.SetSecureBoot(serverName, scopePath, vmName, false);

            // Assert
            Assert.AreEqual(sut.GetSecureBoot(serverName, scopePath, vmName), false);
            sut.RemoveVm(serverName, scopePath, vmName);
        }
    }
}
