namespace Blog.Application.DTOS.Admin;

public class PostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string> Tags { get; set; }
    public string Body { get; set; }
    public Guid CategoryId { get; set; }
    public string CurrentImagePath { get; set; }
}
