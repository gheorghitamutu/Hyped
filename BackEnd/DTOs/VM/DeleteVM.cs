using MediatR;
using System;

namespace BackEnd.DTOs.VMDTOs
{
    public class DeleteVM:IRequest
    {
        public Guid VMId { get; set; }
    }
}
