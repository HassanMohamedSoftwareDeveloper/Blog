using Blog.Portal.ViewModels;
using FluentValidation;
namespace Blog.Portal.Validations;

public class ForgotPasswordViewModelValidator : AbstractValidator<ForgotPasswordViewModel>
{
	public ForgotPasswordViewModelValidator()
	{
		RuleFor(x => x.Email).NotEmpty().WithName("Email").WithMessage("Please provide Email").EmailAddress();
	}
}
