using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs.CDVDDTOs
{
    public class DeleteCDVD:IRequest
    {
        public Guid VMId { get; set; }
        public Guid CDDVDId { get; set; }
    }
}
