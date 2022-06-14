namespace Blog.ViewModels;

public class LatestPostsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int CommentsCount { get; set; }
    public int ViewCount { get; set; }
    public string PostDate { get; set; }
    public string Image { get; set; }
}
