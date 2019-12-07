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
            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
