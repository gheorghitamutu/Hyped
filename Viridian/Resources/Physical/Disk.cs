using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using Viridian.Exceptions;
using Viridian.Machine;
using Viridian.Resources.Physical;
using Viridian.Storage.Handle;
using Viridian.Storage.Native;
using Viridian.Utilities;

namespace Viridian.Storage.Virtual.Hard
{
    public static class Disk
    {
        public class GptType
        {
            private GptType(string value) { Value = value; }

            public string Value { get; set; }

            public static GptType SystemPartitionEFI { get { return new GptType("{c12a7328-f81f-11d2-ba4b-00a0c93ec93b}"); } }
            public static GptType MicrosoftReserved { get { return new GptType("{e3c9e316-0b5c-4db8-817d-f92df00215ae}"); } }
            public static GptType BasicData { get { return new GptType("{ebd0a0a2-b9e5-4433-87c0-68b6b72699c7}"); } }
            public static GptType LDMMetadata { get { return new GptType("{5808c8aa-7e8f-42e0-85d2-e1e90434cfb3}"); } }
            public static GptType LDMData { get { return new GptType("{af9b60a0-1431-4f62-bc68-3311714a69ad}"); } }
            public static GptType MicrosoftRecovery { get { return new GptType("{de94bba4-06d1-4d40-a16a-bfd50179d6ac}"); } }

        }

        public static void Attach(VM vm, string path, bool assignDriveLetter, bool readOnly)
        {
            // readOnly = true for ISOs

            using (var ims = Utils.GetImageManagementService(vm.Scope))
            using (var inParams = ims.GetMethodParameters("AttachVirtualHardDisk"))
            {
                inParams["Path"] = path;
                inParams["AssignDriveLetter"] = assignDriveLetter;
                inParams["ReadOnly"] = readOnly;

                using (var op = ims.InvokeMethod("AttachVirtualHardDisk", inParams, null))
                    Job.Validator.ValidateOutput(op, vm.Scope);
            }
        }

        public static void Compact(VM vm, string path, string mode)
        {
            using (var ims = Utils.GetImageManagementService(vm.Scope))
            using (var ip = ims.GetMethodParameters("CompactVirtualHardDisk"))
            {
                ip["Path"] = path;
                ip["Mode"] = mode;

                using (var op = ims.InvokeMethod("CompactVirtualHardDisk", ip, null))
                    Job.Validator.ValidateOutput(op, vm.Scope);
            }
        }

        public static void Convert(VM vm, string sourcePath, string destinationPath, StorageDeviceType format)
        {
            var vdds = new DiskSettings(HardDiskType.FixedSize, format, destinationPath, null, 0, 0, 0, 0);

            using (var ims = Utils.GetImageManagementService(vm.Scope))
            using (var ip = ims.GetMethodParameters("ConvertVirtualHardDisk"))
            {
                ip["SourcePath"] = sourcePath;
                ip["VirtualDiskSettingData"] = vdds.GetVirtualHardDiskSettingData(vm.ServerName, ims.Path.Path);

                using (var op = ims.InvokeMethod("ConvertVirtualHardDisk", ip, null))
                    Job.Validator.ValidateOutput(op, vm.Scope);
            }
        }

        public static void Create(VM vm, string virtualHardDiskPath, string parentPath, HardDiskType type, StorageDeviceType format, long fileSize, int blockSize, int logicalSectorSize, int physicalSectorSize)
        {
            var vdds = new DiskSettings(type, format, virtualHardDiskPath, parentPath, fileSize, blockSize, logicalSectorSize, physicalSectorSize);

            using (var ims = Utils.GetImageManagementService(vm.Scope))
            using (var ip = ims.GetMethodParameters("CreateVirtualHardDisk"))
            {
                ip["VirtualDiskSettingData"] = vdds.GetVirtualHardDiskSettingData(vm.ServerName, ims.Path.Path);

                using (var op = ims.InvokeMethod("CreateVirtualHardDisk", ip, null))
                    Job.Validator.ValidateOutput(op, vm.Scope);
            }
        }

        public static void Detach(VM vm, string virtualHardDiskPath)
        {
            using (var msi = new ManagementClass("Msvm_MountedStorageImage") { Scope = vm.Scope })
            using (var msiCollection = msi.GetInstances())
                foreach (ManagementObject disk in msiCollection)
                    if (string.Equals(disk["Name"].ToString(), virtualHardDiskPath, StringComparison.OrdinalIgnoreCase))
                        using (var op = disk.InvokeMethod("DetachVirtualHardDisk", null, null))
                            Job.Validator.ValidateOutput(op, vm.Scope);
        }

