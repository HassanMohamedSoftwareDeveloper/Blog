using Blog.Application.DTOS.Dashboard;
using MediatR;

namespace Blog.Application.Queries.Dashboard;

public record SearchPosts(string Search) : IRequest<List<TagDto>>;
