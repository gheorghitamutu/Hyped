using System;
using System.Management;
using Viridian.Exceptions;
using Viridian.Utilities;

namespace Viridian.Resources.Msvm
{
    class ResourcePoolSettingData
    {
        public ManagementObject GetResourcePoolSettingData(ManagementScope scope, string resourceType, string resourceSubType, string poolId)
        {
            using (var pool = Utils.GetResourcePool(resourceType, resourceSubType, poolId, scope))
            {
                using (var rpsdCollection = pool.GetRelated("Msvm_ResourcePoolSettingData", "Msvm_SettingsDefineState", null, null, "SettingData", "ManagedElement", false, null))
                {
                    if (rpsdCollection.Count != 1)
                        throw new ViridianException($"A single Msvm_ResourcePoolSettingData derived instance could not be found for ResourceType \"{resourceType}\", OtherResourceType \"{resourceSubType}\" and PoolId \"{poolId}\"");
                    
                    return Utils.GetFirstObjectFromCollection(rpsdCollection);
                }
            }
        }

        public string GetSettingsForSpecificPool(ManagementScope scope, string resourceType, string resourceSubType, string poolId, string poolName)
        {
            using (var rpsdClass = new ManagementClass("Msvm_ResourcePoolSettingData") { Scope = scope })
            {
                using (var rpsdInstance = rpsdClass.CreateInstance())
                {
                    if (rpsdInstance == null) 
                        return null;

                    rpsdInstance["ResourceType"] = resourceType;

                    if (resourceType == "1")
                    {
                        rpsdInstance["OtherResourceType"] = resourceSubType;
                        rpsdInstance["ResourceSubType"] = string.Empty;
                    }
                    else
                    {
                        rpsdInstance["OtherResourceType"] = string.Empty;
                        rpsdInstance["ResourceSubType"] = resourceSubType;
                    }

                    rpsdInstance["PoolId"] = poolId;
                    rpsdInstance["ElementName"] = poolName;

                    return rpsdInstance.GetText(TextFormat.WmiDtd20);
                }
            }
        }
    }
}
