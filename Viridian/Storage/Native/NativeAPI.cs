using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Viridian.Storage.Native
{
    public static class NativeAPI
    {
        [DllImport("virtdisk.dll", CharSet = CharSet.Unicode)]
        public static extern int OpenVirtualDisk(ref VirtualStorageType virtualStorageType, string path, VirtualDiskAccessMask virtualDiskAccessMask, OpenVirtualDiskFlag flags, ref OpenVirtualDiskParameters parameters, ref Handle.WinHandle safeHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(Handle.WinHandle hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("virtdisk.dll", CharSet = CharSet.Unicode)]
        public static extern int GetVirtualDiskPhysicalPath(Handle.WinHandle virtualDiskSafeHandle, ref int diskPathSizeInBytes, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder diskPath);

        [DllImport("kernel32.dll", EntryPoint = "CreateFileW", SetLastError = true)]
        public static extern SafeFileHandle CreateFile([MarshalAs(UnmanagedType.LPWStr)] string lpFileName, int dwDesiredAccess, int dwShareMode, IntPtr lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", EntryPoint = "DeviceIoControl", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeviceIoControl(SafeFileHandle hDevice, int dwIoControlCode, ref CreateDisk lpInBuffer, int nInBufferSize, IntPtr lpOutBuffer, int nOutBufferSize, ref int lpBytesReturned, IntPtr lpOverlapped);

        [DllImport("kernel32.dll", EntryPoint = "DeviceIoControl", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeviceIoControl2(SafeFileHandle deviceHandle, int ioControlCode, IntPtr inBuffer, int inBufferSize, IntPtr outBuffer, int outBufferSize, ref int bytesReturned, IntPtr overlapped);

        [DllImport("kernel32.dll", EntryPoint = "ReadFile", SetLastError = true)]
        public static extern bool ReadFile(SafeFileHandle hFile, byte[] pBuffer, int numberOfBytesToRead, ref int pNumberOfBytesRead, int overlapped);

        public const int GenericRead = -2147483648;
        public const int GenericWrite = 1073741824;
        public const int OpenExisting = 3;

        public const int IoctlDiskCreateDisk = 0x7C058;

        public static readonly Guid VirtualStorageTypeVendorMicrosoft = new Guid("EC984AEC-A0F9-47e9-901F-71415A66345B");

        public const int OpenVirtualDiskRwDepthDefault = 1;

        public const int ErrorSuccess = 0;
        public const int ErrorFileCorrupt = 1392;
        public const int ErrorFileNotFound = 2;
        public const int ErrorPathNotFound = 3;
        public const int ErrorAccessDenied = 5;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct VirtualStorageType
        {
            public int DeviceId;
            public Guid VendorId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct OpenVirtualDiskParameters
        {
            public OpenVirtualDiskVersion Version;
            public OpenVirtualDiskParametersVersion1 Version1;
        }

        public enum OpenVirtualDiskVersion
        {
            OpenVirtualDiskVersionUnspecified = 0,
            OpenVirtualDiskVersion1 = 1
        }

        public enum PartitionStyle
        {
            Raw = 0,
            Mbr = 1,
            Gpt = 2,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CreateDisk
        {
            public PartitionStyle PartitionStyle;
            public CreateDiskUnionMbrGpt MbrGpt;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CreateDiskUnionMbrGpt
        {
            [FieldOffset(0)]
            public CreateDiskMbr Mbr;
            [FieldOffset(0)]
            private readonly CreateDiskGpt Gpt;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CreateDiskMbr
        {
            public int Signature;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CreateDiskGpt
        {
            private readonly Guid DiskId;
            private readonly int MaxPartitionCount;
        }

        public enum MediaType
        {
            Unknown = 0x00,
            F51Pt2512 = 0x01,
            F31Pt44512 = 0x02,
            F32Pt88512 = 0x03,
            F320Pt8512 = 0x04,
            F3720512 = 0x05,
            F5360512 = 0x06,
            F5320512 = 0x07,
            F53201024 = 0x08,
            F5180512 = 0x09,
            F5160512 = 0x0a,
            RemovableMedia = 0x0b,
            FixedMedia = 0x0c,
            F3120M512 = 0x0d,
            F3640512 = 0x0e,
            F5640512 = 0x0f,
            F5720512 = 0x10,
            F31Pt2512 = 0x11,
            F31Pt231024 = 0x12,
            F51Pt231024 = 0x13,
            F3128Mb512 = 0x14,
            F3230Mb512 = 0x15,
            F8256128 = 0x16,
            F3200Mb512 = 0x17,
            F3240M512 = 0x18,
            F332M512 = 0x19,
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct DiskGeometry
        {
            [FieldOffset(0)]
            private readonly long Cylinders;

            [FieldOffset(8)]
            private readonly MediaType MediaType;

            [FieldOffset(12)]
            private readonly uint TracksPerCylinder;

            [FieldOffset(16)]
            private readonly uint SectorsPerTrack;

            [FieldOffset(20)]
            private readonly uint BytesPerSector;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DiskGeometryEx
        {
            [FieldOffset(0)]
            private readonly DiskGeometry Geometry;

            [FieldOffset(24)]
            private readonly long DiskSize;

            [FieldOffset(32)]
            private readonly byte Data;
        }

        [Flags]
        public enum VirtualDiskAccessMask
        {
            AttachReadOnly = 0x00010000,
            AttachReadWrite = 0x00020000,
            Detach = 0x00040000,
            GetInfo = 0x00080000,
            Create = 0x00100000,
            MetaOperations = 0x00200000,
            All = 0x003f0000,
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct OpenVirtualDiskParametersVersion1
        {
            public int RWDepth; //ULONG
        }

        public enum OpenVirtualDiskFlag
        {
            OpenVirtualDiskFlagNone = 0x00000000,
            OpenVirtualDiskFlagNoParents = 0x00000001,
            OpenVirtualDiskFlagBlankFile = 0x00000002,
            OpenVirtualDiskFlagBootDrive = 0x00000004
        }
    }
}
