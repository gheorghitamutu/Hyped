using System;
using System.Management;
using System.Globalization;

namespace Viridian.Msvm.VirtualSystem
{
    // Functions ShouldSerialize<PropertyName> are functions used by VS property browser to check if a particular property has to be serialized. These functions are added for all ValueType properties ( properties of type Int32, BOOL etc.. which cannot be set to null). These functions use Is<PropertyName>Null function. These functions are also used in the TypeConverter implementation for the properties to check for NULL value of property so that an empty value can be shown in Property browser in case of Drag and Drop in Visual studio.
    // Functions Is<PropertyName>Null() are used to check if a property is NULL.
    // Functions Reset<PropertyName> are added for Nullable Read/Write properties. These functions are used by VS designer in property browser to set a property to NULL.
    // Every property added to the class for WMI property has attributes set to define its behavior in Visual Studio designer and also to define a TypeConverter to be used.
    // Datetime conversion functions ToDateTime and ToDmtfDateTime are added to the class to convert DMTF datetime to System.DateTime and vice-versa.
    // An Early Bound class generated for the WMI class.Msvm_SummaryInformation
    public class SummaryInformation : IDisposable
    {

        // Private property to hold the WMI namespace in which the class resides.
        private static string CreatedWmiNamespace = "root\\virtualization\\v2";

        // Private property to hold the name of WMI class which created this class.
        private static string CreatedClassName = "Msvm_SummaryInformation";

        // Underlying lateBound WMI object.
        private ManagementObject PrivateLateBoundObject;

        // Private variable to hold the embedded property representing the instance.
        private readonly ManagementBaseObject embeddedObj;

        // Flag to indicate if the instance is an embedded object.
        private bool isEmbedded;

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public SummaryInformation() => InitializeObject(null, null, null);

        public SummaryInformation(string keyInstanceID) => InitializeObject(null, new ManagementPath(ConstructPath(keyInstanceID)), null);

        public SummaryInformation(ManagementScope mgmtScope, string keyInstanceID) => InitializeObject(mgmtScope, new ManagementPath(ConstructPath(keyInstanceID)), null);

        public SummaryInformation(ManagementPath path, ObjectGetOptions getOptions) => InitializeObject(null, path, getOptions);

        public SummaryInformation(ManagementScope mgmtScope, ManagementPath path) => InitializeObject(mgmtScope, path, null);

        public SummaryInformation(ManagementPath path) => InitializeObject(null, path, null);

        public SummaryInformation(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) => InitializeObject(mgmtScope, path, getOptions);

        public SummaryInformation(ManagementObject theObject)
        {
            Initialize();
            if (theObject != null && CheckIfProperClass(theObject) == true)
            {
                PrivateLateBoundObject = theObject;
                SystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
                LateBoundObject = PrivateLateBoundObject;
            }
            else
            {
                throw new ArgumentException("Class name does not match.");
            }
        }

        public SummaryInformation(ManagementBaseObject theObject)
        {
            Initialize();
            if (theObject != null && CheckIfProperClass(theObject) == true)
            {
                embeddedObj = theObject;
                SystemProperties = new ManagementSystemProperties(theObject);
                LateBoundObject = embeddedObj;
                isEmbedded = true;
            }
            else
            {
                throw new ArgumentException("Class name does not match.");
            }
        }

        // Property returns the namespace of the WMI class.
        public string OriginatingNamespace => "root\\virtualization\\v2";

        public string ManagementClassName
        {
            get
            {
                string strRet = CreatedClassName;
                if (LateBoundObject != null)
                {
                    if (LateBoundObject.ClassPath != null)
                    {
                        strRet = (string)LateBoundObject["__CLASS"];
                        if (string.IsNullOrEmpty(strRet))
                        {
                            strRet = CreatedClassName;
                        }
                    }
                }
                return strRet;
            }
        }

        // Property pointing to an embedded object to get System properties of the WMI object.
        public ManagementSystemProperties SystemProperties { get; private set; }

        // Property returning the underlying lateBound object.
        public ManagementBaseObject LateBoundObject { get; private set; }

