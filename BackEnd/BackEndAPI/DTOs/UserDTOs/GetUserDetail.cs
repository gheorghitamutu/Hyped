using BackEndAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.UserDTOs
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
