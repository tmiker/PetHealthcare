using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.API.DTOs
{
    public class UpdatePasswordDTO
    {
        [Required]
        public string? Email { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        public string? ConfirmNewPassword { get; set; } = default!;
    }
}
