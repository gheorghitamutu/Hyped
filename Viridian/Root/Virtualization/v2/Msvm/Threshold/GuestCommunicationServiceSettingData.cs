using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Threshold
{
    public class GuestCommunicationServiceSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(GuestCommunicationServiceSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public GuestCommunicationServiceSettingData() : base(ClassName) { }

        public GuestCommunicationServiceSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public GuestCommunicationServiceSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public GuestCommunicationServiceSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public GuestCommunicationServiceSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public GuestCommunicationServiceSettingData(ManagementPath path) : base(path, ClassName) { }

        public GuestCommunicationServiceSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public GuestCommunicationServiceSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public GuestCommunicationServiceSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        /*
         * EnabledStatePolicy is an integer enumeration that indicates the enabled, disabled or default state of the Msvm_GuestCommunicationServiceSettingData.
         * Enabled (2) indicates that the communication service is set to the enabled state.
         * Disabled (3) indicates that the communication service is set to the disabled state.
         * Deferred (8) indicates that the communication service state depends on DefaultEnabledStatePolicy in Msvm_GuestInterfaceComponentSettingData.
         */
        public EnabledStatePolicyValues EnabledStatePolicy
        {
            get
            {
                if (LateBoundObject[nameof(EnabledStatePolicy)] == null)
                {
                    return (EnabledStatePolicyValues)System.Convert.ToInt32(0);
                }
                return (EnabledStatePolicyValues)System.Convert.ToInt32(LateBoundObject[nameof(EnabledStatePolicy)]);
            }
            set
            {
                if (EnabledStatePolicyValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(EnabledStatePolicy)] = null;
                }
                else
                {
                    LateBoundObject[nameof(EnabledStatePolicy)] = value;
                }
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        /*
         * GUID of the service.
         */
        public string Name => (string)LateBoundObject[nameof(Name)];

        private void ResetEnabledStatePolicy()
        {
            LateBoundObject[nameof(EnabledStatePolicy)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<GuestCommunicationServiceSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new GuestCommunicationServiceSettingData(mo)).ToList();

        public new static List<GuestCommunicationServiceSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new GuestCommunicationServiceSettingData(mo)).ToList();

        public static List<GuestCommunicationServiceSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestCommunicationServiceSettingData(mo)).ToList();

        public static List<GuestCommunicationServiceSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestCommunicationServiceSettingData(mo)).ToList();

        public static List<GuestCommunicationServiceSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new GuestCommunicationServiceSettingData(mo)).ToList();

        public static List<GuestCommunicationServiceSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new GuestCommunicationServiceSettingData(mo)).ToList();

        public static List<GuestCommunicationServiceSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestCommunicationServiceSettingData(mo)).ToList();

        public static List<GuestCommunicationServiceSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new GuestCommunicationServiceSettingData(mo)).ToList();

        public static GuestCommunicationServiceSettingData CreateInstance() => new GuestCommunicationServiceSettingData(CreateInstance(ClassName));

        public enum EnabledStatePolicyValues
        {
            Enabled = 2,
            Disabled = 3,
            Deferred = 8,
            NULL_ENUM_VALUE = 0,
        }
    }
}
