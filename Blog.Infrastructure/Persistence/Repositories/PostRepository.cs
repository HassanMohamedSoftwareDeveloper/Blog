using Blog.Domain.Aggregates;
using Blog.Domain.Repositories;
using Blog.Domain.ValueObjects;
using Blog.Infrastructure.Persistence.Contexts;
using Shared.Abstractions.Domain;

namespace Blog.Infrastructure.Persistence.Repositories;

internal sealed class PostRepository : Repository<Post, PostId>, IPostRepository
{
    #region CTORS :
    public PostRepository(WriteDbContext context) : base(context)
    {
    }
    #endregion

    #region Methods :
    public async Task<Post> GetAsync(PostId postId) => await base.GetAsync(x => x.Id.Equals(postId.Value));
    #endregion
}
