using MediatR;

namespace Blog.Application.Commands;

public record AddComment(Guid PostId, string Message, string UserId) : IRequest<bool>;
