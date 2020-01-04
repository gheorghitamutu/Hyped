using BackEndAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
