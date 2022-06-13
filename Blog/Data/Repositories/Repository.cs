using Blog.Models;
using Blog.Models.Comments;
using Blog.ViewModels;
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
    public IndexViewModel GetAllPosts(int pageNumber, string category)
    {
        Func<Post, bool> InCategory = post => post.Category.ToLower().Equals(category.ToLower());

        var query = _ctx.Posts.AsQueryable();

        if (string.IsNullOrWhiteSpace(category) is false)
            query = query.Where(InCategory).AsQueryable();

        int postsCount = query.Count();

        int pageSize = 5;
        int skipAmount = pageSize * (pageNumber - 1);
        int capacity = skipAmount + pageSize;
        int pageCount = (int)Math.Ceiling((double)postsCount / pageSize);
        return new IndexViewModel
        {
            PageNumber = pageNumber,
            PageCount = pageCount,
            NextPage = postsCount > capacity,
            Pages = PageNumbers(pageNumber, pageCount),
            Category = category,
            Posts = query.Skip(skipAmount).Take(pageSize).ToList(),
        };
    }
    private static IEnumerable<int> PageNumbers(int pageNumber, int pageCount)
    {
        int midPoint = pageNumber < 3 ? 3 :
            pageNumber > pageCount - 2 ? pageCount - 2 : pageNumber;

        int lowerBound = midPoint - 2;
        int upperBound = midPoint + 2;

        if (lowerBound != 1)
        {
            yield return 1;
            if (lowerBound - 1 > 1)
                yield return -1;
        }

        for (int i = midPoint - 2; i <= midPoint + 2; i++)
            yield return i;

        if (upperBound != pageCount)
        {
            if (pageCount - upperBound > 1)
                yield return -1;
            yield return pageCount;
        }
    }
    public void AddPost(Post post) => _ctx.Posts.Add(post);
    public void UpdatePost(Post post) => _ctx.Update(post);
    public void RemovePost(int id) => _ctx.Posts.Remove(GetPost(id));
    public void AddSubComment(SubComment comment) => _ctx.SubComments.Add(comment);
    public async Task<bool> SaveChangesAsync() => (await _ctx.SaveChangesAsync()) > 0;
}
