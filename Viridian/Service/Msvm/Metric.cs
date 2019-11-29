﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Service.Msvm;
using Viridian.Utilities;

namespace Viridian.Statistics
{       
    public sealed class Metric : BaseService
    {
        private static Metric instance = null;

        public enum MetricCollectionEnabled : ushort
        {
            Enable = 2,
            Disable = 3,
            Reset = 4
        };

        public enum MetricRequestedState : ushort
        {
            Enabled = 2,
            Disabled = 3,
            ShutDown = 4,
            Offline = 6,
            Test = 7,
            Defer = 8,
            Quiesce = 9,
            Reboot = 10,
            Reset = 11
        };

        private Metric() : base("Msvm_MetricService") { }

        public static Metric Instance
        {
            get
            {
                if (instance == null)
                    instance = new Metric();

                return instance;
            }
        }

#pragma warning disable CA1303 // Do not pass literals as localized parameters
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
        public ManagementObject Msvm_MetricService => Service ?? throw new ViridianException($"{nameof(ServiceName)} is null!");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1303 // Do not pass literals as localized parameters

        public void ControlMetrics(string Subject, string Definition, MetricCollectionEnabled MetricCollectionEnabled)
        {
            using (var ip = Msvm_MetricService.GetMethodParameters(nameof(ControlMetrics)))
            {
                ip[nameof(Subject)] = Subject ?? throw new ViridianException($"{nameof(Subject)} is null!");
                ip[nameof(Definition)] = Definition;
                ip[nameof(MetricCollectionEnabled)] = MetricCollectionEnabled;

                using (var op = Msvm_MetricService.InvokeMethod(nameof(ControlMetrics), ip, null))
                    Validator.ValidateOutput(op, Scope);
            }
        }

        public void ControlMetricsByClass(string Subject, string Definition, MetricCollectionEnabled MetricCollectionEnabled)
        {
            using (var ip = Msvm_MetricService.GetMethodParameters(nameof(ControlMetricsByClass)))
            {
                ip[nameof(Subject)] = Subject ?? throw new ViridianException($"{nameof(Subject)} is null!");
                ip[nameof(Definition)] = Definition ?? throw new ViridianException($"{nameof(Definition)} is null!");
                ip[nameof(MetricCollectionEnabled)] = MetricCollectionEnabled;

                using (var op = Msvm_MetricService.InvokeMethod(nameof(ControlMetricsByClass), ip, null))
                    Validator.ValidateOutput(op, Scope);
            }
        }

        public void ControlSampleTimes(DateTime StartSampleTime, DateTime PreferredSampleInterval, bool RestartGathering)
        {
            using (var ip = Msvm_MetricService.GetMethodParameters(nameof(ControlSampleTimes)))
            {
                ip[nameof(StartSampleTime)] = ManagementDateTimeConverter.ToDmtfDateTime(StartSampleTime) ?? throw new ViridianException($"{nameof(StartSampleTime)} is null!");
                ip[nameof(PreferredSampleInterval)] = ManagementDateTimeConverter.ToDmtfDateTime(PreferredSampleInterval) ?? throw new ViridianException($"{nameof(PreferredSampleInterval)} is null!");
                ip[nameof(RestartGathering)] = RestartGathering;

                using (var op = Msvm_MetricService.InvokeMethod(nameof(ControlSampleTimes), ip, null))
                    Validator.ValidateOutput(op, Scope);
            }
        }

        public void GetMetricValues(string Definition, ushort Range, ushort Count)
        {
            using (var ip = Msvm_MetricService.GetMethodParameters(nameof(GetMetricValues)))
            {
                ip[nameof(Definition)] = Definition ?? throw new ViridianException($"{nameof(Definition)} is null!");
                ip[nameof(Range)] = Range;
                ip[nameof(Count)] = Count;

                using (var op = Msvm_MetricService.InvokeMethod(nameof(GetMetricValues), ip, null))
                    Validator.ValidateOutput(op, Scope);
            }
        }

        public void ModifyServiceSettings(string SettingData)
        {
            using (var ip = Msvm_MetricService.GetMethodParameters(nameof(ModifyServiceSettings)))
            {
                ip[nameof(SettingData)] = SettingData ?? throw new ViridianException($"{nameof(SettingData)} is null!");

                using (var op = Msvm_MetricService.InvokeMethod(nameof(ModifyServiceSettings), ip, null))
                    Validator.ValidateOutput(op, Scope);
            }
        }

