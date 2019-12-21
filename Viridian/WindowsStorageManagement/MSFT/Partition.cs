using System;
using System.Linq;
using System.Management;
using Viridian.Scopes;

namespace Viridian.WindowsStorageManagement.MSFT
{
    public sealed class Partition
    {
        private ManagementObject MSFT_Partition = null;

        public class PartitionGPTType
        {
            private PartitionGPTType(string value) { Value = value; }

            public string Value { get; set; }

            public static PartitionGPTType SystemPartitionEFI => new PartitionGPTType("{c12a7328-f81f-11d2-ba4b-00a0c93ec93b}");
            public static PartitionGPTType MicrosoftReserved => new PartitionGPTType("{e3c9e316-0b5c-4db8-817d-f92df00215ae}");
            public static PartitionGPTType BasicData => new PartitionGPTType("{ebd0a0a2-b9e5-4433-87c0-68b6b72699c7}");
            public static PartitionGPTType LDMMetadata => new PartitionGPTType("{5808c8aa-7e8f-42e0-85d2-e1e90434cfb3}");
            public static PartitionGPTType LDMData => new PartitionGPTType("{af9b60a0-1431-4f62-bc68-3311714a69ad}");
            public static PartitionGPTType MicrosoftRecovery => new PartitionGPTType("{de94bba4-06d1-4d40-a16a-bfd50179d6ac}");
        }

        public enum PartitionMBRType : ushort
        {
            None = 0, // custom added
            FAT12 = 1,
            FAT16 = 4,
            Extended = 5,
            Huge = 6,
            IFS = 7,
            FAT32 = 12
        }
        public enum PartitionOperationalStatus : ushort
        {
            Unknown = 0,
            Online = 1,
            NoMedia = 3,
            Failed = 4,
            Offline = 5
        }
        public enum PartitionTransitionState : ushort
        {
            Reserved = 0,
            Stable = 1,
            Extended = 2,
            Shrunk = 3,
            AutomaticallyReconfigured = 4,
            Restriped = 5
        }

        public Partition(ManagementObject MsftPartition)
        {
            MSFT_Partition = MsftPartition;
        }

        public ManagementObject MSFTPartition => MSFT_Partition;

        #region MsftProperties

        uint DiskNumber => (uint)MSFT_Partition[nameof(DiskNumber)];
        uint PartitionNumber => (uint)MSFT_Partition[nameof(PartitionNumber)];
        char DriveLetter => (char)MSFT_Partition[nameof(DriveLetter)];
        string[] AccessPaths => MSFT_Partition[nameof(AccessPaths)] as string[];
        PartitionOperationalStatus OperationalStatus => (PartitionOperationalStatus)(ushort)MSFT_Partition[nameof(OperationalStatus)];
        PartitionTransitionState TransitionState => (PartitionTransitionState)(ushort)MSFT_Partition[nameof(TransitionState)];
        ulong Size => (ulong)MSFT_Partition[nameof(Size)];
        PartitionMBRType MbrType => (PartitionMBRType)(ushort)MSFT_Partition[nameof(MbrType)];
        string GptType => MSFT_Partition[nameof(GptType)].ToString();
        string Guid => MSFT_Partition[nameof(Guid)].ToString();
        bool IsReadOnly => (bool)MSFT_Partition[nameof(IsReadOnly)];
        bool IsOffline => (bool)MSFT_Partition[nameof(IsOffline)];
        bool IsSystem => (bool)MSFT_Partition[nameof(IsSystem)];
        bool IsBoot => (bool)MSFT_Partition[nameof(IsBoot)];
        bool IsActive => (bool)MSFT_Partition[nameof(IsActive)];
        bool IsHidden => (bool)MSFT_Partition[nameof(IsHidden)];
        bool IsShadowCopy => (bool)MSFT_Partition[nameof(IsShadowCopy)];
        bool NoDefaultDriveLetter => (bool)MSFT_Partition[nameof(NoDefaultDriveLetter)];

        #endregion

