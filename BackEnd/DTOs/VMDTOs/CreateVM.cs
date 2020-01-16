using BackEnd.Data;
using MediatR;
using System;

namespace BackEnd.DTOs.VMDTOs
{
    public class CreateVM:IRequest<VM>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int RAM { get; set; }
        public int Cores { get; set; }
    }
}
