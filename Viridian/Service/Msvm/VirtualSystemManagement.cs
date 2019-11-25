using System;
using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Utilities;

namespace Viridian.Service.Msvm
{
    public sealed class VirtualSystemManagement
    {
        private static VirtualSystemManagement instance = null;
        private const string serverName = ".";
        private const string scopePath = @"\Root\Virtualization\V2";
        private static ManagementObject Msvm_VirtualSystemManagementService = null;
        private static ManagementScope scope = null;
        
        public enum RequestedStateVM : uint
        {
            Other = 1,
            Running = 2,
            Off = 3,
            Saved = 6,
            Paused = 9,
            Starting = 10,
            Reset = 11,
            Saving = 32773,
            Pausing = 32776,
            Resuming = 32777,
            FastSaved = 32779,
            FastSaving = 32780,
        };

        private VirtualSystemManagement()
        {
            scope = new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);

            using (var vsms = new ManagementClass(nameof(Msvm_VirtualSystemManagementService)))
            {
                vsms.Scope = scope;

                Msvm_VirtualSystemManagementService = Utils.GetFirstObjectFromCollection(vsms.GetInstances());
            }
        }

        public static VirtualSystemManagement Instance
        {
            get
            {
                if (instance == null)
                    instance = new VirtualSystemManagement();

                return instance;
            }
        }

        public ManagementObject MsvmVirtualSystemManagementService => Msvm_VirtualSystemManagementService ?? throw new ViridianException($"{nameof(Msvm_VirtualSystemManagementService)} is null!");

        #region MsvmProperties

        string InstanceID => Msvm_VirtualSystemManagementService[nameof(InstanceID)].ToString();
        string Caption => Msvm_VirtualSystemManagementService[nameof(Caption)].ToString();
        string Description => Msvm_VirtualSystemManagementService[nameof(Description)].ToString();
        string ElementName => Msvm_VirtualSystemManagementService[nameof(ElementName)].ToString();
        DateTime InstallDate => (DateTime)Msvm_VirtualSystemManagementService[nameof(InstallDate)];
        string Name => Msvm_VirtualSystemManagementService[nameof(Name)].ToString();
        ushort[] OperationalStatus => (ushort[])Msvm_VirtualSystemManagementService[nameof(OperationalStatus)];
        string[] StatusDescriptions => (string[])Msvm_VirtualSystemManagementService[nameof(StatusDescriptions)];
        string Status => Msvm_VirtualSystemManagementService[nameof(Status)].ToString();
        ushort HealthState => (ushort)Msvm_VirtualSystemManagementService[nameof(HealthState)];
        ushort CommunicationStatus => (ushort)Msvm_VirtualSystemManagementService[nameof(CommunicationStatus)];
        ushort DetailedStatus => (ushort)Msvm_VirtualSystemManagementService[nameof(DetailedStatus)];
        ushort OperatingStatus => (ushort)Msvm_VirtualSystemManagementService[nameof(OperatingStatus)];
        ushort PrimaryStatus => (ushort)Msvm_VirtualSystemManagementService[nameof(PrimaryStatus)];
        ushort EnabledState => (ushort)Msvm_VirtualSystemManagementService[nameof(EnabledState)];
        string OtherEnabledState => Msvm_VirtualSystemManagementService[nameof(OtherEnabledState)].ToString();
        ushort RequestedState => (ushort)Msvm_VirtualSystemManagementService[nameof(RequestedState)];
        ushort EnabledDefault => (ushort)Msvm_VirtualSystemManagementService[nameof(EnabledDefault)];
        DateTime TimeOfLastStateChange => (DateTime)Msvm_VirtualSystemManagementService[nameof(TimeOfLastStateChange)];
        ushort[] AvailableRequestedStates => (ushort[])Msvm_VirtualSystemManagementService[nameof(AvailableRequestedStates)];
        ushort TransitioningToState => (ushort)Msvm_VirtualSystemManagementService[nameof(TransitioningToState)];
        string SystemCreationClassName => Msvm_VirtualSystemManagementService[nameof(SystemCreationClassName)].ToString();
        string SystemName => Msvm_VirtualSystemManagementService[nameof(SystemName)].ToString();
        string CreationClassName => Msvm_VirtualSystemManagementService[nameof(CreationClassName)].ToString();
        string PrimaryOwnerName => Msvm_VirtualSystemManagementService[nameof(PrimaryOwnerName)].ToString();
        string PrimaryOwnerContact => Msvm_VirtualSystemManagementService[nameof(PrimaryOwnerContact)].ToString();
        string StartMode => Msvm_VirtualSystemManagementService[nameof(StartMode)].ToString();
        bool Started => (bool)Msvm_VirtualSystemManagementService[nameof(Started)];

