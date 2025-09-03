namespace PetHealthcare.API.DTOs
{
    public class UserInfoDTO
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        //public List<string> Claims { get; set; } = new List<string>();
        public List<string> Roles { get; set; } = new List<string>();
        public bool IsLocked { get; set; }
        public bool IsAdmin { get; set; }
    }
}
