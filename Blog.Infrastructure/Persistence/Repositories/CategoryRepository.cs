using Blog.Domain.Aggregates;
using Blog.Domain.Repositories;
using Blog.Domain.ValueObjects;
using Blog.Infrastructure.Persistence.Contexts;
using Shared.Abstractions.Domain;

namespace Blog.Infrastructure.Persistence.Repositories;

internal sealed class CategoryRepository : Repository<Category, CategoryId>, ICategoryRepository
{
    #region CTORS :
    public CategoryRepository(WriteDbContext context) : base(context)
    {
    }
    #endregion

    #region Methods :
    public async Task<Category> GetAsync(CategoryId categoryId) => await base.GetAsync(x => x.Id.Equals(categoryId.Value));
    #endregion
}
