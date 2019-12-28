using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Metrics
{
    public class MetricService : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(MetricService)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public MetricService() : base(ClassName) { }

        public MetricService(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MetricService(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MetricService(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public MetricService(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public MetricService(ManagementPath path) : base(path, ClassName) { }

        public MetricService(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public MetricService(ManagementObject theObject) : base(theObject, ClassName) { }

        public MetricService(ManagementBaseObject theObject) : base(theObject, ClassName) { }


        public ushort[] AvailableRequestedStates => (ushort[])LateBoundObject[nameof(AvailableRequestedStates)];

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public ushort CommunicationStatus
        {
            get
            {
                if (LateBoundObject[nameof(CommunicationStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(CommunicationStatus)];
            }
        }

        public string CreationClassName => (string)LateBoundObject[nameof(CreationClassName)];

        public string Description => (string)LateBoundObject[nameof(Description)];

        public ushort DetailedStatus
        {
            get
            {
                if (LateBoundObject[nameof(DetailedStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(DetailedStatus)];
            }
        }

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public ushort EnabledDefault
        {
            get
            {
                if (LateBoundObject[nameof(EnabledDefault)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(EnabledDefault)];
            }
        }

        public ushort EnabledState
        {
            get
            {
                if (LateBoundObject[nameof(EnabledState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(EnabledState)];
            }
        }

        public ushort HealthState
        {
            get
            {
                if (LateBoundObject[nameof(HealthState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(HealthState)];
            }
        }

        public DateTime InstallDate
        {
            get
            {
                if (LateBoundObject[nameof(InstallDate)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(InstallDate)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        public string Name => (string)LateBoundObject[nameof(Name)];

        public ushort OperatingStatus
        {
            get
            {
                if (LateBoundObject[nameof(OperatingStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(OperatingStatus)];
            }
        }

        public ushort[] OperationalStatus => (ushort[])LateBoundObject[nameof(OperationalStatus)];

        public string OtherEnabledState => (string)LateBoundObject[nameof(OtherEnabledState)];

        public string PrimaryOwnerContact => (string)LateBoundObject[nameof(PrimaryOwnerContact)];

        public string PrimaryOwnerName => (string)LateBoundObject[nameof(PrimaryOwnerName)];

        public ushort PrimaryStatus
        {
            get
            {
                if (LateBoundObject[nameof(PrimaryStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(PrimaryStatus)];
            }
        }

        public ushort RequestedState
        {
            get
            {
                if (LateBoundObject[nameof(RequestedState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(RequestedState)];
            }
        }

        public bool Started
        {
            get
            {
                if (LateBoundObject[nameof(Started)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(Started)];
            }
        }

        public string StartMode => (string)LateBoundObject[nameof(StartMode)];

        public string Status => (string)LateBoundObject[nameof(Status)];

        public string[] StatusDescriptions => (string[])LateBoundObject[nameof(StatusDescriptions)];

        public string SystemCreationClassName => (string)LateBoundObject[nameof(SystemCreationClassName)];

        public string SystemName => (string)LateBoundObject[nameof(SystemName)];

        public DateTime TimeOfLastStateChange
        {
            get
            {
                if (LateBoundObject[nameof(TimeOfLastStateChange)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(TimeOfLastStateChange)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public ushort TransitioningToState
        {
            get
            {
                if (LateBoundObject[nameof(TransitioningToState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(TransitioningToState)];
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<MetricService> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricService(mo)).ToList();

        public new static List<MetricService> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricService(mo)).ToList();

        public static List<MetricService> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricService(mo)).ToList();

        public static List<MetricService> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricService(mo)).ToList();

        public static List<MetricService> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new MetricService(mo)).ToList();

        public static List<MetricService> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MetricService(mo)).ToList();

        public static List<MetricService> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricService(mo)).ToList();

        public static List<MetricService> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MetricService(mo)).ToList();

        public static MetricService CreateInstance() => new MetricService(CreateInstance(ClassName));


        public uint ControlMetrics(ManagementPath Definition, ushort MetricCollectionEnabled, ManagementPath Subject)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ControlMetrics");
                inParams["Definition"] = Definition?.Path;
                inParams["MetricCollectionEnabled"] = MetricCollectionEnabled;
                inParams["Subject"] = Subject?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ControlMetrics", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint ControlMetricsByClass(ManagementPath Definition, ushort MetricCollectionEnabled, ManagementPath Subject)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ControlMetricsByClass");
                inParams["Definition"] = Definition?.Path;
                inParams["MetricCollectionEnabled"] = MetricCollectionEnabled;
                inParams["Subject"] = Subject?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ControlMetricsByClass", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint ControlSampleTimes(DateTime PreferredSampleInterval, bool RestartGathering, DateTime StartSampleTime)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ControlSampleTimes");
                inParams["PreferredSampleInterval"] = ToDmtfDateTime(PreferredSampleInterval);
                inParams["RestartGathering"] = RestartGathering;
                inParams["StartSampleTime"] = ToDmtfDateTime(StartSampleTime);
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ControlSampleTimes", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint GetMetricValues(ushort Count, ManagementPath Definition, ushort Range, out ManagementPath[] Values)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("GetMetricValues");
                inParams["Count"] = Count;
                inParams["Definition"] = Definition?.Path;
                inParams["Range"] = Range;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetMetricValues", inParams, null);
                Values = null;
                if (outParams.Properties["Values"] != null)
                {
                    int len = ((Array)outParams.Properties["Values"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["Values"].Value).GetValue(iCounter).ToString());
                    }
                    Values = arrToRet;
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Values = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ModifyServiceSettings(string SettingData, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ModifyServiceSettings");
                inParams["SettingData"] = SettingData;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ModifyServiceSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RequestStateChange(ushort RequestedState, DateTime TimeoutPeriod, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RequestStateChange");
                inParams[nameof(RequestedState)] = RequestedState;
                inParams["TimeoutPeriod"] = ToDmtfDateTime(TimeoutPeriod);
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RequestStateChange", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ShowMetrics(ManagementPath Definition, ManagementPath Subject, out ManagementPath[] DefinitionList, out ManagementPath[] ManagedElements, out ushort[] MetricCollectionEnabled, out string[] MetricNames)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ShowMetrics");
                inParams["Definition"] = Definition?.Path;
                inParams["Subject"] = Subject?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ShowMetrics", inParams, null);
                DefinitionList = null;
                if (outParams.Properties["DefinitionList"] != null)
                {
                    int len = ((Array)outParams.Properties["DefinitionList"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["DefinitionList"].Value).GetValue(iCounter).ToString());
                    }
                    DefinitionList = arrToRet;
                }
                ManagedElements = null;
                if (outParams.Properties["ManagedElements"] != null)
                {
                    int len = ((Array)outParams.Properties["ManagedElements"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["ManagedElements"].Value).GetValue(iCounter).ToString());
                    }
                    ManagedElements = arrToRet;
                }
                MetricCollectionEnabled = (ushort[])outParams.Properties["MetricCollectionEnabled"].Value;
                MetricNames = (string[])outParams.Properties["MetricNames"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                DefinitionList = null;
                ManagedElements = null;
                MetricCollectionEnabled = null;
                MetricNames = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ShowMetricsByClass(ManagementPath Definition, ManagementPath Subject, out ManagementPath[] DefinitionList, out ushort[] MetricCollectionEnabled, out string[] MetricNames)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ShowMetricsByClass");
                inParams["Definition"] = Definition?.Path;
                inParams["Subject"] = Subject?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ShowMetricsByClass", inParams, null);
                DefinitionList = null;
                if (outParams.Properties["DefinitionList"] != null)
                {
                    int len = ((Array)outParams.Properties["DefinitionList"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["DefinitionList"].Value).GetValue(iCounter).ToString());
                    }
                    DefinitionList = arrToRet;
                }
                MetricCollectionEnabled = (ushort[])outParams.Properties["MetricCollectionEnabled"].Value;
                MetricNames = (string[])outParams.Properties["MetricNames"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                DefinitionList = null;
                MetricCollectionEnabled = null;
                MetricNames = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint StartService()
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("StartService", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint StopService()
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("StopService", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }
    }
}
