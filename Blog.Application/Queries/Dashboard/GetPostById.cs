using Blog.Application.DTOS.Dashboard;
using MediatR;

namespace Blog.Application.Queries.Dashboard;

public record GetPostById(Guid Id) : IRequest<PostDto>;
