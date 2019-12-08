using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Viridian.Msvm.VirtualSystem;

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
        {
            var create_vm = new ComputerSystem(request.Name);//create new virtual machine 

            
            string this_vm_directory = System.IO.Path.Combine("Resources" ,create_vm.Name);//directory with resources for this virtual machine
            System.IO.Directory.CreateDirectory(this_vm_directory);
            

            //create snapshot file
            
            create_vm.VirtualSystemSettingData.CreateSnapshot(VirtualSystemSettingData.SnapshotType.Full, false);
            var snapshot = create_vm.VirtualSystemSettingData.GetLastCreatedSnapshot();
            var snapshot_file = JsonConvert.SerializeObject(snapshot.MsvmVirtualSystemSettingData.Properties);//convert the snapshot properties to json
            string this_snapshot_filename = System.IO.Path.Combine(this_vm_directory, "snapshot.json");
            System.IO.File.WriteAllText(this_snapshot_filename,snapshot_file);
            

            //create configuration file
            
            var vm_info = create_vm.VirtualSystemSettingData.GetSummaryInformation();
            var configuration_file = JsonConvert.SerializeObject(vm_info);//convert the virtual machine informations to json
            string this_configuration_filename= System.IO.Path.Combine(this_vm_directory, "configuration.json");
            System.IO.File.WriteAllText(this_configuration_filename, configuration_file);

            var user = await context.Users.SingleOrDefaultAsync(u => u.UserId == request.UserId);//get the user that requested this virtual machine
            if (user == null)
            {
                throw new Exception("Couldn't find the requested user!");
            }
            //add the virtual machine to the DB
            var vm = VM.Create(create_vm.Name,create_vm.ElementName, this_configuration_filename, this_snapshot_filename, user.UserId);
            context.VMs.Add(vm);

            await context.SaveChangesAsync(cancellationToken);
            return vm;
        }
    }
}
