using BackEndAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs
{
    public class CreateCDVD:IRequest<CDDVD>
    {
        public Guid VMId { get; set; }
        public Guid SCId { get; set; }
    }
}
