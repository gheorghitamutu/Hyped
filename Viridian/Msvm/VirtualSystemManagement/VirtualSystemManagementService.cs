using System;
using System.Management;
using Viridian.Job;
using Viridian.Scopes;

namespace Viridian.Msvm.VirtualSystemManagement
{
    public sealed class VirtualSystemManagementService : BaseService
    {
        private static VirtualSystemManagementService instance = null;
        
        public enum RequestedStateVSM : uint
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

        private VirtualSystemManagementService() : base("Msvm_VirtualSystemManagementService") { }

        public static VirtualSystemManagementService Instance
        {
            get
            {
                if (instance == null)
                    instance = new VirtualSystemManagementService();

                return instance;
            }
        }

        public ManagementObject Msvm_VirtualSystemManagementService => Service;

        public string[] AddBootSourceSettings(string AffectedConfiguration, string[] BootSourceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(AddBootSourceSettings)))
            {
                ip[nameof(AffectedConfiguration)] = AffectedConfiguration ?? throw new ArgumentNullException(nameof(AffectedConfiguration));
                ip[nameof(BootSourceSettings)] = BootSourceSettings ?? throw new ArgumentNullException(nameof(BootSourceSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(AddBootSourceSettings), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ResultingBootSourceSettings"] as string[];
                }
            }
        }

