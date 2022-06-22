using Blog.Application.DTOS.Dashboard;
using Blog.Application.Pagination;
using MediatR;

namespace Blog.Application.Queries.Dashboard;

public record GetPosts(int PageNumber, int PageSize, string Category, string Search) : IRequest<PaginationModel<BlogPostDto>>;