        public void RequestStateChange(MetricRequestedState RequestedState, ulong TimeoutPeriod = 0)
        {
            using (var ip = Msvm_MetricService.GetMethodParameters(nameof(RequestStateChange)))
            {
                ip[nameof(RequestedState)] = (uint)RequestedState;
                ip[nameof(TimeoutPeriod)] = null; // CIM_DateTime

                using (var op = Msvm_MetricService.InvokeMethod(nameof(RequestStateChange), ip, null))
                    Validator.ValidateOutput(op, Scope);
            }
        }

        public object[] ShowMetrics(string Subject, string Definition)
        {
            using (var ip = Msvm_MetricService.GetMethodParameters(nameof(ShowMetrics)))
            {
                ip[nameof(Subject)] = Subject ?? throw new ViridianException($"{nameof(Subject)} is null!");
                ip[nameof(Definition)] = Definition ?? throw new ViridianException($"{nameof(Definition)} is null!");

                using (var op = Msvm_MetricService.InvokeMethod(nameof(ShowMetrics), ip, null))
                {
                    Validator.ValidateOutput(op, Scope);

                    return new object[] { op["ManagedElements"] as string[], op["MetricNames"] as string[], op["MetricCollectionEnabled"] as MetricCollectionEnabled[] };
                }
            }
        }

        public object[] ShowMetricsByClass(string Subject, string Definition)
        {
            using (var ip = Msvm_MetricService.GetMethodParameters(nameof(ShowMetricsByClass)))
            {
                ip[nameof(Subject)] = Subject ?? throw new ViridianException($"{nameof(Subject)} is null!");
                ip[nameof(Definition)] = Definition ?? throw new ViridianException($"{nameof(Definition)} is null!");

                using (var op = Msvm_MetricService.InvokeMethod(nameof(ShowMetricsByClass), ip, null))
                {
                    Validator.ValidateOutput(op, Scope);

                    return new object[] { op["DefinitionList"] as string[], op["MetricNames"] as string[], op["MetricCollectionEnabled"] as MetricCollectionEnabled[] };
                }
            }
        }

        public override void StartService()
        {
            using (var ip = Msvm_MetricService.GetMethodParameters(nameof(StartService)))
            using (var op = Msvm_MetricService.InvokeMethod(nameof(StartService), ip, null))
                Validator.ValidateOutput(op, Scope);
        }

        public override void StopService()
        {
            using (var ip = Msvm_MetricService.GetMethodParameters(nameof(StopService)))
            using (var op = Msvm_MetricService.InvokeMethod(nameof(StopService), ip, null))
                Validator.ValidateOutput(op, Scope);
        }


        #region Utils

        public void SetAllMetrics(ManagementObject msvmObject, MetricCollectionEnabled operation)
        {
            if (msvmObject is null)
                throw new ViridianException("", new ArgumentNullException(nameof(msvmObject)));

            ControlMetrics(msvmObject.Path.Path, null, operation);
        }

        public void SetBaseMetric(ManagementObject msvmObject, ManagementObject baseMetricDef, MetricCollectionEnabled operation)
        {
            if (msvmObject is null)
                throw new ViridianException("", new ArgumentNullException(nameof(msvmObject)));

            if (baseMetricDef is null)
                throw new ViridianException("", new ArgumentNullException(nameof(baseMetricDef)));

            ControlMetrics(msvmObject.Path.Path, baseMetricDef.Path.Path, operation);
        }

        public void SetAllMetrics(ManagementObjectCollection msvmObjectCollection, MetricCollectionEnabled operation)
        {
            if (msvmObjectCollection is null)
                throw new ViridianException("", new ArgumentNullException(nameof(msvmObjectCollection)));

            foreach(ManagementObject msvmObject in msvmObjectCollection)
                ControlMetrics(msvmObject.Path.Path, null, operation);
        }

        public void ConfigureMetricsFlushInterval(TimeSpan interval)
        {
            using (var mssdClass = new ManagementClass("Msvm_MetricServiceSettingData"))
            {
                mssdClass.Scope = Scope;

                using (var mssd = mssdClass.CreateInstance())
                {
                    mssd["MetricsFlushInterval"] = ManagementDateTimeConverter.ToDmtfTimeInterval(interval);

                    ModifyServiceSettings(mssd.GetText(TextFormat.WmiDtd20));
                }
            }
        }

