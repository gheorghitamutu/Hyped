using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using Viridian.Exceptions;

namespace Viridian.Utilities
{
    public static class Utils
    {
        #region Constants

        public static readonly string[] AggregationMetricDefinitionCaptions =
        {
            "Average Memory Utilization",
            "Aggregated Average Memory Utilization",
            "Maximum for Memory Utilization",
            "Aggregated Maximum for Memory Utilization",
            "Average CPU Utilization",
            "Aggregated Average CPU Utilization",
            "Average Disk Latency",
            "Aggregated Average Normalized Disk Throughput",
            "Aggregated Average Disk Latency",
            "Average Normalized Disk Throughput",
            "Maximum for Disk Allocation",
            "Aggregated Maximum for Disk Allocations",
            "Minimum for Memory Utilization",
            "Aggregated Minimum for Memory Utilization"
        };

        public static readonly string[] BaseMetricDefinitionCaptions =
        {
            "Filtered Outgoing Network Traffic",
            "Aggregated Filtered Outgoing Network Traffic",
            "Normalized I/O Operations Completed",
            "Disk Data Written",
            "Aggregated Disk Data Read",
            "Filtered Incoming Network Traffic",
            "Aggregated Filtered Incoming Network Traffic",
            "Disk Data Read",
            "Aggregated Normalized I/O Operations Completed",
            "Aggregated Disk Data Written"
        };

        #endregion

        public static ManagementScope GetScope(string serverName, string scopePath) => new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);

        public static ManagementObject GetFirstObjectFromCollection(ManagementObjectCollection collection)
        {
            if (collection is null)
                throw new ViridianException("", new ArgumentNullException(nameof(collection)));

            if (collection.Count == 0)
                throw new ViridianException("The collection contains no objects!");

            foreach (ManagementObject managementObject in collection)
                return managementObject;

            return null;
        }

        public static ManagementObject GetFirstObjectFromWqlQueryByClassAndName(string name, string className, ManagementScope scope)
        {
            var vmQueryWql = $"SELECT * FROM {className} WHERE ElementName=\"{name}\"";
            var vmQuery = new SelectQuery(vmQueryWql);

            using (var mos = new ManagementObjectSearcher(scope, vmQuery))
                return GetFirstObjectFromCollection(mos.Get());
        }

        public static ManagementObjectCollection GetVmCollection(string serverName, string scopePath)
        {
            var scope = GetScope(serverName, scopePath);
            var vmQueryWql = $"SELECT * FROM Msvm_ComputerSystem";
            var vmQuery = new SelectQuery(vmQueryWql);
            
            using (var mos = new ManagementObjectSearcher(scope, vmQuery))
                return mos.Get();
        }

        public static ManagementObject GetVirtualMachine(string name, ManagementScope scope) => GetFirstObjectFromWqlQueryByClassAndName(name, "Msvm_ComputerSystem", scope);

        public static ManagementObject GetVirtualMachineSettings(string vmName, ManagementScope scope)
        {
            using (var vm = GetVirtualMachine(vmName, scope))
                return GetVirtualMachineSettings(vm);
        }

