namespace Blog.Infrastructure.Persistence.Models.Read;

internal class ReplyReadModel
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string UserId { get; set; }
    public Guid CommentId { get; set; }
    public DateTime Created { get; set; }
    public virtual UserReadModel User { get; set; }
    public virtual CommentReadModel Comment { get; set; }
}
