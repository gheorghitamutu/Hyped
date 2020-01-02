using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Networking
{
    public class VirtualEthernetSwitchBandwidthSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(VirtualEthernetSwitchBandwidthSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public VirtualEthernetSwitchBandwidthSettingData() : base(ClassName) { }

        public VirtualEthernetSwitchBandwidthSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualEthernetSwitchBandwidthSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualEthernetSwitchBandwidthSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public VirtualEthernetSwitchBandwidthSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public VirtualEthernetSwitchBandwidthSettingData(ManagementPath path) : base(path, ClassName) { }

        public VirtualEthernetSwitchBandwidthSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public VirtualEthernetSwitchBandwidthSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public VirtualEthernetSwitchBandwidthSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        /*
         * Specifies the absolute bandwidth reservation for un-classified flowson the underlying adapter.
         */
        public ulong DefaultFlowReservation
        {
            get
            {
                if (LateBoundObject[nameof(DefaultFlowReservation)] == null)
                {
                    return System.Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(DefaultFlowReservation)];
            }
            set
            {
                LateBoundObject[nameof(DefaultFlowReservation)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Specifies the bandwidth reservation in weight for un-classified flowson the underlying adapter.
         */
        public ulong DefaultFlowWeight
        {
            get
            {
                if (LateBoundObject[nameof(DefaultFlowWeight)] == null)
                {
                    return System.Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(DefaultFlowWeight)];
            }
            set
            {
                LateBoundObject[nameof(DefaultFlowWeight)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        private void ResetDefaultFlowReservation()
        {
            LateBoundObject[nameof(DefaultFlowReservation)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetDefaultFlowWeight()
        {
            LateBoundObject[nameof(DefaultFlowWeight)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<VirtualEthernetSwitchBandwidthSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchBandwidthSettingData(mo)).ToList();

        public new static List<VirtualEthernetSwitchBandwidthSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchBandwidthSettingData(mo)).ToList();

        public static List<VirtualEthernetSwitchBandwidthSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchBandwidthSettingData(mo)).ToList();

        public static List<VirtualEthernetSwitchBandwidthSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchBandwidthSettingData(mo)).ToList();

        public static List<VirtualEthernetSwitchBandwidthSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchBandwidthSettingData(mo)).ToList();

        public static List<VirtualEthernetSwitchBandwidthSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchBandwidthSettingData(mo)).ToList();

        public static List<VirtualEthernetSwitchBandwidthSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchBandwidthSettingData(mo)).ToList();

        public static List<VirtualEthernetSwitchBandwidthSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualEthernetSwitchBandwidthSettingData(mo)).ToList();

        public static VirtualEthernetSwitchBandwidthSettingData CreateInstance() => new VirtualEthernetSwitchBandwidthSettingData(CreateInstance(ClassName));
    }
}
