using MediatR;

namespace Blog.Application.Commands;

public record DeletePost(Guid Id) : IRequest<bool>;
