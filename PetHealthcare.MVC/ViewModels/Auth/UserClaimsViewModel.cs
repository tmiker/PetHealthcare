using System.ComponentModel;

namespace PetHealthcare.MVC.ViewModels.Auth
{
    public class UserClaimsViewModel
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        [DisplayName("Customer Number")]
        public string? CustomerNumber { get; set; }
        public List<string>? Roles { get; set; } = new List<string>();
        [DisplayName("JWT Token")]
        public string? JwtToken { get; set; }
        [DisplayName("Refresh Token")]
        public string? RefreshToken { get; set; }
    }
}
