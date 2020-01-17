using MediatR;

namespace BackEnd.DTO.Users
{
    public class LoginValidationSingle:IRequest<string>
    {
            public string Email { get; set; }
            public string Password { get; set; }
        
    }
}
