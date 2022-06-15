using Blog.DTOS;
using Blog.Models;
using Blog.Models.Comments;
using Blog.ViewModels;

namespace Blog.Data.Repositories;

public interface IRepository
{
    Post GetPostEntity(int id);
    PostDto GetPost(int id);
    PostDto GetPreviousPost(int id, string userId);
    PostDto GetNextPost(int id, string userId);
    List<PostDto> GetAllPosts();
    List<PostDto> GetLatestPosts(int count);
    IndexViewModel GetAllPosts(int pageNumber, string category, string searcg);
    void AddPost(Post post);
    void UpdatePost(Post post);
    void RemovePost(int id);
    void AddSubComment(SubComment comment);
    void AddViewer(Viewer viewer);
    Task<bool> SaveChangesAsync();
    List<CategoryDto> GetCategories();
    List<string> GetTags();
}
