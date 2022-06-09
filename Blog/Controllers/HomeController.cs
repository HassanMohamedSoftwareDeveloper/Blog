using Blog.Data.FileManager;
using Blog.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class HomeController : Controller
{
    private readonly IRepository _repo;
    private readonly IFileManager _fileManager;

    public HomeController(IRepository repo, IFileManager fileManager)
    {
        _repo = repo;
        _fileManager = fileManager;
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
    [HttpGet("/Image/{image}")]
    public IActionResult Image(string image)
    {
        var mime = image[(image.LastIndexOf('.') + 1)..];//Range Operator
        return new FileStreamResult(_fileManager.ImageStream(image),$"image/{mime}");
    }
}
