using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;

namespace ViridianTester.Resources.Network
{
    [TestClass]
    public class SyntheticEthernetAdapterTest
    {
        [TestMethod]
        public void ViridianSyntheticEthernetAdapter_AddSyntheticEthernetAdapterToVm()
        {
            // Arrange
            var vmName = "vm_test_add_synthetic_ethernet_adapter_to_vm";

            // Act
            var vm = new ComputerSystem(vmName);
            vm.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Running);
            vm.VirtualSystemSettingData.AddSyntheticEthernetAdapter("MyNetworkAdapter");

            // Assert
            Assert.AreEqual(vm.EnabledState, ComputerSystem.EnabledStateVM.Enabled);
            Assert.AreEqual(1, vm.GetSyntheticAdapterCollection().Count);
            vm.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Off);
            vm.DestroySystem();
        }
    }
}
