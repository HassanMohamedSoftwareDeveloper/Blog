using Blog.Data.FileManager;
using Blog.Data.Repositories;
using Blog.Helper;
using Blog.Models;
using Blog.Models.Comments;
using Blog.Services.Email;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class HomeController : Controller
{
    #region Fields :
    private readonly IRepository _repo;
    private readonly IFileManager _fileManager;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly IPasswordGenerator _passwordGenerator;
    #endregion

    #region CTORS :
    public HomeController(IRepository repo, IFileManager fileManager, UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService, IPasswordGenerator passwordGenerator)
    {
        _repo = repo;
        _fileManager = fileManager;
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _passwordGenerator = passwordGenerator;
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
            return RedirectToAction("Post", "Blog", new { id = vm.PostId });
        User user;
        if (User.Identity.IsAuthenticated)
            user = await _userManager.FindByNameAsync(User.Identity.Name);
        else
        {
            user = await _userManager.FindByNameAsync(vm.Username);
            if (user is null)
                user = await _userManager.FindByEmailAsync(vm.Email);
            if (user is null)
            {
                user = new User { UserName = vm.Username, Email = vm.Email, Image = "avatar.jpg" };
                string pass = _passwordGenerator.GeneratePassword(true, true, true, true, 8);
                var result = await _userManager.CreateAsync(user, pass);
                if (result.Succeeded)
                    await _emailService.SendEmailAsync(user.Email, "Welcome", $"Thank you for registration  and your permanent password {pass}");
            }
            await _signInManager.SignInAsync(user, false);
        }

        var post = _repo.GetPostEntity(vm.PostId);
        if (vm.MainCommentId == 0)
        {
            post.MainComments ??= new List<MainComment>(); //compound assignment
            post.MainComments.Add(new MainComment { Message = vm.Message, UserId = user.Id });
            _repo.UpdatePost(post);
        }
        else
        {
            var comment = new SubComment
            {
                MainCommentId = vm.MainCommentId,
                Message = vm.Message,
                UserId = user.Id
            };
            _repo.AddSubComment(comment);
        }
        await _repo.SaveChangesAsync();
        return RedirectToAction("Post", "Blog", new { id = vm.PostId });
    }

    public IActionResult LatestPosts()
    {
        return PartialView("_LatestPosts", _repo.GetLatestPosts(3));
    }
    #endregion
}
