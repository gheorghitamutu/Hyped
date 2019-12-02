using System;
using System.Management;
using Viridian.Exceptions;
using Viridian.Scopes;

namespace Viridian.Resources.MSFT
{
    public sealed class Volume
    {
        private ManagementObject MSFT_Volume = null;

        public enum VolumeDedupMode : uint
        {
            Disabled = 0,
            GeneralPurpose = 1,
            HyperV = 2,
            Backup = 3,
            NotAvailable = 4
        }
        public enum VolumeDriveType : uint
        {
            Unknown = 0,
            InvalidRootPath = 1,
            Removable = 2,
            Fixed = 3,
            Remote = 4,
            CDROM = 5,
            RAMDisk = 6
        }
        public enum VolumeFileSystemType : ushort
        {
            Unknown = 0,
            UFS = 2,
            HFS = 3,
            FAT = 4,
            FAT16 = 5,
            FAT32 = 6,
            NTFS4 = 7,
            NTFS5 = 8,
            XFS = 9,
            AFS = 10,
            EXT2 = 11,
            EXT3 = 12,
            ReiserFS = 13,
            NTFS = 14,
            ReFS = 15,
            CSVFSNTFS = 0x8000,
            CSVFSReFS = 0x8001
        }
        public enum VolumeHealthStatus : uint
        {
            Healthy = 0,
            ScanNeeded = 1,
            SpotFixNeeded = 2,
            FullRepairNeeded = 3
        }

        public class VolumeFileSystem
        {
            private VolumeFileSystem(string value) { Value = value; }

            public string Value { get; set; }

            public static VolumeFileSystem SystemPartitionEFI => new VolumeFileSystem(nameof(SystemPartitionEFI));
            public static VolumeFileSystem MicrosoftReserved => new VolumeFileSystem(nameof(MicrosoftReserved));
            public static VolumeFileSystem BasicData => new VolumeFileSystem(nameof(BasicData));
            public static VolumeFileSystem LDMMetadata => new VolumeFileSystem(nameof(LDMMetadata));
            public static VolumeFileSystem ReFS => new VolumeFileSystem(nameof(ReFS));
            public static VolumeFileSystem NTFS => new VolumeFileSystem(nameof(NTFS));
        }

        public Volume(ManagementObject MsftVolume)
        {
            MSFT_Volume = MsftVolume;
        }

        public ManagementObject MSFTVolume => MSFT_Volume;

        #region MsftProperties

        char DriveLetter => (char)MSFT_Volume[nameof(DriveLetter)];
        string Path => MSFT_Volume[nameof(Path)].ToString();
        VolumeHealthStatus HealthStatus => (VolumeHealthStatus)(ushort)MSFT_Volume[nameof(HealthStatus)];
        string FileSystem => MSFT_Volume[nameof(FileSystem)].ToString();
        string FileSystemLabel => MSFT_Volume[nameof(FileSystemLabel)].ToString();
        VolumeFileSystemType FileSystemType => (VolumeFileSystemType)(ushort)MSFT_Volume[nameof(FileSystemType)];
        ulong Size => (ulong)MSFT_Volume[nameof(Size)];
        ulong SizeRemaining => (ulong)MSFT_Volume[nameof(SizeRemaining)];
        VolumeDriveType DriveType => (VolumeDriveType)(uint)MSFT_Volume[nameof(DriveType)];
        VolumeDedupMode DedupMode => (VolumeDedupMode)(uint)MSFT_Volume[nameof(DedupMode)];

        #endregion

        public string DeleteObject()
        {
            using (var ip = MSFT_Volume.GetMethodParameters(nameof(DeleteObject)))
            using (var op = MSFT_Volume.InvokeMethod(nameof(DeleteObject), ip, null))
            {
                Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                return op["ExtendedStatus"] as string;
            }
        }

        public string[] Diagnose()
        {
            using (var ip = MSFT_Volume.GetMethodParameters(nameof(Diagnose)))
            using (var op = MSFT_Volume.InvokeMethod(nameof(Diagnose), ip, null))
            {
                Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                return op["DiagnoseResults"] as string[];
            }
        }

        public void Flush()
        {
            using (var ip = MSFT_Volume.GetMethodParameters(nameof(Flush)))
            using (var op = MSFT_Volume.InvokeMethod(nameof(Flush), ip, null))
                Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);
        }

