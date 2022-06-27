using Blog.Application.Commands;
using Blog.Portal.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages.Admin;

public class AddCategoryModel : PageModel
{
    [BindProperty]
    public AddCategoryViewModel Category { get; set; } = new();
    public int MyProperty { get; set; }
    public void OnGet()
    {
    }
    public async Task<IActionResult> OnPost([FromServices] IMediator mediator)
    {
        if (ModelState.IsValid is false) return Page();
        var response = await mediator.Send(new AddCategory(Category.Name));
        return RedirectToPage("Categories");
    }
}
