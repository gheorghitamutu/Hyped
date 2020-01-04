using BackEndAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs.NetworkDTOs
{
    public class CreateNetwork:IRequest<Network>
    {
        public Guid VMId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
    }
}
