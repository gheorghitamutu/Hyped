using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.Data;

namespace BackEnd.Business.Handlers.Users
{
    public class LoginValidationSingle:IRequestHandler<DTO.Users.LoginValidationSingle, string>
    {
        
            private readonly DataContext context;
            private IConfiguration _config;

            public LoginValidationSingle(DataContext context,IConfiguration config)
            {
                this.context = context;
                _config = config;
            }

            public async Task<string> Handle(DTO.Users.LoginValidationSingle request, CancellationToken cancellationToken)
            {
                var user = await context.Users.SingleOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);
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
