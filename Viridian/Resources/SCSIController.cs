using System.Management;
using Viridian.Job;
using Viridian.Resources.Msvm;
using Viridian.Utilities;

namespace Viridian.Resources
{
    public class SCSIController
    {
        public void AddToVm(string serverName, string scopePath, string vmName)
        {
            var scope = Utils.GetScope(serverName, scopePath);

            using (var vmms = Utils.GetVirtualMachineManagementService(scope))
            {
                using (var resourcePool = Utils.GetWmiObject(scope, "Msvm_ResourcePool", "ResourceSubType = 'Microsoft:Hyper-V:Synthetic SCSI Controller' and Primordial = True"))
                {
                    var rasd = new ResourceAllocationSettingData();
                    using (var pas = rasd.GetPrototypeAllocationSettings(resourcePool, "0", "0"))
                    {
                        using (var resourceSettings = pas.Clone() as ManagementObject)
                        {
                            resourceSettings["Parent"] = null;

                            using (var inParams = vmms.GetMethodParameters("AddResourceSettings"))
                            {
                                using (var affectedConfiguration = Utils.GetVirtualMachineSettings(vmName, scope))
                                {
                                    inParams["AffectedConfiguration"] = affectedConfiguration;
                                    inParams["ResourceSettings"] = new[] { resourceSettings.GetText(TextFormat.WmiDtd20) };

                                    using (var outParams = vmms.InvokeMethod("AddResourceSettings", inParams, null))
                                    {
                                        Validator.ValidateOutput(outParams, scope);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
