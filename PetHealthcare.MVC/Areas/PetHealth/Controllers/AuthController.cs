using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.MVC.ViewModels.Auth;
using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.DTOs;
using System.Security.Claims;

namespace PetHealthcare.MVC.Areas.PetHealth.Controllers
{
    [Area("PetHealth")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthenticationHttpProvider _authenticationHttpProvider;
        private readonly ITokenStatusDecoder _tokenStatusDecoder;
        private readonly IClaimsDecoder _claimsDecoder;

        public AuthController(ILogger<AuthController> logger, IAuthenticationHttpProvider authenticationHttpProvider, 
            ITokenStatusDecoder tokenStatusDecoder, IClaimsDecoder claimsDecoder)
        {
            _logger = logger;
            _authenticationHttpProvider = authenticationHttpProvider;
            _tokenStatusDecoder = tokenStatusDecoder;
            _claimsDecoder = claimsDecoder;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            RegisterUserViewModel viewModel = new RegisterUserViewModel()
            {
                RegisterDTO = new RegisterUserDTO()
            };
            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticationHttpProvider.RegisterUserAsync(viewModel.RegisterDTO!);
                if (!result.IsSuccess)
                {
                    if (!result.ErrorMessages!.Any()) ModelState.AddModelError(string.Empty, "Unknown error when registering.");
                    else
                    {
                        foreach (var error in result.ErrorMessages!) ModelState.AddModelError(string.Empty, error);
                    }
                    return View(viewModel);
                }
                TempData["alertSuccess"] = "Registration successful!";
                return RedirectToAction(nameof(Login));
            }
            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            LoginUserViewModel viewModel = new LoginUserViewModel()
            {
                LoginDTO = new LoginUserDTO()
            };
            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticationHttpProvider.LoginUserAsync(viewModel.LoginDTO!);
                if (!result.IsSuccess)
                {
                    if (!result.ErrorMessages!.Any()) ModelState.AddModelError(string.Empty, "Unknown error when logging in.");
                    else
                    {
                        foreach (var error in result.ErrorMessages!) ModelState.AddModelError(string.Empty, error);
                    }
                    return View(viewModel);
                }

                List<Claim> claims = _claimsDecoder.GetClaims(result.ResponseDTO!.JwtToken!);
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = viewModel.LoginDTO!.RememberMe });
                HttpContext.Session.SetString("JwtToken", result.ResponseDTO!.JwtToken!);
                HttpContext.Session.SetString("RefreshToken", result.ResponseDTO!.RefreshToken!);
                string welcomeName = string.Empty;
                var nameClaim = claims.First(c => c.Type == ClaimTypes.Name);
                if (nameClaim != null) welcomeName = nameClaim.Value;
                else welcomeName = viewModel.LoginDTO.Email!;
                TempData["alertSuccess"] = $"Welcome {welcomeName}!";
                // return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index), "Home", new { area = "Landing" });
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JwtToken", "");
            HttpContext.Session.SetString("RefreshToken", "");
            TempData["alertSuccess"] = $"Signout successful!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        [Authorize]
        [HttpGet]
        public IActionResult UpdatePassword()
        {
            UpdatePasswordViewModel viewModel = new UpdatePasswordViewModel()
            {
                UpdatePasswordDTO = new UpdatePasswordDTO() { Email = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value }
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticationHttpProvider.UpdatePasswordAsync(viewModel.UpdatePasswordDTO!, HttpContext.Session.GetString("JwtToken")!);
                if (!result.IsSuccess)
                {
                    if (!result.ErrorMessages!.Any()) ModelState.AddModelError(string.Empty, "Unknown error when registering.");
                    else
                    {
                        foreach (var error in result.ErrorMessages!) ModelState.AddModelError(string.Empty, error);
                    }
                    return View(viewModel);
                }
                TempData["alertSuccess"] = "Password update successful!";
                await HttpContext.SignOutAsync();
                HttpContext.Session.SetString("JwtToken", "");
                HttpContext.Session.SetString("RefreshToken", "");
                return RedirectToAction(nameof(Login));
            }
            return View(viewModel);
        }

        [Authorize]
        public IActionResult ShowTokenStatus()
        {
            string jwtToken = HttpContext.Session.GetString("JwtToken")!;
            ShowTokenStatusViewModel viewModel = new ShowTokenStatusViewModel()
            {
                TokenStatusDTO = _tokenStatusDecoder.GetJwtTokenStatus(jwtToken!)
            };
            viewModel.TokenStatusDTO.JwtToken = HttpContext.Session.GetString("JwtToken");
            viewModel.TokenStatusDTO.RefreshToken = HttpContext.Session.GetString("RefreshToken");
            return View(viewModel);
        }

        [Authorize]
        public IActionResult ShowUserClaims()
        {
            var user = HttpContext.User;
            List<Claim> claims = user.Claims.ToList();
            UserClaimsViewModel viewModel = new UserClaimsViewModel()
            {
                Username = claims.First(claim => claim.Type == ClaimTypes.Name).Value,
                Email = claims.First(claim => claim.Type == ClaimTypes.Email).Value,
                CustomerNumber = claims.First(claim => claim.Type == "CustomerNumber").Value
            };
            List<Claim> roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            foreach (var role in roles) viewModel.Roles!.Add(role.Value);
            viewModel.JwtToken = HttpContext.Session.GetString("JwtToken");
            viewModel.RefreshToken = HttpContext.Session.GetString("RefreshToken");
            return View(viewModel);
        }

        

        
    }
}
