using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.MVC.DTOs
{
    public class LoginUserDTO
    {
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
