using BackEndAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs.SCDTOs
{
    public class CreateSC:IRequest<SC>
    {
        public Guid VMId { get; set; }
    }
}
