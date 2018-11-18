using System;
using System.Collections.Generic;

using Microsoft.AspNet.Identity.EntityFramework;

namespace Library.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
