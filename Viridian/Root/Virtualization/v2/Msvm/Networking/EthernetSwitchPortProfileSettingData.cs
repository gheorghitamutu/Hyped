using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Networking
{
    public class EthernetSwitchPortProfileSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(EthernetSwitchPortProfileSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public EthernetSwitchPortProfileSettingData() : base(ClassName) { }

        public EthernetSwitchPortProfileSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchPortProfileSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchPortProfileSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public EthernetSwitchPortProfileSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public EthernetSwitchPortProfileSettingData(ManagementPath path) : base(path, ClassName) { }

        public EthernetSwitchPortProfileSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public EthernetSwitchPortProfileSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public EthernetSwitchPortProfileSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        /*
         * The CDN Label Id.
         */
        public uint CdnLabelId
        {
            get
            {
                if (LateBoundObject[nameof(CdnLabelId)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(CdnLabelId)];
            }
            set
            {
                LateBoundObject[nameof(CdnLabelId)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The CDN label string.
         */
        public string CdnLabelString
        {
            get
            {
                return (string)LateBoundObject[nameof(CdnLabelString)];
            }
            set
            {
                LateBoundObject[nameof(CdnLabelString)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        /*
         * Unique device identifier of the sub-interface.
         */
        public string NetCfgInstanceId
        {
            get
            {
                return (string)LateBoundObject[nameof(NetCfgInstanceId)];
            }
            set
            {
                LateBoundObject[nameof(NetCfgInstanceId)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The PCI bus number.
         */
        public uint PciBusNumber
        {
            get
            {
                if (LateBoundObject[nameof(PciBusNumber)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(PciBusNumber)];
            }
            set
            {
                LateBoundObject[nameof(PciBusNumber)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The PCI device number.
         */
        public uint PciDeviceNumber
        {
            get
            {
                if (LateBoundObject[nameof(PciDeviceNumber)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(PciDeviceNumber)];
            }
            set
            {
                LateBoundObject[nameof(PciDeviceNumber)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The PCI function number.
         */
        public uint PciFunctionNumber
        {
            get
            {
                if (LateBoundObject[nameof(PciFunctionNumber)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(PciFunctionNumber)];
            }
            set
            {
                LateBoundObject[nameof(PciFunctionNumber)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The PCI segment number.
         */
        public uint PciSegmentNumber
        {
            get
            {
                if (LateBoundObject[nameof(PciSegmentNumber)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(PciSegmentNumber)];
            }
            set
            {
                LateBoundObject[nameof(PciSegmentNumber)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Additional data for the Port Profile.
         */
        public uint ProfileData
        {
            get
            {
                if (LateBoundObject[nameof(ProfileData)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(ProfileData)];
            }
            set
            {
                LateBoundObject[nameof(ProfileData)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The id of the Port Profile.
         */
        public string ProfileId
        {
            get
            {
                return (string)LateBoundObject[nameof(ProfileId)];
            }
            set
            {
                LateBoundObject[nameof(ProfileId)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The name of the Port Profile.
         */
        public string ProfileName
        {
            get
            {
                return (string)LateBoundObject[nameof(ProfileName)];
            }
            set
            {
                LateBoundObject[nameof(ProfileName)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The id of the Vendor defining the profile.
         */
        public string VendorId
        {
            get
            {
                return (string)LateBoundObject[nameof(VendorId)];
            }
            set
            {
                LateBoundObject[nameof(VendorId)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The name of the Vendor defining the profile.
         */
        public string VendorName
        {
            get
            {
                return (string)LateBoundObject[nameof(VendorName)];
            }
            set
            {
                LateBoundObject[nameof(VendorName)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        private void ResetCdnLabelId()
        {
            LateBoundObject[nameof(CdnLabelId)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetCdnLabelString()
        {
            LateBoundObject[nameof(CdnLabelString)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetNetCfgInstanceId()
        {
            LateBoundObject[nameof(NetCfgInstanceId)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetPciBusNumber()
        {
            LateBoundObject[nameof(PciBusNumber)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetPciDeviceNumber()
        {
            LateBoundObject[nameof(PciDeviceNumber)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetPciFunctionNumber()
        {
            LateBoundObject[nameof(PciFunctionNumber)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetPciSegmentNumber()
        {
            LateBoundObject[nameof(PciSegmentNumber)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetProfileData()
        {
            LateBoundObject[nameof(ProfileData)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetProfileId()
        {
            LateBoundObject[nameof(ProfileId)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetProfileName()
        {
            LateBoundObject[nameof(ProfileName)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetVendorId()
        {
            LateBoundObject[nameof(VendorId)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetVendorName()
        {
            LateBoundObject[nameof(VendorName)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<EthernetSwitchPortProfileSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortProfileSettingData(mo)).ToList();

        public new static List<EthernetSwitchPortProfileSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortProfileSettingData(mo)).ToList();

        public static List<EthernetSwitchPortProfileSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortProfileSettingData(mo)).ToList();

        public static List<EthernetSwitchPortProfileSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortProfileSettingData(mo)).ToList();

        public static List<EthernetSwitchPortProfileSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortProfileSettingData(mo)).ToList();

        public static List<EthernetSwitchPortProfileSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortProfileSettingData(mo)).ToList();

        public static List<EthernetSwitchPortProfileSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortProfileSettingData(mo)).ToList();

        public static List<EthernetSwitchPortProfileSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortProfileSettingData(mo)).ToList();

        public static EthernetSwitchPortProfileSettingData CreateInstance() => new EthernetSwitchPortProfileSettingData(CreateInstance(ClassName));
    }
}
