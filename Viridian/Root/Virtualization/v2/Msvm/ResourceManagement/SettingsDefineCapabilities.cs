using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.ResourceManagement
{
    public class SettingsDefineCapabilities : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(SettingsDefineCapabilities)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public SettingsDefineCapabilities() : base(ClassName) { }

        public SettingsDefineCapabilities(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public SettingsDefineCapabilities(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public SettingsDefineCapabilities(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public SettingsDefineCapabilities(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public SettingsDefineCapabilities(ManagementPath path) : base(path, ClassName) { }

        public SettingsDefineCapabilities(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public SettingsDefineCapabilities(ManagementObject theObject) : base(theObject, ClassName) { }

        public SettingsDefineCapabilities(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public ManagementPath GroupComponent
        {
            get
            {
                if (LateBoundObject[nameof(GroupComponent)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(GroupComponent)].ToString());
                }
                return null;
            }
        }

        public ManagementPath PartComponent
        {
            get
            {
                if (LateBoundObject[nameof(PartComponent)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(PartComponent)].ToString());
                }
                return null;
            }
        }

        public ushort PropertyPolicy
        {
            get
            {
                if (LateBoundObject[nameof(PropertyPolicy)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(PropertyPolicy)];
            }
        }

        /*
         * Identifies the support statement.
         */
        public ushort SupportStatement
        {
            get
            {
                if (LateBoundObject[nameof(SupportStatement)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(SupportStatement)];
            }
        }

        public ushort ValueRange
        {
            get
            {
                if (LateBoundObject[nameof(ValueRange)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ValueRange)];
            }
        }

        public ushort ValueRole
        {
            get
            {
                if (LateBoundObject[nameof(ValueRole)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ValueRole)];
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<SettingsDefineCapabilities> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineCapabilities(mo)).ToList();

        public new static List<SettingsDefineCapabilities> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineCapabilities(mo)).ToList();

        public static List<SettingsDefineCapabilities> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineCapabilities(mo)).ToList();

        public static List<SettingsDefineCapabilities> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineCapabilities(mo)).ToList();

        public static List<SettingsDefineCapabilities> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineCapabilities(mo)).ToList();

        public static List<SettingsDefineCapabilities> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineCapabilities(mo)).ToList();

        public static List<SettingsDefineCapabilities> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineCapabilities(mo)).ToList();

        public static List<SettingsDefineCapabilities> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new SettingsDefineCapabilities(mo)).ToList();

        public static SettingsDefineCapabilities CreateInstance() => new SettingsDefineCapabilities(CreateInstance(ClassName));
    }
}
