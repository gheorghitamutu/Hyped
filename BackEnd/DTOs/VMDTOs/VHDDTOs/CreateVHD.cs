using BackEnd.Data;
using MediatR;
using System;

namespace BackEnd.DTOs.VMDTOs
{
    public class CreateVHD:IRequest<VHD>
    {
        public Guid SCId { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
    }
}
