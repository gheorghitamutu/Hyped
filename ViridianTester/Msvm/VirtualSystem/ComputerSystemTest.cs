using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;

namespace ViridianTester.Msvm.VirtualSystem
{
    [TestClass]
    public class ComputerSystemTest
    {
        [TestMethod]
        public void CreateInstance_ExpectingNotNullLateBoundObject()
        {
            using (var sut = ComputerSystem.CreateInstance())
            {
                Assert.IsNotNull(sut.LateBoundObject);
            }
        }

        [TestMethod]
        public void GetInstances_ExpectingAtLeastOneElementBeingHostMachine()
        {
            var sut = ComputerSystem.GetInstances();

            Assert.IsTrue(sut.Count > 0);
            Assert.AreEqual(sut.Cast<ComputerSystem>().ToList().FirstOrDefault()?.Name, Environment.MachineName);
        }

        [TestMethod]
        public void GetInstancesWithCondition_ExpectingOneElementBeingHostMachine()
        {
            var sut = ComputerSystem.GetInstances($"Name='{Environment.MachineName}'");

            Assert.AreEqual(sut.Count, 1);
            Assert.AreEqual(sut.Cast<ComputerSystem>().ToList().FirstOrDefault()?.Name, Environment.MachineName);
        }
    }
}
