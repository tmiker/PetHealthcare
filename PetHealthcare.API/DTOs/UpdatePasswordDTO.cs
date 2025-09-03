using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.API.DTOs
{
    public class UpdatePasswordDTO
    {
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
        [DataType(DataType.Password)]
        public string? ConfirmNewPassword { get; set; }
    }
}
