using System;
using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Utilities;

namespace Viridian.Service.Msvm
{
    public sealed class VirtualEthernetSwitchManagement
    {
        private static VirtualEthernetSwitchManagement instance = null;
        private const string serverName = ".";
        private const string scopePath = @"\Root\Virtualization\V2";
        private static ManagementObject Msvm_VirtualEthernetSwitchManagementService = null;
        private static ManagementScope scope = null;

        public enum RequestedStateVESM : uint
        {
            Enabled = 2,
            Disabled = 3,
            ShutDown = 4,
            Offline = 6,
            Test = 7,
            Defer = 8,
            Quiesce = 9,
            Reboot = 10,
            Reset = 11
        };

        private VirtualEthernetSwitchManagement()
        {
            scope = new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);

            using (var vsms = new ManagementClass(nameof(Msvm_VirtualEthernetSwitchManagementService)))
            {
                vsms.Scope = scope;

                Msvm_VirtualEthernetSwitchManagementService = Utils.GetFirstObjectFromCollection(vsms.GetInstances());
            }
        }

        public static VirtualEthernetSwitchManagement Instance
        {
            get
            {
                if (instance == null)
                    instance = new VirtualEthernetSwitchManagement();

                return instance;
            }
        }

        public ManagementObject MsvmVirtualSystemManagementService => Msvm_VirtualEthernetSwitchManagementService ?? throw new ViridianException($"{nameof(Msvm_VirtualEthernetSwitchManagementService)} is null!");
               
        #region MsvmProperties

        string InstanceID => Msvm_VirtualEthernetSwitchManagementService[nameof(InstanceID)].ToString();
        string Caption => Msvm_VirtualEthernetSwitchManagementService[nameof(Caption)].ToString();
        string Description => Msvm_VirtualEthernetSwitchManagementService[nameof(Description)].ToString();
        string ElementName => Msvm_VirtualEthernetSwitchManagementService[nameof(ElementName)].ToString();
        DateTime InstallDate => ManagementDateTimeConverter.ToDateTime(Msvm_VirtualEthernetSwitchManagementService[nameof(InstallDate)].ToString());
        string Name => Msvm_VirtualEthernetSwitchManagementService[nameof(Name)].ToString();
        ushort[] OperationalStatus => (ushort[])Msvm_VirtualEthernetSwitchManagementService[nameof(OperationalStatus)];
        string[] StatusDescriptions => (string[])Msvm_VirtualEthernetSwitchManagementService[nameof(StatusDescriptions)];
        string Status => Msvm_VirtualEthernetSwitchManagementService[nameof(Status)].ToString();
        ushort HealthState => (ushort)Msvm_VirtualEthernetSwitchManagementService[nameof(HealthState)];
        ushort CommunicationStatus => (ushort)Msvm_VirtualEthernetSwitchManagementService[nameof(CommunicationStatus)];
        ushort DetailedStatus => (ushort)Msvm_VirtualEthernetSwitchManagementService[nameof(DetailedStatus)];
        ushort OperatingStatus => (ushort)Msvm_VirtualEthernetSwitchManagementService[nameof(OperatingStatus)];
        ushort PrimaryStatus => (ushort)Msvm_VirtualEthernetSwitchManagementService[nameof(PrimaryStatus)];
        ushort EnabledState => (ushort)Msvm_VirtualEthernetSwitchManagementService[nameof(EnabledState)];
        string OtherEnabledState => Msvm_VirtualEthernetSwitchManagementService[nameof(OtherEnabledState)].ToString();
        ushort RequestedState => (ushort)Msvm_VirtualEthernetSwitchManagementService[nameof(RequestedState)];
        ushort EnabledDefault => (ushort)Msvm_VirtualEthernetSwitchManagementService[nameof(EnabledDefault)];
        DateTime TimeOfLastStateChange => ManagementDateTimeConverter.ToDateTime(Msvm_VirtualEthernetSwitchManagementService[nameof(TimeOfLastStateChange)].ToString());
        ushort[] AvailableRequestedStates => (ushort[])Msvm_VirtualEthernetSwitchManagementService[nameof(AvailableRequestedStates)];
        ushort TransitioningToState => (ushort)Msvm_VirtualEthernetSwitchManagementService[nameof(TransitioningToState)];
        string SystemCreationClassName => Msvm_VirtualEthernetSwitchManagementService[nameof(SystemCreationClassName)].ToString();
        string SystemName => Msvm_VirtualEthernetSwitchManagementService[nameof(SystemName)].ToString();
        string CreationClassName => Msvm_VirtualEthernetSwitchManagementService[nameof(CreationClassName)].ToString();
        string PrimaryOwnerName => Msvm_VirtualEthernetSwitchManagementService[nameof(PrimaryOwnerName)].ToString();
        string PrimaryOwnerContact => Msvm_VirtualEthernetSwitchManagementService[nameof(PrimaryOwnerContact)].ToString();
        string StartMode => Msvm_VirtualEthernetSwitchManagementService[nameof(StartMode)].ToString();
        bool Started => (bool)Msvm_VirtualEthernetSwitchManagementService[nameof(Started)];

