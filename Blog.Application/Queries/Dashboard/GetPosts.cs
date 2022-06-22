using Blog.Application.DTOS.Dashboard;
using Blog.Application.Pagination;
using MediatR;

namespace Blog.Application.Queries.Dashboard;

public record GetPosts(int PageNumber, int PageSize, Guid CategoryId, string Search) : IRequest<PaginationModel<BlogPostDto>>;
