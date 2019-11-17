using System.Collections;
using System.Collections.Generic;
using System.Management;
using Viridian.Utilities;

namespace Viridian.Resources.Msvm
{
    public static class ResourceAllocationSettingData
    {
        public static ManagementObject GetAllocationSettings(ManagementScope scope, string resourceType, string resourceSubType, string poolId)
        {
            using (var rp = Utils.GetResourcePool(resourceType, resourceSubType, poolId, scope))
            {
                if ((bool)rp.GetPropertyValue("Primordial"))
                    return null;

                using (var rasdCollection = rp.GetRelated("CIM_ResourceAllocationSettingData", "Msvm_SettingsDefineState", null, null, "SettingData", "ManagedElement", false, null))
                    if (rasdCollection.Count > 0) 
                        return Utils.GetFirstObjectFromCollection(rasdCollection);
            }

            return null;
        }

        public static string GetNewPoolAllocationSettings(ManagementScope scope, string resourceType, string resourceSubType, string poolId, IEnumerable hostResources)
        {
            using (var rasdClass = new ManagementClass("Msvm_ResourceAllocationSettingData") { Scope = scope })
            {
                using (var rasd = rasdClass.CreateInstance())
                {
                    if (rasd == null)
                        return "";

                    rasd["ResourceType"] = resourceType;

                    if (resourceType == "1")
                    {
                        rasd["OtherResourceType"] = resourceSubType;
                        rasd["ResourceSubType"] = string.Empty;
                    }
                    else
                    {
                        rasd["OtherResourceType"] = string.Empty;
                        rasd["ResourceSubType"] = resourceSubType;
                    }

                    rasd["PoolId"] = poolId;
                    rasd["HostResource"] = hostResources;

                    return rasd.GetText(TextFormat.WmiDtd20);
                }
            }
        }

        public static string[] GetNewPoolAllocationSettingsArray(ManagementScope scope, string resourceType, string resourceSubType, string[] poolIdArray, string[][] hostResourcesArray)
        {
            var rasdList = new List<string>();

            for (uint i = 0; i < poolIdArray.Length; i++)
                rasdList.Add(GetNewPoolAllocationSettings(scope, resourceType, resourceSubType, poolIdArray[i], hostResourcesArray[i]));
            
            return rasdList.ToArray();
        }

        public static ManagementObject GetPrototypeAllocationSettings(ManagementObject pool, string valueRole, string valueRange)
        {
            using (var capabilitiesCollection = pool.GetRelated("Msvm_AllocationCapabilities", "Msvm_ElementCapabilities", null, null, null, null, false, null))
                foreach (ManagementObject capability in capabilitiesCollection)
                    using (var relationshipsCollection = capability.GetRelationships("Cim_SettingsDefineCapabilities"))
                        foreach (ManagementObject relationship in relationshipsCollection)
                        {
                            if (relationship["ValueRole"].ToString() != valueRole || relationship["ValueRange"].ToString() != valueRange)
                                continue;

                            return new ManagementObject(pool.Scope, new ManagementPath(relationship["PartComponent"].ToString()), null);
                        }

            return null;
        }

        public static ManagementObject GetDefaultAllocationSettings(ManagementObject pool) => GetPrototypeAllocationSettings(pool, "0", "0");

        public static ManagementObject GetMinimumAllocationSettings(ManagementObject pool) => GetPrototypeAllocationSettings(pool, "3", "1");

        public static ManagementObject GetMaximumAllocationSettings(ManagementObject pool) => GetPrototypeAllocationSettings(pool, "3", "2");

        public static ManagementObject GetIncrementalAllocationSettings(ManagementObject pool) => GetPrototypeAllocationSettings(pool, "3", "3");
    }
}
