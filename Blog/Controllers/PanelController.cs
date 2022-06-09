using Blog.Data.Repositories;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Authorize(Roles ="Admin")]
public class PanelController:Controller
{
    private readonly IRepository _repo;

    public PanelController(IRepository repo)
    {
        _repo = repo;
    }
    public IActionResult Index()
    {
        var posts = _repo.GetAllPosts();
        return View(posts);
    }
    public IActionResult Edit(int? id)
    {
        var post = id == null ? new Post() : _repo.GetPost((int)id);
        return View(post);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Post post)
    {
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
}
