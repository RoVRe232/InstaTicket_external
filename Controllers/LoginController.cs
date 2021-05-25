using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaTicket.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InstaTicket.Controllers
{
    public class LoginController : Controller
    {
        private UserService _userService;

        public LoginController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(_userService.ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, _userService.ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            _userService.ExternalLogins = (await _userService._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            _userService.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _userService._signInManager.PasswordSignInAsync(_userService.Input.Email,
                    _userService.Input.Password, _userService.Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = _userService.Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Index();
                }
            }

            // If we got this far, something failed, redisplay form
            return Index();
        }
    }
}