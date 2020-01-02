using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.VirtualSystem
{
    public class BootSourceSettingData : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(BootSourceSettingData)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public BootSourceSettingData() : base(ClassName) { }

        public BootSourceSettingData(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public BootSourceSettingData(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public BootSourceSettingData(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public BootSourceSettingData(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public BootSourceSettingData(ManagementPath path) : base(path, ClassName) { }

        public BootSourceSettingData(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public BootSourceSettingData(ManagementObject theObject) : base(theObject, ClassName) { }

        public BootSourceSettingData(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * The description of the boot source provided by the firmware
         */
        public string BootSourceDescription => (string)LateBoundObject[nameof(BootSourceDescription)];

        /*
         * An enumeration value that specifies the type of the boot source.
         */
        public BootSourceTypeValues BootSourceType
        {
            get
            {
                if (LateBoundObject[nameof(BootSourceType)] == null)
                {
                    return (BootSourceTypeValues)System.Convert.ToInt32(4);
                }
                return (BootSourceTypeValues)System.Convert.ToInt32(LateBoundObject[nameof(BootSourceType)]);
            }
        }

        public string Caption => (string)LateBoundObject[nameof(Caption)];

        public string Description => (string)LateBoundObject[nameof(Description)];

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        /*
         * The native path used by the firmware to describe this device.
         */
        public string FirmwareDevicePath => (string)LateBoundObject[nameof(FirmwareDevicePath)];

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        /*
         * Optional data provided by the firmware.
         */
        public byte[] OptionalData => (byte[])LateBoundObject[nameof(OptionalData)];

        /*
         * The other location information, if any, used by firmware to further uniquely identify this boot source.
         */
        public string OtherLocation => (string)LateBoundObject[nameof(OtherLocation)];

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<BootSourceSettingData> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceSettingData(mo)).ToList();

        public new static List<BootSourceSettingData> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceSettingData(mo)).ToList();

        public static List<BootSourceSettingData> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceSettingData(mo)).ToList();

        public static List<BootSourceSettingData> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceSettingData(mo)).ToList();

        public static List<BootSourceSettingData> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceSettingData(mo)).ToList();

        public static List<BootSourceSettingData> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceSettingData(mo)).ToList();

        public static List<BootSourceSettingData> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceSettingData(mo)).ToList();

        public static List<BootSourceSettingData> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new BootSourceSettingData(mo)).ToList();

        public static BootSourceSettingData CreateInstance() => new BootSourceSettingData(CreateInstance(ClassName));

        public enum BootSourceTypeValues
        {
            Unknown0 = 0,
            Drive = 1,
            Network = 2,
            File = 3,
            NULL_ENUM_VALUE = 4,
        }
    }
}
