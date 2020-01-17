using BackEnd.Data;
using MediatR;
using System;


namespace BackEnd.DTOs.VMDTOs.CDVDDTOs
{
    public class UpdateCDVD:IRequest<CDDVD>
    {
        public Guid CDDVDId { get; set; }
        public string Name { get; set; }
        public Guid SCId { get; set; }
    }
}
