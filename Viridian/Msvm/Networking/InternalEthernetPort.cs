﻿using System;
using System.ComponentModel;
using System.Management;
using System.Collections;
using System.Globalization;

namespace Viridian.Msvm.Networking
{
    // Functions ShouldSerialize<PropertyName> are functions used by VS property browser to check if a particular property has to be serialized. These functions are added for all ValueType properties ( properties of type Int32, BOOL etc.. which cannot be set to null). These functions use Is<PropertyName>Null function. These functions are also used in the TypeConverter implementation for the properties to check for NULL value of property so that an empty value can be shown in Property browser in case of Drag and Drop in Visual studio.
    // Functions Is<PropertyName>Null() are used to check if a property is NULL.
    // Functions Reset<PropertyName> are added for Nullable Read/Write properties. These functions are used by VS designer in property browser to set a property to NULL.
    // Every property added to the class for WMI property has attributes set to define its behavior in Visual Studio designer and also to define a TypeConverter to be used.
    // Datetime conversion functions ToDateTime and ToDmtfDateTime are added to the class to convert DMTF datetime to System.DateTime and vice-versa.
    // An Early Bound class generated for the WMI class.Msvm_InternalEthernetPort
    public class InternalEthernetPort : Component
    {

        // Private property to hold the WMI namespace in which the class resides.
        private static string CreatedWmiNamespace = "root\\virtualization\\v2";

        // Private property to hold the name of WMI class which created this class.
        private static string CreatedClassName = "Msvm_InternalEthernetPort";

        // Private member variable to hold the ManagementScope which is used by the various methods.
        private static ManagementScope statMgmtScope = null;

        private ManagementSystemProperties PrivateSystemProperties;

        // Underlying lateBound WMI object.
        private ManagementObject PrivateLateBoundObject;

        // Member variable to store the 'automatic commit' behavior for the class.
        private bool AutoCommitProp;

        // Private variable to hold the embedded property representing the instance.
        private ManagementBaseObject embeddedObj;

        // The current WMI object used
        private ManagementBaseObject curObj;

        // Flag to indicate if the instance is an embedded object.
        private bool isEmbedded;

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public InternalEthernetPort()
        {
            InitializeObject(null, null, null);
        }

        public InternalEthernetPort(string keyCreationClassName, string keyDeviceID, string keySystemCreationClassName, string keySystemName)
        {
            InitializeObject(null, new ManagementPath(InternalEthernetPort.ConstructPath(keyCreationClassName, keyDeviceID, keySystemCreationClassName, keySystemName)), null);
        }

        public InternalEthernetPort(ManagementScope mgmtScope, string keyCreationClassName, string keyDeviceID, string keySystemCreationClassName, string keySystemName)
        {
            InitializeObject(((ManagementScope)(mgmtScope)), new ManagementPath(InternalEthernetPort.ConstructPath(keyCreationClassName, keyDeviceID, keySystemCreationClassName, keySystemName)), null);
        }

        public InternalEthernetPort(ManagementPath path, ObjectGetOptions getOptions)
        {
            InitializeObject(null, path, getOptions);
        }

        public InternalEthernetPort(ManagementScope mgmtScope, ManagementPath path)
        {
            InitializeObject(mgmtScope, path, null);
        }

        public InternalEthernetPort(ManagementPath path)
        {
            InitializeObject(null, path, null);
        }

        public InternalEthernetPort(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions)
        {
            InitializeObject(mgmtScope, path, getOptions);
        }

        public InternalEthernetPort(ManagementObject theObject)
        {
            Initialize();
            if ((CheckIfProperClass(theObject) == true))
            {
                PrivateLateBoundObject = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
                curObj = PrivateLateBoundObject;
            }
            else
            {
                throw new ArgumentException("Class name does not match.");
            }
        }

