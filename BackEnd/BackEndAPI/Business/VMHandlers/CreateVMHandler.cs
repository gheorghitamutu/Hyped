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
    public class CreateVMHandler:IRequestHandler<CreateVM, VM>
    {
        private readonly DataContext context;

        public CreateVMHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<VM> Handle(CreateVM request, CancellationToken cancellationToken)
        {
            var vm = VM.Create(request.RealID,request.Name, request.Configuration, request.LastSave);
            context.VMs.Add(vm);
            await context.SaveChangesAsync(cancellationToken);
            return vm;
        }
    }
}
