using Blog.Portal.ViewModels;
using FluentValidation;

namespace Blog.Portal.Validations;

public class AddCategoryViewModelValidator : AbstractValidator<AddCategoryViewModel>
{
    public AddCategoryViewModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithName("Category Name").WithMessage("Please provide Category Name");
    }
}
