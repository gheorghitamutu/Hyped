using MediatR;
using System;


namespace BackEnd.DTOs.VMDTOs.NetworkDTOs
{
    public class DeleteNetwork:IRequest
    {
        public Guid NetId { get; set; }
    }
}
