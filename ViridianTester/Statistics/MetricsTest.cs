using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using Viridian.Machine;
using Viridian.Resources.Controllers;
using Viridian.Resources.Drives;
using Viridian.Resources.MSFT;
using Viridian.Resources.Network;
using Viridian.Scopes;
using Viridian.Service.Msvm;
using Viridian.Statistics;
using Viridian.Storage.Virtual.Hard;

namespace ViridianTester.Statistics
{
    [TestClass]
    public class MetricsTest
    {
        [TestMethod]
        public void ViridianStatisticsMetrics_GetAggregationMetricValueCollectionForVm()
        {
            // Arrange
            var vmName = "vm_test_get_aggregation_metric_value_collection_for_vm";

            // Act
            var vm = new ComputerSystem(vmName);

            Metric.Instance.ConfigureMetricsFlushInterval(new TimeSpan(1000));
            Metric.Instance.SetAllMetrics(vm.MsvmComputerSystem, Metric.MetricCollectionEnabled.Enable);
            vm.RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Running);

            var mapped = Metric.GetAggregationMetricValueCollection(vm.MsvmComputerSystem);

            foreach (var pair in mapped)
            {
                foreach (var p in pair.Key.Properties)
                    Trace.Write("Definition [" + p.Name?.ToString() + "]:\t\t\t\t" + p.Value?.ToString() + "\n");

                foreach (var p in pair.Value.Properties)
                    Trace.Write("Value [" + p.Name + "]:\t\t\t\t" + p.Value + "\n");

                Trace.Write("----------------------------------------\n");
            }

            // Assert
            Assert.AreEqual(ComputerSystem.EnabledStateVM.Enabled, vm.EnabledState);
            Assert.AreEqual(mapped.Count, 4);

