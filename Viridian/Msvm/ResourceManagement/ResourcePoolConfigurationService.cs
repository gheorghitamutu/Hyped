using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Scopes;

namespace Viridian.Msvm.ResourceManagement
{
    public sealed class ResourcePoolConfigurationService : BaseService
    {
        private static ResourcePoolConfigurationService instance = null;

        private ResourcePoolConfigurationService() : base("Msvm_ResourcePoolConfigurationService") { }

        public static ResourcePoolConfigurationService Instance
        {
            get
            {
                if (instance == null)
                    instance = new ResourcePoolConfigurationService();

                return instance;
            }
        }

#pragma warning disable CA1303 // Do not pass literals as localized parameters
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
        public ManagementObject Msvm_ResourcePoolConfigurationService => Service ?? throw new ViridianException($"{nameof(ServiceName)} is null!");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1303 // Do not pass literals as localized parameters

        public ManagementObject CreatePool(string PoolSettings, string[] ParentPools, string[] AllocationSettings)
        {
            using (var ip = Msvm_ResourcePoolConfigurationService.GetMethodParameters(nameof(CreatePool)))
            {
                ip[nameof(PoolSettings)] = PoolSettings ?? throw new ViridianException($"{nameof(PoolSettings)} is null!");
                ip[nameof(ParentPools)] = ParentPools ?? throw new ViridianException($"{nameof(ParentPools)} is null!");
                ip[nameof(AllocationSettings)] = AllocationSettings ?? throw new ViridianException($"{nameof(AllocationSettings)} is null!");

                using (var op = Msvm_ResourcePoolConfigurationService.InvokeMethod(nameof(CreatePool), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return new ManagementObject(Scope.Virtualization.SpecificScope, new ManagementPath(op["Pool"].ToString()), null);
                }
            }
        }

        public void DeletePool(string Pool)
        {
            using (var ip = Msvm_ResourcePoolConfigurationService.GetMethodParameters(nameof(DeletePool)))
            {
                ip[nameof(Pool)] = Pool ?? throw new ViridianException($"{nameof(Pool)} is null!");

                using (var op = Msvm_ResourcePoolConfigurationService.InvokeMethod(nameof(DeletePool), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
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
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void ModifyPoolSettings(string ChildPool, string PoolSettings)
        {
            using (var ip = Msvm_ResourcePoolConfigurationService.GetMethodParameters(nameof(ModifyPoolSettings)))
            {
                ip[nameof(ChildPool)] = ChildPool ?? throw new ViridianException($"{nameof(ChildPool)} is null!");
                ip[nameof(PoolSettings)] = PoolSettings ?? throw new ViridianException($"{nameof(PoolSettings)} is null!");

                using (var op = Msvm_ResourcePoolConfigurationService.InvokeMethod(nameof(ModifyPoolSettings), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        ~ResourcePoolConfigurationService()
        {
            if (Msvm_ResourcePoolConfigurationService != null)
                Msvm_ResourcePoolConfigurationService.Dispose();
        }
    }
}
