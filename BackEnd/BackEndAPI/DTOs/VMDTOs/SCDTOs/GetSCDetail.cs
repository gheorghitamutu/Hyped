using BackEndAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs.SCDTOs
{
    public class GetSCDetail:IRequest<SC>
    {
        public GetSCDetail(Guid id)
        {
            SCId = id;
        }
        public Guid SCId { get; }
    }
}
