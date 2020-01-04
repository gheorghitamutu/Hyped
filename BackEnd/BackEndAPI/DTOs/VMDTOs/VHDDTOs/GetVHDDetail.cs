using BackEndAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs.VHDDTOs
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
