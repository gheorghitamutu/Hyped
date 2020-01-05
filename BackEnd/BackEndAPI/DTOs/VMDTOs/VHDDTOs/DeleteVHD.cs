using MediatR;
using System;

namespace BackEndAPI.DTOs.VMDTOs
{
    public class DeleteVHD:IRequest
    {
        public Guid VHDId { get; set; }
    }
}
