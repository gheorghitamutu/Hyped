using BackEndAPI.Data;
using MediatR;
using System;


namespace BackEndAPI.DTOs.VMDTOs.CDVDDTOs
{
    public class GetCDVDDetail:IRequest<CDDVD>
    {
        public GetCDVDDetail(Guid id)
        {
            CDDVDId = id;
        }
        public Guid CDDVDId { get;}
    }
}
