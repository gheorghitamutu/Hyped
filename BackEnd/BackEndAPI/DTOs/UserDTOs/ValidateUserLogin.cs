using BackEndAPI.Data;
using MediatR;

namespace BackEndAPI.DTOs.UserDTOs
{
    public class ValidateUserLogin:IRequest<User>
    {
            public string Email { get; set; }
            public string Password { get; set; }
        
    }
}
