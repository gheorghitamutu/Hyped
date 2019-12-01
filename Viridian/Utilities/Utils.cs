using System.Linq;
using System.Management;

namespace Viridian.Utilities
{
    public static class Utils
    {
        public static ManagementScope GetScope(string serverName, string scopePath) => new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);

        public static ManagementObject GetServiceObject(ManagementScope scope, string serviceName)
        {
            using (var serviceClass = new ManagementClass(scope, new ManagementPath(serviceName), null))
                return serviceClass.GetInstances().Cast<ManagementObject>().First();
        }
    }
}
