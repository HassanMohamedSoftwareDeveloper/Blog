using Blog.Domain.Aggregates;
using Blog.Domain.Entities;
using Blog.Domain.Repositories;
using Blog.Domain.ValueObjects;
using Blog.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
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
    public async Task<Post> GetAsync(PostId postId) => await _entities
        .Include("_likes").Include("_comments").Where(x => x.Id.Equals(postId.Value)).FirstOrDefaultAsync();

    public void DeleteLike(Like like)
    {
        _context.Set<Like>().Remove(like);
    }
    #endregion
}
