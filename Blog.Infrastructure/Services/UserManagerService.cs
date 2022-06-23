using Blog.Application.Consts;
using Blog.Application.Services;
using Blog.Infrastructure.Consts;
using Blog.Infrastructure.Persistence.Models.Write;
using Microsoft.AspNetCore.Identity;

namespace Blog.Infrastructure.Services;

internal sealed class UserManagerService : IUserManagerService
{
    #region Fields :
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly IFileManagerService _fileManager;
    #endregion

    #region CTORS :
    public UserManagerService(SignInManager<User> signInManager, UserManager<User> userManager, IEmailService emailService, IFileManagerService fileManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _emailService = emailService;
        _fileManager = fileManager;
    }
    #endregion

    #region Methods :
    public async Task<bool> LoginAsync(string username, string password, bool isPersistent)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent, false);
        return result.Succeeded;
    }
    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
    public async Task RegisterAsync(string username, string email, string firstName, string lastName, string password, string imageFullPath)
    {
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = username,
            Email = email,
            Image = String.IsNullOrWhiteSpace(imageFullPath) ? DefaultFileNames.User : await _fileManager.SaveFileAsync(imageFullPath, FileType.UserImage)
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);

            await _emailService.SendEmailAsync(user.Email, "Welcome", "Thank you for registration");
        }
    }
    #endregion
}
