using Blog.Application.Services;

namespace Blog.Infrastructure.Services;

public class UserManagerService : IUserManagerService
{
    #region Methods :
    public Task LoginAsync(string username, string password)
    {
        throw new NotImplementedException();
    }
    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    }
    public Task RegisterAsync(string username, string email, string firstName, string lastName, string password, string imageFullPath)
    {
        throw new NotImplementedException();
    }
    #endregion
}
