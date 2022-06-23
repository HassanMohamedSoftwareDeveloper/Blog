using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    private readonly IConfigurationProvider _configurationProvider;
    #endregion

    #region CTORS :
    public GetAllCategoriesHandler(ReadDbContext context, IMapper mapper)
    {
        _categories = context.Categories;
        _configurationProvider = mapper.ConfigurationProvider;
    }
    #endregion

    #region Methods :
    public async Task<List<CategoryDto>> Handle(GetAllCategories request, CancellationToken cancellationToken)
    {
        return await _categories.AsNoTracking().ProjectTo<CategoryDto>(_configurationProvider).ToListAsync(cancellationToken);
    }
    #endregion
}
