using BackEnd.Data;
using MediatR;
using System;


namespace BackEnd.DTOs.VMDTOs
{
    public class CreateCDVD:IRequest<CDDVD>
    {
        public Guid SCId { get; set; }
    }
}
