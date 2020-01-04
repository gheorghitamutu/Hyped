using BackEndAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs
{
    public class CreateVHD:IRequest<VHD>
    {
        public Guid SCId { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string Path { get; set; }
    }
}
