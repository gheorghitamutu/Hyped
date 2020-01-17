using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.DTO.Users;

namespace BackEnd.Business.Handlers.Users
{
    public class RegistrationSingle:IRequestHandler<RegisterSingle,User>
    {
        private readonly DataContext context;

        public RegistrationSingle(DataContext context)
        {
            this.context = context;
        }

        public async Task<User> Handle(RegisterSingle request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException($"{nameof(request)} is null!");
            }

            var user = User.Create(request.FirstName, request.LastName, request.Email, request.Country, request.Password, request.Rights, request.Workplace, request.PositionTitle, request.ContactNumber);
            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return user;
        }
    }
}
