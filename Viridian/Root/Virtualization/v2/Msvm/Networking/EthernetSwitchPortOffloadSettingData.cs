using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Networking
{
    public class EthernetSwitchPortOffloadSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(EthernetSwitchPortOffloadSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public EthernetSwitchPortOffloadSettingData() : base(ClassName) { }

        public EthernetSwitchPortOffloadSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchPortOffloadSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public EthernetSwitchPortOffloadSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public EthernetSwitchPortOffloadSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public EthernetSwitchPortOffloadSettingData(ManagementPath path) : base(path, ClassName) { }

        public EthernetSwitchPortOffloadSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public EthernetSwitchPortOffloadSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public EthernetSwitchPortOffloadSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        /*
         * The interrupt moderation value for I/O virtualization (IOV) offloading.
         * The default is Adaptive.
         */
        public IOVInterruptModerationValues IOVInterruptModeration
        {
            get
            {
                if (LateBoundObject[nameof(IOVInterruptModeration)] == null)
                {
                    return (IOVInterruptModerationValues)System.Convert.ToInt32(301);
                }
                return (IOVInterruptModerationValues)System.Convert.ToInt32(LateBoundObject[nameof(IOVInterruptModeration)]);
            }
            set
            {
                if (IOVInterruptModerationValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(IOVInterruptModeration)] = null;
                }
                else
                {
                    LateBoundObject[nameof(IOVInterruptModeration)] = value;
                }
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The weight assigned to this port for I/O virtualization (IOV) offloading.
         * The weight isthe relative importance when assigning IOV resources.
         * Setting the IOVOffloadWeight property to 0 disables IOV offloading on the port.
         * The default is 0.
         */
        public uint IOVOffloadWeight
        {
            get
            {
                if (LateBoundObject[nameof(IOVOffloadWeight)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(IOVOffloadWeight)];
            }
            set
            {
                LateBoundObject[nameof(IOVOffloadWeight)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The number of queue pairs requested for this port for I/O virtualization (IOV) offloading.
         * The default is 1. 
         */
        public uint IOVQueuePairsRequested
        {
            get
            {
                if (LateBoundObject[nameof(IOVQueuePairsRequested)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(IOVQueuePairsRequested)];
            }
            set
            {
                LateBoundObject[nameof(IOVQueuePairsRequested)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The maximum number of SA offload slot allowed from the port.
         */
        public uint IPSecOffloadLimit
        {
            get
            {
                if (LateBoundObject[nameof(IPSecOffloadLimit)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(IPSecOffloadLimit)];
            }
            set
            {
                LateBoundObject[nameof(IPSecOffloadLimit)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The interrupt moderation count value for Packet Direct (PD).
         * The default is 0.
         */
        public uint PacketDirectModerationCount
        {
            get
            {
                if (LateBoundObject[nameof(PacketDirectModerationCount)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(PacketDirectModerationCount)];
            }
            set
            {
                LateBoundObject[nameof(PacketDirectModerationCount)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The interrupt moderation interval value for Packet Direct (PD).
         * The default is 0.
         */
        public uint PacketDirectModerationInterval
        {
            get
            {
                if (LateBoundObject[nameof(PacketDirectModerationInterval)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(PacketDirectModerationInterval)];
            }
            set
            {
                LateBoundObject[nameof(PacketDirectModerationInterval)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The number of processors used by the host for processing packets sent from this port in Packet Direct mode.
         * The default is 1.
         */
        public uint PacketDirectNumProcs
        {
            get
            {
                if (LateBoundObject[nameof(PacketDirectNumProcs)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(PacketDirectNumProcs)];
            }
            set
            {
                LateBoundObject[nameof(PacketDirectNumProcs)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Enable VMMQ offload if supported by hardware.
         * The default is true.
         */
        public bool VmmqEnabled
        {
            get
            {
                if (LateBoundObject[nameof(VmmqEnabled)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(VmmqEnabled)];
            }
            set
            {
                LateBoundObject[nameof(VmmqEnabled)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The number of queues to allocate when VRSS is enabled.
         * The default is 16.
         */
        public uint VmmqQueuePairs
        {
            get
            {
                if (LateBoundObject[nameof(VmmqQueuePairs)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(VmmqQueuePairs)];
            }
            set
            {
                LateBoundObject[nameof(VmmqQueuePairs)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The weight assigned to this port for virtual machine queue (VMQ) offloading.
         * The weight is the relative importance when assigning VMQ resources.
         * Setting the VMQOffloadWeight property to 0 disables VMQ on the port.
         * The default is 100.
         */
        public uint VMQOffloadWeight
        {
            get
            {
                if (LateBoundObject[nameof(VMQOffloadWeight)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(VMQOffloadWeight)];
            }
            set
            {
                LateBoundObject[nameof(VMQOffloadWeight)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Enable VRSS.
         * The default is true.
         */
        public bool VrssEnabled
        {
            get
            {
                if (LateBoundObject[nameof(VrssEnabled)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(VrssEnabled)];
            }
            set
            {
                LateBoundObject[nameof(VrssEnabled)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Whether to exclude primary VMQ processor from the VRSS indirection table when VRSS is enabled.
         * The default is false.
         */
        public bool VrssExcludePrimaryProcessor
        {
            get
            {
                if (LateBoundObject[nameof(VrssExcludePrimaryProcessor)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(VrssExcludePrimaryProcessor)];
            }
            set
            {
                LateBoundObject[nameof(VrssExcludePrimaryProcessor)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Whether to always do host-side VRSS when VRSS is enabled, regardless of RSS setting of the virtual NIC.
         * The default is false.
         */
        public bool VrssIndependentHostSpreading
        {
            get
            {
                if (LateBoundObject[nameof(VrssIndependentHostSpreading)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(VrssIndependentHostSpreading)];
            }
            set
            {
                LateBoundObject[nameof(VrssIndependentHostSpreading)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The minimum number of queues to allocate when VRSS is enabled.
         * The default is 1.
         */
        public uint VrssMinQueuePairs
        {
            get
            {
                if (LateBoundObject[nameof(VrssMinQueuePairs)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(VrssMinQueuePairs)];
            }
            set
            {
                LateBoundObject[nameof(VrssMinQueuePairs)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The queue scheduling mode to use when VRSS is enabled.
         * The default is static scheduling.
         */
        public uint VrssQueueSchedulingMode
        {
            get
            {
                if (LateBoundObject[nameof(VrssQueueSchedulingMode)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(VrssQueueSchedulingMode)];
            }
            set
            {
                LateBoundObject[nameof(VrssQueueSchedulingMode)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The vmbus channel affinity policy to use when VRSS is enabled.
         * The default is strong.
         */
        public uint VrssVmbusChannelAffinityPolicy
        {
            get
            {
                if (LateBoundObject[nameof(VrssVmbusChannelAffinityPolicy)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(VrssVmbusChannelAffinityPolicy)];
            }
            set
            {
                LateBoundObject[nameof(VrssVmbusChannelAffinityPolicy)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        private void ResetIOVInterruptModeration()
        {
            LateBoundObject[nameof(IOVInterruptModeration)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetIOVOffloadWeight()
        {
            LateBoundObject[nameof(IOVOffloadWeight)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetIOVQueuePairsRequested()
        {
            LateBoundObject[nameof(IOVQueuePairsRequested)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetIPSecOffloadLimit()
        {
            LateBoundObject[nameof(IPSecOffloadLimit)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetPacketDirectModerationCount()
        {
            LateBoundObject[nameof(PacketDirectModerationCount)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetPacketDirectModerationInterval()
        {
            LateBoundObject[nameof(PacketDirectModerationInterval)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetPacketDirectNumProcs()
        {
            LateBoundObject[nameof(PacketDirectNumProcs)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetVmmqEnabled()
        {
            LateBoundObject[nameof(VmmqEnabled)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetVmmqQueuePairs()
        {
            LateBoundObject[nameof(VmmqQueuePairs)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetVMQOffloadWeight()
        {
            LateBoundObject[nameof(VMQOffloadWeight)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetVrssEnabled()
        {
            LateBoundObject[nameof(VrssEnabled)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetVrssExcludePrimaryProcessor()
        {
            LateBoundObject[nameof(VrssExcludePrimaryProcessor)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetVrssIndependentHostSpreading()
        {
            LateBoundObject[nameof(VrssIndependentHostSpreading)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetVrssMinQueuePairs()
        {
            LateBoundObject[nameof(VrssMinQueuePairs)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetVrssQueueSchedulingMode()
        {
            LateBoundObject[nameof(VrssQueueSchedulingMode)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetVrssVmbusChannelAffinityPolicy()
        {
            LateBoundObject[nameof(VrssVmbusChannelAffinityPolicy)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<EthernetSwitchPortOffloadSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortOffloadSettingData(mo)).ToList();

        public new static List<EthernetSwitchPortOffloadSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortOffloadSettingData(mo)).ToList();

        public static List<EthernetSwitchPortOffloadSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortOffloadSettingData(mo)).ToList();

        public static List<EthernetSwitchPortOffloadSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortOffloadSettingData(mo)).ToList();

        public static List<EthernetSwitchPortOffloadSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortOffloadSettingData(mo)).ToList();

        public static List<EthernetSwitchPortOffloadSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortOffloadSettingData(mo)).ToList();

        public static List<EthernetSwitchPortOffloadSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortOffloadSettingData(mo)).ToList();

        public static List<EthernetSwitchPortOffloadSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new EthernetSwitchPortOffloadSettingData(mo)).ToList();

        public static EthernetSwitchPortOffloadSettingData CreateInstance() => new EthernetSwitchPortOffloadSettingData(CreateInstance(ClassName));

        public enum IOVInterruptModerationValues
        {
            Default = 0,
            Adaptive = 1,
            Off = 2,
            Low = 100,
            Medium = 200,
            High = 300,
            NULL_ENUM_VALUE = 301,
        }
    }
}
