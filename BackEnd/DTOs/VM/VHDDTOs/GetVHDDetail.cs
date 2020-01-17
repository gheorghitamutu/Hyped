using BackEnd.Data;
using MediatR;
using System;

namespace BackEnd.DTOs.VMDTOs.VHDDTOs
{
    public class GetVHDDetail:IRequest<VHD>
    {
        public GetVHDDetail(Guid id)
        {
            VHDId = id;
        }
        public Guid VHDId { get; }
    }
}
