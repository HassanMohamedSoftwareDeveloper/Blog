using Blog.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages.Auth
{
    public class ForgotPasswordModel : PageModel
    {
        [BindProperty]
        public ForgotPasswordViewModel ForgotPassword { get; set; } = new();
        public void OnGet()
        {
        }
    }
}
