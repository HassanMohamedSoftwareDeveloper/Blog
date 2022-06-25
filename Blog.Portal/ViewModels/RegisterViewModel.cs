using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Portal.ViewModels;

public class RegisterViewModel
{
    [DisplayName("First name")]
    public string FirstName { get; set; }
    [DisplayName("Last name")]
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [DisplayName("Confirm Password")]
    public string ConfirmPassword { get; set; }
    public bool AgreeTerms { get; set; }
    [DisplayName("User image")]
    public IFormFile ImageFile { get; set; }
}
