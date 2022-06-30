namespace Blog.Application.DTOS.Dashboard;

public class CommentDto
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string CommentDate { get; set; }
    public string TimeAgo { get; set; }
    public UserDto User { get; set; }

    public List<ReplyDto> Replies { get; set; }
}