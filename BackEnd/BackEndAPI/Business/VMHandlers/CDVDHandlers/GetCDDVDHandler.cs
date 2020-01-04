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
    public class GetCDDVDHandler : IRequestHandler<GetCDVDDetail, CDDVD>
    {
        private readonly DataContext context;

        public GetCDDVDHandler(DataContext context)
        {
            this.context = context;
        }
        public async Task<CDDVD> Handle(GetCDVDDetail request, CancellationToken cancellationToken)
        {
            var cddvd = await context.CDVDs.SingleOrDefaultAsync(c => c.CDDVDId == request.CDDVDId);
            if(cddvd==null)
            {
                throw new Exception("Requested CDDVD could not be found");
            }

            return cddvd;
        }
    }
}
