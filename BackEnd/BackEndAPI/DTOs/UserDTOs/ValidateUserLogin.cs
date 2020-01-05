using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace BackEndAPI.DTOs.UserDTOs
{
    public class ValidateUserLogin:IRequest<SecurityToken>
    {
            public string Email { get; set; }
            public string Password { get; set; }
        
    }
}
