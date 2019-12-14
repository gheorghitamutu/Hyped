using System;
using System.Linq;
using System.Management;
using Viridian.Job;
using Viridian.Msvm.VirtualSystem;
using Viridian.Resources.Network;
using Viridian.Scopes;

namespace Viridian.Msvm.Networking
{
    public sealed class VirtualEthernetSwitch
    {
        private ManagementObject Msvm_VirtualEthernetSwitch = null; // don't directly use it unless explicitly required (Name property)!
        public VirtualEthernetSwitchSettingData VirtualEthernetSwitchSettingData { get; private set; }

        private string elementName = null; // unique identifier

        public VirtualEthernetSwitch(string ElementName, string[] ResourceSettings = null)
        {
            this.ElementName = ElementName;

            Define(ResourceSettings);

            VirtualEthernetSwitchSettingData = new VirtualEthernetSwitchSettingData(this, Properties.VirtualSystemSettingData.Default.Msvm_SettingsDefineState);
        }

        public ManagementObject MsvmVirtualEthernetSwitch
        {
            get
            {
                if (Name == null && Msvm_VirtualEthernetSwitch == null)
                    return null;

                if (Name == null)
                    return QueryMsvmVirtualEthernetSwitch(nameof(ElementName), ElementName);

                return QueryMsvmVirtualEthernetSwitch(nameof(Name), Name);
            }

            set
            {
                if (Msvm_VirtualEthernetSwitch != null)
                    Msvm_VirtualEthernetSwitch.Dispose();

                Msvm_VirtualEthernetSwitch = value;
            }
        }

        private void Define(string[] ResourceSettings)
        {
            if (MsvmVirtualEthernetSwitch == null)
            {
                var virtualEthernetSwitch = QueryMsvmVirtualEthernetSwitch(nameof(ElementName), ElementName);
                // TODO: fix this
                //MsvmVirtualEthernetSwitch = virtualEthernetSwitch ??
                //        VirtualEthernetSwitchManagementService.Instance.DefineSystem(
                //            new VirtualEthernetSwitchSettingData().ModifyProperties(
                //                new Dictionary<string, object>()
                //                {
                //                    { nameof(ElementName), ElementName }
                //                }).GetText(TextFormat.WmiDtd20),
                //            ResourceSettings,
                //            null);
            }
        }

        #region Enums
        public enum CommunicationStatusVES : ushort
        {
            Unknown = 0,
            NotAvailable = 1,
            CommunicationOK = 2,
            LostCommunication = 3,
            NoContact = 4
        }
        public enum DetailedStatusVES : ushort
        {
            Unknown = 0,
            NoAdditionalInformation = 1,
            Stressed = 2,
            PredictiveFailure = 3,
            NonRecoverableError = 4,
            SupportingEntityInError = 5
        }
        public enum EnabledDefaultVES : ushort
        {
            Enabled = 2,
            Disabled = 3,
            EnabledButOffline = 6
        }
        public enum HealthStateVES : ushort
        {
            OK = 5,
            MajorFailure = 20,
            CriticalFailure = 25
        }
        public enum OperatingStatusVES
        {
            Unknown = 0,
            NotAvailable = 1,
            Servicing = 2,
            Starting = 3,
            Stopping = 4,
            Stopped = 5,
            Aborted = 6,
            Dormant = 7,
            Completed = 8,
            Migrating = 9,
            Emigrating = 10,
            Immigrating = 11,
            Snapshotting = 12,
            ShuttingDown = 13,
            InTest = 14,
            Transitioning = 15,
            InService = 16
        }
        public enum PrimaryStatusVES
        {
            Unknown = 0,
            OK = 1,
            Degraded = 2,
            Error = 3
        }
        public enum AvailableRequestedStatesVES
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

        #endregion

        #region MsvmProperties

