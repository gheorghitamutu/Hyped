using BackEndAPI.DTOs.StorageDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.Controllers
{
    [Route("api/upload")]
    [ApiController]
    [Authorize]
    public class StorageController:ControllerBase
    {
        private readonly IMediator mediator;

        public StorageController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm]UploadFile request)
        {
            var successful=await mediator.Send(request);
            if (successful == true)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
