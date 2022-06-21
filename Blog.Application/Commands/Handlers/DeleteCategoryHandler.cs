using Blog.Application.Exceptions;
using Blog.Application.Services;
using Blog.Domain.Repositories;
using MediatR;

namespace Blog.Application.Commands.Handlers;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategory, bool>
{
    #region Fields :
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryReadService _categoryReadService;
    #endregion

    #region CTORS :
    public DeleteCategoryHandler(ICategoryRepository categoryRepository, ICategoryReadService categoryReadService)
    {
        _categoryRepository = categoryRepository;
        _categoryReadService = categoryReadService;
    }
    #endregion

    #region Methods :
    public async Task<bool> Handle(DeleteCategory request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetAsync(request.Id);
        if (category is null)
            throw new InvalidCategoryIdException(request.Id);

        if (await _categoryReadService.CategoryHasPosts(request.Id))
            throw new CategoryHasPostsException();


        _categoryRepository.Delete(category);
        return await _categoryRepository.SaveChangesAsync(cancellationToken);
    }
    #endregion
}
