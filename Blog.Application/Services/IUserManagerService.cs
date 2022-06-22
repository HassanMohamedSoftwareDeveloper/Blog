namespace Blog.Application.Services;

public interface IUserManagerService
{
    Task LoginAsync(string username, string password);
    Task LogoutAsync();
    Task RegisterAsync(string username, string email, string firstName, string lastName, string password, string imageFullPath);
}
