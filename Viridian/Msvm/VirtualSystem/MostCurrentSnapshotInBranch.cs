using System;
using System.Management;
using System.Globalization;

namespace Viridian.Msvm.VirtualSystem
{
    // Functions ShouldSerialize<PropertyName> are functions used by VS property browser to check if a particular property has to be serialized. These functions are added for all ValueType properties ( properties of type Int32, BOOL etc.. which cannot be set to null). These functions use Is<PropertyName>Null function. These functions are also used in the TypeConverter implementation for the properties to check for NULL value of property so that an empty value can be shown in Property browser in case of Drag and Drop in Visual studio.
    // Functions Is<PropertyName>Null() are used to check if a property is NULL.
    // Functions Reset<PropertyName> are added for Nullable Read/Write properties. These functions are used by VS designer in property browser to set a property to NULL.
    // Every property added to the class for WMI property has attributes set to define its behavior in Visual Studio designer and also to define a TypeConverter to be used.
    // An Early Bound class generated for the WMI class.Msvm_MostCurrentSnapshotInBranch
    public class MostCurrentSnapshotInBranch : IDisposable
    {
        // Private property to hold the WMI namespace in which the class resides.
        private static string CreatedWmiNamespace = "root\\virtualization\\v2";

        // Private property to hold the name of WMI class which created this class.
        private static string CreatedClassName = "Msvm_MostCurrentSnapshotInBranch";

        // Underlying lateBound WMI object.
        private ManagementObject PrivateLateBoundObject;

        // Private variable to hold the embedded property representing the instance.
        private readonly ManagementBaseObject embeddedObj;

        // Flag to indicate if the instance is an embedded object.
        private bool isEmbedded;

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public MostCurrentSnapshotInBranch() => InitializeObject(null, null, null);

        public MostCurrentSnapshotInBranch(ManagementPath keyAntecedent, ManagementPath keyDependent) => InitializeObject(null, new ManagementPath(ConstructPath(keyAntecedent, keyDependent)), null);

        public MostCurrentSnapshotInBranch(ManagementScope mgmtScope, ManagementPath keyAntecedent, ManagementPath keyDependent) => InitializeObject(mgmtScope, new ManagementPath(ConstructPath(keyAntecedent, keyDependent)), null);

        public MostCurrentSnapshotInBranch(ManagementPath path, ObjectGetOptions getOptions) => InitializeObject(null, path, getOptions);

        public MostCurrentSnapshotInBranch(ManagementScope mgmtScope, ManagementPath path) => InitializeObject(mgmtScope, path, null);

        public MostCurrentSnapshotInBranch(ManagementPath path) => InitializeObject(null, path, null);

        public MostCurrentSnapshotInBranch(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) => InitializeObject(mgmtScope, path, getOptions);

        public MostCurrentSnapshotInBranch(ManagementObject theObject)
        {
            Initialize();
            if (CheckIfProperClass(theObject) == true)
            {
                PrivateLateBoundObject = theObject;
                SystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
                LateBoundObject = PrivateLateBoundObject;
            }
            else
            {
                throw new ArgumentException("Class name does not match.");
            }
        }

        public MostCurrentSnapshotInBranch(ManagementBaseObject theObject)
        {
            Initialize();
            if (CheckIfProperClass(theObject) == true)
            {
                embeddedObj = theObject;
                SystemProperties = new ManagementSystemProperties(theObject);
                LateBoundObject = embeddedObj;
                isEmbedded = true;
            }
            else
            {
                throw new ArgumentException("Class name does not match.");
            }
        }

        // Property returns the namespace of the WMI class.
        public string OriginatingNamespace => "root\\virtualization\\v2";

        public string ManagementClassName
        {
            get
            {
                string strRet = CreatedClassName;
                if (LateBoundObject != null)
                {
                    if (LateBoundObject.ClassPath != null)
                    {
                        strRet = (string)LateBoundObject["__CLASS"];
                        if (string.IsNullOrEmpty(strRet))
                        {
                            strRet = CreatedClassName;
                        }
                    }
                }
                return strRet;
            }
        }

        // Property pointing to an embedded object to get System properties of the WMI object.
        public ManagementSystemProperties SystemProperties { get; private set; }

        // Property returning the underlying lateBound object.
        public ManagementBaseObject LateBoundObject { get; private set; }

