namespace Blog.Models;

public class Viewer
{
    public int Id { get; set; }
    public DateTime ViewedAt { get; set; } = DateTime.Now;
    public int PostId { get; set; }
    public string UserId { get; set; }
}
