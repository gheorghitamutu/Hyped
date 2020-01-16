using BackEnd.Data;
using MediatR;
using System;

namespace BackEnd.DTOs.VMDTOs.NetworkDTOs
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