        public string Format(string FileSystem, string FileSystemLabel, ulong AllocationUnitSize, bool Full, bool Force, bool Compress, bool ShortFileNameSupport, bool SetIntegrityStreams, bool UseLargeFRS, bool DisableHeatGathering)
        {
            using (var ip = MSFT_Volume.GetMethodParameters(nameof(Format)))
            {
                ip[nameof(FileSystem)] = FileSystem ?? throw new ViridianException($"{nameof(FileSystem)} is null!");
                ip[nameof(FileSystemLabel)] = FileSystemLabel ?? throw new ViridianException($"{nameof(FileSystemLabel)} is null!");
                ip[nameof(AllocationUnitSize)] = AllocationUnitSize;
                ip[nameof(Full)] = Full;
                ip[nameof(Force)] = Force;
                ip[nameof(Compress)] = Compress;
                ip[nameof(ShortFileNameSupport)] = ShortFileNameSupport;
                if (FileSystem == VolumeFileSystem.ReFS.Value)
                    ip[nameof(SetIntegrityStreams)] = SetIntegrityStreams;
                if (FileSystem != VolumeFileSystem.ReFS.Value) // not supported in current tests; maybe has something to do with AllocationUnitSize?
                    if (UseLargeFRS)
                        throw new ViridianException("Not supported UseLargeFRS!", new NotSupportedException());
                    else
                        ip[nameof(UseLargeFRS)] = UseLargeFRS;
                ip[nameof(DisableHeatGathering)] = DisableHeatGathering;

                using (var op = MSFT_Volume.InvokeMethod(nameof(Format), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                    return op["FormattedVolume"].ToString();
                }
            }
        }

        public bool GetAttributes()
        {
            using (var ip = MSFT_Volume.GetMethodParameters(nameof(GetAttributes)))
            using (var op = MSFT_Volume.InvokeMethod(nameof(GetAttributes), ip, null))
            {
                Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                return (bool)op["VolumeScrubEnabled"];
            }
        }

        public uint GetCorruptionCount()
        {
            using (var ip = MSFT_Volume.GetMethodParameters(nameof(GetCorruptionCount)))
            using (var op = MSFT_Volume.InvokeMethod(nameof(GetCorruptionCount), ip, null))
            {
                Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                return (uint)op["CorruptionCount"];
            }
        }

        public string GetDedupProperties()
        {
            using (var ip = MSFT_Volume.GetMethodParameters(nameof(GetDedupProperties)))
            using (var op = MSFT_Volume.InvokeMethod(nameof(GetDedupProperties), ip, null))
            {
                Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                return op["DedupProperties"].ToString();
            }
        }

        public uint[] GetSupportedClusterSizes(string FileSystem)
        {
            using (var ip = MSFT_Volume.GetMethodParameters(nameof(GetSupportedClusterSizes)))
            {
                ip[nameof(FileSystem)] = FileSystem ?? throw new ViridianException($"{nameof(FileSystem)} is null!");

                using (var op = MSFT_Volume.InvokeMethod(nameof(GetSupportedClusterSizes), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                    return op["SupportedClusterSizes"] as uint[];
                }
            }
        }

        public string[] GetSupportedFileSystems()
        {
            using (var ip = MSFT_Volume.GetMethodParameters(nameof(GetSupportedFileSystems)))
            using (var op = MSFT_Volume.InvokeMethod(nameof(GetSupportedFileSystems), ip, null))
            {
                Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                return op["SupportedFileSystems"] as string[];
            }
        }

        public string Optimize(bool ReTrim, bool Analyze, bool Defrag, bool SlabConsolidate, bool TierOptimize, bool ExtendedStatus)
        {
            using (var ip = MSFT_Volume.GetMethodParameters(nameof(Optimize)))
            {
                ip[nameof(ReTrim)] = ReTrim;
                ip[nameof(Analyze)] = Analyze;
                ip[nameof(Defrag)] = Defrag;
                ip[nameof(SlabConsolidate)] = SlabConsolidate;
                ip[nameof(TierOptimize)] = TierOptimize;
                ip[nameof(ExtendedStatus)] = ExtendedStatus;

                using (var op = MSFT_Volume.InvokeMethod(nameof(Optimize), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                    return op["ExtendedStatus"].ToString();
                }
            }
        }

        public uint Repair(bool OfflineScanAndFix, bool Scan, bool SpotFix)
        {
            using (var ip = MSFT_Volume.GetMethodParameters(nameof(Repair)))
            {
                ip[nameof(OfflineScanAndFix)] = OfflineScanAndFix;
                ip[nameof(Scan)] = Scan;
                ip[nameof(SpotFix)] = SpotFix;

                using (var op = MSFT_Volume.InvokeMethod(nameof(Repair), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                    return (uint)op["Output"];
                }
            }
        }

        public string Resize(ulong Size)
        {
            using (var ip = MSFT_Volume.GetMethodParameters(nameof(Resize)))
            {
                ip[nameof(Size)] = Size;

                using (var op = MSFT_Volume.InvokeMethod(nameof(Resize), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                    return op["ExtendedStatus"].ToString();
                }
            }
        }

        public string SetAttributes(bool EnableVolumeScrub)
        {
            using (var ip = MSFT_Volume.GetMethodParameters(nameof(SetAttributes)))
            {
                ip[nameof(EnableVolumeScrub)] = EnableVolumeScrub;

                using (var op = MSFT_Volume.InvokeMethod(nameof(SetAttributes), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                    return op["ExtendedStatus"].ToString();
                }
            }
        }

        public string SetDedupMode(VolumeDedupMode SetDedupMode)
        {
            using (var ip = MSFT_Volume.GetMethodParameters(nameof(SetDedupMode)))
            {
                ip[nameof(SetDedupMode)] = SetDedupMode;

                using (var op = MSFT_Volume.InvokeMethod(nameof(SetDedupMode), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                    return op["ExtendedStatus"].ToString();
                }
            }
        }

        public string SetFileSystemLabel(string FileSystemLabel)
        {
            using (var ip = MSFT_Volume.GetMethodParameters(nameof(SetFileSystemLabel)))
            {
                ip[nameof(FileSystemLabel)] = FileSystemLabel ?? throw new ViridianException($"{nameof(FileSystemLabel)} is null!"); ;

                using (var op = MSFT_Volume.InvokeMethod(nameof(SetFileSystemLabel), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.SpecificScope);

                    return op["ExtendedStatus"].ToString();
                }
            }
        }
    }
}
