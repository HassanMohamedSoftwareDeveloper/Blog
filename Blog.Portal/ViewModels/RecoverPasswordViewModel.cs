using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Portal.ViewModels;

public class RecoverPasswordViewModel
{
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [DisplayName("Confirm Password")]
    public string ConfirmPassword { get; set; }
}
