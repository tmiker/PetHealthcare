using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.DTOs;
using PetHealthcare.MVC.Paging;
using PetHealthcare.MVC.ViewModels.CarouselImages;
using System.Text;

namespace PetHealthcare.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarouselImagesController : Controller
    {
        private readonly ICarouselImagesHttpProvider _carouselImageHttpProvider;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly int PageSize = 6;

        public CarouselImagesController(ICarouselImagesHttpProvider carouselImageHttpProvider, IWebHostEnvironment webHostEnvironment)
        {
            _carouselImageHttpProvider = carouselImageHttpProvider;
            _webHostEnvironment = webHostEnvironment;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int productPage = 1, string? searchSubject = null, string? searchCaption = null, string? searchImageURL = null)
        {
            CarouselImageIndexViewModel viewModel = new CarouselImageIndexViewModel();

            if (User?.Identity! is null|| !User.Identity.IsAuthenticated)
            {
                viewModel.Message = $"User is not logged in.  Returning empty view model.";
                return View(viewModel);
            }

            var result = await _carouselImageHttpProvider.GetAllCarouselImagesAsync(HttpContext.Session.GetString("JwtToken")!);
            
            if (result.Images == null)
            {
                //ModelState.AddModelError(string.Empty, "Could not find any images.");
                viewModel.CarouselImages = new List<CarouselImageDTO>();
            }
            else
            {
                var ImageList = result.Images!.ToList();
                // search box
                StringBuilder param = new StringBuilder();
                param.Append("/Admin/CarouselImage/Index?productPage=:");
                param.Append("&searchName=");
                if (searchSubject != null)
                {
                    param.Append(searchSubject);
                }
                param.Append("&searchBreed=");
                if (searchCaption != null)
                {
                    param.Append(searchCaption);
                }
                param.Append("&searchChipNumber=");
                if (searchImageURL != null)
                {
                    param.Append(searchImageURL);
                }

                if (searchSubject != null)
                {
                    viewModel.CarouselImages = ImageList.Where(p => p.Subject!.ToLower().Contains(searchSubject.ToLower())).ToList();
                }
                else
                {
                    if (searchCaption != null)
                    {
                        viewModel.CarouselImages = ImageList.Where(p => p.Caption!.ToLower().Contains(searchCaption.ToLower())).ToList();
                    }
                    else
                    {
                        if (searchImageURL != null)
                        {
                            viewModel.CarouselImages = ImageList.Where(p => p.ImageURL!.ToLower().Contains(searchImageURL.ToLower())).ToList();
                        }
                        else
                        {
                            viewModel.CarouselImages = ImageList;
                        }
                    }
                }
                // pagination
                var count = viewModel.CarouselImages.Count();
                viewModel.CarouselImages = viewModel.CarouselImages
                    .OrderBy(c => c.Subject)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();
                // set paging info
                viewModel.PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = count,
                    urlParam = param.ToString()
                };
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _carouselImageHttpProvider.GetCarouselImageAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                TempData["alertError"] = "Error retrieving the image.";
                return RedirectToAction(nameof(Index));
            }

            CarouselImageDetailViewModel viewModel = new CarouselImageDetailViewModel()
            {
                CarouselImage = result.Image
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            CarouselImageUpsertViewModel viewModel = new CarouselImageUpsertViewModel();

            if (id == null)
            {
                viewModel.CarouselImage = new CarouselImageDTO();
                return View(viewModel);
            }
            else
            {
                var result = await _carouselImageHttpProvider.GetCarouselImageAsync(id!.Value, HttpContext.Session.GetString("JwtToken")!);
                if (!result.IsSuccess)
                {
                    if (result.ErrorMessage != null) ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    else ModelState.AddModelError(string.Empty, $"Unknown error getting image with id of {id}.");
                    return View(viewModel);
                }
                viewModel.CarouselImage = result.Image;
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CarouselImageUpsertViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // get image from form and copy to temp directory in wwwroot
                var files = HttpContext.Request.Form.Files;

                if (viewModel.CarouselImage!.Id == 0)
                {
                    if (files.Count() > 0)
                    {
                        viewModel.CarouselImage!.Image = files[0];
                    }
                    var result = await _carouselImageHttpProvider.AddCarouselImageAsync(viewModel.CarouselImage, HttpContext.Session.GetString("JwtToken")!);
                    if (!result.IsSuccess)
                    {
                        if (result.ErrorMessage != null) ModelState.AddModelError(string.Empty, result.ErrorMessage);
                        else ModelState.AddModelError(string.Empty, $"Unknown error creating the image.");
                        return View(viewModel);
                    }
                    TempData["alertSuccess"] = "Image successfully created.";
                }
                else
                {
                    if (files.Count() > 0)
                    {
                        viewModel.CarouselImage!.Image = files[0];
                    }
                    var result = await _carouselImageHttpProvider.EditCarouselImageAsync(viewModel.CarouselImage.Id, viewModel.CarouselImage, HttpContext.Session.GetString("JwtToken")!);
                    if (!result.IsSuccess)
                    {
                        if (result.ErrorMessage != null) ModelState.AddModelError(string.Empty, result.ErrorMessage);
                        else ModelState.AddModelError(string.Empty, $"Unknown error editing the image.");
                        return View(viewModel);
                    }
                    TempData["alertSuccess"] = "Image successfully updated.";
                }
                return RedirectToAction(nameof(Index));
            }
            else return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _carouselImageHttpProvider.DeleteCarouselImageAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage != null) TempData["alertError"] = result.ErrorMessage;
                else TempData["alertError"] = "Error deleting the image.";
            }
            else
            {
                TempData["alertSuccess"] = "Image successfully deleted.";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ShowDeleteConfirmModal(int id)
        {
            var result = await _carouselImageHttpProvider.GetCarouselImageAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage != null) TempData["alertError"] = result.ErrorMessage;
                else TempData["alertError"] = "Error retrieving the object.";
                return RedirectToAction(nameof(Index));
            }
            CarouselImageDeleteViewModel viewModel = new CarouselImageDeleteViewModel() { CarouselImage = result.Image };
            return PartialView("_ConfirmDeleteCarouselImagePartial", viewModel);
        }
    }
}
