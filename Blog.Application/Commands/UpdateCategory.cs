using MediatR;

namespace Blog.Application.Commands;

public record UpdateCategory(Guid Id, string CategoryName) : IRequest<bool>;
