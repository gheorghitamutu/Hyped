using System;
using System.Globalization;
using System.Management;

namespace Viridian.Msvm
{
    public class MsvmBase : IDisposable
    {
        // Property returns the namespace of the WMI class.
        public static string OriginatingNamespace => Properties.Environment.Default.Virtualization;

        public string CreatedClassName { get; protected set; }

        // Underlying lateBound WMI object.
        protected ManagementObject PrivateLateBoundObject { get; set; }

        // Flag to indicate if the instance is an embedded object.
        protected bool IsEmbedded { get; set; }

        // Property to show the commit behavior for the WMI object. If true, WMI object will be automatically saved after each property modification.(ie. Put() is called after modification of a property).
        public bool AutoCommit { get; set; }  
        
        // Property pointing to an embedded object to get System properties of the WMI object.
        public ManagementSystemProperties SystemProperties { get; protected set; }  
        
        // Property returning the underlying lateBound object.
        public ManagementBaseObject LateBoundObject { get; protected set; }

        // Private variable to hold the embedded property representing the instance.
        private readonly ManagementBaseObject embeddedObj;

        // Public static scope property which is used by the various methods.
        public static ManagementScope StaticScope { get; set; } = null;

        #region Constructors

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public MsvmBase(string ClassName)
        {
            CreatedClassName = ClassName;
            InitializeObject(null, null, null);
        }

        public MsvmBase(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName, string ClassName)
        {
            CreatedClassName = ClassName;
            InitializeObject(null, new ManagementPath(ConstructPath(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName)), null);
        }

        public MsvmBase(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName, string ClassName)
        {
            CreatedClassName = ClassName;
            InitializeObject(mgmtScope, new ManagementPath(ConstructPath(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName)), null);
        }

        public MsvmBase(ManagementPath path, ObjectGetOptions getOptions, string ClassName)
        {
            CreatedClassName = ClassName;
            InitializeObject(null, path, getOptions);
        }

        public MsvmBase(ManagementScope mgmtScope, ManagementPath path, string ClassName)
        {
            CreatedClassName = ClassName;
            InitializeObject(mgmtScope, path, null);
        }

        public MsvmBase(ManagementPath path, string ClassName)
        {
            CreatedClassName = ClassName;
            InitializeObject(null, path, null);
        }

        public MsvmBase(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions, string ClassName)
        {
            CreatedClassName = ClassName;
            InitializeObject(mgmtScope, path, getOptions);
        }

        public MsvmBase(ManagementObject theObject, string ClassName)
        {
            CreatedClassName = ClassName;
            Initialize();
            if (theObject != null && CheckIfProperClass(theObject) == true)
            {
                PrivateLateBoundObject = theObject;
                SystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
                LateBoundObject = PrivateLateBoundObject;
            }
            else
            {
                throw new ArgumentException(Properties.Exceptions.Default.ClassNameExceptionMessage);
            }
        }

        public MsvmBase(ManagementBaseObject theObject, string ClassName)
        {
            CreatedClassName = ClassName;
            Initialize();
            if (theObject != null && CheckIfProperClass(theObject) == true)
            {
                embeddedObj = theObject;
                SystemProperties = new ManagementSystemProperties(theObject);
                LateBoundObject = embeddedObj;
                IsEmbedded = true;
            }
            else
            {
                throw new ArgumentException(Properties.Exceptions.Default.ClassNameExceptionMessage);
            }
        }

        #endregion

        private void Initialize()
        {
            AutoCommit = true;
            IsEmbedded = false;
        }

        protected void InitializeObject(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions)
        {
            Initialize();
            if (path != null)
            {
                if (CheckIfProperClass(mgmtScope, path, getOptions) != true)
                {
                    throw new ArgumentException(Properties.Exceptions.Default.ClassNameExceptionMessage);
                }
            }
            PrivateLateBoundObject = new ManagementObject(mgmtScope, path, getOptions);
            SystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            LateBoundObject = PrivateLateBoundObject;
        }

