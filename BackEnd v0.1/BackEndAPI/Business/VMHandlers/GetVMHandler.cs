using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndAPI.Business.VMHandlers
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
            var vm = await context.VMs.SingleOrDefaultAsync(u => u.VMId == request.VMId);
            if (vm == null)
            {
                throw new Exception("Requested virtual machine doen't exist");
            }
            return vm;
        }
    }
}
