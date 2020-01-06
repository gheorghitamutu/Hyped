using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEndAPI.Data
{
    public class User
    {
        [Key]
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Country { get; private set; }
        public string Password { get; private set; }
        public string Rights { get; private set; }
        public string Workplace { get; private set; }
        public string PositionTitle { get; private set; }
        public string ContactNumber { get; private set; }

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
