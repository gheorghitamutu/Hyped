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
    public class DeleteVMHandler:IRequestHandler<DeleteVM>
    {
        private readonly DataContext context;

        public DeleteVMHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteVM request, CancellationToken cancellationToken)
        {
            var vm = context.VMs.SingleOrDefault(u => u.VMId == request.VMId);
            if (vm == null)
            {
                throw new Exception("This virtual machine doesn't exist!");
            }
            //delete this vm's resource folder first
            if (vm.RealID != null)
            {
                var this_vm_path = System.IO.Path.Combine("Resources", vm.RealID);

                if (System.IO.Directory.Exists(this_vm_path))
                {
                    System.IO.Directory.Delete(this_vm_path, true);
                }
            }
            
            //remove the virtual machine from the DB
            context.VMs.Remove(vm);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
