using Blog.Application.DTOS.Dashboard;
using Blog.Application.Pagination;

namespace Blog.Portal.ViewModels;

public class BlogViewModel
{
    public PaginationModel<BlogPostDto> PaginatedPosts { get; set; }
    public IEnumerable<LatestPostDto> LatestPosts { get; set; }
    public IEnumerable<CategoryDto> Categories { get; set; }
    public IEnumerable<TagDto> Tags { get; set; }
}
