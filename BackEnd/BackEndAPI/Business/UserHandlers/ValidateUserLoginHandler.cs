using BackEndAPI.Data;
using BackEndAPI.DTOs.UserDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackEndAPI.Business.UserHandlers
{
    public class ValidateUserLoginHandler:IRequestHandler<ValidateUserLogin,string>
    {
        
            private readonly DataContext context;
            private IConfiguration _config;

            public ValidateUserLoginHandler(DataContext context,IConfiguration config)
            {
                this.context = context;
                _config = config;
            }

            public async Task<string> Handle(ValidateUserLogin request, CancellationToken cancellationToken)
            {
                var user = await context.Users.SingleOrDefaultAsync(u => (u.Email == request.Email && u.Password == request.Password));
                if (user == null)
                {
                    throw new Exception("No such user with these email and password!");
                }
           
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("User Id",user.UserId.ToString())
            };
            var token = new JwtSecurityToken(null,null,claims, expires: DateTime.Now.AddDays(7), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
            }
        
    }
}
