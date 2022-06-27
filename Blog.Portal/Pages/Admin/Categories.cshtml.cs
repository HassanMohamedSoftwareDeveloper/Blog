using Blog.Application.DTOS.Admin;
using Blog.Application.Queries.Admin;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages.Admin;

public class CategoriesModel : PageModel
{
    public List<CategoryDto> Categories { get; set; }
    public async Task OnGet([FromServices] IMediator mediator)
    {
        Categories = await mediator.Send(new GetAllCategories());
    }
}
