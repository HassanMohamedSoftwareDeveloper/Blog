namespace Blog.DTOS;

public class CommentDto
{
    public int Id { get; set; }
    public string Message { get; set; }
    public string CommentDate { get; set; }
    public List<ReplyDto> Replies { get; set; }
}
