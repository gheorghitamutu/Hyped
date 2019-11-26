using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Machine;
using Viridian.Resources.Network;
using Viridian.Service.Msvm;

namespace ViridianTester.Resources.Network
{
    [TestClass]
    public class SyntheticEthernetAdapterTest
    {
        const string serverName = "."; // local
        const string scopePath = @"\Root\Virtualization\V2"; // API v2 
        const string virtualSystemSubType = "Microsoft:Hyper-V:SubType:2"; // Generation 2

        [TestMethod]
        public void ViridianSyntheticEthernetAdapter_AddSyntheticEthernetAdapterToVm()
        {
            // Arrange
            var vmName = "vm_test_add_synthetic_ethernet_adapter_to_vm";
            var vmState = VirtualSystemManagement.RequestedStateVSM.Running;

            // Act
            var vm = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            vm.CreateVm();
            vm.RequestStateChange(vmState);

            SyntheticEthernetAdapter.AddSyntheticAdapter(vm, "MyNetworkAdapter");

            // Assert
            Assert.AreEqual(vmState, vm.GetCurrentState());
            Assert.AreEqual(1, vm.GetSyntheticAdapterCollection().Count);
            vm.RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Off);
            vm.RemoveVm();
        }
    }
}
