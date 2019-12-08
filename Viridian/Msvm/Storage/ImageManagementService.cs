using System;
using System.Management;
using Viridian.Exceptions;
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
        private ManagementObject Msvm_ImageManagementService => Service ?? throw new ViridianException($"{ServiceName} is null!");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
#pragma warning restore CA1707 // Identifiers should not contain underscores

        public void AttachVirtualHardDisk(string Path, bool AssignDriveLetter, bool ReadOnly)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(AttachVirtualHardDisk)))
            {
                ip[nameof(Path)] = Path ?? throw new ViridianException($"{nameof(Path)} is null!");
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
                ip[nameof(Path)] = Path ?? throw new ViridianException($"{nameof(Path)} is null!");
                ip[nameof(Mode)] = Mode;

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(CompactVirtualHardDisk), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void ConvertVirtualHardDisk(string SourcePath, string VirtualDiskSettingData)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(ConvertVirtualHardDisk)))
            {
                ip[nameof(SourcePath)] = SourcePath ?? throw new ViridianException($"{nameof(SourcePath)} is null!");
                ip[nameof(VirtualDiskSettingData)] = VirtualDiskSettingData ?? throw new ViridianException($"{nameof(VirtualDiskSettingData)} is null!");

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(ConvertVirtualHardDisk), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void CreateVirtualHardDisk(string VirtualDiskSettingData)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(CreateVirtualHardDisk)))
            {
                ip[nameof(VirtualDiskSettingData)] = VirtualDiskSettingData ?? throw new ViridianException($"{nameof(VirtualDiskSettingData)} is null!");

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(CreateVirtualHardDisk), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void CreateVirtualFloppyDisk(string Path)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(CreateVirtualFloppyDisk)))
            {
                ip[nameof(Path)] = Path ?? throw new ViridianException($"{nameof(Path)} is null!");

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(CreateVirtualFloppyDisk), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void ConvertVirtualHardDiskToVHDSet(string VirtualHardDiskPath)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(ConvertVirtualHardDiskToVHDSet)))
            {
                ip[nameof(VirtualHardDiskPath)] = VirtualHardDiskPath ?? throw new ViridianException($"{nameof(VirtualHardDiskPath)} is null!");

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(ConvertVirtualHardDiskToVHDSet), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void DeleteVHDSnapshot(string VHDSetPath, string SnapshotId, bool PersistReferenceSnapshot)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(DeleteVHDSnapshot)))
            {
                ip[nameof(VHDSetPath)] = VHDSetPath ?? throw new ViridianException($"{nameof(VHDSetPath)} is null!");
                ip[nameof(SnapshotId)] = SnapshotId ?? throw new ViridianException($"{nameof(SnapshotId)} is null!");
                ip[nameof(PersistReferenceSnapshot)] = PersistReferenceSnapshot;

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(DeleteVHDSnapshot), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public ManagementObject FindMountedStorageImageInstance(string SelectionCriterion, CriterionType CriterionType)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(FindMountedStorageImageInstance)))
            {
                ip[nameof(SelectionCriterion)] = SelectionCriterion ?? throw new ViridianException($"{nameof(SelectionCriterion)} is null!");
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
                ip[nameof(Path)] = Path ?? throw new ViridianException($"{nameof(Path)} is null!");
                ip[nameof(LimitId)] = LimitId ?? throw new ViridianException($"{nameof(LimitId)} is null!");
                ip[nameof(TargetSnapshotId)] = TargetSnapshotId ?? throw new ViridianException($"{nameof(TargetSnapshotId)} is null!");
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
                ip[nameof(Path)] = Path ?? throw new ViridianException($"{nameof(Path)} is null!");

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
                ip[nameof(Path)] = Path ?? throw new ViridianException($"{nameof(Path)} is null!");

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
                ip[nameof(VHDSetPath)] = VHDSetPath ?? throw new ViridianException($"{nameof(VHDSetPath)} is null!");
                ip[nameof(VHDSetPath)] = AdditionalInformation ?? throw new ViridianException($"{nameof(AdditionalInformation)} is null!");

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
                ip[nameof(VHDSetPath)] = VHDSetPath ?? throw new ViridianException($"{nameof(VHDSetPath)} is null!");
                ip[nameof(SnapshotIds)] = SnapshotIds ?? throw new ViridianException($"{nameof(SnapshotIds)} is null!");
                ip[nameof(VHDSetPath)] = AdditionalInformation ?? throw new ViridianException($"{nameof(AdditionalInformation)} is null!");

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
                ip[nameof(SourcePath)] = SourcePath ?? throw new ViridianException($"{nameof(SourcePath)} is null!");
                ip[nameof(DestinationPath)] = DestinationPath ?? throw new ViridianException($"{nameof(DestinationPath)} is null!");

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(MergeVirtualHardDisk), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void OptimizeVHDSet(string VHDSetPath)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(OptimizeVHDSet)))
            {
                ip[nameof(VHDSetPath)] = VHDSetPath ?? throw new ViridianException($"{nameof(VHDSetPath)} is null!");

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(OptimizeVHDSet), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void ResizeVirtualHardDisk(string Path, ulong MaxInternalSize)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(ResizeVirtualHardDisk)))
            {
                ip[nameof(Path)] = Path ?? throw new ViridianException($"{nameof(Path)} is null!");
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
                ip[nameof(ChildPath)] = ChildPath ?? throw new ViridianException($"{nameof(ChildPath)} is null!");
                ip[nameof(ParentPath)] = ParentPath ?? throw new ViridianException($"{nameof(ParentPath)} is null!");
                ip[nameof(LeafPath)] = LeafPath ?? throw new ViridianException($"{nameof(LeafPath)} is null!");
                ip[nameof(IgnoreIDMismatch)] = IgnoreIDMismatch;

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(SetParentVirtualHardDisk), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void SetVirtualHardDiskSettingData(string VirtualDiskSettingData)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(SetVirtualHardDiskSettingData)))
            {
                ip[nameof(VirtualDiskSettingData)] = VirtualDiskSettingData ?? throw new ViridianException($"{nameof(VirtualDiskSettingData)} is null!");

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(SetVirtualHardDiskSettingData), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void SetVHDSnapshotInformation(string Information)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(SetVHDSnapshotInformation)))
            {
                ip[nameof(Information)] = Information ?? throw new ViridianException($"{nameof(Information)} is null!");

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
                ip[nameof(Path)] = Path ?? throw new ViridianException($"{nameof(Path)} is null!");

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(Unmount), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void ValidatePersistentReservationSupport(string Path)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(ValidatePersistentReservationSupport)))
            {
                ip[nameof(Path)] = Path ?? throw new ViridianException($"{nameof(Path)} is null!");

                using (var op = Msvm_ImageManagementService.InvokeMethod(nameof(ValidatePersistentReservationSupport), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void ValidateVirtualHardDisk(string Path)
        {
            using (var ip = Msvm_ImageManagementService.GetMethodParameters(nameof(ValidateVirtualHardDisk)))
            {
                ip[nameof(Path)] = Path ?? throw new ViridianException($"{nameof(Path)} is null!");

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

            using (var settingsClass = new ManagementClass(managementPath))
            using (var settingsInstance = settingsClass.CreateInstance())
            {
                if (settingsInstance == null)
                    return null;

                settingsInstance["Type"] = diskType;
                settingsInstance["Format"] = diskFormat;
                settingsInstance["Path"] = path;
                settingsInstance["ParentPath"] = parentPath;
                settingsInstance["MaxInternalSize"] = maxInternalSize;
                settingsInstance["BlockSize"] = blockSize;
                settingsInstance["LogicalSectorSize"] = logicalSectorSize;
                settingsInstance["PhysicalSectorSize"] = physicalSectorSize;

                return settingsInstance;
            }
        }

        #endregion
    }
}
