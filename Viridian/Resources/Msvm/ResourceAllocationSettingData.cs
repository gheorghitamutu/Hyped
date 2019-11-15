using System.Collections;
using System.Collections.Generic;
using System.Management;
using Viridian.Exceptions;

namespace Viridian.Resources.Msvm
{
    public class ResourceAllocationSettingData
    {
        #region AllocationInfo

        public ManagementObject GetAllocationSettings(ManagementScope scope, string resourceType, string resourceSubType, string poolId)
        {
            using (var pool = GetResourcePool(resourceType, resourceSubType, poolId, scope))
            {
                if ((bool)pool.GetPropertyValue("Primordial"))
                    return null;

                using (var rasdCollection = pool.GetRelated("CIM_ResourceAllocationSettingData", "Msvm_SettingsDefineState", null, null, "SettingData", "ManagedElement", false, null))
                {
                    if (rasdCollection.Count > 0) 
                        return GetFirstObjectFromCollection(rasdCollection);
                }
            }

            return null;
        }

        public string GetNewPoolAllocationSettings(ManagementScope scope, string resourceType, string resourceSubType, string poolId, IEnumerable hostResources)
        {
            using (var rasdClass = new ManagementClass("Msvm_ResourceAllocationSettingData") { Scope = scope })
            {
                using (var rasdMob = rasdClass.CreateInstance())
                {
                    if (rasdMob == null)
                        return "";

                    rasdMob["ResourceType"] = resourceType;

                    if (resourceType == "1")
                    {
                        rasdMob["OtherResourceType"] = resourceSubType;
                        rasdMob["ResourceSubType"] = string.Empty;
                    }
                    else
                    {
                        rasdMob["OtherResourceType"] = string.Empty;
                        rasdMob["ResourceSubType"] = resourceSubType;
                    }

                    rasdMob["PoolId"] = poolId;
                    rasdMob["HostResource"] = hostResources;

                    return rasdMob.GetText(TextFormat.WmiDtd20);
                }
            }
        }

        public string[] GetNewPoolAllocationSettingsArray(ManagementScope scope, string resourceType, string resourceSubType, string[] poolIdArray, string[][] hostResourcesArray)
        {
            var rasdList = new List<string>();

            for (uint index = 0; index < poolIdArray.Length; index++)
                rasdList.Add(GetNewPoolAllocationSettings(scope, resourceType, resourceSubType, poolIdArray[index], hostResourcesArray[index]));
            
            return rasdList.ToArray();
        }

        public ManagementObject GetPrototypeAllocationSettings(ManagementObject pool, string valueRole, string valueRange)
        {
            using (var capabilitiesCollection = pool.GetRelated("Msvm_AllocationCapabilities", "Msvm_ElementCapabilities", null, null, null, null, false, null))
            {
                foreach (ManagementObject capability in capabilitiesCollection)
                {
                    if (capability == null)
                        continue;

                    using (var relationshipsCollection = capability.GetRelationships("Cim_SettingsDefineCapabilities"))
                    {
                        foreach (ManagementObject relationship in relationshipsCollection)
                        {
                            if (relationship["ValueRole"].ToString() != valueRole || relationship["ValueRange"].ToString() != valueRange)
                                continue;

                            return new ManagementObject(pool.Scope, new ManagementPath(relationship["PartComponent"].ToString()), null);
                        }
                    }
                }

                return null;
            }
        }

        public ManagementObject GetDefaultAllocationSettings(ManagementObject pool) => GetPrototypeAllocationSettings(pool, "0", "0");

        public ManagementObject GetMinimumAllocationSettings(ManagementObject pool) => GetPrototypeAllocationSettings(pool, "3", "1");

        public ManagementObject GetMaximumAllocationSettings(ManagementObject pool) => GetPrototypeAllocationSettings(pool, "3", "2");

        public ManagementObject GetIncrementalAllocationSettings(ManagementObject pool) => GetPrototypeAllocationSettings(pool, "3", "3");

        #endregion

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

        public static ManagementObject GetFirstObjectFromCollection(ManagementObjectCollection collection)
        {
            if (collection == null) 
                throw new ViridianException("The collection object is null!");

            if (collection.Count == 0)
                throw new ViridianException("The collection contains no objects!");

            foreach (var managementObject in collection)
                return (ManagementObject)managementObject;

            return null;
        }

        #endregion
    }
}
