using System;
using System.Management;
using System.Linq;
using System.Collections.Generic;

namespace Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement
{
    public class VirtualSystemSnapshotService : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(VirtualSystemSnapshotService)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public VirtualSystemSnapshotService() : base(ClassName) { }

        public VirtualSystemSnapshotService(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualSystemSnapshotService(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualSystemSnapshotService(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public VirtualSystemSnapshotService(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public VirtualSystemSnapshotService(ManagementPath path) : base(path, ClassName) { }

        public VirtualSystemSnapshotService(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public VirtualSystemSnapshotService(ManagementObject theObject) : base(theObject, ClassName) { }

        public VirtualSystemSnapshotService(ManagementBaseObject theObject) : base(theObject, ClassName) { }

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
        public static List<VirtualSystemSnapshotService> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotService(mo)).ToList();

        public new static List<VirtualSystemSnapshotService> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotService(mo)).ToList();

        public static List<VirtualSystemSnapshotService> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotService(mo)).ToList();

        public static List<VirtualSystemSnapshotService> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotService(mo)).ToList();

        public static List<VirtualSystemSnapshotService> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotService(mo)).ToList();

        public static List<VirtualSystemSnapshotService> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotService(mo)).ToList();

        public static List<VirtualSystemSnapshotService> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotService(mo)).ToList();

        public static List<VirtualSystemSnapshotService> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemSnapshotService(mo)).ToList();

        public static VirtualSystemSnapshotService CreateInstance() => new VirtualSystemSnapshotService(CreateInstance(ClassName));

        public uint ApplySnapshot(ManagementPath Snapshot, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ApplySnapshot");
                inParams["Snapshot"] = Snapshot?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ApplySnapshot", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ClearSnapshotState(ManagementPath SnapshotSettingData, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ClearSnapshotState");
                inParams["SnapshotSettingData"] = SnapshotSettingData?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ClearSnapshotState", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ConvertToReferencePoint(ManagementPath AffectedSnapshot, string ReferencePointSettings, ref ManagementPath ResultingReferencePoint, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ConvertToReferencePoint");
                inParams["AffectedSnapshot"] = AffectedSnapshot?.Path;
                inParams["ReferencePointSettings"] = ReferencePointSettings;
                inParams["ResultingReferencePoint"] = ResultingReferencePoint?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ConvertToReferencePoint", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingReferencePoint = (ManagementPath)outParams.Properties["ResultingReferencePoint"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint CreateSnapshot(ManagementPath AffectedSystem, ref ManagementPath ResultingSnapshot, string SnapshotSettings, ushort SnapshotType, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                using (ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("CreateSnapshot"))
                {
                    inParams["AffectedSystem"] = AffectedSystem?.Path;
                    inParams["ResultingSnapshot"] = ResultingSnapshot?.Path;
                    inParams["SnapshotSettings"] = SnapshotSettings;
                    inParams["SnapshotType"] = SnapshotType;
                    using (ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("CreateSnapshot", inParams, null))
                    {
                        Job = null;
                        if (outParams.Properties["Job"] != null)
                        {
                            Job = new ManagementPath(outParams["Job"] as string);
                        }
                        ResultingSnapshot = (ManagementPath)outParams.Properties["ResultingSnapshot"].Value;
                        return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
                    }
                }
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint DestroySnapshot(ManagementPath AffectedSnapshot, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("DestroySnapshot");
                inParams["AffectedSnapshot"] = AffectedSnapshot?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DestroySnapshot", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint DestroySnapshotTree(ManagementPath SnapshotSettingData, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("DestroySnapshotTree");
                inParams["SnapshotSettingData"] = SnapshotSettingData?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DestroySnapshotTree", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
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
                    Job = new ManagementPath(outParams["Job"] as string);
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
    }
}
