using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viridian.Root.Virtualization.v2.Msvm;
using Viridian.Root.Virtualization.v2.Msvm.Metrics;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;

namespace ViridianTester.Msvm.Metrics
{
    [TestClass]
    public class MetricServiceTest
    {
        [TestMethod]
        public void GettingBaseAndAggregationMetricsOnProcessorSettingData_ExpectingZeroBaseAndOneAggregation()
        {
            using (var viridianUtils = new ViridianUtils())
            {
                viridianUtils.SUT_ComputerSystemMO(
                    ViridianUtils.GetCurrentMethod(),
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);

                using (var computerSystem = new ComputerSystem(ResultingSystem))
                {
                    ReturnValue = computerSystem.RequestStateChange(2, null, out Job);

                    ViridianUtils.WaitForConcreteJobToEnd(Job);

                    computerSystem.UpdateObject();

                    var metricServiceSettingData = MetricServiceSettingData.GetInstances().First();
                    metricServiceSettingData.LateBoundObject["MetricsFlushInterval"] = MsvmBase.ToDmtfTimeInterval(new TimeSpan(1000));
                    viridianUtils.MS.ModifyServiceSettings(metricServiceSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20), out Job);

                    var baseMetricDefinitions = BaseMetricDefinition.GetInstances();
                    baseMetricDefinitions.ForEach((bsd) => viridianUtils.MS.ControlMetrics(bsd.Path, 2, computerSystem.Path));

                    var aggregationMetricDefinitions = AggregationMetricDefinition.GetInstances();
                    aggregationMetricDefinitions.ForEach((agd) => viridianUtils.MS.ControlMetrics(agd.Path, 2, computerSystem.Path));

                    var vssdCollection = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem);

                    var psdCollection = ViridianUtils.GetProcessorSettingDataList(vssdCollection.First());

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

                    ViridianUtils.WaitForConcreteJobToEnd(Job);

                    Assert.IsNotNull(ResultingSystem);
                    Assert.AreEqual(4096U, ReturnValue);
                    Assert.AreEqual(1, vssdCollection.Count);
                    Assert.AreEqual(1, aggregationMetricMap.Count);
                    Assert.AreEqual(0, baseMetricMap.Count);
                }
            }
        }
    }
}