        public static void Display(VM vm, string path)
        {
            DiskSettings settings = null;
            DiskState state = null;

            using (var ims = Utils.GetImageManagementService(vm.Scope))
            {
                using (var ip = ims.GetMethodParameters("GetVirtualHardDiskSettingData"))
                {
                    ip["Path"] = path;

                    using (var op = ims.InvokeMethod("GetVirtualHardDiskSettingData", ip, null))
                    {
                        Job.Validator.ValidateOutput(op, vm.Scope);

                        if (op != null)
                            settings = DiskSettings.Parse(op["SettingData"].ToString());
                    }
                }

                using (var ip = ims.GetMethodParameters("GetVirtualHardDiskState"))
                {
                    ip["Path"] = path;

                    using (var op = ims.InvokeMethod("GetVirtualHardDiskState", ip, null))
                    {
                        Job.Validator.ValidateOutput(op, vm.Scope);

                        if (op != null) 
                            state = DiskState.Parse(op["State"].ToString());
                    }
                }
            }

            if (settings != null)
            {
                Trace.Write($"Path:\t\t\t{settings.GetPath}");

                switch (settings.GetDiskFormat)
                {
                    case StorageDeviceType.Vhd:     Trace.Write(" (Vhd)");      break;
                    case StorageDeviceType.Vhdx:    Trace.Write(" (Vhdx)");     break;
                    case StorageDeviceType.Unknown: Trace.Write(" (Unknown)");  break;
                    case StorageDeviceType.Iso:     Trace.Write(" (Iso)");      break;
                    default:                        Trace.Write(" (Unknown)");  break;
                }

                switch (settings.GetDiskType)
                {
                    case HardDiskType.FixedSize:
                        Trace.WriteLine(" (Fixed Disk)");

                        if (state != null)
                            Trace.WriteLine($"FragmentationPercentage:{state.GetFragmentationPercentage}");

                        break;
                    case HardDiskType.DynamicallyExpanding:
                        Trace.WriteLine("(Dynamically Expanding Disk)");
                        Trace.WriteLine($"MaxInternalSize:\t{settings.GetMaxInternalSize}");
                        Trace.WriteLine($"BlockSize:\t\t{settings.GetBlockSize}");

                        if (state != null)
                        {
                            Trace.WriteLine($"Alignment:\t\t{state.GetAlignment}");
                            Trace.WriteLine($"FragmentationPercentage:{state.GetFragmentationPercentage}");
                        }

                        break;
                    case HardDiskType.Differencing:
                        Trace.WriteLine("(Differencing Disk)");
                        Trace.WriteLine($"Parent:\t\t\t{settings.GetParentPath}");
                        Trace.WriteLine($"MaxInternalSize:\t{settings.GetMaxInternalSize}");
                        Trace.WriteLine($"BlockSize:\t\t{settings.GetBlockSize}");

                        if (state != null)
                            Trace.WriteLine($"Alignment:\t\t{state.GetAlignment}");

                        break;
                    case HardDiskType.Unknown:
                        Trace.WriteLine("(Unknown Disk)");
                        break;
                    default:
                        throw new ViridianException("", new ArgumentOutOfRangeException());
                }

                if (state != null) 
                    Trace.WriteLine($"FileSize:\t\t{state.GetFileSize}");

                Trace.WriteLine($"LogicalSectorSize:\t{settings.GetLogicalSectorSize}");
                Trace.WriteLine($"PhysicalSectorSize:\t{settings.PhysicalSectorSize}");
            }

            if (state?.GetMinInternalSize != null)
                Trace.WriteLine($"MinpublicSize:\t{state.GetMinInternalSize}");
        }

        public static void Initialize(ManagementObject msftDisk, Native.NativeAPI.PartitionStyle partitionStyle, string serverName, string scopePath)
        {
            var scope = Utils.GetScope(serverName, scopePath);

            using (var ip = msftDisk.GetMethodParameters("Initialize"))
            {
                ip["PartitionStyle"] = partitionStyle;

                using (var op = msftDisk.InvokeMethod("Initialize", ip, null))
                    Job.Validator.ValidateOutput(op, scope);
            }
        }

        public static void Merge(VM vm, string sourcePath, string destinationPath)
        {
            using (var ims = Utils.GetImageManagementService(vm.Scope))
            using (var ip = ims.GetMethodParameters("MergeVirtualHardDisk"))
            {
                ip["SourcePath"] = sourcePath;
                ip["DestinationPath"] = destinationPath;

                using (var op = ims.InvokeMethod("MergeVirtualHardDisk", ip, null))
                    Job.Validator.ValidateOutput(op, vm.Scope);
            }
        }

