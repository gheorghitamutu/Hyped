using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs.NetworkDTOs;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndAPI.Business.VMHandlers.NetworkHandlers
{
    public class UpdateNetworkHandler:IRequestHandler<UpdateNetwork,Network>
    {
        private readonly DataContext context;

        public UpdateNetworkHandler(DataContext context)
        {
            this.context = context;
        }

        public async Task<Network> Handle(UpdateNetwork request, CancellationToken cancellationToken)
        {
            var network = context.Networks.SingleOrDefault(n => n.NetId == request.NetId);
            if(network==null)
            {
                throw new Exception("Requested network could not be found");
            }

            network.Update(request.Name, request.Notes, "instanceid",network.VMId);
            await context.SaveChangesAsync(cancellationToken);

            return network;
        }
    }
}
