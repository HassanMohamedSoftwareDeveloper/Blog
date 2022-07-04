using Blog.Application.Queries.Dashboard;
using Blog.Portal.ViewModels;
using MediatR;
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

    #region PROPS 
    public SearchResultViewModel SearchResult { get; set; } = new();
    public string Search { get; set; }
    #endregion

    #region Actions :
    public async Task OnGet(string search)
    {
        this.Search = search;
        SearchResult.LatestPosts = await _mediator.Send(new GetLatestPosts());
        SearchResult.Categories = await _mediator.Send(new GetCategories());
        SearchResult.Tags = await _mediator.Send(new GetTags());
    }
    #endregion
}