using System.Management;
using Viridian.Exceptions;
using Viridian.Utilities;

namespace Viridian.Resources.Msvm
{
    class ResourcePool
    {
        public string GetResourcePoolPath(ManagementScope scope, string resourceType, string resourceSubType, string poolId)
        {
            using (var pool = Utils.GetResourcePool(resourceType, resourceSubType, poolId, scope))
            {
                return pool.Path.Path;
            }
        }
    }
}
