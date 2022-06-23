namespace Blog.Application.Services;

public interface IUserManagerService
{
    Task<bool> LoginAsync(string username, string password, bool isPersistent);
    Task LogoutAsync();
    Task RegisterAsync(string username, string email, string firstName, string lastName, string password, string imageFullPath);
}
