using BackEndAPI.Data;
using BackEndAPI.DTOs.UserDTOs;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndAPI.Business.UserHandlers
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
            user.Update(request.FirstName, request.LastName, request.Email, request.Country, request.Password, request.Rights, request.Workplace, request.PositionTitle, request.ContactNumber,request.VMS);
            await context.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
