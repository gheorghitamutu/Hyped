using System;
using System.ComponentModel;
using System.Management;
using System.Collections;
using System.Globalization;

namespace Viridian.Msvm.VirtualSystem
{
    // Functions ShouldSerialize<PropertyName> are functions used by VS property browser to check if a particular property has to be serialized. These functions are added for all ValueType properties ( properties of type Int32, BOOL etc.. which cannot be set to null). These functions use Is<PropertyName>Null function. These functions are also used in the TypeConverter implementation for the properties to check for NULL value of property so that an empty value can be shown in Property browser in case of Drag and Drop in Visual studio.
    // Functions Is<PropertyName>Null() are used to check if a property is NULL.
    // Functions Reset<PropertyName> are added for Nullable Read/Write properties. These functions are used by VS designer in property browser to set a property to NULL.
    // Every property added to the class for WMI property has attributes set to define its behavior in Visual Studio designer and also to define a TypeConverter to be used.
    // An Early Bound class generated for the WMI class.Msvm_SnapshotOfVirtualSystem
    public class SnapshotOfVirtualSystem : Component
    {

        // Private property to hold the WMI namespace in which the class resides.
        private static string CreatedWmiNamespace = "root\\virtualization\\v2";

        // Private property to hold the name of WMI class which created this class.
        private static string CreatedClassName = "Msvm_SnapshotOfVirtualSystem";

        // Underlying lateBound WMI object.
        private ManagementObject PrivateLateBoundObject;

        // Private variable to hold the embedded property representing the instance.
        private readonly ManagementBaseObject embeddedObj;

        // Flag to indicate if the instance is an embedded object.
        private bool isEmbedded;

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public SnapshotOfVirtualSystem()
        {
            InitializeObject(null, null, null);
        }

        public SnapshotOfVirtualSystem(ManagementPath keyAntecedent, ManagementPath keyDependent)
        {
            InitializeObject(null, new ManagementPath(ConstructPath(keyAntecedent, keyDependent)), null);
        }

        public SnapshotOfVirtualSystem(ManagementScope mgmtScope, ManagementPath keyAntecedent, ManagementPath keyDependent)
        {
            InitializeObject(mgmtScope, new ManagementPath(ConstructPath(keyAntecedent, keyDependent)), null);
        }

        public SnapshotOfVirtualSystem(ManagementPath path, ObjectGetOptions getOptions)
        {
            InitializeObject(null, path, getOptions);
        }

        public SnapshotOfVirtualSystem(ManagementScope mgmtScope, ManagementPath path)
        {
            InitializeObject(mgmtScope, path, null);
        }

        public SnapshotOfVirtualSystem(ManagementPath path)
        {
            InitializeObject(null, path, null);
        }

        public SnapshotOfVirtualSystem(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions)
        {
            InitializeObject(mgmtScope, path, getOptions);
        }

        public SnapshotOfVirtualSystem(ManagementObject theObject)
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

        public SnapshotOfVirtualSystem(ManagementBaseObject theObject)
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
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OriginatingNamespace => "root\\virtualization\\v2";

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementSystemProperties SystemProperties { get; private set; }

        // Property returning the underlying lateBound object.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementBaseObject LateBoundObject { get; private set; }

        // ManagementScope of the object.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoCommit { get; set; }

        // The ManagementPath of the underlying WMI object.
        [Browsable(true)]
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
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static ManagementScope StaticScope { get; set; } = null;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
            if ((path != null)
                        && (string.Compare(path.ClassName, ManagementClassName, true, CultureInfo.InvariantCulture) == 0))
            {
                return true;
            }
            else
            {
                using (ManagementObject theObject = new ManagementObject(mgmtScope, path, OptionsParam))
                {
                    return CheckIfProperClass(theObject);
                }
            }
        }

        private bool CheckIfProperClass(ManagementBaseObject theObj)
        {
            if ((theObj != null)
                        && (string.Compare((string)theObj["__CLASS"], ManagementClassName, true, CultureInfo.InvariantCulture) == 0))
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

        [Browsable(true)]
        public void CommitObject()
        {
            if (isEmbedded == false)
            {
                PrivateLateBoundObject.Put();
            }
        }

        [Browsable(true)]
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
            string strPath = "root\\virtualization\\v2:Msvm_SnapshotOfVirtualSystem";
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
        public static SnapshotOfVirtualSystemCollection GetInstances() => GetInstances(null, null, null);

        public static SnapshotOfVirtualSystemCollection GetInstances(string condition) => GetInstances(null, condition, null);

        public static SnapshotOfVirtualSystemCollection GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties);

