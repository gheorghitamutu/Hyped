using MediatR;
using System;

namespace BackEnd.DTOs.UserDTOs
{
    public class DeleteUser:IRequest
    {
        public Guid UserId { get; set; }
    }
}
