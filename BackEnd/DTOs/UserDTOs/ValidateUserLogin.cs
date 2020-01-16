using MediatR;

namespace BackEnd.DTOs.UserDTOs
{
    public class ValidateUserLogin:IRequest<string>
    {
            public string Email { get; set; }
            public string Password { get; set; }
        
    }
}
