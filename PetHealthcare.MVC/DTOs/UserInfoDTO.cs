using System.ComponentModel;

namespace PetHealthcare.MVC.DTOs
{
    public class UserInfoDTO
    {
        public string? Id { get; set; }
        [DisplayName("User Name")]
        public string? UserName { get; set; }
        public string? Email { get; set; }

        //public List<string> Claims { get; set; } = new List<string>();
        public List<string> Roles { get; set; } = new List<string>();
        [DisplayName("Is Locked")]
        public bool IsLocked { get; set; }
        [DisplayName("is Admin")]
        public bool IsAdmin { get; set; }
    }
}
