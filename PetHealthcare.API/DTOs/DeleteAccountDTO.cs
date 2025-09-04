using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.API.DTOs
{
    public class DeleteAccountDTO
    {
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        public string UserName { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
    }
}
