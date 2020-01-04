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
using Viridian.Root.Virtualization.v2.Msvm.Memory;
using Viridian.Root.Virtualization.v2.Msvm.Processor;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystemManagement;
using ViridianTester;

namespace BackEndAPI.Business.VMHandlers
{
    public class CreateVMHandler:IRequestHandler<CreateVM, VM>
    {
        private readonly DataContext context;

        public CreateVMHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<VM> Handle(CreateVM request, CancellationToken cancellationToken)
        {//creeaza o masina virtuala folosind request.Cores,request.RAM si request.Name
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserId == request.UserId);//get the user that requested this virtual machine
            if (user == null)
            {
                throw new Exception("Couldn't find the requested user!"); 
            }
            
            

            //create VM
            var viridianUtils = new ViridianUtils();
            
                viridianUtils.SUT_ComputerSystemMO(
                    request.Name,
                    out uint ReturnValue,
                    out ManagementPath Job,
                    out ManagementPath ResultingSystem);


            var computerSystem = new ComputerSystem(ResultingSystem);
                
                    //change RAM quantity
                    ReturnValue = computerSystem.RequestStateChange(2, null, out Job);

                    ViridianUtils.WaitForConcreteJobToEnd(Job);

                    computerSystem.UpdateObject();

                    var vssdCollection = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem);

                    var memorySettingDataList = ViridianUtils.GetMemorySettingDataList(vssdCollection.First());

                    var memorySettingData = memorySettingDataList.First();
                    
                        memorySettingData.LateBoundObject["VirtualQuantity"] = request.RAM;

                        ReturnValue = computerSystem.RequestStateChange(3, null, out Job);

                        ViridianUtils.WaitForConcreteJobToEnd(Job);

                        computerSystem.UpdateObject();

                        var ResourceSettings = new string[] { memorySettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                        viridianUtils.VSMS.ModifyResourceSettings(ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettingsMemory);

                        var MemoryInfo = new MemorySettingData(ResultingResourceSettingsMemory[0]);
                        
                        var vmRAM=Convert.ToInt32(MemoryInfo.VirtualQuantity);
                        
                    

                    //change number of processors
                    ReturnValue = computerSystem.RequestStateChange(2, null, out Job);

                    ViridianUtils.WaitForConcreteJobToEnd(Job);

                    computerSystem.UpdateObject();

                    vssdCollection = ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem);

                    var processorSettingDataList = ViridianUtils.GetProcessorSettingDataList(vssdCollection.First());

                    var processorSettingData = processorSettingDataList.First();
                    processorSettingData.LateBoundObject["VirtualQuantity"] = request.Cores;

                    ReturnValue = computerSystem.RequestStateChange(3, null, out Job);

                    ViridianUtils.WaitForConcreteJobToEnd(Job);

                    computerSystem.UpdateObject();

                    ResourceSettings = new string[] { processorSettingData.LateBoundObject.GetText(TextFormat.WmiDtd20) };
                    viridianUtils.VSMS.ModifyResourceSettings(ResourceSettings, out Job, out ManagementPath[] ResultingResourceSettingsProcessor);

                    var ProcessorInfo = new ProcessorSettingData(ResultingResourceSettingsProcessor[0]);
                    
                    var vmCores=Convert.ToInt32(ProcessorInfo.VirtualQuantity);
                    
            //get summary information
            vssdCollection= ViridianUtils.GetVirtualSystemSettingDataListThroughSettingsDefineState(computerSystem);
            var RequestedInformation = (uint[])Enum.GetValues(typeof(SummaryInformation.RequestedInformation));
            var SettingData = new ManagementPath[] { vssdCollection.First().Path };
            ReturnValue = viridianUtils.VSMS.GetSummaryInformation(RequestedInformation, SettingData, out ManagementBaseObject[] SummaryInformation);

            //directory with resources for this virtual machine
            string this_vm_directory = System.IO.Path.Combine("Resources", computerSystem.Name);
            System.IO.Directory.CreateDirectory(this_vm_directory);

           
            //create configuration file
            var configuration_file = JsonConvert.SerializeObject(SummaryInformation);//convert the virtual machine informations to json
            string this_configuration_filename = System.IO.Path.Combine(this_vm_directory, "configuration.json");
            System.IO.File.WriteAllText(this_configuration_filename, configuration_file);


            //create snapshot file
            //fisierul snapshot.json e gol!!!!???
            ViridianUtils.WaitForConcreteJobToEnd(Job);
            var sovsCollection = ViridianUtils.GetSnapshotOfVirtualSystemList(computerSystem);
            var mcsibCollection = ViridianUtils.GetMostCurrentSnapshotInBranchList(computerSystem);

            var snapshot_file = JsonConvert.SerializeObject(mcsibCollection);//convert the snapshot properties to json
            string this_snapshot_filename = System.IO.Path.Combine(this_vm_directory, "snapshot.json");
            System.IO.File.WriteAllText(this_snapshot_filename, snapshot_file);





            var vm = VM.Create(computerSystem.Name, computerSystem.ElementName, this_configuration_filename, this_snapshot_filename, vmRAM,request.Processors,vmCores,request.Threads, user.UserId);
            context.VMs.Add(vm);

            await context.SaveChangesAsync(cancellationToken);

            return vm;
        }
    }
}
