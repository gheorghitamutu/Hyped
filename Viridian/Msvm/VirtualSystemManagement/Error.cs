using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Viridian.Msvm.VirtualSystemManagement
{
    public class Error : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(Error)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public Error() : base(ClassName) { }

        public Error(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public Error(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public Error(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public Error(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public Error(ManagementPath path) : base(path, ClassName) { }

        public Error(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public Error(ManagementObject theObject) : base(theObject, ClassName) { }

        public Error(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public uint CIMStatusCode
        {
            get
            {
                if (LateBoundObject[nameof(CIMStatusCode)] == null)
                {
                    return Convert.ToUInt32(0);
                }
                return (uint)LateBoundObject[nameof(CIMStatusCode)];
            }
        }

        public string CIMStatusCodeDescription => (string)LateBoundObject[nameof(CIMStatusCodeDescription)];

        public string ErrorSource => (string)LateBoundObject[nameof(ErrorSource)];

        public ushort ErrorSourceFormat
        {
            get
            {
                if (LateBoundObject[nameof(ErrorSourceFormat)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ErrorSourceFormat)];
            }
        }

        public ushort ErrorType
        {
            get
            {
                if (LateBoundObject[nameof(ErrorType)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ErrorType)];
            }
        }

        public string Message => (string)LateBoundObject[nameof(Message)];

        public string[] MessageArguments => (string[])LateBoundObject[nameof(MessageArguments)];

        public string MessageID => (string)LateBoundObject[nameof(MessageID)];

        public string OtherErrorSourceFormat => (string)LateBoundObject[nameof(OtherErrorSourceFormat)];

        public string OtherErrorType => (string)LateBoundObject[nameof(OtherErrorType)];

        public string OwningEntity => (string)LateBoundObject[nameof(OwningEntity)];

        public ushort PerceivedSeverity
        {
            get
            {
                if (LateBoundObject[nameof(PerceivedSeverity)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(PerceivedSeverity)];
            }
        }

        public ushort ProbableCause
        {
            get
            {
                if (LateBoundObject[nameof(ProbableCause)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(ProbableCause)];
            }
        }

        public string ProbableCauseDescription => (string)LateBoundObject[nameof(ProbableCauseDescription)];

        public string[] RecommendedActions => (string[])LateBoundObject[nameof(RecommendedActions)];

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<Error> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new Error(mo)).ToList();

        public new static List<Error> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new Error(mo)).ToList();

        public static List<Error> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Error(mo)).ToList();

        public static List<Error> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Error(mo)).ToList();

        public static List<Error> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new Error(mo)).ToList();

        public static List<Error> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new Error(mo)).ToList();

        public static List<Error> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Error(mo)).ToList();

        public static List<Error> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new Error(mo)).ToList();

        public static Error CreateInstance() => new Error(CreateInstance(ClassName));

    }
}
