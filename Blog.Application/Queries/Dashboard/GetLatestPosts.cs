using Blog.Application.DTOS.Dashboard;
using MediatR;

namespace Blog.Application.Queries.Dashboard;

public record GetLatestPosts(int Count) : IRequest<List<LatestPostDto>>;
