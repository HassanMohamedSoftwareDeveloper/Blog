using Blog.Application.DTOS.Dashboard;
using MediatR;

namespace Blog.Application.Queries.Dashboard;

public record GetNextPost(Guid PostId, string UserId) : IRequest<NextPrevPostDto>;
