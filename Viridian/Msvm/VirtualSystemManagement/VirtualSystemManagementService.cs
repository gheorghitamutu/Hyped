using System;
using System.ComponentModel;
using System.Management;
using System.Collections;
using System.Globalization;

namespace Viridian.Msvm.VirtualSystemManagement
{
    // Functions ShouldSerialize<PropertyName> are functions used by VS property browser to check if a particular property has to be serialized. These functions are added for all ValueType properties ( properties of type Int32, BOOL etc.. which cannot be set to null). These functions use Is<PropertyName>Null function. These functions are also used in the TypeConverter implementation for the properties to check for NULL value of property so that an empty value can be shown in Property browser in case of Drag and Drop in Visual studio.
    // Functions Is<PropertyName>Null() are used to check if a property is NULL.
    // Functions Reset<PropertyName> are added for Nullable Read/Write properties. These functions are used by VS designer in property browser to set a property to NULL.
    // Every property added to the class for WMI property has attributes set to define its behavior in Visual Studio designer and also to define a TypeConverter to be used.
    // Datetime conversion functions ToDateTime and ToDmtfDateTime are added to the class to convert DMTF datetime to System.DateTime and vice-versa.
    // An Early Bound class generated for the WMI class.Msvm_VirtualSystemManagementService
    public class VirtualSystemManagementService : Component
    {
        // Private property to hold the WMI namespace in which the class resides.
        private static readonly string CreatedWmiNamespace = Properties.VESMS.Default.WMINamespace;

        // Private property to hold the name of WMI class which created this class.
        private static readonly string CreatedClassName = Properties.VESMS.Default.ClassName;

        // Underlying lateBound WMI object.
        private ManagementObject PrivateLateBoundObject;

        // Private variable to hold the embedded property representing the instance.
        private readonly ManagementBaseObject embeddedObj;

        // Flag to indicate if the instance is an embedded object.
        private bool isEmbedded;

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public VirtualSystemManagementService() => InitializeObject(null, null, null);

        public VirtualSystemManagementService(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) => InitializeObject(null, new ManagementPath(ConstructPath(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName)), null);

        public VirtualSystemManagementService(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) => InitializeObject(mgmtScope, new ManagementPath(ConstructPath(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName)), null);

        public VirtualSystemManagementService(ManagementPath path, ObjectGetOptions getOptions) => InitializeObject(null, path, getOptions);

        public VirtualSystemManagementService(ManagementScope mgmtScope, ManagementPath path) => InitializeObject(mgmtScope, path, null);

        public VirtualSystemManagementService(ManagementPath path) => InitializeObject(null, path, null);

        public VirtualSystemManagementService(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) => InitializeObject(mgmtScope, path, getOptions);

        public VirtualSystemManagementService(ManagementObject theObject)
        {
            Initialize();
            if (theObject != null && CheckIfProperClass(theObject) == true)
            {
                PrivateLateBoundObject = theObject;
                SystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
                LateBoundObject = PrivateLateBoundObject;
            }
            else
            {
                throw new ArgumentException(Properties.VESMS.Default.ClassNameExceptionMessage);
            }
        }

        public VirtualSystemManagementService(ManagementBaseObject theObject)
        {
            Initialize();
            if (theObject != null && CheckIfProperClass(theObject) == true)
            {
                embeddedObj = theObject;
                SystemProperties = new ManagementSystemProperties(theObject);
                LateBoundObject = embeddedObj;
                isEmbedded = true;
            }
            else
            {
                throw new ArgumentException(Properties.VESMS.Default.ClassNameExceptionMessage);
            }
        }

