using System;
using System.Management;
using Viridian.Job;
using Viridian.Scopes;

namespace Viridian.Msvm.Storage
{
    public sealed class MountedStorageImage
    {
        private ManagementObject Msvm_MountedStorageImage = null;

        public enum AccessMSI : ushort
        {
            ReadOnly = 1,
            ReadWrite = 2
        }
        public enum TypeMSI : ushort
        {
            VirtualHardDisk = 0,
            ISOImage = 1
        }

        #region MsvmProperties

        string InstanceID => Msvm_MountedStorageImage[nameof(InstanceID)].ToString();
        string Caption => Msvm_MountedStorageImage[nameof(Caption)].ToString();
        string Description => Msvm_MountedStorageImage[nameof(Description)].ToString();
        string ElementName => Msvm_MountedStorageImage[nameof(ElementName)].ToString();
        DateTime InstallDate => ManagementDateTimeConverter.ToDateTime(Msvm_MountedStorageImage[nameof(InstallDate)].ToString());
        string Name => Msvm_MountedStorageImage[nameof(Name)].ToString();
        ushort[] OperationalStatus => (ushort[])Msvm_MountedStorageImage[nameof(OperationalStatus)];
        string[] StatusDescriptions => (string[])Msvm_MountedStorageImage[nameof(StatusDescriptions)];
        string Status => Msvm_MountedStorageImage[nameof(Status)].ToString();
        ushort HealthState => (ushort)Msvm_MountedStorageImage[nameof(HealthState)];
        ushort CommunicationStatus => (ushort)Msvm_MountedStorageImage[nameof(CommunicationStatus)];
        ushort DetailedStatus => (ushort)Msvm_MountedStorageImage[nameof(DetailedStatus)];
        ushort OperatingStatus => (ushort)Msvm_MountedStorageImage[nameof(OperatingStatus)];
        ushort PrimaryStatus => (ushort)Msvm_MountedStorageImage[nameof(PrimaryStatus)];
        TypeMSI Type => (TypeMSI)(ushort)Msvm_MountedStorageImage[nameof(Type)];
        AccessMSI Access => (AccessMSI)(ushort)Msvm_MountedStorageImage[nameof(Access)];
        byte PortNumber => (byte)Msvm_MountedStorageImage[nameof(PortNumber)];
        byte PathId => (byte)Msvm_MountedStorageImage[nameof(PathId)];
        byte TargetId => (byte)Msvm_MountedStorageImage[nameof(TargetId)];
        byte Lun => (byte)Msvm_MountedStorageImage[nameof(Lun)];
        string PnpDevicePath => Msvm_MountedStorageImage[nameof(PnpDevicePath)].ToString();

        #endregion

        public MountedStorageImage(ManagementObject MsvmMountedStorageImage)
        {
            Msvm_MountedStorageImage = MsvmMountedStorageImage;
        }

        public ManagementObject MsvmMountedStorageImage => Msvm_MountedStorageImage;

        public void DetachVirtualHardDisk()
        {
            using (var ip = Msvm_MountedStorageImage.GetMethodParameters(nameof(DetachVirtualHardDisk)))
            using (var op = Msvm_MountedStorageImage.InvokeMethod(nameof(DetachVirtualHardDisk), ip, null))
                Validator.ValidateOutput(op, Scope.Virtualization.SpecificScope);
        }

        ~MountedStorageImage()
        {
            Msvm_MountedStorageImage.Dispose();
        }
    }
}
