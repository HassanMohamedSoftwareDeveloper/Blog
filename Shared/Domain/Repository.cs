﻿using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.Exceptions;

namespace Shared.Abstractions.Domain;

public abstract class Repository<TRoot, TKey> : IRepository<TRoot, TKey> where TRoot : class, IAggregateRoot<TKey>
{
    #region Fields :
    protected readonly DbSet<TRoot> _entities;
    #endregion

    #region CTORS :
    public Repository(DbContext context)
    {
        if (context is null) throw new NullContextException();
        _entities = context.Set<TRoot>();
    }
    #endregion

    #region Methods :
    public async Task<TRoot> GetAsync(Func<TRoot, bool> criteria) => await _entities.Where(criteria).AsQueryable().FirstOrDefaultAsync();
    public void Create(TRoot entity) => _entities.AddAsync(entity);
    public void Update(TRoot entity) => _entities.Update(entity);
    public void Delete(TRoot entity) => _entities.Remove(entity);
    #endregion
}
