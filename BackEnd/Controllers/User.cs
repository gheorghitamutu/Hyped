using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BackEnd.DTO.Users;

namespace BackEnd.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class User : ControllerBase
    {
        private readonly IMediator mediator;

        public User(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Data.User>> Create([FromBody]RegisterSingle request)
        {
            var user = await mediator.Send(request);
            return user;
        }

        [HttpPut]
        public async Task<ActionResult<Data.User>> Update([FromBody]UpdateSingle request)
        {
            var user = await mediator.Send(request);
            return user;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]DeleteSingle request)
        {
            await mediator.Send(request);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Data.User>> Get(Guid id)
        {
            var user = await mediator.Send(new GetSingle(id));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Data.User>> Get()
        {
            var users = await mediator.Send(new GetMultiple());
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<Data.User>> Get([FromBody]LoginValidationSingle request)
        {
            var tokenString = await mediator.Send(request);
            if(tokenString==null)
            {
                return BadRequest();
            }
            return Ok(new { token=tokenString });
        }
         
    }
}
