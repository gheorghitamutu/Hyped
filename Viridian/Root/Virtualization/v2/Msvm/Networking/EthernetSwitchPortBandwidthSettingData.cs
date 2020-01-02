using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Networking
{
    public class EthernetSwitchPortBandwidthSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(EthernetSwitchPortBandwidthSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public EthernetSwitchPortBandwidthSettingData() : base(ClassName) { }

        public EthernetSwitchPortBandwidthSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchPortBandwidthSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchPortBandwidthSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public EthernetSwitchPortBandwidthSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public EthernetSwitchPortBandwidthSettingData(ManagementPath path) : base(path, ClassName) { }

        public EthernetSwitchPortBandwidthSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public EthernetSwitchPortBandwidthSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public EthernetSwitchPortBandwidthSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * The peak bandwidth allowed from the port during bursts.
         */
        public ulong BurstLimit
        {
            get
            {
                if (LateBoundObject[nameof(BurstLimit)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(BurstLimit)];
            }
            set
            {
                LateBoundObject[nameof(BurstLimit)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The maximum burst size allowed.
         */
        public ulong BurstSize
        {
            get
            {
                if (LateBoundObject[nameof(BurstSize)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(BurstSize)];
            }
            set
            {
                LateBoundObject[nameof(BurstSize)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        /*
         * The bandwidth limit allowed for the port.
         */
        public ulong Limit
        {
            get
            {
                if (LateBoundObject[nameof(Limit)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(Limit)];
            }
            set
            {
                LateBoundObject[nameof(Limit)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }
        
        /*
         * The minimum absolute bandwidth guaranteed for the port.
         */
        public ulong Reservation
        {
            get
            {
                if (LateBoundObject[nameof(Reservation)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(Reservation)];
            }
            set
            {
                LateBoundObject[nameof(Reservation)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The minimum bandwidth in weight guaranteed for the port.
         */
        public ulong Weight
        {
            get
            {
                if (LateBoundObject[nameof(Weight)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(Weight)];
            }
            set
            {
                LateBoundObject[nameof(Weight)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        private void ResetBurstLimit()
        {
            LateBoundObject[nameof(BurstLimit)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetBurstSize()
        {
            LateBoundObject[nameof(BurstSize)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetLimit()
        {
            LateBoundObject[nameof(Limit)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetReservation()
        {
            LateBoundObject[nameof(Reservation)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetWeight()
        {
            LateBoundObject[nameof(Weight)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<EthernetSwitchPortBandwidthSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortBandwidthSettingData(mo)).ToList();

        public new static List<EthernetSwitchPortBandwidthSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortBandwidthSettingData(mo)).ToList();

        public static List<EthernetSwitchPortBandwidthSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortBandwidthSettingData(mo)).ToList();

        public static List<EthernetSwitchPortBandwidthSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortBandwidthSettingData(mo)).ToList();

        public static List<EthernetSwitchPortBandwidthSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortBandwidthSettingData(mo)).ToList();

        public static List<EthernetSwitchPortBandwidthSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortBandwidthSettingData(mo)).ToList();

        public static List<EthernetSwitchPortBandwidthSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortBandwidthSettingData(mo)).ToList();

        public static List<EthernetSwitchPortBandwidthSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortBandwidthSettingData(mo)).ToList();

        public static EthernetSwitchPortBandwidthSettingData CreateInstance() => new EthernetSwitchPortBandwidthSettingData(CreateInstance(ClassName));
    }
}
