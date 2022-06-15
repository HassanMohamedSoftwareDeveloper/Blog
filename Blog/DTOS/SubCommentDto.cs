namespace Blog.DTOS;

public class ReplyDto
{
    public int Id { get; set; }
    public string Message { get; set; }
    public string ReplyDate { get; set; }
    public string TimeAgo { get; set; }
    public UserDto User { get; set; }
}
