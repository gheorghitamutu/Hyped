using BackEnd.Data;
using BackEnd.DTOs.VMDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using Viridian.Root.Virtualization.v2.Msvm.VirtualSystem;
using ViridianTester;

namespace BackEnd.Business.VMHandlers
{
    public class StopVMHandler : IRequestHandler<StopVM, VM>
    {
        private readonly DataContext context;

        public StopVMHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<VM> Handle(StopVM request, CancellationToken cancellationToken)
        {
            var vm = await context.VMs.SingleOrDefaultAsync(u => u.VMId == request.VMId);
            if (vm == null)
            {
                throw new Exception("Requested virtual machine doesn't exist");
            }

            var viridianUtils = new ViridianUtils();
            var computerSystem =
                ComputerSystem.GetInstances()
                    .Cast<ComputerSystem>()
                    .Where((cs) => cs.Name == vm.RealID)
                    .ToList()
                    .First();

            var ReturnValue = computerSystem.RequestStateChange(3, null, out ManagementPath Job);

            ViridianUtils.WaitForConcreteJobToEnd(Job);

            return vm;
        }
    }
}
