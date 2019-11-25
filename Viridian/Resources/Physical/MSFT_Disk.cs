using System.Management;
using Viridian.Exceptions;
using Viridian.Utilities;

namespace Viridian.Storage.Virtual.Hard
{
    public static class MSFT_Disk
    {
        public enum PartitionStyle
        {
            Raw = 0,
            Mbr = 1,
            Gpt = 2,
        }

        public class GptType
        {
            private GptType(string value) { Value = value; }

            public string Value { get; set; }

            public static GptType SystemPartitionEFI { get { return new GptType("{c12a7328-f81f-11d2-ba4b-00a0c93ec93b}"); } }
            public static GptType MicrosoftReserved { get { return new GptType("{e3c9e316-0b5c-4db8-817d-f92df00215ae}"); } }
            public static GptType BasicData { get { return new GptType("{ebd0a0a2-b9e5-4433-87c0-68b6b72699c7}"); } }
            public static GptType LDMMetadata { get { return new GptType("{5808c8aa-7e8f-42e0-85d2-e1e90434cfb3}"); } }
            public static GptType LDMData { get { return new GptType("{af9b60a0-1431-4f62-bc68-3311714a69ad}"); } }
            public static GptType MicrosoftRecovery { get { return new GptType("{de94bba4-06d1-4d40-a16a-bfd50179d6ac}"); } }

        }

        public static void Initialize(ManagementObject msftDisk, PartitionStyle partitionStyle, string serverName, string scopePath)
        {
            var scope = Utils.GetScope(serverName, scopePath);

            using (var ip = msftDisk.GetMethodParameters("Initialize"))
            {
                ip["PartitionStyle"] = partitionStyle;

                using (var op = msftDisk.InvokeMethod("Initialize", ip, null))
                    Job.Validator.ValidateOutput(op, scope);
            }
        }

        public static ManagementObject GetMsftDiskFromPath(string serverName, string scopePath, string diskPath)
        {
            var scope = Utils.GetScope(serverName, scopePath);
            var query = new ObjectQuery("SELECT * FROM MSFT_Disk");

            using (var mos = new ManagementObjectSearcher(scope, query))
                foreach (ManagementObject disk in mos.Get())
                    if (disk["Location"] as string == diskPath)
                        return disk;

            throw new ViridianException("Disk not found!");
        }

        public static void CreatePartition(ManagementObject msftDisk, ulong size, string serverName, string scopePath, GptType gptType, char driveLetter = ' ', bool assignDriveLetter = false, bool useMaximumSize = false)
        {
            var scope = Utils.GetScope(serverName, scopePath);

            using (var ip = msftDisk.GetMethodParameters("CreatePartition"))
            {
                ip["UseMaximumSize"] = useMaximumSize;
                if (useMaximumSize == false)
                    ip["Size"] = size;

                ip["AssignDriveLetter"] = assignDriveLetter;
                if (assignDriveLetter)
                    ip["DriveLetter"] = driveLetter;

                ip["GptType"] = gptType.Value;

                using (var op = msftDisk.InvokeMethod("CreatePartition", ip, null))
                    Job.Validator.ValidateOutput(op, scope);
            }
        }

        public static void AssignLetterToPartition(string serverName, string scopePath, ManagementObject msftDisk, int partitionIndex, string accessPath, bool assignNextLetter)
        {
            if (accessPath.Length == 0 && !assignNextLetter || !assignNextLetter && accessPath.Length > 2)
                throw new ViridianException("Invalid letter to partition provided!");

            using (var partitionCollection = msftDisk.GetRelated("MSFT_Partition", null, null, null, null, null, false, null))
            {
                var countPartition = 0;
                foreach (ManagementObject partition in partitionCollection)
                {
                    if (countPartition == partitionIndex)
                        using (var ip = partition.GetMethodParameters("AddAccessPath"))
                        {
                            if (assignNextLetter) 
                                ip["AssignDriveLetter"] = true;
                            else 
                                ip["AccessPath"] = accessPath;

                            using (var op = partition.InvokeMethod("AddAccessPath", ip, null))
                            {
                                Job.Validator.ValidateOutput(op, Utils.GetScope(serverName, scopePath));
                                return;
                            }
                        }

                    countPartition++;
                }
            }

            throw new ViridianException("Partition not found!");
        }

        public static ManagementObject GetMsftDiskVolume(ManagementObject msftDisk, int partitionIndex, int volumeIndex)
        {
            using (var partitionCollection = msftDisk.GetRelated("MSFT_Partition", null, null, null, null, null, false, null))
            {
                var countPartition = 0;
                foreach (ManagementObject partition in partitionCollection)
                {
                    if (countPartition == partitionIndex)
                        using (var volumeCollection = partition.GetRelated("MSFT_Volume", null, null, null, null, null, false, null))
                        {
                            var countVolume = 0;
                            foreach (ManagementObject volume in volumeCollection)
                            {
                                if (countVolume == volumeIndex)
                                    return volume;

                                countVolume++;
                            }
                        }
                    else
                        countPartition++;
                }
            }

            throw new ViridianException("Volume not found!");
        }

        public static void FormatMsftVolume(ManagementObject msftVolume, string serverName, string scopePath)
        {
            var scope = Utils.GetScope(serverName, scopePath);

            using (var ip = msftVolume.GetMethodParameters("Format"))
            {
                ip["FileSystem"] = "NTFS";
                ip["Full"] = true;
                ip["Compress"] = true;

                using (var op = msftVolume.InvokeMethod("Format", ip, null))
                    Job.Validator.ValidateOutput(op, scope);
            }
        }
    }
}