        #endregion

        public string[] AddBootSourceSettings(string AffectedConfiguration, string[] BootSourceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(AddBootSourceSettings)))
            {
                ip[nameof(AffectedConfiguration)] = AffectedConfiguration ?? throw new ViridianException($"{nameof(AffectedConfiguration)} is null!");
                ip[nameof(BootSourceSettings)] = BootSourceSettings ?? throw new ViridianException($"{nameof(BootSourceSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(AddBootSourceSettings), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingBootSourceSettings"] as string[];
                }
            }
        }

        public string[] AddFeatureSettings(string AffectedConfiguration, string[] FeatureSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(AddFeatureSettings)))
            {
                ip[nameof(AffectedConfiguration)] = AffectedConfiguration ?? throw new ViridianException($"{nameof(AffectedConfiguration)} is null!");
                ip[nameof(FeatureSettings)] = FeatureSettings ?? throw new ViridianException($"{nameof(FeatureSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(AddFeatureSettings), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingFeatureSettings"] as string[];
                }
            }
        }

        public string[] AddGuestServiceSettings(string AffectedConfiguration, string[] GuestServiceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(AddGuestServiceSettings)))
            {
                ip[nameof(AffectedConfiguration)] = AffectedConfiguration ?? throw new ViridianException($"{nameof(AffectedConfiguration)} is null!");
                ip[nameof(GuestServiceSettings)] = GuestServiceSettings ?? throw new ViridianException($"{nameof(GuestServiceSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(AddGuestServiceSettings), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingGuestServiceSettings"] as string[];
                }
            }
        }

        public void AddFibreChannelChap(string[] FcPortSettings, byte[] SecretEncoding, byte[] SharedSecret)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(AddFibreChannelChap)))
            {
                ip[nameof(FcPortSettings)] = FcPortSettings ?? throw new ViridianException($"{nameof(FcPortSettings)} is null!");
                ip[nameof(SecretEncoding)] = SecretEncoding ?? throw new ViridianException($"{nameof(SecretEncoding)} is null!");
                ip[nameof(SharedSecret)] = SharedSecret ?? throw new ViridianException($"{nameof(SharedSecret)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(AddFibreChannelChap), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void AddKvpItems(string TargetSystem, string[] DataItems)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(AddKvpItems)))
            {
                ip[nameof(TargetSystem)] = TargetSystem ?? throw new ViridianException($"{nameof(TargetSystem)} is null!");
                ip[nameof(DataItems)] = DataItems ?? throw new ViridianException($"{nameof(DataItems)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(AddKvpItems), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public string[] AddResourceSettings(ManagementObject AffectedConfiguration, string[] ResourceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(AddResourceSettings)))
            {
                ip[nameof(AffectedConfiguration)] = AffectedConfiguration ?? throw new ViridianException($"{nameof(AffectedConfiguration)} is null!");
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ViridianException($"{nameof(ResourceSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(AddResourceSettings), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingResourceSettings"] as string[];
                }
            }
        }

        public string[] AddSystemComponentSettings(ManagementObject AffectedConfiguration, string[] ComponentSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(AddSystemComponentSettings)))
            {
                ip[nameof(AffectedConfiguration)] = AffectedConfiguration ?? throw new ViridianException($"{nameof(AffectedConfiguration)} is null!");
                ip[nameof(ComponentSettings)] = ComponentSettings ?? throw new ViridianException($"{nameof(ComponentSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(AddSystemComponentSettings), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingComponentSettings"] as string[];
                }
            }
        }

        public ManagementObject DefinePlannedSystem(string SystemSettings, string[] ResourceSettings, string ReferenceConfiguration)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(DefinePlannedSystem)))
            {
                ip[nameof(SystemSettings)] = SystemSettings ?? throw new ViridianException($"{nameof(SystemSettings)} is null!");
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ViridianException($"{nameof(ResourceSettings)} is null!");
                ip[nameof(ReferenceConfiguration)] = ReferenceConfiguration ?? throw new ViridianException($"{nameof(ReferenceConfiguration)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(DefinePlannedSystem), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingSystem"] as ManagementObject;
                }
            }
        }

        public ManagementObject DefineSystem(string SystemSettings, string[] ResourceSettings, string ReferenceConfiguration)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(DefineSystem)))
            {
                ip[nameof(SystemSettings)] = SystemSettings ?? throw new ViridianException($"{nameof(SystemSettings)} is null!");
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ViridianException($"{nameof(ResourceSettings)} is null!");
                ip[nameof(ReferenceConfiguration)] = ReferenceConfiguration ?? throw new ViridianException($"{nameof(ReferenceConfiguration)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(DefineSystem), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingSystem"] as ManagementObject;
                }
            }
        }

        public void DestroySystem(ManagementObject AffectedSystem)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(DestroySystem)))
            {
                ip[nameof(AffectedSystem)] = AffectedSystem ?? throw new ViridianException($"{nameof(AffectedSystem)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(DestroySystem), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public string DiagnoseNetworkConnection(ManagementObject TargetNetworkAdapter, string DiagnosticSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(DiagnoseNetworkConnection)))
            {
                ip[nameof(TargetNetworkAdapter)] = TargetNetworkAdapter ?? throw new ViridianException($"{nameof(TargetNetworkAdapter)} is null!");
                ip[nameof(DiagnosticSettings)] = DiagnosticSettings ?? throw new ViridianException($"{nameof(DiagnosticSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(DiagnoseNetworkConnection), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["DiagnosticInformation"] as string;
                }
            }
        }

        public void ExportSystemDefinition(ManagementObject ComputerSystem, string ExportDirectory, string ExportSettingData)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ExportSystemDefinition)))
            {
                ip[nameof(ComputerSystem)] = ComputerSystem ?? throw new ViridianException($"{nameof(ComputerSystem)} is null!");
                ip[nameof(ExportDirectory)] = ExportDirectory ?? throw new ViridianException($"{nameof(ExportDirectory)} is null!");
                ip[nameof(ExportSettingData)] = ExportSettingData ?? throw new ViridianException($"{nameof(ExportSettingData)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ExportSystemDefinition), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public string FormatError(string[] Errors)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(FormatError)))
            {
                ip[nameof(Errors)] = Errors ?? throw new ViridianException($"{nameof(Errors)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(FormatError), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ErrorMessage"] as string;
                }
            }
        }

        public string[] GenerateWwpn(uint NumberOfWwpns)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(GenerateWwpn)))
            {
                ip[nameof(NumberOfWwpns)] = NumberOfWwpns;

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(GenerateWwpn), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["GeneratedWwpn"] as string[];
                }
            }
        }

        public string GetCurrentWwpnFromGenerator()
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(GetCurrentWwpnFromGenerator)))
            using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(GetCurrentWwpnFromGenerator), ip, null))
            {
                Validator.ValidateOutput(op, scope);

                return op["CurrentWwpn"] as string;
            }
        }

        public string[] GetDefinitionFileSummaryInformation(string[] DefinitionFiles)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(GetDefinitionFileSummaryInformation)))
            {
                ip[nameof(DefinitionFiles)] = DefinitionFiles ?? throw new ViridianException($"{nameof(DefinitionFiles)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(GetDefinitionFileSummaryInformation), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["SummaryInformation"] as string[];
                }
            }
        }

        public ulong GetSizeOfSystemFiles(string Vssd)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(GetSizeOfSystemFiles)))
            {
                ip[nameof(Vssd)] = Vssd ?? throw new ViridianException($"{nameof(Vssd)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(GetSizeOfSystemFiles), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return (ulong)op["Size"];
                }
            }
        }

        public ManagementBaseObject[] GetSummaryInformation(ManagementObject[] SettingData, int[] RequestedInformation)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(GetSummaryInformation)))
            {
                ip[nameof(SettingData)] = SettingData ?? throw new ViridianException($"{nameof(SettingData)} is null!");
                ip[nameof(RequestedInformation)] = RequestedInformation ?? throw new ViridianException($"{nameof(RequestedInformation)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(GetSummaryInformation), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["SummaryInformation"] as ManagementBaseObject[];
                }
            }
        }

        public byte[] GetVirtualSystemThumbnailImage(string TargetSystem, ushort WidthPixels, ushort HeightPixels)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(GetVirtualSystemThumbnailImage)))
            {
                ip[nameof(TargetSystem)] = TargetSystem ?? throw new ViridianException($"{nameof(TargetSystem)} is null!");
                ip[nameof(WidthPixels)] = WidthPixels;
                ip[nameof(HeightPixels)] = HeightPixels;

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(GetVirtualSystemThumbnailImage), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ImageData"] as byte[];
                }
            }
        }

        public string[] ImportSnapshotDefinitions(ManagementObject PlannedSystem, string SnapshotFolder)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ImportSnapshotDefinitions)))
            {
                ip[nameof(PlannedSystem)] = PlannedSystem ?? throw new ViridianException($"{nameof(PlannedSystem)} is null!");
                ip[nameof(SnapshotFolder)] = SnapshotFolder ?? throw new ViridianException($"{nameof(SnapshotFolder)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ImportSnapshotDefinitions), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ImportedSnapshots"] as string[];
                }
            }
        }

        public ManagementObject ImportSystemDefinition(string SystemDefinitionFile, string SnapshotFolder, bool GenerateNewSystemIdentifier)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ImportSystemDefinition)))
            {
                ip[nameof(SystemDefinitionFile)] = SystemDefinitionFile ?? throw new ViridianException($"{nameof(SystemDefinitionFile)} is null!");
                ip[nameof(SnapshotFolder)] = SnapshotFolder ?? throw new ViridianException($"{nameof(SnapshotFolder)} is null!");
                ip[nameof(GenerateNewSystemIdentifier)] = GenerateNewSystemIdentifier;

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ImportSystemDefinition), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ImportedSystem"] as ManagementObject;
                }
            }
        }

        public void ModifyDiskMergeSettings(string SettingData)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifyDiskMergeSettings)))
            {
                ip[nameof(SettingData)] = SettingData ?? throw new ViridianException($"{nameof(SettingData)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifyDiskMergeSettings), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public ManagementObject[] ModifyFeatureSettings(string[] FeatureSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifyFeatureSettings)))
            {
                ip[nameof(FeatureSettings)] = FeatureSettings ?? throw new ViridianException($"{nameof(FeatureSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifyFeatureSettings), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingFeatureSettings"] as ManagementObject[];
                }
            }
        }

        public string[] ModifyGuestServiceSettings(string[] GuestServiceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifyGuestServiceSettings)))
            {
                ip[nameof(GuestServiceSettings)] = GuestServiceSettings ?? throw new ViridianException($"{nameof(GuestServiceSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifyGuestServiceSettings), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingGuestServiceSettings"] as string[];
                }
            }
        }

        public string[] ModifyKvpItems(string TargetSystem)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifyKvpItems)))
            {
                ip[nameof(TargetSystem)] = TargetSystem ?? throw new ViridianException($"{nameof(TargetSystem)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifyKvpItems), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["DataItems"] as string[];
                }
            }
        }

        public string[] ModifyResourceSettings(string[] ResourceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifyResourceSettings)))
            {
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ViridianException($"{nameof(ResourceSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifyResourceSettings), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingResourceSettings"] as string[];
                }
            }
        }

        public void ModifyServiceSettings(string SettingData)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifyServiceSettings)))
            {
                ip[nameof(SettingData)] = SettingData ?? throw new ViridianException($"{nameof(SettingData)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifyServiceSettings), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }
            
        public string[] ModifySystemComponentSettings(string[] ComponentSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifySystemComponentSettings)))
            {
                ip[nameof(ComponentSettings)] = ComponentSettings ?? throw new ViridianException($"{nameof(ComponentSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifySystemComponentSettings), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingComponentSettings"] as string[];
                }
            }
        }
            
        public void ModifySystemSettings(string SystemSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifySystemSettings)))
            {
                ip[nameof(SystemSettings)] = SystemSettings ?? throw new ViridianException($"{nameof(SystemSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifySystemSettings), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public ManagementObject RealizePlannedSystem(ManagementObject PlannedSystem)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RealizePlannedSystem)))
            {
                ip[nameof(PlannedSystem)] = PlannedSystem ?? throw new ViridianException($"{nameof(PlannedSystem)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RealizePlannedSystem), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingSystem"] as ManagementObject;
                }
            }
        }

        public void RemoveFeatureSettings(string[] FeatureSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RemoveFeatureSettings)))
            {
                ip[nameof(FeatureSettings)] = FeatureSettings ?? throw new ViridianException($"{nameof(FeatureSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RemoveFeatureSettings), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void RemoveBootSourceSettings(string[] BootSourceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RemoveBootSourceSettings)))
            {
                ip[nameof(BootSourceSettings)] = BootSourceSettings ?? throw new ViridianException($"{nameof(BootSourceSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RemoveBootSourceSettings), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void RemoveFibreChannelChap(string[] FcPortSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RemoveFibreChannelChap)))
            {
                ip[nameof(FcPortSettings)] = FcPortSettings ?? throw new ViridianException($"{nameof(FcPortSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RemoveFibreChannelChap), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void RemoveKvpItems(string TargetSystem, string[] DataItems)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RemoveKvpItems)))
            {
                ip[nameof(TargetSystem)] = TargetSystem ?? throw new ViridianException($"{nameof(TargetSystem)} is null!");
                ip[nameof(DataItems)] = DataItems ?? throw new ViridianException($"{nameof(DataItems)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RemoveKvpItems), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void RemoveGuestServiceSettings(string[] GuestServiceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RemoveGuestServiceSettings)))
            {
                ip[nameof(GuestServiceSettings)] = GuestServiceSettings ?? throw new ViridianException($"{nameof(GuestServiceSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RemoveGuestServiceSettings), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void RemoveResourceSettings(ManagementBaseObject[] ResourceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RemoveResourceSettings)))
            {
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ViridianException($"{nameof(ResourceSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RemoveResourceSettings), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void RemoveSystemComponentSettings(string[] ComponentSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RemoveSystemComponentSettings)))
            {
                ip[nameof(ComponentSettings)] = ComponentSettings ?? throw new ViridianException($"{nameof(ComponentSettings)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RemoveSystemComponentSettings), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void RequestStateChange(RequestedStateVM RequestedState, ulong TimeoutPeriod = 0)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RequestStateChange)))
            {
                ip[nameof(RequestedState)] = (uint)RequestedState;
                ip[nameof(TimeoutPeriod)] = null; // CIM_DateTime

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RequestStateChange), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public uint TestNetworkConnection(string TargetNetworkAdapter, bool IsSender, string SenderIP, string ReceiverIP, string ReceiverMac, uint IsolationId, uint SequenceNumber)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(TestNetworkConnection)))
            {
                ip[nameof(TargetNetworkAdapter)] = TargetNetworkAdapter ?? throw new ViridianException($"{nameof(TargetNetworkAdapter)} is null!");
                ip[nameof(IsSender)] = IsSender;
                ip[nameof(SenderIP)] = SenderIP ?? throw new ViridianException($"{nameof(SenderIP)} is null!");
                ip[nameof(ReceiverIP)] = ReceiverIP ?? throw new ViridianException($"{nameof(ReceiverIP)} is null!");
                ip[nameof(ReceiverMac)] = ReceiverMac ?? throw new ViridianException($"{nameof(ReceiverMac)} is null!");
                ip[nameof(IsolationId)] = IsolationId;
                ip[nameof(SequenceNumber)] = SequenceNumber;

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(TestNetworkConnection), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return (uint)op["RoundTripTime"];
                }
            }
        }

        public void SetGuestNetworkAdapterConfiguration(ManagementObject ComputerSystem, string[] NetworkConfiguration)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(SetGuestNetworkAdapterConfiguration)))
            {
                ip[nameof(ComputerSystem)] = ComputerSystem ?? throw new ViridianException($"{nameof(ComputerSystem)} is null!");
                ip[nameof(NetworkConfiguration)] = NetworkConfiguration ?? throw new ViridianException($"{nameof(NetworkConfiguration)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(SetGuestNetworkAdapterConfiguration), ip, null))    
                    Validator.ValidateOutput(op, scope);
            }
        }
        public void SetInitialMachineConfigurationData(ManagementObject TargetSystem, byte[] ImcData)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(SetInitialMachineConfigurationData)))
            {
                ip[nameof(TargetSystem)] = TargetSystem ?? throw new ViridianException($"{nameof(TargetSystem)} is null!");
                ip[nameof(ImcData)] = ImcData ?? throw new ViridianException($"{nameof(ImcData)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(SetInitialMachineConfigurationData), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void StartService()
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(StartService)))
            using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(StartService), ip, null))
                Validator.ValidateOutput(op, scope);
        }

        public void StopService()
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(StopService)))
            using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(StopService), ip, null))
                Validator.ValidateOutput(op, scope);
        }

        public void UpgradeSystemVersion(ManagementObject ComputerSystem, string UpgradeSettingData)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(UpgradeSystemVersion)))
            {
                ip[nameof(ComputerSystem)] = ComputerSystem ?? throw new ViridianException($"{nameof(ComputerSystem)} is null!");
                ip[nameof(UpgradeSettingData)] = UpgradeSettingData ?? throw new ViridianException($"{nameof(UpgradeSettingData)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(UpgradeSystemVersion), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void ValidatePlannedSystem(ManagementObject PlannedSystem)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ValidatePlannedSystem)))
            {
                ip[nameof(PlannedSystem)] = PlannedSystem ?? throw new ViridianException($"{nameof(PlannedSystem)} is null!");

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ValidatePlannedSystem), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        ~VirtualSystemManagement()
        {
            if (Msvm_VirtualSystemManagementService != null)
                Msvm_VirtualSystemManagementService.Dispose();
        }
    }
}
