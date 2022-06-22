using Blog.Application.DTOS.Dashboard;
using Blog.Application.Queries.Dashboard;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Read;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.QueryHandlers.Dashboard;

internal sealed class GetTagsHandler : IRequestHandler<GetTags, List<TagDto>>
{
    #region Fields :
    private readonly DbSet<PostReadModel> _posts;
    #endregion

    #region CTORS :
    public GetTagsHandler(ReadDbContext context) => _posts = context.Posts;
    #endregion

    #region Methods :
    public async Task<List<TagDto>> Handle(GetTags request, CancellationToken cancellationToken)
    {
        var tags = await _posts.AsNoTracking()
            .Select(x => x.Tags)
            .ToListAsync(cancellationToken);

        return tags
            .SelectMany(x => x.Split(',').ToList())
            .Distinct()
            .Select(x => new TagDto { Tag = x })
            .ToList();
    }
    #endregion
}
