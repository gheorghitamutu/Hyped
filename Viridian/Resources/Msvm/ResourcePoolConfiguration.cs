using System.Management;
using Viridian.Exceptions;
using Viridian.Utilities;

namespace Viridian.Resources.Msvm
{
    class ResourcePoolConfiguration
    {
        public ManagementObject GetResourcePoolConfigurationService(ManagementScope scope)
        {
            using (var rpcsc = new ManagementClass("Msvm_ResourcePoolConfigurationService") { Scope = scope })
            {
                return Utils.GetFirstObjectFromCollection(rpcsc.GetInstances());
            }
        }

        public ManagementObject CreatePoolHelper(ManagementScope scope, ManagementObject configurationService, string resourceType, string resourceSubType, string childPoolId, string childPoolName, string[] parentPoolIdArray, string[][] parentHostResourcesArray)
        {
            if (parentPoolIdArray.Length == 0)            
                throw new ViridianException($"At least one parent pool must be specified when creating a child resource pool (PoolId {childPoolId})");
            
            if (parentPoolIdArray.Length != parentHostResourcesArray.Length)            
                throw new ViridianException($"When creating a child resource pool, a host resource must be specified for each parent pool. Shared allocations are not supported (PoolId {childPoolId})");
            
            var poolSettings = new ResourcePoolSettingData().GetSettingsForSpecificPool(scope, resourceType, resourceSubType, childPoolId, childPoolName);

            var parentPools = Utils.GetParentPoolArrayFromPoolIds(scope, resourceType, resourceSubType, parentPoolIdArray);

            var allocationSettings = new ResourceAllocationSettingData().GetNewPoolAllocationSettingsArray(scope, resourceType, resourceSubType, parentPoolIdArray, parentHostResourcesArray);

            using (var inParams = configurationService.GetMethodParameters("CreatePool"))
            {
                inParams["PoolSettings"] = poolSettings;
                inParams["ParentPools"] = parentPools;
                inParams["AllocationSettings"] = allocationSettings;

                using (var outParams = configurationService.InvokeMethod("CreatePool", inParams, null))
                {
                    Job.Validator.ValidateOutput(outParams, scope);

                    if (outParams == null)
                        return new ManagementObject();

                    var poolPath = outParams["Pool"].ToString();

                    return new ManagementObject(scope, new ManagementPath(poolPath), null);
                }
            }
        }

        public ManagementObject CreatePool(ManagementScope scope, string resourceDisplayName, string childPoolId, string childPoolName, string parentPoolIdsString, string parentHostResourcesString)
        {
            string[] poolDelimiter = { "[p]" };

            // Pool IDs are delimited by "[p], e.g. "[p]Child Pool A[p][p]Child Pool B[p]"

            var parentPoolIdArray = Utils.GetOneDimensionalArray(parentPoolIdsString, poolDelimiter);

            string[] hostResourceDelimiter = { "[h]" };

            // Parent pool host resources are specified by a 2-D array. Each pool is delimited
            // by a "[p]";  Each host resource is delimited by  a "[h]". For example,
            // "[p][h]Child A, Resource 1[h][h]Child A, Resource 2[h][p][p][h]Child B, Resource 1[h][p]"

            var parentHostResourcesArray = Utils.GetTwoDimensionalArray(parentHostResourcesString, poolDelimiter, hostResourceDelimiter);

            using (var configurationService = GetResourcePoolConfigurationService(scope))
            {
                var resourceType = Utils.GetResourceType(resourceDisplayName);
                var resourceSubType = Utils.GetResourceSubType(resourceDisplayName);

                return CreatePoolHelper(scope, configurationService, resourceType, resourceSubType, childPoolId, childPoolName, parentPoolIdArray, parentHostResourcesArray);
            }
        }

        public void ModifyPoolResourcesByPath(ManagementScope scope, ManagementObject configurationService, string resourceType, string resourceSubType, string childPool, string[] parentPoolIdArray, string[][] parentHostResourcesArray)
        {
            if (parentPoolIdArray.Length == 0)            
                throw new ManagementException($"At least one parent pool must be specified when modifying a resource pool's host resources (poolPath {childPool})");
            

            if (parentPoolIdArray.Length != parentHostResourcesArray.Length)            
                throw new ManagementException($"When modifying a child resource pool's host resources, a host resource must be specified for each parent pool. Shared allocations are not supported (poolPath {childPool})");
            
            var parentPools = Utils.GetParentPoolArrayFromPoolIds(scope, resourceType, resourceSubType, parentPoolIdArray);

            var allocationSettings = new ResourceAllocationSettingData().GetNewPoolAllocationSettingsArray(scope, resourceType, resourceSubType, parentPoolIdArray, parentHostResourcesArray);

            using (var inParams = configurationService.GetMethodParameters("ModifyPoolResources"))
            {
                inParams["ChildPool"] = childPool;
                inParams["ParentPools"] = parentPools;
                inParams["AllocationSettings"] = allocationSettings;

                using (var outParams = configurationService.InvokeMethod("ModifyPoolResources", inParams, null))
                {
                    Job.Validator.ValidateOutput(outParams, scope);
                }
            }
        }

