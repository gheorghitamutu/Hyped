using System.Linq;
using System.Management;

namespace Viridian.Msvm.VirtualSystem
{
    public sealed class VirtualSystemSettingData
    {
        private ManagementObject Msvm_VirtualSystemSettingData = null;
        private ComputerSystem ComputerSystem { set; get; }

        public ManagementObject MsvmVirtualSystemSettingData
        {
            get
            {
                Msvm_VirtualSystemSettingData =
                    ComputerSystem?
                        .MsvmComputerSystem.GetRelated("Msvm_VirtualSystemSettingData", "Msvm_SettingsDefineState", null, null, null, null, false, null)
                        .Cast<ManagementObject>()
                        .First();

                return Msvm_VirtualSystemSettingData;
            }

            set
            {
                if (Msvm_VirtualSystemSettingData != null)
                    Msvm_VirtualSystemSettingData.Dispose();

                Msvm_VirtualSystemSettingData = value;
            }
        }

        public VirtualSystemSettingData(ComputerSystem ComputerSystem)
        {
            this.ComputerSystem = ComputerSystem;
        }
    }
}
