using Blog.Application.Exceptions;
using Blog.Application.Services;
using Blog.Domain.Repositories;
using MediatR;

namespace Blog.Application.Commands.Handlers;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategory, bool>
{
    #region Fields :
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryReadService _categoryReadService;
    #endregion

    #region CTORS :
    public UpdateCategoryHandler(ICategoryRepository categoryRepository, ICategoryReadService categoryReadService)
    {
        _categoryRepository = categoryRepository;
        _categoryReadService = categoryReadService;
    }
    #endregion

    #region Methods :
    public async Task<bool> Handle(UpdateCategory request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetAsync(request.Id);
        if (category is null)
            throw new InvalidCategoryIdException(request.Id);

        if (await _categoryReadService.ExistsByNameAsync(request.Id, request.CategoryName))
            throw new CategoryAlreadyExistException(request.CategoryName);

        category.Update(request.CategoryName);

        _categoryRepository.Update(category);
        return await _categoryRepository.SaveChangesAsync(cancellationToken);
    }
    #endregion
}
