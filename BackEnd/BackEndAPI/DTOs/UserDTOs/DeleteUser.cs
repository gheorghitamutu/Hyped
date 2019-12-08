using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.UserDTOs
{
    public class DeleteUser:IRequest
    {
        public Guid UserId { get; set; }
    }
}
