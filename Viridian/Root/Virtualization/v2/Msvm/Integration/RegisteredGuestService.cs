using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.Root.Virtualization.v2.Msvm.Integration
{
    public class RegisteredGuestService : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(RegisteredGuestService)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public RegisteredGuestService() : base(ClassName) { }

        public RegisteredGuestService(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public RegisteredGuestService(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public RegisteredGuestService(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public RegisteredGuestService(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public RegisteredGuestService(ManagementPath path) : base(path, ClassName) { }

        public RegisteredGuestService(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public RegisteredGuestService(ManagementObject theObject) : base(theObject, ClassName) { }

        public RegisteredGuestService(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * The guest service interface component.
         */
        public ManagementPath Antecedent
        {
            get
            {
                if (LateBoundObject[nameof(Antecedent)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(Antecedent)].ToString());
                }
                return null;
            }
        }

        /*
         * The registered guest service.
         */
        public ManagementPath Dependent
        {
            get
            {
                if (LateBoundObject[nameof(Dependent)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(Dependent)].ToString());
                }
                return null;
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<RegisteredGuestService> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new RegisteredGuestService(mo)).ToList();

        public new static List<RegisteredGuestService> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new RegisteredGuestService(mo)).ToList();

        public static List<RegisteredGuestService> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new RegisteredGuestService(mo)).ToList();

        public static List<RegisteredGuestService> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new RegisteredGuestService(mo)).ToList();

        public static List<RegisteredGuestService> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new RegisteredGuestService(mo)).ToList();

        public static List<RegisteredGuestService> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new RegisteredGuestService(mo)).ToList();

        public static List<RegisteredGuestService> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new RegisteredGuestService(mo)).ToList();

        public static List<RegisteredGuestService> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new RegisteredGuestService(mo)).ToList();

        public static RegisteredGuestService CreateInstance() => new RegisteredGuestService(CreateInstance(ClassName));
    }
}
