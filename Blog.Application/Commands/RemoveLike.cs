using MediatR;

namespace Blog.Application.Commands;
public record RemoveLike(Guid PostId, string UserId) : IRequest<bool>;