namespace Blog.Infrastructure.Persistence.Models.Read;

internal class CommentReadModel
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string UserId { get; set; }
    public DateTime Created { get; set; }
    public virtual UserReadModel User { get; set; }
    public virtual ICollection<ReplyReadModel> Replies { get; set; }
}
