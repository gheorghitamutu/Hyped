using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndAPI.Business.VMHandlers
{
    public class UpdateVMHandler:IRequestHandler<UpdateVM,VM>
    {
        private readonly DataContext context;

        public UpdateVMHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<VM> Handle(UpdateVM request, CancellationToken cancellationToken)
        {
            var vm = context.VMs.SingleOrDefault(u => u.VMId == request.VMId);
            if (vm == null)
            {
                throw new Exception("Requested virtual machine doesn't exist");
            }

            var networks = await context.Networks.ToListAsync();
            var scs = await context.SCs.ToListAsync();
            var vhds = await context.VHDs.ToListAsync();
            var cdvds = await context.CDVDs.ToListAsync();

            var vm_networks = networks.Where((n) => n.VMId == vm.VMId).ToList();
            var vm_scs = scs.Where((s) => s.VMId == vm.VMId).ToList();
            foreach (var sc in vm_scs)
            {
                var sc_vhds = vhds.Where((v) => v.SCId == sc.SCId).ToList();
                var sc_cdvds = cdvds.Where((v) => v.SCId == sc.SCId).ToList();
                sc.Update(sc.Name, sc.InstanceId, sc_vhds, sc_cdvds, sc.VMId);
                await context.SaveChangesAsync(cancellationToken);
            }


            vm.Update(request.RealID, request.Name, request.Configuration, request.LastSave,networks.Where((n)=>n.VMId==vm.VMId).ToList(),request.RAM,request.Cores,request.Threads,request.Processors,scs.Where((s)=>s.VMId==vm.VMId).ToList());
            await context.SaveChangesAsync(cancellationToken);
            return vm;
        }
    }
}