        private static WinHandle Open(string fileName, NativeAPI.VirtualDiskAccessMask fileAccessMask, StorageDeviceType storageDeviceTypeType)
        {
            var parameters = new NativeAPI.OpenVirtualDiskParameters 
            {
                Version = NativeAPI.OpenVirtualDiskVersion.OpenVirtualDiskVersion1, 
                Version1 = 
                { 
                    RWDepth = NativeAPI.OpenVirtualDiskRwDepthDefault
                }
            };

            var storageType = new NativeAPI.VirtualStorageType
            {
                DeviceId = (int)storageDeviceTypeType,
                VendorId = NativeAPI.VirtualStorageTypeVendorMicrosoft 
            };

            fileAccessMask = ((fileAccessMask & NativeAPI.VirtualDiskAccessMask.GetInfo) == NativeAPI.VirtualDiskAccessMask.GetInfo) ? NativeAPI.VirtualDiskAccessMask.GetInfo : 0;

            fileAccessMask |= NativeAPI.VirtualDiskAccessMask.AttachReadOnly;

            var handle = new WinHandle();

            var res = NativeAPI.OpenVirtualDisk(ref storageType, fileName, fileAccessMask, NativeAPI.OpenVirtualDiskFlag.OpenVirtualDiskFlagNone, ref parameters, ref handle);

            if (res == NativeAPI.ErrorSuccess)
                return handle;

            handle.SetHandleAsInvalid();

            switch (res)
            {
                case NativeAPI.ErrorFileNotFound:
                case NativeAPI.ErrorPathNotFound:   throw new ViridianException("File not found!", new FileNotFoundException());
                case NativeAPI.ErrorAccessDenied:   throw new ViridianException("Access is denied!", new FileNotFoundException());
                case NativeAPI.ErrorFileCorrupt:    throw new ViridianException("File type not recognized!", new FileNotFoundException());
                default:                            throw new ViridianException("", new Win32Exception(res));
            }
        }

        public static int GetVirtualDiskIndex(string fileName, NativeAPI.VirtualDiskAccessMask fileAccessMask, StorageDeviceType storageDeviceTypeType)
        {
            using (var handle = Open(fileName, fileAccessMask, storageDeviceTypeType))
                return FindVhdPhysicalDriveNumber(handle);
        }

        private static int FindVhdPhysicalDriveNumber(WinHandle safeHandle)
        {
            var bufferSize = 260;

            var vhdPhysicalPath = new StringBuilder(bufferSize);

            NativeAPI.GetVirtualDiskPhysicalPath(safeHandle, ref bufferSize, vhdPhysicalPath);

            if (int.TryParse(Regex.Match(vhdPhysicalPath.ToString(), @"\d+").Value, out var driveNumber))
                return driveNumber;

            throw new ViridianException("Failed parsing physical drive number! Is your drive attached?!");
        }

        public static void Resize(VM vm, string path, ulong maxpublicSize)
        {
            using (var ims = Utils.GetImageManagementService(vm.Scope))
            using (var ip = ims.GetMethodParameters("ResizeVirtualHardDisk"))
            {
                ip["Path"] = path;
                ip["MaxpublicSize"] = maxpublicSize;

                using (var op = ims.InvokeMethod("ResizeVirtualHardDisk", ip, null))
                    Job.Validator.ValidateOutput(op, vm.Scope);
            }
        }

        public static void SetParent(VM vm, string childPath, string parentPath, string leafPath, string ignoreIdMismatch)
        {
            using (var ims = Utils.GetImageManagementService(vm.Scope))
            using (var ip = ims.GetMethodParameters("SetParentVirtualHardDisk"))
            {
                ip["ChildPath"] = childPath;
                ip["ParentPath"] = parentPath;
                ip["LeafPath"] = leafPath;
                ip["IgnoreIDMismatch"] = ignoreIdMismatch;

                using (var op = ims.InvokeMethod("SetParentVirtualHardDisk", ip, null))
                    Job.Validator.ValidateOutput(op, vm.Scope);
            }
        }

        public static void SetVirtualHardDisk(VM vm, string virtualHardDiskPath, string parentPath, int physicalSectorSize)
        {
            var vdsd = new DiskSettings(HardDiskType.Unknown, StorageDeviceType.Unknown, virtualHardDiskPath, parentPath, 0, 0, 0, physicalSectorSize);

            using (var ims = Utils.GetImageManagementService(vm.Scope))
            using (var ip = ims.GetMethodParameters("SetVirtualHardDiskSettingData"))
            {
                ip["VirtualDiskSettingData"] = vdsd.GetVirtualHardDiskSettingData(vm.ServerName, ims.Path.Path);

                using (var op = ims.InvokeMethod("SetVirtualHardDiskSettingData", ip, null))
                    Job.Validator.ValidateOutput(op, vm.Scope);
            }
        }

