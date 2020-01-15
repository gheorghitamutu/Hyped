using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs.NetworkDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndAPI.Business.VMHandlers.NetworkHandlers
{
    public class GetNetworkHandler : IRequestHandler<GetNetworkDetail, Network>
    {
        private readonly DataContext context;
        public GetNetworkHandler(DataContext context)
        {
            this.context = context;
        }
        public async Task<Network> Handle(GetNetworkDetail request, CancellationToken cancellationToken)
        {
            var network = await context.Networks.SingleOrDefaultAsync(n => n.NetId == request.NetId);
            /*if(network==null)
            {
                throw new Exception("Requested network could not be found");
            }*/

            return network;
        }
    }
}
