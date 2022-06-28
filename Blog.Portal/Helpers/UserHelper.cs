using Blog.Infrastructure.Consts;
using System.Security.Claims;

namespace Blog.Portal.Helpers;

internal static class UserHelper
{
    public static string UserFullName(this ClaimsPrincipal @this)
       => @this.Claims.FirstOrDefault(x => x.Type == Claims.FullName)?.Value;

    public static string UserId(this ClaimsPrincipal @this)
        => @this.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

    public static string UserImage(this ClaimsPrincipal @this)
     => @this.Claims.FirstOrDefault(x => x.Type == Claims.ImagePath)?.Value;
}
