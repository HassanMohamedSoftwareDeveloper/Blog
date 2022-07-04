using Blog.Application.DTOS.Dashboard;

namespace Blog.Portal.ViewModels;

public class LatestPostViewModel
{
    public IEnumerable<LatestPostDto> LatestPosts { get; set; }
    public string BasePath { get; set; }
}
