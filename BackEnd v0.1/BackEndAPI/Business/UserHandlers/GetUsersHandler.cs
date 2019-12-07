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
            var users = await context.Users.ToListAsync();
            return users;
        }
    }
}
