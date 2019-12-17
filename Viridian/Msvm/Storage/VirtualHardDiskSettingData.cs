using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.Storage
{
    public class VirtualHardDiskSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(VirtualHardDiskSettingData)}";

        public VirtualHardDiskSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualHardDiskSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualHardDiskSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public VirtualHardDiskSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public VirtualHardDiskSettingData(ManagementPath path) : base(path, ClassName) { }

        public VirtualHardDiskSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public VirtualHardDiskSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public VirtualHardDiskSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * The block size used by the virtual hard disk. 
         */
        public uint BlockSize
        {
            get
            {
                if (LateBoundObject[nameof(BlockSize)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(BlockSize)];
            }
            set
            {
                LateBoundObject[nameof(BlockSize)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string Caption
        {
            get
            {
                return (string)LateBoundObject[nameof(Caption)];
            }
            set
            {
                LateBoundObject[nameof(Caption)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Specifies the desired alignment, in bytes, for the data payload of the virtual disk.
         */
        public ulong DataAlignment
        {
            get
            {
                if (LateBoundObject[nameof(DataAlignment)] == null)
                {
                    return System.Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(DataAlignment)];
            }
            set
            {
                LateBoundObject[nameof(DataAlignment)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string Description
        {
            get
            {
                return (string)LateBoundObject[nameof(Description)];
            }
            set
            {
                LateBoundObject[nameof(Description)] = value;
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string ElementName
        {
            get
            {
                return (string)LateBoundObject[nameof(ElementName)];
            }
            set
            {
                LateBoundObject[nameof(ElementName)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The format for the virtual hard disk.
         */
        public FormatValues Format
        {
            get
            {
                if (LateBoundObject[nameof(Format)] == null)
                {
                    return (FormatValues)System.Convert.ToInt32(0);
                }
                return (FormatValues)System.Convert.ToInt32(LateBoundObject[nameof(Format)]);
            }
            set
            {
                if (FormatValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(Format)] = null;
                }
                else
                {
                    LateBoundObject[nameof(Format)] = value;
                }
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        public string InstanceID
        {
            get
            {
                return (string)LateBoundObject[nameof(InstanceID)];
            }
            set
            {
                LateBoundObject[nameof(InstanceID)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Specifies whether the virtual disk can be used as the backing store for a persistent memory device.
         */
        public bool IsPmemCompatible
        {
            get
            {
                if (LateBoundObject[nameof(IsPmemCompatible)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(IsPmemCompatible)];
            }
            set
            {
                LateBoundObject[nameof(IsPmemCompatible)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The logical sector size used by the virtual hard disk.
         */
        public uint LogicalSectorSize
        {
            get
            {
                if (LateBoundObject[nameof(LogicalSectorSize)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(LogicalSectorSize)];
            }
            set
            {
                LateBoundObject[nameof(LogicalSectorSize)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The maximum size of the virtual hard disk as viewable by the virtual machine, in bytes.
         * The specified size will be rounded up to the next largest multiple of the sector size.
         */
        public ulong MaxInternalSize
        {
            get
            {
                if (LateBoundObject[nameof(MaxInternalSize)] == null)
                {
                    return System.Convert.ToUInt64(0);
                }
                return (ulong)LateBoundObject[nameof(MaxInternalSize)];
            }
            set
            {
                LateBoundObject[nameof(MaxInternalSize)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The GUID used to uniquely identify the parent of the virtual hard disk.
         * If the virtual hard disk does not have a parent, then this field is empty.
         */
        public string ParentIdentifier
        {
            get
            {
                return (string)LateBoundObject[nameof(ParentIdentifier)];
            }
            set
            {
                LateBoundObject[nameof(ParentIdentifier)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The parent of the virtual hard disk. If the virtual hard disk does not have a parent, then this field is empty.
         */
        public string ParentPath
        {
            get
            {
                return (string)LateBoundObject[nameof(ParentPath)];
            }
            set
            {
                LateBoundObject[nameof(ParentPath)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The timestamp of the parent of the virtual hard disk.
         * If the virtual hard disk does not have a parent, then this field is empty.
         */
        public System.DateTime ParentTimestamp
        {
            get
            {
                if (LateBoundObject[nameof(ParentTimestamp)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(ParentTimestamp)]);
                }
                else
                {
                    return System.DateTime.MinValue;
                }
            }
            set
            {
                LateBoundObject[nameof(ParentTimestamp)] = ToDmtfDateTime((System.DateTime)value);
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The path of the virtual hard disk.
         */
        public string Path0
        {
            get
            {
                return (string)LateBoundObject[nameof(Path)];
            }
            set
            {
                LateBoundObject[nameof(Path)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The physical sector size used by the virtual hard disk/
         */
        public uint PhysicalSectorSize
        {
            get
            {
                if (LateBoundObject[nameof(PhysicalSectorSize)] == null)
                {
                    return System.Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(PhysicalSectorSize)];
            }
            set
            {
                LateBoundObject[nameof(PhysicalSectorSize)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The persistent memory address abstraction method to be used with this virtual disk.
         */
        public PmemAddressAbstractionTypeValues PmemAddressAbstractionType
        {
            get
            {
                if (LateBoundObject[nameof(PmemAddressAbstractionType)] == null)
                {
                    return (PmemAddressAbstractionTypeValues)System.Convert.ToInt32(65536);
                }
                return (PmemAddressAbstractionTypeValues)System.Convert.ToInt32(LateBoundObject[nameof(PmemAddressAbstractionType)]);
            }
            set
            {
                if (PmemAddressAbstractionTypeValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(PmemAddressAbstractionType)] = null;
                }
                else
                {
                    LateBoundObject[nameof(PmemAddressAbstractionType)] = value;
                }
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The type of virtual hard disk.
         */
        public TypeValues Type
        {
            get
            {
                if (LateBoundObject[nameof(Type)] == null)
                {
                    return (TypeValues)System.Convert.ToInt32(0);
                }
                return (TypeValues)System.Convert.ToInt32(LateBoundObject[nameof(Type)]);
            }
            set
            {
                if (TypeValues.NULL_ENUM_VALUE == value)
                {
                    LateBoundObject[nameof(Type)] = null;
                }
                else
                {
                    LateBoundObject[nameof(Type)] = value;
                }
                if ((IsEmbedded == false)
                            && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The GUID used to uniquely identify the virtual disk.
         */
        public string VirtualDiskId
        {
            get
            {
                return (string)LateBoundObject[nameof(VirtualDiskId)];
            }
            set
            {
                LateBoundObject[nameof(VirtualDiskId)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        private void ResetBlockSize()
        {
            LateBoundObject[nameof(BlockSize)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetCaption()
        {
            LateBoundObject[nameof(Caption)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetDataAlignment()
        {
            LateBoundObject[nameof(DataAlignment)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetDescription()
        {
            LateBoundObject[nameof(Description)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetElementName()
        {
            LateBoundObject[nameof(ElementName)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetFormat()
        {
            LateBoundObject[nameof(Format)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetIsPmemCompatible()
        {
            LateBoundObject[nameof(IsPmemCompatible)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetLogicalSectorSize()
        {
            LateBoundObject[nameof(LogicalSectorSize)] = null;
            if ((IsEmbedded == false)&& (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetMaxInternalSize()
        {
            LateBoundObject[nameof(MaxInternalSize)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetParentIdentifier()
        {
            LateBoundObject[nameof(ParentIdentifier)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetParentPath()
        {
            LateBoundObject[nameof(ParentPath)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetParentTimestamp()
        {
            LateBoundObject[nameof(ParentTimestamp)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetPath0()
        {
            LateBoundObject[nameof(Path)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }
        private void ResetPhysicalSectorSize()
        {
            LateBoundObject[nameof(PhysicalSectorSize)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetPmemAddressAbstractionType()
        {
            LateBoundObject[nameof(PmemAddressAbstractionType)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetType()
        {
            LateBoundObject[nameof(Type)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetVirtualDiskId()
        {
            LateBoundObject[nameof(VirtualDiskId)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<VirtualHardDiskSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualHardDiskSettingData(mo)).ToList();

        public new static List<VirtualHardDiskSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualHardDiskSettingData(mo)).ToList();

        public static List<VirtualHardDiskSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualHardDiskSettingData(mo)).ToList();

        public static List<VirtualHardDiskSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualHardDiskSettingData(mo)).ToList();

        public static List<VirtualHardDiskSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualHardDiskSettingData(mo)).ToList();

        public static List<VirtualHardDiskSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualHardDiskSettingData(mo)).ToList();

        public static List<VirtualHardDiskSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualHardDiskSettingData(mo)).ToList();

        public static List<VirtualHardDiskSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualHardDiskSettingData(mo)).ToList();

        public static VirtualHardDiskSettingData CreateInstance() => new VirtualHardDiskSettingData(CreateInstance(ClassName));

        public enum FormatValues
        {
            VHD = 2,
            VHDX = 3,
            VHDSet = 4,
            NULL_ENUM_VALUE = 0,
        }

        public enum PmemAddressAbstractionTypeValues
        {
            None = 0,
            BTT = 1,
            Unknown0 = 65535,
            NULL_ENUM_VALUE = 65536,
        }

        public enum TypeValues
        {
            Fixed = 2,
            Dynamic = 3,
            Differencing = 4,
            NULL_ENUM_VALUE = 0,
        }
    }
}
