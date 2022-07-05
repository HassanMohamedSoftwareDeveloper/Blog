using Blog.Application.DTOS.Dashboard;
using Blog.Application.Pagination;
using MediatR;

namespace Blog.Application.Queries.Dashboard;

public record SearchPosts(int PageNumber, int PageSize, string Search) : IRequest<PaginationModel<SearchDto>>;
