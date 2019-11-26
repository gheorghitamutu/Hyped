﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Utilities;

namespace Viridian.Statistics
{       
    public sealed class Metric
    {
        private static Metric instance = null;
        private const string serverName = ".";
        private const string scopePath = @"\Root\Virtualization\V2";
        private static ManagementObject Msvm_MetricService = null;
        private static ManagementScope scope = null;

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

        private Metric()
        {
            scope = new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);

            using (var vsms = new ManagementClass(nameof(Msvm_MetricService)))
            {
                vsms.Scope = scope;

                Msvm_MetricService = Utils.GetFirstObjectFromCollection(vsms.GetInstances());
            }
        }

        public static Metric Instance
        {
            get
            {
                if (instance == null)
                    instance = new Metric();

                return instance;
            }
        }

        public ManagementObject MsvmMetricService => Msvm_MetricService ?? throw new ViridianException($"{nameof(Msvm_MetricService)} is null!");

        #region MsvmProperties

        string InstanceID => Msvm_MetricService[nameof(InstanceID)].ToString();
        string Caption => Msvm_MetricService[nameof(Caption)].ToString();
        string Description => Msvm_MetricService[nameof(Description)].ToString();
        string ElementName => Msvm_MetricService[nameof(ElementName)].ToString();
        DateTime InstallDate => ManagementDateTimeConverter.ToDateTime(Msvm_MetricService[nameof(InstallDate)].ToString());
        string Name => Msvm_MetricService[nameof(Name)].ToString();
        ushort[] OperationalStatus => (ushort[])Msvm_MetricService[nameof(OperationalStatus)];
        string[] StatusDescriptions => (string[])Msvm_MetricService[nameof(StatusDescriptions)];
        string Status => Msvm_MetricService[nameof(Status)].ToString();
        ushort HealthState => (ushort)Msvm_MetricService[nameof(HealthState)];
        ushort CommunicationStatus => (ushort)Msvm_MetricService[nameof(CommunicationStatus)];
        ushort DetailedStatus => (ushort)Msvm_MetricService[nameof(DetailedStatus)];
        ushort OperatingStatus => (ushort)Msvm_MetricService[nameof(OperatingStatus)];
        ushort PrimaryStatus => (ushort)Msvm_MetricService[nameof(PrimaryStatus)];
        ushort EnabledState => (ushort)Msvm_MetricService[nameof(EnabledState)];
        string OtherEnabledState => Msvm_MetricService[nameof(OtherEnabledState)].ToString();
        ushort RequestedState => (ushort)Msvm_MetricService[nameof(RequestedState)];
        ushort EnabledDefault => (ushort)Msvm_MetricService[nameof(EnabledDefault)];
        DateTime TimeOfLastStateChange => ManagementDateTimeConverter.ToDateTime(Msvm_MetricService[nameof(TimeOfLastStateChange)].ToString());
        ushort[] AvailableRequestedStates => (ushort[])Msvm_MetricService[nameof(AvailableRequestedStates)];
        ushort TransitioningToState => (ushort)Msvm_MetricService[nameof(TransitioningToState)];
        string SystemCreationClassName => Msvm_MetricService[nameof(SystemCreationClassName)].ToString();
        string SystemName => Msvm_MetricService[nameof(SystemName)].ToString();
        string CreationClassName => Msvm_MetricService[nameof(CreationClassName)].ToString();
        string PrimaryOwnerName => Msvm_MetricService[nameof(PrimaryOwnerName)].ToString();
        string PrimaryOwnerContact => Msvm_MetricService[nameof(PrimaryOwnerContact)].ToString();
        string StartMode => Msvm_MetricService[nameof(StartMode)].ToString();
        bool Started => (bool)Msvm_MetricService[nameof(Started)];

        #endregion

        public void ControlMetrics(string Subject, string Definition, MetricCollectionEnabled MetricCollectionEnabled)
        {
            using (var ip = Msvm_MetricService.GetMethodParameters(nameof(ControlMetrics)))
            {
                ip[nameof(Subject)] = Subject ?? throw new ViridianException($"{nameof(Subject)} is null!");
                ip[nameof(Definition)] = Definition;
                ip[nameof(MetricCollectionEnabled)] = MetricCollectionEnabled;

                using (var op = Msvm_MetricService.InvokeMethod(nameof(ControlMetrics), ip, null))
                    Validator.ValidateOutput(op, scope);
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
                    Validator.ValidateOutput(op, scope);
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
                    Validator.ValidateOutput(op, scope);
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
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void ModifyServiceSettings(string SettingData)
        {
            using (var ip = Msvm_MetricService.GetMethodParameters(nameof(ModifyServiceSettings)))
            {
                ip[nameof(SettingData)] = SettingData ?? throw new ViridianException($"{nameof(SettingData)} is null!");

                using (var op = Msvm_MetricService.InvokeMethod(nameof(ModifyServiceSettings), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void RequestStateChange(MetricRequestedState RequestedState, ulong TimeoutPeriod = 0)
        {
            using (var ip = Msvm_MetricService.GetMethodParameters(nameof(RequestStateChange)))
            {
                ip[nameof(RequestedState)] = (uint)RequestedState;
                ip[nameof(TimeoutPeriod)] = null; // CIM_DateTime

                using (var op = Msvm_MetricService.InvokeMethod(nameof(RequestStateChange), ip, null))
                    Validator.ValidateOutput(op, scope);
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
                    Validator.ValidateOutput(op, scope);

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
                    Validator.ValidateOutput(op, scope);

                    return new object[] { op["DefinitionList"] as string[], op["MetricNames"] as string[], op["MetricCollectionEnabled"] as MetricCollectionEnabled[] };
                }
            }
        }

        public void StartService()
        {
            using (var ip = Msvm_MetricService.GetMethodParameters(nameof(StartService)))
            using (var op = Msvm_MetricService.InvokeMethod(nameof(StartService), ip, null))
                Validator.ValidateOutput(op, scope);
        }

        public void StopService()
        {
            using (var ip = Msvm_MetricService.GetMethodParameters(nameof(StopService)))
            using (var op = Msvm_MetricService.InvokeMethod(nameof(StopService), ip, null))
                Validator.ValidateOutput(op, scope);
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
                mssdClass.Scope = scope;

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
