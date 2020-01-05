using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs.VHDDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndAPI.Business.VMHandlers.VHDHandlers
{
    public class GetVHDHandler:IRequestHandler<GetVHDDetail,VHD>
    {

        private readonly DataContext context;

        public GetVHDHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<VHD> Handle(GetVHDDetail request, CancellationToken cancellationToken)
        {
            var vhd = await context.VHDs.SingleOrDefaultAsync(v => v.VHDId == request.VHDId);
            if(vhd==null)
            {
                throw new Exception("Could not find the requested VHD");
            }

            return vhd;
        }
    }
}
