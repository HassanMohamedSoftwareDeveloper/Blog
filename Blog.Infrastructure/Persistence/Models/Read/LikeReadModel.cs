namespace Blog.Infrastructure.Persistence.Models.Read;

internal class LikeReadModel
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public string UserId { get; set; }
    public DateTime Created { get; set; }
    public virtual UserReadModel User { get; set; }
    public virtual PostReadModel Post { get; set; }
}
