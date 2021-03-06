using MediatR;

namespace Blog.Application.Commands;

public record AddPost(string Title, string Description, string Tags, string Body, Stream ImageSourceStream, string UserId, Guid CategoryId) : IRequest<bool>;
