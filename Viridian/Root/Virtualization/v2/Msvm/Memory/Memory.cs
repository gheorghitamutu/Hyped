using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Memory
{
    public class Memory : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(Memory)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public Memory() : base(ClassName) { }

        public Memory(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public Memory(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public Memory(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public Memory(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public Memory(ManagementPath path) : base(path, ClassName) { }

        public Memory(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public Memory(ManagementObject theObject) : base(theObject, ClassName) { }

        public Memory(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public ushort Access
        {
            get
            {
                if (LateBoundObject[nameof(Access)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(Access)];
            }
        }

        public ushort[] AdditionalAvailability => (ushort[])LateBoundObject[nameof(AdditionalAvailability)];

        public byte[] AdditionalErrorData => (byte[])LateBoundObject[nameof(AdditionalErrorData)];

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

        public ulong BlockSize
        {
            get
            {
                if (LateBoundObject[nameof(BlockSize)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(BlockSize)];
            }
        }

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

        public ulong ConsumableBlocks
        {
            get
            {
                if (LateBoundObject[nameof(ConsumableBlocks)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(ConsumableBlocks)];
            }
        }

        public bool CorrectableError
        {
            get
            {
                if (LateBoundObject[nameof(CorrectableError)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(CorrectableError)];
            }
        }

        public string CreationClassName => (string)LateBoundObject[nameof(CreationClassName)];

        public ushort DataOrganization
        {
            get
            {
                if (LateBoundObject[nameof(DataOrganization)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(DataOrganization)];
            }
        }

        public ushort DataRedundancy
        {
            get
            {
                if (LateBoundObject[nameof(DataRedundancy)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(DataRedundancy)];
            }
        }

        public byte DeltaReservation
        {
            get
            {
                if (LateBoundObject[nameof(DeltaReservation)] == null)
                {
                    return Convert.ToByte(0);
                }
                return (byte)LateBoundObject[nameof(DeltaReservation)];
            }
        }

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

        public ulong EndingAddress
        {
            get
            {
                if (LateBoundObject[nameof(EndingAddress)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(EndingAddress)];
            }
        }

        public ushort ErrorAccess
        {
            get
            {
                if (LateBoundObject[nameof(ErrorAccess)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ErrorAccess)];
            }
        }

        public ulong ErrorAddress
        {
            get
            {
                if (LateBoundObject[nameof(ErrorAddress)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(ErrorAddress)];
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

        public byte[] ErrorData => (byte[])LateBoundObject[nameof(ErrorData)];

        public ushort ErrorDataOrder
        {
            get
            {
                if (LateBoundObject[nameof(ErrorDataOrder)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ErrorDataOrder)];
            }
        }

        public string ErrorDescription => (string)LateBoundObject[nameof(ErrorDescription)];

        public ushort ErrorInfo
        {
            get
            {
                if (LateBoundObject[nameof(ErrorInfo)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ErrorInfo)];
            }
        }

        public string ErrorMethodology => (string)LateBoundObject[nameof(ErrorMethodology)];

        public ulong ErrorResolution
        {
            get
            {
                if (LateBoundObject[nameof(ErrorResolution)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(ErrorResolution)];
            }
        }

        public DateTime ErrorTime
        {
            get
            {
                if (LateBoundObject[nameof(ErrorTime)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(ErrorTime)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public uint ErrorTransferSize
        {
            get
            {
                if (LateBoundObject[nameof(ErrorTransferSize)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(ErrorTransferSize)];
            }
        }

        public ushort[] ExtentStatus => (ushort[])LateBoundObject[nameof(ExtentStatus)];

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

        public bool IsBasedOnUnderlyingRedundancy
        {
            get
            {
                if (LateBoundObject[nameof(IsBasedOnUnderlyingRedundancy)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsBasedOnUnderlyingRedundancy)];
            }
        }

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

        public string Name => (string)LateBoundObject[nameof(Name)];

        public ushort NameFormat
        {
            get
            {
                if (LateBoundObject[nameof(NameFormat)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(NameFormat)];
            }
        }

        public ushort NameNamespace
        {
            get
            {
                if (LateBoundObject[nameof(NameNamespace)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(NameNamespace)];
            }
        }

        public bool NoSinglePointOfFailure
        {
            get
            {
                if (LateBoundObject[nameof(NoSinglePointOfFailure)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(NoSinglePointOfFailure)];
            }
        }

        public ulong NumberOfBlocks
        {
            get
            {
                if (LateBoundObject[nameof(NumberOfBlocks)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(NumberOfBlocks)];
            }
        }

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

        public string OtherErrorDescription => (string)LateBoundObject[nameof(OtherErrorDescription)];

        public string[] OtherIdentifyingInfo => (string[])LateBoundObject[nameof(OtherIdentifyingInfo)];

        public string OtherNameFormat => (string)LateBoundObject[nameof(OtherNameFormat)];

        public string OtherNameNamespace => (string)LateBoundObject[nameof(OtherNameNamespace)];

        public ushort PackageRedundancy
        {
            get
            {
                if (LateBoundObject[nameof(PackageRedundancy)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(PackageRedundancy)];
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

        public bool Primordial
        {
            get
            {
                if (LateBoundObject[nameof(Primordial)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(Primordial)];
            }
        }

        public string Purpose => (string)LateBoundObject[nameof(Purpose)];

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

        public bool SequentialAccess
        {
            get
            {
                if (LateBoundObject[nameof(SequentialAccess)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(SequentialAccess)];
            }
        }

        public ulong StartingAddress
        {
            get
            {
                if (LateBoundObject[nameof(StartingAddress)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(StartingAddress)];
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

        public string SystemCreationClassName => (string)LateBoundObject[nameof(SystemCreationClassName)];

        public bool SystemLevelAddress
        {
            get
            {
                if (LateBoundObject[nameof(SystemLevelAddress)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(SystemLevelAddress)];
            }
        }

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

        public bool @volatile
        {
            get
            {
                if (LateBoundObject["volatile"] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject["volatile"];
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<Memory> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new Memory(mo)).ToList();

        public new static List<Memory> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new Memory(mo)).ToList();

        public static List<Memory> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Memory(mo)).ToList();

        public static List<Memory> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Memory(mo)).ToList();

        public static List<Memory> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new Memory(mo)).ToList();

        public static List<Memory> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new Memory(mo)).ToList();

        public static List<Memory> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Memory(mo)).ToList();

        public static List<Memory> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Memory(mo)).ToList();

        public static Memory CreateInstance() => new Memory(CreateInstance(ClassName));

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
                    Job = new ManagementPath(outParams.Properties["Job"].ToString());
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
