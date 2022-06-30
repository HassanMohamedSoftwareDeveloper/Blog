using Blog.Application.Commands;
using Blog.Application.Queries.Dashboard;
using Blog.Portal.Helpers;
using Blog.Portal.ViewModels;
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
    public BlogPostViewModel BlogPostModel { get; set; } = new();
    #endregion

    #region Actions :
    public async Task OnGet(Guid id)
    {
        await _mediator.Send(new AddViewer(id));

        BlogPostModel.Post = await _mediator.Send(new GetPostById(id));
        BlogPostModel.LatestPosts = await _mediator.Send(new GetLatestPosts());
        BlogPostModel.Categories = await _mediator.Send(new GetCategories());
        BlogPostModel.Tags = await _mediator.Send(new GetTags());

    }

    public async Task<IActionResult> OnPostComment(Guid postId, string comment)
    {
        var response = await _mediator.Send(new AddComment(postId, comment, User.UserId()));
        return RedirectToPage("Post", new { id = postId });
    }
    #endregion
}
