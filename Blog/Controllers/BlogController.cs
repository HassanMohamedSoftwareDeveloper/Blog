using Blog.Data.FileManager;
using Blog.Data.Repositories;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class BlogController : Controller
{
    #region Fields :
    private readonly IRepository _repo;
    private readonly IFileManager _fileManager;
    #endregion

    #region CTORS :
    public BlogController(IRepository repo, IFileManager fileManager)
    {
        _repo = repo;
        _fileManager = fileManager;
    }
    #endregion

    #region Actions :
    public IActionResult Index(int pageNumber, string category)
    {
        if (pageNumber < 1)
            return RedirectToAction("Index", new { pageNumber = 1, category });
        var vm = _repo.GetAllPosts(pageNumber, category);
        return View(vm);
    }
    public IActionResult Post(int id)
    {
        PostPageViewModel vm = new PostPageViewModel()
        {
            Post = _repo.GetPost(id),
            PreviousPost = _repo.GetPreviousPost(id, ""),
            NextPost = _repo.GetNextPost(id, ""),
        };
        return View(vm);
    }

    public IActionResult LatestPosts()
    {
        return PartialView("_LatestPosts", _repo.GetLatestPosts(3));
    }
    public IActionResult Categories()
    {
        return PartialView("_Categories", _repo.GetCategories());
    }
    public IActionResult Tags()
    {
        return PartialView("_Tags", _repo.GetTags());
    }

    #endregion
}
