using Blog.Data.FileManager;
using Blog.Models;
using Blog.Services.Email;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly IFileManager _fileManager;

    public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, IEmailService emailService, IFileManager fileManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _emailService = emailService;
        _fileManager = fileManager;
    }

    [HttpGet]
    public IActionResult Login() => View(new LoginViewModel());
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        if (ModelState.IsValid is false) return View(login);
        var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
        if (result.Succeeded is false) return View(login);

        var user = await _userManager.FindByNameAsync(login.UserName);
        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        if (isAdmin)
            return RedirectToAction("Index", "Panel");

        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    public IActionResult Register() => View(new RegisterViewModel());
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel vm)
    {
        if (ModelState.IsValid is false) return View(vm);

        var user = new User
        {
            FirstName = vm.FirstName,
            LastName = vm.LastName,
            UserName = vm.UserName,
            Email = vm.Email,
            Image = vm.Image == null ? "avatar.jpg" : SaveNewImageAndGetNameAsync(vm.Image)
        };
        var result = await _userManager.CreateAsync(user, vm.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            await _emailService.SendEmailAsync(user.Email, "Welcome", "Thank you for registration");
            return RedirectToAction("Index", "Home");
        }

        return View(vm);
    }
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


    private string SaveNewImageAndGetNameAsync(IFormFile image)
    {
        return _fileManager.SaveImageAsync(image);
    }
}