        public static Dictionary<ManagementObject, ManagementObject> GetAggregationMetricValueCollection(ManagementObject msvmObject)
        {
            if (msvmObject is null)
                throw new ViridianException("", new ArgumentNullException(nameof(msvmObject)));

            using (var amdCollection = msvmObject.GetRelated("Msvm_AggregationMetricDefinition", "Msvm_MetricDefForME", null, null, null, null, false, null))
            using (var amvCollection = msvmObject.GetRelated("Msvm_AggregationMetricValue", "Msvm_MetricForME", null, null, null, null, false, null))
            {
                var metricMap = new Dictionary<ManagementObject, ManagementObject>();

                foreach (ManagementObject amd in amdCollection)
                    foreach (ManagementObject amv in amvCollection)
                        if (amv["MetricDefinitionId"].ToString() == amd["Id"].ToString())
                            metricMap.Add(amd, amv);

                return metricMap;
            }
        }

        public static Dictionary<ManagementObject, ManagementObject> GetBaseMetricValueCollection(ManagementObject msvmObject)
        {
            if (msvmObject is null)
                throw new ViridianException("", new ArgumentNullException(nameof(msvmObject)));

            using (var amdCollection = msvmObject.GetRelated("Msvm_BaseMetricDefinition", "Msvm_MetricDefForME", null, null, null, null, false, null))
            using (var amvCollection = msvmObject.GetRelated("Msvm_BaseMetricValue", "Msvm_MetricForME", null, null, null, null, false, null))
            {
                var metricMap = new Dictionary<ManagementObject, ManagementObject>();

                foreach (ManagementObject amd in amdCollection)
                    foreach (ManagementObject amv in amvCollection)
                        if (amv["MetricDefinitionId"].ToString() == amd["Id"].ToString())
                            metricMap.Add(amd, amv);

                return metricMap;
            }
        }

        public static ManagementObject GetBaseMetricDefForMEByName(ManagementObject msvmObject, string metricDefinitionName)
        {
            if (msvmObject is null)
                throw new ViridianException("", new ArgumentNullException(nameof(msvmObject)));

            using (var md = Utils.GetBaseMetricDefinition(metricDefinitionName, msvmObject.Scope))
            {
                if (md == null) // definition for this metric has not been found
                    return null;

                var escapedObjPath = Utils.EscapeObjectPath(msvmObject.Path.Path);
                var escapedMdPath = Utils.EscapeObjectPath(md.Path.Path);
                var wqlQuery = string.Format(CultureInfo.InvariantCulture, "SELECT * FROM Msvm_MetricDefForME WHERE Antecedent=\"{0}\" AND Dependent=\"{1}\"", escapedObjPath, escapedMdPath);
                var query = new SelectQuery(wqlQuery);

                using (var mos = new ManagementObjectSearcher(msvmObject.Scope, query))
                using (var definitionsCollection = mos.Get())
                {
                    if (definitionsCollection.Count > 0)
                        return Utils.GetFirstObjectFromCollection(definitionsCollection);
                    else
                        return null;
                }
            }
        }

        public static ManagementObjectCollection GetMetricsByDefinition(ManagementObject msvmObject, ManagementObject metricDefinition)
        {
            if (msvmObject is null)
                throw new ViridianException("", new ArgumentNullException(nameof(msvmObject)));
            
            if (metricDefinition is null)
                throw new ViridianException("", new ArgumentNullException(nameof(metricDefinition)));

            var escapedObjPath = Utils.EscapeObjectPath(msvmObject.Path.Path);
            var escapedMdPath = Utils.EscapeObjectPath(metricDefinition.Path.Path);

            var wqlQuery = string.Format(CultureInfo.InvariantCulture, "SELECT * FROM Msvm_MetricDefForME WHERE Antecedent=\"{0}\" AND Dependent=\"{1}\"", escapedObjPath, escapedMdPath);
            var query = new SelectQuery(wqlQuery);

            using (var mos = new ManagementObjectSearcher(msvmObject.Scope, query))
                return mos.Get();
        }

        public static ManagementObjectCollection GetAllBaseMetricDefinitions(ManagementObject msvmObject)
        {
            if (msvmObject is null)
                throw new ViridianException("", new ArgumentNullException(nameof(msvmObject)));

            return msvmObject.GetRelated("Msvm_BaseMetricDefinition", "Msvm_MetricDefForME", null, null, null, null, false, null);
        }

        public static ManagementObjectCollection GetAllAggregationMetricDefinitions(ManagementObject msvmObject)
        {
            if (msvmObject is null)
                throw new ViridianException("", new ArgumentNullException(nameof(msvmObject)));

            return msvmObject.GetRelated("Msvm_AggregationMetricDefinition", "Msvm_MetricDefForME", null, null, null, null, false, null);
        }

        #endregion
    }
}