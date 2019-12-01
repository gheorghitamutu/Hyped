using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Machine;
using Viridian.Resources.Network;
using Viridian.Service.Msvm;

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
            var vm = new VM(vmName);
            vm.RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Running);

            SyntheticEthernetAdapter.AddSyntheticAdapter(vm, "MyNetworkAdapter");

            // Assert
            Assert.AreEqual(vm.EnabledState, VM.EnabledStateVM.Enabled);
            Assert.AreEqual(1, vm.GetSyntheticAdapterCollection().Count);
            vm.RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Off);
            vm.DestroySystem();
        }
    }
}
