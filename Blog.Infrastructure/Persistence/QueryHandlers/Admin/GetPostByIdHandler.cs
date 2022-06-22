using Blog.Application.DTOS.Admin;
using Blog.Application.Queries.Admin;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Models.Read;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.QueryHandlers.Admin;

internal sealed class GetPostByIdHandler : IRequestHandler<GetPostById, PostDto>
{
    #region Fields :
    private readonly DbSet<PostReadModel> _posts;
    #endregion

    #region CTORS :
    public GetPostByIdHandler(ReadDbContext context) => _posts = context.Posts;
    #endregion

    #region Methods :
    public async Task<PostDto> Handle(GetPostById request, CancellationToken cancellationToken)
    {
        return await _posts.AsNoTracking().Where(x => x.Id.Equals(request.Id))
            .Select(x => new PostDto { Id = x.Id, Title = x.Title, Body = x.Body, CategoryId = x.CategoryId, Description = x.Description, Tags = x.Tags, CurrentImagePath = x.Image })
            .FirstOrDefaultAsync(cancellationToken);
    }
    #endregion
}