        public string AddAccessPath(string AccessPath, bool AssignDriveLetter)
        {
            using (var ip = MSFT_Partition.GetMethodParameters(nameof(AddAccessPath)))
            {
                ip[nameof(AccessPath)] = AccessPath ?? throw new ArgumentNullException(nameof(AccessPath));
                ip[nameof(AssignDriveLetter)] = AssignDriveLetter;

                using (var op = MSFT_Partition.InvokeMethod(nameof(AddAccessPath), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.ScopeObject);

                    return op["ExtendedStatus"] as string;
                }
            }
        }

        public string DeleteObject()
        {
            using (var ip = MSFT_Partition.GetMethodParameters(nameof(DeleteObject)))
            using (var op = MSFT_Partition.InvokeMethod(nameof(DeleteObject), ip, null))
            {
                Job.Validator.ValidateOutput(op, Scope.Storage.ScopeObject);

                return op["ExtendedStatus"] as string;
            }
        }

        public string[] GetAccessPaths()
        {
            using (var ip = MSFT_Partition.GetMethodParameters(nameof(GetAccessPaths)))
            using (var op = MSFT_Partition.InvokeMethod(nameof(GetAccessPaths), ip, null))
            {
                Job.Validator.ValidateOutput(op, Scope.Storage.ScopeObject);

                return op["AccessPaths"] as string[];
            }
        }

        public ulong[] GetSupportedSize()
        {
            using (var ip = MSFT_Partition.GetMethodParameters(nameof(GetSupportedSize)))
            using (var op = MSFT_Partition.InvokeMethod(nameof(GetSupportedSize), ip, null))
            {
                Job.Validator.ValidateOutput(op, Scope.Storage.ScopeObject);

                return new ulong[] { (ulong)op["SizeMin"], (ulong)op["SizeMax"] };
            }
        }

        public string Offline()
        {
            using (var ip = MSFT_Partition.GetMethodParameters(nameof(Offline)))
            using (var op = MSFT_Partition.InvokeMethod(nameof(Offline), ip, null))
            {
                Job.Validator.ValidateOutput(op, Scope.Storage.ScopeObject);

                return op["ExtendedStatus"] as string;
            }
        }

        public string Online()
        {
            using (var ip = MSFT_Partition.GetMethodParameters(nameof(Online)))
            using (var op = MSFT_Partition.InvokeMethod(nameof(Online), ip, null))
            {
                Job.Validator.ValidateOutput(op, Scope.Storage.ScopeObject);

                return op["ExtendedStatus"] as string;
            }
        }

        public string RemoveAccessPath(string AccessPath)
        {
            using (var ip = MSFT_Partition.GetMethodParameters(nameof(RemoveAccessPath)))
            {
                ip[nameof(AccessPath)] = AccessPath ?? throw new ArgumentNullException(nameof(AccessPath));

                using (var op = MSFT_Partition.InvokeMethod(nameof(RemoveAccessPath), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.ScopeObject);

                    return op["ExtendedStatus"] as string;
                }
            }
        }

        public string Resize(ulong Size)
        {
            using (var ip = MSFT_Partition.GetMethodParameters(nameof(Resize)))
            {
                ip[nameof(Size)] = Size;

                using (var op = MSFT_Partition.InvokeMethod(nameof(Resize), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.ScopeObject);

                    return op["ExtendedStatus"] as string;
                }
            }
        }

        public string SetAttributes(bool IsReadOnly, bool NoDefaultDriveLetter, bool IsActive, bool IsHidden)
        {
            using (var ip = MSFT_Partition.GetMethodParameters(nameof(SetAttributes)))
            {
                ip[nameof(IsReadOnly)] = IsReadOnly;
                ip[nameof(NoDefaultDriveLetter)] = NoDefaultDriveLetter;
                ip[nameof(IsActive)] = IsActive;
                ip[nameof(IsHidden)] = IsHidden;

                using (var op = MSFT_Partition.InvokeMethod(nameof(SetAttributes), ip, null))
                {
                    Job.Validator.ValidateOutput(op, Scope.Storage.ScopeObject);

                    return op["ExtendedStatus"] as string;
                }
            }
        }

        ~Partition()
        {
            MSFT_Partition.Dispose();
        }

        #region Utils

        public ManagementObject GetMsftVolume(int volumeIndex)
        {
            return MSFT_Partition.GetRelated("MSFT_Volume").Cast<ManagementObject>().Skip(volumeIndex).First();
        }

        #endregion
    }
}
