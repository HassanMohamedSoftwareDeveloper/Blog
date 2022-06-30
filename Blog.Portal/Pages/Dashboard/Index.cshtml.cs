using Blog.Application.Queries.Dashboard;
using Blog.Portal.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages.Dashboard;

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
    public BlogViewModel BlogModel { get; set; } = new();
    #endregion

    #region Actions :
    public async Task OnGet()
    {
        BlogModel.PaginatedPosts = await _mediator.Send(new GetPosts(1, 4, Guid.Empty, String.Empty));
        BlogModel.LatestPosts = await _mediator.Send(new GetLatestPosts());
        BlogModel.Categories = await _mediator.Send(new GetCategories());
        BlogModel.Tags = await _mediator.Send(new GetTags());
    }
    #endregion
}
