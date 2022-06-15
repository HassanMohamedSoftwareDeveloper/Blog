using Blog.Models.Comments;

namespace Blog.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string Tags { get; set; }
    public string Category { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public virtual ICollection<MainComment> MainComments { get; set; }
    public virtual ICollection<Viewer> Viewers { get; set; }
    public string UserId { get; set; }
    public virtual User User { get; set; }
}
