namespace Blog.Application.DTOS.Dashboard;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int PostsCount { get; set; }
}
