using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.MVC.DTOs
{
    public class UpdatePasswordDTO
    {
        public string? Email { get; set; }

        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }

        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        public string? ConfirmNewPassword { get; set; }
    }
}
