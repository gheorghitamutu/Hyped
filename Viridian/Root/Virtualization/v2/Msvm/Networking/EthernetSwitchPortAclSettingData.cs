using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Networking
{
    public class EthernetSwitchPortAclSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(EthernetSwitchPortAclSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public EthernetSwitchPortAclSettingData() : base(ClassName) { }

        public EthernetSwitchPortAclSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchPortAclSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchPortAclSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public EthernetSwitchPortAclSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public EthernetSwitchPortAclSettingData(ManagementPath path) : base(path, ClassName) { }

        public EthernetSwitchPortAclSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public EthernetSwitchPortAclSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public EthernetSwitchPortAclSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * This indicates if the ACL endpoint is an IPv4 or an IPv6 address or a MAC address.
         */
        public AclTypeValues AclType
        {
            get
            {
                if (LateBoundObject[nameof(AclType)] == null)
                {
                    return (AclTypeValues)System.Convert.ToInt32(4);
                }
                return (AclTypeValues)System.Convert.ToInt32(LateBoundObject[nameof(AclType)]);
            }
            set
            {
                if (AclTypeValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(AclType)] = null;
                }
                else
                {
                    LateBoundObject[nameof(AclType)] = value;
                }
                if (IsEmbedded == false && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * This indicates the Action of the ACL.
         * Could be Allow, Deny or Meter.
         */
        public ActionValues Action
        {
            get
            {
                if (LateBoundObject[nameof(Action)] == null)
                {
                    return (ActionValues)System.Convert.ToInt32(4);
                }
                return (ActionValues)System.Convert.ToInt32(LateBoundObject[nameof(Action)]);
            }
            set
            {
                if (ActionValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(Action)] = null;
                }
                else
                {
                    LateBoundObject[nameof(Action)] = value;
                }
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * This indicates if the ACL applies to the local or remote endpoint.
         */
        public ApplicabilityValues Applicability
        {
            get
            {
                if (LateBoundObject[nameof(Applicability)] == null)
                {
                    return (ApplicabilityValues)System.Convert.ToInt32(3);
                }
                return (ApplicabilityValues)System.Convert.ToInt32(LateBoundObject[nameof(Applicability)]);
            }
            set
            {
                if (ApplicabilityValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(Applicability)] = null;
                }
                else
                {
                    LateBoundObject[nameof(Applicability)] = value;
                }
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public string Description => (string)LateBoundObject[nameof(Description)];

        /*
         * This indicates if the ACL applies to ingress or egress direction.
         */
        public DirectionValues Direction
        {
            get
            {
                if (LateBoundObject[nameof(Direction)] == null)
                {
                    return (DirectionValues)System.Convert.ToInt32(3);
                }
                return (DirectionValues)System.Convert.ToInt32(LateBoundObject[nameof(Direction)]);
            }
            set
            {
                if (DirectionValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(Direction)] = null;
                }
                else
                {
                    LateBoundObject[nameof(Direction)] = value;
                }
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        /*
         * This is the local address of the VM.
         * This can be an IPv4, IPv6 or a Mac address.
         */
        public string LocalAddress
        {
            get
            {
                return (string)LateBoundObject[nameof(LocalAddress)];
            }
            set
            {
                LateBoundObject[nameof(LocalAddress)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * This is the local addres prefix length.
         */
        public byte LocalAddressPrefixLength
        {
            get
            {
                if (LateBoundObject[nameof(LocalAddressPrefixLength)] == null)
                {
                    return System.Convert.ToByte(0);
                }
                return (byte)LateBoundObject[nameof(LocalAddressPrefixLength)];
            }
            set
            {
                LateBoundObject[nameof(LocalAddressPrefixLength)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * This is the friendly name of the ACL.
         */
        public string Name
        {
            get
            {
                return (string)LateBoundObject[nameof(Name)];
            }
            set
            {
                LateBoundObject[nameof(Name)] = value;
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * This is the remote address of the VM.
         * This can bean IPv4, IPv6 or a Mac address.
         */
        public string RemoteAddress
        {
            get
            {
                return (string)LateBoundObject[nameof(RemoteAddress)];
            }
            set
            {
                LateBoundObject[nameof(RemoteAddress)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * This is the remote address prefix length.
         */
        public byte RemoteAddressPrefixLength
        {
            get
            {
                if (LateBoundObject[nameof(RemoteAddressPrefixLength)] == null)
                {
                    return System.Convert.ToByte(0);
                }
                return (byte)LateBoundObject[nameof(RemoteAddressPrefixLength)];
            }
            set
            {
                LateBoundObject[nameof(RemoteAddressPrefixLength)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        private void ResetAclType()
        {
            LateBoundObject[nameof(AclType)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetAction()
        {
            LateBoundObject[nameof(Action)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetApplicability()
        {
            LateBoundObject[nameof(Applicability)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetDirection()
        {
            LateBoundObject[nameof(Direction)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetLocalAddress()
        {
            LateBoundObject[nameof(LocalAddress)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetLocalAddressPrefixLength()
        {
            LateBoundObject[nameof(LocalAddressPrefixLength)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetName()
        {
            LateBoundObject[nameof(Name)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetRemoteAddress()
        {
            LateBoundObject[nameof(RemoteAddress)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetRemoteAddressPrefixLength()
        {
            LateBoundObject[nameof(RemoteAddressPrefixLength)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<EthernetSwitchPortAclSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortAclSettingData(mo)).ToList();

        public new static List<EthernetSwitchPortAclSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortAclSettingData(mo)).ToList();

        public static List<EthernetSwitchPortAclSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortAclSettingData(mo)).ToList();

        public static List<EthernetSwitchPortAclSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortAclSettingData(mo)).ToList();

        public static List<EthernetSwitchPortAclSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortAclSettingData(mo)).ToList();

        public static List<EthernetSwitchPortAclSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortAclSettingData(mo)).ToList();

        public static List<EthernetSwitchPortAclSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortAclSettingData(mo)).ToList();

        public static List<EthernetSwitchPortAclSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortAclSettingData(mo)).ToList();

        public static EthernetSwitchPortAclSettingData CreateInstance() => new EthernetSwitchPortAclSettingData(CreateInstance(ClassName));

        public enum AclTypeValues
        {
            Unknown0 = 0,
            MAC_Acl = 1,
            IPv4_Acl = 2,
            IPv6_Acl = 3,
            NULL_ENUM_VALUE = 4,
        }

        public enum ActionValues
        {
            Unknown0 = 0,
            Allow = 1,
            Deny = 2,
            Meter = 3,
            NULL_ENUM_VALUE = 4,
        }

        public enum ApplicabilityValues
        {
            Unknown0 = 0,
            Local = 1,
            Remote = 2,
            NULL_ENUM_VALUE = 3,
        }

        public enum DirectionValues
        {
            Unknown0 = 0,
            Incoming = 1,
            Outgoing = 2,
            NULL_ENUM_VALUE = 3,
        }
    }
}
