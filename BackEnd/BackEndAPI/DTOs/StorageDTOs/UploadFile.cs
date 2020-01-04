using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.StorageDTOs
{
    public class UploadFile:IRequest<bool>
    {
        public IFormFile FileToUpload { get; set; }
        public Guid VMId { get; set; }
    }
}
