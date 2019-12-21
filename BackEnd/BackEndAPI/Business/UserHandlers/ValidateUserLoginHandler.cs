using BackEndAPI.Data;
using BackEndAPI.DTOs.UserDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndAPI.Business.UserHandlers
{
    public class ValidateUserLoginHandler:IRequestHandler<ValidateUserLogin,User>
    {
        
            private readonly DataContext context;

            public ValidateUserLoginHandler(DataContext context)
            {
                this.context = context;
            }

            public async Task<User> Handle(ValidateUserLogin request, CancellationToken cancellationToken)
            {
                var user = await context.Users.SingleOrDefaultAsync(u => (u.Email == request.Email && u.Password == request.Password));
                if (user == null)
                {
                    throw new Exception("No such user with these email and password!");
                }
                return user;
            }
        
    }
}
