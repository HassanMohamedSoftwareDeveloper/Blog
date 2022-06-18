using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.Exceptions;

namespace Shared.Abstractions.Domain;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    #region Fields :
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _entities;
    #endregion

    #region CTORS :
    public Repository(DbContext context)
    {
        if (context is null) throw new NullContextException();
        _context = context;
    }
    #endregion

    #region Methods :
    public async Task<TEntity> GetAsync(Func<TEntity, bool> criteria)
        => await _context.Set<TEntity>().Where(criteria).AsQueryable().FirstOrDefaultAsync();
    public void Create(TEntity entity) => _entities.AddAsync(entity);
    public void Update(TEntity entity) => _entities.Update(entity);
    public void Delete(TEntity entity) => _entities.Remove(entity);
    #endregion
}
