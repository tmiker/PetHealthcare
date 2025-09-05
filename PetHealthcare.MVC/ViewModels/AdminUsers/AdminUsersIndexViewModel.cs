using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.ViewModels.AdminUsers
{
    public class AdminUsersIndexViewModel
    {
        public List<UserInfoDTO> Users { get; set; } = new List<UserInfoDTO>();
    }
}
