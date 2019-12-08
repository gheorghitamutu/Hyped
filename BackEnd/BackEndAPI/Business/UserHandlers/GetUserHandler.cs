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
    public class GetUserHandler:IRequestHandler<GetUserDetail,User>
    {
        private readonly DataContext context;

        public GetUserHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<User> Handle(GetUserDetail request,CancellationToken cancellationToken)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserId == request.UserId);
            if(user==null)
            {
                throw new Exception("Requested user doesn't exist");
            }
            return user;
        }
    }
    
    
}
