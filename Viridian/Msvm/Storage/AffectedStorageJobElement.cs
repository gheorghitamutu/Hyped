using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Msvm.Storage
{
    public class AffectedStorageJobElement : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(AffectedStorageJobElement)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public AffectedStorageJobElement() : base(ClassName) { }

        public AffectedStorageJobElement(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public AffectedStorageJobElement(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public AffectedStorageJobElement(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public AffectedStorageJobElement(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public AffectedStorageJobElement(ManagementPath path) : base(path, ClassName) { }

        public AffectedStorageJobElement(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public AffectedStorageJobElement(ManagementObject theObject) : base(theObject, ClassName) { }

        public AffectedStorageJobElement(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public ManagementPath AffectedElement
        {
            get
            {
                if (LateBoundObject[nameof(AffectedElement)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(AffectedElement)].ToString());
                }
                return null;
            }
        }

        /*
         * The job that is affecting the affected element. This property is inherited from CIM_AffectedJobElement.
         */
        public ManagementPath AffectingElement
        {
            get
            {
                if (LateBoundObject[nameof(AffectingElement)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(AffectingElement)].ToString());
                }
                return null;
            }
        }

        public ushort[] ElementEffects => (ushort[])LateBoundObject[nameof(ElementEffects)];

        public string[] OtherElementEffectsDescriptions => (string[])LateBoundObject[nameof(OtherElementEffectsDescriptions)];

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<AffectedStorageJobElement> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new AffectedStorageJobElement(mo)).ToList();

        public new static List<AffectedStorageJobElement> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new AffectedStorageJobElement(mo)).ToList();

        public static List<AffectedStorageJobElement> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new AffectedStorageJobElement(mo)).ToList();

        public static List<AffectedStorageJobElement> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new AffectedStorageJobElement(mo)).ToList();

        public static List<AffectedStorageJobElement> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new AffectedStorageJobElement(mo)).ToList();

        public static List<AffectedStorageJobElement> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new AffectedStorageJobElement(mo)).ToList();

        public static List<AffectedStorageJobElement> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new AffectedStorageJobElement(mo)).ToList();

        public static List<AffectedStorageJobElement> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new AffectedStorageJobElement(mo)).ToList();

        public static AffectedStorageJobElement CreateInstance() => new AffectedStorageJobElement(CreateInstance(ClassName));

    }
}
