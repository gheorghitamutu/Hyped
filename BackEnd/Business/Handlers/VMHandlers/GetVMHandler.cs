using BackEnd.Data;
using BackEnd.DTOs.VMDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Business.VMHandlers
{
    public class GetVMHandler:IRequestHandler<GetVMDetail,VM>
    {
        private readonly DataContext context;

        public GetVMHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<VM> Handle(GetVMDetail request, CancellationToken cancellationToken)
        {
            var reqVm = await context.VMs.SingleOrDefaultAsync(u => u.VMId == request.VMId);
            if (reqVm == null)
            {
                throw new Exception("Requested virtual machine doesn't exist");
            }

            var networks = await context.Networks.ToListAsync();
            var scs = await context.SCs.ToListAsync();
            var vhds = await context.VHDs.ToListAsync();
            var cdvds = await context.CDVDs.ToListAsync();

            var reqVM_networks = networks.Where((n) => n.VMId == reqVm.VMId).ToList();
            var reqVM_scs = scs.Where((s) => s.VMId == reqVm.VMId).ToList();
            foreach (var sc in reqVM_scs)
            {
                var sc_vhds = vhds.Where((v) => v.SCId == sc.SCId).ToList();
                var sc_cdvds = cdvds.Where((v) => v.SCId == sc.SCId).ToList();
                sc.Update(sc.Name, sc.InstanceId, sc_vhds, sc_cdvds, sc.VMId);
                await context.SaveChangesAsync(cancellationToken);
            }
            

            reqVm.Update(reqVm.RealID, reqVm.Name, reqVm.Configuration, reqVm.LastSave, reqVM_networks, reqVm.RAM, reqVm.Cores,reqVM_scs);
            await context.SaveChangesAsync(cancellationToken);

            return reqVm;   
        }

    }
}
