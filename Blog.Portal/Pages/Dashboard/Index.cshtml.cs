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
    public PaginationModel<BlogPostDto> PostsModel { get; set; } = new();
    #endregion

    #region Actions :
    public async Task OnGet()
    {
        PostsModel = await _mediator.Send(new GetPosts(1, 10, Guid.Empty, String.Empty));
    }
    #endregion
}
