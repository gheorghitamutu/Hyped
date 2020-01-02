using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Networking
{
    public class EthernetSwitchExtension : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(EthernetSwitchExtension)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public EthernetSwitchExtension() : base(ClassName) { }

        public EthernetSwitchExtension(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchExtension(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchExtension(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public EthernetSwitchExtension(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public EthernetSwitchExtension(ManagementPath path) : base(path, ClassName) { }

        public EthernetSwitchExtension(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public EthernetSwitchExtension(ManagementObject theObject) : base(theObject, ClassName) { }

        public EthernetSwitchExtension(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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

        /*
         * CreationClassName indicates the name of the class or the subclass used in the creation of an instance.
         * When used with the other key properties of this class, this property allows all instances of this class and its subclasses to be uniquely identified.
         */
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

        /*
         * Indicates the type of the extension component.
         */
        public ExtensionTypeValues ExtensionType
        {
            get
            {
                if (LateBoundObject[nameof(ExtensionType)] == null)
                {
                    return (ExtensionTypeValues)System.Convert.ToInt32(6);
                }
                return (ExtensionTypeValues)System.Convert.ToInt32(LateBoundObject[nameof(ExtensionType)]);
            }
        }

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

        /*
         * The unique name of the extension component.
         */
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

        /*
         * This is the System Creation Class Name, which is Msvm_VirtualEthernetSwitch.
         */
        public string SystemCreationClassName => (string)LateBoundObject[nameof(SystemCreationClassName)];

        /*
         * The name of the virtual switch to which the extension instance is bound to.
         */
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

        /*
         * Indicates the vendor providing the extension.
         */
        public string Vendor => (string)LateBoundObject[nameof(Vendor)];

        /*
         * The version of the extension in a format of "major.minor", for example "2.0".
         */
        public string Version => (string)LateBoundObject[nameof(Version)];

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<EthernetSwitchExtension> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchExtension(mo)).ToList();

        public new static List<EthernetSwitchExtension> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchExtension(mo)).ToList();

        public static List<EthernetSwitchExtension> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchExtension(mo)).ToList();

        public static List<EthernetSwitchExtension> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchExtension(mo)).ToList();

        public static List<EthernetSwitchExtension> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchExtension(mo)).ToList();

        public static List<EthernetSwitchExtension> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchExtension(mo)).ToList();

        public static List<EthernetSwitchExtension> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchExtension(mo)).ToList();

        public static List<EthernetSwitchExtension> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchExtension(mo)).ToList();

        public static EthernetSwitchExtension CreateInstance() => new EthernetSwitchExtension(CreateInstance(ClassName));

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

        public enum ExtensionTypeValues
        {
            Unknown0 = 0,
            Capture = 1,
            Filter = 2,
            Forwarding = 3,
            Monitoring = 4,
            Native = 5,
            NULL_ENUM_VALUE = 6,
        }
    }
}
