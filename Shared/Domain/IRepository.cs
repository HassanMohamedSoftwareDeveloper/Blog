using System.Linq.Expressions;

namespace Shared.Abstractions.Domain;

public interface IRepository<TRoot, TKey> where TRoot : class, IAggregateRoot<TKey>
{
    Task<TRoot> GetAsync(Expression<Func<TRoot, bool>> criteria);
    void Create(TRoot entity);
    void Update(TRoot entity);
    void Delete(TRoot entity);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}
