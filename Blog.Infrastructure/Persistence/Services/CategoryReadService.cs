using Blog.Application.Services;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Read;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.Services;

internal sealed class CategoryReadService : ICategoryReadService
{
    #region Fields :
    private readonly DbSet<CategoryReadModel> _categories;
    #endregion

    #region CTORS :
    public CategoryReadService(ReadDbContext context) => _categories = context.Categories;
    #endregion

    #region Methods :
    public async Task<bool> ExistsByNameAsync(Guid id, string name)
        => await _categories.AsNoTracking()
        .Where(x => x.Id.Equals(id) == false && x.Name.ToLower() == name.ToLower()).AnyAsync();
    public async Task<bool> CategoryHasPosts(Guid id)
        => await _categories.AsNoTracking().Where(x => x.Id.Equals(id)).AnyAsync();
    #endregion
}