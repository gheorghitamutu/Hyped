﻿using BackEnd.Data;
using MediatR;
using System;

namespace BackEnd.DTOs.VMDTOs
{
    public class GetVMDetail:IRequest<VM>
    {
        public GetVMDetail(Guid id)
        {
            VMId = id;
        }
        public Guid VMId { get; }
    }
}