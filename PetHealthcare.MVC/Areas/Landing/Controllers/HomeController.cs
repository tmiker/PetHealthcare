using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.Models;
using PetHealthcare.MVC.ViewModels.Home;
using System.Diagnostics;

namespace PetHealthcare.MVC.Areas.Landing.Controllers
{
    [Area("Landing")]
    public class HomeController : Controller
    {
        private readonly ICarouselImagesHttpProvider _carouselImageHttpProvider;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ICarouselImagesHttpProvider carouselImageHttpProvider, ILogger<HomeController> logger)
        {
            _carouselImageHttpProvider = carouselImageHttpProvider;
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var result = await _carouselImageHttpProvider.GetAllCarouselImagesAsync();

            HomeIndexViewModel viewModel = new HomeIndexViewModel();

            if (result.Images != null) viewModel.CarouselImages = result.Images.ToList()!;

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
