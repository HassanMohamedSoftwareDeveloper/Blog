using Blog.Infrastructure.Services;
using Blog.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages.Auth;

public class LoginModel : PageModel
{
    [BindProperty]
    public LoginViewModel Login { get; set; } = new();
    public void OnGet(string ReturnUrl = "")
    {
        Login.ReturnUrl = ReturnUrl;
    }
    public async Task<IActionResult> OnPost([FromServices] IUserManagerService userManagerService)
    {
        if (ModelState.IsValid is false) return Page();
        var result = await userManagerService.LoginAsync(Login.Email, Login.Password, Login.RememberMe);
        if (result)
            return Redirect(string.IsNullOrWhiteSpace(Login.ReturnUrl) ? "../../dashboard/index" : Login.ReturnUrl);
        ModelState.AddModelError("msg", "Invalid username or password");
        return Page();
    }
    public async Task<IActionResult> OnGetLogout([FromServices] IUserManagerService userManagerService)
    {
        await userManagerService.LogoutAsync();
        return Redirect("../../index");
    }
}
