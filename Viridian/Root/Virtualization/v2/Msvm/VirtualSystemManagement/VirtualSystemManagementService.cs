using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;

namespace Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement
{
    public class VirtualSystemManagementService : MsvmBase
    {
        public static string ClassName => $"Msvm_{nameof(VirtualSystemManagementService)}";

        // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
        public VirtualSystemManagementService() : base(ClassName) { }

        public VirtualSystemManagementService(string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualSystemManagementService(ManagementScope mgmtScope, string keyCreationClassName, string keyName, string keySystemCreationClassName, string keySystemName) : base(mgmtScope, keyCreationClassName, keyName, keySystemCreationClassName, keySystemName, ClassName) { }

        public VirtualSystemManagementService(ManagementPath path, ObjectGetOptions getOptions) : base(path, getOptions, ClassName) { }

        public VirtualSystemManagementService(ManagementScope mgmtScope, ManagementPath path) : base(mgmtScope, path, ClassName) { }

        public VirtualSystemManagementService(ManagementPath path) : base(path, ClassName) { }

        public VirtualSystemManagementService(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) : base(mgmtScope, path, getOptions, ClassName) { }

        public VirtualSystemManagementService(ManagementObject theObject) : base(theObject, ClassName) { }

        public VirtualSystemManagementService(ManagementBaseObject theObject) : base(theObject, ClassName) { }

        public ushort[] AvailableRequestedStates => (ushort[])LateBoundObject[nameof(AvailableRequestedStates)];

        public string Caption => (string)LateBoundObject[nameof(Caption)];

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

        public string CreationClassName => (string)LateBoundObject[nameof(CreationClassName)];

        public string Description => (string)LateBoundObject[nameof(Description)];

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

        public string ElementName => (string)LateBoundObject[nameof(ElementName)];

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

        public string InstanceID => (string)LateBoundObject[nameof(InstanceID)];

        public string Name => (string)LateBoundObject[nameof(Name)];

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

        public ushort[] OperationalStatus => (ushort[])LateBoundObject[nameof(OperationalStatus)];

        public string OtherEnabledState => (string)LateBoundObject[nameof(OtherEnabledState)];

        public string PrimaryOwnerContact => (string)LateBoundObject[nameof(PrimaryOwnerContact)];

        public string PrimaryOwnerName => (string)LateBoundObject[nameof(PrimaryOwnerName)];

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

        public string StartMode => (string)LateBoundObject[nameof(StartMode)];

        public string Status => (string)LateBoundObject[nameof(Status)];

        public string[] StatusDescriptions => (string[])LateBoundObject[nameof(StatusDescriptions)];

        public string SystemCreationClassName => (string)LateBoundObject[nameof(SystemCreationClassName)];

        public string SystemName => (string)LateBoundObject[nameof(SystemName)];

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

        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static List<VirtualSystemManagementService> GetInstances() => GetInstances(null, null, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemManagementService(mo)).ToList();

        public new static List<VirtualSystemManagementService> GetInstances(string condition) => GetInstances(null, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemManagementService(mo)).ToList();

        public static List<VirtualSystemManagementService> GetInstances(string[] selectedProperties) => GetInstances(null, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemManagementService(mo)).ToList();

        public static List<VirtualSystemManagementService> GetInstances(string condition, string[] selectedProperties) => GetInstances(null, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemManagementService(mo)).ToList();

        public static List<VirtualSystemManagementService> GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) => GetInstances(mgmtScope, enumOptions, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemManagementService(mo)).ToList();

        public static List<VirtualSystemManagementService> GetInstances(ManagementScope mgmtScope, string condition) => GetInstances(mgmtScope, condition, null, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemManagementService(mo)).ToList();

        public static List<VirtualSystemManagementService> GetInstances(ManagementScope mgmtScope, string[] selectedProperties) => GetInstances(mgmtScope, null, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemManagementService(mo)).ToList();

        public static List<VirtualSystemManagementService> GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) => GetInstances(mgmtScope, condition, selectedProperties, ClassName).Cast<ManagementObject>().Select((mo) => new VirtualSystemManagementService(mo)).ToList();

        public static VirtualSystemManagementService CreateInstance() => new VirtualSystemManagementService(CreateInstance(ClassName));

        public uint AddBootSourceSettings(ManagementPath AffectedConfiguration, string[] BootSourceSettings, out ManagementPath Job, out ManagementPath[] ResultingBootSourceSettings)
        {
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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

        public uint Modify__DiskMergeSettings(string SettingData, out ManagementPath Job)
        {
            if (IsEmbedded == false)
            {
                ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("Modify__DiskMergeSettings");
                inParams["SettingData"] = SettingData;
                ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Modify__DiskMergeSettings", inParams, null);
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
            if (IsEmbedded == false)
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
    }
}
