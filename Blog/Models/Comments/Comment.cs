namespace Blog.Models.Comments;

public class Comment
{
    public int Id { get; set; }
    public string Message { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public string UserId { get; set; }
    public virtual User User { get; set; }
}
