using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;
using ViridianTester;

namespace BackEndAPI.Business.VMHandlers
{
    public class UpdateVMHandler:IRequestHandler<UpdateVM,VM>
    {
        private readonly DataContext context;

        public UpdateVMHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<VM> Handle(UpdateVM request, CancellationToken cancellationToken)
        {//TODO: update virtual machine name
            var vm = await context.VMs.SingleOrDefaultAsync(u => u.VMId == request.VMId);
            if (vm == null)
            {
                throw new Exception("Requested virtual machine doesn't exist");
            }

            var networks = await context.Networks.ToListAsync();
            var scs = await context.SCs.ToListAsync();
            var vhds = await context.VHDs.ToListAsync();
            var cdvds = await context.CDVDs.ToListAsync();

            var vm_networks = networks.Where((n) => n.VMId == vm.VMId).ToList();
            var vm_scs = scs.Where((s) => s.VMId == vm.VMId).ToList();
            foreach (var sc in vm_scs)
            {
                var sc_vhds = vhds.Where((v) => v.SCId == sc.SCId).ToList();
                var sc_cdvds = cdvds.Where((v) => v.SCId == sc.SCId).ToList();
                sc.Update(sc.Name, sc.InstanceId, sc_vhds, sc_cdvds, sc.VMId);
                await context.SaveChangesAsync(cancellationToken);
            }
            

            var viridianUtils = new ViridianUtils();
            var computerSystem =
                ComputerSystem.GetInstances()
                    .Cast<ComputerSystem>()
                    .Where((cs) => cs.Name == vm.RealID)
                    .ToList()
                    .First();

            var virtualSystemSettingData =
                    SettingsDefineState.GetInstances()
                        .Cast<SettingsDefineState>()
                        .Where((sds) => string.Compare(sds.ManagedElement.Path, computerSystem.Path.Path, true, CultureInfo.InvariantCulture) == 0)
                        .Select((sds) => new VirtualSystemSettingData(sds.SettingData))
                        .ToList()
                        .First();
            var AffectedConfiguration = virtualSystemSettingData.Path;
            //change number of cores
            var ReturnValue = computerSystem.RequestStateChange(2, null, out ManagementPath Job);

            ViridianUtils.WaitForConcreteJobToEnd(Job);

            computerSystem.UpdateObject();

            var vssdCollection = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem);

            var processorSettingDataList = ViridianUtils.GetProcessorSettingDataList(vssdCollection.First());

            var processorSettingData = processorSettingDataList.First();
            processorSettingData.LateBoundObject["VirtualQuantity"] = request.Cores;

            ReturnValue = computerSystem.RequestStateChange(3, null, out Job);

            ViridianUtils.WaitForConcreteJobToEnd(Job);

            computerSystem.UpdateObject();

            var ResourceSettings = new string[] { processorSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };
            viridianUtils.VSMS.ModifyResourceSettings(ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettingsProcessor);
            //change ram quantity
            ReturnValue = computerSystem.RequestStateChange(2, null, out Job);

            ViridianUtils.WaitForConcreteJobToEnd(Job);

            computerSystem.UpdateObject();

            vssdCollection = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem);

            var memorySettingDataList = ViridianUtils.GetMemorySettingDataList(vssdCollection.First());

            var memorySettingData = memorySettingDataList.First();
            
                memorySettingData.LateBoundObject["VirtualQuantity"] = request.RAM;

                ReturnValue = computerSystem.RequestStateChange(3, null, out Job);

                ViridianUtils.WaitForConcreteJobToEnd(Job);

                computerSystem.UpdateObject();

                ResourceSettings = new string[] { memorySettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                viridianUtils.VSMS.ModifyResourceSettings(ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettingsMemory);

            //get summary information
            vssdCollection = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem);
            var RequestedInformation = (uint[])Enum.GetValues(typeof(SummaryInformation.RequestedInformation));
            var SettingData = new ManagementPath[] { vssdCollection.First().Path };
            ReturnValue = viridianUtils.VSMS.GetSummaryInformation(RequestedInformation, SettingData, out ManagementBaseObject[] SummaryInformation);

            //directory with resources for this virtual machine
            string this_vm_directory = System.IO.Path.Combine("Resources", computerSystem.Name);

            //update the configuration file
            var configuration_file = JsonConvert.SerializeObject(SummaryInformation);//convert the virtual machine informations to json
            string this_configuration_filename = System.IO.Path.Combine(this_vm_directory, "configuration.json");
            System.IO.File.WriteAllText(this_configuration_filename, configuration_file);


            vm.Update(vm.RealID, vm.Name, vm.Configuration, vm.LastSave,vm_networks,request.RAM,request.Cores,vm_scs);
            await context.SaveChangesAsync(cancellationToken);
            return vm;
        }
    }
}
