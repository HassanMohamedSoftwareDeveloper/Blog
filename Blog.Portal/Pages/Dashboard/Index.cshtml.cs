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
    public async Task OnGet(int pageNumber, Guid category, string tag)
    {
        BlogModel.PaginatedPosts = await _mediator.Send(new GetPosts(pageNumber, 4, category, String.Empty, tag));

        string path = string.IsNullOrWhiteSpace(tag) ? category == default || category == Guid.Empty ? "Blog" : $"Tutorials/{category}" : tag;


        BlogModel.PaginatedPosts.Route = $"{Path.Combine("/", path, "page")}/{{0}}";
        BlogModel.LatestPosts = await _mediator.Send(new GetLatestPosts());
        BlogModel.Categories = await _mediator.Send(new GetCategories());
        BlogModel.Tags = await _mediator.Send(new GetTags());
    }
    #endregion
}
