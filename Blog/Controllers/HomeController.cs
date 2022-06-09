using Blog.Data.Repositories;
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
}
