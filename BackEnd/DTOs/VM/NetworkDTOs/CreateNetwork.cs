using BackEnd.Data;
using MediatR;
using System;

namespace BackEnd.DTOs.VMDTOs.NetworkDTOs
{
    public class CreateNetwork:IRequest<Network>
    {
        public Guid VMId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
    }
}
