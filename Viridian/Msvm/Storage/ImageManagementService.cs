using System;
using System.Management;
using Viridian.Job;
using Viridian.Scopes;

namespace Viridian.Msvm.Storage
{
    public sealed class ImageManagementService : BaseService
    {
        private static ImageManagementService instance = null;

        public enum VirtualHardDiskType : ushort
        {
            Fixed = 2,
            Dynamic = 3,
            PhysicalDrive = 5
        }

        public enum VirtualDiskFormat
        {
            VHD = 2,
            VHDX = 3,
            VHDSet = 4
        }

        public enum CompactMode : ushort
        {
            Full = 0,
            Quick = 1,
            Retrim = 2,
            Pretrimmed = 3,
            Prezeroed = 4
        }

        public enum CriterionType : ushort
        {
            Unknown = 0,
            Path = 2
        }

        public enum State : ushort
        {
            Enabled = 2,
            Disabled = 3,
            ShutDown = 4,
            Offline = 6,
            Test = 7,
            Defer = 8,
            Quiesce = 9,
            Reboot = 10,
            Reset = 11
        }

        private ImageManagementService() : base("Msvm_ImageManagementService") {}

        public static ImageManagementService Instance
        {
            get
            {
                if (instance == null)
                    instance = new ImageManagementService();

                return instance;
            }
        }

#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
#pragma warning disable CA1303 // Do not pass literals as localized parameters
        private ManagementObject Msvm_ImageManagementService => Service ?? throw new NullReferenceException(nameof(Service));
#pragma warning restore CA1303 // Do not pass literals as localized parameters
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
#pragma warning restore CA1707 // Identifiers should not contain underscores

