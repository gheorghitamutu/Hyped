using MediatR;
using System;

namespace BackEnd.DTOs.VMDTOs
{
    public class DeleteVHD:IRequest
    {
        public Guid VHDId { get; set; }
    }
}
