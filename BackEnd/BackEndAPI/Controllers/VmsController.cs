using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs;
using BackEndAPI.DTOs.VMDTOs.CDVDDTOs;
using BackEndAPI.DTOs.VMDTOs.NetworkDTOs;
using BackEndAPI.DTOs.VMDTOs.SCDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BackEndAPI.Controllers
{
    [Route("api/vms")]
    [ApiController]
    [Authorize]
    public class VmsController:ControllerBase
    {
        private readonly IMediator mediator;

        public VmsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        //VM
        [HttpPost]
        public async Task<ActionResult<VM>> Create([FromBody]CreateVM request)
        {
            var vm = await mediator.Send(request);
            return vm;
        }

        [HttpPut]
        public async Task<ActionResult<VM>> Update([FromBody]UpdateVM request)
        {
            var vm = await mediator.Send(request);
            return vm;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]DeleteVM request)
        {
            await mediator.Send(request);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VM>> Get(Guid id)
        {
            var vm = await mediator.Send(new GetVMDetail(id));
            if(vm==null)
            {
                return NotFound();
            }
            return Ok(vm);
        }

        [HttpGet]
        public async Task<ActionResult<VM>> Get()
        {
            var vms = await mediator.Send(new GetVMs());
            if(vms==null)
            {
                return NotFound();
            }
            return Ok(vms);
        }

        //VHD
        [HttpPost("vhd")]//api/vms/vhd
        public async Task<ActionResult<VHD>> CreateVHD([FromBody]CreateVHD request)
        {
                var vhd = await mediator.Send(request);
                return Ok(vhd);
        }

        [HttpGet("vhd/{id}")]//api/vms/vhd/id
        public async Task<ActionResult<VHD>> GetVHD(Guid id)
        {
            var vhd = await mediator.Send(new GetNetworkDetail(id));
            if (vhd == null)
            {
                return NotFound();
            }
            return Ok(vhd);
        }

        [HttpPut("vhd")]
        public async Task<ActionResult<VHD>> UpdateVHD([FromBody]UpdateVHD request)
        {
            var vhd = await mediator.Send(request);
            return vhd;
        }

        [HttpDelete("vhd")]
        public async Task<IActionResult> DeleteVHD([FromBody]DeleteVHD request)
        {
            await mediator.Send(request);
            return NoContent();
        }

        //CDDVD
        [HttpPost("cddvd")]//api/vms/cddvd
        public async Task<ActionResult<CDDVD>> CreateCDDVD([FromBody]CreateCDVD request)
        {
            var cddvd = await mediator.Send(request);
            return cddvd;
        }

        [HttpPost("cddvd/imgfile")]//api/vms/cddvd/imgfile
        public async Task<IActionResult> ApplyImageFileToCDDVD([FromBody]ApplyImageFile request)
        {
            var successful = await mediator.Send(request);
            if(successful==true)
            {
                return Ok();
            }
            
            return BadRequest();
        }

        [HttpPut("cddvd")]//api/vms/cddvd
        public async Task<ActionResult<CDDVD>> UpdateCDDVD([FromBody]UpdateCDVD request)
        {
            var cddvd = await mediator.Send(request);
            return cddvd;
        }

        [HttpGet("cddvd/{id}")]//api/vms/cddvd/id
        public async Task<ActionResult<CDDVD>> GetCDDVD(Guid id)
        {
            var cddvd = await mediator.Send(new GetCDVDDetail(id));
            if (cddvd == null)
            {
                return NotFound();
            }
            return Ok(cddvd);
        }

        [HttpDelete("cddvd")]//api/vms/cddvd
        public async Task<IActionResult> DeleteCDDVD([FromBody]DeleteCDVD request)
        {
            await mediator.Send(request);
            return NoContent();
        }

        //SCSIController
        [HttpPost("scsi")]//api/vms/scsi
        public async Task<ActionResult<SC>> CreateSCSIController([FromBody]CreateSC request)
        {
            var sc = await mediator.Send(request);
            return sc;
        }

        [HttpGet("scsi/{id}")]//api/vms/scsi/id
        public async Task<ActionResult<SC>> GetSCSIController(Guid id)
        {
            var sc = await mediator.Send(new GetSCDetail(id));
            if (sc== null)
            {
                return NotFound();
            }
            return Ok(sc);
        }


        [HttpDelete("scsi")]//api/vms/scsi
        public async Task<IActionResult> DeleteSCSIController([FromBody]DeleteSC request)
        {
            await mediator.Send(request);
            return NoContent();
        }

        //Network
        
        [HttpPost("network")]//api/vms/network
        public async Task<ActionResult<Network>> CreateNetwork([FromBody]CreateNetwork request)
        {
            var network = await mediator.Send(request);
            return network;
        }

        [HttpPut("network")]
        public async Task<ActionResult<Network>> UpdateNetwork([FromBody]UpdateNetwork request)
        {
            var network = await mediator.Send(request);
            return network;
        }

        [HttpGet("network/{id}")]//api/vms/network/id
        public async Task<ActionResult<Network>> GetNetwork(Guid id)
        {
            var network = await mediator.Send(new GetNetworkDetail(id));
            if (network == null)
            {
                return NotFound();
            }
            return Ok(network);
        }

        [HttpDelete("network")]
        public async Task<IActionResult> DeleteNetwork([FromBody]DeleteNetwork request)
        {
            await mediator.Send(request);
            return NoContent();
        }
        
    }
}
