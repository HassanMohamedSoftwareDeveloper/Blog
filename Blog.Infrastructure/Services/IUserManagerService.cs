using Blog.Infrastructure.Persistence.Models.Write;
using Blog.Infrastructure.Results;

namespace Blog.Infrastructure.Services;

public interface IUserManagerService
{
    Task<bool> LoginAsync(string username, string password, bool isPersistent);
    Task LogoutAsync();
    Task<User> RegisterAsync(string username, string email, string firstName, string lastName, string password, Stream imageSourceStream);
    Task SubscribeAsync(string email);
    Task VerifyEmailAddressAsync(User user, string callbackURL);
    Task<(bool IsConfirmed, string Message)> ConfirmAccountAsync(string userId, string code);
    Task<AuthenticationResult> LoginWithFacebookAsync(string accessToken);
}
