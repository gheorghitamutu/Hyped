using BackEndAPI.Data;
using BackEndAPI.DTOs.UserDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndAPI.Business.UserHandlers
{
    public class GetUsersHandler : IRequestHandler<GetUsers, List<User>>
    {
        private readonly DataContext context;

        public GetUsersHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<User>> Handle(GetUsers request,CancellationToken cancellationToken)
        {
            var users = await context?.Users?.ToListAsync();
            var vms = await context?.VMs?.ToListAsync();
            var networks = await context.Networks.ToListAsync();
            var scs = await context.SCs.ToListAsync();
            var vhds = await context.VHDs.ToListAsync();
            var cdvds = await context.CDVDs.ToListAsync();

            
            //get the virtual machines of each user
            //users?.ForEach((u) => u.VMS = vms?.Where((vm) => vm.UserId == u.UserId).ToList());
            foreach (var user in users)
            {
                var user_vm = vms.Where((v) => v.UserId == user.UserId).ToList();
                         
                foreach (var vm in user_vm)
                {
                    var vm_networks = networks.Where((n) => n.VMId == vm.VMId).ToList();
                    var vm_scs = scs.Where((s) => s.VMId == vm.VMId).ToList();
                    foreach (var sc in vm_scs)
                    {
                        var sc_vhds = vhds.Where((v) => v.SCId == sc.SCId).ToList();
                        var sc_cdvds = cdvds.Where((v) => v.SCId == sc.SCId).ToList();
                        sc.Update(sc.Name, sc.InstanceId, sc_vhds, sc_cdvds, sc.VMId);
                        await context.SaveChangesAsync(cancellationToken);
                    }

                    vm.Update(vm.RealID, vm.Name, vm.Configuration, vm.LastSave, vm_networks, vm.RAM, vm.Cores, vm_scs);
                    await context.SaveChangesAsync(cancellationToken);
                }
                user.Update(user.FirstName, user.LastName, user.Email, user.Country, user.Password, user.Rights, user.Workplace, user.PositionTitle, user.ContactNumber, user_vm);
                await context.SaveChangesAsync(cancellationToken);      
            }

            return users;
        }
    }
}
