namespace Blog.DTOS;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public List<string> Tags { get; set; }
    public string Category { get; set; }
    public int CommentsCount { get; set; }
    public int ViewsCount { get; set; }
    public string PostDate { get; set; }
    public UserDto User { get; set; }
    public List<CommentDto> Comments { get; set; }
}
