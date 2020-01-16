using BackEnd.Data;
using BackEnd.DTOs.UserDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Create([FromBody]CreateUser request)
        {
            var user = await mediator.Send(request);
            return user;
        }

        [HttpPut]
        public async Task<ActionResult<User>> Update([FromBody]UpdateUser request)
        {
            var user = await mediator.Send(request);
            return user;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]DeleteUser request)
        {
            await mediator.Send(request);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(Guid id)
        {
            var user = await mediator.Send(new GetUserDetail(id));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Get()
        {
            var users = await mediator.Send(new GetUsers());
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Get([FromBody]ValidateUserLogin request)
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
