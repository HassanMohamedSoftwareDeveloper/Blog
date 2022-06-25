using Blog.Portal.ViewModels;
using FluentValidation;
namespace Blog.Portal.Validations;

public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
{
	public LoginViewModelValidator()
	{
		RuleFor(x => x.Email).NotEmpty().WithName("Email").WithMessage("Please provide Email").EmailAddress();
		RuleFor(x => x.Password).NotEmpty().WithName("Password").WithMessage("Please provide Password");
	}
}
