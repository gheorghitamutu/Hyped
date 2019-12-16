using MediatR;
using System;

namespace BackEndAPI.DTOs.UserDTOs
{
    public class DeleteUser:IRequest
    {
        public Guid UserId { get; set; }
    }
}
