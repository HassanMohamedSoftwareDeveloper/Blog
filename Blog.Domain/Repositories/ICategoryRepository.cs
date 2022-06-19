using Blog.Domain.Aggregates;
using Blog.Domain.ValueObjects;
using Shared.Abstractions.Domain;

namespace Blog.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category, CategoryId>
{
}
