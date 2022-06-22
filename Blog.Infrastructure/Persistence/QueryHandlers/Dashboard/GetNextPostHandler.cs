using Blog.Application.DTOS.Dashboard;
using Blog.Application.Queries.Dashboard;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Read;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.QueryHandlers.Dashboard;

internal sealed class GetNextPostHandler : IRequestHandler<GetNextPost, NextPrevPostDto>
{
    #region Fields :
    private readonly DbSet<PostReadModel> _posts;
    #endregion

    #region CTORS :
    public GetNextPostHandler(ReadDbContext context) => _posts = context.Posts;
    #endregion

    #region Methods :
    public async Task<NextPrevPostDto> Handle(GetNextPost request, CancellationToken cancellationToken)
    {
        return await _posts.AsNoTracking()
            .Where(x => x.UserId.Equals(request.UserId))
            .SkipWhile(x => !x.Id.Equals(request.PostId))
            .Skip(1)
            .Select(x => new NextPrevPostDto
            {
                Id = x.Id,
                Title = x.Title
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
    #endregion
}
