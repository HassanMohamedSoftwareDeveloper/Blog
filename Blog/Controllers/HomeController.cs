using Blog.Data;
using Blog.Data.Repositories;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class HomeController : Controller
{
    private readonly IRepository _repo;

    public HomeController(IRepository repo)
    {
        _repo = repo;
    }
    public IActionResult Index()
    {
        var posts = _repo.GetAllPosts();
        return View(posts);
    }
    public IActionResult Post(int id)
    {
        var post = _repo.GetPost(id);
        return View(post);
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
