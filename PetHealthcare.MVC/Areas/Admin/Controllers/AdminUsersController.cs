using Microsoft.AspNetCore.Mvc;

namespace PetHealthcare.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
