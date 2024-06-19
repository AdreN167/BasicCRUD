using BasicCrud.Domain.Interfaces;

namespace BasicCrud.DAL.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public async Task<T> CreateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("Entity in null");
        }

        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("Entity in null");
        }

        _context.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("Entity in null");
        }

        _context.Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
}

