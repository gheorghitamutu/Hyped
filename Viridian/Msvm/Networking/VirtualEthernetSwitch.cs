using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.Networking
{
    public class VirtualEthernetSwitch : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(VirtualEthernetSwitch)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public VirtualEthernetSwitch() : base(ClassName) { }

        public VirtualEthernetSwitch(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualEthernetSwitch(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualEthernetSwitch(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public VirtualEthernetSwitch(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public VirtualEthernetSwitch(ManagementPath path) : base(path, ClassName) { }

        public VirtualEthernetSwitch(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public VirtualEthernetSwitch(ManagementObject theObject) : base(theObject, ClassName) { }

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

        public ushort[] Dedicated => (ushort[])LateBoundObject[nameof(Dedicated)];

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

        /*
         * The maximum number of SR-IOV Virtual Function offloads available on this switch.
         */
        public uint MaxIOVOffloads
        {
            get
            {
                if (LateBoundObject[nameof(MaxIOVOffloads)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(MaxIOVOffloads)];
            }
        }

        /*
         * The maximum number of VMQ offloads allowed for a port on this switch.
         */
        public uint MaxVMQOffloads
        {
            get
            {
                if (LateBoundObject[nameof(MaxVMQOffloads)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(MaxVMQOffloads)];
            }
            set
            {
                LateBoundObject[nameof(MaxVMQOffloads)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string Name => (string)LateBoundObject[nameof(Name)];

        public string NameFormat => (string)LateBoundObject[nameof(NameFormat)];

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

        public string[] OtherDedicatedDescriptions => (string[])LateBoundObject[nameof(OtherDedicatedDescriptions)];

        public string OtherEnabledState => (string)LateBoundObject[nameof(OtherEnabledState)];

        public string[] OtherIdentifyingInfo => (string[])LateBoundObject[nameof(OtherIdentifyingInfo)];

        public ushort[] PowerManagementCapabilities => (ushort[])LateBoundObject[nameof(PowerManagementCapabilities)];

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

        public ushort ResetCapability
        {
            get
            {
                if (LateBoundObject[nameof(ResetCapability)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ResetCapability)];
            }
        }

        public string[] Roles => (string[])LateBoundObject[nameof(Roles)];

        public string Status => (string)LateBoundObject[nameof(Status)];

        public string[] StatusDescriptions => (string[])LateBoundObject[nameof(StatusDescriptions)];

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

        public VirtualEthernetSwitch(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<VirtualEthernetSwitch> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitch(mo)).ToList();

        public new static List<VirtualEthernetSwitch> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitch(mo)).ToList();

        public static List<VirtualEthernetSwitch> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitch(mo)).ToList();

        public static List<VirtualEthernetSwitch> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitch(mo)).ToList();

        public static List<VirtualEthernetSwitch> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitch(mo)).ToList();

        public static List<VirtualEthernetSwitch> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitch(mo)).ToList();

        public static List<VirtualEthernetSwitch> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitch(mo)).ToList();

        public static List<VirtualEthernetSwitch> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitch(mo)).ToList();

        public static VirtualEthernetSwitch CreateInstance() => new VirtualEthernetSwitch(CreateInstance(ClassName));
        
        public uint RequestStateChange(ushort RequestedState, DateTime TimeoutPeriod, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("RequestStateChange");
                inParams[nameof(RequestedState)] = (ushort)RequestedState;
                inParams["TimeoutPeriod"] = ToDmtfDateTime((DateTime)TimeoutPeriod);
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

        public uint SetPowerState(uint PowerState, DateTime Time)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetPowerState");
                inParams["PowerState"] = (uint)PowerState;
                inParams["Time"] = ToDmtfDateTime((DateTime)Time);
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
