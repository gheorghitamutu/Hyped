using BackEndAPI.Data;
using MediatR;
using System;

namespace BackEndAPI.DTOs.VMDTOs
{
    public class UpdateVHD:IRequest<VHD>
    {
        public Guid VHDId { get; set; }
        public Guid SCId { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string Path { get; set; }
    }
}