        // ManagementScope of the object.
        public ManagementScope Scope
        {
            get
            {
                if (isEmbedded == false)
                {
                    return PrivateLateBoundObject.Scope;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (isEmbedded == false)
                {
                    PrivateLateBoundObject.Scope = value;
                }
            }
        }

        // Property to show the commit behavior for the WMI object. If true, WMI object will be automatically saved after each property modification.(ie. Put() is called after modification of a property).
        public bool AutoCommit { get; set; }

        // The ManagementPath of the underlying WMI object.
        public ManagementPath Path
        {
            get
            {
                if (isEmbedded == false)
                {
                    return PrivateLateBoundObject.Path;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (isEmbedded == false)
                {
                    if (CheckIfProperClass(null, value, null) != true)
                    {
                        throw new ArgumentException("Class name does not match.");
                    }
                    PrivateLateBoundObject.Path = value;
                }
            }
        }

        // Public static scope property which is used by the various methods.
        public static ManagementScope StaticScope { get; set; } = null;

        /*
         * The identifier of the physical graphics processing unit (GPU) allocated to this virtual machine (VM). This property only applies to VMs that use RemoteFX.
         */
        public string AllocatedGPU => (string)LateBoundObject[nameof(AllocatedGPU)];

        public bool IsApplicationHealthNull
        {
            get
            {
                if (LateBoundObject[nameof(ApplicationHealth)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * The current application health status for the virtual system.
         * This property may be one of the following values: 
         * "OK"; "Application Critical"; "Disabled".
         * For more information, see the documentation for the StatusDescriptions property of the Msvm_HeartbeatComponent class. 
         * This property is not valid for instances of Msvm_SummaryInformation representing a virtual system snapshot. 
         */
        public ApplicationHealthValues ApplicationHealth
        {
            get
            {
                if (LateBoundObject[nameof(ApplicationHealth)] == null)
                {
                    return (ApplicationHealthValues)Convert.ToInt32(0);
                }
                return (ApplicationHealthValues)Convert.ToInt32(LateBoundObject[nameof(ApplicationHealth)]);
            }
        }

        /*
         * An array of Msvm_ConcreteJob instances representing any asynchronous operations related to the virtual system which are currently executing.
         * This property is not valid for instances of Msvm_SummaryInformation representing a virtual system snapshot.
         */
        public ManagementBaseObject[] AsynchronousTasks => (ManagementBaseObject[])LateBoundObject[nameof(AsynchronousTasks)];

        public bool IsAvailableMemoryBufferNull
        {
            get
            {
                if (LateBoundObject[nameof(AvailableMemoryBuffer)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * The available memory buffer percentage in the virtual system.
         */
        public int AvailableMemoryBuffer
        {
            get
            {
                if (LateBoundObject[nameof(AvailableMemoryBuffer)] == null)
                {
                    return Convert.ToInt32(0);
                }
                return (int)LateBoundObject[nameof(AvailableMemoryBuffer)];
            }
        }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public bool IsCreationTimeNull
        {
            get
            {
                if (LateBoundObject[nameof(CreationTime)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public DateTime CreationTime
        {
            get
            {
                if (LateBoundObject[nameof(CreationTime)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(CreationTime)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public bool IsEnabledStateNull
        {
            get
            {
                if (LateBoundObject[nameof(EnabledState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ushort EnabledState
        {
            get
            {
                if (LateBoundObject[nameof(EnabledState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(EnabledState)];
            }
        }

        public bool IsEnhancedSessionModeStateNull
        {
            get
            {
                if (LateBoundObject[nameof(EnhancedSessionModeState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ushort EnhancedSessionModeState
        {
            get
            {
                if (LateBoundObject[nameof(EnhancedSessionModeState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(EnhancedSessionModeState)];
            }
        }

        /*
         * The name of the guest operating system, if available. If this information is not available,
         * the value of this property is NULL. This property is not valid for 
         * instances of Msvm_SummaryInformation representing a virtual system snapshot.
         */
        public string GuestOperatingSystem => (string)LateBoundObject[nameof(GuestOperatingSystem)];

        public bool IsHealthStateNull
        {
            get
            {
                if (LateBoundObject[nameof(HealthState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ushort HealthState
        {
            get
            {
                if (LateBoundObject[nameof(HealthState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(HealthState)];
            }
        }

        public bool IsHeartbeatNull
        {
            get
            {
                if (LateBoundObject[nameof(Heartbeat)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * The current heartbeat status for the virtual system. 
         * This property may be one of the following values: 
         * "OK"; "Error"; "No Contact"; or "Lost Communication". 
         * For more information, see the documentation for the StatusDescriptions property of the Msvm_HeartbeatComponent class. 
         * This property is not valid for instances of Msvm_SummaryInformation representing a virtual system snapshot.
         */
        public ushort Heartbeat
        {
            get
            {
                if (LateBoundObject[nameof(Heartbeat)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(Heartbeat)];
            }
        }

        public string HostComputerSystemName => (string)LateBoundObject[nameof(HostComputerSystemName)];

        public bool IsHypervisorPartitionIdNull
        {
            get
            {
                if (LateBoundObject[nameof(HypervisorPartitionId)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * The unique identifier of the hypervisor partition used by the virtual system.
         */
        public ulong HypervisorPartitionId
        {
            get
            {
                if (LateBoundObject[nameof(HypervisorPartitionId)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(HypervisorPartitionId)];
            }
        }

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        public bool IsIntegrationServicesVersionStateNull
        {
            get
            {
                if (LateBoundObject[nameof(IntegrationServicesVersionState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * Whether or not the integration services installed in the virtual machine are up to date.
         */
        public IntegrationServicesVersionStateValues IntegrationServicesVersionState
        {
            get
            {
                if (LateBoundObject[nameof(IntegrationServicesVersionState)] == null)
                {
                    return (IntegrationServicesVersionStateValues)Convert.ToInt32(3);
                }
                return (IntegrationServicesVersionStateValues)Convert.ToInt32(LateBoundObject[nameof(IntegrationServicesVersionState)]);
            }
        }

        public bool IsMemoryAvailableNull
        {
            get
            {
                if (LateBoundObject[nameof(MemoryAvailable)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * The memory available percentage in the virtual system.
         */
        public int MemoryAvailable
        {
            get
            {
                if (LateBoundObject[nameof(MemoryAvailable)] == null)
                {
                    return Convert.ToInt32(0);
                }
                return (int)LateBoundObject[nameof(MemoryAvailable)];
            }
        }

        public bool IsMemorySpansPhysicalNumaNodesNull
        {
            get
            {
                if (LateBoundObject[nameof(MemorySpansPhysicalNumaNodes)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * Indicates whether or not the memory of the one or more of the virtual non-uniform
         * memory access (NUMA) nodes of the virtual machine spans multiple physical NUMA nodes of the hosting computer system.
         */
        public bool MemorySpansPhysicalNumaNodes
        {
            get
            {
                if (LateBoundObject[nameof(MemorySpansPhysicalNumaNodes)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(MemorySpansPhysicalNumaNodes)];
            }
        }

        public bool IsMemoryUsageNull
        {
            get
            {
                if (LateBoundObject[nameof(MemoryUsage)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * The current memory usage of the virtual system. This property is not valid for instances
         * of Msvm_SummaryInformation representing a virtual system snapshot.
         */
        public ulong MemoryUsage
        {
            get
            {
                if (LateBoundObject[nameof(MemoryUsage)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(MemoryUsage)];
            }
        }

        public string Name => (string)LateBoundObject[nameof(Name)];

        public string Notes => (string)LateBoundObject[nameof(Notes)];

        public bool IsNumberOfProcessorsNull
        {
            get
            {
                if (LateBoundObject[nameof(NumberOfProcessors)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ushort NumberOfProcessors
        {
            get
            {
                if (LateBoundObject[nameof(NumberOfProcessors)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(NumberOfProcessors)];
            }
        }

        public ushort[] OperationalStatus => (ushort[])LateBoundObject[nameof(OperationalStatus)];

        public string OtherEnabledState => (string)LateBoundObject[nameof(OtherEnabledState)];

        public bool IsProcessorLoadNull
        {
            get
            {
                if (LateBoundObject[nameof(ProcessorLoad)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * The current processor usage of the virtual system. This property is not valid for
         * instances of Msvm_SummaryInformation representing a virtual system snapshot.
         */
        public ushort ProcessorLoad
        {
            get
            {
                if (LateBoundObject[nameof(ProcessorLoad)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ProcessorLoad)];
            }
        }

        /*
         * An array of the previous 100 samples of the processor usage for the virtual system.
         * This property is not valid for instances of Msvm_SummaryInformation representing a virtual system snapshot. 
         */
        public ushort[] ProcessorLoadHistory => (ushort[])LateBoundObject[nameof(ProcessorLoadHistory)];

        public bool IsReplicationHealthNull
        {
            get
            {
                if (LateBoundObject[nameof(ReplicationHealth)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * Replication health for the virtual machine.
         */
        public ReplicationHealthValues ReplicationHealth
        {
            get
            {
                if (LateBoundObject[nameof(ReplicationHealth)] == null)
                {
                    return (ReplicationHealthValues)Convert.ToInt32(4);
                }
                return (ReplicationHealthValues)Convert.ToInt32(LateBoundObject[nameof(ReplicationHealth)]);
            }
        }

        /*
         * The array of Replication health values for the various replication relationships of the virtual machine.
         */
        public ReplicationHealthExValues[] ReplicationHealthEx
        {
            get
            {
                Array arrEnumVals = (Array)LateBoundObject[nameof(ReplicationHealthEx)];
                ReplicationHealthExValues[] enumToRet = new ReplicationHealthExValues[arrEnumVals.Length];
                int counter;
                for (counter = 0; counter < arrEnumVals.Length; counter = counter + 1)
                {
                    enumToRet[counter] = (ReplicationHealthExValues)Convert.ToInt32(arrEnumVals.GetValue(counter));
                }
                return enumToRet;
            }
        }

        public bool IsReplicationModeNull
        {
            get
            {
                if (LateBoundObject[nameof(ReplicationMode)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * "Identifies replication type for the virtual machine.
         */
        public ReplicationModeValues ReplicationMode
        {
            get
            {
                if (LateBoundObject[nameof(ReplicationMode)] == null)
                {
                    return (ReplicationModeValues)Convert.ToInt32(5);
                }
                return (ReplicationModeValues)Convert.ToInt32(LateBoundObject[nameof(ReplicationMode)]);
            }
        }

        /*
         * Globally unique identifier of provider that identifies the endpoint provider. 
         * For Hyper-V host to another Hyper-V host, provider id is fixed as 22391CDC-272C-4DDF-BA88-9BEFB1A0975C.
         * In case of external provider this is CLSID of provider COM class object.
         */
        public string[] ReplicationProviderId => (string[])LateBoundObject[nameof(ReplicationProviderId)];

        public bool IsReplicationStateNull
        {
            get
            {
                if (LateBoundObject[nameof(ReplicationState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * Replication state for the virtual machine.
         */
        public ReplicationStateValues ReplicationState
        {
            get
            {
                if (LateBoundObject[nameof(ReplicationState)] == null)
                {
                    return (ReplicationStateValues)Convert.ToInt32(13);
                }
                return (ReplicationStateValues)Convert.ToInt32(LateBoundObject[nameof(ReplicationState)]);
            }
        }

        /*
         * The array of Replication state values for the various replication relationships of the virtual machine.
         */
        public ReplicationStateExValues[] ReplicationStateEx
        {
            get
            {
                Array arrEnumVals = (Array)LateBoundObject[nameof(ReplicationStateEx)];
                ReplicationStateExValues[] enumToRet = new ReplicationStateExValues[arrEnumVals.Length];
                int counter;
                for (counter = 0; counter < arrEnumVals.Length; counter = counter + 1)
                {
                    enumToRet[counter] = (ReplicationStateExValues)Convert.ToInt32(arrEnumVals.GetValue(counter));
                }
                return enumToRet;
            }
        }

        public bool IsShieldedNull
        {
            get
            {
                if (LateBoundObject[nameof(Shielded)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * Indicates whether or not shielding is configured for the virtual machine.
         */
        public bool Shielded
        {
            get
            {
                if (LateBoundObject[nameof(Shielded)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(Shielded)];
            }
        }

        /*
         * An array of Msvm_VirtualSystemSettingData instances representing the snapshots for the virtual system.
         * This property is not valid for instances of Msvm_SummaryInformation representing a virtual system snapshot.
         */
        public ManagementBaseObject[] Snapshots => (ManagementBaseObject[])LateBoundObject[nameof(Snapshots)];

        public string[] StatusDescriptions => (string[])LateBoundObject[nameof(StatusDescriptions)];

        public bool IsSwapFilesInUseNull
        {
            get
            {
                if (LateBoundObject[nameof(SwapFilesInUse)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * Indicates if Smart Paging is active.
         */
        public bool SwapFilesInUse
        {
            get
            {
                if (LateBoundObject[nameof(SwapFilesInUse)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(SwapFilesInUse)];
            }
        }

        /*
         * Reference to the CIM_ComputerSystem instance representing the test replica virtual
         * system for the virtual machine. This property is not valid for instances of Msvm_SummaryInformation
         * representing a virtual system snapshot.
         */
        public ManagementPath TestReplicaSystem
        {
            get
            {
                if (LateBoundObject[nameof(TestReplicaSystem)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(TestReplicaSystem)].ToString());
                }
                return null;
            }
        }

        /*
         * An array containing a small, thumbnail-sized image of the desktop for the virtual system or snapshot in RGB565 format.
         */
        public byte[] ThumbnailImage => (byte[])LateBoundObject[nameof(ThumbnailImage)];

        public bool IsThumbnailImageHeightNull
        {
            get
            {
                if (LateBoundObject[nameof(ThumbnailImageHeight)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * The height in pixels of the image in the ThumbnailImage property.
         */
        public ushort ThumbnailImageHeight
        {
            get
            {
                if (LateBoundObject[nameof(ThumbnailImageHeight)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ThumbnailImageHeight)];
            }
        }

        public bool IsThumbnailImageWidthNull
        {
            get
            {
                if (LateBoundObject[nameof(ThumbnailImageWidth)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * The width in pixels of the image in the ThumbnailImage property.
         */
        public ushort ThumbnailImageWidth
        {
            get
            {
                if (LateBoundObject[nameof(ThumbnailImageWidth)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ThumbnailImageWidth)];
            }
        }

        public bool IsUpTimeNull
        {
            get
            {
                if (LateBoundObject[nameof(UpTime)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ulong UpTime
        {
            get
            {
                if (LateBoundObject[nameof(UpTime)] == null)
                {
                    return Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(UpTime)];
            }
        }

        public string Version => (string)LateBoundObject[nameof(Version)];

        public string[] VirtualSwitchNames => (string[])LateBoundObject[nameof(VirtualSwitchNames)];

        public string VirtualSystemSubType => (string)LateBoundObject[nameof(VirtualSystemSubType)];

        private bool CheckIfProperClass(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions OptionsParam)
        {
            if ((path != null) && (string.Compare(path.ClassName, ManagementClassName, true, CultureInfo.InvariantCulture) == 0))
            {
                return true;
            }
            else
            {
                using (ManagementObject theObj = new ManagementObject(mgmtScope, path, OptionsParam))
                {
                    return CheckIfProperClass(theObj);
                }
            }
        }

        private bool CheckIfProperClass(ManagementBaseObject theObj)
        {
            if ((theObj != null)
                        && (string.Compare((string)theObj["__CLASS"], ManagementClassName, true, CultureInfo.InvariantCulture) == 0))
            {
                return true;
            }
            else
            {
                Array parentClasses = (Array)theObj["__DERIVATION"];
                if (parentClasses != null)
                {
                    int count;
                    for (count = 0; count < parentClasses.Length; count = count + 1)
                    {
                        if (string.Compare((string)parentClasses.GetValue(count), ManagementClassName, true, CultureInfo.InvariantCulture) == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool ShouldSerializeApplicationHealth()
        {
            if (IsApplicationHealthNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeAvailableMemoryBuffer()
        {
            if (IsAvailableMemoryBufferNull == false)
            {
                return true;
            }
            return false;
        }

        // Converts a given datetime in DMTF format to System.DateTime object.
        static DateTime ToDateTime(string dmtfDate)
        {
            DateTime initializer = DateTime.MinValue;
            int year = initializer.Year;
            int month = initializer.Month;
            int day = initializer.Day;
            int hour = initializer.Hour;
            int minute = initializer.Minute;
            int second = initializer.Second;
            long ticks = 0;
            string dmtf = dmtfDate;
            if (dmtf == null)
            {
                throw new ArgumentOutOfRangeException(dmtf);
            }
            if (dmtf.Length == 0)
            {
                throw new ArgumentOutOfRangeException(dmtf);
            }
            if (dmtf.Length != 25)
            {
                throw new ArgumentOutOfRangeException(dmtf);
            }

            string tempString;
            try
            {
                tempString = dmtf.Substring(0, 4);
                if ("****" != tempString)
                {
                    year = int.Parse(tempString);
                }
                tempString = dmtf.Substring(4, 2);
                if ("**" != tempString)
                {
                    month = int.Parse(tempString);
                }
                tempString = dmtf.Substring(6, 2);
                if ("**" != tempString)
                {
                    day = int.Parse(tempString);
                }
                tempString = dmtf.Substring(8, 2);
                if ("**" != tempString)
                {
                    hour = int.Parse(tempString);
                }
                tempString = dmtf.Substring(10, 2);
                if ("**" != tempString)
                {
                    minute = int.Parse(tempString);
                }
                tempString = dmtf.Substring(12, 2);
                if ("**" != tempString)
                {
                    second = int.Parse(tempString);
                }
                tempString = dmtf.Substring(15, 6);
                if ("******" != tempString)
                {
                    ticks = long.Parse(tempString) * (TimeSpan.TicksPerMillisecond / 1000);
                }
                if ((year < 0)
                            || (month < 0)
                            || (day < 0)
                            || (hour < 0)
                            || (minute < 0)
                            || (minute < 0)
                            || (second < 0)
                            || (ticks < 0))
                {
                    throw new ArgumentOutOfRangeException(year.ToString());
                }
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException(null, e.Message);
            }
            DateTime datetime = new DateTime(year, month, day, hour, minute, second, 0);
            datetime = datetime.AddTicks(ticks);
            TimeSpan tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(datetime);
            long OffsetMins = tickOffset.Ticks / TimeSpan.TicksPerMinute;
            tempString = dmtf.Substring(22, 3);
            if (tempString != "******")
            {
                tempString = dmtf.Substring(21, 4);
                int UTCOffset;
                try
                {
                    UTCOffset = int.Parse(tempString);
                }
                catch (Exception e)
                {
                    throw new ArgumentOutOfRangeException(null, e.Message);
                }

                int OffsetToBeAdjusted = (int)(OffsetMins - UTCOffset);
                datetime = datetime.AddMinutes(OffsetToBeAdjusted);
            }
            return datetime;
        }

        // Converts a given System.DateTime object to DMTF datetime format.
        static string ToDmtfDateTime(DateTime date)
        {
            TimeSpan tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(date);
            long OffsetMins = tickOffset.Ticks / TimeSpan.TicksPerMinute;
            string utcString;
            if (Math.Abs(OffsetMins) > 999)
            {
                date = date.ToUniversalTime();
                utcString = "+000";
            }
            else
            {
                if (tickOffset.Ticks >= 0)
                {
                    utcString = string.Concat("+", (tickOffset.Ticks / TimeSpan.TicksPerMinute).ToString().PadLeft(3, '0'));
                }
                else
                {
                    string strTemp = OffsetMins.ToString();
                    utcString = string.Concat("-", strTemp.Substring(1, strTemp.Length - 1).PadLeft(3, '0'));
                }
            }
            string dmtfDateTime = date.Year.ToString().PadLeft(4, '0');
            dmtfDateTime = string.Concat(dmtfDateTime, date.Month.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Day.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Hour.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Minute.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Second.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ".");
            DateTime dtTemp = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
            long microsec = (date.Ticks - dtTemp.Ticks)
                        * 1000
                        / TimeSpan.TicksPerMillisecond;
            string strMicrosec = microsec.ToString();
            if (strMicrosec.Length > 6)
            {
                strMicrosec = strMicrosec.Substring(0, 6);
            }
            dmtfDateTime = string.Concat(dmtfDateTime, strMicrosec.PadLeft(6, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, utcString);
            return dmtfDateTime;
        }

        private bool ShouldSerializeCreationTime()
        {
            if (IsCreationTimeNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeEnabledState()
        {
            if (IsEnabledStateNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeEnhancedSessionModeState()
        {
            if (IsEnhancedSessionModeStateNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeHealthState()
        {
            if (IsHealthStateNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeHeartbeat()
        {
            if (IsHeartbeatNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeHypervisorPartitionId()
        {
            if (IsHypervisorPartitionIdNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeIntegrationServicesVersionState()
        {
            if (IsIntegrationServicesVersionStateNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeMemoryAvailable()
        {
            if (IsMemoryAvailableNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeMemorySpansPhysicalNumaNodes()
        {
            if (IsMemorySpansPhysicalNumaNodesNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeMemoryUsage()
        {
            if (IsMemoryUsageNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeNumberOfProcessors()
        {
            if (IsNumberOfProcessorsNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeProcessorLoad()
        {
            if (IsProcessorLoadNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeReplicationHealth()
        {
            if (IsReplicationHealthNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeReplicationMode()
        {
            if (IsReplicationModeNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeReplicationState()
        {
            if (IsReplicationStateNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeShielded()
        {
            if (IsShieldedNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeSwapFilesInUse()
        {
            if (IsSwapFilesInUseNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeThumbnailImageHeight()
        {
            if (IsThumbnailImageHeightNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeThumbnailImageWidth()
        {
            if (IsThumbnailImageWidthNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeUpTime()
        {
            if (IsUpTimeNull == false)
            {
                return true;
            }
            return false;
        }

        public void CommitObject()
        {
            if (isEmbedded == false)
            {
                PrivateLateBoundObject.Put();
            }
        }

        public void CommitObject(PutOptions putOptions)
        {
            if (isEmbedded == false)
            {
                PrivateLateBoundObject.Put(putOptions);
            }
        }

        private void Initialize()
        {
            AutoCommit = true;
            isEmbedded = false;
        }

        private static string ConstructPath(string keyInstanceID)
        {
            string strPath = "root\\virtualization\\v2:Msvm_SummaryInformation";
            strPath = string.Concat(strPath, string.Concat(".InstanceID=", string.Concat("\"", string.Concat(keyInstanceID, "\""))));
            return strPath;
        }

        private void InitializeObject(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions)
        {
            Initialize();
            if (path != null)
            {
                if (CheckIfProperClass(mgmtScope, path, getOptions) != true)
                {
                    throw new ArgumentException("Class name does not match.");
                }
            }
            PrivateLateBoundObject = new ManagementObject(mgmtScope, path, getOptions);
            SystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            LateBoundObject = PrivateLateBoundObject;
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static MsvmCollection<SummaryInformation> GetInstances() => GetInstances(null, null, null);

        public static MsvmCollection<SummaryInformation> GetInstances(string condition) => GetInstances(null, condition, null);

        public static MsvmCollection<SummaryInformation> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties);

        public static MsvmCollection<SummaryInformation> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties);

        public static MsvmCollection<SummaryInformation> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions)
        {
            if (mgmtScope == null)
            {
                if (StaticScope == null)
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\virtualization\\v2";
                }
                else
                {
                    mgmtScope = StaticScope;
                }
            }
            ManagementPath pathObj = new ManagementPath();
            pathObj.ClassName = "Msvm_SummaryInformation";
            pathObj.NamespacePath = "root\\virtualization\\v2";
            using (ManagementClass clsObject = new ManagementClass(mgmtScope, pathObj, null))
            {
                if (enumOptions == null)
                {
                    enumOptions = new EnumerationOptions
                    {
                        EnsureLocatable = true
                    };
                }
                return new MsvmCollection<SummaryInformation>(clsObject.GetInstances(enumOptions));
            }
        }

        public static MsvmCollection<SummaryInformation> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null);

        public static MsvmCollection<SummaryInformation> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties);

        public static MsvmCollection<SummaryInformation> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties)
        {
            if (mgmtScope == null)
            {
                if (StaticScope == null)
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\virtualization\\v2";
                }
                else
                {
                    mgmtScope = StaticScope;
                }
            }
            using (ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery("Msvm_SummaryInformation", condition, selectedProperties)))
            {
                EnumerationOptions enumOptions = new EnumerationOptions
                {
                    EnsureLocatable = true
                };
                ObjectSearcher.Options = enumOptions;
                return new MsvmCollection<SummaryInformation>(ObjectSearcher.Get());
            }
        }

        public static SummaryInformation CreateInstance()
        {
            ManagementScope mgmtScope;
            if (StaticScope == null)
            {
                mgmtScope = new ManagementScope();
                mgmtScope.Path.NamespacePath = CreatedWmiNamespace;
            }
            else
            {
                mgmtScope = StaticScope;
            }
            ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
            using (ManagementClass tmpMgmtClass = new ManagementClass(mgmtScope, mgmtPath, null))
            {
                return new SummaryInformation(tmpMgmtClass.CreateInstance());
            }
        }

        public void Delete()
        {
            PrivateLateBoundObject.Delete();
        }

        public enum ApplicationHealthValues
        {
            OK = 2,
            Application_Critical = 32782,
            Disabled = 32896,
            NULL_ENUM_VALUE = 0,
        }

        public enum IntegrationServicesVersionStateValues
        {
            Unknown0 = 0,
            UpToDate = 1,
            Mismatch = 2,
            NULL_ENUM_VALUE = 3,
        }

        public enum ReplicationHealthValues
        {

            Not_applicable = 0,

            Ok = 1,

            Warning = 2,

            Critical = 3,

            NULL_ENUM_VALUE = 4,
        }

        public enum ReplicationHealthExValues
        {
            Not_applicable = 0,
            Ok = 1,
            Warning = 2,
            Critical = 3,
            NULL_ENUM_VALUE = 4,
        }

        public enum ReplicationModeValues
        {
            None = 0,
            Primary = 1,
            Replica = 2,
            Test_Replica = 3,
            Extended_Replica = 4,
            NULL_ENUM_VALUE = 5,
        }

        public enum ReplicationStateValues
        {
            Disabled = 0,
            Ready_for_replication = 1,
            Waiting_to_complete_initial_replication = 2,
            Replicating = 3,
            Synced_replication_complete = 4,
            Recovered = 5,
            Committed = 6,
            Suspended = 7,
            Critical = 8,
            Waiting_to_start_resynchronization = 9,
            Resynchronizing = 10,
            Resynchronization_suspended = 11,
            Failover_in_progress = 12,
            NULL_ENUM_VALUE = 13,
        }

        public enum ReplicationStateExValues
        {
            Disabled = 0,
            Ready_for_replication = 1,
            Waiting_to_complete_initial_replication = 2,
            Replicating = 3,
            Synced_replication_complete = 4,
            Recovered = 5,
            Committed = 6,
            Suspended = 7,
            Critical = 8,
            Waiting_to_start_resynchronization = 9,
            Resynchronizing = 10,
            Resynchronization_suspended = 11,
            Failover_in_progress = 12,
            Failback_in_progress = 13,
            Failback_complete = 14,
            Disk_update_in_progress = 15,
            Disk_update_critical = 16,
            Unknown0 = 17,
            Repurpose_replication_in_progress = 18,
            Prepared_for_sync_replication = 19,
            Prepared_for_group_reverse_replication = 20,
            Firedrill_in_progress = 21,
            NULL_ENUM_VALUE = 22,
        }
        
        public enum RequestedInformation      : uint
        {
            Name                               = 0,
            ElementName                        = 1,  
            CreationTime                       = 2,  
            Notes                              = 3,  
            NumberOfProcessors                 = 4,  
            ThumbnailImage                     = 5,  
            ThumbnailImageHeight               = 6,  
            ThumbnailImageWidth                = 7,  
            AllocatedGPU                       = 8,  
            VirtualSwitchNames                 = 9,  
            Version                            = 10,   // Added in Windows 10 and Windows Server 2016.
            Shielded                           = 11,   // Added in Windows 10, version 1703 and Windows Server 2016.
            EnabledState                       = 100,
            ProcessorLoad                      = 101,
            ProcessorLoadHistory               = 102,
            MemoryUsage                        = 103,
            Heartbeat                          = 104,
            UpTime                             = 105,
            GuestOperatingSystem               = 106,
            Snapshots                          = 107,
            AsynchronousTasks                  = 108,
            HealthState                        = 109,
            OperationalStatus                  = 110,
            StatusDescriptions                 = 111,
            MemoryAvailable                    = 112,
            AvailableMemoryBuffer              = 113,
            ReplicationMode                    = 114,
            ReplicationState                   = 115,
            ReplicationHealthTestReplicaSystem = 116,
            ApplicationHealth                  = 117,
            ReplicationStateEx                 = 118,
            ReplicationHealthEx                = 119,
            SwapFilesInUse                     = 120,
            IntegrationServicesVersionState    = 121,
            ReplicationProviderId              = 122,
            MemorySpansPhysicalNumaNodes       = 123 
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                PrivateLateBoundObject.Dispose();
            }
        }

        ~SummaryInformation()
        {
            Dispose(false);
        }
    }
}
