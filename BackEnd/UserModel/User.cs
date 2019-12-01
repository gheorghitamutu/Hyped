using System;
using System.Collections.Generic;

namespace Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName {get;set;}
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        public string Rights { get; set; }

        public virtual ICollection<VM> VMS { get; set; }

    }
}