        // Property returns the namespace of the WMI class.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static string OriginatingNamespace => Properties.VESMS.Default.WMINamespace;

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
                        throw new ArgumentException(Properties.VESMS.Default.ClassNameExceptionMessage);
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
        public ushort[] AvailableRequestedStates => (ushort[])LateBoundObject[nameof(AvailableRequestedStates)];

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Caption => (string)LateBoundObject[nameof(Caption)];

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsCommunicationStatusNull
        {
            get
            {
                if (LateBoundObject[nameof(CommunicationStatus)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort CommunicationStatus
        {
            get
            {
                if (LateBoundObject[nameof(CommunicationStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(CommunicationStatus)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CreationClassName => (string)LateBoundObject[nameof(CreationClassName)];

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Description => (string)LateBoundObject[nameof(Description)];

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDetailedStatusNull
        {
            get
            {
                if (LateBoundObject[nameof(DetailedStatus)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort DetailedStatus
        {
            get
            {
                if (LateBoundObject[nameof(DetailedStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(DetailedStatus)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEnabledDefaultNull
        {
            get
            {
                if (LateBoundObject[nameof(EnabledDefault)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort EnabledDefault
        {
            get
            {
                if (LateBoundObject[nameof(EnabledDefault)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(EnabledDefault)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEnabledStateNull
        {
            get
            {
                if (LateBoundObject[nameof(EnabledState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort EnabledState
        {
            get
            {
                if (LateBoundObject[nameof(EnabledState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(EnabledState)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsHealthStateNull
        {
            get
            {
                if (LateBoundObject[nameof(HealthState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort HealthState
        {
            get
            {
                if (LateBoundObject[nameof(HealthState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(HealthState)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInstallDateNull
        {
            get
            {
                if (LateBoundObject[nameof(InstallDate)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public DateTime InstallDate
        {
            get
            {
                if (LateBoundObject[nameof(InstallDate)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(InstallDate)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Name => (string)LateBoundObject[nameof(Name)];

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsOperatingStatusNull
        {
            get
            {
                if (LateBoundObject[nameof(OperatingStatus)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort OperatingStatus
        {
            get
            {
                if (LateBoundObject[nameof(OperatingStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(OperatingStatus)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ushort[] OperationalStatus => (ushort[])LateBoundObject[nameof(OperationalStatus)];

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OtherEnabledState => (string)LateBoundObject[nameof(OtherEnabledState)];

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PrimaryOwnerContact => (string)LateBoundObject[nameof(PrimaryOwnerContact)];

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PrimaryOwnerName => (string)LateBoundObject[nameof(PrimaryOwnerName)];

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPrimaryStatusNull
        {
            get
            {
                if (LateBoundObject[nameof(PrimaryStatus)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort PrimaryStatus
        {
            get
            {
                if (LateBoundObject[nameof(PrimaryStatus)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(PrimaryStatus)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsRequestedStateNull
        {
            get
            {
                if (LateBoundObject[nameof(RequestedState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort RequestedState
        {
            get
            {
                if (LateBoundObject[nameof(RequestedState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(RequestedState)];
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsStartedNull
        {
            get
            {
                if (LateBoundObject[nameof(Started)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool Started
        {
            get
            {
                if (LateBoundObject[nameof(Started)] == null)
                {
                    return Convert.ToBoolean(0);
                }
                return (bool)LateBoundObject[nameof(Started)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string StartMode => (string)LateBoundObject[nameof(StartMode)];

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Status => (string)LateBoundObject[nameof(Status)];

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] StatusDescriptions => (string[])LateBoundObject[nameof(StatusDescriptions)];

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SystemCreationClassName => (string)LateBoundObject[nameof(SystemCreationClassName)];

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SystemName => (string)LateBoundObject[nameof(SystemName)];

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTimeOfLastStateChangeNull
        {
            get
            {
                if (LateBoundObject[nameof(TimeOfLastStateChange)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public DateTime TimeOfLastStateChange
        {
            get
            {
                if (LateBoundObject[nameof(TimeOfLastStateChange)] != null)
                {
                    return ToDateTime((string)LateBoundObject[nameof(TimeOfLastStateChange)]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTransitioningToStateNull
        {
            get
            {
                if (LateBoundObject[nameof(TransitioningToState)] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public ushort TransitioningToState
        {
            get
            {
                if (LateBoundObject[nameof(TransitioningToState)] == null)
                {
                    return Convert.ToUInt16(0);
                }
                return (ushort)LateBoundObject[nameof(TransitioningToState)];
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
                    for (count = 0; count < parentClasses.Length; count += 1)
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

        private bool ShouldSerializeCommunicationStatus()
        {
            if (IsCommunicationStatusNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeDetailedStatus()
        {
            if (IsDetailedStatusNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeEnabledDefault()
        {
            if (IsEnabledDefaultNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeEnabledState()
        {
            if (IsEnabledStateNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeHealthState()
        {
            if (IsHealthStateNull == false)
            {
                return true;
            }
            return false;
        }

        // Converts a given datetime in DMTF format to System.DateTime object.
        static DateTime ToDateTime(string dmtfDate)
        {
            DateTime initializer = DateTime.MinValue;
            int year = initializer.Year;
            int month = initializer.Month;
            int day = initializer.Day;
            int hour = initializer.Hour;
            int minute = initializer.Minute;
            int second = initializer.Second;
            long ticks = 0;
            string dmtf = dmtfDate;
            if (dmtf == null)
            {
                throw new ArgumentOutOfRangeException(dmtf);
            }
            if (dmtf.Length == 0)
            {
                throw new ArgumentOutOfRangeException(dmtf);
            }
            if (dmtf.Length != 25)
            {
                throw new ArgumentOutOfRangeException(dmtf);
            }

            string tempString;
            try
            {
                tempString = dmtf.Substring(0, 4);
                if ("****" != tempString)
                {
                    year = int.Parse(tempString);
                }
                tempString = dmtf.Substring(4, 2);
                if ("**" != tempString)
                {
                    month = int.Parse(tempString);
                }
                tempString = dmtf.Substring(6, 2);
                if ("**" != tempString)
                {
                    day = int.Parse(tempString);
                }
                tempString = dmtf.Substring(8, 2);
                if ("**" != tempString)
                {
                    hour = int.Parse(tempString);
                }
                tempString = dmtf.Substring(10, 2);
                if ("**" != tempString)
                {
                    minute = int.Parse(tempString);
                }
                tempString = dmtf.Substring(12, 2);
                if ("**" != tempString)
                {
                    second = int.Parse(tempString);
                }
                tempString = dmtf.Substring(15, 6);
                if ("******" != tempString)
                {
                    ticks = long.Parse(tempString) * (TimeSpan.TicksPerMillisecond / 1000);
                }
                if ((year < 0)
                            || (month < 0)
                            || (day < 0)
                            || (hour < 0)
                            || (minute < 0)
                            || (minute < 0)
                            || (second < 0)
                            || (ticks < 0))
                {
                    throw new ArgumentOutOfRangeException(year.ToString());
                }
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException(null, e.Message);
            }
            DateTime datetime = new DateTime(year, month, day, hour, minute, second, 0);
            datetime = datetime.AddTicks(ticks);
            TimeSpan tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(datetime);
            long OffsetMins = tickOffset.Ticks / TimeSpan.TicksPerMinute;
            tempString = dmtf.Substring(22, 3);
            if (tempString != "******")
            {
                tempString = dmtf.Substring(21, 4);
                int UTCOffset;
                try
                {
                    UTCOffset = int.Parse(tempString);
                }
                catch (Exception e)
                {
                    throw new ArgumentOutOfRangeException(null, e.Message);
                }

                int OffsetToBeAdjusted = (int)(OffsetMins - UTCOffset);
                datetime = datetime.AddMinutes(OffsetToBeAdjusted);
            }
            return datetime;
        }

        // Converts a given System.DateTime object to DMTF datetime format.
        static string ToDmtfDateTime(DateTime date)
        {
            TimeSpan tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(date);
            long OffsetMins = tickOffset.Ticks / TimeSpan.TicksPerMinute;
            string utcString;
            if (Math.Abs(OffsetMins) > 999)
            {
                date = date.ToUniversalTime();
                utcString = "+000";
            }
            else
            {
                if (tickOffset.Ticks >= 0)
                {
                    utcString = string.Concat("+", (tickOffset.Ticks / TimeSpan.TicksPerMinute).ToString().PadLeft(3, '0'));
                }
                else
                {
                    string strTemp = OffsetMins.ToString();
                    utcString = string.Concat("-", strTemp.Substring(1, strTemp.Length - 1).PadLeft(3, '0'));
                }
            }
            string dmtfDateTime = date.Year.ToString().PadLeft(4, '0');
            dmtfDateTime = string.Concat(dmtfDateTime, date.Month.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Day.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Hour.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Minute.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Second.ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ".");
            DateTime dtTemp = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
            long microsec = (date.Ticks - dtTemp.Ticks)
                        * 1000
                        / TimeSpan.TicksPerMillisecond;
            string strMicrosec = microsec.ToString();
            if (strMicrosec.Length > 6)
            {
                strMicrosec = strMicrosec.Substring(0, 6);
            }
            dmtfDateTime = string.Concat(dmtfDateTime, strMicrosec.PadLeft(6, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, utcString);
            return dmtfDateTime;
        }

        private bool ShouldSerializeInstallDate()
        {
            if (IsInstallDateNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeOperatingStatus()
        {
            if (IsOperatingStatusNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializePrimaryStatus()
        {
            if (IsPrimaryStatusNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeRequestedState()
        {
            if (IsRequestedStateNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeStarted()
        {
            if (IsStartedNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeTimeOfLastStateChange()
        {
            if (IsTimeOfLastStateChangeNull == false)
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeTransitioningToState()
        {
            if (IsTransitioningToStateNull == false)
            {
                return true;
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

        private static string ConstructPath(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName)
        {
            string strPath = $"{Properties.VESMS.Default.WMINamespace}:{Properties.VESMS.Default.ClassName}";
            strPath = string.Concat(strPath, string.Concat(".CreationClassName=", string.Concat("\"", string.Concat(keyCreationClassName, "\""))));
            strPath = string.Concat(strPath, string.Concat(",Name=", string.Concat("\"", string.Concat(keyName, "\""))));
            strPath = string.Concat(strPath, string.Concat(",SystemCreationClassName=", string.Concat("\"", string.Concat(keySystemCreationClassName, "\""))));
            strPath = string.Concat(strPath, string.Concat(",SystemName=", string.Concat("\"", string.Concat(keySystemName, "\""))));
            return strPath;
        }

        private void InitializeObject(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions)
        {
            Initialize();
            if (path != null)
            {
                if (CheckIfProperClass(mgmtScope, path, getOptions) != true)
                {
                    throw new ArgumentException(Properties.VESMS.Default.ClassNameExceptionMessage);
                }
            }
            PrivateLateBoundObject = new ManagementObject(mgmtScope, path, getOptions);
            SystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            LateBoundObject = PrivateLateBoundObject;
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static VirtualSystemManagementServiceCollection GetInstances()
        {
            return GetInstances(null, null, null);
        }

        public static VirtualSystemManagementServiceCollection GetInstances(string condition)
        {
            return GetInstances(null, condition, null);
        }

        public static VirtualSystemManagementServiceCollection GetInstances(string[] selectedProperties)
        {
            return GetInstances(null, null, selectedProperties);
        }

        public static VirtualSystemManagementServiceCollection GetInstances(string condition, string[] selectedProperties)
        {
            return GetInstances(null, condition, selectedProperties);
        }

        public static VirtualSystemManagementServiceCollection GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions)
        {
            if (mgmtScope == null)
            {
                if (StaticScope == null)
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = Properties.VESMS.Default.WMINamespace;
                }
                else
                {
                    mgmtScope = StaticScope;
                }
            }
            ManagementPath pathObj = new ManagementPath
            {
                ClassName = Properties.VESMS.Default.ClassName,
                NamespacePath = Properties.VESMS.Default.WMINamespace
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
                return new VirtualSystemManagementServiceCollection(clsObject.GetInstances(enumOptions));
            }
        }

        public static VirtualSystemManagementServiceCollection GetInstances(ManagementScope mgmtScope, string condition)
        {
            return GetInstances(mgmtScope, condition, null);
        }

        public static VirtualSystemManagementServiceCollection GetInstances(ManagementScope mgmtScope, string[] selectedProperties)
        {
            return GetInstances(mgmtScope, null, selectedProperties);
        }

        public static VirtualSystemManagementServiceCollection GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties)
        {
            if (mgmtScope == null)
            {
                if (StaticScope == null)
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = Properties.VESMS.Default.WMINamespace;
                }
                else
                {
                    mgmtScope = StaticScope;
                }
            }
            using (ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery(Properties.VESMS.Default.ClassName, condition, selectedProperties)))
            {
                EnumerationOptions enumOptions = new EnumerationOptions
                {
                    EnsureLocatable = true
                };
                ObjectSearcher.Options = enumOptions;
                return new VirtualSystemManagementServiceCollection(ObjectSearcher.Get());
            }
        }

        [Browsable(true)]
        public static VirtualSystemManagementService CreateInstance()
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
                return new VirtualSystemManagementService(tmpMgmtClass.CreateInstance());
            }
        }

        [Browsable(true)]
        public void Delete()
        {
            PrivateLateBoundObject.Delete();
        }

        public uint AddBootSourceSettings(ManagementPath AffectedConfiguration, string[] BootSourceSettings, out ManagementPath Job, out ManagementPath[] ResultingBootSourceSettings)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("AddBootSourceSettings");
                inParams["AffectedConfiguration"] = AffectedConfiguration?.Path;
                inParams["BootSourceSettings"] = BootSourceSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("AddBootSourceSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingBootSourceSettings = null;
                if (outParams.Properties["ResultingBootSourceSettings"] != null)
                {
                    int len = ((Array)outParams.Properties["ResultingBootSourceSettings"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["ResultingBootSourceSettings"].Value).GetValue(iCounter).ToString());
                    }
                    ResultingBootSourceSettings = arrToRet;
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingBootSourceSettings = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint AddFeatureSettings(ManagementPath AffectedConfiguration, string[] FeatureSettings, out ManagementPath Job, out ManagementPath[] ResultingFeatureSettings)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("AddFeatureSettings");
                inParams["AffectedConfiguration"] = AffectedConfiguration?.Path;
                inParams["FeatureSettings"] = FeatureSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("AddFeatureSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingFeatureSettings = null;
                if (outParams.Properties["ResultingFeatureSettings"] != null)
                {
                    int len = ((Array)outParams.Properties["ResultingFeatureSettings"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["ResultingFeatureSettings"].Value).GetValue(iCounter).ToString());
                    }
                    ResultingFeatureSettings = arrToRet;
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingFeatureSettings = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint AddFibreChannelChap(string[] FcPortSettings, byte SecretEncoding, byte[] SharedSecret)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("AddFibreChannelChap");
                inParams["FcPortSettings"] = FcPortSettings;
                inParams["SecretEncoding"] = SecretEncoding;
                inParams["SharedSecret"] = SharedSecret;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("AddFibreChannelChap", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint AddGuestServiceSettings(ManagementPath AffectedConfiguration, string[] GuestServiceSettings, out ManagementPath Job, out ManagementPath[] ResultingGuestServiceSettings)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("AddGuestServiceSettings");
                inParams["AffectedConfiguration"] = AffectedConfiguration?.Path;
                inParams["GuestServiceSettings"] = GuestServiceSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("AddGuestServiceSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingGuestServiceSettings = null;
                if (outParams.Properties["ResultingGuestServiceSettings"] != null)
                {
                    int len = ((Array)outParams.Properties["ResultingGuestServiceSettings"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["ResultingGuestServiceSettings"].Value).GetValue(iCounter).ToString());
                    }
                    ResultingGuestServiceSettings = arrToRet;
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingGuestServiceSettings = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint AddKvpItems(string[] DataItems, ManagementPath TargetSystem, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("AddKvpItems");
                inParams["DataItems"] = DataItems;
                inParams["TargetSystem"] = TargetSystem?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("AddKvpItems", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint AddResourceSettings(ManagementPath AffectedConfiguration, string[] ResourceSettings, out ManagementPath Job, out ManagementPath[] ResultingResourceSettings)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("AddResourceSettings");
                inParams["AffectedConfiguration"] = AffectedConfiguration?.Path;
                inParams["ResourceSettings"] = ResourceSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("AddResourceSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingResourceSettings = null;
                if (outParams.Properties["ResultingResourceSettings"] != null)
                {
                    int len = ((Array)outParams.Properties["ResultingResourceSettings"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["ResultingResourceSettings"].Value).GetValue(iCounter).ToString());
                    }
                    ResultingResourceSettings = arrToRet;
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingResourceSettings = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint AddSystemComponentSettings(ManagementPath AffectedConfiguration, string[] ComponentSettings, out ManagementPath Job, out ManagementPath[] ResultingComponentSettings)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("AddSystemComponentSettings");
                inParams["AffectedConfiguration"] = AffectedConfiguration?.Path;
                inParams["ComponentSettings"] = ComponentSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("AddSystemComponentSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingComponentSettings = null;
                if (outParams.Properties["ResultingComponentSettings"] != null)
                {
                    int len = ((Array)outParams.Properties["ResultingComponentSettings"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["ResultingComponentSettings"].Value).GetValue(iCounter).ToString());
                    }
                    ResultingComponentSettings = arrToRet;
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingComponentSettings = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint DefinePlannedSystem(ManagementPath ReferenceConfiguration, string[] ResourceSettings, string SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("DefinePlannedSystem");
                inParams["ReferenceConfiguration"] = ReferenceConfiguration?.Path;
                inParams["ResourceSettings"] = ResourceSettings;
                inParams["SystemSettings"] = SystemSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DefinePlannedSystem", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingSystem = null;
                if (outParams.Properties["ResultingSystem"] != null)
                {
                    ResultingSystem = new ManagementPath(outParams["ResultingSystem"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingSystem = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint DefineSystem(ManagementPath ReferenceConfiguration, string[] ResourceSettings, string SystemSettings, out ManagementPath Job, out ManagementPath ResultingSystem)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("DefineSystem");
                inParams["ReferenceConfiguration"] = ReferenceConfiguration?.Path;
                inParams["ResourceSettings"] = ResourceSettings;
                inParams["SystemSettings"] = SystemSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DefineSystem", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingSystem = null;
                if (outParams.Properties["ResultingSystem"] != null)
                {
                    ResultingSystem = new ManagementPath(outParams["ResultingSystem"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingSystem = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint DestroySystem(ManagementPath AffectedSystem, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("DestroySystem");
                inParams["AffectedSystem"] = AffectedSystem?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DestroySystem", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint DiagnoseNetworkConnection(string DiagnosticSettings, ManagementPath TargetNetworkAdapter, out string DiagnosticInformation, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("DiagnoseNetworkConnection");
                inParams["DiagnosticSettings"] = DiagnosticSettings;
                inParams["TargetNetworkAdapter"] = TargetNetworkAdapter?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DiagnoseNetworkConnection", inParams, null);
                DiagnosticInformation = Convert.ToString(outParams.Properties["DiagnosticInformation"].Value);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                DiagnosticInformation = null;
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ExportSystemDefinition(ManagementPath ComputerSystem, string ExportDirectory, string ExportSettingData, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ExportSystemDefinition");
                inParams["ComputerSystem"] = ComputerSystem?.Path;
                inParams["ExportDirectory"] = ExportDirectory;
                inParams["ExportSettingData"] = ExportSettingData;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ExportSystemDefinition", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint FormatError(string[] Errors, out string ErrorMessage)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("FormatError");
                inParams["Errors"] = Errors;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("FormatError", inParams, null);
                ErrorMessage = Convert.ToString(outParams.Properties["ErrorMessage"].Value);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ErrorMessage = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GenerateWwpn(uint NumberOfWwpns, out string[] GeneratedWwpn)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("GenerateWwpn");
                inParams["NumberOfWwpns"] = NumberOfWwpns;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GenerateWwpn", inParams, null);
                GeneratedWwpn = (string[])outParams.Properties["GeneratedWwpn"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                GeneratedWwpn = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetCurrentWwpnFromGenerator(out string CurrentWwpn)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetCurrentWwpnFromGenerator", inParams, null);
                CurrentWwpn = Convert.ToString(outParams.Properties["CurrentWwpn"].Value);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                CurrentWwpn = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetDefinitionFileSummaryInformation(string[] DefinitionFiles, out ManagementBaseObject[] SummaryInformation)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("GetDefinitionFileSummaryInformation");
                inParams["DefinitionFiles"] = DefinitionFiles;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetDefinitionFileSummaryInformation", inParams, null);
                SummaryInformation = (ManagementBaseObject[])outParams.Properties["SummaryInformation"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                SummaryInformation = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetSizeOfSystemFiles(ManagementPath Vssd, out ulong Size)
        {
            if (isEmbedded == false)
            {
                using (ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("GetSizeOfSystemFiles"))
                {
                    inParams["Vssd"] = Vssd?.Path;
                    using (ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetSizeOfSystemFiles", inParams, null))
                    {
                        Size = Convert.ToUInt64(outParams.Properties["Size"].Value);
                        return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
                    }
                }
            }
            else
            {
                Size = Convert.ToUInt64(0);
                return Convert.ToUInt32(0);
            }
        }

        public uint GetSummaryInformation(uint[] RequestedInformation, ManagementPath[] SettingData, out ManagementBaseObject[] SummaryInformation)
        {
            if (isEmbedded == false)
            {
                using (ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("GetSummaryInformation"))
                {
                    inParams["RequestedInformation"] = RequestedInformation;
                    if (SettingData != null)
                    {
                        int len = SettingData.Length;
                        string[] arrProp = new string[len];
                        for (int iCounter = 0; iCounter < len; iCounter = iCounter + 1)
                        {
                            arrProp[iCounter] = ((ManagementPath)SettingData.GetValue(iCounter)).Path;
                        }
                        inParams["SettingData"] = arrProp;
                    }
                    else
                    {
                        inParams["SettingData"] = null;
                    }
                    using (ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetSummaryInformation", inParams, null))
                    {
                        SummaryInformation = (ManagementBaseObject[])outParams.Properties["SummaryInformation"].Value;
                        return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
                    }
                }
            }
            else
            {
                SummaryInformation = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint GetVirtualSystemThumbnailImage(ushort HeightPixels, ManagementPath TargetSystem, ushort WidthPixels, out byte[] ImageData)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("GetVirtualSystemThumbnailImage");
                inParams["HeightPixels"] = HeightPixels;
                inParams["TargetSystem"] = TargetSystem?.Path;
                inParams["WidthPixels"] = WidthPixels;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("GetVirtualSystemThumbnailImage", inParams, null);
                ImageData = (byte[])outParams.Properties["ImageData"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ImageData = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ImportSnapshotDefinitions(ManagementPath PlannedSystem, string SnapshotFolder, out ManagementPath[] ImportedSnapshots, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ImportSnapshotDefinitions");
                inParams["PlannedSystem"] = PlannedSystem?.Path;
                inParams["SnapshotFolder"] = SnapshotFolder;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ImportSnapshotDefinitions", inParams, null);
                ImportedSnapshots = null;
                if (outParams.Properties["ImportedSnapshots"] != null)
                {
                    int len = ((Array)outParams.Properties["ImportedSnapshots"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter = iCounter + 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["ImportedSnapshots"].Value).GetValue(iCounter).ToString());
                    }
                    ImportedSnapshots = arrToRet;
                }
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ImportedSnapshots = null;
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ImportSystemDefinition(bool GenerateNewSystemIdentifier, string SnapshotFolder, string SystemDefinitionFile, out ManagementPath ImportedSystem, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ImportSystemDefinition");
                inParams["GenerateNewSystemIdentifier"] = GenerateNewSystemIdentifier;
                inParams["SnapshotFolder"] = SnapshotFolder;
                inParams["SystemDefinitionFile"] = SystemDefinitionFile;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ImportSystemDefinition", inParams, null);
                ImportedSystem = null;
                if (outParams.Properties["ImportedSystem"] != null)
                {
                    ImportedSystem = new ManagementPath(outParams.Properties["ImportedSystem"].ToString());
                }
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                ImportedSystem = null;
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ModifyDiskMergeSettings(string SettingData, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ModifyDiskMergeSettings");
                inParams["SettingData"] = SettingData;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ModifyDiskMergeSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ModifyFeatureSettings(string[] FeatureSettings, out ManagementPath Job, out ManagementPath[] ResultingFeatureSettings)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ModifyFeatureSettings");
                inParams["FeatureSettings"] = FeatureSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ModifyFeatureSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingFeatureSettings = null;
                if (outParams.Properties["ResultingFeatureSettings"] != null)
                {
                    int len = ((Array)outParams.Properties["ResultingFeatureSettings"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter = iCounter + 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["ResultingFeatureSettings"].Value).GetValue(iCounter).ToString());
                    }
                    ResultingFeatureSettings = arrToRet;
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingFeatureSettings = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ModifyGuestServiceSettings(string[] GuestServiceSettings, out ManagementPath Job, out ManagementPath[] ResultingGuestServiceSettings)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ModifyGuestServiceSettings");
                inParams["GuestServiceSettings"] = GuestServiceSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ModifyGuestServiceSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingGuestServiceSettings = null;
                if (outParams.Properties["ResultingGuestServiceSettings"] != null)
                {
                    int len = ((Array)outParams.Properties["ResultingGuestServiceSettings"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["ResultingGuestServiceSettings"].Value).GetValue(iCounter).ToString());
                    }
                    ResultingGuestServiceSettings = arrToRet;
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingGuestServiceSettings = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ModifyKvpItems(string[] DataItems, ManagementPath TargetSystem, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ModifyKvpItems");
                inParams["DataItems"] = DataItems;
                inParams["TargetSystem"] = TargetSystem?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ModifyKvpItems", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ModifyResourceSettings(string[] ResourceSettings, out ManagementPath Job, out ManagementPath[] ResultingResourceSettings)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ModifyResourceSettings");
                inParams["ResourceSettings"] = ResourceSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ModifyResourceSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingResourceSettings = null;
                if (outParams.Properties["ResultingResourceSettings"] != null)
                {
                    int len = ((Array)outParams.Properties["ResultingResourceSettings"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["ResultingResourceSettings"].Value).GetValue(iCounter).ToString());
                    }
                    ResultingResourceSettings = arrToRet;
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingResourceSettings = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ModifyServiceSettings(string SettingData, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ModifyServiceSettings");
                inParams["SettingData"] = SettingData;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ModifyServiceSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ModifySystemComponentSettings(string[] ComponentSettings, out ManagementPath Job, out ManagementPath[] ResultingComponentSettings)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ModifySystemComponentSettings");
                inParams["ComponentSettings"] = ComponentSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ModifySystemComponentSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingComponentSettings = null;
                if (outParams.Properties["ResultingComponentSettings"] != null)
                {
                    int len = ((Array)outParams.Properties["ResultingComponentSettings"].Value).Length;
                    ManagementPath[] arrToRet = new ManagementPath[len];
                    for (int iCounter = 0; iCounter < len; iCounter = iCounter + 1)
                    {
                        arrToRet[iCounter] = new ManagementPath(((Array)outParams.Properties["ResultingComponentSettings"].Value).GetValue(iCounter).ToString());
                    }
                    ResultingComponentSettings = arrToRet;
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingComponentSettings = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ModifySystemSettings(string SystemSettings, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ModifySystemSettings");
                inParams["SystemSettings"] = SystemSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ModifySystemSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RealizePlannedSystem(ManagementPath PlannedSystem, out ManagementPath Job, out ManagementPath ResultingSystem)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RealizePlannedSystem");
                inParams["PlannedSystem"] = PlannedSystem?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RealizePlannedSystem", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingSystem = null;
                if (outParams.Properties["ResultingSystem"] != null)
                {
                    ResultingSystem = new ManagementPath(outParams["ResultingSystem"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                ResultingSystem = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RemoveBootSourceSettings(ManagementPath[] BootSourceSettings, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RemoveBootSourceSettings");
                if (BootSourceSettings != null)
                {
                    int len = BootSourceSettings.Length;
                    string[] arrProp = new string[len];
                    for (int iCounter = 0; iCounter < len; iCounter = iCounter + 1)
                    {
                        arrProp[iCounter] = ((ManagementPath)BootSourceSettings.GetValue(iCounter)).Path;
                    }
                    inParams["BootSourceSettings"] = arrProp;
                }
                else
                {
                    inParams["BootSourceSettings"] = null;
                }
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RemoveBootSourceSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RemoveFeatureSettings(ManagementPath[] FeatureSettings, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RemoveFeatureSettings");
                if (FeatureSettings != null)
                {
                    int len = FeatureSettings.Length;
                    string[] arrProp = new string[len];
                    for (int iCounter = 0; iCounter < len; iCounter = iCounter + 1)
                    {
                        arrProp[iCounter] = ((ManagementPath)FeatureSettings.GetValue(iCounter)).Path;
                    }
                    inParams["FeatureSettings"] = arrProp;
                }
                else
                {
                    inParams["FeatureSettings"] = null;
                }
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RemoveFeatureSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RemoveFibreChannelChap(string[] FcPortSettings)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RemoveFibreChannelChap");
                inParams["FcPortSettings"] = FcPortSettings;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RemoveFibreChannelChap", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint RemoveGuestServiceSettings(ManagementPath[] GuestServiceSettings, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RemoveGuestServiceSettings");
                if (GuestServiceSettings != null)
                {
                    int len = GuestServiceSettings.Length;
                    string[] arrProp = new string[len];
                    for (int iCounter = 0; iCounter < len; iCounter = iCounter + 1)
                    {
                        arrProp[iCounter] = ((ManagementPath)GuestServiceSettings.GetValue(iCounter)).Path;
                    }
                    inParams["GuestServiceSettings"] = arrProp;
                }
                else
                {
                    inParams["GuestServiceSettings"] = null;
                }
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RemoveGuestServiceSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RemoveKvpItems(string[] DataItems, ManagementPath TargetSystem, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RemoveKvpItems");
                inParams["DataItems"] = DataItems;
                inParams["TargetSystem"] = TargetSystem?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RemoveKvpItems", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RemoveResourceSettings(ManagementPath[] ResourceSettings, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RemoveResourceSettings");
                if (ResourceSettings != null)
                {
                    int len = ResourceSettings.Length;
                    string[] arrProp = new string[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrProp[iCounter] = ((ManagementPath)ResourceSettings.GetValue(iCounter)).Path;
                    }
                    inParams["ResourceSettings"] = arrProp;
                }
                else
                {
                    inParams["ResourceSettings"] = null;
                }
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RemoveResourceSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RemoveSystemComponentSettings(ManagementPath[] ComponentSettings, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RemoveSystemComponentSettings");
                if (ComponentSettings != null)
                {
                    int len = ComponentSettings.Length;
                    string[] arrProp = new string[len];
                    for (int iCounter = 0; iCounter < len; iCounter += 1)
                    {
                        arrProp[iCounter] = ((ManagementPath)ComponentSettings.GetValue(iCounter)).Path;
                    }
                    inParams["ComponentSettings"] = arrProp;
                }
                else
                {
                    inParams["ComponentSettings"] = null;
                }
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RemoveSystemComponentSettings", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint RequestStateChange(ushort RequestedState, DateTime TimeoutPeriod, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("RequestStateChange");
                inParams[nameof(RequestedState)] = RequestedState;
                inParams["TimeoutPeriod"] = ToDmtfDateTime(TimeoutPeriod);
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RequestStateChange", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint SetGuestNetworkAdapterConfiguration(ManagementPath ComputerSystem, string[] NetworkConfiguration, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("SetGuestNetworkAdapterConfiguration");
                inParams["ComputerSystem"] = ComputerSystem?.Path;
                inParams["NetworkConfiguration"] = NetworkConfiguration;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetGuestNetworkAdapterConfiguration", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint SetInitialMachineConfigurationData(byte[] ImcData, ManagementPath TargetSystem, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("SetInitialMachineConfigurationData");
                inParams["ImcData"] = ImcData;
                inParams["TargetSystem"] = TargetSystem?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetInitialMachineConfigurationData", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint StartService()
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("StartService", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint StopService()
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("StopService", inParams, null);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return Convert.ToUInt32(0);
            }
        }

        public uint TestNetworkConnection(uint IsolationId, bool IsSender, string ReceiverIP, string ReceiverMac, string SenderIP, uint SequenceNumber, ManagementPath TargetNetworkAdapter, out ManagementPath Job, out uint RoundTripTime)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("TestNetworkConnection");
                inParams["IsolationId"] = IsolationId;
                inParams["IsSender"] = IsSender;
                inParams["ReceiverIP"] = ReceiverIP;
                inParams["ReceiverMac"] = ReceiverMac;
                inParams["SenderIP"] = SenderIP;
                inParams["SequenceNumber"] = SequenceNumber;
                inParams["TargetNetworkAdapter"] = TargetNetworkAdapter?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("TestNetworkConnection", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                RoundTripTime = Convert.ToUInt32(outParams.Properties["RoundTripTime"].Value);
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                RoundTripTime = Convert.ToUInt32(0);
                return Convert.ToUInt32(0);
            }
        }

        public uint UpgradeSystemVersion(ManagementPath ComputerSystem, string UpgradeSettingData, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("UpgradeSystemVersion");
                inParams["ComputerSystem"] = ComputerSystem?.Path;
                inParams["UpgradeSettingData"] = UpgradeSettingData;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("UpgradeSystemVersion", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint ValidatePlannedSystem(ManagementPath PlannedSystem, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ValidatePlannedSystem");
                inParams["PlannedSystem"] = PlannedSystem?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ValidatePlannedSystem", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        // Enumerator implementation for enumerating instances of the class.
        public class VirtualSystemManagementServiceCollection : object, ICollection
        {
            private readonly ManagementObjectCollection privColObj;

            public VirtualSystemManagementServiceCollection(ManagementObjectCollection objCollection) => privColObj = objCollection;

            public virtual int Count => privColObj.Count;

            public virtual bool IsSynchronized => privColObj.IsSynchronized;

            public virtual object SyncRoot => this;

            public virtual void CopyTo(Array array, int index)
            {
                privColObj.CopyTo(array, index);
                int nCtr;
                for (nCtr = 0; nCtr < array?.Length; nCtr += 1)
                {
                    using (ManagementObject theObj = (ManagementObject)array.GetValue(nCtr))
                    {
                        using (var vsmsObj = new VirtualSystemManagementService(theObj))
                        {
                            array.SetValue(vsmsObj, nCtr);
                        }
                    }
                }
            }

            public virtual IEnumerator GetEnumerator() => new VirtualSystemManagementServiceEnumerator(privColObj.GetEnumerator());

            public class VirtualSystemManagementServiceEnumerator : object, IEnumerator
            {
                private readonly ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;

                public VirtualSystemManagementServiceEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum) => privObjEnum = objEnum;

                public virtual object Current => new VirtualSystemManagementService((ManagementObject)privObjEnum.Current);

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
            public int GENUS => (int)PrivateLateBoundObject[$"__{nameof(GENUS)}"];
            [Browsable(true)]
            public string CLASS => (string)PrivateLateBoundObject[$"__{nameof(CLASS)}"];
            [Browsable(true)]
            public string SUPERCLASS => (string)PrivateLateBoundObject[$"__{nameof(SUPERCLASS)}"];
            [Browsable(true)]
            public string DYNASTY => (string)PrivateLateBoundObject[$"__{nameof(DYNASTY)}"];
            [Browsable(true)]
            public string RELPATH => (string)PrivateLateBoundObject[$"__{nameof(RELPATH)}"];
            [Browsable(true)]
            public int PROPERTY_COUNT => (int)PrivateLateBoundObject[$"__{nameof(PROPERTY_COUNT)}"];
            [Browsable(true)]
            public string[] DERIVATION => (string[])PrivateLateBoundObject[$"__{nameof(DERIVATION)}"];
            [Browsable(true)]
            public string SERVER => (string)PrivateLateBoundObject[$"__{nameof(SERVER)}"];
            [Browsable(true)]
            public string NAMESPACE => (string)PrivateLateBoundObject[$"__{nameof(NAMESPACE)}"];
            [Browsable(true)]
            public string PATH => (string)PrivateLateBoundObject[$"__{nameof(PATH)}"];
        }
    }
}
