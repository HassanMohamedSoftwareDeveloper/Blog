using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.DTOS.Dashboard;
using Blog.Application.Queries.Dashboard;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Read;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.QueryHandlers.Dashboard;

internal sealed class GetLatestPostsHandler : IRequestHandler<GetLatestPosts, List<LatestPostDto>>
{
    #region Fields :
    private readonly DbSet<PostReadModel> _posts;
    private readonly IConfigurationProvider _configurationProvider;
    #endregion

    #region CTORS :
    public GetLatestPostsHandler(ReadDbContext context, IMapper mapper)
    {
        _posts = context.Posts;
        _configurationProvider = mapper.ConfigurationProvider;
    }
    #endregion

    #region Methods :
    public async Task<List<LatestPostDto>> Handle(GetLatestPosts request, CancellationToken cancellationToken)
    {
        return await _posts.AsNoTracking().OrderByDescending(x => x.Created).Take(3)
            .ProjectTo<LatestPostDto>(_configurationProvider)
            .ToListAsync(cancellationToken);
    }
    #endregion
}
