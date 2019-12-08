using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using Viridian.Msvm.Metrics;
using Viridian.Msvm.Storage;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;
using Viridian.Resources.Controllers;
using Viridian.Resources.Drives;
using Viridian.Resources.Network;
using Viridian.Scopes;
using Viridian.WindowsStorageManagement.MSFT;

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

            MetricService.Instance.ConfigureMetricsFlushInterval(new TimeSpan(1000));
            MetricService.Instance.SetAllMetrics(vm.MsvmComputerSystem, MetricService.MetricCollectionEnabled.Enable);
            vm.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Running);

            var mapped = MetricService.GetAggregationMetricValueCollection(vm.MsvmComputerSystem);

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

            vm.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Off);
            MetricService.Instance.SetAllMetrics(vm.MsvmComputerSystem, MetricService.MetricCollectionEnabled.Disable);
            vm.DestroySystem();
        }

        [TestMethod]
        public void ViridianStatisticsMetrics_GetBaseMetricValueCollectionForSyntheticEthernetPortsFromVm()
        {
            // Arrange
            var vmName = "vm_test_get_base_metric_value_collection_for_synthetic_ethernet_ports_from_vm";

            // Act
            var vm = new ComputerSystem(vmName);
            vm.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Running);
            vm.VirtualSystemSettingData.ConnectVmToSwitch("Default Switch", "MyNetworkAdapter");
            NetSwitch.AddCustomFeatureSettings(vm, NetSwitch.PortFeatureType.Acl);
            MetricService.Instance.ConfigureMetricsFlushInterval(new TimeSpan(1000));
            vm.VirtualSystemSettingData.SetBaseMetricsForEthernetSwitchPortAclSettingData(MetricService.MetricCollectionEnabled.Enable);

            var port = vm.VirtualSystemSettingData.GetEthernetSwitchPortAclSettingDatas();

            var sut = MetricService.GetBaseMetricValueCollection(port.FirstOrDefault());

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

            vm.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Off);
            vm.VirtualSystemSettingData.SetBaseMetricsForEthernetSwitchPortAclSettingData(MetricService.MetricCollectionEnabled.Disable);
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
            using (var vhdsd = ImageManagementService.CreateVirtualHardDiskSettingData(ImageManagementService.VirtualHardDiskType.Dynamic, ImageManagementService.VirtualDiskFormat.VHDX, vhdxName, null, 1024 * 1024 * 1024))
                ImageManagementService.Instance.CreateVirtualHardDisk(vhdsd.GetText(TextFormat.WmiDtd20));

            ImageManagementService.Instance.AttachVirtualHardDisk(vhdxName, false, false);

            var msftDisk = new Disk(vhdxName);
            msftDisk.Initialize(Disk.DiskPartitionStyle.GPT);

            var partition = new Partition(msftDisk.CreatePartition(0, true, 0, 0, ' ', false, Partition.PartitionMBRType.None, Partition.PartitionGPTType.BasicData.Value, false, true));
            var volume = new Volume(partition.GetMsftVolume(0));

            volume.Format(Volume.VolumeFileSystem.NTFS.Value, "Test", 4096, true, true, true, true, true, false, false);

            using (var msi = ImageManagementService.Instance.FindMountedStorageImageInstance(vhdxName, ImageManagementService.CriterionType.Path))
            using (var op = msi.InvokeMethod("DetachVirtualHardDisk", null, null))
                Viridian.Job.Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);
            // end operations on the host

            Dictionary<ManagementObject, ManagementObject> mapped = null;

            var vhd = new VHD();
            var vhdxString = vhd.AddToSyntheticDiskDrive(vm, vhdxName, 0, 0, VHD.HardDiskAccess.ReadWrite); // expecting 1 element
            using (var sut = new ManagementObject(new ManagementPath(vhdxString[0])))
            {
                vm.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Running);

                MetricService.Instance.ConfigureMetricsFlushInterval(new TimeSpan(1000));
                MetricService.Instance.SetAllMetrics(sut, MetricService.MetricCollectionEnabled.Enable);

                mapped = MetricService.GetAggregationMetricValueCollection(vm.MsvmComputerSystem);

                foreach (var pair in mapped)
                {
                    foreach (var p in pair.Key.Properties)
                        Trace.Write("Definition [" + p.Name?.ToString() + "]:\t\t\t\t" + p.Value?.ToString() + "\n");

                    foreach (var p in pair.Value.Properties)
                        Trace.Write("Value [" + p.Name + "]:\t\t\t\t" + p.Value + "\n");

                    Trace.Write("----------------------------------------\n");
                }

                vm.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Off);
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
            using (var vhdsd = ImageManagementService.CreateVirtualHardDiskSettingData(ImageManagementService.VirtualHardDiskType.Dynamic, ImageManagementService.VirtualDiskFormat.VHDX, vhdxName, null, 1024 * 1024 * 1024))
                ImageManagementService.Instance.CreateVirtualHardDisk(vhdsd.GetText(TextFormat.WmiDtd20));

            ImageManagementService.Instance.AttachVirtualHardDisk(vhdxName, false, false);

            var msftDisk = new Disk(vhdxName);
            msftDisk.Initialize(Disk.DiskPartitionStyle.GPT);

            var partition = new Partition(msftDisk.CreatePartition(0, true, 0, 0, ' ', false, Partition.PartitionMBRType.None, Partition.PartitionGPTType.BasicData.Value, false, true));
            var volume = new Volume(partition.GetMsftVolume(0));

            volume.Format(Volume.VolumeFileSystem.NTFS.Value, "Test", 4096, true, true, true, true, true, false, false);

            using (var msi = ImageManagementService.Instance.FindMountedStorageImageInstance(vhdxName, ImageManagementService.CriterionType.Path))
            using (var op = msi.InvokeMethod("DetachVirtualHardDisk", null, null))
                Viridian.Job.Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);
            // end operations on the host

            Dictionary<ManagementObject, ManagementObject> mapped = null;

            var vhd = new VHD();
            var vhdxString = vhd.AddToSyntheticDiskDrive(vm, vhdxName, 0, 0, VHD.HardDiskAccess.ReadWrite); // expecting 1 element
            using (var sut = new ManagementObject(new ManagementPath(vhdxString[0])))
            {
                vm.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Running);

                MetricService.Instance.ConfigureMetricsFlushInterval(new TimeSpan(1000));

                foreach (ManagementObject baseMetricDef in MetricService.GetAllBaseMetricDefinitions(sut))
                    MetricService.Instance.SetBaseMetric(sut, baseMetricDef, MetricService.MetricCollectionEnabled.Enable);

                mapped = MetricService.GetBaseMetricValueCollection(sut);

                foreach (var pair in mapped)
                {
                    foreach (var p in pair.Key.Properties)
                        Trace.Write("Definition [" + p.Name?.ToString() + "]:\t\t\t\t" + p.Value?.ToString() + "\n");

                    foreach (var p in pair.Value.Properties)
                        Trace.Write("Value [" + p.Name + "]:\t\t\t\t" + p.Value + "\n");

                    Trace.Write("--------------------------------------------------------------------------------\n");
                }

                vm.RequestStateChange(VirtualSystemManagementService.RequestedStateVSM.Off);
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
