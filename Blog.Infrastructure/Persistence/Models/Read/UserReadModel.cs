using Microsoft.AspNetCore.Identity;

namespace Blog.Infrastructure.Persistence.Models.Read;

internal class UserReadModel : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Image { get; set; }
}
