using System;
using System.Management;
using Viridian.Job;
using Viridian.Scopes;

namespace Viridian.Msvm.Networking
{
    public sealed class VirtualEthernetSwitchManagementService : BaseService
    {
        private static VirtualEthernetSwitchManagementService instance = null;

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

        private VirtualEthernetSwitchManagementService() : base("Msvm_VirtualEthernetSwitchManagementService") { }

        public static VirtualEthernetSwitchManagementService Instance
        {
            get
            {
                if (instance == null)
                    instance = new VirtualEthernetSwitchManagementService();

                return instance;
            }
        }

#pragma warning disable CA1303 // Do not pass literals as localized parameters
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
        public ManagementObject Msvm_VirtualEthernetSwitchManagementService => Service ?? throw new NullReferenceException(nameof(Service));
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1303 // Do not pass literals as localized parameters

        public string[] AddFeatureSettings(string AffectedConfiguration, string[] FeatureSettings)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(AddFeatureSettings)))
            {
                ip[nameof(AffectedConfiguration)] = AffectedConfiguration ?? throw new ArgumentNullException(nameof(AffectedConfiguration));
                ip[nameof(FeatureSettings)] = FeatureSettings ?? throw new ArgumentNullException(nameof(FeatureSettings));

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(AddFeatureSettings), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);

                    return op["ResultingFeatureSettings"] as string[];
                }
            }
        }

        public string[] AddResourceSettings(string AffectedConfiguration, string[] ResourceSettings)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(AddResourceSettings)))
            {
                ip[nameof(AffectedConfiguration)] = AffectedConfiguration ?? throw new ArgumentNullException(nameof(AffectedConfiguration));
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ArgumentNullException(nameof(ResourceSettings));

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(AddResourceSettings), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);

                    return op["ResultingResourceSettings"] as string[];
                }
            }
        }

        public ManagementObject DefineSystem(string SystemSettings, string[] ResourceSettings, string ReferenceConfiguration)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(DefineSystem)))
            {
                ip[nameof(SystemSettings)] = SystemSettings ?? throw new ArgumentNullException(nameof(SystemSettings));
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ArgumentNullException(nameof(ResourceSettings));
                ip[nameof(ReferenceConfiguration)] = ResourceSettings ?? throw new ArgumentNullException(nameof(ResourceSettings));

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(DefineSystem), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);

                    return new ManagementObject(new ManagementPath(op["ResultingSystem"] as string));
                }
            }
        }

        public void DestroySystem(ManagementObject AffectedSystem)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(DestroySystem)))
            {
                ip[nameof(AffectedSystem)] = AffectedSystem ?? throw new ArgumentNullException(nameof(AffectedSystem));

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(DestroySystem), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);
            }
        }

        public string[] ModifyFeatureSettings(string FeatureSettings)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(ModifyFeatureSettings)))
            {
                ip[nameof(FeatureSettings)] = FeatureSettings ?? throw new ArgumentNullException(nameof(FeatureSettings));

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(ModifyFeatureSettings), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);

                    return op["ResultingFeatureSettings"] as string[];
                }
            }
        }

        public string[] ModifyResourceSettings(string[] ResourceSettings)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(ModifyResourceSettings)))
            {
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ArgumentNullException(nameof(ResourceSettings));

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(ModifyResourceSettings), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);

                    return op["ResultingResourceSettings"] as string[];
                }
            }
        }

        public void ModifySystemSettings(string SystemSettings)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(ModifySystemSettings)))
            {
                ip[nameof(SystemSettings)] = SystemSettings ?? throw new ArgumentNullException(nameof(SystemSettings));

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(ModifySystemSettings), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);
            }
        }

        public void RemoveFeatureSettings(string[] FeatureSettings)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(RemoveFeatureSettings)))
            {
                ip[nameof(FeatureSettings)] = FeatureSettings ?? throw new ArgumentNullException(nameof(RemoveFeatureSettings));

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(RemoveFeatureSettings), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);
            }
        }

        public void RemoveResourceSettings(string[] ResourceSettings)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(RemoveResourceSettings)))
            {
                ip[nameof(ResourceSettings)] = ResourceSettings ?? throw new ArgumentNullException(nameof(ResourceSettings));

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(RemoveResourceSettings), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);
            }
        }

        public void RequestStateChange(RequestedStateVESM RequestedState, ulong TimeoutPeriod = 0)
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(RequestStateChange)))
            {
                ip[nameof(RequestedState)] = (uint)RequestedState;
                ip[nameof(TimeoutPeriod)] = null; // CIM_DateTime

                using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(RequestStateChange), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);
            }
        }

        public override void StartService()
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(StartService)))
            using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(StartService), ip, null))
                Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);
        }

        public override void StopService()
        {
            using (var ip = Msvm_VirtualEthernetSwitchManagementService.GetMethodParameters(nameof(StopService)))
            using (var op = Msvm_VirtualEthernetSwitchManagementService.InvokeMethod(nameof(StopService), ip, null))
                Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);
        }

    }
}
