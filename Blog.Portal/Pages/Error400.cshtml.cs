using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages;

public class Error400Model : PageModel
{
    public string Error { get; set; }
    public void OnGet(string error)
    {
        this.Error = error;
    }
}