        public static SnapshotOfVirtualSystemCollection GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties);

        public static SnapshotOfVirtualSystemCollection GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions)
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
                ClassName = "Msvm_SnapshotOfVirtualSystem",
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
                return new SnapshotOfVirtualSystemCollection(clsObject.GetInstances(enumOptions));
            }
        }

        public static SnapshotOfVirtualSystemCollection GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null);

        public static SnapshotOfVirtualSystemCollection GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties);

        public static SnapshotOfVirtualSystemCollection GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties)
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
            using (ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery("Msvm_SnapshotOfVirtualSystem", condition, selectedProperties)))
            {
                EnumerationOptions enumOptions = new EnumerationOptions
                {
                    EnsureLocatable = true
                };
                ObjectSearcher.Options = enumOptions;
                return new SnapshotOfVirtualSystemCollection(ObjectSearcher.Get());
            }
        }

        [Browsable(true)]
        public static SnapshotOfVirtualSystem CreateInstance()
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
                return new SnapshotOfVirtualSystem(tmpMgmtClass.CreateInstance());
            }
        }

        [Browsable(true)]
        public void Delete() => PrivateLateBoundObject.Delete();

        // Enumerator implementation for enumerating instances of the class.
        public class SnapshotOfVirtualSystemCollection : object, ICollection
        {
            private readonly ManagementObjectCollection privColObj;

            public SnapshotOfVirtualSystemCollection(ManagementObjectCollection objCollection) => privColObj = objCollection;

            public virtual int Count => privColObj.Count;

            public virtual bool IsSynchronized => privColObj.IsSynchronized;

            public virtual object SyncRoot => this;

            public virtual void CopyTo(Array array, int index)
            {
                privColObj.CopyTo(array, index);
                int nCtr;
                for (nCtr = 0; nCtr < array?.Length; nCtr += 1)
                {
                    using (SnapshotOfVirtualSystem value = new SnapshotOfVirtualSystem((ManagementObject)array.GetValue(nCtr)))
                    {
                        array.SetValue(value, nCtr);
                    }
                }
            }

            public virtual IEnumerator GetEnumerator() => new SnapshotOfVirtualSystemEnumerator(privColObj.GetEnumerator());

            public class SnapshotOfVirtualSystemEnumerator : object, IEnumerator
            {
                private readonly ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;

                public SnapshotOfVirtualSystemEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum) => privObjEnum = objEnum;

                public virtual object Current => new SnapshotOfVirtualSystem((ManagementObject)privObjEnum.Current);

                public virtual bool MoveNext() => privObjEnum.MoveNext();

                public virtual void Reset() => privObjEnum.Reset();
            }
        }

        // TypeConverter to handle null values for ValueType properties
        public class WMIValueTypeConverter : TypeConverter
        {
            private readonly TypeConverter baseConverter;

            private readonly Type baseType;

            public WMIValueTypeConverter(Type inBaseType)
            {
                baseConverter = TypeDescriptor.GetConverter(inBaseType);
                baseType = inBaseType;
            }

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type srcType) => baseConverter.CanConvertFrom(context, srcType);

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => baseConverter.CanConvertTo(context, destinationType);

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => baseConverter.ConvertFrom(context, culture, value);

            public override object CreateInstance(ITypeDescriptorContext context, IDictionary dictionary) => baseConverter.CreateInstance(context, dictionary);

            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => baseConverter.GetCreateInstanceSupported(context);

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributeVar) => baseConverter.GetProperties(context, value, attributeVar);

            public override bool GetPropertiesSupported(ITypeDescriptorContext context) => baseConverter.GetPropertiesSupported(context);

            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) => baseConverter.GetStandardValues(context);

            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => baseConverter.GetStandardValuesExclusive(context);

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => baseConverter.GetStandardValuesSupported(context);

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (baseType.BaseType == typeof(Enum))
                {
                    if (value?.GetType() == destinationType)
                    {
                        return value;
                    }
                    if ((value == null)
                                && (context != null)
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))
                    {
                        return "NULL_ENUM_VALUE";
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if ((baseType == typeof(bool))
                            && (baseType.BaseType == typeof(ValueType)))
                {
                    if ((value == null)
                                && (context != null)
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))
                    {
                        return "";
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if ((context != null)
                            && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))
                {
                    return "";
                }
                return baseConverter.ConvertTo(context, culture, value, destinationType);
            }
        }

        // Embedded class to represent WMI system Properties.
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ManagementSystemProperties
        {
            private readonly ManagementBaseObject PrivateLateBoundObject;

            public ManagementSystemProperties(ManagementBaseObject ManagedObject) => PrivateLateBoundObject = ManagedObject;

            [Browsable(true)]
            public int GENUS => (int)PrivateLateBoundObject["__GENUS"];

            [Browsable(true)]
            public string CLASS => (string)PrivateLateBoundObject["__CLASS"];

            [Browsable(true)]
            public string SUPERCLASS => (string)PrivateLateBoundObject["__SUPERCLASS"];

            [Browsable(true)]
            public string DYNASTY => (string)PrivateLateBoundObject["__DYNASTY"];

            [Browsable(true)]
            public string RELPATH => (string)PrivateLateBoundObject["__RELPATH"];

            [Browsable(true)]
            public int PROPERTY_COUNT => (int)PrivateLateBoundObject["__PROPERTY_COUNT"];

            [Browsable(true)]
            public string[] DERIVATION => (string[])PrivateLateBoundObject["__DERIVATION"];

            [Browsable(true)]
            public string SERVER => (string)PrivateLateBoundObject["__SERVER"];

            [Browsable(true)]
            public string NAMESPACE => (string)PrivateLateBoundObject["__NAMESPACE"];

            [Browsable(true)]
            public string PATH => (string)PrivateLateBoundObject["__PATH"];
        }
    }
}
