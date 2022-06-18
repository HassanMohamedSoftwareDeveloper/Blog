namespace Shared.Abstractions.Domain;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetAsync(Func<TEntity, bool> criteria);
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
