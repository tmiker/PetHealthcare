using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.API.DTOs
{
    public class RegisterUserDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