            vm.RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Off);
            Metric.Instance.SetAllMetrics(vm.MsvmComputerSystem, Metric.MetricCollectionEnabled.Disable);
            vm.DestroySystem();
        }

        [TestMethod]
        public void ViridianStatisticsMetrics_GetBaseMetricValueCollectionForSyntheticEthernetPortsFromVm()
        {
            // Arrange
            var vmName = "vm_test_get_base_metric_value_collection_for_synthetic_ethernet_ports_from_vm";

            // Act
            var vm = new ComputerSystem(vmName);
            vm.RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Running);
            vm.ConnectVmToSwitch("Default Switch", "MyNetworkAdapter");
            NetSwitch.AddCustomFeatureSettings(vm, NetSwitch.PortFeatureType.Acl);
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
            Assert.AreEqual(vm.EnabledState, ComputerSystem.EnabledStateVM.Enabled);
            Assert.AreEqual(sut.Count, 1);

            vm.RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Off);
            vm.SetBaseMetricsForEthernetSwitchPortAclSettingData(Metric.MetricCollectionEnabled.Disable);
            vm.DestroySystem();
        }

        [TestMethod]
        public void ViridianStatisticsMetrics_GetAggregationMetricValueCollectionOfVHDXOfSyntheticDiskOfSCSIOfVM()
        {
            // Arrange
            var vmName = "vm_test_get_aggregation_metric_value_collection_of_vhdx_of_scsi_of_vm";
            var vhdxName = AppDomain.CurrentDomain.BaseDirectory + "\\test_get_aggregation_metric_value_collection_of_vhdx_of_scsi_of_vm.vhdx";

            // Act
            var vm = new ComputerSystem(vmName);

            var scsi = new SCSI();
            scsi.AddToVm(vm);

            var disk = new SyntheticDisk();
            disk.AddToScsi(vm, 0, 0);

            // operations on the host
            using (var vhdsd = ImageManagement.CreateVirtualHardDiskSettingData(ImageManagement.VirtualHardDiskType.Dynamic, ImageManagement.VirtualDiskFormat.VHDX, vhdxName, null, 1024 * 1024 * 1024))
                ImageManagement.Instance.CreateVirtualHardDisk(vhdsd.GetText(TextFormat.WmiDtd20));

            ImageManagement.Instance.AttachVirtualHardDisk(vhdxName, false, false);

            var msftDisk = new Disk(vhdxName);
            msftDisk.Initialize(Disk.DiskPartitionStyle.GPT);

            var partition = new Partition(msftDisk.CreatePartition(0, true, 0, 0, ' ', false, Partition.PartitionMBRType.None, Partition.PartitionGPTType.BasicData.Value, false, true));
            var volume = new Volume(partition.GetMsftVolume(0));

            volume.Format(Volume.VolumeFileSystem.NTFS.Value, "Test", 4096, true, true, true, true, true, false, false);

            using (var msi = ImageManagement.Instance.FindMountedStorageImageInstance(vhdxName, ImageManagement.CriterionType.Path))
            using (var op = msi.InvokeMethod("DetachVirtualHardDisk", null, null))
                Viridian.Job.Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            // end operations on the host

            Dictionary<ManagementObject, ManagementObject> mapped = null;

            var vhd = new VHD();
            var vhdxString = vhd.AddToSyntheticDiskDrive(vm, vhdxName, 0, 0, VHD.HardDiskAccess.ReadWrite); // expecting 1 element
            using (var sut = new ManagementObject(new ManagementPath(vhdxString[0])))
            {
                vm.RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Running);

                Metric.Instance.ConfigureMetricsFlushInterval(new TimeSpan(1000));
                Metric.Instance.SetAllMetrics(sut, Metric.MetricCollectionEnabled.Enable);

                mapped = Metric.GetAggregationMetricValueCollection(vm.MsvmComputerSystem);

                foreach (var pair in mapped)
                {
                    foreach (var p in pair.Key.Properties)
                        Trace.Write("Definition [" + p.Name?.ToString() + "]:\t\t\t\t" + p.Value?.ToString() + "\n");

                    foreach (var p in pair.Value.Properties)
                        Trace.Write("Value [" + p.Name + "]:\t\t\t\t" + p.Value + "\n");

                    Trace.Write("----------------------------------------\n");
                }

                vm.RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Off);
            }

            // Assert
            Assert.IsTrue(File.Exists(vhdxName));
            Assert.IsTrue(VHD.IsVHDAttached(vm, 0, 0));
            Assert.AreEqual(mapped.Count, 3);
            vm.DestroySystem();
            File.Delete(vhdxName);
        }

        [TestMethod]
        public void ViridianStatisticsMetrics_GetBaseMetricValueCollectionOfVHDXOfSyntheticDiskOfSCSIOfVM()
        {
            // Arrange
            var vmName = "vm_test_get_base_metric_value_collection_of_vhdx_of_scsi_of_vm";
            var vhdxName = AppDomain.CurrentDomain.BaseDirectory + "\\test_get_base_metric_value_collection_of_vhdx_of_scsi_of_vm.vhdx";

            // Act
            var vm = new ComputerSystem(vmName);

            var scsi = new SCSI();
            scsi.AddToVm(vm);

            var disk = new SyntheticDisk();
            disk.AddToScsi(vm, 0, 0);

            // operations on the host
            using (var vhdsd = ImageManagement.CreateVirtualHardDiskSettingData(ImageManagement.VirtualHardDiskType.Dynamic, ImageManagement.VirtualDiskFormat.VHDX, vhdxName, null, 1024 * 1024 * 1024))
                ImageManagement.Instance.CreateVirtualHardDisk(vhdsd.GetText(TextFormat.WmiDtd20));

            ImageManagement.Instance.AttachVirtualHardDisk(vhdxName, false, false);

            var msftDisk = new Disk(vhdxName);
            msftDisk.Initialize(Disk.DiskPartitionStyle.GPT);

            var partition = new Partition(msftDisk.CreatePartition(0, true, 0, 0, ' ', false, Partition.PartitionMBRType.None, Partition.PartitionGPTType.BasicData.Value, false, true));
            var volume = new Volume(partition.GetMsftVolume(0));

            volume.Format(Volume.VolumeFileSystem.NTFS.Value, "Test", 4096, true, true, true, true, true, false, false);

            using (var msi = ImageManagement.Instance.FindMountedStorageImageInstance(vhdxName, ImageManagement.CriterionType.Path))
            using (var op = msi.InvokeMethod("DetachVirtualHardDisk", null, null))
                Viridian.Job.Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            // end operations on the host

            Dictionary<ManagementObject, ManagementObject> mapped = null;

            var vhd = new VHD();
            var vhdxString = vhd.AddToSyntheticDiskDrive(vm, vhdxName, 0, 0, VHD.HardDiskAccess.ReadWrite); // expecting 1 element
            using (var sut = new ManagementObject(new ManagementPath(vhdxString[0])))
            {
                vm.RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Running);

                Metric.Instance.ConfigureMetricsFlushInterval(new TimeSpan(1000));

                foreach (ManagementObject baseMetricDef in Metric.GetAllBaseMetricDefinitions(sut))
                    Metric.Instance.SetBaseMetric(sut, baseMetricDef, Metric.MetricCollectionEnabled.Enable);

                mapped = Metric.GetBaseMetricValueCollection(sut);

                foreach (var pair in mapped)
                {
                    foreach (var p in pair.Key.Properties)
                        Trace.Write("Definition [" + p.Name?.ToString() + "]:\t\t\t\t" + p.Value?.ToString() + "\n");

                    foreach (var p in pair.Value.Properties)
                        Trace.Write("Value [" + p.Name + "]:\t\t\t\t" + p.Value + "\n");

                    Trace.Write("--------------------------------------------------------------------------------\n");
                }

                vm.RequestStateChange(VirtualSystemManagement.RequestedStateVSM.Off);
            }

            // Assert
            Assert.IsTrue(File.Exists(vhdxName));
            Assert.IsTrue(VHD.IsVHDAttached(vm, 0, 0));
            Assert.AreEqual(mapped.Count, 3);
            vm.DestroySystem();
            File.Delete(vhdxName);
        }
    }
}
