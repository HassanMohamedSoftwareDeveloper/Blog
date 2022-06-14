using Blog.Data.FileManager;
using Blog.Data.Repositories;
using Blog.Models.Comments;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class HomeController : Controller
{
    #region Fields :
    private readonly IRepository _repo;
    private readonly IFileManager _fileManager;
    #endregion

    #region CTORS :
    public HomeController(IRepository repo, IFileManager fileManager)
    {
        _repo = repo;
        _fileManager = fileManager;
    }
    #endregion
    #region Actions :
    public IActionResult Index()
    {
        return View(_repo.GetLatestPosts(4));
    }

    [HttpGet("/Image/{image}")]
    [ResponseCache(CacheProfileName = "Monthly")]
    public IActionResult Image(string image) =>
        //Range Operator
        new FileStreamResult(_fileManager.ImageStream(image), $"image/{image[(image.LastIndexOf('.') + 1)..]}");
    [HttpPost]
    public async Task<IActionResult> Comment(CommentViewModel vm)
    {
        if (ModelState.IsValid is false)
            return RedirectToAction("Post", new { id = vm.PostId });
        var post = _repo.GetPostEntity(vm.PostId);
        if (vm.MainCommentId == 0)
        {
            post.MainComments ??= new List<MainComment>(); //compound assignment
            post.MainComments.Add(new MainComment { Message = vm.Message });
            _repo.UpdatePost(post);
        }
        else
        {
            var comment = new SubComment
            {
                MainCommentId = vm.MainCommentId,
                Message = vm.Message,
            };
            _repo.AddSubComment(comment);
        }
        await _repo.SaveChangesAsync();
        return RedirectToAction("Post", new { id = vm.PostId });
    }

    public IActionResult LatestPosts()
    {
        return PartialView("_LatestPosts", _repo.GetLatestPosts(3));
    }
    #endregion
}
