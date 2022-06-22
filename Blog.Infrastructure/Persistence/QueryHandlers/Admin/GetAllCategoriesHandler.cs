using Blog.Application.DTOS.Admin;
using Blog.Application.Queries.Admin;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Read;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.QueryHandlers.Admin;

internal sealed class GetAllCategoriesHandler : IRequestHandler<GetAllCategories, List<CategoryDto>>
{
    #region Fields :
    private readonly DbSet<CategoryReadModel> _categories;
    #endregion

    #region CTORS :
    public GetAllCategoriesHandler(ReadDbContext context) => _categories = context.Categories;
    #endregion

    #region Methods :
    public async Task<List<CategoryDto>> Handle(GetAllCategories request, CancellationToken cancellationToken)
    {
        return await _categories.AsNoTracking().Select(x => new CategoryDto { Id = x.Id, Name = x.Name }).ToListAsync(cancellationToken);
    }
    #endregion
}
