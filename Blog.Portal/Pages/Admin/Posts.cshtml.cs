using Blog.Application.DTOS.Admin;
using Blog.Application.Pagination;
using Blog.Application.Queries.Admin;
using Blog.Portal.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages.Admin;

[Authorize(Roles = "Admin,Blogger")]
public class PostsModel : PageModel
{
    #region Fields :
    private readonly IMediator _mediator;
    #endregion

    #region PROPS :
    public PaginationModel<UserPostDto> Posts { get; set; }
    #endregion

    #region CTORS :
    public PostsModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region Actions :
    public async Task OnGet(int pageNumber, int pageSize)
    {
        Posts = await _mediator.Send(new GetPostsByUser(pageNumber, pageSize, User.UserId()));
    }
    #endregion
}
