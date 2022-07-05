using Blog.Application.DTOS.Dashboard;
using Blog.Application.Pagination;
using Blog.Application.Queries.Dashboard;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages.Dashboard;

public class SearchModel : PageModel
{
    #region Fields :
    private readonly IMediator _mediator;
    #endregion

    #region CTORS :
    public SearchModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region PROPS 
    public PaginationModel<SearchDto> PostsWithPagination { get; set; } = new();
    public string Search { get; set; }
    #endregion

    #region Actions :
    public async Task OnGet(string search)
    {
        this.Search = search;
        PostsWithPagination = await _mediator.Send(new SearchPosts(1, 10, search));
        PostsWithPagination.Route = $"{Path.Combine("/", "page")}/{{0}}?search={search}";
    }
    #endregion
}
