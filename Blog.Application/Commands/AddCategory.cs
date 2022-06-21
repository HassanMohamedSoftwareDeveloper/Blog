using MediatR;

namespace Blog.Application.Commands;

public record AddCategory(string CategoryName) : IRequest<bool>;
