using Blog.DTOS;

namespace Blog.ViewModels;

public class PostPageViewModel
{
    public PostDto Post { get; set; }
    public PostDto PreviousPost { get; set; }
    public PostDto NextPost { get; set; }
}
