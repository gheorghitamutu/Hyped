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

        public enum ResourceType : ushort
        {
            Other = 1,
            ComputerSystem = 2,
            Processor = 3,
            Memory = 4,
            IdeController = 5,
            ParallelScsiHba = 6,
            FcHba = 7,
            ScsiHba = 8,
            IbHca = 9,
            EthernetAdapter = 10,
            OtherNetworkAdapter = 11,
            IoSlot = 12,
            IoDevice = 13,
            DisketteDrive = 14,
            CdDrive = 15,
            DvdDrive = 16,
            DiskDrive = 17,
            TapeDrive = 18,
            StorageExtent = 19,
            OtherStorageDevice = 20,
            SerialPort = 21,
            ParallelPort = 22,
            UsbController = 23,
            GraphicsController = 24,
            Ieee1394Controller = 25,
            PartitionableUnit = 26,
            BasePartitionableUnit = 27,
            PowerSupply = 28,
            CoolingDevice = 29,
            EthernetSwitchPort = 30,
            LogicalDisk = 31,
            StorageVolume = 32,
            EthernetConnection = 33
        }

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

        public static ManagementObjectCollection GetWmiObjects(ManagementScope scope, string classname, string where)
        {
            var query = where != null ? $"select * from {classname} where {where}" : $"select * from {classname}";

            using (var mos = new ManagementObjectSearcher(scope, new ObjectQuery(query)))
                return mos.Get();
        }
        
        public static ManagementObject GetVirtualMachineManagementService(ManagementScope scope)
        {
            using (var vsms = new ManagementClass("Msvm_VirtualSystemManagementService"))
            {
                vsms.Scope = scope;

                return GetFirstObjectFromCollection(vsms.GetInstances());
            }
        }

        public static ManagementObject GetFirstObjectFromCollection(ManagementObjectCollection collection)
        {
            if (collection.Count == 0)            
                throw new ViridianException("The collection contains no objects!");

            foreach (ManagementObject managementObject in collection)
                return managementObject;

            return null;
        }

        public static ManagementObject GetVMFirstObject(string name, string className, ManagementScope scope)
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

        public static ManagementObject GetVirtualMachine(string name, ManagementScope scope) => GetVMFirstObject(name, "Msvm_ComputerSystem", scope);

        public static ManagementObject GetVirtualMachineSettings(string vmName, ManagementScope scope)
        {
            using (var vm = GetVirtualMachine(vmName, scope))
            using (var vssd = vm.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null))
                return GetFirstObjectFromCollection(vssd);
        }

        public static ManagementObject GetVirtualMachineSnapshotService(ManagementScope scope)
        {
            using (var vsss = new ManagementClass("Msvm_VirtualSystemSnapshotService") { Scope = scope })
                return GetFirstObjectFromCollection(vsss.GetInstances());
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
    }
}
