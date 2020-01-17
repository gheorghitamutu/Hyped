using BackEnd.Data;
using MediatR;
using System;

namespace BackEnd.DTO.Users
{
    public class GetSingle:IRequest<User>
    {
        public GetSingle(Guid id)
        {
            UserId = id;
        }
        public Guid UserId { get; }
    }
}
