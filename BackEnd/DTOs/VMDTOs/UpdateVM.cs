using BackEndAPI.Data;
using MediatR;
using System;

namespace BackEndAPI.DTOs.VMDTOs
{
    public class UpdateVM:IRequest<VM>
    {
        public Guid VMId { get; set; }
        public string Name { get; set; }
        public int RAM { get; set; }
        public int Cores { get; set; }
    }
}
