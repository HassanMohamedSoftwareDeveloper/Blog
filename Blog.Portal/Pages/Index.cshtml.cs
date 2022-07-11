using Blog.Application.DTOS.Dashboard;
using Blog.Application.Queries.Dashboard;
using Blog.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages;

public class IndexModel : PageModel
{
    #region Fields :
    private readonly IMediator _mediator;
    #endregion

    #region CTORS :
    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region PROPS :
    public List<LatestPostDto> LatestPosts { get; set; }
    [BindProperty]
    public string Email { get; set; }
    #endregion
    #region Actions :
    public async Task OnGet()
    {
        LatestPosts = await _mediator.Send(new GetLatestPosts(6));
    }
    public async Task<IActionResult> OnPostSubscribe([FromServices] IUserManagerService userManager)
    {
        await userManager.SubscribeAsync(Email);
        return RedirectToPage("Index");
    }
    #endregion
}