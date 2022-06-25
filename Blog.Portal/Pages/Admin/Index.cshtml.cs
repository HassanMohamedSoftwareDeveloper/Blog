using Blog.Application.DTOS.Admin;
using Blog.Application.Pagination;
using Blog.Application.Queries.Admin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages.Admin;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    #region Fields :
    private readonly IMediator _mediator;
    #endregion

    #region PROPS :
    public PaginationModel<UserPostDto> Posts { get; set; }
    #endregion

    #region CTORS :
    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region Actions :
    public async Task OnGet()
    {
        var response = await _mediator.Send(new GetPostsByUser(1, 10, "fb21e0f0-e692-43ca-b188-867c63e7f329"));
    }
    #endregion
}
