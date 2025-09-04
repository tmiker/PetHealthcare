using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.MVC.DTOs
{
    public class DeleteAccountDTO
    {
        public string? Email { get; set; }

        public string? UserName { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
