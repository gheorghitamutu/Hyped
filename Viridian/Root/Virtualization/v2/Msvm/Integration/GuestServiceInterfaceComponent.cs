using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Integration
{
    public class GuestServiceInterfaceComponent : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(GuestServiceInterfaceComponent)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public GuestServiceInterfaceComponent() : base(ClassName) { }

        public GuestServiceInterfaceComponent(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public GuestServiceInterfaceComponent(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public GuestServiceInterfaceComponent(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public GuestServiceInterfaceComponent(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public GuestServiceInterfaceComponent(ManagementPath path) : base(path, ClassName) { }

        public GuestServiceInterfaceComponent(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public GuestServiceInterfaceComponent(ManagementObject theObject) : base(theObject, ClassName) { }

        public GuestServiceInterfaceComponent(ManagementBaseObject theObject) : base(theObject, ClassName) { }
        
        public ushort[] AdditionalAvailability => (ushort[])LateBoundObject[nameof(AdditionalAvailability)];

        public ushort Availability
        {
            get
            {
                if (LateBoundObject[nameof(Availability)] == null)
                {
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(Availability)];
            }
        }

        public ushort[] AvailableRequestedStates => (ushort[])LateBoundObject[nameof(AvailableRequestedStates)];

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public ushort CommunicationStatus
        {
            get
            {
                if (LateBoundObject[nameof(CommunicationStatus)] == null)
                {
                    return System.Convert.ToUInt16(0);
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
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(DetailedStatus)];
            }
        }

        public string DeviceID => (string)LateBoundObject[nameof(DeviceID)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public ushort EnabledDefault
        {
            get
            {
                if (LateBoundObject[nameof(EnabledDefault)] == null)
                {
                    return System.Convert.ToUInt16(0);
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
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(EnabledState)];
            }
        }

        public bool ErrorCleared
        {
            get
            {
                if (LateBoundObject[nameof(ErrorCleared)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(ErrorCleared)];
            }
        }

        public string ErrorDescription => (string)LateBoundObject[nameof(ErrorDescription)];

        public ushort HealthState
        {
            get
            {
                if (LateBoundObject[nameof(HealthState)] == null)
                {
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(HealthState)];
            }
        }

        public string[] IdentifyingDescriptions => (string[])LateBoundObject[nameof(IdentifyingDescriptions)];

        public System.DateTime InstallDate
        {
            get
            {
                if (LateBoundObject[nameof(InstallDate)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(InstallDate)]);
                }
                else
                {
                    return System.DateTime.MinValue;
                }
            }
        }

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        public uint LastErrorCode
        {
            get
            {
                if (LateBoundObject[nameof(LastErrorCode)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(LastErrorCode)];
            }
        }

        public ulong MaxQuiesceTime
        {
            get
            {
                if (LateBoundObject[nameof(MaxQuiesceTime)] == null)
                {
                    return System.Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(MaxQuiesceTime)];
            }
        }

        public string Name => (string)LateBoundObject[nameof(Name)];

        public ushort OperatingStatus
        {
            get
            {
                if (LateBoundObject[nameof(OperatingStatus)] == null)
                {
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(OperatingStatus)];
            }
        }

        public ushort[] OperationalStatus => (ushort[])LateBoundObject[nameof(OperationalStatus)];

        public string OtherEnabledState => (string)LateBoundObject[nameof(OtherEnabledState)];

        public string[] OtherIdentifyingInfo => (string[])LateBoundObject[nameof(OtherIdentifyingInfo)];

        public ushort[] PowerManagementCapabilities => (ushort[])LateBoundObject[nameof(PowerManagementCapabilities)];

        public bool PowerManagementSupported
        {
            get
            {
                if (LateBoundObject[nameof(PowerManagementSupported)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(PowerManagementSupported)];
            }
        }
        
        public ulong PowerOnHours
        {
            get
            {
                if (LateBoundObject[nameof(PowerOnHours)] == null)
                {
                    return System.Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(PowerOnHours)];
            }
        }

        public ushort PrimaryStatus
        {
            get
            {
                if (LateBoundObject[nameof(PrimaryStatus)] == null)
                {
                    return System.Convert.ToUInt16(0);
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
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(RequestedState)];
            }
        }

        public string Status => (string)LateBoundObject[nameof(Status)];

        public string[] StatusDescriptions => (string[])LateBoundObject[nameof(StatusDescriptions)];

        public ushort StatusInfo
        {
            get
            {
                if (LateBoundObject[nameof(StatusInfo)] == null)
                {
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(StatusInfo)];
            }
        }

        public string SystemCreationClassName => (string)LateBoundObject[nameof(SystemCreationClassName)];

        public string SystemName => (string)LateBoundObject[nameof(SystemName)];

        public System.DateTime TimeOfLastStateChange
        {
            get
            {
                if (LateBoundObject[nameof(TimeOfLastStateChange)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(TimeOfLastStateChange)]);
                }
                else
                {
                    return System.DateTime.MinValue;
                }
            }
        }

        public ulong TotalPowerOnHours
        {
            get
            {
                if (LateBoundObject[nameof(TotalPowerOnHours)] == null)
                {
                    return System.Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(TotalPowerOnHours)];
            }
        }

        public ushort TransitioningToState
        {
            get
            {
                if (LateBoundObject[nameof(TransitioningToState)] == null)
                {
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(TransitioningToState)];
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<GuestServiceInterfaceComponent> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponent(mo)).ToList();

        public new static List<GuestServiceInterfaceComponent> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponent(mo)).ToList();

        public static List<GuestServiceInterfaceComponent> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponent(mo)).ToList();

        public static List<GuestServiceInterfaceComponent> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponent(mo)).ToList();

        public static List<GuestServiceInterfaceComponent> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponent(mo)).ToList();

        public static List<GuestServiceInterfaceComponent> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponent(mo)).ToList();

        public static List<GuestServiceInterfaceComponent> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponent(mo)).ToList();

        public static List<GuestServiceInterfaceComponent> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestServiceInterfaceComponent(mo)).ToList();

        public static GuestServiceInterfaceComponent CreateInstance() => new GuestServiceInterfaceComponent(CreateInstance(ClassName));

        public uint EnableDevice(bool Enabled)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("EnableDevice");
                inParams["Enabled"] = Enabled;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("EnableDevice", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return System.Convert.ToUInt32(0);
            }
        }

        public uint OnlineDevice(bool Online)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("OnlineDevice");
                inParams["Online"] = Online;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("OnlineDevice", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return System.Convert.ToUInt32(0);
            }
        }

        public uint QuiesceDevice(bool Quiesce)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("QuiesceDevice");
                inParams["Quiesce"] = Quiesce;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("QuiesceDevice", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return System.Convert.ToUInt32(0);
            }
        }

        public uint RequestStateChange(ushort RequestedState, System.DateTime TimeoutPeriod, out ManagementPath Job)
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
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return System.Convert.ToUInt32(0);
            }
        }

        public uint Reset()
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Reset", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return System.Convert.ToUInt32(0);
            }
        }

        public uint RestoreProperties()
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RestoreProperties", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return System.Convert.ToUInt32(0);
            }
        }

        public uint SaveProperties()
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SaveProperties", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return System.Convert.ToUInt32(0);
            }
        }

        public uint SetPowerState(ushort PowerState, System.DateTime Time)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("SetPowerState");
                inParams["PowerState"] = PowerState;
                inParams["Time"] = ToDmtfDateTime(Time);
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetPowerState", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return System.Convert.ToUInt32(0);
            }
        }
    }
}
