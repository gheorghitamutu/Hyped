using MediatR;
using System;

namespace BackEndAPI.DTOs.VMDTOs
{
    public class DeleteVM:IRequest
    {
        public Guid VMId { get; set; }
    }
}
