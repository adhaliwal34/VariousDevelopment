using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using <ProjectName>.Entities;
using <ProjectName>.Models;

namespace <ProjectName>.Controllers;

public class AccountsController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;

    public AccountsController (SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([Bind("Email,Password,ReturnUrl,RememberMe")] AccountLoginViewModel vm)
    {
        vm.ReturnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return LocalRedirect(vm.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
        }

        // If we got this far, something failed, redisplay form
        return View();
    }

    public async Task<IActionResult> Logout(string returnUrl)
    {
        await _signInManager.SignOutAsync();

        if (returnUrl != null)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            // This needs to be a redirect so that the browser performs a new
            // request and the identity for the user gets updated.
            return RedirectToAction("Index", "Home");
        }
    }
}
