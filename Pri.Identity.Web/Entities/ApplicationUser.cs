using Microsoft.AspNetCore.Identity;
using System;

namespace Pri.Identity.Web.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
