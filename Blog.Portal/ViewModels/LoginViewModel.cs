using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Portal.ViewModels;

public class LoginViewModel
{
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DisplayName("Remember Me")]
    public bool RememberMe { get; set; }
    public string ReturnUrl { get; set; }
}
