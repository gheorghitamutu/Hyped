using BackEndAPI.Data;
using BackEndAPI.DTOs.UserDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
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
            //get the virtual machines of each user
            users?.ForEach((u) => u.VMS = vms?.Where((vm) => vm.UserId == u.UserId).ToList());

            return users;
        }
    }
}
