using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs
{
    public class DeleteVM:IRequest
    {
        public Guid VMId { get; set; }
    }
}
