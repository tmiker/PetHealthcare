using System.ComponentModel;

namespace PetHealthcare.MVC.DTOs
{
    public class TokenStatusDTO
    {
        [DisplayName("Current UTC Time")]
        public DateTime CurrentUtcTime { get; set; }
        [DisplayName("Valid From")]
        public string? ValidFrom { get; set; }
        [DisplayName("Valid To")]
        public string? ValidTo { get; set; }
        [DisplayName("Issued At")]
        public string? IssuedAt { get; set; }
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
