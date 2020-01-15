using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs.NetworkDTOs;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndAPI.Business.VMHandlers.NetworkHandlers
{
    public class DeleteNetworkHandler:IRequestHandler<DeleteNetwork>
    {
        private readonly DataContext context;

        public DeleteNetworkHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteNetwork request, CancellationToken cancellationToken)
        {
            var network = context.Networks.SingleOrDefault(n => n.NetId == request.NetId);
            if(network==null)
            {
                throw new Exception("Requested network could not be found");
            }

            context.Networks.Remove(network);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