        public void AttachVirtualHardDisk(string Path, bool AssignDriveLetter, bool ReadOnly)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(AttachVirtualHardDisk)))
            {
                ip[nameof(Path)] = Path ?? throw new ArgumentNullException(nameof(Path));
                ip[nameof(AssignDriveLetter)] = AssignDriveLetter;
                ip[nameof(ReadOnly)] = ReadOnly;

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(AttachVirtualHardDisk), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void CompactVirtualHardDisk(string Path, CompactMode Mode)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(CompactVirtualHardDisk)))
            {
                ip[nameof(Path)] = Path ?? throw new ArgumentNullException(nameof(Path));
                ip[nameof(Mode)] = Mode;

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(CompactVirtualHardDisk), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void ConvertVirtualHardDisk(string SourcePath, string VirtualDiskSettingData)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(ConvertVirtualHardDisk)))
            {
                ip[nameof(SourcePath)] = SourcePath ?? throw new ArgumentNullException(nameof(SourcePath));
                ip[nameof(VirtualDiskSettingData)] = VirtualDiskSettingData ?? throw new ArgumentNullException(nameof(VirtualDiskSettingData));

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(ConvertVirtualHardDisk), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void CreateVirtualHardDisk(string VirtualDiskSettingData)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(CreateVirtualHardDisk)))
            {
                ip[nameof(VirtualDiskSettingData)] = VirtualDiskSettingData ?? throw new ArgumentNullException(nameof(VirtualDiskSettingData));

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(CreateVirtualHardDisk), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void CreateVirtualFloppyDisk(string Path)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(CreateVirtualFloppyDisk)))
            {
                ip[nameof(Path)] = Path ?? throw new ArgumentNullException(nameof(Path));

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(CreateVirtualFloppyDisk), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void ConvertVirtualHardDiskToVHDSet(string VirtualHardDiskPath)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(ConvertVirtualHardDiskToVHDSet)))
            {
                ip[nameof(VirtualHardDiskPath)] = VirtualHardDiskPath ?? throw new ArgumentNullException(nameof(VirtualHardDiskPath));

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(ConvertVirtualHardDiskToVHDSet), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void DeleteVHDSnapshot(string VHDSetPath, string SnapshotId, bool PersistReferenceSnapshot)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(DeleteVHDSnapshot)))
            {
                ip[nameof(VHDSetPath)] = VHDSetPath ?? throw new ArgumentNullException(nameof(VHDSetPath));
                ip[nameof(SnapshotId)] = SnapshotId ?? throw new ArgumentNullException(nameof(SnapshotId));
                ip[nameof(PersistReferenceSnapshot)] = PersistReferenceSnapshot;

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(DeleteVHDSnapshot), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public ManagementObject FindMountedStorageImageInstance(string SelectionCriterion, CriterionType CriterionType)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(FindMountedStorageImageInstance)))
            {
                ip[nameof(SelectionCriterion)] = SelectionCriterion ?? throw new ArgumentNullException(nameof(SelectionCriterion));
                ip[nameof(CriterionType)] = CriterionType;

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(FindMountedStorageImageInstance), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return new ManagementObject(op["Image"] as string);
                }
            }
        }

        public object GetVirtualDiskChanges(string Path, string LimitId, string TargetSnapshotId, ulong ByteOffset, ulong ByteLength)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(GetVirtualDiskChanges)))
            {
                ip[nameof(Path)] = Path ?? throw new ArgumentNullException(nameof(Path));
                ip[nameof(LimitId)] = LimitId ?? throw new ArgumentNullException(nameof(LimitId));
                ip[nameof(TargetSnapshotId)] = TargetSnapshotId ?? throw new ArgumentNullException(nameof(TargetSnapshotId));
                ip[nameof(ByteOffset)] = ByteOffset;
                ip[nameof(ByteLength)] = ByteLength;

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(GetVirtualDiskChanges), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return new object[] { (ulong)op["ProcessedByteLength"], (ulong[])op["ChangedByteOffsets"], (ulong[])op["ChangedByteLengths"] };
                }
            }
        }

        public string GetVirtualHardDiskSettingData(string Path)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(GetVirtualHardDiskSettingData)))
            {
                ip[nameof(Path)] = Path ?? throw new ArgumentNullException(nameof(Path));

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(GetVirtualHardDiskSettingData), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["SettingData"].ToString();
                }
            }
        }

        public string GetVirtualHardDiskState(string Path)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(GetVirtualHardDiskState)))
            {
                ip[nameof(Path)] = Path ?? throw new ArgumentNullException(nameof(Path));

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(GetVirtualHardDiskState), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["State"].ToString();
                }
            }
        }

        public string GetVHDSetInformation(string VHDSetPath, uint[] AdditionalInformation)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(GetVHDSetInformation)))
            {
                ip[nameof(VHDSetPath)] = VHDSetPath ?? throw new ArgumentNullException(nameof(VHDSetPath));
                ip[nameof(VHDSetPath)] = AdditionalInformation ?? throw new ArgumentNullException(nameof(AdditionalInformation));

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(GetVHDSetInformation), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["Information"].ToString();
                }
            }
        }

        public string[] GetVHDSnapshotInformation(string VHDSetPath, string[] SnapshotIds, uint[] AdditionalInformation)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(GetVHDSnapshotInformation)))
            {
                ip[nameof(VHDSetPath)] = VHDSetPath ?? throw new ArgumentNullException(nameof(VHDSetPath));
                ip[nameof(SnapshotIds)] = SnapshotIds ?? throw new ArgumentNullException(nameof(SnapshotIds));
                ip[nameof(VHDSetPath)] = AdditionalInformation ?? throw new ArgumentNullException(nameof(AdditionalInformation));

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(GetVHDSnapshotInformation), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["SnapshotInformation"] as string[];
                }
            }
        }

        public void MergeVirtualHardDisk(string SourcePath, string DestinationPath)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(MergeVirtualHardDisk)))
            {
                ip[nameof(SourcePath)] = SourcePath ?? throw new ArgumentNullException(nameof(SourcePath));
                ip[nameof(DestinationPath)] = DestinationPath ?? throw new ArgumentNullException(nameof(DestinationPath));

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(MergeVirtualHardDisk), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void OptimizeVHDSet(string VHDSetPath)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(OptimizeVHDSet)))
            {
                ip[nameof(VHDSetPath)] = VHDSetPath ?? throw new ArgumentNullException(nameof(VHDSetPath));

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(OptimizeVHDSet), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void ResizeVirtualHardDisk(string Path, ulong MaxInternalSize)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(ResizeVirtualHardDisk)))
            {
                ip[nameof(Path)] = Path ?? throw new ArgumentNullException(nameof(Path));
                ip[nameof(MaxInternalSize)] = MaxInternalSize;

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(ResizeVirtualHardDisk), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void RequestStateChange(State RequestedState, DateTime TimeoutPeriod)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(RequestStateChange)))
            {
                ip[nameof(RequestedState)] = RequestedState;
                ip[nameof(TimeoutPeriod)] = TimeoutPeriod;

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(RequestStateChange), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void SetParentVirtualHardDisk(string ChildPath, string ParentPath, string LeafPath, bool IgnoreIDMismatch)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(SetParentVirtualHardDisk)))
            {
                ip[nameof(ChildPath)] = ChildPath ?? throw new ArgumentNullException(nameof(ChildPath));
                ip[nameof(ParentPath)] = ParentPath ?? throw new ArgumentNullException(nameof(ParentPath));
                ip[nameof(LeafPath)] = LeafPath ?? throw new ArgumentNullException(nameof(LeafPath));
                ip[nameof(IgnoreIDMismatch)] = IgnoreIDMismatch;

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(SetParentVirtualHardDisk), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void SetVirtualHardDiskSettingData(string VirtualDiskSettingData)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(SetVirtualHardDiskSettingData)))
            {
                ip[nameof(VirtualDiskSettingData)] = VirtualDiskSettingData ?? throw new ArgumentNullException(nameof(VirtualDiskSettingData));

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(SetVirtualHardDiskSettingData), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void SetVHDSnapshotInformation(string Information)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(SetVHDSnapshotInformation)))
            {
                ip[nameof(Information)] = Information ?? throw new ArgumentNullException(nameof(Information));

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(SetVHDSnapshotInformation), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public override void StartService()
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(StartService)))
            using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(StartService), ip, null))
                Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
        }

        public override void StopService()
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(StopService)))
            using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(StopService), ip, null))
                Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
        }

        public void Unmount(string Path)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(Unmount)))
            {
                ip[nameof(Path)] = Path ?? throw new ArgumentNullException(nameof(Path));

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(Unmount), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void ValidatePersistentReservationSupport(string Path)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(ValidatePersistentReservationSupport)))
            {
                ip[nameof(Path)] = Path ?? throw new ArgumentNullException(nameof(Path));

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(ValidatePersistentReservationSupport), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void ValidateVirtualHardDisk(string Path)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(ValidateVirtualHardDisk)))
            {
                ip[nameof(Path)] = Path ?? throw new ArgumentNullException(nameof(Path));

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(ValidateVirtualHardDisk), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        #region Utils

        public static ManagementObject CreateVirtualHardDiskSettingData(VirtualHardDiskType diskType, VirtualDiskFormat diskFormat, string path, string parentPath, long maxInternalSize, int blockSize = 0, int logicalSectorSize = 0, int physicalSectorSize = 0)
        {
            var managementPath = new ManagementPath()
            { 
                Server = Properties.Environment.Default.Server,
                NamespacePath = Instance.Msvm_ImageManagementService.Path.Path,
                ClassName = "Msvm_VirtualHardDiskSettingData"
            };

            using (var Msvm_VirtualHardDiskSettingDataClass = new ManagementClass(managementPath))
            using (var Msvm_VirtualHardDiskSettingData = Msvm_VirtualHardDiskSettingDataClass.CreateInstance())
            {
                if (Msvm_VirtualHardDiskSettingData == null)
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    throw new NullReferenceException(nameof(Msvm_VirtualHardDiskSettingData));
#pragma warning restore CA1303 // Do not pass literals as localized parameters

                Msvm_VirtualHardDiskSettingData["Type"] = diskType;
                Msvm_VirtualHardDiskSettingData["Format"] = diskFormat;
                Msvm_VirtualHardDiskSettingData["Path"] = path;
                Msvm_VirtualHardDiskSettingData["ParentPath"] = parentPath;
                Msvm_VirtualHardDiskSettingData["MaxInternalSize"] = maxInternalSize;
                Msvm_VirtualHardDiskSettingData["BlockSize"] = blockSize;
                Msvm_VirtualHardDiskSettingData["LogicalSectorSize"] = logicalSectorSize;
                Msvm_VirtualHardDiskSettingData["PhysicalSectorSize"] = physicalSectorSize;

                return Msvm_VirtualHardDiskSettingData;
            }
        }

        #endregion
    }
}
