namespace Books_Api.Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll(Func<IQueryable<T>, IQueryable<T>>? include = null);  
    Task<IEnumerable<T>> Find(Func<T, bool> predicate);
    Task<T?> GetById(int id);
    Task<T?> GetByIdWithRelations(int id, Func<IQueryable<T>, IQueryable<T>>? include = null); 
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(int id);
    Task<(IEnumerable<T> Items, int TotalCount)> GetPaged(int pageNumber, int pageSize, string? searchQuery = null);
}
