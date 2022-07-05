using Blog.Application.DTOS.Dashboard;
using Blog.Application.Pagination;
using Blog.Application.Queries.Dashboard;
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
    public PaginationModel<BlogPostDto> PaginatedPosts { get; set; } = new();
    #endregion

    #region Actions :
    public async Task OnGet(int pageNumber, Guid category, string tag)
    {
        PaginatedPosts = await _mediator.Send(new GetPosts(pageNumber, 4, category, tag));

        string path = string.IsNullOrWhiteSpace(tag) ? category == default || category == Guid.Empty ? "Blog" : $"Tutorials/{category}" : tag;

        PaginatedPosts.Route = $"{Path.Combine("/", path, "page")}/{{0}}";
    }
    #endregion
}
