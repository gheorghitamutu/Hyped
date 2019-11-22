using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Utilities;

namespace Viridian.Resources.Msvm
{
    class ResourcePoolConfiguration
    {
        // Pool IDs are delimited by "[p], e.g. "[p]Child Pool A[p][p]Child Pool B[p]"
        string[] poolDelimiter = { "[p]" };

        // Parent pool host resources are specified by a 2-D array. Each pool is delimited
        // by a "[p]";  Each host resource is delimited by  a "[h]". For example,
        // "[p][h]Child A, Resource 1[h][h]Child A, Resource 2[h][p][p][h]Child B, Resource 1[h][p]"
        string[] hostResourceDelimiter = { "[h]" };

        public ManagementObject GetResourcePoolConfigurationService(ManagementScope scope)
        {
            using (var rpcsc = new ManagementClass("Msvm_ResourcePoolConfigurationService") { Scope = scope })
                return Utils.GetFirstObjectFromCollection(rpcsc.GetInstances());
        }

        public ManagementObject CreatePool(ManagementScope scope, string resourceDisplayName, string childPoolId, string childPoolName, string parentPoolIdsString, string parentHostResourcesString)
        {
            var parentPoolIdArray = Utils.GetOneDimensionalArray(parentPoolIdsString, poolDelimiter);
            var parentHostResourcesArray = Utils.GetTwoDimensionalArray(parentHostResourcesString, poolDelimiter, hostResourceDelimiter);

            if (parentPoolIdArray.Length == 0)
                throw new ViridianException($"At least one parent pool must be specified when creating a child resource pool (PoolId {childPoolId})");

            if (parentPoolIdArray.Length != parentHostResourcesArray.Length)
                throw new ViridianException($"When creating a child resource pool, a host resource must be specified for each parent pool. Shared allocations are not supported (PoolId {childPoolId})");

            using (var rpcs = GetResourcePoolConfigurationService(scope))
            using (var ip = rpcs.GetMethodParameters("CreatePool"))
            {
                var rt = Utils.GetResourceType(resourceDisplayName);
                var rst = Utils.GetResourceSubType(resourceDisplayName);

                ip["PoolSettings"] = new ResourcePoolSettingData().GetSettingsForSpecificPool(scope, rt, rst, childPoolId, childPoolName);
                ip["ParentPools"] = Utils.GetParentPoolArrayFromPoolIds(scope, rt, rst, parentPoolIdArray);
                ip["AllocationSettings"] = ResourceAllocationSettingData.GetNewPoolAllocationSettingsArray(scope, rt, rst, parentPoolIdArray, parentHostResourcesArray);

                using (var op = rpcs.InvokeMethod("CreatePool", ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return new ManagementObject(scope, new ManagementPath(op["Pool"].ToString()), null);
                }
            }
        }

        public void ModifyPoolResourcesByPath(ManagementScope scope, ManagementObject configurationService, string resourceType, string resourceSubType, string childPool, string[] parentPoolIdArray, string[][] parentHostResourcesArray)
        {
            if (parentPoolIdArray.Length == 0)            
                throw new ManagementException($"At least one parent pool must be specified when modifying a resource pool's host resources (poolPath {childPool})");            

            if (parentPoolIdArray.Length != parentHostResourcesArray.Length)            
                throw new ManagementException($"When modifying a child resource pool's host resources, a host resource must be specified for each parent pool. Shared allocations are not supported (poolPath {childPool})");
            
            using (var ip = configurationService.GetMethodParameters("ModifyPoolResources"))
            {
                ip["ChildPool"] = childPool;
                ip["ParentPools"] = Utils.GetParentPoolArrayFromPoolIds(scope, resourceType, resourceSubType, parentPoolIdArray);
                ip["AllocationSettings"] = ResourceAllocationSettingData.GetNewPoolAllocationSettingsArray(scope, resourceType, resourceSubType, parentPoolIdArray, parentHostResourcesArray);

                using (var op = configurationService.InvokeMethod("ModifyPoolResources", ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void ModifyPoolResources(ManagementScope scope, string resourceDisplayName, string poolId, string parentPoolIdsString, string parentHostResourcesString)
        {            
            using (var rpsc = GetResourcePoolConfigurationService(scope))
            {
                var rt = Utils.GetResourceType(resourceDisplayName);
                var rst = Utils.GetResourceSubType(resourceDisplayName);
                var pp = Utils.GetResourcePoolPath(scope, rt, rst, poolId);
                var parentPoolIdArray = Utils.GetOneDimensionalArray(parentPoolIdsString, poolDelimiter);
                var parentHostResourcesArray = Utils.GetTwoDimensionalArray(parentHostResourcesString, poolDelimiter, hostResourceDelimiter);

                ModifyPoolResourcesByPath(scope, rpsc, rt, rst, pp, parentPoolIdArray, parentHostResourcesArray);
            }
        }

        public void ModifyPoolSettingsByPath(ManagementScope scope, ManagementObject configurationService, string childPool, string poolSettings)
        {
            using (var ip = configurationService.GetMethodParameters("ModifyPoolSettings"))
            {
                ip["ChildPool"] = childPool;
                ip["PoolSettings"] = poolSettings;

                using (var op = configurationService.InvokeMethod("ModifyPoolSettings", ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }
        
        public void ModifyPoolSettings(ManagementScope scope, string resourceDisplayName, string poolId, string newPoolId, string newPoolName)
        {
            using (var rpcs = GetResourcePoolConfigurationService(scope))
            {
                var rt = Utils.GetResourceType(resourceDisplayName);
                var rst = Utils.GetResourceSubType(resourceDisplayName);
                var rpsd = new ResourcePoolSettingData().GetSettingsForSpecificPool(scope, rt, rst, newPoolId, newPoolName);
                var pp = Utils.GetResourcePoolPath(scope, rt, rst, poolId);

                ModifyPoolSettingsByPath(scope, rpcs, pp, rpsd);
            }
        }
        
        public void DeletePool(ManagementScope scope, string resourceDisplayName, string poolId)
        {
            using (var rpcs = GetResourcePoolConfigurationService(scope))
            using (var ip = rpcs.GetMethodParameters("DeletePool"))
            {
                var rt = Utils.GetResourceType(resourceDisplayName);
                var rst = Utils.GetResourceSubType(resourceDisplayName);

                ip["Pool"] = Utils.GetResourcePoolPath(scope, rt, rst, poolId);

                using (var op = rpcs.InvokeMethod("DeletePool", ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }
    }
}
