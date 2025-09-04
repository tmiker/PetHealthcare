using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.DTOs;
using PetHealthcare.MVC.ViewModels.Account;
using System.Security.Claims;

namespace PetHealthcare.MVC.Areas.PetHealth.Controllers
{
    [Area("PetHealth")]
    public class AccountController : Controller
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IAuthenticationHttpProvider _authenticationHttpProvider;

        public AccountController(ILogger<AccountController> logger, IAuthenticationHttpProvider authenticationHttpProvider)
        {
            _logger = logger;
            _authenticationHttpProvider = authenticationHttpProvider;
        }

        public IActionResult Index()
        {
            AccountIndexViewModel model = new AccountIndexViewModel() { WelcomeMessage = "Welcome to the Account Index Page!" };
            return View(model);
        }

        [Authorize]
        public IActionResult UserAccount()
        {
            var user = HttpContext.User;
            List<Claim> claims = user.Claims.ToList();
            bool isInt = Int32.TryParse((claims.First(claim => claim.Type == "CustomerNumber").Value), out int number);
            UserAccountViewModel viewModel = new UserAccountViewModel()
            {
                UserAccountDTO = new UserAccountDTO()
                {
                    Email = claims.First(claim => claim.Type == ClaimTypes.Email).Value,
                    UserName = claims.First(claim => claim.Type == ClaimTypes.Name).Value
                }
            };
            if (isInt) viewModel.UserAccountDTO.CustomerNumber = number;
            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult DeleteAccount(string email)
        {
            var user = User;
            string userEmail = user.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            if (userEmail != email) throw new InvalidOperationException();
            DeleteAccountDTO deleteAccountDTO = new DeleteAccountDTO()
            {
                UserName = user.Claims.First(c => c.Type == ClaimTypes.Name).Value,
                Email = user.Claims.First(c => c.Type == ClaimTypes.Email).Value,
                Password = null
            };

            DeleteAccountViewModel viewModel = new DeleteAccountViewModel()
            {
                DeleteAccountDTO = deleteAccountDTO
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount(DeleteAccountViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.DeleteAccountDTO.Password == null)
                {
                    ModelState.AddModelError(string.Empty, "Please enter your password.");
                    return View(viewModel);
                }
                var result = await _authenticationHttpProvider.DeleteAccountAsync(viewModel.DeleteAccountDTO, HttpContext.Session.GetString("JwtToken")!);

                if (!result.IsSuccess)
                {
                    if (!result.ErrorMessages!.Any()) ModelState.AddModelError(string.Empty, "Unknown error removing account. Please contact support.");
                    else
                    {
                        foreach (var error in result.ErrorMessages!) ModelState.AddModelError(string.Empty, error);
                    }
                    return View(viewModel);

                }
                else
                {
                    await HttpContext.SignOutAsync();
                    HttpContext.Session.SetString("JwtToken", "");
                    HttpContext.Session.SetString("RefreshToken", "");
                    TempData["alertSuccess"] = $"Account deletion successful!";
                    if (result.SuccessMessage != null) TempData["alertSuccess"] += $"\n{result.SuccessMessage}";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return View(viewModel);
            }
        }
    }
}
