﻿using MediatR;
using System;

namespace BackEnd.DTOs.VMDTOs.CDVDDTOs
{
    public class ApplyImageFile:IRequest<bool>
    {
        public Guid CDDVDId { get; set; }
        public string ImageFile { get; set; }
    }
}
