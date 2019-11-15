using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Viridian.Exceptions;

namespace Viridian.Resources.Msvm
{
    class ResourcePoolConfiguration
    {
        #region Constants

        private static readonly string[][] ResourceTypeInformation =
        {
            //      Display Name           Resource Type  Resource Subtype
            new[] { @"RDV",                @"1",          @"Microsoft:Hyper-V:Rdv Component" },
            new[] { @"Processor",          @"3",          @"Microsoft:Hyper-V:Processor" },
            new[] { @"Memory",             @"4",          @"Microsoft:Hyper-V:Memory" },
            new[] { @"ScsiHBA",            @"6",          @"Microsoft:Hyper-V:Synthetic SCSI Controller" },
            new[] { @"FCPort",             @"7",          @"Microsoft:Hyper-V:Synthetic FiberChannel Port" },
            new[] { @"EmulatedEthernet",   @"10",         @"Microsoft:Hyper-V:Emulated Ethernet Port" },
            new[] { @"SyntheticEthernet",  @"10",         @"Microsoft:Hyper-V:Synthetic Ethernet Port" },
            new[] { @"Mouse",              @"13",         @"Microsoft:Hyper-V:Synthetic Mouse" },
            new[] { @"SyntheticDVD",       @"16",         @"Microsoft:Hyper-V:Synthetic DVD Drive" },
            new[] { @"PhysicalDisk",       @"17",         @"Microsoft:Hyper-V:Physical Disk Drive" },
            new[] { @"SyntheticDisk",      @"17",         @"Microsoft:Hyper-V:Synthetic Disk Drive" },
            new[] { @"CD/DVD",             @"31",         @"Microsoft:Hyper-V:Virtual CD/DVD Disk" },
            new[] { @"3DGraphics",         @"24",         @"Microsoft:Hyper-V:Synthetic 3D Display Controller" },
            new[] { @"Graphics",           @"24",         @"Microsoft:Hyper-V:Synthetic Display Controller" },
            new[] { @"VHD",                @"31",         @"Microsoft:Hyper-V:Virtual Hard Disk" },
            new[] { @"Floppy",             @"31",         @"Microsoft:Hyper-V:Virtual Floppy Disk" },
            new[] { @"EthernetConnection", @"33",         @"Microsoft:Hyper-V:Ethernet Connection" },
            new[] { @"FCConnection",       @"64764",      @"Microsoft:Hyper-V:FiberChannel Connection" }
        };

        #endregion

        public ManagementObject GetResourcePoolConfigurationService(ManagementScope scope)
        {
            using (var rpcsc = new ManagementClass("Msvm_ResourcePoolConfigurationService") { Scope = scope })
            {
                return GetFirstObjectFromCollection(rpcsc.GetInstances());
            }
        }

        public ManagementObject CreatePoolHelper(ManagementScope scope, ManagementObject configurationService, string resourceType, string resourceSubType, string childPoolId, string childPoolName, string[] parentPoolIdArray, string[][] parentHostResourcesArray)
        {
            if (parentPoolIdArray.Length == 0)            
                throw new ViridianException($"At least one parent pool must be specified when creating a child resource pool (PoolId {childPoolId})");
            
            if (parentPoolIdArray.Length != parentHostResourcesArray.Length)            
                throw new ViridianException($"When creating a child resource pool, a host resource must be specified for each parent pool. Shared allocations are not supported (PoolId {childPoolId})");
            
            var poolSettings = new ResourcePoolSettingData().GetSettingsForPool(scope, resourceType, resourceSubType, childPoolId, childPoolName);

            var parentPools = GetParentPoolArrayFromPoolIds(scope, resourceType, resourceSubType, parentPoolIdArray);

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

            var parentPoolIdArray = GetOneDimensionalArray(parentPoolIdsString, poolDelimiter);

            string[] hostResourceDelimiter = { "[h]" };

            // Parent pool host resources are specified by a 2-D array. Each pool is delimited
            // by a "[p]";  Each host resource is delimited by  a "[h]". For example,
            // "[p][h]Child A, Resource 1[h][h]Child A, Resource 2[h][p][p][h]Child B, Resource 1[h][p]"

            var parentHostResourcesArray = GetTwoDimensionalArray(parentHostResourcesString, poolDelimiter, hostResourceDelimiter);

            using (var configurationService = GetResourcePoolConfigurationService(scope))
            {
                var resourceType = GetResourceType(resourceDisplayName);
                var resourceSubType = GetResourceSubType(resourceDisplayName);

                return CreatePoolHelper(scope, configurationService, resourceType, resourceSubType, childPoolId, childPoolName, parentPoolIdArray, parentHostResourcesArray);
            }
        }

        public void ModifyPoolResourcesByPath(ManagementScope scope, ManagementObject configurationService, string resourceType, string resourceSubType, string childPool, string[] parentPoolIdArray, string[][] parentHostResourcesArray)
        {
            if (parentPoolIdArray.Length == 0)            
                throw new ManagementException($"At least one parent pool must be specified when modifying a resource pool's host resources (poolPath {childPool})");
            

            if (parentPoolIdArray.Length != parentHostResourcesArray.Length)            
                throw new ManagementException($"When modifying a child resource pool's host resources, a host resource must be specified for each parent pool. Shared allocations are not supported (poolPath {childPool})");
            
            var parentPools = GetParentPoolArrayFromPoolIds(scope, resourceType, resourceSubType, parentPoolIdArray);

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
            var poolPath = pool.GetPath(scope, resourceType, resourceSubType, poolId);

            ModifyPoolResourcesByPath(scope, configurationService, resourceType, resourceSubType, poolPath, parentPoolIdArray, parentHostResourcesArray);
        }

