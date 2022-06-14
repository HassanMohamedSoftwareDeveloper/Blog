using Blog.Data.FileManager;
using Blog.Data.Repositories;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Authorize(Roles = "Admin")]
public class PanelController : Controller
{
    private readonly IRepository _repo;
    private readonly IFileManager _fileManager;

    public PanelController(IRepository repo, IFileManager fileManager)
    {
        _repo = repo;
        _fileManager = fileManager;
    }
    public IActionResult Index() => View(_repo.GetAllPosts());
    public IActionResult Edit(int? id) => View(id == null ? new PostViewModel() : MapToPostViewModel(_repo.GetPostEntity((int)id)));
    [HttpPost]
    public async Task<IActionResult> Edit(PostViewModel postVm)
    {
        var post = MapToPost(postVm);

        if (post.Id.Equals(0))
            _repo.AddPost(post);
        else
            _repo.UpdatePost(post);
        if (await _repo.SaveChangesAsync())
            return RedirectToAction("Index");
        return View(post);
    }
    public async Task<IActionResult> Remove(int id)
    {
        _repo.RemovePost(id);
        await _repo.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    private static PostViewModel MapToPostViewModel(Post post) => new PostViewModel
    {
        Id = post.Id,
        Title = post.Title,
        Body = post.Body,
        CurrentImage = post.Image,
        Description = post.Description,
        Tags = post.Tags,
        Category = post.Category
    };
    private Post MapToPost(PostViewModel post) => new Post
    {
        Id = post.Id,
        Title = post.Title,
        Body = post.Body,
        Image = SaveNewImageAndGetNameAsync(post),
        Description = post.Description,
        Tags = post.Tags,
        Category = post.Category
    };
    private string SaveNewImageAndGetNameAsync(PostViewModel post)
    {
        if (post.Image is null)
            return post.CurrentImage;

        if (string.IsNullOrWhiteSpace(post.CurrentImage) is false)
            _fileManager.RemoveImage(post.CurrentImage);

        return _fileManager.SaveImageAsync(post.Image);
    }
}
