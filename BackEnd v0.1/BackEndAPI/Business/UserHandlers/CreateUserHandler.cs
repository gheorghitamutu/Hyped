using BackEndAPI.Data;
using BackEndAPI.DTOs.UserDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndAPI.Business.UserHandlers
{
    public class CreateUserHandler:IRequestHandler<CreateUser,User>
    {
        private readonly DataContext context;

        public CreateUserHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<User> Handle(CreateUser request,CancellationToken cancellationToken)
        {
            var user = User.Create(request.FirstName, request.LastName, request.Email, request.Country, request.Password, request.Rights, request.Workplace, request.PositionTitle, request.ContactNumber);
            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
