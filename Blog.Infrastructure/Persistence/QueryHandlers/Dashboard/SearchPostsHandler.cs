using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.DTOS.Dashboard;
using Blog.Application.Pagination;
using Blog.Application.Queries.Dashboard;
using Blog.Infrastructure.Helpers;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Read;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.QueryHandlers.Dashboard;

internal sealed class SearchPostsHandler : IRequestHandler<SearchPosts, PaginationModel<SearchDto>>
{
    #region Fields :
    private readonly DbSet<PostReadModel> _posts;
    private readonly IConfigurationProvider _configurationProvider;
    #endregion

    #region CTORS :
    public SearchPostsHandler(ReadDbContext context, IMapper mapper)
    {
        _posts = context.Posts;
        _configurationProvider = mapper.ConfigurationProvider;
    }
    #endregion

    #region Methods :
    public async Task<PaginationModel<SearchDto>> Handle(SearchPosts request, CancellationToken cancellationToken)
    {
        var query = _posts.AsNoTracking();

        if (string.IsNullOrWhiteSpace(request.Search) is false)
            query = query.Where(x => (x.Title + x.Description + x.Category.Name).ToLower().Contains(request.Search.ToLower()));

        var sourceQuery = query.OrderBy(x => x.Created).ProjectTo<SearchDto>(_configurationProvider);

        return await new PaginationHelper<SearchDto>(request.PageNumber, request.PageSize, sourceQuery).CreateAsync(cancellationToken);
    }
    #endregion
}
