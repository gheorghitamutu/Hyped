using System.Management;
using Viridian.Exceptions;

namespace Viridian.Resources.Msvm
{
    class ResourcePool
    {
        #region Path

        public string GetPath(ManagementScope scope, string resourceType, string resourceSubType, string poolId)
        {
            using (var pool = GetResourcePool(resourceType, resourceSubType, poolId, scope))
            {
                return pool.Path.Path;
            }
        }

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
