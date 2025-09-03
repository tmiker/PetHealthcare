using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.API.DTOs
{
    public class LoginUserDTO
    {
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
