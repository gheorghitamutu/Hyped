using BackEnd.Data;
using MediatR;
using System;

namespace BackEnd.DTOs.UserDTOs
{
    public class GetUserDetail:IRequest<User>
    {
        public GetUserDetail(Guid id)
        {
            UserId = id;
        }
        public Guid UserId { get; }
    }
}
