using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;

namespace Viridian.Msvm.Networking
{
    public class VirtualEthernetSwitchManagementService : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(VirtualEthernetSwitchManagementService)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public VirtualEthernetSwitchManagementService() : base(ClassName) { }

        public VirtualEthernetSwitchManagementService(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualEthernetSwitchManagementService(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualEthernetSwitchManagementService(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public VirtualEthernetSwitchManagementService(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public VirtualEthernetSwitchManagementService(ManagementPath path) : base(path, ClassName) { }

        public VirtualEthernetSwitchManagementService(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public VirtualEthernetSwitchManagementService(ManagementObject theObject) : base(theObject, ClassName) { }

        public VirtualEthernetSwitchManagementService(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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
        public static List<VirtualEthernetSwitchManagementService> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchManagementService(mo)).ToList();

        public new static List<VirtualEthernetSwitchManagementService> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchManagementService(mo)).ToList();

        public static List<VirtualEthernetSwitchManagementService> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchManagementService(mo)).ToList();

        public static List<VirtualEthernetSwitchManagementService> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchManagementService(mo)).ToList();

        public static List<VirtualEthernetSwitchManagementService> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchManagementService(mo)).ToList();

        public static List<VirtualEthernetSwitchManagementService> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchManagementService(mo)).ToList();

        public static List<VirtualEthernetSwitchManagementService> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchManagementService(mo)).ToList();

        public static List<VirtualEthernetSwitchManagementService> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchManagementService(mo)).ToList();

        public static VirtualEthernetSwitchManagementService CreateInstance() => new VirtualEthernetSwitchManagementService(CreateInstance(ClassName));

        public uint AddFeatureSettings(ManagementPath AffectedConfiguration, string[] FeatureSettings, out ManagementPath Job, out ManagementPath[] ResultingFeatureSettings)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("AddFeatureSettings");
                inParams["AffectedConfiguration"] = AffectedConfiguration?.Path;
                inParams["FeatureSettings"] = FeatureSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("AddFeatureSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingFeatureSettings = null;
                if (outParams.Properties["ResultingFeatureSettings"] != null)
                {
                    int len = ((Array)outParams.Properties["ResultingFeatureSettings"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["ResultingFeatureSettings"].Value).GetValue(iCounter).ToString());
                    }
                    ResultingFeatureSettings = arrToRet;
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingFeatureSettings = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint AddResourceSettings(ManagementPath AffectedConfiguration, string[] ResourceSettings, out ManagementPath Job, out ManagementPath[] ResultingResourceSettings)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("AddResourceSettings");
                inParams["AffectedConfiguration"] = AffectedConfiguration?.Path;
                inParams["ResourceSettings"] = ResourceSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("AddResourceSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingResourceSettings = null;
                if (outParams.Properties["ResultingResourceSettings"] != null)
                {
                    int len = ((Array)outParams.Properties["ResultingResourceSettings"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["ResultingResourceSettings"].Value).GetValue(iCounter).ToString());
                    }
                    ResultingResourceSettings = arrToRet;
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingResourceSettings = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint DefineSystem(ManagementPath ReferenceConfiguration, string[] ResourceSettings, string SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("DefineSystem");
                inParams["ReferenceConfiguration"] = ReferenceConfiguration?.Path;
                inParams["ResourceSettings"] = ResourceSettings;
                inParams["SystemSettings"] = SystemSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DefineSystem", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingSystem = null;
                if (outParams.Properties["ResultingSystem"] != null)
                {
                    ResultingSystem = new ManagementPath(outParams["ResultingSystem"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingSystem = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint DestroySystem(ManagementPath AffectedSystem, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("DestroySystem");
                inParams["AffectedSystem"] = AffectedSystem?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DestroySystem", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ModifyFeatureSettings(string[] FeatureSettings, out ManagementPath Job, out ManagementPath[] ResultingFeatureSettings)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ModifyFeatureSettings");
                inParams["FeatureSettings"] = FeatureSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ModifyFeatureSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingFeatureSettings = null;
                if (outParams.Properties["ResultingFeatureSettings"] != null)
                {
                    int len = ((Array)outParams.Properties["ResultingFeatureSettings"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["ResultingFeatureSettings"].Value).GetValue(iCounter).ToString());
                    }
                    ResultingFeatureSettings = arrToRet;
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingFeatureSettings = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ModifyResourceSettings(string[] ResourceSettings, out ManagementPath Job, out ManagementPath[] ResultingResourceSettings)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ModifyResourceSettings");
                inParams["ResourceSettings"] = ResourceSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ModifyResourceSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingResourceSettings = null;
                if (outParams.Properties["ResultingResourceSettings"] != null)
                {
                    int len = ((Array)outParams.Properties["ResultingResourceSettings"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["ResultingResourceSettings"].Value).GetValue(iCounter).ToString());
                    }
                    ResultingResourceSettings = arrToRet;
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingResourceSettings = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ModifySystemSettings(string SystemSettings, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ModifySystemSettings");
                inParams["SystemSettings"] = SystemSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ModifySystemSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RemoveFeatureSettings(ManagementPath[] FeatureSettings, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RemoveFeatureSettings");
                if (FeatureSettings != null)
                {
                    int len = FeatureSettings.Length;
                    string[] arrProp = new string[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrProp[iCounter] = ((ManagementPath)FeatureSettings.GetValue(iCounter)).Path;
                    }
                    inParams["FeatureSettings"] = arrProp;
                }
                else
                {
                    inParams["FeatureSettings"] = null;
                }
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RemoveFeatureSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RemoveResourceSettings(ManagementPath[] ResourceSettings, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RemoveResourceSettings");
                if (ResourceSettings != null)
                {
                    int len = ResourceSettings.Length;
                    string[] arrProp = new string[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrProp[iCounter] = ((ManagementPath)ResourceSettings.GetValue(iCounter)).Path;
                    }
                    inParams["ResourceSettings"] = arrProp;
                }
                else
                {
                    inParams["ResourceSettings"] = null;
                }
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RemoveResourceSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
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
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
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