        // ManagementScope of the object.
        public ManagementScope Scope
        {
            get { return IsEmbedded == false ? PrivateLateBoundObject.Scope : null; }
            set
            {
                if (IsEmbedded == false)
                {
                    PrivateLateBoundObject.Scope = value;
                }
            }
        }

        // The ManagementPath of the underlying WMI object.
        public ManagementPath Path
        {
            get
            {
                return IsEmbedded == false ? PrivateLateBoundObject.Path : null;
            }
            set
            {
                if (IsEmbedded == false)
                {
                    if (CheckIfProperClass(null, value, null) != true)
                    {
                        throw new ArgumentException(Properties.Exceptions.Default.ClassNameExceptionMessage);
                    }
                    PrivateLateBoundObject.Path = value;
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

        protected bool CheckIfProperClass(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions OptionsParam)
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

        public void CommitObject()
        {
            if (IsEmbedded == false)
            {
                PrivateLateBoundObject.Put();
            }
        }

        public void CommitObject(PutOptions putOptions)
        {
            if (IsEmbedded == false)
            {
                PrivateLateBoundObject.Put(putOptions);
            }
        }
        public void Delete() => PrivateLateBoundObject.Delete();

        private string ConstructPath(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName)
        {
            string strPath = $"{OriginatingNamespace}:{CreatedClassName}";
            strPath = string.Concat(strPath, string.Concat(".CreationClassName=", string.Concat("\"", string.Concat(keyCreationClassName, "\""))));
            strPath = string.Concat(strPath, string.Concat(",Name=", string.Concat("\"", string.Concat(keyName, "\""))));
            strPath = string.Concat(strPath, string.Concat(",SystemCreationClassName=", string.Concat("\"", string.Concat(keySystemCreationClassName, "\""))));
            strPath = string.Concat(strPath, string.Concat(",SystemName=", string.Concat("\"", string.Concat(keySystemName, "\""))));
            return strPath;
        }

        #region Instantiation

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static ManagementObjectCollection GetInstances(string ClassName) => GetInstances(null, null, null, ClassName);

        public static ManagementObjectCollection GetInstances(string condition, string ClassName) => GetInstances(null, condition, null, ClassName);

        public static ManagementObjectCollection GetInstances(string[] selectedProperties, string ClassName) => GetInstances(null, null, selectedProperties, ClassName);

        public static ManagementObjectCollection GetInstances(string condition, string[] selectedProperties, string ClassName) => GetInstances(null, condition, selectedProperties, ClassName);

        public static ManagementObjectCollection GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions, string ClassName)
        {
            if (mgmtScope == null)
            {
                if (StaticScope == null)
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = OriginatingNamespace;
                }
                else
                {
                    mgmtScope = StaticScope;
                }
            }
            ManagementPath pathObj = new ManagementPath
            {
                ClassName = ClassName,
                NamespacePath = OriginatingNamespace
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
                return clsObject.GetInstances(enumOptions);
            }
        }

        public static ManagementObjectCollection GetInstances(ManagementScope mgmtScope, string condition, string ClassName) => GetInstances(mgmtScope, condition, null, ClassName);

        public static ManagementObjectCollection GetInstances(ManagementScope mgmtScope, string[] selectedProperties, string ClassName) => GetInstances(mgmtScope, null, selectedProperties, ClassName);

        public static ManagementObjectCollection GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties, string ClassName)
        {
            if (mgmtScope == null)
            {
                if (StaticScope == null)
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = OriginatingNamespace;
                }
                else
                {
                    mgmtScope = StaticScope;
                }
            }
            using (ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery(ClassName, condition, selectedProperties)))
            {
                EnumerationOptions enumOptions = new EnumerationOptions
                {
                    EnsureLocatable = true
                };
                ObjectSearcher.Options = enumOptions;
                return ObjectSearcher.Get();
            }
        }

        public static ManagementObject CreateInstance(string ClassName)
        {
            ManagementScope mgmtScope;
            if (StaticScope == null)
            {
                mgmtScope = new ManagementScope();
                mgmtScope.Path.NamespacePath = OriginatingNamespace;
            }
            else
            {
                mgmtScope = StaticScope;
            }

            ManagementPath mgmtPath = new ManagementPath(ClassName);
            using (ManagementClass tmpMgmtClass = new ManagementClass(mgmtScope, mgmtPath, null))
            {
                return tmpMgmtClass.CreateInstance();
            }
        }

        #endregion

        #region DateTime

        // Converts a given datetime in DMTF format to System.DateTime object.
        protected static DateTime ToDateTime(string dmtfDate)
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
        protected static string ToDmtfDateTime(DateTime date)
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

        // Converts a given time interval in DMTF format to System.TimeSpan object.
        protected static TimeSpan ToTimeSpan(string dmtfTimespan)
        {
            if (dmtfTimespan == null)
            {
                throw new ArgumentOutOfRangeException(dmtfTimespan);
            }
            if (dmtfTimespan.Length == 0)
            {
                throw new ArgumentOutOfRangeException(dmtfTimespan);
            }
            if (dmtfTimespan.Length != 25)
            {
                throw new ArgumentOutOfRangeException(dmtfTimespan);
            }
            if (dmtfTimespan.Substring(21, 4) != ":000")
            {
                throw new ArgumentOutOfRangeException(dmtfTimespan);
            }
            int days;
            int hours;
            int minutes;
            int seconds;
            long ticks;
            try
            {
                string tempString = string.Empty;
                tempString = dmtfTimespan.Substring(0, 8);
                days = int.Parse(tempString);
                tempString = dmtfTimespan.Substring(8, 2);
                hours = int.Parse(tempString);
                tempString = dmtfTimespan.Substring(10, 2);
                minutes = int.Parse(tempString);
                tempString = dmtfTimespan.Substring(12, 2);
                seconds = int.Parse(tempString);
                tempString = dmtfTimespan.Substring(15, 6);
                ticks = long.Parse(tempString) * (TimeSpan.TicksPerMillisecond / 1000);
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException(null, e.Message);
            }
            TimeSpan timespan = new TimeSpan(days, hours, minutes, seconds, 0);
            TimeSpan tsTemp = TimeSpan.FromTicks(ticks);
            timespan = timespan.Add(tsTemp);
            return timespan;
        }

        // Converts a given System.TimeSpan object to DMTF Time interval format.
        protected static string ToDmtfTimeInterval(TimeSpan timespan)
        {
            string dmtftimespan = timespan.Days.ToString().PadLeft(8, '0');
            TimeSpan maxTimeSpan = TimeSpan.MaxValue;
            if (timespan.Days > maxTimeSpan.Days)
            {
                throw new ArgumentOutOfRangeException(timespan.ToString());
            }
            TimeSpan minTimeSpan = TimeSpan.MinValue;
            if (timespan.Days < minTimeSpan.Days)
            {
                throw new ArgumentOutOfRangeException(timespan.ToString());
            }
            dmtftimespan = string.Concat(dmtftimespan, timespan.Hours.ToString().PadLeft(2, '0'));
            dmtftimespan = string.Concat(dmtftimespan, timespan.Minutes.ToString().PadLeft(2, '0'));
            dmtftimespan = string.Concat(dmtftimespan, timespan.Seconds.ToString().PadLeft(2, '0'));
            dmtftimespan = string.Concat(dmtftimespan, ".");
            TimeSpan tsTemp = new TimeSpan(timespan.Days, timespan.Hours, timespan.Minutes, timespan.Seconds, 0);
            long microsec = (timespan.Ticks - tsTemp.Ticks)
                        * 1000
                        / TimeSpan.TicksPerMillisecond;
            string strMicroSec = microsec.ToString();
            if (strMicroSec.Length > 6)
            {
                strMicroSec = strMicroSec.Substring(0, 6);
            }
            dmtftimespan = string.Concat(dmtftimespan, strMicroSec.PadLeft(6, '0'));
            dmtftimespan = string.Concat(dmtftimespan, ":000");
            return dmtftimespan;
        }

        #endregion

        #region ClassCleanup

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

        ~MsvmBase()
        {
            Dispose(false);
        }

        #endregion
    }
}
