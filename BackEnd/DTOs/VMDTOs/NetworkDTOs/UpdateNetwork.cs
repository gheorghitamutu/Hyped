using BackEndAPI.Data;
using MediatR;
using System;

namespace BackEndAPI.DTOs.VMDTOs.NetworkDTOs
{
    public class UpdateNetwork:IRequest<Network>
    {
        public Guid NetId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
    }
}