        public static ManagementObject GetVirtualMachineSettings(ManagementObject virtualMachine)
        {
            if (virtualMachine is null)
                throw new ViridianException("", new ArgumentNullException(nameof(virtualMachine)));

            using (var vssd = virtualMachine.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null))
                return GetFirstObjectFromCollection(vssd);
        }

        public static ManagementObject GetServiceObject(ManagementScope scope, string serviceName)
        {
            var wmiPath = new ManagementPath(serviceName);

            using (var serviceClass = new ManagementClass(scope, wmiPath, null))
                return GetFirstObjectFromCollection(serviceClass.GetInstances());
        }

        public static ManagementObjectCollection GetResourcePools(string resourceType, string resourceSubtype, ManagementScope scope)
        {
            string query = resourceType == "1" ? 
                string.Format(CultureInfo.InvariantCulture, "SELECT * FROM CIM_ResourcePool WHERE ResourceType=\"{0}\" AND OtherResourceType=\"{1}\"", resourceType, resourceSubtype) :
                string.Format(CultureInfo.InvariantCulture, "SELECT * FROM CIM_ResourcePool WHERE ResourceType=\"{0}\" AND ResourceSubType=\"{1}\"", resourceType, resourceSubtype);

            var poolQuery = new SelectQuery(query);

            using (var mos = new ManagementObjectSearcher(scope, poolQuery))
                return mos.Get();
        }

        public static ManagementObject GetResourcePool(string resourceType, string resourceSubtype, string poolId, ManagementScope scope)
        {
            var query = resourceType == "1" ?
                $"SELECT * FROM CIM_ResourcePool WHERE ResourceType=\"{resourceType}\" AND OtherResourceType=\"{resourceSubtype}\" AND PoolId=\"{poolId}\"" :
                $"SELECT * FROM CIM_ResourcePool WHERE ResourceType=\"{resourceType}\" AND ResourceSubType=\"{resourceSubtype}\" AND PoolId=\"{poolId}\"";

            var poolQuery = new SelectQuery(query);

            using (var mos = new ManagementObjectSearcher(scope, poolQuery))
            using (var poolCollection = mos.Get())
            {
                if (poolCollection.Count != 1)
                    throw new ViridianException($"A single CIM_ResourcePool derived instance could not be found for ResourceType \"{resourceType}\", ResourceSubtype \"{resourceSubtype}\" and PoolId \"{poolId}\"");

                return GetFirstObjectFromCollection(poolCollection);
            }
        }

        public static string GetResourcePoolPath(ManagementScope scope, string resourceType, string resourceSubType, string poolId)
        {
            using (var pool = GetResourcePool(resourceType, resourceSubType, poolId, scope))
                return pool.Path.Path;
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
               
        public static List<ManagementObject> GetResourceAllocationSettingDataResourcesByTypeAndSubtype(string vmName, ManagementScope scope, string resourceType, string resourceSubtype)
        {
            var resources = new List<ManagementObject>();

            using (var vm = GetVirtualMachine(vmName, scope))
            using (var vssdCollection = vm.GetRelated("Msvm_VirtualSystemSettingData", null, null, null, null, null, false, null))
            {
                var settings = GetFirstObjectFromCollection(vssdCollection);

                using (var rasdCollection = settings.GetRelated("Msvm_ResourceAllocationSettingData", null, null, null, null, null, false, null))
                    foreach (ManagementObject resource in rasdCollection)
                        if (resource["ResourceType"].ToString() == resourceType && resource["ResourceSubType"].ToString() == resourceSubtype)
                            resources.Add(resource);
            }

            return resources;
        }

        public static ManagementObject GetScsiControllerChildBySubtypeAndIndex(ManagementObject scsiController, string resourceSubType, int index)
        {
            if (scsiController == null)
                throw new ViridianException("Null SCSI Controller class!");

            using (var scsiControllerChildren = scsiController.GetRelated("Msvm_ResourceAllocationSettingData", null, null, null, "Dependent", "Antecedent", false, null))
            {
                if (scsiControllerChildren.Count < index)
                    throw new ViridianException("Invalid SCSI child address/index specified!");

                uint count = 0;

                foreach (ManagementObject drive in scsiControllerChildren)
                {
                    if (count == index && drive["ResourceSubType"].ToString() == resourceSubType)
                        return drive;

                    count++;
                }

                throw new ViridianException("Invalid SCSI child subtype specified!");
            }
        }

        public static ManagementObject GetBaseMetricDefinition(string name, ManagementScope scope)
        {
            var wqlQuery = string.Format(CultureInfo.InvariantCulture, "SELECT * FROM CIM_BaseMetricDefinition WHERE ElementName=\"{0}\"", name);
            var query = new SelectQuery(wqlQuery);

            using (var mos = new ManagementObjectSearcher(scope, query))
            using (var collection = mos.Get())
            {
                if (collection.Count == 0)
                    return null; // definition does not exists

                if (collection.Count != 1)
                    throw new ManagementException(string.Format(CultureInfo.CurrentCulture, "A single CIM_BaseMetricDefinition derived instance could not be found for name \"{0}\"", name));

                return GetFirstObjectFromCollection(collection);
            }
        }

        public static string EscapeObjectPath(string objectPath)
        {
            string escapedObjectPath = objectPath.Replace("\\", "\\\\");
            escapedObjectPath = escapedObjectPath.Replace("\"", "\\\"");

            return escapedObjectPath;
        }

    }
}
