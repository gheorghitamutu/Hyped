using BackEnd.Data;
using BackEnd.DTOs.VMDTOs.SCDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Business.VMHandlers.SCHandlers
{
    public class GetSCHandler:IRequestHandler<GetSCDetail,SC>
    {
        private readonly DataContext context;

        public GetSCHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<SC> Handle(GetSCDetail request, CancellationToken cancellationToken)
        {
            var sc = await context.SCs.SingleOrDefaultAsync(s => s.SCId == request.SCId);
            if(sc==null)
            {
                throw new Exception("Requested SCSI Controller could not be found");
            }

            var vhds = await context.VHDs.ToListAsync();
            var cddvds = await context.CDVDs.ToListAsync();

            sc.Update(sc.Name, sc.InstanceId, vhds.Where((vhd) => vhd.SCId == sc.SCId).ToList(), cddvds.Where((cdvd) => cdvd.SCId == sc.SCId).ToList(),sc.VMId);
            await context.SaveChangesAsync(cancellationToken);

            return sc;
        }
    }
}
