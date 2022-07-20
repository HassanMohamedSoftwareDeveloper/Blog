using Blog.Infrastructure.Persistence.Models.Write;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Blog.Portal.Pages.Auth;

public class ExternalLoginModel : PageModel
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    [BindProperty]
    public ExternalLoginViewModel Model { get; set; } = new();
    public ExternalLoginModel(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    public async Task<IActionResult> OnGet()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null) return RedirectToPage("Register");

        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
            isPersistent: false, bypassTwoFactor: true);

        if (signInResult.Succeeded) return Redirect(Path.Combine("/"));

        if (signInResult.IsLockedOut)
            return RedirectToPage("ForgotPassword");
        //ViewData["ReturnUrl"] = returnUrl;
        //ViewData["Provider"] = info.LoginProvider;
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        Model = new ExternalLoginViewModel { Email = email };
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
            return RedirectToPage("");
        var user = await _userManager.FindByEmailAsync(Model.Email);
        IdentityResult result;
        if (user != null)
        {
            result = await _userManager.AddLoginAsync(user, info);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Redirect(Path.Combine("/"));
            }
        }
        else
        {
            Model.Principal = info.Principal;
            user = new User
            {
                Email = Model.Email,
                UserName = Model.Email,
            };
            result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    //TODO: Send an email for the email confirmation and add a default role as in the Register action
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect(Path.Combine("/"));
                }
            }
        }

        foreach (var error in result.Errors)
        {
            ModelState.TryAddModelError(error.Code, error.Description);
        }
        return Page();
    }
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public ClaimsPrincipal Principal { get; set; }
    }
}
