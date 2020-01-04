using BackEndAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs.CDVDDTOs
{
    public class ApplyImageFile:IRequest<bool>
    {
        public Guid CDDVDId { get; set; }
        public string ImageFile { get; set; }
    }
}
