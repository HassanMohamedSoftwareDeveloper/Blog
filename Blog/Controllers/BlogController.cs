using Blog.Data.Repositories;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class BlogController : Controller
{
    #region Fields :
    private readonly IRepository _repo;
    private readonly UserManager<User> _userManager;
    #endregion

    #region CTORS :
    public BlogController(IRepository repo, UserManager<User> userManager)
    {
        _repo = repo;
        _userManager = userManager;
    }
    #endregion

    #region Actions :
    public IActionResult Index(int pageNumber, string category, string search)
    {
        if (pageNumber < 1)
            return RedirectToAction("Index", new { pageNumber = 1, category, search });
        var vm = _repo.GetAllPosts(pageNumber, category, search);
        return View(vm);
    }
    public async Task<IActionResult> Post(int id)
    {
        User user = null;
        if (User.Identity.IsAuthenticated)
            await _userManager.FindByNameAsync(User.Identity.Name);
        _repo.AddViewer(new Viewer { PostId = id, UserId = user?.Id });
        await _repo.SaveChangesAsync();
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
