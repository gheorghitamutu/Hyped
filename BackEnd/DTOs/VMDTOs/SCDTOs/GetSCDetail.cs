using BackEndAPI.Data;
using MediatR;
using System;

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
