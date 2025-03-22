using Books_Api.Domain.Interfaces;
using Books_Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Books_Api.Infrastructure.Repository;

public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _appDbContext = context;

    public async Task<T> Add(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula");

        try
        {
            await _appDbContext.Set<T>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al agregar la entidad: {ex.Message}");
            throw new Exception("Ocurrió un error al agregar la entidad.");
        }
    }

    public async Task<T> Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));

        try
        {
            var entity = await _appDbContext.Set<T>().FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"No se encontró la entidad con el ID {id}");

            _appDbContext.Set<T>().Remove(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar la entidad: {ex.Message}");
            throw new Exception("Ocurrió un error al eliminar la entidad.");
        }
    }

    public async Task<IEnumerable<T>> Find(Func<T, bool> predicate) => await Task.FromResult(_appDbContext.Set<T>().Where(predicate).ToList());

    public async Task<T?> GetByIdWithRelations(int id, Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        try
        {
            IQueryable<T> query = _appDbContext.Set<T>();

            if (include != null)
                query = include(query); // Aplica el Include si se proporciona una función

            return await query.FirstOrDefaultAsync(entity => EF.Property<int>(entity, "Id") == id); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener la entidad con relaciones: {ex.Message}");
            throw new Exception("Ocurrió un error al obtener la entidad con relaciones.");
        }
    }
    public async Task<IEnumerable<T>> GetAll(Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        try
        {
            IQueryable<T> query = _appDbContext.Set<T>();

            if (include != null)
                query = include(query); 

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener las entidades: {ex.Message}");
            throw new Exception("Ocurrió un error al obtener las entidades.");
        }
    }

    public async Task<T?> GetById(int id) => await _appDbContext.Set<T>().FindAsync(id);

    public async Task<(IEnumerable<T> Items, int TotalCount)> GetPaged(int pageNumber, int pageSize, string? searchQuery = null)
    {
        if (pageNumber <= 0 || pageSize <= 0)
            throw new ArgumentException("El número de página y el tamaño de página deben ser mayores que cero");

        try
        {
            IQueryable<T> query = _appDbContext.Set<T>();

            if (!string.IsNullOrWhiteSpace(searchQuery))
                query = query.Where(item => item.ToString()!.Contains(searchQuery));

            int totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (items, totalCount);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener las entidades paginadas: {ex.Message}");
            throw new Exception("Ocurrió un error al obtener las entidades paginadas.");
        }
    }

    public async Task<T> Update(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula");

        try
        {
            _appDbContext.Set<T>().Update(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al actualizar la entidad: {ex.Message}");
            throw new Exception("Ocurrió un error al actualizar la entidad.");
        }
    }
}
