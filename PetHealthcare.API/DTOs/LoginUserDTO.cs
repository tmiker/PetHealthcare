using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.API.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}
