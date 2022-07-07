using MediatR;

namespace Blog.Application.Queries.Dashboard;

public record CheckUserLikePost(Guid PostId, string UserId) : IRequest<bool>;
