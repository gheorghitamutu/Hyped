using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Integration
{
    public class CopyFileToGuestSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(CopyFileToGuestSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public CopyFileToGuestSettingData() : base(ClassName) { }

        public CopyFileToGuestSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public CopyFileToGuestSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public CopyFileToGuestSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public CopyFileToGuestSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public CopyFileToGuestSettingData(ManagementPath path) : base(path, ClassName) { }

        public CopyFileToGuestSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public CopyFileToGuestSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public CopyFileToGuestSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }
        
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
         * Specifies whether missing directories in the destination file's path must be created before copying the file.
         */
        public bool CreateFullPath
        {
            get
            {
                if (LateBoundObject[nameof(CreateFullPath)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(CreateFullPath)];
            }
            set
            {
                LateBoundObject[nameof(CreateFullPath)] = value;
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
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The complete path of the destination file to be copied, that is accessibleto the guest.
         * It can contain environment variables which areexpanded by the guest.
         *  If the path specified is an existing directory in the guest, the destination file is created in this directory.
         *  In this case, the filename from SourcePath is used as the destination file name.
         */
        public string DestinationPath
        {
            get
            {
                return (string)LateBoundObject[nameof(DestinationPath)];
            }
            set
            {
                LateBoundObject[nameof(DestinationPath)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
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
         * Specifies whether an existing destination file must be overwritten.
         */
        public bool OverwriteExisting
        {
            get
            {
                if (LateBoundObject[nameof(OverwriteExisting)] == null)
                {
                    return System.Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(OverwriteExisting)];
            }
            set
            {
                LateBoundObject[nameof(OverwriteExisting)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The complete path of the source file to be copied, that is accessibleto the Hyper-V host.
         * It can contain environment variables which areexpanded by the Hyper-V host.
         * 
         */
        public string SourcePath
        {
            get
            {
                return (string)LateBoundObject[nameof(SourcePath)];
            }
            set
            {
                LateBoundObject[nameof(SourcePath)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
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

        private void ResetCreateFullPath()
        {
            LateBoundObject[nameof(CreateFullPath)] = null;
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

        private void ResetDestinationPath()
        {
            LateBoundObject[nameof(DestinationPath)] = null;
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

        private void ResetOverwriteExisting()
        {
            LateBoundObject[nameof(OverwriteExisting)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetSourcePath()
        {
            LateBoundObject[nameof(SourcePath)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<CopyFileToGuestSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestSettingData(mo)).ToList();

        public new static List<CopyFileToGuestSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestSettingData(mo)).ToList();

        public static List<CopyFileToGuestSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestSettingData(mo)).ToList();

        public static List<CopyFileToGuestSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestSettingData(mo)).ToList();

        public static List<CopyFileToGuestSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestSettingData(mo)).ToList();

        public static List<CopyFileToGuestSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestSettingData(mo)).ToList();

        public static List<CopyFileToGuestSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestSettingData(mo)).ToList();

        public static List<CopyFileToGuestSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new CopyFileToGuestSettingData(mo)).ToList();

        public static CopyFileToGuestSettingData CreateInstance() => new CopyFileToGuestSettingData(CreateInstance(ClassName));

    }
}
