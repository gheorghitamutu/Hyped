using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndAPI.Business.VMHandlers
{
    public class GetVMsHandler: IRequestHandler<GetVMs, List<VM>>
    {
        private readonly DataContext context;

        public GetVMsHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<VM>> Handle(GetVMs request, CancellationToken cancellationToken)
        {
            var vms = await context.VMs.ToListAsync();
            return vms;
        }
    }
}
