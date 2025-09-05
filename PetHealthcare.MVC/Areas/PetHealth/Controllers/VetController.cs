using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.DTOs;
using PetHealthcare.MVC.Paging;
using PetHealthcare.MVC.ViewModels.Vet;
using System.Text;

namespace PetHealthcare.MVC.Areas.PetHealth.Controllers
{
    [Authorize]
    [Area("PetHealth")]
    public class VetController : Controller
    {
        private readonly IVetHttpProvider _vetHttpProvider;
        private readonly int PageSize = 6;

        public VetController(IVetHttpProvider vetHttpProvider)
        {
            _vetHttpProvider = vetHttpProvider;
        }

        public async Task<IActionResult> Index(int productPage = 1, string? searchHospital = null, string? searchDoctor = null, string? searchState = null)
        {
            var result = await _vetHttpProvider.GetAllVetsAsync(HttpContext.Session.GetString("JwtToken")!);
            VetIndexViewModel viewModel = new VetIndexViewModel();

            if (result.Vets == null)
            {
                //ModelState.AddModelError(string.Empty, "Could not find any vets.");
                viewModel.Vets = new List<VetDTO>();
            }
            else
            {
                var VetList = result.Vets!.ToList();
                // search box
                StringBuilder param = new StringBuilder();
                param.Append("/Customer/Vet/Index?productPage=:");
                param.Append("&searchHospital=");
                if (searchHospital != null)
                {
                    param.Append(searchHospital);
                }
                param.Append("&searchDoctor=");
                if (searchDoctor != null)
                {
                    param.Append(searchDoctor);
                }
                param.Append("&searchState=");
                if (searchState != null)
                {
                    param.Append(searchState);
                }

                if (searchHospital != null)
                {
                    viewModel.Vets = VetList.Where(v => v.Hospital!.ToLower().Contains(searchHospital.ToLower())).ToList();
                }
                else
                {
                    if (searchDoctor != null)
                    {
                        viewModel.Vets = VetList.Where(v => v.Doctor!.ToLower().Contains(searchDoctor.ToLower())).ToList();
                    }
                    else
                    {
                        if (searchState != null)
                        {
                            viewModel.Vets = VetList.Where(v => v.State!.ToLower().Contains(searchState.ToLower())).ToList();
                        }
                        else
                        {
                            viewModel.Vets = VetList;
                        }
                    }
                }
                // pagination
                var count = viewModel.Vets.Count;
                viewModel.Vets = viewModel.Vets
                    .OrderBy(c => c.Id)
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
            var result = await _vetHttpProvider.GetVetAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                TempData["alertError"] = "Error retrieving the vet.";
                return RedirectToAction(nameof(Index));
            }

            VetDetailViewModel viewModel = new VetDetailViewModel()
            {
                Vet = result.Vet
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            VetUpsertViewModel viewModel = new VetUpsertViewModel();

            if (id == null)
            {
                viewModel.Vet = new VetDTO();
                return View(viewModel);
            }
            else
            {
                var result = await _vetHttpProvider.GetVetAsync(id!.Value, HttpContext.Session.GetString("JwtToken")!);
                if (!result.IsSuccess)
                {
                    if (result.ErrorMessage != null) ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    else ModelState.AddModelError(string.Empty, $"Unknown error getting vet with id of {id}");
                    return View(viewModel);
                }
                viewModel.Vet = result.Vet;
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(VetUpsertViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Vet!.Id == 0)
                {
                    var result = await _vetHttpProvider.AddVetAsync(viewModel.Vet, HttpContext.Session.GetString("JwtToken")!);
                    if (!result.IsSuccess)
                    {
                        if (result.ErrorMessage != null) ModelState.AddModelError(string.Empty, result.ErrorMessage);
                        else ModelState.AddModelError(string.Empty, $"Unknown error creating the vet");
                        return View(viewModel);
                    }
                    TempData["alertSuccess"] = "Vet successfully created.";
                }
                else
                {
                    var result = await _vetHttpProvider.EditVetAsync(viewModel.Vet.Id, viewModel.Vet, HttpContext.Session.GetString("JwtToken")!);
                    if (!result.IsSuccess)
                    {
                        if (result.ErrorMessage != null) ModelState.AddModelError(string.Empty, result.ErrorMessage);
                        else ModelState.AddModelError(string.Empty, $"Unknown error editing the vet.");
                        return View(viewModel);
                    }
                    TempData["alertSuccess"] = "Vet successfully updated.";
                }
                return RedirectToAction(nameof(Index));
            }
            else return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _vetHttpProvider.DeleteVetAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage != null) TempData["alertError"] = result.ErrorMessage;
                else TempData["alertError"] = "Error deleting the vet.";
            }
            else
            {
                TempData["alertSuccess"] = "Vet successfully deleted.";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ShowDeleteConfirmModal(int id)
        {
            var result = await _vetHttpProvider.GetVetAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage != null) TempData["alertError"] = result.ErrorMessage;
                else TempData["alertError"] = "Error retrieving the object.";
                return RedirectToAction(nameof(Index));
            }
            VetDeleteViewModel viewModel = new VetDeleteViewModel() { Vet = result.Vet };
            return PartialView("_ConfirmDeleteVetPartial", viewModel);
        }
    }
}
