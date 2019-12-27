using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Root.Virtualization.v2.Msvm;
using Viridian.Root.Virtualization.v2.Msvm.Metrics;
using Viridian.Root.Virtualization.v2.Msvm.Processor;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;

namespace ViridianTester.Msvm.Metrics
{
    [TestClass]
    public class MetricServiceTest
    {
        [TestMethod]
        public void GettingBaseAndAggregationMetricsOnProcessorSettingData_ExpectingZeroBaseAndOneAggregation()
        {
            using (var virtualSystemManagementService = VirtualSystemManagementService.GetInstances().Cast<VirtualSystemManagementService>().ToList().First())
            {
                var virtualSystemSettingData = VirtualSystemSettingData.CreateInstance();

                virtualSystemSettingData.LateBoundObject["ElementName"] = nameof(GettingBaseAndAggregationMetricsOnProcessorSettingData_ExpectingZeroBaseAndOneAggregation);
                virtualSystemSettingData.LateBoundObject["ConfigurationDataRoot"] = @"ConfigurationDataRoot";
                virtualSystemSettingData.LateBoundObject["VirtualSystemSubtype"] = "Microsoft:Hyper-V:SubType:2";

                ManagementPath ReferenceConfiguration = null;
                string[] ResourceSettings = null;
                string SystemSettings = virtualSystemSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20);

                var ReturnValue = virtualSystemManagementService.DefineSystem(ReferenceConfiguration, ResourceSettings, SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem);

                var computerSystem = new ComputerSystem(ResultingSystem);

                ReturnValue = computerSystem.RequestStateChange(2, null, out Job);

                using (ConcreteJob concreteJob = new ConcreteJob(Job))
                {
                    while (
                        concreteJob.JobState != 7 &&     // Completed
                        concreteJob.JobState != 8 &&     // Terminated
                        concreteJob.JobState != 9 &&     // Killed
                        concreteJob.JobState != 10 &&    // Exception
                        concreteJob.JobState != 32768)   // CompletedWithWarnings
                    {
                        ((ManagementObject)concreteJob.LateBoundObject).Get();
                    }
                }

                computerSystem = ComputerSystem.GetInstances($"Name='{computerSystem.Name}'").Cast<ComputerSystem>().ToList().First();

                using (var sut = MetricService.GetInstances().First())
                {
                    var metricServiceSettingData = MetricServiceSettingData.GetInstances().First();
                    metricServiceSettingData.LateBoundObject["MetricsFlushInterval"] = MsvmBase.ToDmtfTimeInterval(new TimeSpan(1000));
                    sut.ModifyServiceSettings(metricServiceSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20), out Job);

                    var baseMetricDefinitions = BaseMetricDefinition.GetInstances();                    
                    baseMetricDefinitions.ForEach((bsd) => sut.ControlMetrics(bsd.Path, 2, computerSystem.Path));

                    var aggregationMetricDefinitions = AggregationMetricDefinition.GetInstances();
                    aggregationMetricDefinitions.ForEach((agd) => sut.ControlMetrics(agd.Path, 2, computerSystem.Path));

                    var vssdCollection =
                        SettingsDefineState.GetInstances()
                            .Cast<SettingsDefineState>()
                            .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                            .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                            .ToList();

                    var psdCollection =
                        VirtualSystemSettingDataComponent.GetInstances()
                            .Cast<VirtualSystemSettingDataComponent>()
                            .Where((sds) =>
                                string.Compare(sds.GroupComponent.Path, vssdCollection.First().Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                                string.Compare(sds.PartComponent.ClassName, $"Msvm_{nameof(ProcessorSettingData)}", true, CultureInfo.InvariantCulture) == 0)
                            .Select((sds) => new ProcessorSettingData(sds.PartComponent))
                            .ToList();

                    var amdCollection =
                        MetricDefForME.GetInstances()
                            .Cast<MetricDefForME>()
                            .Where((mdfm) => 
                                string.Compare(mdfm.Antecedent.Path, psdCollection.First().Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                                mdfm.Dependent.ClassName == $"Msvm_{nameof(AggregationMetricDefinition)}")
                            .Select((mdfm) => new AggregationMetricDefinition(mdfm.Dependent))
                            .ToList();

                    var amvCollection =
                        MetricForME.GetInstances()
                            .Cast<MetricForME>()
                            .Where((mdfm) => string.Compare(mdfm.Antecedent.Path, psdCollection.First().Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                                mdfm.Dependent.ClassName == $"Msvm_{nameof(AggregationMetricValue)}")
                            .Select((mdfm) => new AggregationMetricValue(mdfm.Dependent))
                            .ToList();

                    var aggregationMetricMap = new Dictionary<AggregationMetricDefinition, AggregationMetricValue>();

                    amdCollection
                        .Where((amd) => amvCollection.Where((amv) => string.Compare(amv.MetricDefinitionId, amd.Id, true, CultureInfo.InvariantCulture) == 0).Any())
                        .ToList()
                        .ForEach((amd) => 
                        aggregationMetricMap.Add(
                            amd,
                            amvCollection.Where((amv) => string.Compare(amv.MetricDefinitionId, amd.Id, true, CultureInfo.InvariantCulture) == 0).ToList().First())
                        );

                    var bmdCollection =
                        MetricDefForME.GetInstances()
                            .Cast<MetricDefForME>()
                            .Where((mdfm) => string.Compare(mdfm.Antecedent.Path, psdCollection.First().Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                                mdfm.Dependent.ClassName == $"Msvm_{nameof(BaseMetricDefinition)}")
                            .Select((mdfm) => new BaseMetricDefinition(mdfm.Dependent))
                            .ToList();

                    var bmvCollection =
                        MetricForME.GetInstances()
                            .Cast<MetricForME>()
                            .Where((mdfm) => string.Compare(mdfm.Antecedent.Path, psdCollection.First().Path.Path, true, CultureInfo.InvariantCulture) == 0 &&
                                mdfm.Dependent.ClassName == $"Msvm_{nameof(BaseMetricValue)}")
                            .Select((mdfm) => new BaseMetricValue(mdfm.Dependent))
                            .ToList();

                    var baseMetricMap = new Dictionary<BaseMetricDefinition, BaseMetricValue>();

                    bmdCollection
                        .Where((bmd) => bmvCollection.Where((amv) => string.Compare(amv.MetricDefinitionId, bmd.Id, true, CultureInfo.InvariantCulture) == 0).Any())
                        .ToList()
                        .ForEach((bmd) =>
                        baseMetricMap.Add(
                            bmd,
                            bmvCollection.Where((amv) => string.Compare(amv.MetricDefinitionId, bmd.Id, true, CultureInfo.InvariantCulture) == 0).ToList().First())
                        );

                    ReturnValue = computerSystem.RequestStateChange(3, null, out Job);

                    using (ConcreteJob concreteJob = new ConcreteJob(Job))
                    {
                        while (
                            concreteJob.JobState != 7 &&     // Completed
                            concreteJob.JobState != 8 &&     // Terminated
                            concreteJob.JobState != 9 &&     // Killed
                            concreteJob.JobState != 10 &&    // Exception
                            concreteJob.JobState != 32768)   // CompletedWithWarnings
                        {
                            ((ManagementObject)concreteJob.LateBoundObject).Get();
                        }
                    }

                    Assert.IsNotNull(ResultingSystem);
                    Assert.AreEqual(4096U, ReturnValue);
                    Assert.AreEqual(1, vssdCollection.Count);
                    Assert.AreEqual(1, aggregationMetricMap.Count);
                    Assert.AreEqual(0, baseMetricMap.Count);
                }

                virtualSystemManagementService.DestroySystem(ResultingSystem, out Job);
            }
        }
    }
}
