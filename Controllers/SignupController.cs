using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaTicket.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InstaTicket.Controllers
{
    public class SignupController : Controller
    {
        private UserService _userService;

        public SignupController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            _userService.ReturnUrl = returnUrl;
            _userService.ExternalLogins = (await _userService._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(dynamic obj ,string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            _userService.ExternalLogins = (await _userService._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = _userService.Input.Email, Email = _userService.Input.Email };
                var result = await _userService._userManager.CreateAsync(user, _userService.Input.Password);
                if (result.Succeeded)
                {

                    if (_userService._userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = _userService.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _userService._signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction(controllerName:"Signup", actionName: "Index");
        }
    }
}