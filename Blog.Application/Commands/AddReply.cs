using MediatR;

namespace Blog.Application.Commands;

public record AddReply(Guid PostId, Guid CommentId, string Message, string UserId) : IRequest<bool>;
