using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace BackEnd.DTOs.StorageDTOs
{
    public class UploadFile:IRequest<bool>
    {
        public IFormFile FileToUpload { get; set; }
        public Guid VMId { get; set; }
    }
}
