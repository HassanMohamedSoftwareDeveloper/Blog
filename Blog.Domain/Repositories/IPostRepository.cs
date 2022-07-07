using Blog.Domain.Aggregates;
using Blog.Domain.Entities;
using Blog.Domain.ValueObjects;
using Shared.Abstractions.Domain;

namespace Blog.Domain.Repositories;

public interface IPostRepository : IRepository<Post, PostId>
{
    Task<Post> GetAsync(PostId id);
    void DeleteLike(Like like);
}
