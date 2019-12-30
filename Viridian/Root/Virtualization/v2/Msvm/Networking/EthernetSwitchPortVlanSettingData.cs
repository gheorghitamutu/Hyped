using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Networking
{
    public class EthernetSwitchPortVlanSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(EthernetSwitchPortVlanSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public EthernetSwitchPortVlanSettingData() : base(ClassName) { }

        public EthernetSwitchPortVlanSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchPortVlanSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchPortVlanSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public EthernetSwitchPortVlanSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public EthernetSwitchPortVlanSettingData(ManagementPath path) : base(path, ClassName) { }

        public EthernetSwitchPortVlanSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public EthernetSwitchPortVlanSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public EthernetSwitchPortVlanSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * The vlan ID in access mode.
         */
        public ushort AccessVlanId
        {
            get
            {
                if (LateBoundObject[nameof(AccessVlanId)] == null)
                {
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(AccessVlanId)];
            }
            set
            {
                LateBoundObject[nameof(AccessVlanId)] = value;
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
         * The vlan ID in trunk mode.
         */
        public ushort NativeVlanId
        {
            get
            {
                if (LateBoundObject[nameof(NativeVlanId)] == null)
                {
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(NativeVlanId)];
            }
            set
            {
                LateBoundObject[nameof(NativeVlanId)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The vlan operation modes.
         */
        public OperationModeValues OperationMode
        {
            get
            {
                if (LateBoundObject[nameof(OperationMode)] == null)
                {
                    return (OperationModeValues)System.Convert.ToInt32(0);
                }
                return (OperationModeValues)System.Convert.ToInt32(LateBoundObject[nameof(OperationMode)]);
            }
            set
            {
                if (OperationModeValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(OperationMode)] = null;
                }
                else
                {
                    LateBoundObject[nameof(OperationMode)] = value;
                }
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The primary vlan ID in private mode.
         */
        public ushort PrimaryVlanId
        {
            get
            {
                if (LateBoundObject[nameof(PrimaryVlanId)] == null)
                {
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(PrimaryVlanId)];
            }
            set
            {
                LateBoundObject[nameof(PrimaryVlanId)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The prune vlan ID bitmap in trunk mode.
         */
        public ushort[] PruneVlanIdArray
        {
            get
            {
                return (ushort[])LateBoundObject[nameof(PruneVlanIdArray)];
            }
            set
            {
                LateBoundObject[nameof(PruneVlanIdArray)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The private vlan modes.
         */
        public PvlanModeValues PvlanMode
        {
            get
            {
                if (LateBoundObject[nameof(PvlanMode)] == null)
                {
                    return (PvlanModeValues)System.Convert.ToInt32(0);
                }
                return (PvlanModeValues)System.Convert.ToInt32(LateBoundObject[nameof(PvlanMode)]);
            }
            set
            {
                if (PvlanModeValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(PvlanMode)] = null;
                }
                else
                {
                    LateBoundObject[nameof(PvlanMode)] = value;
                }
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The secondary vlan ID in private mode.
         */
        public ushort SecondaryVlanId
        {
            get
            {
                if (LateBoundObject[nameof(SecondaryVlanId)] == null)
                {
                    return System.Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(SecondaryVlanId)];
            }
            set
            {
                LateBoundObject[nameof(SecondaryVlanId)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The secondary vlan ID bitmap in private mode.
         */
        public ushort[] SecondaryVlanIdArray
        {
            get
            {
                return (ushort[])LateBoundObject[nameof(SecondaryVlanIdArray)];
            }
            set
            {
                LateBoundObject[nameof(SecondaryVlanIdArray)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The trunk vlan ID bitmap in trunk mode.
         */
        public ushort[] TrunkVlanIdArray
        {
            get
            {
                return (ushort[])LateBoundObject[nameof(TrunkVlanIdArray)];
            }
            set
            {
                LateBoundObject[nameof(TrunkVlanIdArray)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        private void ResetAccessVlanId()
        {
            LateBoundObject[nameof(AccessVlanId)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetNativeVlanId()
        {
            LateBoundObject[nameof(NativeVlanId)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetOperationMode()
        {
            LateBoundObject[nameof(OperationMode)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetPrimaryVlanId()
        {
            LateBoundObject[nameof(PrimaryVlanId)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetPruneVlanIdArray()
        {
            LateBoundObject[nameof(PruneVlanIdArray)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetPvlanMode()
        {
            LateBoundObject[nameof(PvlanMode)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetSecondaryVlanId()
        {
            LateBoundObject[nameof(SecondaryVlanId)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetSecondaryVlanIdArray()
        {
            LateBoundObject[nameof(SecondaryVlanIdArray)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetTrunkVlanIdArray()
        {
            LateBoundObject[nameof(TrunkVlanIdArray)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<EthernetSwitchPortVlanSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortVlanSettingData(mo)).ToList();

        public new static List<EthernetSwitchPortVlanSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortVlanSettingData(mo)).ToList();

        public static List<EthernetSwitchPortVlanSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortVlanSettingData(mo)).ToList();

        public static List<EthernetSwitchPortVlanSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortVlanSettingData(mo)).ToList();

        public static List<EthernetSwitchPortVlanSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortVlanSettingData(mo)).ToList();

        public static List<EthernetSwitchPortVlanSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortVlanSettingData(mo)).ToList();

        public static List<EthernetSwitchPortVlanSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortVlanSettingData(mo)).ToList();

        public static List<EthernetSwitchPortVlanSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortVlanSettingData(mo)).ToList();

        public static EthernetSwitchPortVlanSettingData CreateInstance() => new EthernetSwitchPortVlanSettingData(CreateInstance(ClassName));

        public enum OperationModeValues
        {
            Access = 1,
            Trunk = 2,
            Private = 3,
            NULL_ENUM_VALUE = 0,
        }

        public enum PvlanModeValues
        {
            Isolated = 1,
            Community = 2,
            Promiscuous = 3,
            NULL_ENUM_VALUE = 0,
        }
    }
}
