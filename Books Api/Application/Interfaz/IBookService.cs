using Books_Api.Application.Dto.Book;
using Books_Api.Domain.Entities;

namespace Books_Api.Application.Interfaz;
public interface IBookService
{
    Task<Book> GetBookById(int id);
    Task<Book> AddBook(CreateBookDto bookDto);
    Task<Book> UpdateBook(int id, UpdateBookDto bookDto);
    Task<bool> DeleteBook(int id);
}

