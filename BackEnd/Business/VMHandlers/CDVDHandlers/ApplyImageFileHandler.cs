using BackEnd.Data;
using BackEnd.DTOs.VMDTOs.CDVDDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Business.VMHandlers.CDVDHandlers
{
    public class ApplyImageFileHandler : IRequestHandler<ApplyImageFile,bool>
    {
        private readonly DataContext context;

        public ApplyImageFileHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(ApplyImageFile request, CancellationToken cancellationToken)
        {
            var cddvd = await context.CDVDs.SingleOrDefaultAsync(c => c.CDDVDId == request.CDDVDId);
            if(cddvd==null)
            {
                throw new Exception("Requested CDDVD could not be found");
            }
            //TODO: check if ImageFile exists in resource folder and return true; else false; 

            return true;
        }
    }
}
