using System.Management;
using System.Collections.Generic;
using System.Linq;

namespace Viridian.Msvm.VirtualSystemManagement
{
    public class SettingsDefineState : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(SettingsDefineState)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public SettingsDefineState() : base(ClassName) { }

        public SettingsDefineState(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public SettingsDefineState(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public SettingsDefineState(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public SettingsDefineState(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public SettingsDefineState(ManagementPath path) : base(path, ClassName) { }

        public SettingsDefineState(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public SettingsDefineState(ManagementObject theObject) : base(theObject, ClassName) { }

        public SettingsDefineState(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public ManagementPath ManagedElement
        {
            get
            {
                if (LateBoundObject[nameof(ManagedElement)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(ManagedElement)].ToString());
                }
                return null;
            }
        }

        public ManagementPath SettingData
        {
            get
            {
                if (LateBoundObject[nameof(SettingData)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(SettingData)].ToString());
                }
                return null;
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<SettingsDefineState> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineState(mo)).ToList();

        public new static List<SettingsDefineState> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineState(mo)).ToList();

        public static List<SettingsDefineState> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineState(mo)).ToList();

        public static List<SettingsDefineState> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineState(mo)).ToList();

        public static List<SettingsDefineState> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineState(mo)).ToList();

        public static List<SettingsDefineState> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineState(mo)).ToList();

        public static List<SettingsDefineState> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineState(mo)).ToList();

        public static List<SettingsDefineState> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineState(mo)).ToList();

        public static SettingsDefineState CreateInstance() => new SettingsDefineState(CreateInstance(ClassName));
    }
}
