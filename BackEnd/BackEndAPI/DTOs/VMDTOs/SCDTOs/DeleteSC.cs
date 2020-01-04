using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs.SCDTOs
{
    public class DeleteSC:IRequest
    {
        public Guid VMId { get; set; }
        public Guid SCId { get; set; }
    }
}
