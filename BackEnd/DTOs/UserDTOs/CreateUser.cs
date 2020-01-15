using BackEndAPI.Data;
using MediatR;

namespace BackEndAPI.DTOs.UserDTOs
{
    public class CreateUser:IRequest<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        public string Rights { get; set; }
        public string Workplace { get; set; }
        public string PositionTitle { get; set; }
        public string ContactNumber { get; set; }
    }
}
