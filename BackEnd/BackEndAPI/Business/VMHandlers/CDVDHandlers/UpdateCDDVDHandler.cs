using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs.CDVDDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndAPI.Business.VMHandlers.CDVDHandlers
{
    public class UpdateCDDVDHandler : IRequestHandler<UpdateCDVD, CDDVD>
    {
        private readonly DataContext context;

        public UpdateCDDVDHandler(DataContext context)
        {
            this.context = context;
        }
        public async Task<CDDVD> Handle(UpdateCDVD request, CancellationToken cancellationToken)
        {
            var sc = await context.SCs.SingleOrDefaultAsync(u => u.SCId == request.SCId);
            if (sc == null)
            {
                throw new Exception("Requested SCSI Controller doesn't exist");
            }

            var cddvd = context.CDVDs.SingleOrDefault(c => c.CDDVDId == request.CDDVDId);
            if (cddvd == null)
            {
                throw new Exception("Requested CDDVD could not be found");
            }

            cddvd.Update(cddvd.InstanceId,request.Name, request.SCId);
            await context.SaveChangesAsync(cancellationToken);

            return cddvd;
        }
    }
}
