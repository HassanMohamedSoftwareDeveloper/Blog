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

internal sealed class GetPostsHandler : IRequestHandler<GetPosts, PaginationModel<BlogPostDto>>
{
    #region Fields :
    private readonly DbSet<PostReadModel> _posts;
    private readonly IConfigurationProvider _configurationProvider;
    #endregion

    #region CTORS :
    public GetPostsHandler(ReadDbContext context, IMapper mapper)
    {
        _posts = context.Posts;
        _configurationProvider = mapper.ConfigurationProvider;
    }
    #endregion

    #region Methods :
    public async Task<PaginationModel<BlogPostDto>> Handle(GetPosts request, CancellationToken cancellationToken)
    {
        var query = _posts.AsNoTracking();
        if (request.CategoryId != default && request.CategoryId != Guid.Empty)
            query = query.Where(x => x.CategoryId.Equals(request.CategoryId));
        if (string.IsNullOrWhiteSpace(request.Search) is false)
            query = query.Where(x => (x.Title + x.Description + x.Category.Name).ToLower().Contains(request.Search.ToLower()));

        var mappedQuery = query.ProjectTo<BlogPostDto>(_configurationProvider);

        return await new PaginationHelper<BlogPostDto>(request.PageNumber, request.PageSize, mappedQuery).CreateAsync(cancellationToken);
    }
    #endregion
}
