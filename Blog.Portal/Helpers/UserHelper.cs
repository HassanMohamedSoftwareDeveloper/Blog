using System.Security.Claims;

namespace Blog.Portal.Helpers;

internal static class UserHelper
{
    public static string UserId(this ClaimsPrincipal @this)
        => @this.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
}
