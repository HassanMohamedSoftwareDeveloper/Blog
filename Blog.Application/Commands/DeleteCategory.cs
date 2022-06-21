using MediatR;

namespace Blog.Application.Commands;

public record DeleteCategory(Guid Id) : IRequest<bool>;
