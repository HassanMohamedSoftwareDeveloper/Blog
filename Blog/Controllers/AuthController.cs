using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
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
    public async Task<IActionResult> Register(RegisterViewModel register)
    {
        if (ModelState.IsValid is false) return View(register);
        var user = new IdentityUser
        {
            UserName = register.Email,
            Email = register.Email,
        };
        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        return View(register);
    }
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
