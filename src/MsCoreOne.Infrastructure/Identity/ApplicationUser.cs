using Microsoft.AspNetCore.Identity;

namespace MsCoreOne.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
    }
}
