using Blog.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages.Auth
{
    public class RecoverPasswordModel : PageModel
    {
        [BindProperty]
        public RecoverPasswordViewModel RecoverPassword { get; set; } = new();
        public void OnGet()
        {
        }
    }
}
