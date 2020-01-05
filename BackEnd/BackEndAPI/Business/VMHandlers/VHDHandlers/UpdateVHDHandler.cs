using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndAPI.Business.VMHandlers.VHDHandlers
{
    public class UpdateVHDHandler : IRequestHandler<UpdateVHD, VHD>
    {

        private readonly DataContext context;

        public UpdateVHDHandler(DataContext context)
        {
            this.context = context;
        }
        public async Task<VHD> Handle(UpdateVHD request, CancellationToken cancellationToken)
        {
            var vhd = await context.VHDs.SingleOrDefaultAsync(v => v.VHDId == request.VHDId);
            if(vhd==null)
            {
                throw new Exception("Could not find the requested VHD");
            }

            vhd.Update(request.SCId, request.Name, vhd.InstanceId, request.Path, request.Size);
            await context.SaveChangesAsync(cancellationToken);

            return vhd;
        }
    }
}
