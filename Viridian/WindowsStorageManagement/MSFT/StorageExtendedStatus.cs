using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Viridian.WindowsStorageManagement.MSFT
{
    public class StorageExtendedStatus : MsftBase
    {
        public static string ClassName => $"MSFT_{nameof(StorageExtendedStatus)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public StorageExtendedStatus() : base(ClassName) { }

        public StorageExtendedStatus(string keyObjectId) : base(keyObjectId, ClassName) { }

        public StorageExtendedStatus(ManagementScope mgmtScope, string keyObjectId) : base(mgmtScope, keyObjectId, ClassName) { }

        public StorageExtendedStatus(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public StorageExtendedStatus(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public StorageExtendedStatus(ManagementPath path) : base(path, ClassName) { }

        public StorageExtendedStatus(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public StorageExtendedStatus(ManagementObject theObject) : base(theObject, ClassName) { }

        public StorageExtendedStatus(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        /*
         * The CIM status code that characterizes this instance.
         * This property defines the status codes that MAY be return by a conforming CIM Server or Listener.
         * Note that not all status codes are valid for each operation.
         * The specification for each operation SHOULD define the status codes that may be returned by that operation.
         * The following values for CIM status code are defined: 
         * 1 - CIM_ERR_FAILED. A general error occurred that is not covered by a more specific error code.
         * 2 - CIM_ERR_ACCESS_DENIED. Access to a CIM resource was not available to the client.
         * 3 - CIM_ERR_INVALID_NAMESPACE. The target namespace does not exist.
         * 4 - CIM_ERR_INVALID_PARAMETER. One or more parameter values passed to the method were invalid.
         * 5 - CIM_ERR_INVALID_CLASS. The specified Class does not exist.
         * 6 - CIM_ERR_NOT_FOUND. The requested object could not be found.
         * 7 - CIM_ERR_NOT_SUPPORTED. The requested operation is not supported. 
         * 8 - CIM_ERR_CLASS_HAS_CHILDREN. Operation cannot be carried out on this class since it has instances.
         * 9 - CIM_ERR_CLASS_HAS_INSTANCES. Operation cannot be carried out on this class since it has instances.
         * 10 - CIM_ERR_INVALID_SUPERCLASS. Operation cannot be carried out since the specified superclass does not exist.
         * 11 - CIM_ERR_ALREADY_EXISTS. Operation cannot be carried out because an object already exists.
         * 12 - CIM_ERR_NO_SUCH_PROPERTY. The specified Property does not exist.
         * 13 - CIM_ERR_TYPE_MISMATCH. The value supplied is incompatible with the type.
         * 14 - CIM_ERR_QUERY_LANGUAGE_NOT_SUPPORTED. The query language is not recognized or supported.
         * 15 - CIM_ERR_INVALID_QUERY. The query is not valid for the specified query language.
         * 16 - CIM_ERR_METHOD_NOT_AVAILABLE. The extrinsic Method could not be executed.
         * 17 - CIM_ERR_METHOD_NOT_FOUND. The specified extrinsic Method does not exist.
         * 18 - CIM_ERR_UNEXPECTED_RESPONSE. The returned response to the asynchronous operation was not expected.
         * 19 - CIM_ERR_INVALID_RESPONSE_DESTINATION. The specified destination for the asynchronous response is not valid.
         * 20 - CIM_ERR_NAMESPACE_NOT_EMPTY. The specified Namespace is not empty.
         * 21 - CIM_ERR_INVALID_ENUMERATION_CONTEXT. The enumeration context supplied is not valid.
         * 22 - CIM_ERR_INVALID_OPERATION_TIMEOUT. The specified Namespace is not empty.
         * 23 - CIM_ERR_PULL_HAS_BEEN_ABANDONED. The specified Namespace is not empty.
         * 24 - CIM_ERR_PULL_CANNOT_BE_ABANDONED. The attempt to abandon a pull operation has failed.
         * 25 - CIM_ERR_FILTERED_ENUMERATION_NOT_SUPPORTED. Filtered Enumeratrions are not supported.
         * 26 - CIM_ERR_CONTINUATION_ON_ERROR_NOT_SUPPORTED. Continue on error is not supported.
         * 27 - CIM_ERR_SERVER_LIMITS_EXCEEDED. The WBEM Server limits have been exceeded (e.g. memory, connections, ...).
         * 28 - CIM_ERR_SERVER_IS_SHUTTING_DOWN. The WBEM Server is shutting down.
         * 29 - CIM_ERR_QUERY_FEATURE_NOT_SUPPORTED. The specified Query Feature is not supported.
         */
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
            set
            {
                LateBoundObject[nameof(CIMStatusCode)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * A free-form string containing a human-readable description of CIMStatusCode.
         * This description MAY extend, but MUST be consistent with, the definition of CIMStatusCode.
         */
        public string CIMStatusCodeDescription
        {
            get
            {
                return (string)LateBoundObject[nameof(CIMStatusCodeDescription)];
            }
            set
            {
                LateBoundObject[nameof(CIMStatusCodeDescription)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The identifying information of the entity (i.e., the instance) generating the error.
         * If this entity is modeled in the CIM Schema, this property contains the path of the instance encoded as a string parameter.
         * If not modeled, the property contains some identifying string that names the entity that generated the error.
         * The path or identifying string is formatted per the ErrorSourceFormat property.
         */
        public string ErrorSource
        {
            get
            {
                return (string)LateBoundObject[nameof(ErrorSource)];
            }
            set
            {
                LateBoundObject[nameof(ErrorSource)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The format of the ErrorSource property is interpretable based on the value of this property.
         * Values are defined as: 
         * 0 - Unknown. The format is unknown or not meaningfully interpretable by a CIM client application.
         * 1 - Other. The format is defined by the value of the OtherErrorSourceFormat property.
         * 2 - CIMObjectPath. A CIM Object Path as defined in the CIM Infrastructure specification. Note: CIM 2.5 and earlier used the term object names.
         */
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
            set
            {
                LateBoundObject[nameof(ErrorSourceFormat)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * Primary classification of the error.
         * The following values are defined: 
         * 2 - Communications Error. Errors of this type are principally associated with the procedures and/or processes required to convey information from one point to another. 
         * 3 - Quality of Service Error. Errors of this type are principally associated with failures that result in reduced functionality or performance. 
         * 4 - Software Error. Error of this type are principally associated with a software or processing fault. 
         * 5 - Hardware Error. Errors of this type are principally associated with an equipment or hardware failure. 
         * 6 - Environmental Error. Errors of this type are principally associated with a failure condition relating the to facility, or other environmental considerations. 
         * 7 - Security Error. Errors of this type are associated with security violations, detection of viruses, and similar issues. 
         * 8 - Oversubscription Error. Errors of this type are principally associated with the failure to allocate sufficient resources to complete the operation. 
         * 9 - Unavailable Resource Error. Errors of this type are principally associated with the failure to access a required resource. 
         * 10 -Unsupported Operation Error. Errors of this type are principally associated with requests that are not supported.
         */
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
            set
            {
                LateBoundObject[nameof(ErrorType)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * The formatted message.
         * This message is constructed by combining some or all of the dynamic elements specified in the MessageArguments property with the static elements
         * uniquely identified by the MessageID in a message registry or other catalog associated with the OwningEntity.
         */
        public string Message
        {
            get
            {
                return (string)LateBoundObject[nameof(Message)];
            }
            set
            {
                LateBoundObject[nameof(Message)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * An array containing the dynamic content of the message.
         */
        public string[] MessageArguments
        {
            get
            {
                return (string[])LateBoundObject[nameof(MessageArguments)];
            }
            set
            {
                LateBoundObject[nameof(MessageArguments)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * An opaque string that uniquely identifies, within the scope of the OwningEntity, the format of the Message.
         */
        public string MessageID
        {
            get
            {
                return (string)LateBoundObject[nameof(MessageID)];
            }
            set
            {
                LateBoundObject[nameof(MessageID)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * A string defining "Other" values for ErrorSourceFormat.
         * This value MUST be set to a non NULL value when ErrorSourceFormat is set to a value of 1 ("Other").
         * For all other values of ErrorSourceFormat, the value of this string must be set to NULL.
         */
        public string OtherErrorSourceFormat
        {
            get
            {
                return (string)LateBoundObject[nameof(OtherErrorSourceFormat)];
            }
            set
            {
                LateBoundObject[nameof(OtherErrorSourceFormat)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * A free-form string describing the ErrorType when 1, "Other", is specified as the ErrorType.
         */
        public string OtherErrorType
        {
            get
            {
                return (string)LateBoundObject[nameof(OtherErrorType)];
            }
            set
            {
                LateBoundObject[nameof(OtherErrorType)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * A string that uniquely identifies the entity that owns the definition of the format of the Message described in this instance.
         * OwningEntity MUST include a copyrighted, trademarked or otherwise unique name that is owned by the business entity or standards body defining the format.
         */
        public string OwningEntity
        {
            get
            {
                return (string)LateBoundObject[nameof(OwningEntity)];
            }
            set
            {
                LateBoundObject[nameof(OwningEntity)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * An enumerated value that describes the severity of the Indication from the notifier's point of view: 
         * 0 - the Perceived Severity of the indication is unknown or indeterminate. 
         * 1 - Other, by CIM convention, is used to indicate that the Severity\'s value can be found in the OtherSeverity property. 
         * 2 - Information should be used when providing an informative response. 
         * 3 - Degraded/Warning should be used when its appropriate to let the user decide if action is needed. 
         * 4 - Minor should be used to indicate action is needed, but the situation is not serious at this time. 
         * 5 - Major should be used to indicate action is needed NOW. 
         * 6 - Critical should be used to indicate action is needed NOW and the scope is broad (perhaps an imminent outage to a critical resource will result). 
         * 7 - Fatal/NonRecoverable should be used to indicate an error occurred, but it\'s too late to take remedial action. 
         * 2 and 0 - Information and Unknown (respectively) follow common usage. Literally, the Error is purely informational or its severity is simply unknown.
         */
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
            set
            {
                LateBoundObject[nameof(PerceivedSeverity)] = value;
                if ((IsEmbedded == false)&& (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * An enumerated value that describes the probable cause of the error.
         */
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
            set
            {
                LateBoundObject[nameof(ProbableCause)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * A free-form string describing the probable cause of the error.
         */
        public string ProbableCauseDescription
        {
            get
            {
                return (string)LateBoundObject[nameof(ProbableCauseDescription)];
            }
            set
            {
                LateBoundObject[nameof(ProbableCauseDescription)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        /*
         * A free-form string describing recommended actions to take to resolve the error.
         */
        public string[] RecommendedActions
        {
            get
            {
                return (string[])LateBoundObject[nameof(RecommendedActions)];
            }
            set
            {
                LateBoundObject[nameof(RecommendedActions)] = value;
                if ((IsEmbedded == false) && (AutoCommit == true))
                {
                    PrivateLateBoundObject.Put();
                }
            }
        }

        private void ResetCIMStatusCode()
        {
            LateBoundObject[nameof(CIMStatusCode)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetCIMStatusCodeDescription()
        {
            LateBoundObject[nameof(CIMStatusCodeDescription)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetErrorSource()
        {
            LateBoundObject[nameof(ErrorSource)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetErrorSourceFormat()
        {
            LateBoundObject[nameof(ErrorSourceFormat)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }
        
        private void ResetErrorType()
        {
            LateBoundObject[nameof(ErrorType)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetMessage()
        {
            LateBoundObject[nameof(Message)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetMessageArguments()
        {
            LateBoundObject[nameof(MessageArguments)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetMessageID()
        {
            LateBoundObject[nameof(MessageID)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetOtherErrorSourceFormat()
        {
            LateBoundObject[nameof(OtherErrorSourceFormat)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetOtherErrorType()
        {
            LateBoundObject[nameof(OtherErrorType)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetOwningEntity()
        {
            LateBoundObject[nameof(OwningEntity)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetPerceivedSeverity()
        {
            LateBoundObject[nameof(PerceivedSeverity)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetProbableCause()
        {
            LateBoundObject[nameof(ProbableCause)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetProbableCauseDescription()
        {
            LateBoundObject[nameof(ProbableCauseDescription)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        private void ResetRecommendedActions()
        {
            LateBoundObject[nameof(RecommendedActions)] = null;
            if ((IsEmbedded == false) && (AutoCommit == true))
            {
                PrivateLateBoundObject.Put();
            }
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<StorageExtendedStatus> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new StorageExtendedStatus(mo)).ToList();

        public new static List<StorageExtendedStatus> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new StorageExtendedStatus(mo)).ToList();

        public static List<StorageExtendedStatus> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new StorageExtendedStatus(mo)).ToList();

        public static List<StorageExtendedStatus> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new StorageExtendedStatus(mo)).ToList();

        public static List<StorageExtendedStatus> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new StorageExtendedStatus(mo)).ToList();

        public static List<StorageExtendedStatus> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new StorageExtendedStatus(mo)).ToList();

        public static List<StorageExtendedStatus> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new StorageExtendedStatus(mo)).ToList();

        public static List<StorageExtendedStatus> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new StorageExtendedStatus(mo)).ToList();

        public static StorageExtendedStatus CreateInstance() => new StorageExtendedStatus(CreateInstance(ClassName));
    }
}
