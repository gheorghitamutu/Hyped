using BackEndAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.VMDTOs
{
    public class GetVMDetail:IRequest<VM>
    {
        public GetVMDetail(Guid id)
        {
            VMId = id;
        }
        public Guid VMId { get; }
    }
}
