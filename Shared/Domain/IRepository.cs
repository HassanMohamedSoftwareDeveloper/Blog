namespace Shared.Abstractions.Domain;

public interface IRepository<TRoot, TKey> where TRoot : class, IAggregateRoot<TKey>
{
    Task<TRoot> GetAsync(Func<TRoot, bool> criteria);
    void Create(TRoot entity);
    void Update(TRoot entity);
    void Delete(TRoot entity);
}
