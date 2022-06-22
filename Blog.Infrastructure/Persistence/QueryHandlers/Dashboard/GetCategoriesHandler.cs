using Blog.Application.DTOS.Dashboard;
using Blog.Application.Queries.Dashboard;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Read;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.QueryHandlers.Dashboard;

internal sealed class GetCategoriesHandler : IRequestHandler<GetCategories, List<CategoryDto>>
{
    #region Fields :
    private readonly DbSet<CategoryReadModel> _categories;
    #endregion

    #region CTORS :
    public GetCategoriesHandler(ReadDbContext context) => _categories = context.Categories;
    #endregion

    #region Methods :
    public async Task<List<CategoryDto>> Handle(GetCategories request, CancellationToken cancellationToken)
    {
        return await _categories.AsNoTracking().Select(x => new CategoryDto { Id = x.Id, Name = x.Name, PostsCount = x.Posts.Count }).ToListAsync(cancellationToken: cancellationToken);
    }
    #endregion
}
