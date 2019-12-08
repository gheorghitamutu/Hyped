using System.Collections.Generic;
using System.Linq;
using System.Management;
using Viridian.Scopes;

namespace Viridian.Msvm.Threshold
{
    public class VirtualSystemSnapshotSettingData
    {
        private ManagementObject Msvm_VirtualSystemSnapshotSettingData = null;

        public ManagementObject MsvmVirtualSystemSettingData
        {
            get
            {
                if (Msvm_VirtualSystemSnapshotSettingData == null)
                    using (var serviceClass = new ManagementClass(Scope.Virtualization.ScopeObject, new ManagementPath(nameof(Msvm_VirtualSystemSnapshotSettingData)), null))
                        Msvm_VirtualSystemSnapshotSettingData = serviceClass.GetInstances().Cast<ManagementObject>().First();

                return Msvm_VirtualSystemSnapshotSettingData;
            }

            private set
            {
                if (Msvm_VirtualSystemSnapshotSettingData != null)
                    Msvm_VirtualSystemSnapshotSettingData.Dispose();

                Msvm_VirtualSystemSnapshotSettingData = value;
            }
        }

        public enum ConsistencyLevelVSSSD : byte
        {
            Unknown = 0,
            ApplicationConsistent = 1,
            CrashConsistent = 2
        }
        public enum GuestBackupTypeVSSSD : byte
        {
            Undefined = 0,
            Full = 1,
            Copy = 2
        }

        #region MsvmProperties

        public ConsistencyLevelVSSSD ConsistencyLevel => (ConsistencyLevelVSSSD)(ushort)MsvmVirtualSystemSettingData[nameof(ConsistencyLevel)];
        public bool IgnoreNonSnapshottableDisks => (bool)MsvmVirtualSystemSettingData[nameof(IgnoreNonSnapshottableDisks)];
        public GuestBackupTypeVSSSD GuestBackupType => (GuestBackupTypeVSSSD)(ushort)MsvmVirtualSystemSettingData[nameof(GuestBackupType)];

        #endregion

        public void ModifyProperties(Dictionary<string, object> Properties)
        {
            Properties.ToList().ForEach((p) => MsvmVirtualSystemSettingData[p.Key] = p.Value);
        }
    }
}
