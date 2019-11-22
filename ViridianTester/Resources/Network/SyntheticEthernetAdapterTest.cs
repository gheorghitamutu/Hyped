using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viridian.Machine;
using Viridian.Resources.Network;
using Viridian.Utilities;

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

            // Act
            var vm = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            vm.CreateVm();
            vm.RequestStateChange(VM.RequestedState.Running);

            SyntheticEthernetAdapter.AddSyntheticAdapter(vm, "MyNetworkAdapter");
            SyntheticEthernetAdapter.AddSyntheticAdapter(vm, "MyNetworkAdapter2");

            // Assert
            Assert.AreEqual(VM.RequestedState.Running, vm.GetCurrentState());
            Assert.AreEqual(2, vm.GetSyntheticAdapterCollection().Count);
            vm.RequestStateChange(VM.RequestedState.Off);
            vm.RemoveVm();
        }
    }
}
