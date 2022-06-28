using Blog.Portal.ViewModels;
using FluentValidation;

namespace Blog.Portal.Validations;

public class PostViewModelValidator : AbstractValidator<PostViewModel>
{
    public PostViewModelValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithName("Title").WithMessage("Please provide Title");
        RuleFor(x => x.Description).NotEmpty().WithName("Description").WithMessage("Please provide Description");
        RuleFor(x => x.CategoryId).NotEmpty().WithName("Category").WithMessage("Please provide Category");
        RuleFor(x => x.ImageFile).NotNull().WithName("Post Image").WithMessage("Please provide Post Image").When(x => x.Id == Guid.Empty || x.Id == default);
        RuleFor(x => x.Tags).NotEmpty().WithName("Tags").WithMessage("Please provide Tags");
        RuleFor(x => x.Body).NotEmpty().WithName("Body").WithMessage("Please provide Body");
    }
}
