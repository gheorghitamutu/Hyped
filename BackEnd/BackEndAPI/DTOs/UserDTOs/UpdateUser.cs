using BackEndAPI.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.DTOs.UserDTOs
{
    public class UpdateUser:IRequest<User>
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        public string Rights { get; set; }
        public string Workplace { get; set; }
        public string PositionTitle { get; set; }
        public string ContactNumber { get; set; }

        public virtual ICollection<VM> VMS { get; set; }

    }
}
