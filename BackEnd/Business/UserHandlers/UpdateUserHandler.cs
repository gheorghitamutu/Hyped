using BackEnd.Data;
using BackEnd.DTOs.UserDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Business.UserHandlers
{
    public class UpdateUserHandler:IRequestHandler<UpdateUser,User>
    {
        private readonly DataContext context;

        public UpdateUserHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<User> Handle(UpdateUser request,CancellationToken cancellationToken)
        {
            var user = context.Users.SingleOrDefault(u => u.UserId == request.UserId);
            if(user==null)
            {
                throw new Exception("Requested user doesn't exist");
            }

            var vms = await context?.VMs?.ToListAsync();
            var networks = await context.Networks.ToListAsync();
            var vhds = await context.VHDs.ToListAsync();
            var scs = await context.SCs.ToListAsync();
            var cdvds = await context.CDVDs.ToListAsync();
            var user_vms = vms.Where((vm) => vm.UserId == user.UserId).ToList();
            //get this user's virtual machines
            //user.VMS=vms.Where((vm) => vm.UserId == user.UserId).ToList(); when icollection is private
            foreach (var vm in user_vms)
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
            user.Update(request.FirstName, request.LastName, request.Email, request.Country, request.Password, request.Rights, request.Workplace, request.PositionTitle, request.ContactNumber,user_vms);
            await context.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
