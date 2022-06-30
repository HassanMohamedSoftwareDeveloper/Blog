using Blog.Application.DTOS.Dashboard;

namespace Blog.Portal.ViewModels;

public class BlogPostViewModel
{
    public PostDto Post { get; set; }
    public IEnumerable<LatestPostDto> LatestPosts { get; set; }
    public IEnumerable<CategoryDto> Categories { get; set; }
    public IEnumerable<TagDto> Tags { get; set; }
}
