using Blog.Models;
using Blog.Models.Comments;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories;

public class Repository : IRepository
{
    private readonly AppDbContext _ctx;

    public Repository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public Post GetPost(int id) => _ctx.Posts
        .Include(x => x.MainComments).ThenInclude(x => x.SubComments)
        .Where(x => x.Id.Equals(id)).FirstOrDefault();
    public List<Post> GetAllPosts() => _ctx.Posts.ToList();
    public List<Post> GetAllPosts(string category)
        => _ctx.Posts.AsQueryable().Where(x => x.Category.ToLower().Equals(category.ToLower())).ToList();
    public void AddPost(Post post) => _ctx.Posts.Add(post);
    public void UpdatePost(Post post) => _ctx.Update(post);
    public void RemovePost(int id) => _ctx.Posts.Remove(GetPost(id));
    public void AddSubComment(SubComment comment) => _ctx.SubComments.Add(comment);
    public async Task<bool> SaveChangesAsync() => (await _ctx.SaveChangesAsync()) > 0;


}
