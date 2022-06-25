using Blog.Portal.ViewModels;
using FluentValidation;
namespace Blog.Portal.Validations;

public class RecoverPasswordViewModelValidator : AbstractValidator<RecoverPasswordViewModel>
{
    public RecoverPasswordViewModelValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithName("Password").WithMessage("Please provide Password");
        RuleFor(x => x.ConfirmPassword).NotEmpty().WithName("Confirm password").WithMessage("Please provide Confirm password")
            .Equal(x => x.Password).WithMessage("Confirm password not equal password");
    }
}
