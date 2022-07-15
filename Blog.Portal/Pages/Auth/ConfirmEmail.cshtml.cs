using Blog.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages.Auth;

public class ConfirmEmailModel : PageModel
{
    #region PROPS :
    public (bool IsConfirmed, string Message) ConfirmationResult { get; set; }
    #endregion

    #region Actions :
    public async Task<IActionResult> OnGet([FromQuery] string userId, [FromQuery] string code, [FromServices] IUserManagerService userManager)
    {
        if (userId == null || code == null)
            return Redirect(Path.Combine("/", "Error400", "Invalid email confirmation url"));

        ConfirmationResult = await userManager.ConfirmAccountAsync(userId, code);
        return Page();
    }
    #endregion
}
