namespace Blog.Application.DTOS.Dashboard;

public class LatestPostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public int ViewersCount { get; set; }

    public int CommentsCount { get; set; }
}
