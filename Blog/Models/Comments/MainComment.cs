namespace Blog.Models.Comments;

public class MainComment : Comment
{
    public virtual ICollection<SubComment> SubComments { get; set; }
}