        #endregion

        public string[] AddFeatureSettings(string AffectedConfiguration, string[] FeatureSettings)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(AddFeatureSettings)))
            {
                ip[nameof(AffectedConfiguration)] = AffectedConfiguration ?? throw new ViridianException($"{nameof(AffectedConfiguration)} is null!");
                ip[nameof(FeatureSettings)] = FeatureSettings ?? throw new ViridianException($"{nameof(FeatureSettings)} is null!");

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(AddFeatureSettings), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingFeatureSettings"] as string[];
                }
            }
        }

        public string[] AddResourceSettings(string AffectedConfiguration, string[] ResourceSettings)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(AddResourceSettings)))
            {
                ip[nameof(AffectedConfiguration)] = AffectedConfiguration ?? throw new ViridianException($"{nameof(AffectedConfiguration)} is null!");
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ViridianException($"{nameof(ResourceSettings)} is null!");

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(AddResourceSettings), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingResourceSettings"] as string[];
                }
            }
        }

        public string DefineSystem(string SystemSettings, string[] ResourceSettings, string ReferenceConfiguration)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(DefineSystem)))
            {
                ip[nameof(SystemSettings)] = SystemSettings ?? throw new ViridianException($"{nameof(SystemSettings)} is null!");
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ViridianException($"{nameof(ResourceSettings)} is null!");
                ip[nameof(ReferenceConfiguration)] = ResourceSettings ?? throw new ViridianException($"{nameof(ReferenceConfiguration)} is null!");

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(DefineSystem), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingSystem"] as string;
                }
            }
        }

        public void DestroySystem(string AffectedSystem)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(DestroySystem)))
            {
                ip[nameof(AffectedSystem)] = AffectedSystem ?? throw new ViridianException($"{nameof(AffectedSystem)} is null!");

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(DestroySystem), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public string[] ModifyFeatureSettings(string FeatureSettings)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(ModifyFeatureSettings)))
            {
                ip[nameof(FeatureSettings)] = FeatureSettings ?? throw new ViridianException($"{nameof(FeatureSettings)} is null!");

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(ModifyFeatureSettings), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingFeatureSettings"] as string[];
                }
            }
        }

        public string[] ModifyResourceSettings(string[] ResourceSettings)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(ModifyResourceSettings)))
            {
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ViridianException($"{nameof(ResourceSettings)} is null!");

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(ModifyResourceSettings), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingResourceSettings"] as string[];
                }
            }
        }

        public void ModifySystemSettings(string SystemSettings)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(ModifySystemSettings)))
            {
                ip[nameof(SystemSettings)] = SystemSettings ?? throw new ViridianException($"{nameof(SystemSettings)} is null!");

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(ModifySystemSettings), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void RemoveFeatureSettings(string[] FeatureSettings)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(RemoveFeatureSettings)))
            {
                ip[nameof(FeatureSettings)] = FeatureSettings ?? throw new ViridianException($"{nameof(FeatureSettings)} is null!");

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(RemoveFeatureSettings), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void RemoveResourceSettings(string[] ResourceSettings)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(RemoveResourceSettings)))
            {
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ViridianException($"{nameof(ResourceSettings)} is null!");

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(RemoveResourceSettings), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void RequestStateChange(RequestedStateVESM RequestedState, ulong TimeoutPeriod = 0)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(RequestStateChange)))
            {
                ip[nameof(RequestedState)] = (uint)RequestedState;
                ip[nameof(TimeoutPeriod)] = null; // CIM_DateTime

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(RequestStateChange), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }
        public void StartService()
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(StartService)))
            using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(StartService), ip, null))
                Validator.ValidateOutput(op, scope);
        }

        public void StopService()
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(StopService)))
            using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(StopService), ip, null))
                Validator.ValidateOutput(op, scope);
        }

    }
}
