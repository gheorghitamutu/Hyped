﻿using MediatR;
using System;

namespace BackEnd.DTOs.VMDTOs.SCDTOs
{
    public class DeleteSC:IRequest
    {
        public Guid SCId { get; set; }
    }
}