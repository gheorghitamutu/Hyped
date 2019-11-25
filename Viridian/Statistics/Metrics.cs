using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Utilities;

namespace Viridian.Statistics
{
    public static class Metrics
    {
        public static ManagementObject GetMetricService(ManagementScope scope)
        {
            using (var msClass = new ManagementClass("Msvm_MetricService"))
            {
                msClass.Scope = scope;

                return Utils.GetFirstObjectFromCollection(msClass.GetInstances());
            }
        }

        public static void SetAllMetrics(ManagementObject msvmObject, Utils.MetricOperation operation)
        {
            if (msvmObject is null)
                throw new ViridianException("", new ArgumentNullException(nameof(msvmObject)));

            ControlMetrics(msvmObject.Scope, msvmObject.Path.Path, null, operation);
        }

        public static void SetBaseMetric(ManagementObject msvmObject, ManagementObject baseMetricDef, Utils.MetricOperation operation)
        {
            if (msvmObject is null)
                throw new ViridianException("", new ArgumentNullException(nameof(msvmObject)));

            if (baseMetricDef is null)
                throw new ViridianException("", new ArgumentNullException(nameof(baseMetricDef)));

            ControlMetrics(msvmObject.Scope, msvmObject.Path.Path, baseMetricDef.Path.Path, operation);
        }

        public static void SetAllMetrics(ManagementObjectCollection msvmObjectCollection, Utils.MetricOperation operation)
        {
            if (msvmObjectCollection is null)
                throw new ViridianException("", new ArgumentNullException(nameof(msvmObjectCollection)));

            foreach(ManagementObject msvmObject in msvmObjectCollection)
                ControlMetrics(msvmObject.Scope, msvmObject.Path.Path, null, operation);
        }

        public static void ControlMetrics(ManagementScope scope, string managedElementPath, string metricDefinitionPath, Utils.MetricOperation operation)
        {
            using (var ms = GetMetricService(scope))
            using (var ip = ms.GetMethodParameters("ControlMetrics"))
            {
                ip["Subject"] = managedElementPath;
                ip["Definition"] = metricDefinitionPath;
                ip["MetricCollectionEnabled"] = (uint)operation;

                using (var op = ms.InvokeMethod("ControlMetrics", ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public static void ConfigureMetricsFlushInterval(ManagementScope scope, TimeSpan interval)
        {
            using (var mssdClass = new ManagementClass("Msvm_MetricServiceSettingData"))
            {
                mssdClass.Scope = scope;

                using (var mssd = mssdClass.CreateInstance())
                {
                    mssd["MetricsFlushInterval"] = ManagementDateTimeConverter.ToDmtfTimeInterval(interval);

                    using (var ms = GetMetricService(scope))
                    using (var ip = ms.GetMethodParameters("ModifyServiceSettings"))
                    {
                        ip["SettingData"] = mssd.GetText(TextFormat.WmiDtd20);

                        using (var op = ms.InvokeMethod("ModifyServiceSettings", ip, null))
                            Validator.ValidateOutput(op, scope);
                    }
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
    }
}
