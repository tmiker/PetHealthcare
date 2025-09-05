using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.DTOs;
using PetHealthcare.MVC.Paging;
using PetHealthcare.MVC.Utility;
using PetHealthcare.MVC.ViewModels.Pet;
using System.Text;

namespace PetHealthcare.MVC.Areas.PetHealth.Controllers
{
    [Authorize]
    [Area("PetHealth")]
    public class PetController : Controller
    {
        private readonly IPetHttpProvider _petHttpProvider;
        private readonly IHealthSummaryService _healthSummaryService;

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly int PageSize = 6;

        public PetController(IPetHttpProvider petHttpProvider, IHealthSummaryService healthSummaryService, IWebHostEnvironment webHostEnvironment)
        {
            _petHttpProvider = petHttpProvider;
            _healthSummaryService = healthSummaryService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(int productPage = 1, string? searchName = null, string? searchBreed = null, string? searchChipNumber = null)
        {
            var result = await _petHttpProvider.GetAllPetsAsync(HttpContext.Session.GetString("JwtToken")!);
            PetIndexViewModel viewModel = new PetIndexViewModel();

            if (result.Pets == null)
            {
                // ModelState.AddModelError(string.Empty, "Could not find any pets.");
                viewModel.Pets = new List<PetDTO>();
            }
            else
            {
                var PetList = result.Pets!.ToList();
                // search box
                StringBuilder param = new StringBuilder();
                param.Append("/Customer/Pet/Index?productPage=:");
                param.Append("&searchName=");
                if (searchName != null)
                {
                    param.Append(searchName);
                }
                param.Append("&searchBreed=");
                if (searchBreed != null)
                {
                    param.Append(searchBreed);
                }
                param.Append("&searchChipNumber=");
                if (searchChipNumber != null)
                {
                    param.Append(searchChipNumber);
                }

                if (searchName != null)
                {
                    viewModel.Pets = PetList.Where(p => p.Name!.ToLower().Contains(searchName.ToLower())).ToList();
                }
                else
                {
                    if (searchBreed != null)
                    {
                        viewModel.Pets = PetList.Where(p => p.Breed!.ToLower().Contains(searchBreed.ToLower())).ToList();
                    }
                    else
                    {
                        if (searchChipNumber != null)
                        {
                            viewModel.Pets = PetList.Where(p => p.ChipNumber!.ToLower().Contains(searchChipNumber.ToLower())).ToList();
                        }
                        else
                        {
                            viewModel.Pets = PetList;
                        }
                    }
                }
                // pagination
                var count = viewModel.Pets.Count();
                viewModel.Pets = viewModel.Pets
                    .OrderBy(c => c.Name)
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

        public async Task<IActionResult> HealthSummary(int id)
        {
            HealthSummaryDTO healthSummary = await _healthSummaryService.GetPetHealthSummaryAsync(id, HttpContext.Session.GetString("JwtToken")!);

            HealthSummaryViewModel viewModel = new HealthSummaryViewModel()
            {
                HealthSummaryDTO = healthSummary
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _petHttpProvider.GetPetAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                TempData["alertError"] = "Error retrieving the pet.";
                return RedirectToAction(nameof(Index));
            }

            PetDetailViewModel viewModel = new PetDetailViewModel()
            {
                Pet = result.Pet
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            PetUpsertViewModel viewModel = new PetUpsertViewModel() { GenderList = StaticDetails.GenderList };

            if (id == null)
            {
                viewModel.Pet = new PetDTO();
                return View(viewModel);
            }
            else
            {
                var result = await _petHttpProvider.GetPetAsync(id!.Value, HttpContext.Session.GetString("JwtToken")!);
                if (!result.IsSuccess)
                {
                    if (result.ErrorMessage != null) ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    else ModelState.AddModelError(string.Empty, $"Unknown error getting pet with id of {id}.");
                    return View(viewModel);
                }
                viewModel.Pet = result.Pet;
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(PetUpsertViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // get image from form and copy to temp directory in wwwroot
                var files = HttpContext.Request.Form.Files;

                if (viewModel.Pet!.Id == 0)
                {
                    if (files.Count() > 0)
                    {
                        viewModel.Pet!.Image = files[0];
                        // viewModel.Pet!.ImageFileName = files[0].FileName;    // DONT DO THIS !!! - this is managed by the api which needs to control when value changes for manipulating in conjunction with image upload and deletion
                    }
                    var result = await _petHttpProvider.AddPetAsync(viewModel.Pet, HttpContext.Session.GetString("JwtToken")!);
                    if (!result.IsSuccess)
                    {
                        if (result.ErrorMessage != null) ModelState.AddModelError(string.Empty, result.ErrorMessage);
                        else ModelState.AddModelError(string.Empty, $"Unknown error creating the pet.");
                        viewModel.GenderList = StaticDetails.GenderList;
                        return View(viewModel);
                    }
                    TempData["alertSuccess"] = "Pet successfully created.";
                }
                else
                {
                    if (files.Count() > 0)
                    {
                        viewModel.Pet!.Image = files[0];
                        // viewModel.Pet!.ImageFileName = files[0].FileName;    // DONT DO THIS !!! - this is managed by the api which needs to control when value changes for manipulating in conjunction with image upload and deletion
                    }
                    var result = await _petHttpProvider.EditPetAsync(viewModel.Pet.Id, viewModel.Pet, HttpContext.Session.GetString("JwtToken")!);
                    if (!result.IsSuccess)
                    {
                        if (result.ErrorMessage != null) ModelState.AddModelError(string.Empty, result.ErrorMessage);
                        else ModelState.AddModelError(string.Empty, $"Unknown error editing the pet.");
                        viewModel.GenderList = StaticDetails.GenderList;
                        return View(viewModel);
                    }
                    TempData["alertSuccess"] = "Pet successfully updated.";
                }
                return RedirectToAction(nameof(Index));
            }
            else return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _petHttpProvider.DeletePetAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage != null) TempData["alertError"] = result.ErrorMessage;
                else TempData["alertError"] = "Error deleting the pet.";
            }
            else
            {
                TempData["alertSuccess"] = "Pet successfully deleted.";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ShowDeleteConfirmModal(int id)
        {
            var result = await _petHttpProvider.GetPetAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage != null) TempData["alertError"] = result.ErrorMessage;
                else TempData["alertError"] = "Error retrieving the object.";
                return RedirectToAction(nameof(Index));
            }
            PetDeleteViewModel viewModel = new PetDeleteViewModel() { Pet = result.Pet };
            return PartialView("_ConfirmDeletePetPartial", viewModel);
        }
    }
}
