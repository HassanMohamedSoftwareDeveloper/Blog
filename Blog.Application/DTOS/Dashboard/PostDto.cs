namespace Blog.Application.DTOS.Dashboard;

public class PostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid CategoryId { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public string TimeAgo { get; set; }
    public string PostDate { get; set; }
    public string Image { get; set; }
    public string Body { get; set; }
    public int ViewersCount { get; set; }
    public string Tags { get; set; }
    public int CommentsCount { get; set; }

    public UserDto User { get; set; }

    public List<CommentDto> Comments { get; set; }
    public List<TagDto> TagsList => Tags.Split(',').Select(x => new TagDto { Tag = x }).ToList();
}
