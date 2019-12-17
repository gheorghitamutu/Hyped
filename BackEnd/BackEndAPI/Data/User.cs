using System;
using System.Collections.Generic;

namespace BackEndAPI.Data
{
    public class User
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

        public static User Create(string firstname, string lastname, string email, string country, string password, string rights, string workplace, string positiontitle, string contactnumber)
        {
            return new User
            {
                UserId = Guid.NewGuid(),
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                Country = country,
                Password = password,
                Rights = rights,
                Workplace = workplace,
                PositionTitle = positiontitle,
                ContactNumber = contactnumber
            };
        }


        public void Update(string firstname, string lastname, string email, string country, string password, string rights, string workplace, string positiontitle, string contactnumber, ICollection<VM> vms)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Country = country;
            Password = password;
            Rights = rights;
            Workplace = workplace;
            PositionTitle = positiontitle;
            ContactNumber = contactnumber;
            VMS = vms;
        }
    }
}
