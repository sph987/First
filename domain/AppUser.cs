using Microsoft.AspNetCore.Identity;

namespace domain
{
    public class AppUser : IdentityUser 
    {
        public string DisplayName { get; set; }
        
    }
}