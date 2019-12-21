using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.Storage
{
    public class MountedStorageImage : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(MountedStorageImage)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public MountedStorageImage() : base(ClassName) { }

        public MountedStorageImage(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MountedStorageImage(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public MountedStorageImage(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public MountedStorageImage(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public MountedStorageImage(ManagementPath path) : base(path, ClassName) { }

        public MountedStorageImage(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public MountedStorageImage(ManagementObject theObject) : base(theObject, ClassName) { }

        public MountedStorageImage(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * The access under which the storage image is mounted.
         */
        public AccessValues Access
        {
            get
            {
                if (LateBoundObject[nameof(Access)] == null)
                {
                    return (AccessValues)Convert.ToInt32(0);
                }
                return (AccessValues)Convert.ToInt32(LateBoundObject[nameof(Access)]);
            }
        }

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

        /*
         * The SCSI address LUN ID.
         */
        public byte Lun
        {
            get
            {
                if (LateBoundObject[nameof(Lun)] == null)
                {
                    return Convert.ToByte(0);
                }
                return (byte)LateBoundObject[nameof(Lun)];
            }
        }

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

        /*
         * The SCSI address path ID.
         */
        public byte PathId
        {
            get
            {
                if (LateBoundObject[nameof(PathId)] == null)
                {
                    return Convert.ToByte(0);
                }
                return (byte)LateBoundObject[nameof(PathId)];
            }
        }

        /*
         * The PNP Device Path.
         */
        public string PnpDevicePath => (string)LateBoundObject[nameof(PnpDevicePath)];

        /*
         * The SCSI address port number.
         */
        public byte PortNumber
        {
            get
            {
                if (LateBoundObject[nameof(PortNumber)] == null)
                {
                    return Convert.ToByte(0);
                }
                return (byte)LateBoundObject[nameof(PortNumber)];
            }
        }

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

        public string Status => (string)LateBoundObject[nameof(Status)];

        public string[] StatusDescriptions => (string[])LateBoundObject[nameof(StatusDescriptions)];

        /*
         * The SCSI address target ID.
         */
        public byte TargetId
        {
            get
            {
                if (LateBoundObject[nameof(TargetId)] == null)
                {
                    return Convert.ToByte(0);
                }
                return (byte)LateBoundObject[nameof(TargetId)];
            }
        }

        /*
         * The type of storage image mounted.
         */
        public TypeValues Type
        {
            get
            {
                if (LateBoundObject[nameof(Type)] == null)
                {
                    return (TypeValues)Convert.ToInt32(2);
                }
                return (TypeValues)Convert.ToInt32(LateBoundObject[nameof(Type)]);
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<MountedStorageImage> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new MountedStorageImage(mo)).ToList();

        public new static List<MountedStorageImage> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MountedStorageImage(mo)).ToList();

        public static List<MountedStorageImage> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MountedStorageImage(mo)).ToList();

        public static List<MountedStorageImage> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MountedStorageImage(mo)).ToList();

        public static List<MountedStorageImage> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new MountedStorageImage(mo)).ToList();

        public static List<MountedStorageImage> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new MountedStorageImage(mo)).ToList();

        public static List<MountedStorageImage> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MountedStorageImage(mo)).ToList();

        public static List<MountedStorageImage> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new MountedStorageImage(mo)).ToList();

        public static MountedStorageImage CreateInstance() => new MountedStorageImage(CreateInstance(ClassName));

        public uint DetachVirtualHardDisk()
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DetachVirtualHardDisk", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public enum AccessValues
        {
            Read_only = 1,
            Read_Write = 2,
            NULL_ENUM_VALUE = 0,
        }

        public enum TypeValues
        {
            Virtual_Hard_Disk = 0,
            ISO_Image = 1,
            NULL_ENUM_VALUE = 2,
        }
    }
}
