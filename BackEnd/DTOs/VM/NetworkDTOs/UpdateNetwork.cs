using BackEnd.Data;
using MediatR;
using System;

namespace BackEnd.DTOs.VMDTOs.NetworkDTOs
{
    public class UpdateNetwork:IRequest<Network>
    {
        public Guid NetId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
    }
}