        public string InstanceID => MsvmVirtualEthernetSwitch[nameof(InstanceID)]?.ToString();
        public string Caption => MsvmVirtualEthernetSwitch[nameof(Caption)].ToString();
        public string Description => MsvmVirtualEthernetSwitch[nameof(Description)].ToString();
        public string ElementName
        {
            get { return Msvm_VirtualEthernetSwitch != null ? MsvmVirtualEthernetSwitch[nameof(ElementName)].ToString() : elementName; }
            private set { elementName = value; }
        }
        public DateTime InstallDate => ManagementDateTimeConverter.ToDateTime(MsvmVirtualEthernetSwitch[nameof(InstallDate)].ToString());
        public ushort[] OperationalStatus => (ushort[])MsvmVirtualEthernetSwitch[nameof(OperationalStatus)];
        public string[] StatusDescriptions => (string[])MsvmVirtualEthernetSwitch[nameof(StatusDescriptions)];
        public string Status => MsvmVirtualEthernetSwitch[nameof(Status)].ToString();
        public HealthStateVES HealthState => (HealthStateVES)MsvmVirtualEthernetSwitch[nameof(HealthState)];
        public CommunicationStatusVES CommunicationStatus => (CommunicationStatusVES)(ushort)MsvmVirtualEthernetSwitch[nameof(CommunicationStatus)];
        public DetailedStatusVES DetailedStatus => (DetailedStatusVES)(ushort)MsvmVirtualEthernetSwitch[nameof(DetailedStatus)];
        public OperatingStatusVES OperatingStatus => (OperatingStatusVES)(ushort)MsvmVirtualEthernetSwitch[nameof(OperatingStatus)];
        public PrimaryStatusVES PrimaryStatus => (PrimaryStatusVES)(ushort)MsvmVirtualEthernetSwitch[nameof(PrimaryStatus)];
        public ushort EnabledState => (ushort)MsvmVirtualEthernetSwitch[nameof(EnabledState)];
        public string OtherEnabledState => MsvmVirtualEthernetSwitch[nameof(OtherEnabledState)].ToString();
        public ushort RequestedState => (ushort)MsvmVirtualEthernetSwitch[nameof(RequestedState)];
        public EnabledDefaultVES EnabledDefault => (EnabledDefaultVES)MsvmVirtualEthernetSwitch[nameof(EnabledDefault)];
        public DateTime TimeOfLastStateChange => ManagementDateTimeConverter.ToDateTime(MsvmVirtualEthernetSwitch[nameof(TimeOfLastStateChange)].ToString());
        public AvailableRequestedStatesVES[] AvailableRequestedStates => (AvailableRequestedStatesVES[])MsvmVirtualEthernetSwitch[nameof(AvailableRequestedStates)];
        public ushort TransitioningToState => (ushort)MsvmVirtualEthernetSwitch[nameof(TransitioningToState)];
        public string CreationClassName => MsvmVirtualEthernetSwitch[nameof(CreationClassName)].ToString();
        public string Name => Msvm_VirtualEthernetSwitch?[nameof(Name)].ToString();
        public string PrimaryOwnerName => MsvmVirtualEthernetSwitch[nameof(PrimaryOwnerName)].ToString();
        public string PrimaryOwnerContact => MsvmVirtualEthernetSwitch[nameof(PrimaryOwnerContact)].ToString();
        public string[] Roles => (string[])MsvmVirtualEthernetSwitch[nameof(Roles)];
        public string NameFormat => MsvmVirtualEthernetSwitch[nameof(NameFormat)].ToString();
        public string[] OtherIdentifyingInfo => (string[])MsvmVirtualEthernetSwitch[nameof(OtherIdentifyingInfo)];
        public string[] IdentifyingDescriptions => (string[])MsvmVirtualEthernetSwitch[nameof(IdentifyingDescriptions)];
        public ushort[] Dedicated => (ushort[])MsvmVirtualEthernetSwitch[nameof(Dedicated)];
        public string[] OtherDedicatedDescriptions => (string[])MsvmVirtualEthernetSwitch[nameof(OtherDedicatedDescriptions)];
        public ushort ResetCapability => (ushort)MsvmVirtualEthernetSwitch[nameof(ResetCapability)];
        public ushort[] PowerManagementCapabilities => (ushort[])MsvmVirtualEthernetSwitch[nameof(PowerManagementCapabilities)];
        public uint MaxVMQOffloads => (uint)MsvmVirtualEthernetSwitch[nameof(MaxVMQOffloads)];
        public uint MaxIOVOffloads => (uint)MsvmVirtualEthernetSwitch[nameof(MaxIOVOffloads)];


        #endregion

