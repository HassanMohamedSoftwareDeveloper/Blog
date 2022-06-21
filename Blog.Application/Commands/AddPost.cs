using MediatR;

namespace Blog.Application.Commands;

public record AddPost(string Title, string Description, List<string> Tags, string Body, string ImageSourcePath, string UserId, Guid CategoryId) : IRequest<bool>;
