using Microsoft.AspNetCore.Identity;

namespace Blog.Infrastructure.Persistence.Models.Write;

internal class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Image { get; set; }
}
