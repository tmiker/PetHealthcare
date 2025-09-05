using Microsoft.AspNetCore.Mvc;
using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.ViewModels.AdminUsers;

namespace PetHealthcare.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IAdminUsersHttpProvider _userHttpProvider;

        public AdminUsersController(IAdminUsersHttpProvider userHttpProvider)
        {
            _userHttpProvider = userHttpProvider;
        }

        public IActionResult Home()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            AdminUsersIndexViewModel viewModel = new AdminUsersIndexViewModel();
            var result = await _userHttpProvider.GetAllUsersAsync(HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage != null) ModelState.AddModelError(string.Empty, result.ErrorMessage);
                else ModelState.AddModelError(string.Empty, "Unknown error retrieving users.");
            }
            else viewModel.Users = result.UserInfoDTOs!.ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> LockUser(string id)
        {
            var result = await _userHttpProvider.LockAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage != null) TempData["alertError"] = result.ErrorMessage;
                else TempData["alertError"] = "Unknown error managing user.";
            }
            else TempData["alertSuccess"] = "User locked.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UnlockUser(string id)
        {
            var result = await _userHttpProvider.UnLockAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage != null) TempData["alertError"] = result.ErrorMessage;
                else TempData["alertError"] = "Unknown error managing user.";
            }
            else TempData["alertSuccess"] = "User unlocked.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FlipUserAdminRole(string id)
        {
            var result = await _userHttpProvider.FlipUserAdminRoleAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage != null) TempData["alertError"] = result.ErrorMessage;
                else TempData["alertError"] = "Unknown error managing user.";
            }
            else
            {
                if (result.SuccessMessage != null && result.SuccessMessage.Length > 0) TempData["alertSuccess"] = result.SuccessMessage;
                else TempData["alertSuccess"] = "User roled updated successfully.";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FlipUserManagerRole(string id)
        {
            var result = await _userHttpProvider.FlipUserManagerRoleAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage != null) TempData["alertError"] = result.ErrorMessage;
                else TempData["alertError"] = "Unknown error managing user.";
            }
            else
            {
                if (result.SuccessMessage != null && result.SuccessMessage.Length > 0) TempData["alertSuccess"] = result.SuccessMessage;
                else TempData["alertSuccess"] = "User roled updated successfully.";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FlipUserEmployeeRole(string id)
        {
            var result = await _userHttpProvider.FlipUserEmployeeRoleAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage != null) TempData["alertError"] = result.ErrorMessage;
                else TempData["alertError"] = "Unknown error managing user.";
            }
            else
            {
                if (result.SuccessMessage != null && result.SuccessMessage.Length > 0) TempData["alertSuccess"] = result.SuccessMessage;
                else TempData["alertSuccess"] = "User roled updated successfully.";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FlipUserCustomerRole(string id)
        {
            var result = await _userHttpProvider.FlipUserCustomerRoleAsync(id, HttpContext.Session.GetString("JwtToken")!);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage != null) TempData["alertError"] = result.ErrorMessage;
                else TempData["alertError"] = "Unknown error managing user.";
            }
            else
            {
                if (result.SuccessMessage != null && result.SuccessMessage.Length > 0) TempData["alertSuccess"] = result.SuccessMessage;
                else TempData["alertSuccess"] = "User roled updated successfully.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
