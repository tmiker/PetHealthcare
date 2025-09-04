using Microsoft.AspNetCore.Mvc;
using PetHealthcare.MVC.ViewModels.AdminUsers;

namespace PetHealthcare.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUsersController : Controller
    {
        public IActionResult Index()
        {
            AdminUsersIndexViewModel model = new AdminUsersIndexViewModel() { WelcomeMessage = "Welcome to Admin Users Index Page!" };
            return View(model);
        }
    }
}
