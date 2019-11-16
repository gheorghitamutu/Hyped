using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using Viridian.Exceptions;
using Viridian.Resources.Msvm;

namespace Viridian.Utilities
{
    public static class Utils
    {
        #region Constants

        private static readonly string[][] ResourceTypeInformation =
        {
            //      Display Name          Resource Type Resource Subtype
            new[] { "RDV",                "1",          "Microsoft:Hyper-V:Rdv Component" },
            new[] { "Processor",          "3",          "Microsoft:Hyper-V:Processor" },
            new[] { "Memory",             "4",          "Microsoft:Hyper-V:Memory" },
            new[] { "ScsiHBA",            "6",          "Microsoft:Hyper-V:Synthetic SCSI Controller" },
            new[] { "FCPort",             "7",          "Microsoft:Hyper-V:Synthetic FiberChannel Port" },
            new[] { "EmulatedEthernet",   "10",         "Microsoft:Hyper-V:Emulated Ethernet Port" },
            new[] { "SyntheticEthernet",  "10",         "Microsoft:Hyper-V:Synthetic Ethernet Port" },
            new[] { "Mouse",              "13",         "Microsoft:Hyper-V:Synthetic Mouse" },
            new[] { "SyntheticDVD",       "16",         "Microsoft:Hyper-V:Synthetic DVD Drive" },
            new[] { "PhysicalDisk",       "17",         "Microsoft:Hyper-V:Physical Disk Drive" },
            new[] { "SyntheticDisk",      "17",         "Microsoft:Hyper-V:Synthetic Disk Drive" },
            new[] { "CD/DVD",             "31",         "Microsoft:Hyper-V:Virtual CD/DVD Disk" },
            new[] { "3DGraphics",         "24",         "Microsoft:Hyper-V:Synthetic 3D Display Controller" },
            new[] { "Graphics",           "24",         "Microsoft:Hyper-V:Synthetic Display Controller" },
            new[] { "VHD",                "31",         "Microsoft:Hyper-V:Virtual Hard Disk" },
            new[] { "Floppy",             "31",         "Microsoft:Hyper-V:Virtual Floppy Disk" },
            new[] { "EthernetConnection", "33",         "Microsoft:Hyper-V:Ethernet Connection" },
            new[] { "FCConnection",       "64764",      "Microsoft:Hyper-V:FiberChannel Connection" }
        };

        #endregion

        public static ManagementScope GetScope(string serverName, string scopePath) => new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);

        public static ManagementObject GetWmiObject(ManagementScope scope, string classname, string where)
        {
            using (var collection = GetWmiObjects(scope, classname, where))
            {
                if (collection.Count != 1)
                    throw new ViridianException($"Cannot locate {classname} where {@where}!");

                using (var collectionEnumerator = collection.GetEnumerator())
                {
                    collectionEnumerator.MoveNext();

                    if (!(collectionEnumerator.Current is ManagementObject result))
                        throw new ViridianException($"Failure retrieving {classname} where {@where}!");

                    return result;
                }
            }
        }

        private static ManagementObjectCollection GetWmiObjects(ManagementScope scope, string classname, string where)
        {
            var query = where != null ? $"select * from {classname} where {where}" : $"select * from {classname}";

            return (new ManagementObjectSearcher(scope, new ObjectQuery(query))).Get();
        }
        
        public static ManagementObject GetVirtualMachineManagementService(ManagementScope scope)
        {
            using (ManagementClass managementServiceClass = new ManagementClass("Msvm_VirtualSystemManagementService"))
            {
                managementServiceClass.Scope = scope;

                ManagementObject managementService = GetFirstObjectFromCollection(managementServiceClass.GetInstances());

                return managementService;
            }
        }

        public static ManagementObject GetFirstObjectFromCollection(ManagementObjectCollection collection)
        {
            if (collection.Count == 0)            
                throw new ViridianException("The collection contains no objects!");

            foreach (ManagementObject managementObject in collection)
            {
                return managementObject;
            }

            return null;
        }

        public static ManagementObject GetVMFirstObject(string name, string className, ManagementScope scope)
        {
            var vmQueryWql = $"SELECT * FROM {className} WHERE ElementName=\"{name}\"";

            var vmQuery = new SelectQuery(vmQueryWql);

            using (var vmSearcher = new ManagementObjectSearcher(scope, vmQuery))
            {
                return GetFirstObjectFromCollection(vmSearcher.Get());
            }
        }

        public static ManagementObject GetVirtualMachine(string name, ManagementScope scope) => GetVMFirstObject(name, "Msvm_ComputerSystem", scope);

