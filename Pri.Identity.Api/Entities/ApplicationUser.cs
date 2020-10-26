using Microsoft.AspNetCore.Identity;

namespace Pri.Identity.Api.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}
