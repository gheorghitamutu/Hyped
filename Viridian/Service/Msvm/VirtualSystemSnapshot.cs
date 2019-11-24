using System;
using System.Management;
using Viridian.Exceptions;
using Viridian.Job;
using Viridian.Utilities;

namespace Viridian.Service.Msvm
{
    public sealed class VirtualSystemSnapshot
    {
        private static VirtualSystemSnapshot instance = null;
        private const string serverName = ".";
        private const string scopePath = @"\Root\Virtualization\V2";
        private static ManagementObject Msvm_VirtualSystemSnapshotService = null;
        private static ManagementScope scope = null;

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

        private VirtualSystemSnapshot()
        {
            scope = new ManagementScope(new ManagementPath { Server = serverName, NamespacePath = scopePath }, null);

            using (var vsms = new ManagementClass(nameof(Msvm_VirtualSystemSnapshotService)))
            {
                vsms.Scope = scope;

                Msvm_VirtualSystemSnapshotService = Utils.GetFirstObjectFromCollection(vsms.GetInstances());
            } 
        }

        public static VirtualSystemSnapshot Instance
        {
            get
            {
                if (instance == null)                                    
                    instance = new VirtualSystemSnapshot();

                return instance;
            }
        }

        public ManagementObject MsvmVirtualSystemSnapshotService => Msvm_VirtualSystemSnapshotService ?? throw new ViridianException($"{nameof(Msvm_VirtualSystemSnapshotService)} is null!");

        #region MsvmProperties

        string InstanceID => Msvm_VirtualSystemSnapshotService[nameof(InstanceID)].ToString();
        string Caption => Msvm_VirtualSystemSnapshotService[nameof(Caption)].ToString();
        string Description => Msvm_VirtualSystemSnapshotService[nameof(Description)].ToString();
        string ElementName => Msvm_VirtualSystemSnapshotService[nameof(ElementName)].ToString();
        DateTime InstallDate => (DateTime)Msvm_VirtualSystemSnapshotService[nameof(InstallDate)];
        string Name => Msvm_VirtualSystemSnapshotService[nameof(Name)].ToString();
        ushort[] OperationalStatus => (ushort[])Msvm_VirtualSystemSnapshotService[nameof(OperationalStatus)];
        string[] StatusDescriptions => (string[])Msvm_VirtualSystemSnapshotService[nameof(StatusDescriptions)];
        string Status => Msvm_VirtualSystemSnapshotService[nameof(Status)].ToString();
        ushort HealthState => (ushort)Msvm_VirtualSystemSnapshotService[nameof(HealthState)];
        ushort CommunicationStatus => (ushort)Msvm_VirtualSystemSnapshotService[nameof(CommunicationStatus)];
        ushort DetailedStatus => (ushort)Msvm_VirtualSystemSnapshotService[nameof(DetailedStatus)];
        ushort OperatingStatus => (ushort)Msvm_VirtualSystemSnapshotService[nameof(OperatingStatus)];
        ushort PrimaryStatus => (ushort)Msvm_VirtualSystemSnapshotService[nameof(PrimaryStatus)];
        ushort EnabledState => (ushort)Msvm_VirtualSystemSnapshotService[nameof(EnabledState)];
        string OtherEnabledState => Msvm_VirtualSystemSnapshotService[nameof(OtherEnabledState)].ToString();
        ushort RequestedState => (ushort)Msvm_VirtualSystemSnapshotService[nameof(RequestedState)];
        ushort EnabledDefault => (ushort)Msvm_VirtualSystemSnapshotService[nameof(EnabledDefault)];
        DateTime TimeOfLastStateChange => (DateTime)Msvm_VirtualSystemSnapshotService[nameof(TimeOfLastStateChange)];
        ushort[] AvailableRequestedStates => (ushort[])Msvm_VirtualSystemSnapshotService[nameof(AvailableRequestedStates)];
        ushort TransitioningToState => (ushort)Msvm_VirtualSystemSnapshotService[nameof(TransitioningToState)];
        string SystemCreationClassName => Msvm_VirtualSystemSnapshotService[nameof(SystemCreationClassName)].ToString();
        string SystemName => Msvm_VirtualSystemSnapshotService[nameof(SystemName)].ToString();
        string CreationClassName => Msvm_VirtualSystemSnapshotService[nameof(CreationClassName)].ToString();
        string PrimaryOwnerName => Msvm_VirtualSystemSnapshotService[nameof(PrimaryOwnerName)].ToString();
        string PrimaryOwnerContact => Msvm_VirtualSystemSnapshotService[nameof(PrimaryOwnerContact)].ToString();
        string StartMode => Msvm_VirtualSystemSnapshotService[nameof(StartMode)].ToString();
        bool Started => (bool)Msvm_VirtualSystemSnapshotService[nameof(Started)];

