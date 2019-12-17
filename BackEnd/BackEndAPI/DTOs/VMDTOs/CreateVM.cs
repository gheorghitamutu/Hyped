using BackEndAPI.Data;
using MediatR;
using System;

namespace BackEndAPI.DTOs.VMDTOs
{
    public class CreateVM:IRequest<VM>,IRequest<User>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
