using Blog.Application.DTOS.Dashboard;
using Blog.Application.Queries.Dashboard;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Read;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.QueryHandlers.Dashboard;

internal sealed class GetPreviousPostHandler : IRequestHandler<GetPreviousPost, NextPrevPostDto>
{
    #region Fields :
    private readonly DbSet<PostReadModel> _posts;
    #endregion

    #region CTORS :
    public GetPreviousPostHandler(ReadDbContext context) => _posts = context.Posts;
    #endregion

    #region Methods :
    public async Task<NextPrevPostDto> Handle(GetPreviousPost request, CancellationToken cancellationToken)
    {
        return await _posts.AsNoTracking()
            .Where(x => x.UserId.Equals(request.UserId))
            .SkipWhile(x => !x.Id.Equals(request.PostId))
            .Select(x => new NextPrevPostDto
            {
                Id = x.Id,
                Title = x.Title
            })
            .LastOrDefaultAsync(cancellationToken);
    }
    #endregion
}
