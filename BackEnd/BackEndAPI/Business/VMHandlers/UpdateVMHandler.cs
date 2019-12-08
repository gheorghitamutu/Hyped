using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs;
using MediatR;
using System;
using System.Collections.Generic;
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
            vm.Update(request.RealID, request.Name, request.Configuration, request.LastSave);
            await context.SaveChangesAsync(cancellationToken);
            return vm;
        }
    }
}
