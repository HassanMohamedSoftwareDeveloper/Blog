using Blog.Application.DTOS.Admin;
using MediatR;

namespace Blog.Application.Queries.Admin;

public record GetAllCategories() : IRequest<List<CategoryDto>>;
