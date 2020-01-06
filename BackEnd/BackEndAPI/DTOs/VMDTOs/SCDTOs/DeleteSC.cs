using MediatR;
using System;

namespace BackEndAPI.DTOs.VMDTOs.SCDTOs
{
    public class DeleteSC:IRequest
    {
        public Guid SCId { get; set; }
    }
}
