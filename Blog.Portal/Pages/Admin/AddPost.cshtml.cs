using Blog.Application.Commands;
using Blog.Application.Queries.Admin;
using Blog.Portal.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Portal.Pages.Admin;
[Authorize(Roles = "Admin,Blogger")]
public class AddPostModel : PageModel
{
    private readonly IMediator _mediator;

    [BindProperty]
    public PostViewModel Post { get; set; }
    public AddPostModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task OnGet()
    {
        Post = new PostViewModel
        {
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
        var response = await _mediator.Send(new AddPost(Post.Title, Post.Description, Post.Tags, Post.Body,
            Post.ImageFile.OpenReadStream(), User.Identity.Name, Post.CategoryId));
        return RedirectToPage("Posts");
    }
}
