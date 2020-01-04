using BackEndAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs.NetworkDTOs
{
    public class GetNetworkDetail:IRequest<Network>
    {
        public GetNetworkDetail(Guid id)
        {
            NetId = id;
        }
        public Guid NetId { get; }
    }
}
