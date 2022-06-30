namespace Blog.Application.DTOS.Dashboard;

public class ReplyDto
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string ReplyDate { get; set; }
    public string TimeAgo { get; set; }
    public UserDto User { get; set; }
}