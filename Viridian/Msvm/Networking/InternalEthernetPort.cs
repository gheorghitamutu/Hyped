using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;

namespace Viridian.Msvm.Networking
{
    public class InternalEthernetPort : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(InternalEthernetPort)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public InternalEthernetPort() : base(ClassName) { }

        public InternalEthernetPort(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public InternalEthernetPort(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public InternalEthernetPort(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public InternalEthernetPort(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public InternalEthernetPort(ManagementPath path) : base(path, ClassName) { }

        public InternalEthernetPort(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public InternalEthernetPort(ManagementObject theObject) : base(theObject, ClassName) { }

        public InternalEthernetPort(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public ulong ActiveMaximumTransmissionUnit
        {
            get
            {
                if (LateBoundObject[nameof(ActiveMaximumTransmissionUnit)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(ActiveMaximumTransmissionUnit)];
            }
        }

        public ushort[] AdditionalAvailability => (ushort[])LateBoundObject[nameof(AdditionalAvailability)];

        public bool AutoSense
        {
            get
            {
                if (LateBoundObject[nameof(AutoSense)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(AutoSense)];
            }
        }

        public ushort Availability
        {
            get
            {
                if (LateBoundObject[nameof(Availability)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(Availability)];
            }
        }

        public ushort[] AvailableRequestedStates => (ushort[])LateBoundObject[nameof(AvailableRequestedStates)];

        public ushort[] Capabilities => (ushort[])LateBoundObject[nameof(Capabilities)];

        public string[] CapabilityDescriptions => (string[])LateBoundObject[nameof(CapabilityDescriptions)];

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

        public string DeviceID => (string)LateBoundObject[nameof(DeviceID)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public ushort[] EnabledCapabilities => (ushort[])LateBoundObject[nameof(EnabledCapabilities)];

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

        public bool ErrorCleared
        {
            get
            {
                if (LateBoundObject[nameof(ErrorCleared)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(ErrorCleared)];
            }
        }

        public string ErrorDescription => (string)LateBoundObject[nameof(ErrorDescription)];

        public bool FullDuplex
        {
            get
            {
                if (LateBoundObject[nameof(FullDuplex)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(FullDuplex)];
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

        public string[] IdentifyingDescriptions => (string[])LateBoundObject[nameof(IdentifyingDescriptions)];

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

        public uint LastErrorCode
        {
            get
            {
                if (LateBoundObject[nameof(LastErrorCode)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(LastErrorCode)];
            }
        }

        public ushort LinkTechnology
        {
            get
            {
                if (LateBoundObject[nameof(LinkTechnology)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(LinkTechnology)];
            }
        }

        public uint MaxDataSize
        {
            get
            {
                if (LateBoundObject[nameof(MaxDataSize)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(MaxDataSize)];
            }
        }

        public ulong MaxQuiesceTime
        {
            get
            {
                if (LateBoundObject[nameof(MaxQuiesceTime)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(MaxQuiesceTime)];
            }
        }

        public ulong MaxSpeed
        {
            get
            {
                if (LateBoundObject[nameof(MaxSpeed)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(MaxSpeed)];
            }
        }

        public string Name => (string)LateBoundObject[nameof(Name)];

        public string[] NetworkAddresses => (string[])LateBoundObject[nameof(NetworkAddresses)];

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

        public string[] OtherEnabledCapabilities => (string[])LateBoundObject[nameof(OtherEnabledCapabilities)];

        public string OtherEnabledState => (string)LateBoundObject[nameof(OtherEnabledState)];

        public string[] OtherIdentifyingInfo => (string[])LateBoundObject[nameof(OtherIdentifyingInfo)];

        public string OtherLinkTechnology => (string)LateBoundObject[nameof(OtherLinkTechnology)];

        public string OtherNetworkPortType => (string)LateBoundObject[nameof(OtherNetworkPortType)];

        public string OtherPortType => (string)LateBoundObject[nameof(OtherPortType)];

        public string PermanentAddress => (string)LateBoundObject[nameof(PermanentAddress)];

        public ushort PortNumber
        {
            get
            {
                if (LateBoundObject[nameof(PortNumber)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(PortNumber)];
            }
        }

        public ushort PortType
        {
            get
            {
                if (LateBoundObject[nameof(PortType)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(PortType)];
            }
        }

        public ushort[] PowerManagementCapabilities => (ushort[])LateBoundObject[nameof(PowerManagementCapabilities)];

        public bool PowerManagementSupported
        {
            get
            {
                if (LateBoundObject[nameof(PowerManagementSupported)] == null)
                {
                    return Convert.ToBoolean(0);
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
                    return Convert.ToUInt64(0);
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
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(PrimaryStatus)];
            }
        }

        public ulong RequestedSpeed
        {
            get
            {
                if (LateBoundObject[nameof(RequestedSpeed)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(RequestedSpeed)];
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

        public ulong Speed
        {
            get
            {
                if (LateBoundObject[nameof(Speed)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(Speed)];
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
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(StatusInfo)];
            }
        }

        public ulong SupportedMaximumTransmissionUnit
        {
            get
            {
                if (LateBoundObject[nameof(SupportedMaximumTransmissionUnit)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(SupportedMaximumTransmissionUnit)];
            }
        }

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

        public ulong TotalPowerOnHours
        {
            get
            {
                if (LateBoundObject[nameof(TotalPowerOnHours)] == null)
                {
                    return Convert.ToUInt64(0);
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
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(TransitioningToState)];
            }
        }

        public ushort UsageRestriction
        {
            get
            {
                if (LateBoundObject[nameof(UsageRestriction)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(UsageRestriction)];
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<InternalEthernetPort> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new InternalEthernetPort(mo)).ToList();

        public new static List<InternalEthernetPort> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new InternalEthernetPort(mo)).ToList();

        public static List<InternalEthernetPort> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new InternalEthernetPort(mo)).ToList();

        public static List<InternalEthernetPort> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new InternalEthernetPort(mo)).ToList();

        public static List<InternalEthernetPort> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new InternalEthernetPort(mo)).ToList();

        public static List<InternalEthernetPort> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new InternalEthernetPort(mo)).ToList();

        public static List<InternalEthernetPort> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new InternalEthernetPort(mo)).ToList();

        public static List<InternalEthernetPort> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new InternalEthernetPort(mo)).ToList();

        public static InternalEthernetPort CreateInstance() => new InternalEthernetPort(CreateInstance(ClassName));

        public uint EnableDevice(bool Enabled)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("EnableDevice");
                inParams["Enabled"] = Enabled;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("EnableDevice", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint OnlineDevice(bool Online)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("OnlineDevice");
                inParams["Online"] = Online;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("OnlineDevice", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint QuiesceDevice(bool Quiesce)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("QuiesceDevice");
                inParams["Quiesce"] = Quiesce;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("QuiesceDevice", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
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

        public uint Reset()
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Reset", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint RestoreProperties()
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RestoreProperties", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint SaveProperties()
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SaveProperties", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint SetPowerState(ushort PowerState, DateTime Time)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("SetPowerState");
                inParams["PowerState"] = PowerState;
                inParams["Time"] = ToDmtfDateTime(Time);
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetPowerState", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }
    }
}
