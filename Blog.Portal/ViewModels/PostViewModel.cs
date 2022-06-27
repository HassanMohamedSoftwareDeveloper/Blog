using Blog.Application.DTOS.Admin;
using System.ComponentModel;

namespace Blog.Portal.ViewModels;

public class PostViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Tags { get; set; }
    public string Body { get; set; }
    [DisplayName("Post Image")]
    public IFormFile ImageFile { get; set; }
    [DisplayName("Category")]
    public Guid CategoryId { get; set; }

    public IEnumerable<CategoryDto> Categories { get; set; }
}
