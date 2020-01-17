using MediatR;
using System;

namespace BackEnd.DTO.Users
{
    public class DeleteSingle:IRequest
    {
        public Guid UserId { get; set; }
    }
}