        // ManagementScope of the object.
        public ManagementScope Scope
        {
            get
            {
                if (isEmbedded == false)
                {
                    return PrivateLateBoundObject.Scope;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (isEmbedded == false)
                {
                    PrivateLateBoundObject.Scope = value;
                }
            }
        }

        // Property to show the commit behavior for the WMI object. If true, WMI object will be automatically saved after each property modification.(ie. Put() is called after modification of a property).
        public bool AutoCommit { get; set; }

        // The ManagementPath of the underlying WMI object.
        public ManagementPath Path
        {
            get
            {
                if (isEmbedded == false)
                {
                    return PrivateLateBoundObject.Path;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (isEmbedded == false)
                {
                    if (CheckIfProperClass(null, value, null) != true)
                    {
                        throw new ArgumentException("Class name does not match.");
                    }
                    PrivateLateBoundObject.Path = value;
                }
            }
        }

        // Public static scope property which is used by the various methods.
        public static ManagementScope StaticScope { get; set; } = null;

        /*
         * Reference to the instance of the Msvm_ComputerSystem or 
         * Msvm_PlannedComputerSystem class representing a virtual system.
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
         * Reference to the instance of the Msvm_VirtualSystemSettingData class representing
         * the virtual system snapshot that is the most current snapshot in a branch of dependent snapshots.
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

        private bool CheckIfProperClass(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions OptionsParam)
        {
            if ((path != null) && (string.Compare(path.ClassName, ManagementClassName, true, CultureInfo.InvariantCulture) == 0))
            {
                return true;
            }
            else
            {
                using (ManagementObject theObj = new ManagementObject(mgmtScope, path, OptionsParam))
                {
                    return CheckIfProperClass(theObj);
                }
            }
        }

        private bool CheckIfProperClass(ManagementBaseObject theObj)
        {
            if ((theObj != null) && (string.Compare((string)theObj["__CLASS"], ManagementClassName, true, CultureInfo.InvariantCulture) == 0))
            {
                return true;
            }
            else
            {
                Array parentClasses = (Array)theObj["__DERIVATION"];
                if (parentClasses != null)
                {
                    int count;
                    for (count = 0; count < parentClasses.Length; count = count + 1)
                    {
                        if (string.Compare((string)parentClasses.GetValue(count), ManagementClassName, true, CultureInfo.InvariantCulture) == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void CommitObject()
        {
            if (isEmbedded == false)
            {
                PrivateLateBoundObject.Put();
            }
        }

        public void CommitObject(PutOptions putOptions)
        {
            if (isEmbedded == false)
            {
                PrivateLateBoundObject.Put(putOptions);
            }
        }

        private void Initialize()
        {
            AutoCommit = true;
            isEmbedded = false;
        }

        private static string ConstructPath(ManagementPath keyAntecedent, ManagementPath keyDependent)
        {
            string strPath = "root\\virtualization\\v2:Msvm_MostCurrentSnapshotInBranch";
            strPath = string.Concat(strPath, string.Concat(".Antecedent=", keyAntecedent.ToString()));
            strPath = string.Concat(strPath, string.Concat(",Dependent=", keyDependent.ToString()));
            return strPath;
        }

        private void InitializeObject(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions)
        {
            Initialize();
            if (path != null)
            {
                if (CheckIfProperClass(mgmtScope, path, getOptions) != true)
                {
                    throw new ArgumentException("Class name does not match.");
                }
            }
            PrivateLateBoundObject = new ManagementObject(mgmtScope, path, getOptions);
            SystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            LateBoundObject = PrivateLateBoundObject;
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static MsvmCollection<MostCurrentSnapshotInBranch> GetInstances() => GetInstances(null, null, null);

        public static MsvmCollection<MostCurrentSnapshotInBranch> GetInstances(string condition) => GetInstances(null, condition, null);

        public static MsvmCollection<MostCurrentSnapshotInBranch> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties);

        public static MsvmCollection<MostCurrentSnapshotInBranch> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties);

        public static MsvmCollection<MostCurrentSnapshotInBranch> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions)
        {
            if (mgmtScope == null)
            {
                if (StaticScope == null)
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\virtualization\\v2";
                }
                else
                {
                    mgmtScope = StaticScope;
                }
            }
            ManagementPath pathObj = new ManagementPath
            {
                ClassName = "Msvm_MostCurrentSnapshotInBranch",
                NamespacePath = "root\\virtualization\\v2"
            };
            using (ManagementClass clsObject = new ManagementClass(mgmtScope, pathObj, null))
            {
                if (enumOptions == null)
                {
                    enumOptions = new EnumerationOptions
                    {
                        EnsureLocatable = true
                    };
                }
                return new MsvmCollection<MostCurrentSnapshotInBranch>(clsObject.GetInstances(enumOptions));
            }
        }

        public static MsvmCollection<MostCurrentSnapshotInBranch> GetInstances(ManagementScope mgmtScope, string condition)
        {
            return GetInstances(mgmtScope, condition, null);
        }

        public static MsvmCollection<MostCurrentSnapshotInBranch> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties);

        public static MsvmCollection<MostCurrentSnapshotInBranch> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties)
        {
            if (mgmtScope == null)
            {
                if (StaticScope == null)
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\virtualization\\v2";
                }
                else
                {
                    mgmtScope = StaticScope;
                }
            }
            using (ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery("Msvm_MostCurrentSnapshotInBranch", condition, selectedProperties)))
            {
                EnumerationOptions enumOptions = new EnumerationOptions
                {
                    EnsureLocatable = true
                };
                ObjectSearcher.Options = enumOptions;
                return new MsvmCollection<MostCurrentSnapshotInBranch>(ObjectSearcher.Get());
            }
        }

        public static MostCurrentSnapshotInBranch CreateInstance()
        {
            ManagementScope mgmtScope;
            if (StaticScope == null)
            {
                mgmtScope = new ManagementScope();
                mgmtScope.Path.NamespacePath = CreatedWmiNamespace;
            }
            else
            {
                mgmtScope = StaticScope;
            }
            ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
            using (ManagementClass tmpMgmtClass = new ManagementClass(mgmtScope, mgmtPath, null))
            {
                return new MostCurrentSnapshotInBranch(tmpMgmtClass.CreateInstance());
            }
        }

        public void Delete() => PrivateLateBoundObject.Delete();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                PrivateLateBoundObject.Dispose();
            }
        }

        ~MostCurrentSnapshotInBranch()
        {
            Dispose(false);
        }
    }
}
