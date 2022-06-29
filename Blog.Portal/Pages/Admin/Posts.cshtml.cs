using Blog.Application.Commands;
using Blog.Application.DTOS.Admin;
using Blog.Application.Pagination;
using Blog.Application.Queries.Admin;
using Blog.Portal.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task OnGet(int pageNumber, int pageSize, string search)
    {
        Posts = await _mediator.Send(new GetPostsByUser(pageNumber, pageSize, search, User.UserId()));
        Posts.Search = search;
    }
    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        var response = await _mediator.Send(new DeletePost(id));
        return RedirectToPage("Posts");
    }
    #endregion
}
