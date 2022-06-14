using Blog.Models;

namespace Blog.ViewModels
{
    public class PostVM
    {
        public Post Post { get; set; }
        public IEnumerable<LatestPostsViewModel> LatestPosts { get; set; }
    }
}
