using System;
using System.Linq;
using System.Management;
using Viridian.Scopes;

namespace Viridian.Msvm
{
    public class BaseService
    {
        protected ManagementObject Service { get; set; }
        protected string ServiceName { get; set; }

        protected BaseService(string MsvmServiceName)
        {
            using (var vsms = new ManagementClass(MsvmServiceName))
            {
                vsms.Scope = Scope.Virtualization.SpecificScope;

                Service = vsms.GetInstances().Cast<ManagementObject>().First();
            }

            ServiceName = MsvmServiceName;
        }

        ~BaseService()
        {
            Service.Dispose();
        }

        #region MsvmProperties

        public string InstanceID => Service[nameof(InstanceID)].ToString();
        public string Caption => Service[nameof(Caption)].ToString();
        public string Description => Service[nameof(Description)].ToString();
        public string ElementName => Service[nameof(ElementName)].ToString();
        public DateTime InstallDate => ManagementDateTimeConverter.ToDateTime(Service[nameof(InstallDate)].ToString());
#pragma warning disable CA1819 // Properties should not return arrays
        public ushort[] OperationalStatus => (ushort[])Service[nameof(OperationalStatus)];
#pragma warning restore CA1819 // Properties should not return arrays
#pragma warning disable CA1819 // Properties should not return arrays
        public string[] StatusDescriptions => (string[])Service[nameof(StatusDescriptions)];
#pragma warning restore CA1819 // Properties should not return arrays
        public string Status => Service[nameof(Status)].ToString();
        public ushort HealthState => (ushort)Service[nameof(HealthState)];
        public ushort CommunicationStatus => (ushort)Service[nameof(CommunicationStatus)];
        public ushort DetailedStatus => (ushort)Service[nameof(DetailedStatus)];
        public ushort OperatingStatus => (ushort)Service[nameof(OperatingStatus)];
        public ushort PrimaryStatus => (ushort)Service[nameof(PrimaryStatus)];
        public ushort EnabledState => (ushort)Service[nameof(EnabledState)];
        public string OtherEnabledState => Service[nameof(OtherEnabledState)].ToString();
        public ushort RequestedState => (ushort)Service[nameof(RequestedState)];
        public ushort EnabledDefault => (ushort)Service[nameof(EnabledDefault)];
        public DateTime TimeOfLastStateChange => ManagementDateTimeConverter.ToDateTime(Service[nameof(TimeOfLastStateChange)].ToString());
#pragma warning disable CA1819 // Properties should not return arrays
        public ushort[] AvailableRequestedStates => (ushort[])Service[nameof(AvailableRequestedStates)];
#pragma warning restore CA1819 // Properties should not return arrays
        public ushort TransitioningToState => (ushort)Service[nameof(TransitioningToState)];
        public string SystemCreationClassName => Service[nameof(SystemCreationClassName)].ToString();
        public string SystemName => Service[nameof(SystemName)].ToString();
        public string CreationClassName => Service[nameof(CreationClassName)].ToString();
        public string Name => Service[nameof(Name)].ToString();
        public string PrimaryOwnerName => Service[nameof(PrimaryOwnerName)].ToString();
        public string PrimaryOwnerContact => Service[nameof(PrimaryOwnerContact)].ToString();
        public string StartMode => Service[nameof(StartMode)].ToString();
        public bool Started => (bool)Service[nameof(Started)];

        #endregion

        public virtual void StartService() => throw new NotSupportedException();

        public virtual void StopService() => throw new NotSupportedException();
    }
}
