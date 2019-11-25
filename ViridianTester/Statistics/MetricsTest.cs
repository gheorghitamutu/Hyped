using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using Viridian.Machine;
using Viridian.Resources.Network;
using Viridian.Service.Msvm;
using Viridian.Statistics;

namespace ViridianTester.Statistics
{
    [TestClass]
    public class MetricsTest
    {
        const string serverName = "."; // local
        const string scopePath = @"\Root\Virtualization\V2"; // API v2 
        const string virtualSystemSubType = "Microsoft:Hyper-V:SubType:2"; // Generation 2

        [TestMethod]
        public void ViridiaStatisticsMetrics_GetAggregationMetricValueCollectionForVm()
        {
            // Arrange
            var vmName = "vm_test_get_aggregation_metric_value_collection_for_vm";
            var vmState = VirtualSystemManagement.RequestedStateVM.Running;

            // Act
            var vm = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            vm.CreateVm();

            Metric.Instance.SetAllMetrics(vm.GetComputerSystemByName(), Metric.MetricCollectionEnabled.Enable);
            Metric.Instance.ConfigureMetricsFlushInterval(new TimeSpan(1000));
            vm.RequestStateChange(vmState);

            var mapped = Metric.GetAggregationMetricValueCollection(vm.GetComputerSystemByName());

            foreach (var pair in mapped)
            {
                foreach (var p in pair.Key.Properties)
                    Trace.Write("Definition [" + p.Name?.ToString() + "]:\t\t\t\t" + p.Value?.ToString() + "\n");

                foreach (var p in pair.Value.Properties)
                    Trace.Write("Value [" + p.Name + "]:\t\t\t\t" + p.Value + "\n");

                Trace.Write("----------------------------------------\n");
            }

            // Assert
            Assert.AreEqual(vm.GetCurrentState(), vmState);
            Assert.AreEqual(mapped.Count, 4);

            vm.RequestStateChange(VirtualSystemManagement.RequestedStateVM.Off);
            Metric.Instance.SetAllMetrics(vm.GetComputerSystemByName(), Metric.MetricCollectionEnabled.Disable);
            vm.RemoveVm();
        }

        [TestMethod]
        public void ViridiaStatisticsMetrics_GetBaseMetricValueCollectionForSyntheticEthernetPortsFromVm()
        {
            // Arrange
            var vmName = "vm_test_get_base_metric_value_collection_for_synthetic_ethernet_ports_from_vm";
            var vmState = VirtualSystemManagement.RequestedStateVM.Running;

            // Act
            var vm = new VM(serverName, scopePath, vmName, virtualSystemSubType);
            vm.CreateVm();
            vm.RequestStateChange(vmState);
            vm.ConnectVmToSwitch("Default Switch", "MyNetworkAdapter");
            NetSwitch.AddCustomFeatureSettings(vm.Scope, vm.VmName, NetSwitch.PortFeatureType.Acl);
            Metric.Instance.ConfigureMetricsFlushInterval(new TimeSpan(1000));
            vm.SetBaseMetricsForEthernetSwitchPortAclSettingData(Metric.MetricCollectionEnabled.Enable);

            var port = vm.GetEthernetSwitchPortAclSettingDatas();
           
            var sut = Metric.GetBaseMetricValueCollection(port.FirstOrDefault());

            foreach (var pair in sut)
            {
                foreach (var p in pair.Key.Properties)
                    Trace.Write("Definition [" + p.Name?.ToString() + "]:\t\t\t\t" + p.Value?.ToString() + "\n");

                foreach (var p in pair.Value.Properties)
                    Trace.Write("Value [" + p.Name + "]:\t\t\t\t" + p.Value + "\n");

                Trace.Write("--------------------------------------------------------------------------------\n");
            }

            // Assert
            Assert.AreEqual(vm.GetCurrentState(), vmState);
            Assert.AreEqual(sut.Count, 1);

            vm.RequestStateChange(VirtualSystemManagement.RequestedStateVM.Off);
            vm.SetBaseMetricsForEthernetSwitchPortAclSettingData(Metric.MetricCollectionEnabled.Disable);
            vm.RemoveVm();
        }
    }
}
