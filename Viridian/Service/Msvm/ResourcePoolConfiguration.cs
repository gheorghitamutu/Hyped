using System;
using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Utilities;

namespace Viridian.Resources.Msvm
{
    public sealed class ResourcePoolConfiguration
    {
        private static ResourcePoolConfiguration instance = null;
        private const string serverName = ".";
        private const string scopePath = @"\Root\Virtualization\V2";
        private static ManagementObject Msvm_ResourcePoolConfigurationService = null;
        private static ManagementScope scope = null;

        private ResourcePoolConfiguration()
        {
            scope = new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);

            using (var vsms = new ManagementClass(nameof(Msvm_ResourcePoolConfigurationService)))
            {
                vsms.Scope = scope;

                Msvm_ResourcePoolConfigurationService = Utils.GetFirstObjectFromCollection(vsms.GetInstances());
            }
        }

        public static ResourcePoolConfiguration Instance
        {
            get
            {
                if (instance == null)
                    instance = new ResourcePoolConfiguration();

                return instance;
            }
        }

        public ManagementObject MsvmResourcePoolConfigurationService => Msvm_ResourcePoolConfigurationService ?? throw new ViridianException($"{nameof(Msvm_ResourcePoolConfigurationService)} is null!");

        #region MsvmProperties

        string InstanceID => MsvmResourcePoolConfigurationService[nameof(InstanceID)].ToString();
        string Caption => MsvmResourcePoolConfigurationService[nameof(Caption)].ToString();
        string Description => MsvmResourcePoolConfigurationService[nameof(Description)].ToString();
        string ElementName => MsvmResourcePoolConfigurationService[nameof(ElementName)].ToString();
        DateTime InstallDate => (DateTime)MsvmResourcePoolConfigurationService[nameof(InstallDate)];
        ushort[] OperationalStatus => (ushort[])MsvmResourcePoolConfigurationService[nameof(OperationalStatus)];
        string[] StatusDescriptions => (string[])MsvmResourcePoolConfigurationService[nameof(StatusDescriptions)];
        string Status => MsvmResourcePoolConfigurationService[nameof(Status)].ToString();
        ushort HealthState => (ushort)MsvmResourcePoolConfigurationService[nameof(HealthState)];
        ushort CommunicationStatus => (ushort)MsvmResourcePoolConfigurationService[nameof(CommunicationStatus)];
        ushort DetailedStatus => (ushort)MsvmResourcePoolConfigurationService[nameof(DetailedStatus)];
        ushort OperatingStatus => (ushort)MsvmResourcePoolConfigurationService[nameof(OperatingStatus)];
        ushort PrimaryStatus => (ushort)MsvmResourcePoolConfigurationService[nameof(PrimaryStatus)];
        ushort EnabledState => (ushort)MsvmResourcePoolConfigurationService[nameof(EnabledState)];
        string OtherEnabledState => MsvmResourcePoolConfigurationService[nameof(OtherEnabledState)].ToString();
        ushort RequestedState => (ushort)MsvmResourcePoolConfigurationService[nameof(RequestedState)];
        ushort EnabledDefault => (ushort)MsvmResourcePoolConfigurationService[nameof(EnabledDefault)];
        DateTime TimeOfLastStateChange => (DateTime)MsvmResourcePoolConfigurationService[nameof(TimeOfLastStateChange)];
        ushort[] AvailableRequestedStates => (ushort[])MsvmResourcePoolConfigurationService[nameof(AvailableRequestedStates)];
        ushort TransitioningToState => (ushort)MsvmResourcePoolConfigurationService[nameof(TransitioningToState)];
        string SystemCreationClassName => MsvmResourcePoolConfigurationService[nameof(SystemCreationClassName)].ToString();
        string SystemName => MsvmResourcePoolConfigurationService[nameof(SystemName)].ToString();
        string CreationClassName => MsvmResourcePoolConfigurationService[nameof(CreationClassName)].ToString();
        string Name => MsvmResourcePoolConfigurationService[nameof(Name)].ToString();
        string PrimaryOwnerName => MsvmResourcePoolConfigurationService[nameof(PrimaryOwnerName)].ToString();
        string PrimaryOwnerContact => MsvmResourcePoolConfigurationService[nameof(PrimaryOwnerContact)].ToString();
        string StartMode => MsvmResourcePoolConfigurationService[nameof(StartMode)].ToString();
        bool Started => (bool)MsvmResourcePoolConfigurationService[nameof(Started)];

        #endregion

        public ManagementObject CreatePool(string PoolSettings, string[] ParentPools, string[] AllocationSettings)
        {
            using (var ip = Msvm_ResourcePoolConfigurationService.GetMethodParameters(nameof(CreatePool)))
            {
                ip[nameof(PoolSettings)] = PoolSettings ?? throw new ViridianException($"{nameof(PoolSettings)} is null!");
                ip[nameof(ParentPools)] = ParentPools ?? throw new ViridianException($"{nameof(ParentPools)} is null!");
                ip[nameof(AllocationSettings)] = AllocationSettings ?? throw new ViridianException($"{nameof(AllocationSettings)} is null!");

                using (var op = Msvm_ResourcePoolConfigurationService.InvokeMethod(nameof(CreatePool), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return new ManagementObject(scope, new ManagementPath(op["Pool"].ToString()), null);
                }
            }
        }

        public void DeletePool(string Pool)
        {
            using (var ip = Msvm_ResourcePoolConfigurationService.GetMethodParameters(nameof(DeletePool)))
            {
                ip[nameof(Pool)] = Pool ?? throw new ViridianException($"{nameof(Pool)} is null!");

                using (var op = Msvm_ResourcePoolConfigurationService.InvokeMethod(nameof(DeletePool), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void ModifyPoolResources(string ChildPool, string[] ParentPools, string[] AllocationSettings)
        {
            using (var ip = Msvm_ResourcePoolConfigurationService.GetMethodParameters(nameof(ModifyPoolResources)))
            {
                ip[nameof(ChildPool)] = ChildPool ?? throw new ViridianException($"{nameof(ChildPool)} is null!");
                ip[nameof(ParentPools)] = ParentPools ?? throw new ViridianException($"{nameof(ParentPools)} is null!");
                ip[nameof(AllocationSettings)] = AllocationSettings ?? throw new ViridianException($"{nameof(AllocationSettings)} is null!");

                using (var op = Msvm_ResourcePoolConfigurationService.InvokeMethod(nameof(ModifyPoolResources), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void ModifyPoolSettings(string ChildPool, string PoolSettings)
        {
            using (var ip = Msvm_ResourcePoolConfigurationService.GetMethodParameters(nameof(ModifyPoolSettings)))
            {
                ip[nameof(ChildPool)] = ChildPool ?? throw new ViridianException($"{nameof(ChildPool)} is null!");
                ip[nameof(PoolSettings)] = PoolSettings ?? throw new ViridianException($"{nameof(PoolSettings)} is null!");

                using (var op = Msvm_ResourcePoolConfigurationService.InvokeMethod(nameof(ModifyPoolSettings), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        ~ResourcePoolConfiguration()
        {
            if (Msvm_ResourcePoolConfigurationService != null)
                Msvm_ResourcePoolConfigurationService.Dispose();
        }
    }
}
