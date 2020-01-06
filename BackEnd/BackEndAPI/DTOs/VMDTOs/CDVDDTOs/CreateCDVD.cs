using BackEndAPI.Data;
using MediatR;
using System;


namespace BackEndAPI.DTOs.VMDTOs
{
    public class CreateCDVD:IRequest<CDDVD>
    {
        public Guid SCId { get; set; }
    }
}
