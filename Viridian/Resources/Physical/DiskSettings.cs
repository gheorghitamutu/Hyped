using System;
using System.Globalization;
using System.Management;
using System.Xml;
using Viridian.Exceptions;

namespace Viridian.Storage.Virtual.Hard
{
    public enum HardDiskType
    {
        Unknown = 0,
        FixedSize = 2,
        DynamicallyExpanding = 3,
        Differencing = 4
    }

    public enum HardDiskAccess
    {
        Unknown = 0,
        Readable = 1,
        Writeable = 2,
        ReadWrite = 3
    }

    public enum StorageDeviceType
    {
        Unknown = 0,
        Iso = 1,
        Vhd = 2,
        Vhdx = 3
    }

    public class DiskSettings
    {
        public DiskSettings(HardDiskType diskType, StorageDeviceType diskFormat, string path, string parentPath, long maxInternalSize, int blockSize, int logicalSectorSize, int physicalSectorSize)
        {
            DiskType = diskType;
            DiskFormat = diskFormat;
            Path = path;
            ParentPath = parentPath;
            MaxInternalSize = maxInternalSize;
            BlockSize = blockSize;
            LogicalSectorSize = logicalSectorSize;
            PhysicalSectorSize1 = physicalSectorSize;
        }

        public HardDiskType GetDiskType => DiskType;
        public StorageDeviceType GetDiskFormat => DiskFormat;
        public string GetPath => Path;
        public string GetParentPath => ParentPath;
        public long GetMaxInternalSize => MaxInternalSize;
        public long GetBlockSize => BlockSize;
        public long GetLogicalSectorSize => LogicalSectorSize;
        public long PhysicalSectorSize => PhysicalSectorSize1;

        private string Path { get; }
        private HardDiskType DiskType { get; }
        private StorageDeviceType DiskFormat { get; }
        private string ParentPath { get; }
        private long MaxInternalSize { get; }
        private int BlockSize { get; }
        private int LogicalSectorSize { get; }
        private int PhysicalSectorSize1 { get; }

        public string GetVirtualHardDiskSettingData(string serverName, string namespacePath)
        {
            var path = new ManagementPath() { Server = serverName, NamespacePath = namespacePath, ClassName = "Msvm_VirtualHardDiskSettingData" };

            using (var settingsClass = new ManagementClass(path))
            using (var settingsInstance = settingsClass.CreateInstance())
            {
                if (settingsInstance == null)
                    return null;

                settingsInstance["Type"] = DiskType;
                settingsInstance["Format"] = DiskFormat;
                settingsInstance["Path"] = Path;
                settingsInstance["ParentPath"] = ParentPath;
                settingsInstance["MaxInternalSize"] = MaxInternalSize;
                settingsInstance["BlockSize"] = BlockSize;
                settingsInstance["LogicalSectorSize"] = LogicalSectorSize;
                settingsInstance["PhysicalSectorSize"] = PhysicalSectorSize1;
                var settingsInstanceString = settingsInstance.GetText(TextFormat.WmiDtd20);

                return settingsInstanceString;
            }
        }

        public static DiskSettings Parse(string virtualHardDiskSettingData)
        {
            var parentPath = string.Empty;

            var doc = new XmlDocument();
            doc.LoadXml(virtualHardDiskSettingData);

            var nodelist = doc.SelectNodes(@"/INSTANCE/@CLASSNAME");

            if (nodelist == null) 
                return null;

            if (nodelist.Count != 1)
                throw new ViridianException("", new FormatException());

            if (nodelist[0].Value != "Msvm_VirtualHardDiskSettingData")
                throw new ViridianException("", new FormatException());

            // Disk type
            nodelist = doc.SelectNodes(@"//PROPERTY[@NAME = 'Type']/VALUE/child::text()");

            if (nodelist == null)
                return null;

            if (nodelist.Count != 1) 
                throw new ViridianException("", new FormatException());

            var itype = int.Parse(nodelist[0].Value, NumberStyles.None, CultureInfo.InvariantCulture);

            var type = (HardDiskType)itype;

            if (type != HardDiskType.Differencing && type != HardDiskType.DynamicallyExpanding && type != HardDiskType.FixedSize)
                throw new ViridianException("The type integer returned is of an unrecognized type!", new FormatException());

            // Disk format
            nodelist = doc.SelectNodes(@"//PROPERTY[@NAME = 'Format']/VALUE/child::text()");

            if (nodelist == null)
                return null;

            if (nodelist.Count != 1)
                throw new ViridianException("", new FormatException());

            var iformat = int.Parse(nodelist[0].Value, NumberStyles.None, CultureInfo.InvariantCulture);

            var format = (StorageDeviceType)iformat;

            if (format != StorageDeviceType.Vhd && format != StorageDeviceType.Vhdx) throw new ViridianException("", new FormatException());

            // Path
            nodelist = doc.SelectNodes(@"//PROPERTY[@NAME = 'Path']/VALUE/child::text()");

            if (nodelist == null)
                return null;

            if (nodelist.Count != 1)
                throw new ViridianException("There can not be multiple parents!", new FormatException());

            var path = nodelist[0].Value;

            // ParentPath
            nodelist = doc.SelectNodes(@"//PROPERTY[@NAME = 'ParentPath']/VALUE/child::text()");

            if (nodelist == null)
                return null;

            // A nodeList.Count == 0 is okay and indicates that there is no parent.
            if (nodelist.Count == 1) 
                parentPath = nodelist[0].Value;
            else if (nodelist.Count != 0)
                throw new ViridianException("There can not be multiple parents!", new FormatException());

            if (type == HardDiskType.Differencing && string.IsNullOrEmpty(parentPath))
                throw new ViridianException("Parent path must be set if this is a differencing disk!", new FormatException());

            // MaxInternalSize
            nodelist = doc.SelectNodes(@"//PROPERTY[@NAME = 'MaxInternalSize']/VALUE/child::text()");

            if (nodelist == null) 
                return null;

            if (nodelist.Count != 1)
                throw new ViridianException("", new FormatException());

            var maxInternalSize = long.Parse(nodelist[0].Value, CultureInfo.InvariantCulture);

            // BlockSize
            nodelist = doc.SelectNodes(@"//PROPERTY[@NAME = 'BlockSize']/VALUE/child::text()");

            if (nodelist == null)
                return null;

            if (nodelist.Count != 1)
                throw new ViridianException("", new FormatException());

            var blockSize = int.Parse(nodelist[0].Value, CultureInfo.InvariantCulture);

            // LogicalSectorSize
            nodelist = doc.SelectNodes(@"//PROPERTY[@NAME = 'LogicalSectorSize']/VALUE/child::text()");

            if (nodelist == null) 
                return null;

            if (nodelist.Count != 1) 
                throw new ViridianException("", new FormatException());

            var logicalSectorSize = int.Parse(nodelist[0].Value, CultureInfo.InvariantCulture);

            // PhysicalSectorSize
            nodelist = doc.SelectNodes(@"//PROPERTY[@NAME = 'PhysicalSectorSize']/VALUE/child::text()");

            if (nodelist == null) 
                return null;

            if (nodelist.Count != 1) 
                throw new ViridianException("", new FormatException());

            var physicalSectorSize = int.Parse(nodelist[0].Value, CultureInfo.InvariantCulture);

            return new DiskSettings(type, format, path, parentPath, maxInternalSize, blockSize, logicalSectorSize, physicalSectorSize);
        }
    }
}
