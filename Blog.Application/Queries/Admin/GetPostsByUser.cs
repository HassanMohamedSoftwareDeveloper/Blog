using Blog.Application.DTOS.Admin;
using Blog.Application.Pagination;
using MediatR;
namespace Blog.Application.Queries.Admin;

public record GetPostsByUser(int PageNumber, int PageSize, string UserId) : IRequest<PaginationModel<UserPostDto>>;