        #region MsvmMethods
        public void RequestStateChange(AvailableRequestedStatesVES RequestedState, ulong TimeoutPeriod = 0)
        {
            using (var ip = MsvmVirtualEthernetSwitch.GetMethodParameters(nameof(RequestStateChange)))
            {
                ip[nameof(RequestedState)] = (ushort)RequestedState;
                ip[nameof(TimeoutPeriod)] = null; // CIM_DateTime

                using (var op = MsvmVirtualEthernetSwitch.InvokeMethod(nameof(RequestStateChange), ip, null))
                    Validator.ValidateOutput(op, Scope.Virtualization.ScopeObject);
            }
        }

        #endregion

        public static ManagementObject QueryMsvmVirtualEthernetSwitch(string Name, string Value)
        {
            using (var mos = new ManagementObjectSearcher(Scope.Virtualization.ScopeObject, new ObjectQuery($"SELECT * FROM {nameof(Msvm_VirtualEthernetSwitch)}")))
                return mos.Get().Cast<ManagementObject>().Where((c) => c[Name]?.ToString() == Value).FirstOrDefault();
        }

        public static VirtualEthernetSwitch CreatePrivateSwitch(string ElementName)
        {
            return new VirtualEthernetSwitch(ElementName, null);
        }

        public static VirtualEthernetSwitch CreateInternalSwitch(string ElementName)
        {
            using (var host = ComputerSystem.GetInstances($"Name='{Environment.MachineName}'").Cast<ManagementObject>().ToList().First())
            using (var depasd = NetSwitch.GetDefaultEthernetPortAllocationSettingData())
            {
                depasd[nameof(ElementName)] = ElementName;
                depasd["HostResource"] = new string[] { host.Path.Path };

                string[] ResourceSettings = new string[] { depasd.GetText(TextFormat.WmiDtd20) };

                return new VirtualEthernetSwitch(ElementName, ResourceSettings);
            }
        }

        public static VirtualEthernetSwitch CreateExternalOnlySwitch(string ExternalAdapterName, string ElementName)
        {
            using (var eep = NetSwitch.FindExternalAdapter(ExternalAdapterName))
            using (var depasd = NetSwitch.GetDefaultEthernetPortAllocationSettingData())
            {
                depasd[nameof(ElementName)] = ElementName;
                depasd["HostResource"] = new string[] { eep.Path.Path };

                string[] ResourceSettings = new string[] { depasd.GetText(TextFormat.WmiDtd20) };

                return new VirtualEthernetSwitch(ElementName, ResourceSettings);
            }
        }

        public static VirtualEthernetSwitch CreateExternalSwitch(string externalAdapterName, string ElementName)
        {
            using (var eep = NetSwitch.FindExternalAdapter(externalAdapterName))
            using (var host = ComputerSystem.GetInstances($"Name='{Environment.MachineName}'").Cast<ManagementObject>().ToList().First())
            using (var depasdInternal = NetSwitch.GetDefaultEthernetPortAllocationSettingData())
            using (var depasdExternal = NetSwitch.GetDefaultEthernetPortAllocationSettingData())
            {
                depasdExternal[nameof(ElementName)] = ElementName + "_External";
                depasdExternal["HostResource"] = new string[] { eep.Path.Path };

                depasdInternal[nameof(ElementName)] = ElementName + "_Internal";
                depasdInternal["HostResource"] = new string[] { host.Path.Path };
                depasdInternal["Address"] = eep["PermanentAddress"];

                string[] ResourceSettings = new string[] { depasdExternal.GetText(TextFormat.WmiDtd20), depasdInternal.GetText(TextFormat.WmiDtd20) };

                return new VirtualEthernetSwitch(ElementName, ResourceSettings);
            }
        }
        public void DestroySystem()
        {
            // TODO: fix this
            //VirtualEthernetSwitchManagementService.Instance.DestroySystem(MsvmVirtualEthernetSwitch);
            MsvmVirtualEthernetSwitch.Dispose();
            MsvmVirtualEthernetSwitch = null;
        }

        ~VirtualEthernetSwitch()
        {
            if (Msvm_VirtualEthernetSwitch != null)
                Msvm_VirtualEthernetSwitch.Dispose();
        }
    }
}
