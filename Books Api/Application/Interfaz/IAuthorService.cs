using Books_Api.Application.Dto.Author;
using Books_Api.Domain.Entities;

namespace Books_Api.Application.Interfaz;
public interface IAuthorService
{
    Task<(IEnumerable<Author> Authors, int TotalCount)> GetAllAuthors(int pageNumber, int pageSize);
    Task<Author> GetAuthorById(int id);
    Task<Author> AddAuthor(CreateAuthorDto authorDto);
    Task<Author> UpdateAuthor(int id, UpdateAuthorDto authorDto);
    Task<bool> DeleteAuthor(int id);
}