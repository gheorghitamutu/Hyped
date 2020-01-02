using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Storage
{
    public class ImageManagementService : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(ImageManagementService)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public ImageManagementService() : base(ClassName) { }

        public ImageManagementService(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ImageManagementService(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public ImageManagementService(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public ImageManagementService(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public ImageManagementService(ManagementPath path) : base(path, ClassName) { }

        public ImageManagementService(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public ImageManagementService(ManagementObject theObject) : base(theObject, ClassName) { }

        public ImageManagementService(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public ushort[] AvailableRequestedStates => (ushort[])LateBoundObject[nameof(AvailableRequestedStates)];

        public string Caption => (string)LateBoundObject[nameof(Caption)];
        
        public ushort CommunicationStatus
        {
            get
            {
                if (LateBoundObject[nameof(CommunicationStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(CommunicationStatus)];
            }
        }

        public string CreationClassName => (string)LateBoundObject[nameof(CreationClassName)];

        public string Description => (string)LateBoundObject[nameof(Description)];

        public ushort DetailedStatus
        {
            get
            {
                if (LateBoundObject[nameof(DetailedStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(DetailedStatus)];
            }
        }

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        public ushort EnabledDefault
        {
            get
            {
                if (LateBoundObject[nameof(EnabledDefault)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(EnabledDefault)];
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

        public DateTime InstallDate
        {
            get
            {
                if (LateBoundObject[nameof(InstallDate)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(InstallDate)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        public string Name => (string)LateBoundObject[nameof(Name)];

        public ushort OperatingStatus
        {
            get
            {
                if (LateBoundObject[nameof(OperatingStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(OperatingStatus)];
            }
        }

        public ushort[] OperationalStatus => (ushort[])LateBoundObject[nameof(OperationalStatus)];

        public string OtherEnabledState => (string)LateBoundObject[nameof(OtherEnabledState)];

        public string PrimaryOwnerContact => (string)LateBoundObject[nameof(PrimaryOwnerContact)];

        public string PrimaryOwnerName => (string)LateBoundObject[nameof(PrimaryOwnerName)];

        public ushort PrimaryStatus
        {
            get
            {
                if (LateBoundObject[nameof(PrimaryStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(PrimaryStatus)];
            }
        }

        public ushort RequestedState
        {
            get
            {
                if (LateBoundObject[nameof(RequestedState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(RequestedState)];
            }
        }

        public bool Started
        {
            get
            {
                if (LateBoundObject[nameof(Started)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(Started)];
            }
        }

        public string StartMode => (string)LateBoundObject[nameof(StartMode)];

        public string Status => (string)LateBoundObject[nameof(Status)];

        public string[] StatusDescriptions => (string[])LateBoundObject[nameof(StatusDescriptions)];

        public string SystemCreationClassName => (string)LateBoundObject[nameof(SystemCreationClassName)];

        public string SystemName => (string)LateBoundObject[nameof(SystemName)];

        public DateTime TimeOfLastStateChange
        {
            get
            {
                if (LateBoundObject[nameof(TimeOfLastStateChange)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(TimeOfLastStateChange)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public ushort TransitioningToState
        {
            get
            {
                if (LateBoundObject[nameof(TransitioningToState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(TransitioningToState)];
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<ImageManagementService> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new ImageManagementService(mo)).ToList();

        public new static List<ImageManagementService> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ImageManagementService(mo)).ToList();

        public static List<ImageManagementService> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ImageManagementService(mo)).ToList();

        public static List<ImageManagementService> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ImageManagementService(mo)).ToList();

        public static List<ImageManagementService> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new ImageManagementService(mo)).ToList();

        public static List<ImageManagementService> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new ImageManagementService(mo)).ToList();

        public static List<ImageManagementService> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ImageManagementService(mo)).ToList();

        public static List<ImageManagementService> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new ImageManagementService(mo)).ToList();

        public static ImageManagementService CreateInstance() => new ImageManagementService(CreateInstance(ClassName));

        public uint AttachVirtualHardDisk(bool AssignDriveLetter, string Path, bool ReadOnly, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("AttachVirtualHardDisk");
                inParams["AssignDriveLetter"] = AssignDriveLetter;
                inParams["Path"] = Path;
                inParams["ReadOnly"] = ReadOnly;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("AttachVirtualHardDisk", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint CompactVirtualHardDisk(ushort Mode, string Path, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("CompactVirtualHardDisk");
                inParams["Mode"] = Mode;
                inParams["Path"] = Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("CompactVirtualHardDisk", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ConvertVirtualHardDisk(string SourcePath, string VirtualDiskSettingData, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ConvertVirtualHardDisk");
                inParams["SourcePath"] = SourcePath;
                inParams["VirtualDiskSettingData"] = VirtualDiskSettingData;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ConvertVirtualHardDisk", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ConvertVirtualHardDiskToVHDSet(string VirtualHardDiskPath, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ConvertVirtualHardDiskToVHDSet");
                inParams["VirtualHardDiskPath"] = VirtualHardDiskPath;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ConvertVirtualHardDiskToVHDSet", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint CreateVirtualFloppyDisk(string Path, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("CreateVirtualFloppyDisk");
                inParams["Path"] = Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("CreateVirtualFloppyDisk", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint CreateVirtualHardDisk(string VirtualDiskSettingData, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("CreateVirtualHardDisk");
                inParams["VirtualDiskSettingData"] = VirtualDiskSettingData;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("CreateVirtualHardDisk", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint DeleteVHDSnapshot(bool PersistReferenceSnapshot, string SnapshotId, string VHDSetPath, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("DeleteVHDSnapshot");
                inParams["PersistReferenceSnapshot"] = PersistReferenceSnapshot;
                inParams["SnapshotId"] = SnapshotId;
                inParams["VHDSetPath"] = VHDSetPath;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DeleteVHDSnapshot", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint FindMountedStorageImageInstance(ushort CriterionType, string SelectionCriterion, out ManagementPath Image)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("FindMountedStorageImageInstance");
                inParams["CriterionType"] = CriterionType;
                inParams["SelectionCriterion"] = SelectionCriterion;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("FindMountedStorageImageInstance", inParams, null);
                Image = null;
                if (outParams.Properties["Image"] != null)
                {
                    Image = new ManagementPath(outParams.Properties["Image"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Image = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetVHDSetInformation(uint[] AdditionalInformation, string VHDSetPath, out string Information, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("GetVHDSetInformation");
                inParams["AdditionalInformation"] = AdditionalInformation;
                inParams["VHDSetPath"] = VHDSetPath;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetVHDSetInformation", inParams, null);
                Information = Convert.ToString(outParams.Properties["Information"].Value);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Information = null;
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetVHDSnapshotInformation(uint[] AdditionalInformation, string[] SnapshotIds, string VHDSetPath, out ManagementPath Job, out string[] SnapshotInformation)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("GetVHDSnapshotInformation");
                inParams["AdditionalInformation"] = AdditionalInformation;
                inParams["SnapshotIds"] = SnapshotIds;
                inParams["VHDSetPath"] = VHDSetPath;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetVHDSnapshotInformation", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                SnapshotInformation = (string[])outParams.Properties["SnapshotInformation"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                SnapshotInformation = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetVirtualDiskChanges(ulong ByteLength, ulong ByteOffset, string LimitId, string Path, string TargetSnapshotId, out ulong[] ChangedByteLengths, out ulong[] ChangedByteOffsets, out ManagementPath Job, out ulong ProcessedByteLength)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("GetVirtualDiskChanges");
                inParams["ByteLength"] = ByteLength;
                inParams["ByteOffset"] = ByteOffset;
                inParams["LimitId"] = LimitId;
                inParams["Path"] = Path;
                inParams["TargetSnapshotId"] = TargetSnapshotId;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetVirtualDiskChanges", inParams, null);
                ChangedByteLengths = (ulong[])outParams.Properties["ChangedByteLengths"].Value;
                ChangedByteOffsets = (ulong[])outParams.Properties["ChangedByteOffsets"].Value;
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                ProcessedByteLength = Convert.ToUInt64(outParams.Properties["ProcessedByteLength"].Value);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ChangedByteLengths = null;
                ChangedByteOffsets = null;
                Job = null;
                ProcessedByteLength = Convert.ToUInt64(0);
                return Convert.ToUInt32(0);
            }
        }

        public uint GetVirtualHardDiskSettingData(string Path, out ManagementPath Job, out string SettingData)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("GetVirtualHardDiskSettingData");
                inParams["Path"] = Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetVirtualHardDiskSettingData", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                SettingData = Convert.ToString(outParams.Properties["SettingData"].Value);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                SettingData = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetVirtualHardDiskState(string Path, out ManagementPath Job, out string State)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("GetVirtualHardDiskState");
                inParams["Path"] = Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetVirtualHardDiskState", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                State = Convert.ToString(outParams.Properties["State"].Value);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                State = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint MergeVirtualHardDisk(string DestinationPath, string SourcePath, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("MergeVirtualHardDisk");
                inParams["DestinationPath"] = DestinationPath;
                inParams["SourcePath"] = SourcePath;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("MergeVirtualHardDisk", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint OptimizeVHDSet(string VHDSetPath, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("OptimizeVHDSet");
                inParams["VHDSetPath"] = VHDSetPath;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("OptimizeVHDSet", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RequestStateChange(ushort RequestedState, DateTime TimeoutPeriod, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RequestStateChange");
                inParams[nameof(RequestedState)] = RequestedState;
                inParams["TimeoutPeriod"] = ToDmtfDateTime(TimeoutPeriod);
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RequestStateChange", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ResizeVirtualHardDisk(ulong MaxInternalSize, string Path, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ResizeVirtualHardDisk");
                inParams["MaxInternalSize"] = MaxInternalSize;
                inParams["Path"] = Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ResizeVirtualHardDisk", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint SetParentVirtualHardDisk(string ChildPath, bool IgnoreIDMismatch, string LeafPath, string ParentPath, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("SetParentVirtualHardDisk");
                inParams["ChildPath"] = ChildPath;
                inParams["IgnoreIDMismatch"] = IgnoreIDMismatch;
                inParams["LeafPath"] = LeafPath;
                inParams["ParentPath"] = ParentPath;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetParentVirtualHardDisk", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint SetVHDSnapshotInformation(string Information, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("SetVHDSnapshotInformation");
                inParams["Information"] = Information;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetVHDSnapshotInformation", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint SetVirtualHardDiskSettingData(string VirtualDiskSettingData, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("SetVirtualHardDiskSettingData");
                inParams["VirtualDiskSettingData"] = VirtualDiskSettingData;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetVirtualHardDiskSettingData", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint StartService()
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("StartService", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint StopService()
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("StopService", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint ValidatePersistentReservationSupport(string Path, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ValidatePersistentReservationSupport");
                inParams["Path"] = Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ValidatePersistentReservationSupport", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ValidateVirtualHardDisk(string Path, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ValidateVirtualHardDisk");
                inParams["Path"] = Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ValidateVirtualHardDisk", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams.Properties["Job"].Value as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public void CreateVirtualHardDisk(object virtualDiskSettingData, out ManagementPath job)
        {
            throw new NotImplementedException();
        }
    }
}