        public static ManagementObject GetVirtualMachineSettings(string vmName, ManagementScope scope)
        {
            using (var virtualMachine = GetVirtualMachine(vmName, scope))
            {
                using (var settingsCollection = virtualMachine.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null))
                {
                    return GetFirstObjectFromCollection(settingsCollection);
                }
            }
        }

        public static ManagementObject GetVirtualMachineSnapshotService(ManagementScope scope)
        {
            using (var virtualSystemSnapshotServiceCollection = new ManagementClass("Msvm_VirtualSystemSnapshotService") { Scope = scope })
            {
                return GetFirstObjectFromCollection(virtualSystemSnapshotServiceCollection.GetInstances());
            }
        }

        public static ManagementObject GetServiceObject(ManagementScope scope, string serviceName)
        {
            var wmiPath = new ManagementPath(serviceName);
            using (var serviceClass = new ManagementClass(scope, wmiPath, null))
            {
                return GetFirstObjectFromCollection(serviceClass.GetInstances());
            }
        }

        public static ManagementObjectCollection GetResourcePools(string resourceType, string resourceSubtype, ManagementScope scope)
        {
            string poolQueryWql = resourceType == "1" ? 
                string.Format(CultureInfo.InvariantCulture, "SELECT * FROM CIM_ResourcePool WHERE ResourceType=\"{0}\" AND OtherResourceType=\"{1}\"", resourceType, resourceSubtype) :
                string.Format(CultureInfo.InvariantCulture, "SELECT * FROM CIM_ResourcePool WHERE ResourceType=\"{0}\" AND ResourceSubType=\"{1}\"", resourceType, resourceSubtype);

            SelectQuery poolQuery = new SelectQuery(poolQueryWql);

            using (ManagementObjectSearcher poolSearcher = new ManagementObjectSearcher(scope, poolQuery))
            {
                return poolSearcher.Get();
            }
        }

        public static ManagementObject GetResourcePool(string resourceType, string resourceSubtype, string poolId, ManagementScope scope)
        {
            var poolQueryWql = resourceType == "1" ?
                $"SELECT * FROM CIM_ResourcePool WHERE ResourceType=\"{resourceType}\" AND OtherResourceType=\"{resourceSubtype}\" AND PoolId=\"{poolId}\"" :
                $"SELECT * FROM CIM_ResourcePool WHERE ResourceType=\"{resourceType}\" AND ResourceSubType=\"{resourceSubtype}\" AND PoolId=\"{poolId}\"";

            var poolQuery = new SelectQuery(poolQueryWql);

            using (var poolSearcher = new ManagementObjectSearcher(scope, poolQuery))
            {
                using (var poolCollection = poolSearcher.Get())
                {
                    if (poolCollection.Count != 1)
                        throw new ViridianException($"A single CIM_ResourcePool derived instance could not be found for ResourceType \"{resourceType}\", ResourceSubtype \"{resourceSubtype}\" and PoolId \"{poolId}\"");

                    return GetFirstObjectFromCollection(poolCollection);
                }
            }
        }

        public static string GetResourcePoolPath(ManagementScope scope, string resourceType, string resourceSubType, string poolId)
        {
            using (var pool = Utils.GetResourcePool(resourceType, resourceSubType, poolId, scope))
            {
                return pool.Path.Path;
            }
        }

        public static string GetResourceType(string displayName)
        {
            foreach (var resource in ResourceTypeInformation)
                if (string.Equals(displayName, resource[0], StringComparison.CurrentCultureIgnoreCase))
                    return resource[1];

            throw new ViridianException($"Invalid resource {displayName} specified.");
        }

        public static string GetResourceSubType(string displayName)
        {
            foreach (var resource in ResourceTypeInformation)
                if (string.Equals(displayName, resource[0], StringComparison.CurrentCultureIgnoreCase))
                    return resource[2];

            throw new ViridianException($"Invalid resource {displayName} specified.");
        }

        public static string[] GetParentPoolArrayFromPoolIds(ManagementScope scope, string resourceType, string resourceSubType, IEnumerable<string> poolIdArray)
        {
            return poolIdArray.Select(poolId => GetResourcePoolPath(scope, resourceType, resourceSubType, poolId)).ToArray();
        }

        public static string[] GetOneDimensionalArray(string delimitedString, string[] delimiter)
        {
            var poolIdArray = delimitedString.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

            return poolIdArray.Length != 0 ? poolIdArray : new[] { string.Empty };
        }

        public static string[][] GetTwoDimensionalArray(string delimitedString, string[] dimension1Delimiter, string[] dimension2Delimiter)
        {
            var hostResourceArray = delimitedString.Split(dimension1Delimiter, StringSplitOptions.RemoveEmptyEntries);

            return hostResourceArray.Select(resource => resource.Split(dimension2Delimiter, StringSplitOptions.RemoveEmptyEntries)).ToArray();
        }
    }
}
