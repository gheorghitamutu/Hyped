using BackEndAPI.Data;
using BackEndAPI.DTOs.VMDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.Controllers
{
    [Route("api/vms")]
    [ApiController]
    public class VmsController:ControllerBase
    {
        private readonly IMediator mediator;

        public VmsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<VM>> Create([FromBody]CreateVM request)
        {
            var vm = await mediator.Send<VM>(request);
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

    }
}
