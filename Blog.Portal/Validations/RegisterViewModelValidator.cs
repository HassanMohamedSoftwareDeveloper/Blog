using Blog.Portal.ViewModels;
using FluentValidation;
namespace Blog.Portal.Validations;

public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
{
	public RegisterViewModelValidator()
	{
		RuleFor(x => x.FirstName).NotEmpty().WithName("First name").WithMessage("Please provide First name");
		RuleFor(x => x.LastName).NotEmpty().WithName("Last name").WithMessage("Please provide Last name");
		RuleFor(x => x.Username).NotEmpty().WithName("Username").WithMessage("Please provide Username").Matches(@"^[a-zA-Z][a-zA-Z0-9\._\-]{0,22}?[a-zA-Z0-9]{0,2}$");
		RuleFor(x => x.Email).NotEmpty().WithName("Email").WithMessage("Please provide Email").EmailAddress();
		RuleFor(x => x.Password).NotEmpty().WithName("Password").WithMessage("Please provide Password");
		RuleFor(x => x.ConfirmPassword).NotEmpty().WithName("Confirm password").WithMessage("Please provide Confirm password")
			.Equal(x => x.Password).WithMessage("Confirm password not equal password");
		RuleFor(x => x.ImageFile).NotNull().WithName("User image").WithMessage("Please provide your image");
		RuleFor(x => x.AgreeTerms).Equal(false).WithMessage("Please accept our terms");


	}

	//private bool ImageMustBeJpg(IFormFile file)
	//{
	//	file.FileName
	//}
}