        public string[] AddFeatureSettings(string AffectedConfiguration, string[] FeatureSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(AddFeatureSettings)))
            {
                ip[nameof(AffectedConfiguration)] = AffectedConfiguration ?? throw new ArgumentNullException(nameof(AffectedConfiguration));
                ip[nameof(FeatureSettings)] = FeatureSettings ?? throw new ArgumentNullException(nameof(FeatureSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(AddFeatureSettings), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ResultingFeatureSettings"] as string[];
                }
            }
        }

        public string[] AddGuestServiceSettings(string AffectedConfiguration, string[] GuestServiceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(AddGuestServiceSettings)))
            {
                ip[nameof(AffectedConfiguration)] = AffectedConfiguration ?? throw new ArgumentNullException(nameof(AffectedConfiguration));
                ip[nameof(GuestServiceSettings)] = GuestServiceSettings ?? throw new ArgumentNullException(nameof(GuestServiceSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(AddGuestServiceSettings), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ResultingGuestServiceSettings"] as string[];
                }
            }
        }

        public void AddFibreChannelChap(string[] FcPortSettings, byte[] SecretEncoding, byte[] SharedSecret)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(AddFibreChannelChap)))
            {
                ip[nameof(FcPortSettings)] = FcPortSettings ?? throw new ArgumentNullException(nameof(FcPortSettings));
                ip[nameof(SecretEncoding)] = SecretEncoding ?? throw new ArgumentNullException(nameof(SecretEncoding));
                ip[nameof(SharedSecret)] = SharedSecret ?? throw new ArgumentNullException(nameof(SharedSecret));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(AddFibreChannelChap), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void AddKvpItems(string TargetSystem, string[] DataItems)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(AddKvpItems)))
            {
                ip[nameof(TargetSystem)] = TargetSystem ?? throw new ArgumentNullException(nameof(TargetSystem));
                ip[nameof(DataItems)] = DataItems ?? throw new ArgumentNullException(nameof(DataItems));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(AddKvpItems), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public string[] AddResourceSettings(ManagementObject AffectedConfiguration, string[] ResourceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(AddResourceSettings)))
            {
                ip[nameof(AffectedConfiguration)] = AffectedConfiguration ?? throw new ArgumentNullException(nameof(AffectedConfiguration));
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ArgumentNullException(nameof(ResourceSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(AddResourceSettings), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ResultingResourceSettings"] as string[];
                }
            }
        }

        public string[] AddSystemComponentSettings(ManagementObject AffectedConfiguration, string[] ComponentSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(AddSystemComponentSettings)))
            {
                ip[nameof(AffectedConfiguration)] = AffectedConfiguration ?? throw new ArgumentNullException(nameof(AffectedConfiguration));
                ip[nameof(ComponentSettings)] = ComponentSettings ?? throw new ArgumentNullException(nameof(ComponentSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(AddSystemComponentSettings), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ResultingComponentSettings"] as string[];
                }
            }
        }

        public ManagementObject DefinePlannedSystem(string SystemSettings, string[] ResourceSettings, string ReferenceConfiguration)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(DefinePlannedSystem)))
            {
                ip[nameof(SystemSettings)] = SystemSettings ?? throw new ArgumentNullException(nameof(SystemSettings));
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ArgumentNullException(nameof(ResourceSettings));
                ip[nameof(ReferenceConfiguration)] = ReferenceConfiguration ?? throw new ArgumentNullException(nameof(ReferenceConfiguration));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(DefinePlannedSystem), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ResultingSystem"] as ManagementObject;
                }
            }
        }

        public ManagementObject DefineSystem(string SystemSettings, string[] ResourceSettings, string ReferenceConfiguration)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(DefineSystem)))
            {
                ip[nameof(SystemSettings)] = SystemSettings ?? throw new ArgumentNullException(nameof(SystemSettings));
                ip[nameof(ResourceSettings)] = ResourceSettings;
                ip[nameof(ReferenceConfiguration)] = ReferenceConfiguration;

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(DefineSystem), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return new ManagementObject(new ManagementPath(op["ResultingSystem"] as string));
                }
            }
        }

        public void DestroySystem(ManagementObject AffectedSystem)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(DestroySystem)))
            {
                ip[nameof(AffectedSystem)] = AffectedSystem ?? throw new ArgumentNullException(nameof(AffectedSystem));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(DestroySystem), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public string DiagnoseNetworkConnection(ManagementObject TargetNetworkAdapter, string DiagnosticSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(DiagnoseNetworkConnection)))
            {
                ip[nameof(TargetNetworkAdapter)] = TargetNetworkAdapter ?? throw new ArgumentNullException(nameof(TargetNetworkAdapter));
                ip[nameof(DiagnosticSettings)] = DiagnosticSettings ?? throw new ArgumentNullException(nameof(DiagnosticSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(DiagnoseNetworkConnection), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["DiagnosticInformation"] as string;
                }
            }
        }

        public void ExportSystemDefinition(ManagementObject ComputerSystem, string ExportDirectory, string ExportSettingData)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ExportSystemDefinition)))
            {
                ip[nameof(ComputerSystem)] = ComputerSystem ?? throw new ArgumentNullException(nameof(ComputerSystem));
                ip[nameof(ExportDirectory)] = ExportDirectory ?? throw new ArgumentNullException(nameof(ExportDirectory));
                ip[nameof(ExportSettingData)] = ExportSettingData ?? throw new ArgumentNullException(nameof(ExportSettingData));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ExportSystemDefinition), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public string FormatError(string[] Errors)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(FormatError)))
            {
                ip[nameof(Errors)] = Errors ?? throw new ArgumentNullException(nameof(Errors));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(FormatError), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

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
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["GeneratedWwpn"] as string[];
                }
            }
        }

        public string GetCurrentWwpnFromGenerator()
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(GetCurrentWwpnFromGenerator)))
            using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(GetCurrentWwpnFromGenerator), ip, null))
            {
                Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                return op["CurrentWwpn"] as string;
            }
        }

        public string[] GetDefinitionFileSummaryInformation(string[] DefinitionFiles)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(GetDefinitionFileSummaryInformation)))
            {
                ip[nameof(DefinitionFiles)] = DefinitionFiles ?? throw new ArgumentNullException(nameof(DefinitionFiles));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(GetDefinitionFileSummaryInformation), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["SummaryInformation"] as string[];
                }
            }
        }

        public ulong GetSizeOfSystemFiles(string Vssd)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(GetSizeOfSystemFiles)))
            {
                ip[nameof(Vssd)] = Vssd ?? throw new ArgumentNullException(nameof(Vssd));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(GetSizeOfSystemFiles), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return (ulong)op["Size"];
                }
            }
        }

        public ManagementBaseObject[] GetSummaryInformation(ManagementObject[] SettingData, int[] RequestedInformation)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(GetSummaryInformation)))
            {
                ip[nameof(SettingData)] = SettingData ?? throw new ArgumentNullException(nameof(SettingData));
                ip[nameof(RequestedInformation)] = RequestedInformation ?? throw new ArgumentNullException(nameof(RequestedInformation));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(GetSummaryInformation), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["SummaryInformation"] as ManagementBaseObject[];
                }
            }
        }

        public byte[] GetVirtualSystemThumbnailImage(string TargetSystem, ushort WidthPixels, ushort HeightPixels)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(GetVirtualSystemThumbnailImage)))
            {
                ip[nameof(TargetSystem)] = TargetSystem ?? throw new ArgumentNullException(nameof(TargetSystem));
                ip[nameof(WidthPixels)] = WidthPixels;
                ip[nameof(HeightPixels)] = HeightPixels;

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(GetVirtualSystemThumbnailImage), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ImageData"] as byte[];
                }
            }
        }

        public string[] ImportSnapshotDefinitions(ManagementObject PlannedSystem, string SnapshotFolder)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ImportSnapshotDefinitions)))
            {
                ip[nameof(PlannedSystem)] = PlannedSystem ?? throw new ArgumentNullException(nameof(PlannedSystem));
                ip[nameof(SnapshotFolder)] = SnapshotFolder ?? throw new ArgumentNullException(nameof(SnapshotFolder));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ImportSnapshotDefinitions), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ImportedSnapshots"] as string[];
                }
            }
        }

        public ManagementObject ImportSystemDefinition(string SystemDefinitionFile, string SnapshotFolder, bool GenerateNewSystemIdentifier)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ImportSystemDefinition)))
            {
                ip[nameof(SystemDefinitionFile)] = SystemDefinitionFile ?? throw new ArgumentNullException(nameof(SystemDefinitionFile));
                ip[nameof(SnapshotFolder)] = SnapshotFolder ?? throw new ArgumentNullException(nameof(SnapshotFolder));
                ip[nameof(GenerateNewSystemIdentifier)] = GenerateNewSystemIdentifier;

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ImportSystemDefinition), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ImportedSystem"] as ManagementObject;
                }
            }
        }

        public void ModifyDiskMergeSettings(string SettingData)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifyDiskMergeSettings)))
            {
                ip[nameof(SettingData)] = SettingData ?? throw new ArgumentNullException(nameof(SettingData));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifyDiskMergeSettings), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public ManagementObject[] ModifyFeatureSettings(string[] FeatureSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifyFeatureSettings)))
            {
                ip[nameof(FeatureSettings)] = FeatureSettings ?? throw new ArgumentNullException(nameof(FeatureSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifyFeatureSettings), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ResultingFeatureSettings"] as ManagementObject[];
                }
            }
        }

        public string[] ModifyGuestServiceSettings(string[] GuestServiceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifyGuestServiceSettings)))
            {
                ip[nameof(GuestServiceSettings)] = GuestServiceSettings ?? throw new ArgumentNullException(nameof(GuestServiceSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifyGuestServiceSettings), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ResultingGuestServiceSettings"] as string[];
                }
            }
        }

        public string[] ModifyKvpItems(string TargetSystem)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifyKvpItems)))
            {
                ip[nameof(TargetSystem)] = TargetSystem ?? throw new ArgumentNullException(nameof(TargetSystem));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifyKvpItems), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["DataItems"] as string[];
                }
            }
        }

        public string[] ModifyResourceSettings(string[] ResourceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifyResourceSettings)))
            {
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ArgumentNullException(nameof(ResourceSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifyResourceSettings), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ResultingResourceSettings"] as string[];
                }
            }
        }

        public void ModifyServiceSettings(string SettingData)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifyServiceSettings)))
            {
                ip[nameof(SettingData)] = SettingData ?? throw new ArgumentNullException(nameof(SettingData));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifyServiceSettings), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }
            
        public string[] ModifySystemComponentSettings(string[] ComponentSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifySystemComponentSettings)))
            {
                ip[nameof(ComponentSettings)] = ComponentSettings ?? throw new ArgumentNullException(nameof(ComponentSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifySystemComponentSettings), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ResultingComponentSettings"] as string[];
                }
            }
        }
            
        public void ModifySystemSettings(string SystemSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ModifySystemSettings)))
            {
                ip[nameof(SystemSettings)] = SystemSettings ?? throw new ArgumentNullException(nameof(SystemSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ModifySystemSettings), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public ManagementObject RealizePlannedSystem(ManagementObject PlannedSystem)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RealizePlannedSystem)))
            {
                ip[nameof(PlannedSystem)] = PlannedSystem ?? throw new ArgumentNullException(nameof(PlannedSystem));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RealizePlannedSystem), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ResultingSystem"] as ManagementObject;
                }
            }
        }

        public void RemoveFeatureSettings(string[] FeatureSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RemoveFeatureSettings)))
            {
                ip[nameof(FeatureSettings)] = FeatureSettings ?? throw new ArgumentNullException(nameof(FeatureSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RemoveFeatureSettings), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void RemoveBootSourceSettings(string[] BootSourceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RemoveBootSourceSettings)))
            {
                ip[nameof(BootSourceSettings)] = BootSourceSettings ?? throw new ArgumentNullException(nameof(BootSourceSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RemoveBootSourceSettings), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void RemoveFibreChannelChap(string[] FcPortSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RemoveFibreChannelChap)))
            {
                ip[nameof(FcPortSettings)] = FcPortSettings ?? throw new ArgumentNullException(nameof(FcPortSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RemoveFibreChannelChap), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void RemoveKvpItems(string TargetSystem, string[] DataItems)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RemoveKvpItems)))
            {
                ip[nameof(TargetSystem)] = TargetSystem ?? throw new ArgumentNullException(nameof(TargetSystem));
                ip[nameof(DataItems)] = DataItems ?? throw new ArgumentNullException(nameof(DataItems));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RemoveKvpItems), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void RemoveGuestServiceSettings(string[] GuestServiceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RemoveGuestServiceSettings)))
            {
                ip[nameof(GuestServiceSettings)] = GuestServiceSettings ?? throw new ArgumentNullException(nameof(GuestServiceSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RemoveGuestServiceSettings), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void RemoveResourceSettings(ManagementBaseObject[] ResourceSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RemoveResourceSettings)))
            {
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ArgumentNullException(nameof(ResourceSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RemoveResourceSettings), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void RemoveSystemComponentSettings(string[] ComponentSettings)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RemoveSystemComponentSettings)))
            {
                ip[nameof(ComponentSettings)] = ComponentSettings ?? throw new ArgumentNullException(nameof(ComponentSettings));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RemoveSystemComponentSettings), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void RequestStateChange(RequestedStateVSM RequestedState, ulong TimeoutPeriod = 0)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(RequestStateChange)))
            {
                ip[nameof(RequestedState)] = (uint)RequestedState;
                ip[nameof(TimeoutPeriod)] = null; // CIM_DateTime

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(RequestStateChange), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public uint TestNetworkConnection(string TargetNetworkAdapter, bool IsSender, string SenderIP, string ReceiverIP, string ReceiverMac, uint IsolationId, uint SequenceNumber)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(TestNetworkConnection)))
            {
                ip[nameof(TargetNetworkAdapter)] = TargetNetworkAdapter ?? throw new ArgumentNullException(nameof(TargetNetworkAdapter));
                ip[nameof(IsSender)] = IsSender;
                ip[nameof(SenderIP)] = SenderIP ?? throw new ArgumentNullException(nameof(SenderIP));
                ip[nameof(ReceiverIP)] = ReceiverIP ?? throw new ArgumentNullException(nameof(ReceiverIP));
                ip[nameof(ReceiverMac)] = ReceiverMac ?? throw new ArgumentNullException(nameof(ReceiverMac));
                ip[nameof(IsolationId)] = IsolationId;
                ip[nameof(SequenceNumber)] = SequenceNumber;

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(TestNetworkConnection), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return (uint)op["RoundTripTime"];
                }
            }
        }

        public void SetGuestNetworkAdapterConfiguration(ManagementObject ComputerSystem, string[] NetworkConfiguration)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(SetGuestNetworkAdapterConfiguration)))
            {
                ip[nameof(ComputerSystem)] = ComputerSystem ?? throw new ArgumentNullException(nameof(ComputerSystem));
                ip[nameof(NetworkConfiguration)] = NetworkConfiguration ?? throw new ArgumentNullException(nameof(NetworkConfiguration));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(SetGuestNetworkAdapterConfiguration), ip, null))    
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }
        public void SetInitialMachineConfigurationData(ManagementObject TargetSystem, byte[] ImcData)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(SetInitialMachineConfigurationData)))
            {
                ip[nameof(TargetSystem)] = TargetSystem ?? throw new ArgumentNullException(nameof(TargetSystem));
                ip[nameof(ImcData)] = ImcData ?? throw new ArgumentNullException(nameof(ImcData));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(SetInitialMachineConfigurationData), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public override void StartService()
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(StartService)))
            using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(StartService), ip, null))
                Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
        }

        public override void StopService()
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(StopService)))
            using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(StopService), ip, null))
                Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
        }

        public void UpgradeSystemVersion(ManagementObject ComputerSystem, string UpgradeSettingData)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(UpgradeSystemVersion)))
            {
                ip[nameof(ComputerSystem)] = ComputerSystem ?? throw new ArgumentNullException(nameof(ComputerSystem));
                ip[nameof(UpgradeSettingData)] = UpgradeSettingData ?? throw new ArgumentNullException(nameof(UpgradeSettingData));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(UpgradeSystemVersion), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void ValidatePlannedSystem(ManagementObject PlannedSystem)
        {
            using (var ip = Msvm_VirtualSystemManagementService.GetMethodParameters(nameof(ValidatePlannedSystem)))
            {
                ip[nameof(PlannedSystem)] = PlannedSystem ?? throw new ArgumentNullException(nameof(PlannedSystem));

                using (var op = Msvm_VirtualSystemManagementService.InvokeMethod(nameof(ValidatePlannedSystem), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        ~VirtualSystemManagementService()
        {
            if (Msvm_VirtualSystemManagementService != null)
                Msvm_VirtualSystemManagementService.Dispose();
        }
    }
}
