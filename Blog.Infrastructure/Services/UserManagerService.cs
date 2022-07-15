using Blog.Application.Consts;
using Blog.Application.Services;
using Blog.Infrastructure.Consts;
using Blog.Infrastructure.Persistence.Models.Write;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;

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
        var user = await _userManager.FindByEmailAsync(username);
        if (user is null)
            user = await _userManager.FindByNameAsync(username);
        if (user is null) return false;

        if (user.EmailConfirmed is false) return false;

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (result.Succeeded is false || result.IsNotAllowed) return false;

        IEnumerable<Claim> claims = new List<Claim>
        {
            new Claim(Claims.FullName,String.Join(" ",user.FirstName,user.LastName)),
            new Claim(Claims.ImagePath,user.Image),
        };
        await _signInManager.SignInWithClaimsAsync(user, isPersistent, claims);

        return true;
    }
    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
    public async Task<User> RegisterAsync(string username, string email, string firstName, string lastName, string password, Stream imageSourceStream)
    {
        var user = await _userManager.FindByEmailAsync(username);
        if (user is null)
            user = await _userManager.FindByNameAsync(username);

        if (user is not null) return user;

        user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = username,
            Email = email,
            Image = imageSourceStream == null ? DefaultFileNames.User : await _fileManager.SaveFileAsync(imageSourceStream, FileType.UserImage),
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded) return user;

        return null;
    }
    public async Task SubscribeAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is not null) return;
        user = new User
        {
            Email = email,
            UserName = email
        };
        await _userManager.CreateAsync(user);
    }

    public async Task VerifyEmailAddressAsync(User user, string callbackURL)
    {
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var codeBytes = Encoding.UTF8.GetBytes(code);
        var codeEncoded = WebEncoders.Base64UrlEncode(codeBytes);
        var param = new Dictionary<string, string>
        {
            {"userId",user.Id },
            {"code",codeEncoded }
        };
        var callbackUrlWithParams = QueryHelpers.AddQueryString(callbackURL, param);


        var emailBody = $"<h3>Account Verification</h3>\r\n<p>\r\nHello!\r\n<br/>\r\n<br/>\r\n" +
            $"Thank you for choosing <a href=\"http://www.hassanmohamed.website/\" title=\"HassanMohamed.website\">" +
            $"<strong>HassanMohamed.website</strong>" +
            $"</a>! Please confirm your email address by clicking the link below." +
            $" We'll communicate important updates with you from time to time via email, " +
            $"so it's essential that we have an up-to-date email address on file.\r\n</p>\r\n" +
            $"<div style=\"margin-top: 10px;\">\r\n" +
            $"<a style=\"text-decoration:none;padding:10px;font-size:18px;background-color:green;color:white\"" +
            $" href=\"{callbackUrlWithParams}\" title=\"Confirm your Email\">Verify your Email address</a>\r\n</div>";

        await _emailService.SendEmailAsync(user.Email, $"Hi {string.Join(" ", user.FirstName, user.LastName)}, please verify your account", emailBody);
        //await _emailService.SendVerificationEmailAsync(user.Email, $"Hi {string.Join(" ", user.FirstName, user.LastName)}, please verify your account", emailBody);
    }

    public async Task<(bool IsConfirmed, string Message)> ConfirmAccountAsync(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return (IsConfirmed: false, Message: "Invalid email parameter");

        var codeBytes = WebEncoders.Base64UrlDecode(code);
        var decodedCode = Encoding.UTF8.GetString(codeBytes);
        var result = await _userManager.ConfirmEmailAsync(user, decodedCode);

        return (IsConfirmed: result.Succeeded, Message: result.Succeeded ? "Thank you for confirming your mail"
            : "Your email is not confirmed, please try again later");
    }
    #endregion
}
