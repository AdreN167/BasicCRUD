namespace BasicCrud.Domain.Interfaces;

public interface IBaseRepository<T>
{
    IQueryable<T> GetAll();
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}

