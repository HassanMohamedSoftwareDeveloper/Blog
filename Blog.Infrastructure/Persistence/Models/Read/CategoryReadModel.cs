namespace Blog.Infrastructure.Persistence.Models.Read;

internal class CategoryReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<PostReadModel> Posts { get; set; }
}
