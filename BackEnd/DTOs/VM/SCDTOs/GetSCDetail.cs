using BackEnd.Data;
using MediatR;
using System;

namespace BackEnd.DTOs.VMDTOs.SCDTOs
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
