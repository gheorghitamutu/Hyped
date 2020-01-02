using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Networking
{
    public class EthernetSwitchPortFeatureSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(EthernetSwitchPortFeatureSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public EthernetSwitchPortFeatureSettingData() : base(ClassName) { }

        public EthernetSwitchPortFeatureSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchPortFeatureSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchPortFeatureSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public EthernetSwitchPortFeatureSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public EthernetSwitchPortFeatureSettingData(ManagementPath path) : base(path, ClassName) { }

        public EthernetSwitchPortFeatureSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public EthernetSwitchPortFeatureSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public EthernetSwitchPortFeatureSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string Caption
        {
            get
            {
                return (string)LateBoundObject[nameof(Caption)];
            }
            set
            {
                LateBoundObject[nameof(Caption)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string Description
        {
            get
            {
                return (string)LateBoundObject[nameof(Description)];
            }
            set
            {
                LateBoundObject[nameof(Description)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string ElementName
        {
            get
            {
                return (string)LateBoundObject[nameof(ElementName)];
            }
            set
            {
                LateBoundObject[nameof(ElementName)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string InstanceID
        {
            get
            {
                return (string)LateBoundObject[nameof(InstanceID)];
            }
            set
            {
                LateBoundObject[nameof(InstanceID)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        private void ResetCaption()
        {
            LateBoundObject[nameof(Caption)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetDescription()
        {
            LateBoundObject[nameof(Description)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetElementName()
        {
            LateBoundObject[nameof(ElementName)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<EthernetSwitchPortFeatureSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortFeatureSettingData(mo)).ToList();

        public new static List<EthernetSwitchPortFeatureSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortFeatureSettingData(mo)).ToList();

        public static List<EthernetSwitchPortFeatureSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortFeatureSettingData(mo)).ToList();

        public static List<EthernetSwitchPortFeatureSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortFeatureSettingData(mo)).ToList();

        public static List<EthernetSwitchPortFeatureSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortFeatureSettingData(mo)).ToList();

        public static List<EthernetSwitchPortFeatureSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortFeatureSettingData(mo)).ToList();

        public static List<EthernetSwitchPortFeatureSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortFeatureSettingData(mo)).ToList();

        public static List<EthernetSwitchPortFeatureSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortFeatureSettingData(mo)).ToList();

        public static EthernetSwitchPortFeatureSettingData CreateInstance() => new EthernetSwitchPortFeatureSettingData(CreateInstance(ClassName));
    }
}
