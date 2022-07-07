using MediatR;

namespace Blog.Application.Commands;
public record AddLike(Guid PostId, string UserId) : IRequest<bool>;