namespace Blog.Infrastructure.Persistence.Models.Read;

internal class PostReadModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Tags { get; set; }
    public string Body { get; set; }
    public string UserId { get; set; }
    public Guid CategoryId { get; set; }
    public string Image { get; set; }
    public int ViewersCount { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public virtual ICollection<CommentReadModel> Comments { get; set; }
    public virtual CategoryReadModel Category { get; set; }
    public virtual UserReadModel User { get; set; }
}
