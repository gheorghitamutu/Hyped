using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs.NetworkDTOs
{
    public class DeleteNetwork:IRequest
    {
        public Guid NetId { get; set; }
    }
}
