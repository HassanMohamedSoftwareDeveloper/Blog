using Blog.Application.DTOS.Admin;
using MediatR;
namespace Blog.Application.Queries.Admin;

public record GetPostsByUser(int PageNumber, int PageSize, string UserId) : IRequest<List<UserPostDto>>;