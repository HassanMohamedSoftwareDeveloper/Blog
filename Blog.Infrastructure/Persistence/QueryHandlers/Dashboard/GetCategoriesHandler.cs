using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    private readonly IConfigurationProvider _configurationProvider;
    #endregion

    #region CTORS :
    public GetCategoriesHandler(ReadDbContext context, IMapper mapper)
    {
        _categories = context.Categories;
        _configurationProvider = mapper.ConfigurationProvider;
    }
    #endregion

    #region Methods :
    public async Task<List<CategoryDto>> Handle(GetCategories request, CancellationToken cancellationToken)
    {
        return await _categories.AsNoTracking().ProjectTo<CategoryDto>(_configurationProvider)
            .ToListAsync(cancellationToken: cancellationToken);
    }
    #endregion
}
