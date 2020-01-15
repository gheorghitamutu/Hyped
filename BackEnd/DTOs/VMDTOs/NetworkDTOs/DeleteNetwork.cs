using MediatR;
using System;


namespace BackEndAPI.DTOs.VMDTOs.NetworkDTOs
{
    public class DeleteNetwork:IRequest
    {
        public Guid NetId { get; set; }
    }
}
