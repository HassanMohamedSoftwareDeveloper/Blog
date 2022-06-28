using Blog.Application.Commands;
using Blog.Application.Queries.Admin;
using Blog.Portal.Helpers;
using Blog.Portal.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages.Admin;

[Authorize(Roles = "Admin,Blogger")]
public class EditPostModel : PageModel
{
    #region Fields :
    private readonly IMediator _mediator;
    #endregion

    #region PROPS :
    [BindProperty]
    public PostViewModel Post { get; set; }
    #endregion

    #region CTORS :
    public EditPostModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region Actions :
    public async Task OnGet(Guid id)
    {
        var post = await _mediator.Send(new GetPostById(id));
        Post = new PostViewModel
        {
            Id = post.Id,
            Title = post.Title,
            Description = post.Description,
            Body = post.Body,
            CategoryId = post.CategoryId,
            Tags = post.Tags,
            Categories = await _mediator.Send(new GetAllCategories())
        };
    }
    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid is false)
        {
            Post.Categories = await _mediator.Send(new GetAllCategories());
            return Page();
        }
        var response = await _mediator.Send(new UpdatePost(Post.Id, Post.Title, Post.Description, Post.Tags, Post.Body,
            Post?.ImageFile?.OpenReadStream(), User.UserId(), Post.CategoryId));
        return RedirectToPage("Posts");
    }
    #endregion
}
