using Blog.Application.Exceptions;
using Blog.Application.Services;
using Blog.Domain.Aggregates;
using Blog.Domain.Repositories;
using Blog.Domain.ValueObjects;
using MediatR;

namespace Blog.Application.Commands.Handlers;

public class AddCategoryHandler : IRequestHandler<AddCategory, bool>
{
    #region Fields :
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryReadService _categoryReadService;
    #endregion

    #region CTORS :
    public AddCategoryHandler(ICategoryRepository categoryRepository, ICategoryReadService categoryReadService)
    {
        _categoryRepository = categoryRepository;
        _categoryReadService = categoryReadService;
    }
    #endregion

    #region Methods :
    public async Task<bool> Handle(AddCategory request, CancellationToken cancellationToken)
    {
        var id = new CategoryId(Guid.NewGuid());

        if (await _categoryReadService.ExistsByNameAsync(id, request.CategoryName))
            throw new CategoryAlreadyExistException(request.CategoryName);

        var category = new Category(id, request.CategoryName);

        _categoryRepository.Create(category);
        return await _categoryRepository.SaveChangesAsync(cancellationToken);
    }
    #endregion
}
