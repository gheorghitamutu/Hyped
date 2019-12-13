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
    // An Early Bound class generated for the WMI class.Msvm_VirtualSystemSnapshotService
    public class VirtualSystemSnapshotService : Component
    {
        // Private property to hold the WMI namespace in which the class resides.
        private static string CreatedWmiNamespace = "root\\virtualization\\v2";

        // Private property to hold the name of WMI class which created this class.
        private static string CreatedClassName = "Msvm_VirtualSystemSnapshotService";

        // Underlying lateBound WMI object.
        private ManagementObject PrivateLateBoundObject;

        // Private variable to hold the embedded property representing the instance.
        private readonly ManagementBaseObject embeddedObj;

        // Flag to indicate if the instance is an embedded object.
        private bool isEmbedded;

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public VirtualSystemSnapshotService() => InitializeObject(null, null, null);

        public VirtualSystemSnapshotService(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) => InitializeObject(null, new ManagementPath(ConstructPath(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName)), null);

        public VirtualSystemSnapshotService(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) => InitializeObject(mgmtScope, new ManagementPath(ConstructPath(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName)), null);

        public VirtualSystemSnapshotService(ManagementPath path, ObjectGetOptions getOptions) => InitializeObject(null, path, getOptions);

        public VirtualSystemSnapshotService(ManagementScope mgmtScope, ManagementPath path) => InitializeObject(mgmtScope, path, null);

        public VirtualSystemSnapshotService(ManagementPath path) => InitializeObject(null, path, null);

        public VirtualSystemSnapshotService(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) => InitializeObject(mgmtScope, path, getOptions);

        public VirtualSystemSnapshotService(ManagementObject theObject)
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

        public VirtualSystemSnapshotService(ManagementBaseObject theObject)
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
        public string OriginatingNamespace
        {
            get
            {
                return "root\\virtualization\\v2";
            }
        }

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
                        if ((strRet == null)
                                    || (strRet == string.Empty))
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
        public ushort[] AvailableRequestedStates
        {
            get
            {
                return (ushort[])LateBoundObject[nameof(AvailableRequestedStates)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Caption
        {
            get
            {
                return (string)LateBoundObject[nameof(Caption)];
            }
        }

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
        public string CreationClassName
        {
            get
            {
                return (string)LateBoundObject[nameof(CreationClassName)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Description
        {
            get
            {
                return (string)LateBoundObject[nameof(Description)];
            }
        }

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
        public string ElementName
        {
            get
            {
                return (string)LateBoundObject[nameof(ElementName)];
            }
        }

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
        public string InstanceID
        {
            get
            {
                return (string)LateBoundObject[nameof(InstanceID)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Name
        {
            get
            {
                return (string)LateBoundObject[nameof(Name)];
            }
        }

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
        public ushort[] OperationalStatus
        {
            get
            {
                return (ushort[])LateBoundObject[nameof(OperationalStatus)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OtherEnabledState
        {
            get
            {
                return (string)LateBoundObject[nameof(OtherEnabledState)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PrimaryOwnerContact
        {
            get
            {
                return (string)LateBoundObject[nameof(PrimaryOwnerContact)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PrimaryOwnerName
        {
            get
            {
                return (string)LateBoundObject[nameof(PrimaryOwnerName)];
            }
        }

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
        public string StartMode
        {
            get
            {
                return (string)LateBoundObject[nameof(StartMode)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Status
        {
            get
            {
                return (string)LateBoundObject[nameof(Status)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] StatusDescriptions
        {
            get
            {
                return (string[])LateBoundObject[nameof(StatusDescriptions)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SystemCreationClassName
        {
            get
            {
                return (string)LateBoundObject[nameof(SystemCreationClassName)];
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SystemName
        {
            get
            {
                return (string)LateBoundObject[nameof(SystemName)];
            }
        }

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
            if ((path != null)
                        && (string.Compare(path.ClassName, ManagementClassName, true, CultureInfo.InvariantCulture) == 0))
            {
                return true;
            }
            else
            {
                return CheckIfProperClass(new ManagementObject(mgmtScope, path, OptionsParam));
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
                throw new ArgumentOutOfRangeException();
            }
            if (dmtf.Length == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (dmtf.Length != 25)
            {
                throw new ArgumentOutOfRangeException();
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
                    throw new ArgumentOutOfRangeException();
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
            string strPath = "root\\virtualization\\v2:Msvm_VirtualSystemSnapshotService";
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
                    throw new ArgumentException("Class name does not match.");
                }
            }
            PrivateLateBoundObject = new ManagementObject(mgmtScope, path, getOptions);
            SystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            LateBoundObject = PrivateLateBoundObject;
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static VirtualSystemSnapshotServiceCollection GetInstances()
        {
            return GetInstances(null, null, null);
        }

        public static VirtualSystemSnapshotServiceCollection GetInstances(string condition)
        {
            return GetInstances(null, condition, null);
        }

        public static VirtualSystemSnapshotServiceCollection GetInstances(string[] selectedProperties)
        {
            return GetInstances(null, null, selectedProperties);
        }

        public static VirtualSystemSnapshotServiceCollection GetInstances(string condition, string[] selectedProperties)
        {
            return GetInstances(null, condition, selectedProperties);
        }

        public static VirtualSystemSnapshotServiceCollection GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions)
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
            ManagementPath pathObj = new ManagementPath();
            pathObj.ClassName = "Msvm_VirtualSystemSnapshotService";
            pathObj.NamespacePath = "root\\virtualization\\v2";
            ManagementClass clsObject = new ManagementClass(mgmtScope, pathObj, null);
            if (enumOptions == null)
            {
                enumOptions = new EnumerationOptions();
                enumOptions.EnsureLocatable = true;
            }
            return new VirtualSystemSnapshotServiceCollection(clsObject.GetInstances(enumOptions));
        }

        public static VirtualSystemSnapshotServiceCollection GetInstances(ManagementScope mgmtScope, string condition)
        {
            return GetInstances(mgmtScope, condition, null);
        }

        public static VirtualSystemSnapshotServiceCollection GetInstances(ManagementScope mgmtScope, string[] selectedProperties)
        {
            return GetInstances(mgmtScope, null, selectedProperties);
        }

        public static VirtualSystemSnapshotServiceCollection GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties)
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
            ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery("Msvm_VirtualSystemSnapshotService", condition, selectedProperties));
            EnumerationOptions enumOptions = new EnumerationOptions();
            enumOptions.EnsureLocatable = true;
            ObjectSearcher.Options = enumOptions;
            return new VirtualSystemSnapshotServiceCollection(ObjectSearcher.Get());
        }

        [Browsable(true)]
        public static VirtualSystemSnapshotService CreateInstance()
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
            ManagementClass tmpMgmtClass = new ManagementClass(mgmtScope, mgmtPath, null);
            return new VirtualSystemSnapshotService(tmpMgmtClass.CreateInstance());
        }

        [Browsable(true)]
        public void Delete()
        {
            PrivateLateBoundObject.Delete();
        }

        public uint ApplySnapshot(ManagementPath Snapshot, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ApplySnapshot");
                inParams["Snapshot"] = Snapshot?.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ApplySnapshot", inParams, null);
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

        public uint ClearSnapshotState(ManagementPath SnapshotSettingData, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ClearSnapshotState");
                inParams["SnapshotSettingData"] = SnapshotSettingData.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ClearSnapshotState", inParams, null);
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

        public uint ConvertToReferencePoint(ManagementPath AffectedSnapshot, string ReferencePointSettings, ref ManagementPath ResultingReferencePoint, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("ConvertToReferencePoint");
                inParams["AffectedSnapshot"] = AffectedSnapshot.Path;
                inParams["ReferencePointSettings"] = ReferencePointSettings;
                inParams["ResultingReferencePoint"] = ResultingReferencePoint.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ConvertToReferencePoint", inParams, null);
                Job = null;
                if (outParams.Properties["Job"] != null)
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                ResultingReferencePoint = (ManagementPath)outParams.Properties["ResultingReferencePoint"].Value;
                return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint CreateSnapshot(ManagementPath AffectedSystem, ref ManagementPath ResultingSnapshot, string SnapshotSettings, ushort SnapshotType, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                using (ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("CreateSnapshot"))
                {
                    inParams["AffectedSystem"] = AffectedSystem?.Path;
                    inParams["ResultingSnapshot"] = ResultingSnapshot?.Path;
                    inParams["SnapshotSettings"] = SnapshotSettings;
                    inParams["SnapshotType"] = SnapshotType;
                    using (ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("CreateSnapshot", inParams, null))
                    {
                        Job = null;
                        if (outParams.Properties["Job"] != null)
                        {
                            Job = new ManagementPath(outParams["Job"] as string);
                        }
                        ResultingSnapshot = (ManagementPath)outParams.Properties["ResultingSnapshot"].Value;
                        return Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
                    }
                }
            }
            else
            {
                Job = null;
                return Convert.ToUInt32(0);
            }
        }

        public uint DestroySnapshot(ManagementPath AffectedSnapshot, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("DestroySnapshot");
                inParams["AffectedSnapshot"] = AffectedSnapshot.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DestroySnapshot", inParams, null);
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

        public uint DestroySnapshotTree(ManagementPath SnapshotSettingData, out ManagementPath Job)
        {
            if (isEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("DestroySnapshotTree");
                inParams["SnapshotSettingData"] = SnapshotSettingData.Path;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("DestroySnapshotTree", inParams, null);
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

        // Enumerator implementation for enumerating instances of the class.
        public class VirtualSystemSnapshotServiceCollection : object, ICollection
        {

            private ManagementObjectCollection privColObj;

            public VirtualSystemSnapshotServiceCollection(ManagementObjectCollection objCollection)
            {
                privColObj = objCollection;
            }

            public virtual int Count
            {
                get
                {
                    return privColObj.Count;
                }
            }

            public virtual bool IsSynchronized
            {
                get
                {
                    return privColObj.IsSynchronized;
                }
            }

            public virtual object SyncRoot
            {
                get
                {
                    return this;
                }
            }

            public virtual void CopyTo(Array array, int index)
            {
                privColObj.CopyTo(array, index);
                int nCtr;
                for (nCtr = 0; nCtr < array.Length; nCtr = nCtr + 1)
                {
                    array.SetValue(new VirtualSystemSnapshotService((ManagementObject)array.GetValue(nCtr)), nCtr);
                }
            }

            public virtual IEnumerator GetEnumerator()
            {
                return new VirtualSystemSnapshotServiceEnumerator(privColObj.GetEnumerator());
            }

            public class VirtualSystemSnapshotServiceEnumerator : object, IEnumerator
            {

                private ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;

                public VirtualSystemSnapshotServiceEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum)
                {
                    privObjEnum = objEnum;
                }

                public virtual object Current
                {
                    get
                    {
                        return new VirtualSystemSnapshotService((ManagementObject)privObjEnum.Current);
                    }
                }

                public virtual bool MoveNext()
                {
                    return privObjEnum.MoveNext();
                }

                public virtual void Reset()
                {
                    privObjEnum.Reset();
                }
            }
        }

        // TypeConverter to handle null values for ValueType properties
        public class WMIValueTypeConverter : TypeConverter
        {

            private TypeConverter baseConverter;

            private Type baseType;

            public WMIValueTypeConverter(Type inBaseType)
            {
                baseConverter = TypeDescriptor.GetConverter(inBaseType);
                baseType = inBaseType;
            }

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type srcType)
            {
                return baseConverter.CanConvertFrom(context, srcType);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return baseConverter.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                return baseConverter.ConvertFrom(context, culture, value);
            }

            public override object CreateInstance(ITypeDescriptorContext context, IDictionary dictionary)
            {
                return baseConverter.CreateInstance(context, dictionary);
            }

            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                return baseConverter.GetCreateInstanceSupported(context);
            }

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributeVar)
            {
                return baseConverter.GetProperties(context, value, attributeVar);
            }

            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return baseConverter.GetPropertiesSupported(context);
            }

            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return baseConverter.GetStandardValues(context);
            }

            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return baseConverter.GetStandardValuesExclusive(context);
            }

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return baseConverter.GetStandardValuesSupported(context);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (baseType.BaseType == typeof(Enum))
                {
                    if (value.GetType() == destinationType)
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

            private ManagementBaseObject PrivateLateBoundObject;

            public ManagementSystemProperties(ManagementBaseObject ManagedObject)
            {
                PrivateLateBoundObject = ManagedObject;
            }

            [Browsable(true)]
            public int GENUS
            {
                get
                {
                    return (int)PrivateLateBoundObject["__GENUS"];
                }
            }

            [Browsable(true)]
            public string CLASS
            {
                get
                {
                    return (string)PrivateLateBoundObject["__CLASS"];
                }
            }

            [Browsable(true)]
            public string SUPERCLASS
            {
                get
                {
                    return (string)PrivateLateBoundObject["__SUPERCLASS"];
                }
            }

            [Browsable(true)]
            public string DYNASTY
            {
                get
                {
                    return (string)PrivateLateBoundObject["__DYNASTY"];
                }
            }

            [Browsable(true)]
            public string RELPATH
            {
                get
                {
                    return (string)PrivateLateBoundObject["__RELPATH"];
                }
            }

            [Browsable(true)]
            public int PROPERTY_COUNT
            {
                get
                {
                    return (int)PrivateLateBoundObject["__PROPERTY_COUNT"];
                }
            }

            [Browsable(true)]
            public string[] DERIVATION
            {
                get
                {
                    return (string[])PrivateLateBoundObject["__DERIVATION"];
                }
            }

            [Browsable(true)]
            public string SERVER
            {
                get
                {
                    return (string)PrivateLateBoundObject["__SERVER"];
                }
            }

            [Browsable(true)]
            public string NAMESPACE
            {
                get
                {
                    return (string)PrivateLateBoundObject["__NAMESPACE"];
                }
            }

            [Browsable(true)]
            public string PATH
            {
                get
                {
                    return (string)PrivateLateBoundObject["__PATH"];
                }
            }
        }
    }
}
