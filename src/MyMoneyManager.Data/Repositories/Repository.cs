﻿using Microsoft.EntityFrameworkCore;
using MyMoneyManager.Data.DbContexts;
using MyMoneyManager.Data.IRepositories;
using MyMoneyManager.Domain.Commons;

namespace MyMoneyManager.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var entry = await _dbSet.AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        return entry.Entity;
    }


    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        _dbSet.Remove(entity);

        return await _dbContext.SaveChangesAsync() > 0;
    }


    public IQueryable<TEntity> SelectAll()
        => _dbSet;

    public async Task<TEntity> SelectByIdAsync(long id)
        => await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entry = _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();

        return entry.Entity;
    }
}