        public static void Validate(VM vm, string virtualHardDiskPath)
        {
            using (var ims = Utils.GetImageManagementService(vm.Scope))
            using (var ip = ims.GetMethodParameters("ValidateVirtualHardDisk"))
            {
                ip["Path"] = virtualHardDiskPath;

                using (var op = ims.InvokeMethod("ValidateVirtualHardDisk", ip, null))
                    Job.Validator.ValidateOutput(op, vm.Scope);
            }
        }

        #region MSFT
        //  @"\Root\Microsoft\Windows\Storage"

        public static ManagementObject GetMsftDiskFromPath(string serverName, string scopePath, string diskPath)
        {
            var scope = Utils.GetScope(serverName, scopePath);
            var query = new ObjectQuery("SELECT * FROM MSFT_Disk");

            using (var mos = new ManagementObjectSearcher(scope, query))
                foreach (ManagementObject disk in mos.Get())
                    if (disk["Location"] as string == diskPath)
                        return disk;

            throw new ViridianException("Disk not found!");
        }

        public static void CreatePartition(ManagementObject msftDisk, ulong size, string serverName, string scopePath, GptType gptType, char driveLetter = ' ', bool assignDriveLetter = false, bool useMaximumSize = false)
        {
            var scope = Utils.GetScope(serverName, scopePath);

            using (var ip = msftDisk.GetMethodParameters("CreatePartition"))
            {
                ip["UseMaximumSize"] = useMaximumSize;
                if (useMaximumSize == false)
                    ip["Size"] = size;

                ip["AssignDriveLetter"] = assignDriveLetter;
                if (assignDriveLetter)
                    ip["DriveLetter"] = driveLetter;

                ip["GptType"] = gptType.Value;

                using (var op = msftDisk.InvokeMethod("CreatePartition", ip, null))
                    Job.Validator.ValidateOutput(op, scope);
            }
        }

        public static void AssignLetterToPartition(string serverName, string scopePath, ManagementObject msftDisk, int partitionIndex, string accessPath, bool assignNextLetter)
        {
            if (accessPath.Length == 0 && !assignNextLetter || !assignNextLetter && accessPath.Length > 2)
                throw new ViridianException("Invalid letter to partition provided!");

            using (var partitionCollection = msftDisk.GetRelated("MSFT_Partition", null, null, null, null, null, false, null))
            {
                var countPartition = 0;
                foreach (ManagementObject partition in partitionCollection)
                {
                    if (countPartition == partitionIndex)
                        using (var ip = partition.GetMethodParameters("AddAccessPath"))
                        {
                            if (assignNextLetter) 
                                ip["AssignDriveLetter"] = true;
                            else 
                                ip["AccessPath"] = accessPath;

                            using (var op = partition.InvokeMethod("AddAccessPath", ip, null))
                            {
                                Job.Validator.ValidateOutput(op, Utils.GetScope(serverName, scopePath));
                                return;
                            }
                        }

                    countPartition++;
                }
            }

            throw new ViridianException("Partition not found!");
        }

        public static ManagementObject GetMsftDiskVolume(ManagementObject msftDisk, int partitionIndex, int volumeIndex)
        {
            using (var partitionCollection = msftDisk.GetRelated("MSFT_Partition", null, null, null, null, null, false, null))
            {
                var countPartition = 0;
                foreach (ManagementObject partition in partitionCollection)
                {
                    if (countPartition == partitionIndex)
                        using (var volumeCollection = partition.GetRelated("MSFT_Volume", null, null, null, null, null, false, null))
                        {
                            var countVolume = 0;
                            foreach (ManagementObject volume in volumeCollection)
                            {
                                if (countVolume == volumeIndex)
                                    return volume;

                                countVolume++;
                            }
                        }
                    else
                        countPartition++;
                }
            }

            throw new ViridianException("Volume not found!");
        }

        public static void FormatMsftVolume(ManagementObject msftVolume, string serverName, string scopePath)
        {
            var scope = Utils.GetScope(serverName, scopePath);

            using (var ip = msftVolume.GetMethodParameters("Format"))
            {
                ip["FileSystem"] = "NTFS";
                ip["Full"] = true;
                ip["Compress"] = true;

                using (var op = msftVolume.InvokeMethod("Format", ip, null))
                    Job.Validator.ValidateOutput(op, scope);
            }
        }

        #endregion
    }
}