        public InternalEthernetPort(ManagementBaseObject theObject)
        {
            Initialize();
            if ((CheckIfProperClass(theObject) == true))
            {
                embeddedObj = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(theObject);
                curObj = embeddedObj;
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
                if ((curObj != null))
                {
                    if ((curObj.ClassPath != null))
                    {
                        strRet = ((string)(curObj["__CLASS"]));
                        if (((strRet == null)
                                    || (strRet == string.Empty)))
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
        public ManagementSystemProperties SystemProperties
        {
            get
            {
                return PrivateSystemProperties;
            }
        }

        // Property returning the underlying lateBound object.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementBaseObject LateBoundObject
        {
            get
            {
                return curObj;
            }
        }

        // ManagementScope of the object.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementScope Scope
        {
            get
            {
                if ((isEmbedded == false))
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
                if ((isEmbedded == false))
                {
                    PrivateLateBoundObject.Scope = value;
                }
            }
        }

        // Property to show the commit behavior for the WMI object. If true, WMI object will be automatically saved after each property modification.(ie. Put() is called after modification of a property).
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoCommit
        {
            get
            {
                return AutoCommitProp;
            }
            set
            {
                AutoCommitProp = value;
            }
        }

        // The ManagementPath of the underlying WMI object.
        [Browsable(true)]
        public ManagementPath Path
        {
            get
            {
                if ((isEmbedded == false))
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
                if ((isEmbedded == false))
                {
                    if ((CheckIfProperClass(null, value, null) != true))
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
        public static ManagementScope StaticScope
        {
            get
            {
                return statMgmtScope;
            }
            set
            {
                statMgmtScope = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsActiveMaximumTransmissionUnitNull
        {
            get
            {
                if ((curObj[nameof(ActiveMaximumTransmissionUnit)] == null))
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
        public ulong ActiveMaximumTransmissionUnit
        {
            get
            {
                if ((curObj[nameof(ActiveMaximumTransmissionUnit)] == null))
                {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj[nameof(ActiveMaximumTransmissionUnit)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ushort[] AdditionalAvailability
        {
            get
            {
                return ((ushort[])(curObj[nameof(AdditionalAvailability)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAutoSenseNull
        {
            get
            {
                if ((curObj[nameof(AutoSense)] == null))
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
        public bool AutoSense
        {
            get
            {
                if ((curObj[nameof(AutoSense)] == null))
                {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(AutoSense)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAvailabilityNull
        {
            get
            {
                if ((curObj[nameof(Availability)] == null))
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
        public ushort Availability
        {
            get
            {
                if ((curObj[nameof(Availability)] == null))
                {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(Availability)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ushort[] AvailableRequestedStates
        {
            get
            {
                return ((ushort[])(curObj[nameof(AvailableRequestedStates)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ushort[] Capabilities
        {
            get
            {
                return ((ushort[])(curObj[nameof(Capabilities)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] CapabilityDescriptions
        {
            get
            {
                return ((string[])(curObj[nameof(CapabilityDescriptions)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Caption
        {
            get
            {
                return ((string)(curObj[nameof(Caption)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsCommunicationStatusNull
        {
            get
            {
                if ((curObj[nameof(CommunicationStatus)] == null))
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
                if ((curObj[nameof(CommunicationStatus)] == null))
                {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(CommunicationStatus)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CreationClassName
        {
            get
            {
                return ((string)(curObj[nameof(CreationClassName)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Description
        {
            get
            {
                return ((string)(curObj[nameof(Description)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDetailedStatusNull
        {
            get
            {
                if ((curObj[nameof(DetailedStatus)] == null))
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
                if ((curObj[nameof(DetailedStatus)] == null))
                {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(DetailedStatus)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DeviceID
        {
            get
            {
                return ((string)(curObj[nameof(DeviceID)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ElementName
        {
            get
            {
                return ((string)(curObj[nameof(ElementName)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ushort[] EnabledCapabilities
        {
            get
            {
                return ((ushort[])(curObj[nameof(EnabledCapabilities)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEnabledDefaultNull
        {
            get
            {
                if ((curObj[nameof(EnabledDefault)] == null))
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
                if ((curObj[nameof(EnabledDefault)] == null))
                {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(EnabledDefault)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEnabledStateNull
        {
            get
            {
                if ((curObj[nameof(EnabledState)] == null))
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
                if ((curObj[nameof(EnabledState)] == null))
                {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(EnabledState)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsErrorClearedNull
        {
            get
            {
                if ((curObj[nameof(ErrorCleared)] == null))
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
        public bool ErrorCleared
        {
            get
            {
                if ((curObj[nameof(ErrorCleared)] == null))
                {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(ErrorCleared)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ErrorDescription
        {
            get
            {
                return ((string)(curObj[nameof(ErrorDescription)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFullDuplexNull
        {
            get
            {
                if ((curObj[nameof(FullDuplex)] == null))
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
        public bool FullDuplex
        {
            get
            {
                if ((curObj[nameof(FullDuplex)] == null))
                {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(FullDuplex)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsHealthStateNull
        {
            get
            {
                if ((curObj[nameof(HealthState)] == null))
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
                if ((curObj[nameof(HealthState)] == null))
                {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(HealthState)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] IdentifyingDescriptions
        {
            get
            {
                return ((string[])(curObj[nameof(IdentifyingDescriptions)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInstallDateNull
        {
            get
            {
                if ((curObj[nameof(InstallDate)] == null))
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
                if ((curObj[nameof(InstallDate)] != null))
                {
                    return ToDateTime(((string)(curObj[nameof(InstallDate)])));
                }
                else
                {
                    return System.DateTime.MinValue;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InstanceID
        {
            get
            {
                return ((string)(curObj[nameof(InstanceID)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLastErrorCodeNull
        {
            get
            {
                if ((curObj[nameof(LastErrorCode)] == null))
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
        public uint LastErrorCode
        {
            get
            {
                if ((curObj[nameof(LastErrorCode)] == null))
                {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj[nameof(LastErrorCode)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLinkTechnologyNull
        {
            get
            {
                if ((curObj[nameof(LinkTechnology)] == null))
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
        public ushort LinkTechnology
        {
            get
            {
                if ((curObj[nameof(LinkTechnology)] == null))
                {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(LinkTechnology)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMaxDataSizeNull
        {
            get
            {
                if ((curObj[nameof(MaxDataSize)] == null))
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
        public uint MaxDataSize
        {
            get
            {
                if ((curObj[nameof(MaxDataSize)] == null))
                {
                    return System.Convert.ToUInt32(0);
                }
                return ((uint)(curObj[nameof(MaxDataSize)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMaxQuiesceTimeNull
        {
            get
            {
                if ((curObj[nameof(MaxQuiesceTime)] == null))
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
        public ulong MaxQuiesceTime
        {
            get
            {
                if ((curObj[nameof(MaxQuiesceTime)] == null))
                {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj[nameof(MaxQuiesceTime)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMaxSpeedNull
        {
            get
            {
                if ((curObj[nameof(MaxSpeed)] == null))
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
        public ulong MaxSpeed
        {
            get
            {
                if ((curObj[nameof(MaxSpeed)] == null))
                {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj[nameof(MaxSpeed)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Name
        {
            get
            {
                return ((string)(curObj[nameof(Name)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] NetworkAddresses
        {
            get
            {
                return ((string[])(curObj[nameof(NetworkAddresses)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsOperatingStatusNull
        {
            get
            {
                if ((curObj[nameof(OperatingStatus)] == null))
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
                if ((curObj[nameof(OperatingStatus)] == null))
                {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(OperatingStatus)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ushort[] OperationalStatus
        {
            get
            {
                return ((ushort[])(curObj[nameof(OperationalStatus)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] OtherEnabledCapabilities
        {
            get
            {
                return ((string[])(curObj[nameof(OtherEnabledCapabilities)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OtherEnabledState
        {
            get
            {
                return ((string)(curObj[nameof(OtherEnabledState)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] OtherIdentifyingInfo
        {
            get
            {
                return ((string[])(curObj[nameof(OtherIdentifyingInfo)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OtherLinkTechnology
        {
            get
            {
                return ((string)(curObj[nameof(OtherLinkTechnology)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OtherNetworkPortType
        {
            get
            {
                return ((string)(curObj[nameof(OtherNetworkPortType)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OtherPortType
        {
            get
            {
                return ((string)(curObj[nameof(OtherPortType)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PermanentAddress
        {
            get
            {
                return ((string)(curObj[nameof(PermanentAddress)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPortNumberNull
        {
            get
            {
                if ((curObj[nameof(PortNumber)] == null))
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
        public ushort PortNumber
        {
            get
            {
                if ((curObj[nameof(PortNumber)] == null))
                {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(PortNumber)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPortTypeNull
        {
            get
            {
                if ((curObj[nameof(PortType)] == null))
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
        public ushort PortType
        {
            get
            {
                if ((curObj[nameof(PortType)] == null))
                {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(PortType)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ushort[] PowerManagementCapabilities
        {
            get
            {
                return ((ushort[])(curObj[nameof(PowerManagementCapabilities)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPowerManagementSupportedNull
        {
            get
            {
                if ((curObj[nameof(PowerManagementSupported)] == null))
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
        public bool PowerManagementSupported
        {
            get
            {
                if ((curObj[nameof(PowerManagementSupported)] == null))
                {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj[nameof(PowerManagementSupported)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPowerOnHoursNull
        {
            get
            {
                if ((curObj[nameof(PowerOnHours)] == null))
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
        public ulong PowerOnHours
        {
            get
            {
                if ((curObj[nameof(PowerOnHours)] == null))
                {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj[nameof(PowerOnHours)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPrimaryStatusNull
        {
            get
            {
                if ((curObj[nameof(PrimaryStatus)] == null))
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
                if ((curObj[nameof(PrimaryStatus)] == null))
                {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(PrimaryStatus)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsRequestedSpeedNull
        {
            get
            {
                if ((curObj[nameof(RequestedSpeed)] == null))
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
        public ulong RequestedSpeed
        {
            get
            {
                if ((curObj[nameof(RequestedSpeed)] == null))
                {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj[nameof(RequestedSpeed)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsRequestedStateNull
        {
            get
            {
                if ((curObj[nameof(RequestedState)] == null))
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
                if ((curObj[nameof(RequestedState)] == null))
                {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(RequestedState)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSpeedNull
        {
            get
            {
                if ((curObj[nameof(Speed)] == null))
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
        public ulong Speed
        {
            get
            {
                if ((curObj[nameof(Speed)] == null))
                {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj[nameof(Speed)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Status
        {
            get
            {
                return ((string)(curObj[nameof(Status)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string[] StatusDescriptions
        {
            get
            {
                return ((string[])(curObj[nameof(StatusDescriptions)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsStatusInfoNull
        {
            get
            {
                if ((curObj[nameof(StatusInfo)] == null))
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
        public ushort StatusInfo
        {
            get
            {
                if ((curObj[nameof(StatusInfo)] == null))
                {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(StatusInfo)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSupportedMaximumTransmissionUnitNull
        {
            get
            {
                if ((curObj[nameof(SupportedMaximumTransmissionUnit)] == null))
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
        public ulong SupportedMaximumTransmissionUnit
        {
            get
            {
                if ((curObj[nameof(SupportedMaximumTransmissionUnit)] == null))
                {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj[nameof(SupportedMaximumTransmissionUnit)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SystemCreationClassName
        {
            get
            {
                return ((string)(curObj[nameof(SystemCreationClassName)]));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SystemName
        {
            get
            {
                return ((string)(curObj[nameof(SystemName)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTimeOfLastStateChangeNull
        {
            get
            {
                if ((curObj[nameof(TimeOfLastStateChange)] == null))
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
                if ((curObj[nameof(TimeOfLastStateChange)] != null))
                {
                    return ToDateTime(((string)(curObj[nameof(TimeOfLastStateChange)])));
                }
                else
                {
                    return System.DateTime.MinValue;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTotalPowerOnHoursNull
        {
            get
            {
                if ((curObj[nameof(TotalPowerOnHours)] == null))
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
        public ulong TotalPowerOnHours
        {
            get
            {
                if ((curObj[nameof(TotalPowerOnHours)] == null))
                {
                    return System.Convert.ToUInt64(0);
                }
                return ((ulong)(curObj[nameof(TotalPowerOnHours)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTransitioningToStateNull
        {
            get
            {
                if ((curObj[nameof(TransitioningToState)] == null))
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
                if ((curObj[nameof(TransitioningToState)] == null))
                {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(TransitioningToState)]));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsUsageRestrictionNull
        {
            get
            {
                if ((curObj[nameof(UsageRestriction)] == null))
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
        public ushort UsageRestriction
        {
            get
            {
                if ((curObj[nameof(UsageRestriction)] == null))
                {
                    return System.Convert.ToUInt16(0);
                }
                return ((ushort)(curObj[nameof(UsageRestriction)]));
            }
        }

        private bool CheckIfProperClass(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions OptionsParam)
        {
            if (((path != null)
                        && (string.Compare(path.ClassName, ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0)))
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
            if (((theObj != null)
                        && (string.Compare(((string)(theObj["__CLASS"])), ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0)))
            {
                return true;
            }
            else
            {
                Array parentClasses = ((Array)(theObj["__DERIVATION"]));
                if ((parentClasses != null))
                {
                    int count = 0;
                    for (count = 0; (count < parentClasses.Length); count = (count + 1))
                    {
                        if ((string.Compare(((string)(parentClasses.GetValue(count))), ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool ShouldSerializeActiveMaximumTransmissionUnit()
        {
            if ((IsActiveMaximumTransmissionUnitNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeAutoSense()
        {
            if ((IsAutoSenseNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeAvailability()
        {
            if ((IsAvailabilityNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeCommunicationStatus()
        {
            if ((IsCommunicationStatusNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeDetailedStatus()
        {
            if ((IsDetailedStatusNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeEnabledDefault()
        {
            if ((IsEnabledDefaultNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeEnabledState()
        {
            if ((IsEnabledStateNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeErrorCleared()
        {
            if ((IsErrorClearedNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeFullDuplex()
        {
            if ((IsFullDuplexNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeHealthState()
        {
            if ((IsHealthStateNull == false))
            {
                return true;
            }
            return false;
        }

        // Converts a given datetime in DMTF format to System.DateTime object.
        static DateTime ToDateTime(string dmtfDate)
        {
            DateTime initializer = System.DateTime.MinValue;
            int year = initializer.Year;
            int month = initializer.Month;
            int day = initializer.Day;
            int hour = initializer.Hour;
            int minute = initializer.Minute;
            int second = initializer.Second;
            long ticks = 0;
            string dmtf = dmtfDate;
            DateTime datetime = System.DateTime.MinValue;
            string tempString = string.Empty;
            if ((dmtf == null))
            {
                throw new ArgumentOutOfRangeException();
            }
            if ((dmtf.Length == 0))
            {
                throw new ArgumentOutOfRangeException();
            }
            if ((dmtf.Length != 25))
            {
                throw new ArgumentOutOfRangeException();
            }
            try
            {
                tempString = dmtf.Substring(0, 4);
                if (("****" != tempString))
                {
                    year = int.Parse(tempString);
                }
                tempString = dmtf.Substring(4, 2);
                if (("**" != tempString))
                {
                    month = int.Parse(tempString);
                }
                tempString = dmtf.Substring(6, 2);
                if (("**" != tempString))
                {
                    day = int.Parse(tempString);
                }
                tempString = dmtf.Substring(8, 2);
                if (("**" != tempString))
                {
                    hour = int.Parse(tempString);
                }
                tempString = dmtf.Substring(10, 2);
                if (("**" != tempString))
                {
                    minute = int.Parse(tempString);
                }
                tempString = dmtf.Substring(12, 2);
                if (("**" != tempString))
                {
                    second = int.Parse(tempString);
                }
                tempString = dmtf.Substring(15, 6);
                if (("******" != tempString))
                {
                    ticks = (long.Parse(tempString) * ((long)((System.TimeSpan.TicksPerMillisecond / 1000))));
                }
                if (((((((((year < 0)
                            || (month < 0))
                            || (day < 0))
                            || (hour < 0))
                            || (minute < 0))
                            || (minute < 0))
                            || (second < 0))
                            || (ticks < 0)))
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException(null, e.Message);
            }
            datetime = new DateTime(year, month, day, hour, minute, second, 0);
            datetime = datetime.AddTicks(ticks);
            TimeSpan tickOffset = System.TimeZone.CurrentTimeZone.GetUtcOffset(datetime);
            int UTCOffset = 0;
            int OffsetToBeAdjusted = 0;
            long OffsetMins = ((long)((tickOffset.Ticks / System.TimeSpan.TicksPerMinute)));
            tempString = dmtf.Substring(22, 3);
            if ((tempString != "******"))
            {
                tempString = dmtf.Substring(21, 4);
                try
                {
                    UTCOffset = int.Parse(tempString);
                }
                catch (Exception e)
                {
                    throw new ArgumentOutOfRangeException(null, e.Message);
                }
                OffsetToBeAdjusted = ((int)((OffsetMins - UTCOffset)));
                datetime = datetime.AddMinutes(((double)(OffsetToBeAdjusted)));
            }
            return datetime;
        }

        // Converts a given System.DateTime object to DMTF datetime format.
        static string ToDmtfDateTime(DateTime date)
        {
            string utcString = string.Empty;
            TimeSpan tickOffset = System.TimeZone.CurrentTimeZone.GetUtcOffset(date);
            long OffsetMins = ((long)((tickOffset.Ticks / System.TimeSpan.TicksPerMinute)));
            if ((System.Math.Abs(OffsetMins) > 999))
            {
                date = date.ToUniversalTime();
                utcString = "+000";
            }
            else
            {
                if ((tickOffset.Ticks >= 0))
                {
                    utcString = string.Concat("+", ((long)((tickOffset.Ticks / System.TimeSpan.TicksPerMinute))).ToString().PadLeft(3, '0'));
                }
                else
                {
                    string strTemp = ((long)(OffsetMins)).ToString();
                    utcString = string.Concat("-", strTemp.Substring(1, (strTemp.Length - 1)).PadLeft(3, '0'));
                }
            }
            string dmtfDateTime = ((int)(date.Year)).ToString().PadLeft(4, '0');
            dmtfDateTime = string.Concat(dmtfDateTime, ((int)(date.Month)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((int)(date.Day)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((int)(date.Hour)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((int)(date.Minute)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((int)(date.Second)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ".");
            DateTime dtTemp = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
            long microsec = ((long)((((date.Ticks - dtTemp.Ticks)
                        * 1000)
                        / System.TimeSpan.TicksPerMillisecond)));
            string strMicrosec = ((long)(microsec)).ToString();
            if ((strMicrosec.Length > 6))
            {
                strMicrosec = strMicrosec.Substring(0, 6);
            }
            dmtfDateTime = string.Concat(dmtfDateTime, strMicrosec.PadLeft(6, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, utcString);
            return dmtfDateTime;
        }

        private bool ShouldSerializeInstallDate()
        {
            if ((IsInstallDateNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeLastErrorCode()
        {
            if ((IsLastErrorCodeNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeLinkTechnology()
        {
            if ((IsLinkTechnologyNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeMaxDataSize()
        {
            if ((IsMaxDataSizeNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeMaxQuiesceTime()
        {
            if ((IsMaxQuiesceTimeNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeMaxSpeed()
        {
            if ((IsMaxSpeedNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeOperatingStatus()
        {
            if ((IsOperatingStatusNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializePortNumber()
        {
            if ((IsPortNumberNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializePortType()
        {
            if ((IsPortTypeNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializePowerManagementSupported()
        {
            if ((IsPowerManagementSupportedNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializePowerOnHours()
        {
            if ((IsPowerOnHoursNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializePrimaryStatus()
        {
            if ((IsPrimaryStatusNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeRequestedSpeed()
        {
            if ((IsRequestedSpeedNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeRequestedState()
        {
            if ((IsRequestedStateNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeSpeed()
        {
            if ((IsSpeedNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeStatusInfo()
        {
            if ((IsStatusInfoNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeSupportedMaximumTransmissionUnit()
        {
            if ((IsSupportedMaximumTransmissionUnitNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeTimeOfLastStateChange()
        {
            if ((IsTimeOfLastStateChangeNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeTotalPowerOnHours()
        {
            if ((IsTotalPowerOnHoursNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeTransitioningToState()
        {
            if ((IsTransitioningToStateNull == false))
            {
                return true;
            }
            return false;
        }

        private bool ShouldSerializeUsageRestriction()
        {
            if ((IsUsageRestrictionNull == false))
            {
                return true;
            }
            return false;
        }

        [Browsable(true)]
        public void CommitObject()
        {
            if ((isEmbedded == false))
            {
                PrivateLateBoundObject.Put();
            }
        }

        [Browsable(true)]
        public void CommitObject(PutOptions putOptions)
        {
            if ((isEmbedded == false))
            {
                PrivateLateBoundObject.Put(putOptions);
            }
        }

        private void Initialize()
        {
            AutoCommitProp = true;
            isEmbedded = false;
        }

        private static string ConstructPath(string keyCreationClassName, string keyDeviceID, string keySystemCreationClassName, string keySystemName)
        {
            string strPath = "root\\virtualization\\v2:Msvm_InternalEthernetPort";
            strPath = string.Concat(strPath, string.Concat(".CreationClassName=", string.Concat("\"", string.Concat(keyCreationClassName, "\""))));
            strPath = string.Concat(strPath, string.Concat(",DeviceID=", string.Concat("\"", string.Concat(keyDeviceID, "\""))));
            strPath = string.Concat(strPath, string.Concat(",SystemCreationClassName=", string.Concat("\"", string.Concat(keySystemCreationClassName, "\""))));
            strPath = string.Concat(strPath, string.Concat(",SystemName=", string.Concat("\"", string.Concat(keySystemName, "\""))));
            return strPath;
        }

        private void InitializeObject(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions)
        {
            Initialize();
            if ((path != null))
            {
                if ((CheckIfProperClass(mgmtScope, path, getOptions) != true))
                {
                    throw new ArgumentException("Class name does not match.");
                }
            }
            PrivateLateBoundObject = new ManagementObject(mgmtScope, path, getOptions);
            PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            curObj = PrivateLateBoundObject;
        }

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static InternalEthernetPortCollection GetInstances()
        {
            return GetInstances(null, null, null);
        }

        public static InternalEthernetPortCollection GetInstances(string condition)
        {
            return GetInstances(null, condition, null);
        }

        public static InternalEthernetPortCollection GetInstances(string[] selectedProperties)
        {
            return GetInstances(null, null, selectedProperties);
        }

        public static InternalEthernetPortCollection GetInstances(string condition, string[] selectedProperties)
        {
            return GetInstances(null, condition, selectedProperties);
        }

        public static InternalEthernetPortCollection GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions)
        {
            if ((mgmtScope == null))
            {
                if ((statMgmtScope == null))
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\virtualization\\v2";
                }
                else
                {
                    mgmtScope = statMgmtScope;
                }
            }
            ManagementPath pathObj = new ManagementPath();
            pathObj.ClassName = "Msvm_InternalEthernetPort";
            pathObj.NamespacePath = "root\\virtualization\\v2";
            ManagementClass clsObject = new ManagementClass(mgmtScope, pathObj, null);
            if ((enumOptions == null))
            {
                enumOptions = new EnumerationOptions();
                enumOptions.EnsureLocatable = true;
            }
            return new InternalEthernetPortCollection(clsObject.GetInstances(enumOptions));
        }

        public static InternalEthernetPortCollection GetInstances(ManagementScope mgmtScope, string condition)
        {
            return GetInstances(mgmtScope, condition, null);
        }

        public static InternalEthernetPortCollection GetInstances(ManagementScope mgmtScope, string[] selectedProperties)
        {
            return GetInstances(mgmtScope, null, selectedProperties);
        }

        public static InternalEthernetPortCollection GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties)
        {
            if ((mgmtScope == null))
            {
                if ((statMgmtScope == null))
                {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\virtualization\\v2";
                }
                else
                {
                    mgmtScope = statMgmtScope;
                }
            }
            ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery("Msvm_InternalEthernetPort", condition, selectedProperties));
            EnumerationOptions enumOptions = new EnumerationOptions();
            enumOptions.EnsureLocatable = true;
            ObjectSearcher.Options = enumOptions;
            return new InternalEthernetPortCollection(ObjectSearcher.Get());
        }

        [Browsable(true)]
        public static InternalEthernetPort CreateInstance()
        {
            ManagementScope mgmtScope = null;
            if ((statMgmtScope == null))
            {
                mgmtScope = new ManagementScope();
                mgmtScope.Path.NamespacePath = CreatedWmiNamespace;
            }
            else
            {
                mgmtScope = statMgmtScope;
            }
            ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
            ManagementClass tmpMgmtClass = new ManagementClass(mgmtScope, mgmtPath, null);
            return new InternalEthernetPort(tmpMgmtClass.CreateInstance());
        }

        [Browsable(true)]
        public void Delete()
        {
            PrivateLateBoundObject.Delete();
        }

        public uint EnableDevice(bool Enabled)
        {
            if ((isEmbedded == false))
            {
                ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("EnableDevice");
                inParams["Enabled"] = ((bool)(Enabled));
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("EnableDevice", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return System.Convert.ToUInt32(0);
            }
        }

        public uint OnlineDevice(bool Online)
        {
            if ((isEmbedded == false))
            {
                ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("OnlineDevice");
                inParams["Online"] = ((bool)(Online));
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("OnlineDevice", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return System.Convert.ToUInt32(0);
            }
        }

        public uint QuiesceDevice(bool Quiesce)
        {
            if ((isEmbedded == false))
            {
                ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("QuiesceDevice");
                inParams["Quiesce"] = ((bool)(Quiesce));
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("QuiesceDevice", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return System.Convert.ToUInt32(0);
            }
        }

        public uint RequestStateChange(ushort RequestedState, DateTime TimeoutPeriod, out ManagementPath Job)
        {
            if ((isEmbedded == false))
            {
                ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("RequestStateChange");
                inParams[nameof(RequestedState)] = ((ushort)(RequestedState));
                inParams["TimeoutPeriod"] = ToDmtfDateTime(((DateTime)(TimeoutPeriod)));
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RequestStateChange", inParams, null);
                Job = null;
                if ((outParams.Properties["Job"] != null))
                {
                    Job = new ManagementPath(outParams["Job"] as string);
                }
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                Job = null;
                return System.Convert.ToUInt32(0);
            }
        }

        public uint Reset()
        {
            if ((isEmbedded == false))
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Reset", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return System.Convert.ToUInt32(0);
            }
        }

        public uint RestoreProperties()
        {
            if ((isEmbedded == false))
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("RestoreProperties", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return System.Convert.ToUInt32(0);
            }
        }

        public uint SaveProperties()
        {
            if ((isEmbedded == false))
            {
                ManagementBaseObject inParams = null;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SaveProperties", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return System.Convert.ToUInt32(0);
            }
        }

        public uint SetPowerState(ushort PowerState, DateTime Time)
        {
            if ((isEmbedded == false))
            {
                ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("SetPowerState");
                inParams["PowerState"] = ((ushort)(PowerState));
                inParams["Time"] = ToDmtfDateTime(((DateTime)(Time)));
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("SetPowerState", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else
            {
                return System.Convert.ToUInt32(0);
            }
        }

        // Enumerator implementation for enumerating instances of the class.
        public class InternalEthernetPortCollection : object, ICollection
        {

            private ManagementObjectCollection privColObj;

            public InternalEthernetPortCollection(ManagementObjectCollection objCollection)
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
                for (nCtr = 0; (nCtr < array.Length); nCtr = (nCtr + 1))
                {
                    array.SetValue(new InternalEthernetPort(((ManagementObject)(array.GetValue(nCtr)))), nCtr);
                }
            }

            public virtual IEnumerator GetEnumerator()
            {
                return new InternalEthernetPortEnumerator(privColObj.GetEnumerator());
            }

            public class InternalEthernetPortEnumerator : object, IEnumerator
            {

                private ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;

                public InternalEthernetPortEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum)
                {
                    privObjEnum = objEnum;
                }

                public virtual object Current
                {
                    get
                    {
                        return new InternalEthernetPort(((ManagementObject)(privObjEnum.Current)));
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
                if ((baseType.BaseType == typeof(Enum)))
                {
                    if ((value.GetType() == destinationType))
                    {
                        return value;
                    }
                    if ((((value == null)
                                && (context != null))
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false)))
                    {
                        return "NULL_ENUM_VALUE";
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (((baseType == typeof(bool))
                            && (baseType.BaseType == typeof(ValueType))))
                {
                    if ((((value == null)
                                && (context != null))
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false)))
                    {
                        return "";
                    }
                    return baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (((context != null)
                            && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false)))
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
                    return ((int)(PrivateLateBoundObject["__GENUS"]));
                }
            }

            [Browsable(true)]
            public string CLASS
            {
                get
                {
                    return ((string)(PrivateLateBoundObject["__CLASS"]));
                }
            }

            [Browsable(true)]
            public string SUPERCLASS
            {
                get
                {
                    return ((string)(PrivateLateBoundObject["__SUPERCLASS"]));
                }
            }

            [Browsable(true)]
            public string DYNASTY
            {
                get
                {
                    return ((string)(PrivateLateBoundObject["__DYNASTY"]));
                }
            }

            [Browsable(true)]
            public string RELPATH
            {
                get
                {
                    return ((string)(PrivateLateBoundObject["__RELPATH"]));
                }
            }

            [Browsable(true)]
            public int PROPERTY_COUNT
            {
                get
                {
                    return ((int)(PrivateLateBoundObject["__PROPERTY_COUNT"]));
                }
            }

            [Browsable(true)]
            public string[] DERIVATION
            {
                get
                {
                    return ((string[])(PrivateLateBoundObject["__DERIVATION"]));
                }
            }

            [Browsable(true)]
            public string SERVER
            {
                get
                {
                    return ((string)(PrivateLateBoundObject["__SERVER"]));
                }
            }

            [Browsable(true)]
            public string NAMESPACE
            {
                get
                {
                    return ((string)(PrivateLateBoundObject["__NAMESPACE"]));
                }
            }

            [Browsable(true)]
            public string PATH
            {
                get
                {
                    return ((string)(PrivateLateBoundObject["__PATH"]));
                }
            }
        }
    }
}
