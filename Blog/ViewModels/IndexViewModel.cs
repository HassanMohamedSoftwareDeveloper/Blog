using Blog.DTOS;

namespace Blog.ViewModels;

public class IndexViewModel
{
    public int PageNumber { get; set; }
    public int PageCount { get; set; }
    public bool NextPage { get; set; }
    public string Category { get; set; }
    public IEnumerable<PostDto> Posts { get; set; }
    public IEnumerable<int> Pages { get; internal set; }
}