        #endregion

        public void ApplySnapshot(ManagementObject Snapshot)
        {
            using (var ip = Msvm_VirtualSystemSnapshotService.GetMethodParameters(nameof(ApplySnapshot)))
            {
                ip[nameof(Snapshot)] = Snapshot ?? throw new ViridianException($"{nameof(Snapshot)} is null!");

                using (var op = Msvm_VirtualSystemSnapshotService.InvokeMethod(nameof(ApplySnapshot), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void ClearSnapshotState(ManagementObject SnapshotSettingData)
        {
            using (var ip = Msvm_VirtualSystemSnapshotService.GetMethodParameters(nameof(ClearSnapshotState)))
            {
                ip[nameof(SnapshotSettingData)] = SnapshotSettingData ?? throw new ViridianException($"{nameof(SnapshotSettingData)} is null!");

                using (var op = Msvm_VirtualSystemSnapshotService.InvokeMethod(nameof(ClearSnapshotState), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public ManagementObject CreateSnapshot(string AffectedSystem, string SnapshotSettings, ushort SnapshotType)
        {
            using (var ip = Msvm_VirtualSystemSnapshotService.GetMethodParameters(nameof(CreateSnapshot)))
            {
                ip[nameof(AffectedSystem)] = AffectedSystem ?? throw new ViridianException($"{nameof(AffectedSystem)} is null!");
                ip[nameof(SnapshotSettings)] = SnapshotSettings ?? throw new ViridianException($"{nameof(SnapshotSettings)} is null!");
                ip[nameof(SnapshotType)] = SnapshotType;

                using (var op = MsvmVirtualSystemSnapshotService.InvokeMethod(nameof(CreateSnapshot), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingSnapshot"] as ManagementObject;
                }
            }
        }

        public void DestroySnapshot(string AffectedSnapshot)
        {
            using (var ip = Msvm_VirtualSystemSnapshotService.GetMethodParameters(nameof(DestroySnapshot)))
            {
                ip[nameof(AffectedSnapshot)] = AffectedSnapshot ?? throw new ViridianException($"{nameof(AffectedSnapshot)} is null!");

                using (var op = MsvmVirtualSystemSnapshotService.InvokeMethod(nameof(DestroySnapshot), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void DestroySnapshotTree(string SnapshotSettingData)
        {
            using (var ip = Msvm_VirtualSystemSnapshotService.GetMethodParameters(nameof(DestroySnapshotTree)))
            {
                ip[nameof(SnapshotSettingData)] = SnapshotSettingData ?? throw new ViridianException($"{nameof(SnapshotSettingData)} is null!");

                using (var op = MsvmVirtualSystemSnapshotService.InvokeMethod(nameof(DestroySnapshotTree), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public void RequestStateChange(State RequestedState, DateTime TimeoutPeriod)
        {
            using (var ip = Msvm_VirtualSystemSnapshotService.GetMethodParameters(nameof(RequestStateChange)))
            {
                ip[nameof(RequestedState)] = RequestedState;
                ip[nameof(TimeoutPeriod)] = TimeoutPeriod;

                using (var op = MsvmVirtualSystemSnapshotService.InvokeMethod(nameof(RequestStateChange), ip, null))
                    Validator.ValidateOutput(op, scope);
            }
        }

        public ManagementObject ConvertToReferencePoint(string AffectedSnapshot, string ReferencePointSettings)
        {
            using (var ip = Msvm_VirtualSystemSnapshotService.GetMethodParameters(nameof(ConvertToReferencePoint)))
            {
                ip[nameof(AffectedSnapshot)] = AffectedSnapshot;
                ip[nameof(ReferencePointSettings)] = ReferencePointSettings;

                using (var op = MsvmVirtualSystemSnapshotService.InvokeMethod(nameof(ConvertToReferencePoint), ip, null))
                {
                    Validator.ValidateOutput(op, scope);

                    return op["ResultingReferencePoint"] as ManagementObject;
                }
            }
        }

        public void StartService()
        {
            using (var ip = MsvmVirtualSystemSnapshotService.GetMethodParameters(nameof(StartService)))
            using (var op = MsvmVirtualSystemSnapshotService.InvokeMethod(nameof(StartService), ip, null))
                Validator.ValidateOutput(op, scope);
        }

        public void StopService()
        {
            using (var ip = MsvmVirtualSystemSnapshotService.GetMethodParameters(nameof(StopService)))
            using (var op = MsvmVirtualSystemSnapshotService.InvokeMethod(nameof(StopService), ip, null))
                Validator.ValidateOutput(op, scope);
        }

        ~VirtualSystemSnapshot()
        {
            if (Msvm_VirtualSystemSnapshotService != null)
                Msvm_VirtualSystemSnapshotService.Dispose();
        }
    }
}
