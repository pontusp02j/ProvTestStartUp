using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Core.Services;

public interface IRepositoryBase<T> where T : class
{
    Task<IEnumerable<T>> FindAllAsync();
    Task<T?> FindByIdAsync(object id);
    T Create(T entity);
    Task UpdateByIdAsync(object id, T updatedEntry);
    void Delete(T entity);
    Task<int> SaveChangesAsync();
}

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected DbContextService _dbContext;
    protected IMapper _mapper;

    public RepositoryBase(DbContextService dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<T>> FindAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }
    public async Task<T?> FindByIdAsync(object id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public T Create(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        return entity;
    }

    public async Task UpdateByIdAsync(object id, T updatedEntry)
    {
        var existingEntity = await _dbContext.Set<T>().FindAsync(id);
        if (existingEntity != null)
        {
            _dbContext.Entry(existingEntity).CurrentValues.SetValues(updatedEntry);
        }
        else
        {
            throw new Exception("Entity not found in the database");
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }
}