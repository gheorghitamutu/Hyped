using System;
using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Scopes;

namespace Viridian.Msvm.VirtualSystemManagement
{
    public sealed class VirtualSystemSnapshotService : BaseService
    {
        private static VirtualSystemSnapshotService instance = null;

        public enum State : ushort
        {
            Enabled = 2,
            Disabled = 3,
            ShutDown = 4,
            Offline = 6,
            Test = 7,
            Defer = 8,
            Quiesce = 9,
            Reboot = 10,
            Reset = 11
        }

        private VirtualSystemSnapshotService() : base("Msvm_VirtualSystemSnapshotService") { }

        public static VirtualSystemSnapshotService Instance
        {
            get
            {
                if (instance == null)                                    
                    instance = new VirtualSystemSnapshotService();

                return instance;
            }
        }

        public ManagementObject Msvm_VirtualSystemSnapshotService => Service ?? throw new ViridianException($"{nameof(ServiceName)} is null!");
              
        public void ApplySnapshot(ManagementObject Snapshot)
        {
            using (var ip = Msvm_VirtualSystemSnapshotService.GetMethodParameters(nameof(ApplySnapshot)))
            {
                ip[nameof(Snapshot)] = Snapshot ?? throw new ViridianException($"{nameof(Snapshot)} is null!");

                using (var op = Msvm_VirtualSystemSnapshotService.InvokeMethod(nameof(ApplySnapshot), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void ClearSnapshotState(ManagementObject SnapshotSettingData)
        {
            using (var ip = Msvm_VirtualSystemSnapshotService.GetMethodParameters(nameof(ClearSnapshotState)))
            {
                ip[nameof(SnapshotSettingData)] = SnapshotSettingData ?? throw new ViridianException($"{nameof(SnapshotSettingData)} is null!");

                using (var op = Msvm_VirtualSystemSnapshotService.InvokeMethod(nameof(ClearSnapshotState), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public ManagementObject CreateSnapshot(string AffectedSystem, string SnapshotSettings, ushort SnapshotType)
        {
            using (var ip = Msvm_VirtualSystemSnapshotService.GetMethodParameters(nameof(CreateSnapshot)))
            {
                ip[nameof(AffectedSystem)] = AffectedSystem ?? throw new ViridianException($"{nameof(AffectedSystem)} is null!");
                ip[nameof(SnapshotSettings)] = SnapshotSettings ?? throw new ViridianException($"{nameof(SnapshotSettings)} is null!");
                ip[nameof(SnapshotType)] = SnapshotType;

                using (var op = Msvm_VirtualSystemSnapshotService.InvokeMethod(nameof(CreateSnapshot), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ResultingSnapshot"] as ManagementObject;
                }
            }
        }

        public void DestroySnapshot(string AffectedSnapshot)
        {
            using (var ip = Msvm_VirtualSystemSnapshotService.GetMethodParameters(nameof(DestroySnapshot)))
            {
                ip[nameof(AffectedSnapshot)] = AffectedSnapshot ?? throw new ViridianException($"{nameof(AffectedSnapshot)} is null!");

                using (var op = Msvm_VirtualSystemSnapshotService.InvokeMethod(nameof(DestroySnapshot), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void DestroySnapshotTree(string SnapshotSettingData)
        {
            using (var ip = Msvm_VirtualSystemSnapshotService.GetMethodParameters(nameof(DestroySnapshotTree)))
            {
                ip[nameof(SnapshotSettingData)] = SnapshotSettingData ?? throw new ViridianException($"{nameof(SnapshotSettingData)} is null!");

                using (var op = Msvm_VirtualSystemSnapshotService.InvokeMethod(nameof(DestroySnapshotTree), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public void RequestStateChange(State RequestedState, DateTime TimeoutPeriod)
        {
            using (var ip = Msvm_VirtualSystemSnapshotService.GetMethodParameters(nameof(RequestStateChange)))
            {
                ip[nameof(RequestedState)] = RequestedState;
                ip[nameof(TimeoutPeriod)] = TimeoutPeriod;

                using (var op = Msvm_VirtualSystemSnapshotService.InvokeMethod(nameof(RequestStateChange), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
            }
        }

        public ManagementObject ConvertToReferencePoint(string AffectedSnapshot, string ReferencePointSettings)
        {
            using (var ip = Msvm_VirtualSystemSnapshotService.GetMethodParameters(nameof(ConvertToReferencePoint)))
            {
                ip[nameof(AffectedSnapshot)] = AffectedSnapshot;
                ip[nameof(ReferencePointSettings)] = ReferencePointSettings;

                using (var op = Msvm_VirtualSystemSnapshotService.InvokeMethod(nameof(ConvertToReferencePoint), ip, null))
                {
                    Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);

                    return op["ResultingReferencePoint"] as ManagementObject;
                }
            }
        }

        ~VirtualSystemSnapshotService()
        {
            if (Msvm_VirtualSystemSnapshotService != null)
                Msvm_VirtualSystemSnapshotService.Dispose();
        }
    }
}