        public void ModifyPoolResourcesHelper(ManagementScope scope, ManagementObject configurationService, string resourceType, string resourceSubType, string poolId, string[] parentPoolIdArray, string[][] parentHostResourcesArray)
        {
            var pool = new ResourcePool();
            var poolPath = pool.GetResourcePoolPath(scope, resourceType, resourceSubType, poolId);

            ModifyPoolResourcesByPath(scope, configurationService, resourceType, resourceSubType, poolPath, parentPoolIdArray, parentHostResourcesArray);
        }

        public void ModifyPoolResources(ManagementScope scope, string resourceDisplayName, string poolId, string parentPoolIdsString, string parentHostResourcesString)
        {
            string[] poolDelimiter = { "[p]" };
            string[] hostResourceDelimiter = { "[h]" };

            var parentPoolIdArray = Utils.GetOneDimensionalArray(parentPoolIdsString, poolDelimiter);

            var parentHostResourcesArray = Utils.GetTwoDimensionalArray(parentHostResourcesString, poolDelimiter, hostResourceDelimiter);

            using (var configurationService = GetResourcePoolConfigurationService(scope))
            {
                var resourceType = Utils.GetResourceType(resourceDisplayName);
                var resourceSubType = Utils.GetResourceSubType(resourceDisplayName);

                ModifyPoolResourcesHelper(scope, configurationService, resourceType, resourceSubType, poolId, parentPoolIdArray, parentHostResourcesArray);
            }
        }

        public void ModifyPoolSettingsByPath(ManagementScope scope, ManagementObject configurationService, string childPool, string poolSettings)
        {
            using (var inParams = configurationService.GetMethodParameters("ModifyPoolSettings"))
            {
                inParams["ChildPool"] = childPool;
                inParams["PoolSettings"] = poolSettings;

                using (var outParams = configurationService.InvokeMethod("ModifyPoolSettings", inParams, null))
                {
                    Job.Validator.ValidateOutput(outParams, scope);
                }
            }
        }

        public void ModifyPoolSettingsHelper(ManagementScope scope, ManagementObject configurationService, string resourceType, string resourceSubType, string poolId, string resourcePoolSettingData)
        {
            var pool = new ResourcePool();
            var poolPath = pool.GetResourcePoolPath(scope, resourceType, resourceSubType, poolId);

            ModifyPoolSettingsByPath(scope, configurationService, poolPath, resourcePoolSettingData);
        }

        public void ModifyPoolSettings(ManagementScope scope, string resourceDisplayName, string poolId, string newPoolId, string newPoolName)
        {
            using (var configurationService = GetResourcePoolConfigurationService(scope))
            {
                var resourceType = Utils.GetResourceType(resourceDisplayName);
                var resourceSubType = Utils.GetResourceSubType(resourceDisplayName);

                var resourcePoolSettingData = new ResourcePoolSettingData().GetSettingsForSpecificPool(scope, resourceType, resourceSubType, newPoolId, newPoolName);

                ModifyPoolSettingsHelper(scope, configurationService, resourceType, resourceSubType, poolId, resourcePoolSettingData);
            }
        }

        public void DeletePoolHelper(ManagementScope scope, ManagementObject configurationService, string resourceType, string resourceSubType, string poolId)
        {
            var pool = new ResourcePool().GetResourcePoolPath(scope, resourceType, resourceSubType, poolId);

            using (var inParams = configurationService.GetMethodParameters("DeletePool"))
            {
                inParams["Pool"] = pool;

                using (var outParams = configurationService.InvokeMethod("DeletePool", inParams, null))
                {
                    Job.Validator.ValidateOutput(outParams, scope);
                }
            }
        }

        public void DeletePool(ManagementScope scope, string resourceDisplayName, string poolId)
        {
            using (var configurationService = GetResourcePoolConfigurationService(scope))
            {
                var resourceType = Utils.GetResourceType(resourceDisplayName);
                var resourceSubType = Utils.GetResourceSubType(resourceDisplayName);

                DeletePoolHelper(scope, configurationService, resourceType, resourceSubType, poolId);
            }
        }
    }
}
