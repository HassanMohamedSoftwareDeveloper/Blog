using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages.Auth;

public class AuthResultModel : PageModel
{
    public string Message { get; set; }
    public void OnGet(string message)
    {
        this.Message = message;
    }
}
