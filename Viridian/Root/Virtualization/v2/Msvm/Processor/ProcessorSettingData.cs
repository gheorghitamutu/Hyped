using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Processor
{
    public class ProcessorSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(ProcessorSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public ProcessorSettingData() : base(ClassName) { }

        public ProcessorSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ProcessorSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ProcessorSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public ProcessorSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public ProcessorSettingData(ManagementPath path) : base(path, ClassName) { }

        public ProcessorSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public ProcessorSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public ProcessorSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public string Address => (string)LateBoundObject[nameof(Address)];

        public string AddressOnParent => (string)LateBoundObject[nameof(AddressOnParent)];

        public string AllocationUnits => (string)LateBoundObject[nameof(AllocationUnits)];

        public bool AutomaticAllocation
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticAllocation)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(AutomaticAllocation)];
            }
        }

        public bool AutomaticDeallocation
        {
            get
            {
                if (LateBoundObject[nameof(AutomaticDeallocation)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(AutomaticDeallocation)];
            }
        }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public string[] Connection => (string[])LateBoundObject[nameof(Connection)];

        public ushort ConsumerVisibility
        {
            get
            {
                if (LateBoundObject[nameof(ConsumerVisibility)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ConsumerVisibility)];
            }
        }

        /*
         * The Cpu Group Id this VM is bound to.
         * When value is 0 it means is not bound to a specific cpu group. 
         */
        public string CpuGroupId => (string)LateBoundObject[nameof(CpuGroupId)];

        public string Description => (string)LateBoundObject[nameof(Description)];

        /*
         * Indicates whether the protections for speculative execution should be disabled for the virtual processors.
         */
        public bool DisableSpeculationControls
        {
            get
            {
                if (LateBoundObject[nameof(DisableSpeculationControls)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(DisableSpeculationControls)];
            }
            set
            {
                LateBoundObject[nameof(DisableSpeculationControls)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        /*
         * Indicates whether the VM should enable features that increase the protection of host  resources from workload running in the VM.
         */
        public bool EnableHostResourceProtection
        {
            get
            {
                if (LateBoundObject[nameof(EnableHostResourceProtection)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(EnableHostResourceProtection)];
            }
        }

        /*
         * Indicates whether Hyper-V should expose virtualized IPT (Intel Processor Trace) facilities to the VM.
         */
        public bool EnablePerfmonIpt
        {
            get
            {
                if (LateBoundObject[nameof(EnablePerfmonIpt)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(EnablePerfmonIpt)];
            }
        }

        /*
         * Indicates whether Hyper-V should expose virtualized LBR (Last Branch Record) facilities to the VM.
         */
        public bool EnablePerfmonLbr
        {
            get
            {
                if (LateBoundObject[nameof(EnablePerfmonLbr)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(EnablePerfmonLbr)];
            }
        }

        /*
         * Indicates whether Hyper-V should expose virtualized PEBS (Processor Event-Based Sampling) facilities to the VM.
         */
        public bool EnablePerfmonPebs
        {
            get
            {
                if (LateBoundObject[nameof(EnablePerfmonPebs)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(EnablePerfmonPebs)];
            }
        }

        /*
         * Indicates whether Hyper-V should expose virtualized PMU (Performance Monitoring Unit) facilities to the VM.
         */
        public bool EnablePerfmonPmu
        {
            get
            {
                if (LateBoundObject[nameof(EnablePerfmonPmu)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(EnablePerfmonPmu)];
            }
        }

        /*
         * Indicates whether Hyper-V should expose virtualized hardware virtualization extensionsto the VM.
         */
        public bool ExposeVirtualizationExtensions
        {
            get
            {
                if (LateBoundObject[nameof(ExposeVirtualizationExtensions)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(ExposeVirtualizationExtensions)];
            }
        }

        /*
         * Indicates whether Hyper-V should report that a hypervisor is present to the nested guest.
         */
        public bool HideHypervisorPresent
        {
            get
            {
                if (LateBoundObject[nameof(HideHypervisorPresent)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(HideHypervisorPresent)];
            }
        }

        public string[] HostResource => (string[])LateBoundObject[nameof(HostResource)];

        /*
         * Indicates the number of SMT threads per core reported to the guest.
         * This reporting is independent of whether the hardware for SMT is present.
         */
        public ulong HwThreadsPerCore
        {
            get
            {
                if (LateBoundObject[nameof(HwThreadsPerCore)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(HwThreadsPerCore)];
            }
        }

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

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
        }

        /*
         * Indicates whether the virtual machine should lower the CPU identifier.
         * Some older operating systems may require you to limit processor functionality in this way in order to run.
         */
        public bool LimitCPUID
        {
            get
            {
                if (LateBoundObject[nameof(LimitCPUID)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(LimitCPUID)];
            }
        }

        /*
         * Indicates whether the VM should limit the CPU features exposed to the operating system.
         * Limiting the processor features enables the VM to be migrated to different host computer systems with different processors.
         * Migrating VMs between computers with processors from different vendors is not supported.
         */
        public bool LimitProcessorFeatures
        {
            get
            {
                if (LateBoundObject[nameof(LimitProcessorFeatures)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(LimitProcessorFeatures)];
            }
        }

        public ushort MappingBehavior
        {
            get
            {
                if (LateBoundObject[nameof(MappingBehavior)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(MappingBehavior)];
            }
        }

        /*
         * The maximum number of NUMA nodes that can be observed within the VM as belonging to a single processor socket.
         */
        public ulong MaxNumaNodesPerSocket
        {
            get
            {
                if (LateBoundObject[nameof(MaxNumaNodesPerSocket)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(MaxNumaNodesPerSocket)];
            }
        }

        /*
         * The maximum number of virtual processors that can be observed within the VM as belonging to a single virtual NUMA node.
         */
        public ulong MaxProcessorsPerNumaNode
        {
            get
            {
                if (LateBoundObject[nameof(MaxProcessorsPerNumaNode)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(MaxProcessorsPerNumaNode)];
            }
        }

        public string OtherResourceType => (string)LateBoundObject[nameof(OtherResourceType)];

        public string Parent => (string)LateBoundObject[nameof(Parent)];

        public string PoolID => (string)LateBoundObject[nameof(PoolID)];

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
        }

        public string ResourceSubType => (string)LateBoundObject[nameof(ResourceSubType)];

        public ushort ResourceType
        {
            get
            {
                if (LateBoundObject[nameof(ResourceType)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ResourceType)];
            }
        }

        public ulong VirtualQuantity
        {
            get
            {
                if (LateBoundObject[nameof(VirtualQuantity)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(VirtualQuantity)];
            }
        }

        public string VirtualQuantityUnits => (string)LateBoundObject[nameof(VirtualQuantityUnits)];

        public uint Weight
        {
            get
            {
                if (LateBoundObject[nameof(Weight)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(Weight)];
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<ProcessorSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorSettingData(mo)).ToList();

        public new static List<ProcessorSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorSettingData(mo)).ToList();

        public static List<ProcessorSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorSettingData(mo)).ToList();

        public static List<ProcessorSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorSettingData(mo)).ToList();

        public static List<ProcessorSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorSettingData(mo)).ToList();

        public static List<ProcessorSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorSettingData(mo)).ToList();

        public static List<ProcessorSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorSettingData(mo)).ToList();

        public static List<ProcessorSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ProcessorSettingData(mo)).ToList();

        public static ProcessorSettingData CreateInstance() => new ProcessorSettingData(CreateInstance(ClassName));
    }
}
