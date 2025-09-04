using Microsoft.AspNetCore.Mvc;

namespace PetHealthcare.MVC.Areas.PetHealth.Controllers
{
    [Area("PetHealth")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
