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
    #endregion

    #region CTORS :
    public GetLatestPostsHandler(ReadDbContext context) => _posts = context.Posts;
    #endregion

    #region Methods :
    public async Task<List<LatestPostDto>> Handle(GetLatestPosts request, CancellationToken cancellationToken)
    {
        return await _posts.AsNoTracking().OrderByDescending(x => x.Created).Take(3)
            .Select(x => new LatestPostDto { Id = x.Id, Title = x.Title, Image = x.Image, ViewsCount = x.ViewersCount, CommentsCount = x.Comments.Count })
            .ToListAsync(cancellationToken);
    }
    #endregion
}
