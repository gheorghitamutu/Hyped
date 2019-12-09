using BackEndAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs
{
    public class CreateVM:IRequest<VM>,IRequest<User>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
