using Blog.Application.Commands;
using Blog.Application.DTOS.Dashboard;
using Blog.Application.Queries.Dashboard;
using Blog.Portal.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages.Dashboard;

public class PostModel : PageModel
{
    #region Fields :
    private readonly IMediator _mediator;
    #endregion

    #region CTORS :
    public PostModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region PROPS :
    public PostDto Post { get; set; } = new();
    public bool CurrentUserIsLikeThisPost { get; set; } = false;
    #endregion

    #region Actions :
    public async Task OnGet(Guid id)
    {
        await _mediator.Send(new AddViewer(id));
        CurrentUserIsLikeThisPost = User.Identity.IsAuthenticated.Equals(false) ? false :
            await _mediator.Send(new CheckUserLikePost(id, User.UserId()));

        Post = await _mediator.Send(new GetPostById(id));
    }

    public async Task<IActionResult> OnPostComment(Guid postId, string comment)
    {
        var response = await _mediator.Send(new AddComment(postId, comment, User.UserId()));
        return RedirectToPage("Post", new { id = postId });
    }
    public async Task<IActionResult> OnPostLike(Guid postId)
    {
        var response = await _mediator.Send(new AddLike(postId, User.UserId()));
        return RedirectToPage("Post", new { id = postId });
    }
    public async Task<IActionResult> OnPostDisLike(Guid postId)
    {
        var response = await _mediator.Send(new RemoveLike(postId, User.UserId()));
        return RedirectToPage("Post", new { id = postId });
    }
    #endregion
}
