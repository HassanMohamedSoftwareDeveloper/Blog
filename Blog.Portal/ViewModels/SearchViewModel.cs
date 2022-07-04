using Blog.Application.DTOS.Dashboard;

namespace Blog.Portal.ViewModels;

public class SearchViewModel
{
    public string Search { get; set; }
    public Guid Category { get; set; }
    public IEnumerable<CategoryDto> Categories { get; set; }
}
