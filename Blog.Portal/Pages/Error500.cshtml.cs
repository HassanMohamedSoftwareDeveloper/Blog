using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages;

public class Error500Model : PageModel
{
    public void OnGet()
    {
    }
    public IActionResult OnPost(string search)
    {
        return Redirect(Path.Combine("/", "search", search));
    }
}
