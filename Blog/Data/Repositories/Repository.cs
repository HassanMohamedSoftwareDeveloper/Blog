using Blog.DTOS;
using Blog.Helper;
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
    public Post GetPostEntity(int id) => _ctx.Posts
        .Include(x => x.MainComments).ThenInclude(x => x.SubComments)
        .Where(x => x.Id.Equals(id))
        .FirstOrDefault();
    public PostDto GetPost(int id) => _ctx.Posts
        .Include(x => x.MainComments).ThenInclude(x => x.SubComments)
        .Where(x => x.Id.Equals(id))
        .Select(post => MapToPostDto(post))
        .FirstOrDefault();

    public PostDto GetPreviousPost(int id, string userId) => _ctx.Posts
        .Include(x => x.MainComments).ThenInclude(x => x.SubComments)
        .Where(x => x.Id < id)
        .Select(post => MapToPostDto(post))
        .FirstOrDefault();

    public PostDto GetNextPost(int id, string userId) => _ctx.Posts
        .Include(x => x.MainComments).ThenInclude(x => x.SubComments)
        .Where(x => x.Id > id)
        .Select(post => MapToPostDto(post))
        .FirstOrDefault();

    public List<PostDto> GetAllPosts() => _ctx.Posts.Include(x => x.MainComments).ThenInclude(x => x.SubComments)
         .Select(post => MapToPostDto(post)).ToList();
    public List<PostDto> GetLatestPosts(int count)
    {
        return _ctx.Posts.Include(x => x.MainComments).ThenInclude(x => x.SubComments)
            .OrderByDescending(p => p.Category).Take(count)
              .Select(post => MapToPostDto(post)).ToList();
    }
    public IndexViewModel GetAllPosts(int pageNumber, string category)
    {
        Func<Post, bool> InCategory = post => post.Category.ToLower().Equals(category.ToLower());

        var query = _ctx.Posts.AsQueryable();

        if (string.IsNullOrWhiteSpace(category) is false)
            query = query.Where(InCategory).AsQueryable();

        int postsCount = query.Count();

        int pageSize = 4;
        int skipAmount = pageSize * (pageNumber - 1);
        int capacity = skipAmount + pageSize;
        int pageCount = (int)Math.Ceiling((double)postsCount / pageSize);
        return new IndexViewModel
        {
            PageNumber = pageNumber,
            PageCount = pageCount,
            NextPage = postsCount > capacity,
            Pages = PageHelper.PageNumbers(pageNumber, pageCount).ToList(),
            Category = category,
            Posts = query.Include(p => p.MainComments).ThenInclude(x => x.SubComments)
             .Select(post => MapToPostDto(post))
            .Skip(skipAmount).Take(pageSize).ToList(),
        };
    }

    public void AddPost(Post post) => _ctx.Posts.Add(post);
    public void UpdatePost(Post post) => _ctx.Update(post);
    public void RemovePost(int id) => _ctx.Posts.Remove(_ctx.Posts.FirstOrDefault(x => x.Id == id));
    public void AddSubComment(SubComment comment) => _ctx.SubComments.Add(comment);
    public async Task<bool> SaveChangesAsync() => (await _ctx.SaveChangesAsync()) > 0;

    public List<CategoryDto> GetCategories()
    {
        return _ctx.Posts.GroupBy(x => x.Category, x => x.Id)
              .Select(x => new CategoryDto { Description = x.Key, PostsCount = x.Count() }).ToList();
    }

    public List<string> GetTags()
    {
        var tags = _ctx.Posts.Select(x => x.Tags).ToList();
        return tags.SelectMany(x => x.Split(',').ToList()).Distinct().ToList();
    }

    #region Helpers :
    private static PostDto MapToPostDto(Post post)
    {
        return new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body,
            Description = post.Description,
            Category = post.Category,
            Tags = post.Tags.Split(',').ToList(),
            Comments = post.MainComments?.Select(comment => MapToCommentDto(comment)).ToList(),
            CommentsCount = post.MainComments?.Count ?? 0,
            ViewsCount = 0,
            Image = post.Image,
            PostDate = post.Created.ToString("dd MMMMM yyyy"),
            User = new UserDto()
        };
    }
    private static CommentDto MapToCommentDto(MainComment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Message = comment.Message,
            CommentDate = comment.Created.ToString("dd MMMMM yyyy"),
            Replies = comment.SubComments?.Select(reply => MapToReplyDto(reply)).ToList()
        };
    }
    private static ReplyDto MapToReplyDto(SubComment reply)
    {
        return new ReplyDto
        {
            Id = reply.Id,
            Message = reply.Message,
            ReplyDate = reply.Created.ToString("dd MMMMM yyyy")
        };
    }
    #endregion
}
