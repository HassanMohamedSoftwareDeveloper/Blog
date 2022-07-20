using Blog.Infrastructure.External.Contracts;

namespace Blog.Infrastructure.Services;

public interface IFacebookAuthService
{
    Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
    Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
}
