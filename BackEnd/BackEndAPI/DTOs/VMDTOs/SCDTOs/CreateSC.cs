using BackEndAPI.Data;
using MediatR;
using System;

namespace BackEndAPI.DTOs.VMDTOs.SCDTOs
{
    public class CreateSC:IRequest<SC>
    {
        public Guid VMId { get; set; }
    }
}
