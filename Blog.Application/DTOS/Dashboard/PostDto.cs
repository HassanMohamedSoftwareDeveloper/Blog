namespace Blog.Application.DTOS.Dashboard;

public class PostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public string TimeAgo { get; set; }
    public string PostDate { get; set; }
    public string Image { get; set; }
    public int ViewsCount { get; set; }

    public int CommentsCount { get; set; }

    public UserDto User { get; set; }

    public List<CommentDto> Comments { get; set; }
}
