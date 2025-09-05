using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.DTOs;
using PetHealthcare.MVC.Paging;
using PetHealthcare.MVC.Utility;
using PetHealthcare.MVC.ViewModels.Visit;
using System.Text;

namespace PetHealthcare.MVC.Areas.PetHealth.Controllers
{
    [Authorize]
    [Area("PetHealth")]
    public class VisitController : Controller
    {
        private readonly IVisitHttpProvider _visitHttpProvider;
        private readonly IPetHttpProvider _petHttpProvider;
        private readonly IVetHttpProvider _vetHttpProvider;
        private readonly int PageSize = 6;

        public VisitController(IVisitHttpProvider visitHttpProvider, IPetHttpProvider petHttpProvider, IVetHttpProvider vetHttpProvider)
        {
            _visitHttpProvider = visitHttpProvider;
            _petHttpProvider = petHttpProvider;
            _vetHttpProvider = vetHttpProvider;
        }

        public async Task<IActionResult> Index(int productPage = 1, string searchPet = null!, string searchDoctor = null!, string searchHospital = null!)
        {
            var result = await _visitHttpProvider.GetAllVisitAggregatesAsync(HttpContext.Session.GetString("JwtToken")!);
            VisitIndexViewModel viewModel = new VisitIndexViewModel();

            if (result.VisitAggregates == null)
            {
                //ModelState.AddModelError(string.Empty, "Could not find any visits.");
                viewModel.VisitAggregates = new List<VisitAggregateDTO>();
            }
            else
            {
                List<VisitAggregateDTO> VisitList = result.VisitAggregates!.ToList();
                // search box
                StringBuilder param = new StringBuilder();
                param.Append("/Customer/Visit/Index?productPage=:");
                param.Append("&searchPet=");
                if (searchPet != null)
                {
                    param.Append(searchPet);
                }
                param.Append("&searchDoctor=");
                if (searchDoctor != null)
                {
                    param.Append(searchDoctor);
                }
                param.Append("&searchHospital=");
                if (searchHospital != null)
                {
                    param.Append(searchHospital);
                }

                if (searchPet != null)
                {
                    viewModel.VisitAggregates = VisitList.Where(v => v.PetName!.ToLower().Contains(searchPet.ToLower())).ToList();
                }
                else
                {
                    if (searchDoctor != null)
                    {
                        viewModel.VisitAggregates = VisitList.Where(v => v.Doctor!.ToLower().Contains(searchDoctor.ToLower())).ToList();
                    }
                    else
                    {
                        if (searchHospital != null)
                        {
                            viewModel.VisitAggregates = VisitList.Where(v => v.Hospital!.ToLower().Contains(searchHospital.ToLower())).ToList();
                        }
                        else
                        {
                            viewModel.VisitAggregates = VisitList;
                        }
                    }
                }
                // end search box            

                // pagination start
                var count = viewModel.VisitAggregates.Count();
                viewModel.VisitAggregates = viewModel.VisitAggregates
                    //.OrderBy(c => c.DateOfVisit)
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
                // pagination end

            }
            return View(viewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _visitHttpProvider.GetVisitAggregateAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                TempData["alertError"] = "Error retrieving the visit.";
                return RedirectToAction(nameof(Index));
            }

            VisitDetailViewModel viewModel = new VisitDetailViewModel()
            {
                VisitAggregate = result.VisitAggregate
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            var petResult = await _petHttpProvider.GetAllPetsAsync(HttpContext.Session.GetString("JwtToken")!);
            var vetResult = await _vetHttpProvider.GetAllVetsAsync(HttpContext.Session.GetString("JwtToken")!);
            VisitUpsertViewModel viewModel = new VisitUpsertViewModel()
            {
                PetList = petResult.Pets!.ToList(),
                VetList = vetResult.Vets!.ToList(),
                VisitTypeList = StaticDetails.VisitTypelist
            };

            if (id == null)
            {
                viewModel.Visit = new VisitDTO();
                return View(viewModel);
            }
            else
            {
                var result = await _visitHttpProvider.GetVisitAsync(id!.Value, HttpContext.Session.GetString("JwtToken")!);
                if (!result.IsSuccess)
                {
                    if (result.ErrorMessage != null) ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    else ModelState.AddModelError(string.Empty, $"Unknown error getting visit with id of {id}.");
                    return View(viewModel);
                }
                viewModel.Visit = result.Visit;
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(VisitUpsertViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Visit!.Id == 0)
                {
                    var result = await _visitHttpProvider.AddVisitAsync(viewModel.Visit, HttpContext.Session.GetString("JwtToken")!);
                    if (!result.IsSuccess)
                    {
                        if (result.ErrorMessage != null) ModelState.AddModelError(string.Empty, result.ErrorMessage);
                        else ModelState.AddModelError(string.Empty, $"Unknown error creating the visit.");
                        var vetResult = await _vetHttpProvider.GetAllVetsAsync();
                        viewModel.VetList = vetResult.Vets!.ToList();
                        viewModel.VisitTypeList = StaticDetails.VisitTypelist;
                        return View(viewModel);
                    }
                    TempData["alertSuccess"] = "Visit successfully created.";
                }
                else
                {
                    var result = await _visitHttpProvider.EditVisitAsync(viewModel.Visit.Id, viewModel.Visit, HttpContext.Session.GetString("JwtToken")!);
                    if (!result.IsSuccess)
                    {
                        if (result.ErrorMessage != null) ModelState.AddModelError(string.Empty, result.ErrorMessage);
                        else ModelState.AddModelError(string.Empty, $"Unknown error editing the visit.");
                        var vetResult = await _vetHttpProvider.GetAllVetsAsync();
                        viewModel.VetList = vetResult.Vets!.ToList();
                        viewModel.VisitTypeList = StaticDetails.VisitTypelist;
                        return View(viewModel);
                    }
                    TempData["alertSuccess"] = "Visit successfully updated.";
                }
                return RedirectToAction(nameof(Index));
            }
            else return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _visitHttpProvider.DeleteVisitAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage != null) TempData["alertError"] = result.ErrorMessage;
                else TempData["alertError"] = "Error deleting the visit.";
            }
            else
            {
                TempData["alertSuccess"] = "Visit successfully deleted.";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ShowDeleteConfirmModal(int id)
        {
            var result = await _visitHttpProvider.GetVisitAggregateAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage != null) TempData["alertError"] = result.ErrorMessage;
                else TempData["alertError"] = "Error retrieving the object.";
                return RedirectToAction(nameof(Index));
            }
            VisitDeleteViewModel viewModel = new VisitDeleteViewModel() { VisitAggregate = result.VisitAggregate };
            return PartialView("_ConfirmDeleteVisitPartial", viewModel);
        }
    }
}
