using Microsoft.AspNetCore.Identity;

namespace PetHealthcare.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        // public string? Username { get; set; }
        public int CustomerNumber { get; set; }
        public string? RefreshToken { get; set; }
    }
}
