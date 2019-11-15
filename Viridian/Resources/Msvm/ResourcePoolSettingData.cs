using System;
using System.Management;
using Viridian.Exceptions;

namespace Viridian.Resources.Msvm
{
    class ResourcePoolSettingData
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

        #region Settings

        public ManagementObject GetSettingData(ManagementScope scope, string resourceType, string resourceSubType, string poolId)
        {
            using (var pool = GetResourcePool(resourceType, resourceSubType, poolId, scope))
            {
                using (var rpsdCollection = pool.GetRelated("Msvm_ResourcePoolSettingData", "Msvm_SettingsDefineState", null, null, "SettingData", "ManagedElement", false, null))
                {
                    // There will always only be one RPSD for a given resource pool.
                    if (rpsdCollection.Count != 1)                    
                        throw new ViridianException($"A single Msvm_ResourcePoolSettingData derived instance could not be found for ResourceType \"{resourceType}\", OtherResourceType \"{resourceSubType}\" and PoolId \"{poolId}\"");
                    
                    return rpsdCollection.Count > 0 ? GetFirstObjectFromCollection(rpsdCollection) : null;
                }
            }
        }

        public string GetSettingsForPool(ManagementScope scope, string resourceType, string resourceSubType, string poolId, string poolName)
        {
            using (var rpsdClass = new ManagementClass("Msvm_ResourcePoolSettingData") { Scope = scope })
            {
                using (var rpsdMob = rpsdClass.CreateInstance())
                {
                    if (rpsdMob == null) return null;
                    rpsdMob["ResourceType"] = resourceType;

                    if (resourceType == "1")
                    {
                        rpsdMob["OtherResourceType"] = resourceSubType;
                        rpsdMob["ResourceSubType"] = string.Empty;
                    }
                    else
                    {
                        rpsdMob["OtherResourceType"] = string.Empty;
                        rpsdMob["ResourceSubType"] = resourceSubType;
                    }

                    rpsdMob["PoolId"] = poolId;
                    rpsdMob["ElementName"] = poolName;

                    return rpsdMob.GetText(TextFormat.WmiDtd20);

                }
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

        private ManagementObject GetFirstObjectFromCollection(ManagementObjectCollection collection)
        {
            if (collection == null)
                throw new ViridianException("The collection object is null!");

            if (collection.Count == 0)
                throw new ViridianException("The collection contains no objects!");

            foreach (ManagementObject managementObject in collection)
                return managementObject;

            return null;
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

        #endregion
    }
}
