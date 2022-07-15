using Blog.Infrastructure.Services;
using Blog.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages.Auth;

public class RegisterModel : PageModel
{
    [BindProperty]
    public RegisterViewModel Register { get; set; } = new();
    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPost([FromServices] IUserManagerService userManagerService)
    {
        var user = await userManagerService.RegisterAsync(Register.Username, Register.Email, Register.FirstName, Register.LastName, Register.Password, Register.ImageFile.OpenReadStream());
        var callback = $"{Request.Scheme}://{Request.Host}/Auth/ConfirmEmail";
        await userManagerService.VerifyEmailAddressAsync(user, callback);
        return RedirectToPage("AuthResult", new { Message = "Please verify your email, through the verification email we have just sent." });
    }
}
