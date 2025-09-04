using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.API.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        public string Username { get; set; } = default!;
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = default!;
    }
}