        public void ModifyPoolResources(ManagementScope scope, string resourceDisplayName, string poolId, string parentPoolIdsString, string parentHostResourcesString)
        {
            string[] poolDelimiter = { "[p]" };
            string[] hostResourceDelimiter = { "[h]" };

            var parentPoolIdArray = GetOneDimensionalArray(parentPoolIdsString, poolDelimiter);

            var parentHostResourcesArray = GetTwoDimensionalArray(parentHostResourcesString, poolDelimiter, hostResourceDelimiter);

            using (var configurationService = GetResourcePoolConfigurationService(scope))
            {
                var resourceType = GetResourceType(resourceDisplayName);
                var resourceSubType = GetResourceSubType(resourceDisplayName);

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
            var poolPath = pool.GetPath(scope, resourceType, resourceSubType, poolId);

            ModifyPoolSettingsByPath(scope, configurationService, poolPath, resourcePoolSettingData);
        }

        public void ModifyPoolSettings(ManagementScope scope, string resourceDisplayName, string poolId, string newPoolId, string newPoolName)
        {
            using (var configurationService = GetResourcePoolConfigurationService(scope))
            {
                var resourceType = GetResourceType(resourceDisplayName);
                var resourceSubType = GetResourceSubType(resourceDisplayName);

                var resourcePoolSettingData = new ResourcePoolSettingData().GetSettingsForPool(scope, resourceType, resourceSubType, newPoolId, newPoolName);

                ModifyPoolSettingsHelper(scope, configurationService, resourceType, resourceSubType, poolId, resourcePoolSettingData);
            }
        }

        public void DeletePoolHelper(ManagementScope scope, ManagementObject configurationService, string resourceType, string resourceSubType, string poolId)
        {
            var pool = new ResourcePool().GetPath(scope, resourceType, resourceSubType, poolId);

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
                var resourceType = GetResourceType(resourceDisplayName);
                var resourceSubType = GetResourceSubType(resourceDisplayName);

                DeletePoolHelper(scope, configurationService, resourceType, resourceSubType, poolId);
            }
        }

        #region Utilities

        private ManagementObject GetResourcePool(string resourceType, string resourceSubtype, string poolId, ManagementScope scope)
        {
            var poolQueryWql = resourceType == "1" ?
                $"SELECT * FROM CIM_ResourcePool WHERE ResourceType=\"{resourceType}\" AND OtherResourceType=\"{resourceSubtype}\" AND PoolId=\"{poolId}\"" :
                $"SELECT * FROM CIM_ResourcePool WHERE ResourceType=\"{resourceType}\" AND ResourceSubType=\"{resourceSubtype}\" AND PoolId=\"{poolId}\"";

            var poolQuery = new SelectQuery(poolQueryWql);

            using (var poolSearcher = new ManagementObjectSearcher(scope, poolQuery))
            {
                using (var poolCollection = poolSearcher.Get())
                {
                    // There will always only be one resource pool for a given type, subtype and pool id.
                    if (poolCollection.Count != 1)
                        throw new ViridianException($"A single CIM_ResourcePool derived instance could not be found for ResourceType \"{resourceType}\", ResourceSubtype \"{resourceSubtype}\" and PoolId \"{poolId}\"");

                    return GetFirstObjectFromCollection(poolCollection);
                }
            }
        }

        public ManagementObject GetFirstObjectFromCollection(ManagementObjectCollection collection)
        {
            if (collection == null)
                throw new ViridianException("The collection object is null!");

            if (collection.Count == 0)
                throw new ViridianException("The collection contains no objects!");

            foreach (var managementObject in collection)
                return (ManagementObject)managementObject;

            return null;
        }

        public string[] GetParentPoolArrayFromPoolIds(ManagementScope scope, string resourceType, string resourceSubType, IEnumerable<string> poolIdArray)
        {
            return poolIdArray.Select(poolId => new ResourcePool().GetPath(scope, resourceType, resourceSubType, poolId)).ToArray();
        }

        private string GetResourceType(string displayName)
        {
            foreach (var resource in ResourceTypeInformation)
                if (string.Equals(displayName, resource[0], StringComparison.CurrentCultureIgnoreCase))
                    return resource[1];

            throw new ViridianException($"Invalid resource {displayName} specified.");
        }

        private string GetResourceSubType(string displayName)
        {
            foreach (var resource in ResourceTypeInformation)
                if (string.Equals(displayName, resource[0], StringComparison.CurrentCultureIgnoreCase))
                    return resource[2];

            throw new ViridianException($"Invalid resource {displayName} specified.");
        }
        
        private string[] GetOneDimensionalArray(string delimitedString, string[] delimiter)
        {
            var poolIdArray = delimitedString.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

            return poolIdArray.Length != 0 ? poolIdArray : new[] { string.Empty };
        }

        private string[][] GetTwoDimensionalArray(string delimitedString, string[] dimension1Delimiter, string[] dimension2Delimiter)
        {
            var hostResourceArray = delimitedString.Split(dimension1Delimiter, StringSplitOptions.RemoveEmptyEntries);

            return hostResourceArray.Select(resource => resource.Split(dimension2Delimiter, StringSplitOptions.RemoveEmptyEntries)).ToArray();
        }

        #endregion
    }
}
