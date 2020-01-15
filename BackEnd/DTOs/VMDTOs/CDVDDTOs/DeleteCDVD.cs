using MediatR;
using System;


namespace BackEndAPI.DTOs.VMDTOs.CDVDDTOs
{
    public class DeleteCDVD:IRequest
    {
        public Guid CDDVDId { get; set; }
    }
}
