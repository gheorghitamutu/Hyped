using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Viridian.Msvm.VirtualSystemManagement
{
    public class OwningJobElement : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(OwningJobElement)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public OwningJobElement() : base(ClassName) { }

        public OwningJobElement(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public OwningJobElement(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public OwningJobElement(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public OwningJobElement(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public OwningJobElement(ManagementPath path) : base(path, ClassName) { }

        public OwningJobElement(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public OwningJobElement(ManagementObject theObject) : base(theObject, ClassName) { }

        public OwningJobElement(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public ManagementPath OwnedElement
        {
            get
            {
                if (LateBoundObject[nameof(OwnedElement)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(OwnedElement)].ToString());
                }
                return null;
            }
        }

        public ManagementPath OwningElement
        {
            get
            {
                if (LateBoundObject[nameof(OwningElement)] != null)
                {
                    return new ManagementPath(LateBoundObject[nameof(OwningElement)].ToString());
                }
                return null;
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<OwningJobElement> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new OwningJobElement(mo)).ToList();

        public new static List<OwningJobElement> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new OwningJobElement(mo)).ToList();

        public static List<OwningJobElement> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new OwningJobElement(mo)).ToList();

        public static List<OwningJobElement> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new OwningJobElement(mo)).ToList();

        public static List<OwningJobElement> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new OwningJobElement(mo)).ToList();

        public static List<OwningJobElement> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new OwningJobElement(mo)).ToList();

        public static List<OwningJobElement> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new OwningJobElement(mo)).ToList();

        public static List<OwningJobElement> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new OwningJobElement(mo)).ToList();

        public static OwningJobElement CreateInstance() => new OwningJobElement(CreateInstance(ClassName));

    }
}
