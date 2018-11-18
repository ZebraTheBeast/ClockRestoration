using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace ClockRestoration.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public ICollection<Order> Orders { get; set; }

        public UserRole Role { get; set; }
    }
}
