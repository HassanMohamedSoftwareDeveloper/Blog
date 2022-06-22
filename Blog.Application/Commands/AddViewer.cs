using MediatR;

namespace Blog.Application.Commands;

public record AddViewer(Guid PostId) : IRequest<bool>;
