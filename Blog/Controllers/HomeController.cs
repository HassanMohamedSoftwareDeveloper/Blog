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
    public IActionResult Index(string category) =>
        View(string.IsNullOrWhiteSpace(category) ? _repo.GetAllPosts() : _repo.GetAllPosts(category));
    public IActionResult Post(int id) =>
        View(_repo.GetPost(id));
    [HttpGet("/Image/{image}")]
    public IActionResult Image(string image) =>
        //Range Operator
        new FileStreamResult(_fileManager.ImageStream(image), $"image/{image[(image.LastIndexOf('.') + 1)..]}");
}
