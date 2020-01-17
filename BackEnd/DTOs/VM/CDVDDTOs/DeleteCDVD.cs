using MediatR;
using System;


namespace BackEnd.DTOs.VMDTOs.CDVDDTOs
{
    public class DeleteCDVD:IRequest
    {
        public Guid CDDVDId { get; set; }
    }
}
