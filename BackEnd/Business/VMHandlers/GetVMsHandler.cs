using BackEnd.Data;
using BackEnd.DTOs.VMDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Business.VMHandlers
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
            var networks = await context.Networks.ToListAsync();
            var scs = await context.SCs.ToListAsync();
            var vhds = await context.VHDs.ToListAsync();
            var cdvds = await context.CDVDs.ToListAsync();

            foreach (var vm in vms)
            {
                
                var vm_networks = networks.Where((n) => n.VMId == vm.VMId).ToList();
                var vm_scs = scs.Where((s) => s.VMId == vm.VMId).ToList();
                foreach (var sc in vm_scs)
                {
                    var sc_vhds = vhds.Where((v) => v.SCId == sc.SCId).ToList();
                    var sc_cdvds = cdvds.Where((v) => v.SCId == sc.SCId).ToList();
                    sc.Update(sc.Name, sc.InstanceId, sc_vhds, sc_cdvds, sc.VMId);
                    await context.SaveChangesAsync(cancellationToken);
                }                

                vm.Update(vm.RealID, vm.Name, vm.Configuration, vm.LastSave, vm_networks, vm.RAM, vm.Cores,vm_scs);
                await context.SaveChangesAsync(cancellationToken);
            }
            return vms;
        }
    }
}
