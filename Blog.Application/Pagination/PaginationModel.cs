namespace Blog.Application.Pagination;

public class PaginationModel<TModel>
{
    public int PageNumber { get; set; }
    public int PagesCount { get; set; }
    public int TotalCount { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public IEnumerable<TModel> Data { get; set; }
    public IEnumerable<int> Pages { get; set; }
}
