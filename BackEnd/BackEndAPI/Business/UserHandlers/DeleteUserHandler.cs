using BackEndAPI.Data;
using BackEndAPI.DTOs.UserDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using Viridian.Msvm.VirtualSystem;
using Viridian.Msvm.VirtualSystemManagement;

namespace BackEndAPI.Business.UserHandlers
{
    public class DeleteUserHandler:IRequestHandler<DeleteUser>
    {
        private readonly DataContext context;

        public DeleteUserHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteUser request,CancellationToken cancellationToken)
        {
            var user = context.Users.SingleOrDefault(u => u.UserId == request.UserId);
            if(user==null)
            {
                throw new Exception("This user doesn't exist!");
            }
            //get this user's virtual machines
            var vms = await context?.VMs?.ToListAsync();
            user.VMS = vms.Where((vm) => vm.UserId == user.UserId).ToList();

            foreach(var vm in user.VMS)//for each virtual machine this user has
                {
                var delete_vm = context.VMs.SingleOrDefault(v => v.VMId == vm.VMId);
                if (delete_vm.RealID != null)//delete its resource folder
                {
                    var this_vm_path = System.IO.Path.Combine("Resources", delete_vm.RealID);

                    if (System.IO.Directory.Exists(this_vm_path))
                    {
                        System.IO.Directory.Delete(this_vm_path, true);
                    }
                    var to_delete_vm = new ComputerSystem(delete_vm.Name);
                    to_delete_vm.DestroySystem();
                }
                context.VMs.Remove(delete_vm);//remove the virtual machine from DB

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

                VirtualSystemManagementService.GetInstances().First().DestroySystem(virtualSystemSettingData.Path, out ManagementPath Job);
            }

            //remove the user from DB
            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
